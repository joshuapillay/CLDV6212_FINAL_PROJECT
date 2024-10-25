using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CLDV6212_FINAL_PROJECT.Controllers
{
    public class FileController : Controller
    {
        private readonly string _fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public FileController()
        {
            if (!Directory.Exists(_fileDirectory))
            {
                Directory.CreateDirectory(_fileDirectory);
            }
        }

        // Show the file upload page and list of files
        public IActionResult Index()
        {
            var files = Directory.GetFiles(_fileDirectory);
            ViewBag.Files = files;
            return View();
        }

        // Handle file upload
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(_fileDirectory, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return RedirectToAction("Index");
        }

        // Handle file download
        public IActionResult Download(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return BadRequest("Filename is not provided.");

            var filePath = Path.Combine(_fileDirectory, fileName);
            if (!System.IO.File.Exists(filePath)) return NotFound();

            var mimeType = "application/octet-stream";
            return File(System.IO.File.ReadAllBytes(filePath), mimeType, fileName);
        }
    }
}
