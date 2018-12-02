using Microsoft.EntityFrameworkCore;

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
    }
}