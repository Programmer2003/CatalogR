using Microsoft.AspNetCore.Mvc;
using CatalogR.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CatalogR.Data;
using CatalogR.Services;

namespace CatalogR.Controllers
{
    public class SearchController : Controller
    {

        private readonly FullTextSearchService _searchService;

        public SearchController(FullTextSearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public JsonResult ItemsSearch(string query)
        {
            var items = _searchService.FastSearch(query).ToList<Item>();
            return Json(items);
        }

        public async Task<IActionResult> Index(string? query)
        {
            if (string.IsNullOrWhiteSpace(query)) return NotFound();

            var collections = await _searchService.GetAllCollections(query).ToListAsync();
            var items = await _searchService.GetAllItems(query).ToListAsync();
            var comments = await _searchService.GetAllComments(query).ToListAsync();
            var model = new SearchModel() { Items = items, Collections = collections, Comments = comments, Query = query };
            return View(model);
        }

        public async Task<IActionResult> Tag(string? tag)
        {
            if (tag == null) return NotFound();

            var items = await _searchService.SearchByTag(tag).ToListAsync();
            var model = new SearchModel() { Items = items, Query = tag};
            return View(nameof(Index), model);
        }
    }
}
