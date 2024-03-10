﻿using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CatalogR.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Collection> Collections { get; } = new List<Collection>();
        public bool IsLocked { get; set; } = false;
    }
}
