using KerazaWebApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KerazaWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Speaker>(entity =>
            {
                entity.Property(e => e.AddedOn)
                      .HasDefaultValueSql("GETDATE()"); 
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.AddedOn)
                      .HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.AddedOn)
                      .HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<Sermon>(entity =>
            {
                entity.Property(e => e.AddedOn)
                      .HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.AddedOn)
                      .HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.AddedOn)
                      .HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<MediaFile>(entity =>
            {
                entity.Property(e => e.AddedOn)
                      .HasDefaultValueSql("GETDATE()");
            });
        }
        public DbSet<Sermon> Sermons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<Topic> Topics { get; set; }


    }
}
