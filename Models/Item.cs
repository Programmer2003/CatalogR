using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogR.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Item name is required")]
        [StringLength(255, ErrorMessage = "Item name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public int? CollectionId { get; set; }
        public virtual Collection? Collection { get; set; }
    }
}
