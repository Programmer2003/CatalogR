namespace CatalogR.Models
{
    public class CollectionListModel
    {
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
        public Collection Collection { get; set; } = new Collection();

        public int collectionId;
    }
}
