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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CatalogR.Controllers
{
    [Authorize]
    [Authorize(Policy = "CollectionAccessPolicy")]
    [Route("[controller]/{collectionId}")]
    public class CollectionItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollectionItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int collectionId)
        {
            if (_context.Items == null) return NotFound();

            var collection = await _context.Collections.Include(c => c.Items).ThenInclude(i => i.Tags).FirstOrDefaultAsync(c => c.Id == collectionId);
            if (collection == null) return NotFound();

            var model = new CollectionListModel();
            model.Items = collection.Items.ToList();
            model.Collection = collection;
            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult Create(int collectionId)
        {
            ItemModel model = new ItemModel();
            model.TagsListItems = _context.Tags.Select(t => t.Name).ToArray();
            model.collectionId = collectionId;
            model.Item.Collection =  _context.Collections.FirstOrDefault(c=> c.Id == collectionId);
            return View(model);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemModel model)
        {
            if (ModelState.IsValid)
            {
                model.Item = _context.Add(model.Item).Entity;
                model.Item.Tags = await _context.Entry(model.Item).Collection(i => i.Tags).Query().ToListAsync();
                model.Item.CollectionId = model.collectionId;
                if (!UpdateTags(model)) return View(model);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { model.collectionId });
            }

            model.TagsListItems = _context.Tags.Select(t => t.Name).ToArray();
            model.Item.Collection = _context.Collections.FirstOrDefault(c => c.Id == model.collectionId);
            return View(model);
        }

        [HttpGet("Edit/{id?}")]
        public async Task<IActionResult> Edit(int collectionId, int? id)
        {
            if (id == null || _context.Items == null) return NotFound();

            var item = await _context.Items.Include(i => i.Tags).FirstOrDefaultAsync(i => i.Id == id);
            if (item == null) return NotFound();

            ItemModel model = new ItemModel() { Item = item };
            model.TagsListItems = _context.Tags.Select(t => t.Name).ToArray();
            model.collectionId = collectionId;
            model.SelectedTags = item.Tags.Select(t => t.Name).ToArray();
            return View(model);
        }

        [HttpPost("Edit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int collectionId, int id, ItemModel model)
        {
            if (id != model.Item.Id) return NotFound();

            model.TagsListItems = _context.Tags.Select(t => t.Name).ToArray();
            model.Item.Collection = _context.Collections.FirstOrDefault(c => c.Id == model.collectionId);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Item = _context.Update(model.Item).Entity;
                    model.Item.Tags = await _context.Entry(model.Item).Collection(i => i.Tags).Query().ToListAsync();
                    model.Item.CollectionId = model.collectionId;
                    if (!UpdateTags(model))
                    {
                        return View(model);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(model.Item.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index), new { model.collectionId });
            }

            return View(model);
        }

        [HttpGet("Delete/{id?}")]
        public async Task<IActionResult> Delete(int collectionId, int? id)
        {
            if (id == null || _context.Items == null) return NotFound();

            var item = await _context.Items
                .Include(i => i.Collection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost("Delete/{id?}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int collectionId, int id)
        {
            if (_context.Items == null) return Problem("Entity set 'ApplicationDbContext.Items'  is null.");

            var item = await _context.Items.FindAsync(id);
            if (item != null) _context.Items.Remove(item);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { collectionId });
        }

        private bool ItemExists(int id)
        {
            return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool UpdateTags(ItemModel model)
        {
            var tags = model.Item.Tags;
            foreach (var tag in tags.ToArray())
            {
                if (!model.SelectedTags.Contains(tag.Name)) tags.Remove(tag);
            }

            var existingTags = _context.Tags.Where(t => model.SelectedTags.Contains(t.Name) && !tags.Contains(t)).ToArray();
            var newTags = model.SelectedTags.Except(existingTags.Union(tags).Select(t => t.Name)).ToArray();
            foreach (var selectedTag in existingTags)
            {
                _context.Tags.Attach(selectedTag);
                tags.Add(selectedTag);
            }

            return CreateAndAttachTags(tags, newTags);
        }

        private bool CreateAndAttachTags(ICollection<Tag> tags, string[] newTags)
        {
            foreach (var selectedTag in newTags)
            {
                if (!CheckTag(selectedTag)) return false;

                var newTag = new Tag { Name = selectedTag };
                _context.Tags.Attach(newTag);
                tags.Add(newTag);
            }

            return true;
        }

        private bool CheckTag(string tagName)
        {
            if (tagName.Length <= 10 && tagName.Length > 2) return true;
            if (tagName.Length > 10) ModelState.AddModelError("", "Tag name cannot be longer than 10 characters");
            else ModelState.AddModelError("", "Tag name cannot be less than 3 characters");
            return false;
        }
    }
}
