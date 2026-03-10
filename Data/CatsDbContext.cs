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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CatEntity>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CatId).IsRequired().HasMaxLength(100);
                entity.HasIndex(c => c.CatId).IsUnique();
                entity.Property(c => c.Height).IsRequired();
                entity.Property(c => c.Width).IsRequired();
                entity.Property(c => c.Image).IsRequired().HasMaxLength(1000);
                entity.Property(c => c.Created).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<TagEntity>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Created).HasDefaultValueSql("GETDATE()");
            });


            modelBuilder.Entity<CatEntity>(entity => 
            {
                entity.HasMany(c => c.Tags)
                      .WithMany(t => t.Cats)
                      .UsingEntity(j => j.ToTable("CatTags"));
            });
        }
    }
}
