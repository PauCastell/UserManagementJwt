using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserManagerDbContext: DbContext
    {
        public UserManagerDbContext(DbContextOptions<UserManagerDbContext> options)
            : base(options)
        {
        }

        // DbSets:
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email) // Añadir índice único en el campo Email
                .IsUnique();
        }
    }

}
