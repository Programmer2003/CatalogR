using CatalogR.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogR.Services.SearchInfo
{
    public class SearchCollection : Searchable<Collection>
    {
        public override List<Expression<Func<Collection, bool>>> GetPredicates(string query)
        {
            var search = GetSearchString(query);
            return new List<Expression<Func<Collection, bool>>>
            {
                c => EF.Functions.Contains(c.Name, search),
            };
        }

        public override Expression<Func<Collection, Collection>> GetSelector()
        {
            Expression<Func<Collection, Collection>> selector = c =>
            new Collection()
            {
                Id = c.Id,
                Name = c.Name,
                TimeStamp = c.TimeStamp
            };

            return selector;
        }
    }
}
