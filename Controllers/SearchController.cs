using CatalogR.Models;
using CatalogR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Controllers
{
    public class SearchController : Controller
    {

        private readonly FullTextSearchService _searchService;

        public SearchController(FullTextSearchService searchService) => _searchService = searchService;

        [HttpPost]
        public async Task<JsonResult> FastSearch(string query)
        {
            var items = await _searchService
                .FastItems(query)
                .AsNoTracking()
                .Select(i => new { i.Id, i.FullTextIndexPropetries, i.Name, Url = "Items/Details" })
                .ToListAsync();
            var collections = await _searchService
                .FastCollections(query)
                .AsNoTracking()
                .Select(c => new { c.Id, c.FullTextIndexPropetries, c.Name, Url = "Collections/Details" })
                .ToListAsync();
            items.AddRange(collections);
            return Json(items);
        }

        public async Task<IActionResult> Index(string? query, string? tag)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                if (string.IsNullOrWhiteSpace(tag)) return NotFound();

                return View(new SearchModel() { Items = await _searchService.SearchByTag(tag).AsNoTracking().ToListAsync(), Query = tag });
            }

            var items = await _searchService.GetAllItems(query)
                                            .OrderByDescending(i => i.TimeStamp)
                                            .AsNoTracking()
                                            .ToListAsync();
            var comments = await _searchService.GetAllComments(query)
                                               .OrderByDescending(c => c.TimeStamp)
                                               .AsNoTracking()
                                               .ToListAsync();
            var collections = await _searchService.GetAllCollections(query)
                                                  .OrderByDescending(c => c.TimeStamp)
                                                  .AsNoTracking()
                                                  .ToListAsync();
            var model = new SearchModel() { Items = items, Collections = collections, Comments = comments, Query = query };
            return View(model);
        }
    }
}
