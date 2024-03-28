using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CatalogR.Models
{
    [Index(nameof(Name), IsUnique = true)]
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
