using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogR.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Collection name is required")]
        [StringLength(255, ErrorMessage = "Collection name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [Column(TypeName = "TEXT")]
        public string? Description { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        [ForeignKey(nameof(Topic))]
        public int? CollectionTopicId { get; set; }
        public virtual CollectionTopic? Topic { get; set; }
    }
}
