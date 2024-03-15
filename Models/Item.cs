﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public int? CollectionId { get; set; }
        public virtual Collection? Collection { get; set; }

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

        [Column(TypeName = "TEXT")]
        public string? CustomText1 { get; set; }
        [Column(TypeName = "TEXT")]
        public string? CustomText2 { get; set; }
        [Column(TypeName = "TEXT")]
        public string? CustomText3 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CustomDate1 { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CustomDate2 { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CustomDate3 { get; set; }
    }
}
