using CatalogR.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogR.Services.SearchInfo
{
    public class SearchComment : Searchable<Comment>
    {
        public override List<Expression<Func<Comment, bool>>> GetPredicates(string query)
        {
            var search = GetSearchString(query);
            return new List<Expression<Func<Comment, bool>>>
            {
                c => EF.Functions.Contains(c.Text, search),
            };
        }

        public override Expression<Func<Comment, Comment>> GetSelector()
        {
            Expression<Func<Comment, Comment>> selector = c =>
            new Comment()
            {
                Id = c.Id,
                Text = c.Text,
                Item = c.Item,
                ItemId = c.ItemId,
            };

            return selector;
        }
    }
}
