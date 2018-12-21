using System.Collections.Generic;
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

            modelBuilder.Entity<CalenderItem>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Gallery>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Queue>()
                .HasKey(q => q.Id);

            modelBuilder.Entity<Tournament>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Tournament>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TournamentUser>()
                .HasKey(tu => new {tu.UserId, tu.TournamentId});
            
            modelBuilder.Entity<TournamentUser>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TournamentUsers)
                .HasForeignKey(tu => tu.UserId);

            modelBuilder.Entity<TournamentUser>()
                .HasOne(tu => tu.Tournament)
                .WithMany(t => t.TournamentUsers)
                .HasForeignKey(tu => tu.TournamentId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Queue>()
                .HasOne(q => q.User);
        }
        
        public DbSet<CalenderItem> CalenderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<TournamentUser> TournamentUsers { get; set; }
    }
}