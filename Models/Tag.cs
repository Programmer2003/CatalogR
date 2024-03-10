using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CatalogR.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(10, ErrorMessage = "Tag name cannot be longer than 10 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
