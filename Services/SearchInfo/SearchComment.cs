using CatalogR.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Services.SearchInfo
{
    public class SearchComment : Searchable<Comment>
    {
        public override List<Expression<Func<Comment, bool>>> GetPredicates(string query) =>
            new()
            {
                c => EF.Functions.Contains(c.Text, GetSearchString(query))
            };

        public override Expression<Func<Comment, Comment>> Selector
        {
            get
            {
                Expression<Func<Comment, Comment>> selector = c =>
                new()
                {
                    Id = c.Id,
                    Text = c.Text,
                    Item = c.Item,
                    ItemId = c.ItemId,
                    TimeStamp = c.TimeStamp
                };

                return selector;
            }
        }
    }
}
