using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatalogR.Data;
using CatalogR.Models;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace CatalogR.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Items != null ?
                        View(await _context.Items.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Items'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            await Console.Out.WriteLineAsync("TEst");
            var item = await _context.Items.Include(i => i.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public IActionResult Create()
        {
            ItemModel model = new ItemModel();
            model.TagsListItems = _context.Tags.Select(t => t.Name).ToArray();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemModel model)
        {
            model.TagsListItems = _context.Tags.Select(t => t.Name).ToArray();

            if (ModelState.IsValid)
            {
                model.Item = _context.Add(model.Item).Entity;

                model.Item.Tags = await _context.Entry(model.Item).Collection(i => i.Tags).Query().ToListAsync();

                if (!UpdateTags(model))
                {
                    return View(model);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.Include(i => i.Tags).FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            ItemModel model = new ItemModel();
            model.Item = item;
            model.TagsListItems = _context.Tags.Select(t => t.Name).ToArray();
            model.SelectedTags = item.Tags.Select(t => t.Name).ToArray();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemModel model)
        {
            if (id != model.Item.Id)
            {
                return NotFound();
            }

            model.TagsListItems = _context.Tags.Select(t => t.Name).ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    model.Item = _context.Update(model.Item).Entity;

                    model.Item.Tags = await _context.Entry(model.Item).Collection(i => i.Tags).Query().ToListAsync();

                    if (!UpdateTags(model))
                    {
                        return View(model);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(model.Item.Id))
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

            return View(model);
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
                if (selectedTag.Length > 10)
                {
                    ModelState.AddModelError("", "Tag name cannot be longer than 10 characters");
                    return false;
                }

                var newTag = new Tag { Name = selectedTag };
                _context.Tags.Attach(newTag);
                tags.Add(newTag);
            }

            return true;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
