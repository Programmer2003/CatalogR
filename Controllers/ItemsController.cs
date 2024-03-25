using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogR.Data;
using CatalogR.Models;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace CatalogR.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ItemsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null) return NotFound();

            var item = await _context.Items
                .Include(i=>i.Collection)
                    .ThenInclude(c => c!.User)
                .Include(i => i.Tags)
                .Include(i => i.Comments.OrderByDescending(c=>c.TimeStamp))
                    .ThenInclude(c => c.User)
                .Include(i => i.Likes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();
            ViewData["OwnsItem"] = item.Collection?.UserId == _userManager.GetUserId(User);

            return View(item);
        }
    }
}
