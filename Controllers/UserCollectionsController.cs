﻿using CatalogR.Data;
using CatalogR.Models;
using CatalogR.Services;
using CatalogR.CloudStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CatalogR.Controllers
{
    [Authorize]
    [Authorize(Policy = "UserCollectionPolicy")]
    public class UserCollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly HtmlSanitizationService _sanitizer;
        private readonly ICloudStorage _cloudStorage;
        private readonly TopicsLocalizerService _topicLocalizer;
        public UserCollectionsController(ApplicationDbContext context, UserManager<User> userManager, ICloudStorage cloudStorage, HtmlSanitizationService sanitizer, TopicsLocalizerService topicLocalizer)
        {
            _context = context;
            _userManager = userManager;
            _sanitizer = sanitizer;
            _cloudStorage = cloudStorage;
            _topicLocalizer = topicLocalizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? UserId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userId = currentUser.Id;
            if (!string.IsNullOrEmpty(UserId) && currentUser.IsAdmin)
            {
                userId = UserId;
                ViewData["UserId"] = userId;
            }

            var user = await _context.Users.Include(u => u.Collections).ThenInclude(u => u.Topic).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound();

            var collections = user.Collections.ToList();
            return View(collections);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string? userId)
        {
            ViewData["Topics"] = new SelectList(_context.CollectionTopics.OrderBy(t=>t.Id).Select(t=> new {t.Id, Name = _topicLocalizer.GetString(t.Name) }), "Id", "Name");
            var collection = new Collection();
            var currentUser = await _userManager.GetUserAsync(User);
            if (!string.IsNullOrEmpty(userId) && currentUser.IsAdmin)
                collection.UserId = userId;
            else
                collection.UserId = currentUser.Id;

            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Collection collection)
        {
            if ((await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == collection.UserId)) == null)
                collection.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                collection.Description = _sanitizer.Sanitize(collection.Description);
                await UpdateImage(collection);
                _context.Add(collection);
                await _context.SaveChangesAsync();
                if (collection.UserId == _userManager.GetUserId(User))
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Index), new { collection.UserId });

            }

            ViewData["Topics"] = new SelectList(_context.CollectionTopics.OrderBy(t => t.Id).Select(t => new { t.Id, Name = _topicLocalizer.GetString(t.Name) }), "Id", "Name", collection.CollectionTopicId);
            return View(collection);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collections == null) return NotFound();

            var collection = await _context.Collections.FindAsync(id);
            if (collection == null) return NotFound();

            ViewData["Topics"] = new SelectList(_context.CollectionTopics.OrderBy(t => t.Id).Select(t => new { t.Id, Name = _topicLocalizer.GetString(t.Name) }), "Id", "Name", collection.CollectionTopicId);
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Collection collection)
        {
            if (id != collection.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    collection.Description = _sanitizer.Sanitize(collection.Description);
                    await UpdateImage(collection);
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id)) return NotFound();
                    else throw;
                }

                if (collection.UserId == _userManager.GetUserId(User))
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Index), new { collection.UserId });
            }

            ViewData["Topics"] = new SelectList(_context.CollectionTopics.OrderBy(t => t.Id).Select(t => new { t.Id, Name = _topicLocalizer.GetString(t.Name) }), "Id", "Name", collection.CollectionTopicId);
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
            if (collection == null) return NotFound();

            var count = _context.Items.Where(i => i.CollectionId == collection.Id).Count();
            ViewData["ItemsCount"] = count.ToString();
            return View(collection);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Collections == null) return Problem("Entity set 'ApplicationDbContext.Collections'  is null.");

            var collection = await _context.Collections.FindAsync(id);
            if (collection != null)
            {
                if (collection.ImageStorageName != null) await _cloudStorage.DeleteFileAsync(collection.ImageStorageName);

                _context.Collections.Remove(collection);
            }

            await _context.SaveChangesAsync();
            if (collection != null && collection.UserId == _userManager.GetUserId(User))
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction(nameof(Index), new { collection?.UserId });
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
            return $"{title}-{DateTime.Now:yyyyMMddHHmmss}{extension}";
        }
    }
}
