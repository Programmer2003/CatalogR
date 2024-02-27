﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

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
        public string Description { get; set; } = string.Empty;

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
