namespace CatalogR.Models
{
    public class CollectionPreviewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int ItemsCount { get; set; }
    }
}
