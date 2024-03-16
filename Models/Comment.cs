using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogR.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Text is required")]
        [Column(TypeName = "TEXT")]
        public string Text { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public Comment()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
