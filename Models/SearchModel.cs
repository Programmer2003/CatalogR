namespace CatalogR.Models
{
    public class SearchModel
    {
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
        public IEnumerable<Collection> Collections { get; set; } = new List<Collection>();
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
        public string Query { get; set; } = string.Empty;
    }
}
