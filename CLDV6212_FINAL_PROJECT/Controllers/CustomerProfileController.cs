using Microsoft.AspNetCore.Mvc;
using CLDV6212_FINAL_PROJECT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CLDV6212_FINAL_PROJECT.Controllers
{
    

    public class CustomerProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customerProfiles = await _context.CustomerProfiles.ToListAsync();
            return View(customerProfiles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerProfile customerProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerProfile);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerProfile = await _context.CustomerProfiles.FindAsync(id);
            if (customerProfile == null)
            {
                return NotFound();
            }

            return View(customerProfile);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerProfile = await _context.CustomerProfiles.FindAsync(id);
            _context.CustomerProfiles.Remove(customerProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
