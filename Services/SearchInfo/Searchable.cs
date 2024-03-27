using System.Linq.Expressions;

namespace CatalogR.Services.SearchInfo
{
    public abstract class Searchable<T> 
    {
        abstract public List<Expression<Func<T, bool>>> GetPredicates(string query);

        abstract public Expression<Func<T, T>> GetSelector();

        protected string GetSearchString(string query)
        {
            return $"\"{query}*\"";
        }
    }
}
