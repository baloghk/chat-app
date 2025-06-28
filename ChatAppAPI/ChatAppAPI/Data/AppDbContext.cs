using ChatAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAppAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.PublicId).IsUnique();
                entity.Property(e => e.Content).IsRequired().HasMaxLength(100);
                entity.Property(e => e.User).IsRequired().HasMaxLength(100);

                entity.Property(e => e.PublicId)
                    .HasDefaultValueSql("gen_random_uuid()");
            });
        }
    }
}
