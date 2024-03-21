using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogR.Data;
using CatalogR.Models;
using System.Data;

namespace CatalogR.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null) return NotFound();

            var item = await _context.Items
                .Include(i => i.Tags)
                .Include(i => i.Comments)
                    .ThenInclude(c => c.User)
                .Include(i => i.Likes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            return View(item);
        }
    }
}
