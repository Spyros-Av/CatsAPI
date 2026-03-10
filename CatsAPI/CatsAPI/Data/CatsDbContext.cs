using CatsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatsAPI.Data
{
    public class CatsDbContext: DbContext
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> dbContextoptions) : base(dbContextoptions)
        {
            
        }

        public DbSet<CatEntity> Cats { get; set; }
        public DbSet<TagEntity> Tags { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<CatEntity>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Cats)
                .UsingEntity(j => j.ToTable("CatTags"));
        }
    }
}
