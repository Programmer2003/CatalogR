using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatalogR.Data;
using CatalogR.Models;
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
            var collections = _context.Collections.Include(c => c.Topic).Include(c => c.User);
            return View(await collections.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Collections == null) return NotFound();

            var collection = await _context.Collections
                .Include(c => c.Topic)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null) return NotFound();

            ViewData["OwnsCollection"] = collection.UserId == _userManager.GetUserId(User);
            return View(collection);
        }

        public async Task<IActionResult> ByUser(string? id)
        {
            if (id == null || _context.Collections == null) return NotFound();
            if (id == _userManager.GetUserId(User)) return RedirectToAction("Index", "UserCollections");

            var collection = await _context.Collections
                .Where(c => c.UserId == id)
                .Include(c => c.Topic)
                .ToListAsync();
            if (collection == null) return NotFound();

            return View(collection);
        }

        public async Task<IActionResult> Items(int id)
        {
            if (_context.Items == null) return NotFound();

            var collection = await _context.Collections.Include(c => c.Items).ThenInclude(i => i.Tags).FirstOrDefaultAsync(c => c.Id == id);
            if (collection == null) return NotFound();
            if (collection.UserId == _userManager.GetUserId(User)) return RedirectToAction("Index", "CollectionItems", new { collectionId = collection.Id });

            var model = new CollectionListModel()
            {
                collectionId = id,
                Collection = collection,
                Items = collection.Items.ToList()
            };
            ViewData["OwnsCollection"] = collection.UserId == _userManager.GetUserId(User);
            return View(model);
        }
    }
}
