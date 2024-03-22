namespace CatalogR.Models
{
    public class MainPageModel
    {
        public IEnumerable<CollectionPreviewModel> Collections = new List<CollectionPreviewModel>();
        public IEnumerable<Item> Items = new List<Item>();
    }
}
