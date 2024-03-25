namespace CatalogR.Models
{
    public class MainPageModel
    {
        public IEnumerable<CollectionPreviewModel> Collections = new List<CollectionPreviewModel>();
        public IEnumerable<Item> Items = new List<Item>();
        public IEnumerable<Tag> Tags = new List<Tag>();
    }
}
