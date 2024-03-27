using CatalogR.CustomValidationAttributes;
using CatalogR.Services;
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

        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Collection Image")]
        [MaxFileSize(1 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".webp" })]
        [NotMapped]
        public virtual IFormFile? ImageFile { get; set; }

        public string? ImageStorageName { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "TEXT")]
        public string? Description { get; set; }

        [NotMapped]
        public string DescriptionMarkdown { get => MarkdownService.Parse(Description); }

        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        [ForeignKey(nameof(Topic))]
        public int? CollectionTopicId { get; set; }
        public virtual CollectionTopic? Topic { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Item> Items { get; set; } = new List<Item>();

        public bool CustomInt1_State { get; set; } = false;
        public bool CustomInt2_State { get; set; } = false;
        public bool CustomInt3_State { get; set; } = false;
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomInt1_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomInt2_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomInt3_Name { get; set; }

        public bool CustomString1_State { get; set; } = false;
        public bool CustomString2_State { get; set; } = false;
        public bool CustomString3_State { get; set; } = false;


        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomString1_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomString2_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomString3_Name { get; set; }

        public bool CustomText1_State { get; set; } = false;
        public bool CustomText2_State { get; set; } = false;
        public bool CustomText3_State { get; set; } = false;

        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomText1_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomText2_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomText3_Name { get; set; }

        public bool CustomBool1_State { get; set; } = false;
        public bool CustomBool2_State { get; set; } = false;
        public bool CustomBool3_State { get; set; } = false;

        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomBool1_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomBool2_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomBool3_Name { get; set; }

        public bool CustomDate1_State { get; set; } = false;
        public bool CustomDate2_State { get; set; } = false;
        public bool CustomDate3_State { get; set; } = false;

        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomDate1_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomDate2_Name { get; set; }
        [StringLength(255, ErrorMessage = "Field Name cannot be longer than 255 characters")]
        [DataType(DataType.Text)]
        public string? CustomDate3_Name { get; set; }

        public List<string> GetCustomString()
        {
            List<string> strings = new List<string>();
            if (CustomString1_State && CustomString1_Name != null) strings.Add(CustomString1_Name);
            if (CustomString2_State && CustomString2_Name != null) strings.Add(CustomString2_Name);
            if (CustomString3_State && CustomString3_Name != null) strings.Add(CustomString3_Name);
            return strings;
        }

        public List<string> GetCustomDate()
        {
            List<string> dates = new List<string>();
            if (CustomDate1_State && CustomDate1_Name != null) dates.Add(CustomDate1_Name);
            if (CustomDate2_State && CustomDate2_Name != null) dates.Add(CustomDate2_Name);
            if (CustomDate3_State && CustomDate3_Name != null) dates.Add(CustomDate3_Name);
            return dates;
        }

        public List<string> GetCustomBool()
        {
            List<string> dates = new List<string>();
            if (CustomBool1_State && CustomBool1_Name != null) dates.Add(CustomBool1_Name);
            if (CustomBool2_State && CustomBool2_Name != null) dates.Add(CustomBool2_Name);
            if (CustomBool3_State && CustomBool3_Name != null) dates.Add(CustomBool3_Name);
            return dates;
        }

        public List<string> GetCustomInt()
        {
            List<string> dates = new List<string>();
            if (CustomInt1_State && CustomInt1_Name != null) dates.Add(CustomInt1_Name);
            if (CustomInt2_State && CustomInt2_Name != null) dates.Add(CustomInt2_Name);
            if (CustomInt3_State && CustomInt3_Name != null) dates.Add(CustomInt3_Name);
            return dates;
        }

        public List<string> GetCustomText()
        {
            List<string> dates = new List<string>();
            if (CustomText1_State && CustomText1_Name != null) dates.Add(CustomText1_Name);
            if (CustomText2_State && CustomText2_Name != null) dates.Add(CustomText2_Name);
            if (CustomText3_State && CustomText3_Name != null) dates.Add(CustomText3_Name);
            return dates;
        }

        public List<string?> FullTextIndexPropetries => new List<string?>()
        {
            Name
        };
    }
}
