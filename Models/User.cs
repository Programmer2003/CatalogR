using Microsoft.AspNetCore.Identity;

namespace CatalogR.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Collection> Collections { get; } = new List<Collection>();
    }
}
