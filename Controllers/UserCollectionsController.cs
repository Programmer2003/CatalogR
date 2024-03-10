using CatalogR.Data;
using CatalogR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Controllers
{
    [Authorize]
    public class UserCollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserCollectionsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _context.Users.Include(u => u.Collections).ThenInclude(u => u.Topic).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound();

            var collections = user.Collections.ToList();
            return View(collections);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Topics"] = new SelectList(_context.CollectionTopics, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,UserId,CollectionTopicId")] Collection collection)
        {
            collection.UserId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Topics"] = new SelectList(_context.CollectionTopics, "Id", "Name", collection.CollectionTopicId);
            return View(collection);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collections == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            ViewData["Topics"] = new SelectList(_context.CollectionTopics, "Id", "Name", collection.CollectionTopicId);
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CollectionTopicId")] Collection collection)
        {
            collection.UserId = _userManager.GetUserId(User);
            if (id != collection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Topics"] = new SelectList(_context.CollectionTopics, "Id", "Name", collection.CollectionTopicId);
            return View(collection);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Collections == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null || collection.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: UserCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Collections == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Collections'  is null.");
            }
            var collection = await _context.Collections.FindAsync(id);
            if (collection != null)
            {
                _context.Collections.Remove(collection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id)
        {
            return (_context.Collections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
