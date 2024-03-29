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
        public int? CollectionId { get; set; }
        [Display(Name = "Collection")]
        public Collection? Collection { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        [NotMapped]
        public int LikesCount { get => Likes.Count; }

        public int? CustomInt1 { get; set; }
        public int? CustomInt2 { get; set; }
        public int? CustomInt3 { get; set; }

        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "String field cannot be longer than 255 characters")]
        public string? CustomString1 { get; set; }
        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "String field cannot be longer than 255 characters")]
        public string? CustomString2 { get; set; }
        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "String field cannot be longer than 255 characters")]
        public string? CustomString3 { get; set; }

        public bool CustomBool1 { get; set; } = false;
        public bool CustomBool2 { get; set; } = false;
        public bool CustomBool3 { get; set; } = false;

        [Column(TypeName = "NTEXT")]
        public string? CustomText1 { get; set; }
        [Column(TypeName = "NTEXT")]
        public string? CustomText2 { get; set; }
        [Column(TypeName = "NTEXT")]
        public string? CustomText3 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime? CustomDate1 { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime? CustomDate2 { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime? CustomDate3 { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        public List<string?> FullTextIndexPropetries => new()
        {
            Name,
            CustomString1,
            CustomString2,
            CustomString3,
        };

        [NotMapped]
        public string TagsString
        {
            get => string.Join(",", Tags.Select(t=>t.Name));
        }
    }
}
