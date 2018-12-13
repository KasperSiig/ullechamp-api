using Microsoft.EntityFrameworkCore;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Infrastructure.Data
{
    public class UllechampContext : DbContext
    {
        public UllechampContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Wins)
                .HasDefaultValue(0);
            
            modelBuilder.Entity<User>()
                .Property(u => u.Losses)
                .HasDefaultValue(0);
            
            modelBuilder.Entity<User>()
                .Property(u => u.Kills)
                .HasDefaultValue(0);
            
            modelBuilder.Entity<User>()
                .Property(u => u.Deaths)
                .HasDefaultValue(0);
            
            modelBuilder.Entity<User>()
                .Property(u => u.Assists)
                .HasDefaultValue(0);
            
            modelBuilder.Entity<User>()
                .Property(u => u.Kda)
                .HasDefaultValue(0.0);
            
            modelBuilder.Entity<User>()
                .Property(u => u.WinLoss)
                .HasDefaultValue(0);
            
            modelBuilder.Entity<User>()
                .Property(u => u.Point)
                .HasDefaultValue(0);
            
            modelBuilder.Entity<User>()
                .Property(u => u.Rank)
                .HasDefaultValue(0);
        }
        
        public DbSet<CalenderItem> CalenderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
    }
}