using System.ComponentModel.DataAnnotations;

namespace CatalogR.Models
{
    public class CollectionTopic
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Topic Name")]
        [Required(ErrorMessage = "Topic name is required")]
        [StringLength(255, ErrorMessage = "Topic cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;
    }
}
