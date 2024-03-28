using System.Linq.Expressions;

namespace CatalogR.Services.SearchInfo
{
    public abstract class Searchable<T>
    {
        public abstract List<Expression<Func<T, bool>>> GetPredicates(string query);

        public abstract Expression<Func<T, T>> Selector { get; }

        protected string GetSearchString(string query) => $"\"{query}*\"";
    }
}
