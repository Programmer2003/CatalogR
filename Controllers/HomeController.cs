using CatalogR.Data;
using CatalogR.Models;
using System.Diagnostics;
using Syncfusion.Blazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;

namespace CatalogR.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var collections = await _context.Items
                .Include(i => i.Collection)
                    .ThenInclude(c => c!.User)
                .GroupBy(i => new { i.CollectionId, i.Collection!.Name, i.Collection.ImageUrl })
                .Select(c => new CollectionPreviewModel() { Id = c.Key.CollectionId, Name = c.Key.Name, ImageUrl = c.Key.ImageUrl, ItemsCount = c.Count() })
                .OrderByDescending(c => c.ItemsCount)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();

            var items = await _context.Items
                .Include(i => i.Collection)
                    .ThenInclude(c => c!.User)
                .OrderByDescending(i => i.TimeStamp)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();

            var tags = await _context.Tags
                .Select(t => new { t.Id, t.Name, t.Items.Count })
                .OrderByDescending(t => t.Count)
                .Select(t => new Tag { Id = t.Id, Name = t.Name })
                .Take(10)
                .AsNoTracking()
                .ToListAsync();

            return View(new MainPageModel() { Collections = collections, Items = items, Tags = tags });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                ViewBag.ErrorMessage = "404 Page Not Found Exception";
                return View("404");
            }

            ViewBag.ErrorMessage = $"Error {statusCode}";
            return View("ErrorStatus");
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}