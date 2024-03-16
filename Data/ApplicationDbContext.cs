using CatalogR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace CatalogR.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<CollectionTopic> CollectionTopics { get; set; } = null!;
        public DbSet<Collection> Collections { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

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

            builder.Entity<Item>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Items);

            builder.Entity<Collection>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Collection)
                .HasForeignKey(c => c.CollectionId)
            .IsRequired();

            builder.Entity<Comment>()
                .Property(b => b.TimeStamp)
                .HasDefaultValueSql("getdate()");
        }
    }
}