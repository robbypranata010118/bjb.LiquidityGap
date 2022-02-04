using Bjb.LiquidityGap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bjb.LiquidityGap.Infrastructure.Persistence
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.SetCommandTimeout(300);

        }
        public DbSet<Category> Categories;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
            .HasIndex(u => u.Code)
            .IsUnique();
        }
    }
}
