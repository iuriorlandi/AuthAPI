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
                entity.Property(e => e.UserId).HasColumnName("userid");
                entity.Property(e => e.Username).HasColumnName("username").IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).HasColumnName("passwordhash").IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(200);
                entity.Property(e => e.Salt).HasColumnName("salt").IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
