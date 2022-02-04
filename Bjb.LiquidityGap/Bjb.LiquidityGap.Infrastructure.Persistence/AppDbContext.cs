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
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<SheetItem> SheetItems { get; set; }
        public virtual DbSet<DataSource> DataSources { get; set; }
        public virtual DbSet<Characteristic> Characteristics { get; set; }
        public virtual DbSet<SheetItemCharacteristic> SheetItemCharacteristics { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<SummarySource> SummarySources { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasIndex(u => u.Code)
                .IsUnique();
            builder.Entity<Characteristic>()
                .HasIndex(u => u.Code)
                .IsUnique();
        }
    }
}
