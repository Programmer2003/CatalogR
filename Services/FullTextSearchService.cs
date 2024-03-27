using CatalogR.Data;
using CatalogR.Models;
using CatalogR.Services.SearchInfo;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Services
{
    public class FullTextSearchService
    {
        private readonly ApplicationDbContext _context;
        private SearchItem searchItem;
        private SearchComment searchComment;
        private SearchCollection searchCollection;
        public FullTextSearchService(ApplicationDbContext context)
        {
            _context = context;
            searchItem = new SearchItem();
            searchComment = new SearchComment();
            searchCollection = new SearchCollection();
        }

        public IQueryable<Item> SearchByTag(string tagName)
        {
            return _context.Items.Where(i => i.Tags.Any(t => t.Name == tagName));
        }

        public IQueryable<T> FastSearch<T>(string query, DbSet<T> dbSet, Searchable<T> searchable, int currentPage = 1, int pageSize = 5)
        where T : class
        {
            if (pageSize <= 0) pageSize = 5;
            if (currentPage <= 0) currentPage = 1;

            return GetAll(query, dbSet, searchable)
                .OrderBy(c => "Id")
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);
        }

        public IQueryable<Item> FastItems(string query)
        {
            return FastSearch(query, _context.Items, searchItem, 1, 3);
        }
        public IQueryable<Collection> FastCollections(string query)
        {
            return FastSearch(query, _context.Collections, searchCollection, 1, 3);
        }

        private IQueryable<T> GetAll<T>(string query, DbSet<T> dbSet, Searchable<T> searchable)
            where T : class
        {
            var predicate = PredicateBuilder.New<T>();
            searchable.GetPredicates(query).ForEach(p => predicate = predicate.Or(p));
            return dbSet
                .Where(predicate)
                .Select(searchable.GetSelector());
        }


        public IQueryable<Item> GetAllItems(string query)
        {
            return GetAll(query, _context.Items, searchItem);
        }
        public IQueryable<Collection> GetAllCollections(string query)
        {
            return GetAll(query, _context.Collections, searchCollection);
        }

        public IQueryable<Comment> GetAllComments(string query)
        {
            return GetAll(query, _context.Comments, searchComment);
        }
    }
}
