using Bjb.LiquidityGap.Domain.Common;
using Bjb.LiquidityGap.Domain.Configurations;
using Bjb.LiquidityGap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bjb.LiquidityGap.Infrastructure.Persistence
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.SetCommandTimeout(300);
        }
        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CharacteristicFormula> CharacteristicFormulas { get; set; }
        public virtual DbSet<Characteristic> Characteristics { get; set; }
        public virtual DbSet<CharacteristicTimebucket> CharacteristicTimebuckets { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<DataSource> DataSources { get; set; }
        public virtual DbSet<Domain.Entities.LiquidityGap> LiquidityGaps { get; set; }
        public virtual DbSet<LiquidityGapBucket> LiquidityGapBuckets{ get; set; }
        public virtual DbSet<SheetItemCharacteristic> SheetItemCharacteristics { get; set; }
        public virtual DbSet<SheetItem> SheetItems { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<SummarySource> SummarySources { get; set; }
       
        public virtual DbSet<Timebucket> Timebuckets { get; set; }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyBaseEntityConfiguration();
            builder.Entity<Category>()
                .HasIndex(u => u.Code)
                .IsUnique();
            builder.Entity<Characteristic>()
                .HasIndex(u => u.Code)
                .IsUnique();
        }
    }
}
