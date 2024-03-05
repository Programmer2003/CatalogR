using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatalogR.Models
{
    public class ItemModel
    {
        public Item Item { get; set; } = new Item();
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
        public string[] TagsListItems { get; set; } = Array.Empty<string>();

    }
}
