using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogR.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        public int ItemId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Item? Item { get; set; }

        public List<string?> FullTextIndexPropetries => new()
        {
            Text,
        };
    }
}
