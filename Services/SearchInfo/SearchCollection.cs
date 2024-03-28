using CatalogR.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Services.SearchInfo
{
    public class SearchCollection : Searchable<Collection>
    {
        public override List<Expression<Func<Collection, bool>>> GetPredicates(string query) =>
            new() {
                c => EF.Functions.Contains(c.Name, GetSearchString(query)),
            };

        public override Expression<Func<Collection, Collection>> Selector
        {
            get
            {
                Expression<Func<Collection, Collection>> selector = c =>
                new ()
                {
                    Id = c.Id,
                    Name = c.Name,
                    TimeStamp = c.TimeStamp
                };

                return selector;
            }
        }
    }
}
