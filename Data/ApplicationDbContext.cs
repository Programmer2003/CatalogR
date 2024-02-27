using CatalogR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CollectionTopic> CollectionTopics { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}