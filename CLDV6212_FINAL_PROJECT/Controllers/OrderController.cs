using Microsoft.AspNetCore.Mvc;
using CLDV6212_FINAL_PROJECT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CLDV6212_FINAL_PROJECT.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ApplicationDbContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // List all orders
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Fetching all orders...");
            var orders = await _context.Orders
                .Include(o => o.Product)
                .Include(o => o.CustomerProfile)
                .ToListAsync();
            _logger.LogInformation("Orders fetched successfully.");
            return View(orders);
        }

        // Show create order form
        public IActionResult Create()
        {
            _logger.LogInformation("Preparing to create a new order...");

            // Fetch products and customers as strongly-typed lists
            var products = _context.Products.ToList();
            var customers = _context.CustomerProfiles.ToList();

            // Assign to ViewBag for use in the view
            ViewBag.Products = products;
            ViewBag.Customers = customers;

            // Check for empty lists with .Count instead of .Any() to avoid the dynamic issue
            if (products.Count == 0)
            {
                _logger.LogWarning("No products found.");
                ModelState.AddModelError("", "No products found. Please add products before creating an order.");
            }

            if (customers.Count == 0)
            {
                _logger.LogWarning("No customers found.");
                ModelState.AddModelError("", "No customers found. Please add customers before creating an order.");
            }

            _logger.LogInformation("Create order form is ready.");
            return View();
        }


        // Process the creation of an order
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            _logger.LogInformation("Creating a new order...");
            _logger.LogInformation($"Selected ProductId: {order.ProductId}");
            _logger.LogInformation($"Selected CustomerProfileId: {order.CustomerProfileId}");

            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                _context.Add(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order created successfully.");
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors
            _logger.LogWarning("Model validation failed.");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError($"Validation error: {error.ErrorMessage}");
            }

            // Reload dropdown lists if validation fails
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Customers = _context.CustomerProfiles.ToList();
            return View(order);
        }

        // Delete an order
        // GET: Display delete confirmation view
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Order deletion failed. No OrderId provided.");
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Product)
                .Include(o => o.CustomerProfile)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                _logger.LogWarning($"Order with id {id} not found.");
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation($"Deleting order with id {id}...");
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                _logger.LogWarning($"Order with id {id} not found.");
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Order with id {id} deleted successfully.");
            return RedirectToAction(nameof(Index));
        }





    }
}
