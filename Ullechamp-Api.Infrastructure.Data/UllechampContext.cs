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
        }
        
        public DbSet<CalenderItem> CalenderItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}