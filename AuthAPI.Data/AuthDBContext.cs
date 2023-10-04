using AuthAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data
{
    public class AuthDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AuthDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(128);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Salt).IsRequired().HasMaxLength(128);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
