using CatalogR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<CollectionTopic> CollectionTopics { get; set; } = null!;
        public DbSet<Collection> Collections { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasMany(u => u.Collections)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .IsRequired();
        }
    }
}