using CatalogR.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace CatalogR.Controllers
{
    [Authorize]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .AsNoTracking()
                .OrderBy(u=>u.Id)
                .ToListAsync();

            return View(users);
        }

        [HttpPost]
        public async Task<JsonResult> UsersChangeBlockStatus(string data, bool block)
        {
            var list = JsonConvert.DeserializeObject<List<string>>(data);
            if (list == null) return Json(HttpStatusCode.OK);

            var users = await _context.Users.Where(u => list.Contains(u.Id)).ToListAsync();
            users.ForEach(u => u.IsLocked = block);
            await _context.SaveChangesAsync();
            return Json(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteUsers(string data)
        {
            var list = JsonConvert.DeserializeObject<List<string>>(data);
            if (list == null) return Json(HttpStatusCode.OK);

            _context.Comments.RemoveRange(_context.Comments.Where(c => list.Contains(c.UserId!)));
            _context.Users.RemoveRange(_context.Users.Where(u => list.Contains(u.Id)));
            await _context.SaveChangesAsync();
            return Json(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<JsonResult> UsersChangeRole(string data, bool admin)
        {
            var list = JsonConvert.DeserializeObject<List<string>>(data);
            if (list == null) return Json(HttpStatusCode.OK);

            var users = await _context.Users.Where(u => list.Contains(u.Id)).ToListAsync();
            users.ForEach(u => u.IsAdmin = admin);
            await _context.SaveChangesAsync();
            return Json(HttpStatusCode.OK);
        }
    }
}
