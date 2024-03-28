using CatalogR.Data;
using CatalogR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CatalogR.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public CollectionsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var collections = _context.Collections.Include(c => c.Topic).Include(c => c.User).AsNoTracking();
            return View(await collections.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Collections == null) return NotFound();

            var collection = await _context.Collections
                .Include(c => c.Topic)
                .Include(c => c.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            ViewData["OwnsCollection"] = user != null && (collection.UserId == user.Id || user.IsAdmin);
            return View(collection);
        }

        public async Task<IActionResult> ByUser(string? id)
        {
            if (id == null || _context.Collections == null) return NotFound();
            if (id == _userManager.GetUserId(User)) return RedirectToAction("Index", "UserCollections");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();

            var collection = await _context.Collections
                .Where(c => c.UserId == id)
                .Include(c => c.Topic)
                .AsNoTracking()
                .ToListAsync();
            if (collection == null) return NotFound();

            ViewData["UserName"] = user.GetName;
            return View(collection);
        }

        public async Task<IActionResult> Items(int id)
        {
            if (_context.Collections == null) return NotFound();

            var collection = await _context.Collections
                .Include(c => c.Items)
                    .ThenInclude(i => i.Tags)
                .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id);
            if (collection == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            ViewData["OwnsCollection"] = user != null && (collection.UserId == user.Id || user.IsAdmin);
            return View(collection);
        }
    }
}
