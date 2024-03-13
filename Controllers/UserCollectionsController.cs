using CatalogR.CloudStorage;
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
        private readonly ICloudStorage _cloudStorage;
        public UserCollectionsController(ApplicationDbContext context, UserManager<User> userManager, ICloudStorage cloudStorage)
        {
            _context = context;
            _userManager = userManager;
            _cloudStorage = cloudStorage;
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
        public async Task<IActionResult> Create(Collection collection)
        {
            collection.UserId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                await UpdateImage(collection);

                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Topics"] = new SelectList(_context.CollectionTopics, "Id", "Name", collection.CollectionTopicId);
            return View(collection);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collections == null) return NotFound();

            var collection = await _context.Collections.FindAsync(id);
            if (collection == null) return NotFound();

            ViewData["Topics"] = new SelectList(_context.CollectionTopics, "Id", "Name", collection.CollectionTopicId);
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Collection collection)
        {
            collection.UserId = _userManager.GetUserId(User);
            if (id != collection.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateImage(collection);

                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["Topics"] = new SelectList(_context.CollectionTopics, "Id", "Name", collection.CollectionTopicId);
            return View(collection);
        }

        private async Task UpdateImage(Collection collection)
        {
            if (collection.ImageFile == null) return;

            if (collection.ImageStorageName != null) await _cloudStorage.DeleteFileAsync(collection.ImageStorageName);

            await UploadFile(collection);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Collections == null) return NotFound();

            var collection = await _context.Collections.FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null || collection.UserId != _userManager.GetUserId(User)) return NotFound();

            return View(collection);
        }

        // POST: UserCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Collections == null) return Problem("Entity set 'ApplicationDbContext.Collections'  is null.");

            var collection = await _context.Collections.FindAsync(id);
            if (collection != null)
            {
                if(collection.ImageStorageName != null) await _cloudStorage.DeleteFileAsync(collection.ImageStorageName);

                _context.Collections.Remove(collection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id) => (_context.Collections?.Any(e => e.Id == id)).GetValueOrDefault();

        private async Task UploadFile(Collection collection)
        {
            if (collection.ImageFile == null) return;

            string imageStorageName = FormFileName(collection.Name, collection.ImageFile.FileName);
            collection.ImageUrl = await _cloudStorage.UploadFileAsync(collection.ImageFile, imageStorageName);
            collection.ImageStorageName = imageStorageName;
        }

        private static string FormFileName(string title, string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var fileStorageName = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";
            return fileStorageName;
        }
    }
}
