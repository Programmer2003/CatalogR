﻿using CatalogR.Data;
using CatalogR.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Data;
using System.Diagnostics;

namespace CatalogR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Items.OrderByDescending(i => i.TimeStamp).Take(5).ToListAsync();
            var collections = await _context.Items
                .Include(i => i.Collection)
                    .ThenInclude(c => c!.User)
                .GroupBy(i => new { i.CollectionId, i.Collection!.Name, i.Collection.ImageUrl })
                .Select(c => new CollectionPreviewModel(){ Id = c.Key.CollectionId, Name = c.Key.Name, ImageUrl = c.Key.ImageUrl, ItemsCount = c.Count() })
                .OrderBy(c => c.ItemsCount)
                .Take(5)
                .ToListAsync();
            var model = new MainPageModel() { Collections = collections };
            return View(model);
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