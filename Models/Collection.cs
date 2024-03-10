using CatalogR.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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


        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Collection Image")]
        [MaxFileSize(1*1024*1024)]
        [PermittedExtensions(new string[] {".jpg", ".png", ".jpeg", ".webp"})]
        [NotMapped]
        public virtual IFormFile? ImageFile { get; set; }

        public string? ImageStorageName { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "TEXT")]
        public string? Description { get; set; }

        [NotMapped]
        public string DescriptionMarkdown { get => Markdown.Parse(Description); }

        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        [ForeignKey(nameof(Topic))]
        public int? CollectionTopicId { get; set; }
        public virtual CollectionTopic? Topic { get; set; }
        
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
