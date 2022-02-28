﻿// <auto-generated />
using System;
using Bjb.LiquidityGap.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.AuditTrail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Feature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LogDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Module")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferenceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsActive");

                    b.ToTable("AuditTrails", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("IsActive");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.Characteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CalcDay")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("IsActive");

                    b.ToTable("Characteristics", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.CharacteristicFormula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CharacteristicId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Formula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicId");

                    b.HasIndex("IsActive");

                    b.ToTable("CharacteristicFormulas", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.CharacteristicTimebucket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CharacteristicId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<int>("DayRange")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TimebucketId")
                        .HasColumnType("int");

                    b.Property<bool>("UsePercentage")
                        .HasColumnType("bit");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicId");

                    b.HasIndex("IsActive");

                    b.HasIndex("TimebucketId");

                    b.ToTable("CharacteristicTimebuckets", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsActive");

                    b.ToTable("Currencies", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.DataSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConnString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UseETL")
                        .HasColumnType("bit");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsActive");

                    b.ToTable("DataSources", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.LiquidityGap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BussinessDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("Nominal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ScenarioNominal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SheetItemId")
                        .HasColumnType("int");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsActive");

                    b.HasIndex("SheetItemId");

                    b.ToTable("LiquidityGaps", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.LiquidityGapBucket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ActualCalc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ActualPercentage")
                        .HasColumnType("real");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("LiquidityGapId")
                        .HasColumnType("int");

                    b.Property<string>("ScenarioCalc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ScenarioPercentage")
                        .HasColumnType("real");

                    b.Property<int>("TimeBucketId")
                        .HasColumnType("int");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsActive");

                    b.HasIndex("LiquidityGapId");

                    b.HasIndex("TimeBucketId");

                    b.ToTable("LiquidityGapBuckets", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.SheetItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DataSourceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsManualInput")
                        .HasColumnType("bit");

                    b.Property<bool>("MarkToCalculate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SheetItemParentId")
                        .HasColumnType("int");

                    b.Property<string>("Statement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DataSourceId");

                    b.HasIndex("IsActive");

                    b.HasIndex("SheetItemParentId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("SheetItems", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.SheetItemCharacteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CharacteristicId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("SheetItemId")
                        .HasColumnType("int");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicId");

                    b.HasIndex("IsActive");

                    b.HasIndex("SheetItemId");

                    b.ToTable("SheetItemCharacteristics", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("IsActive");

                    b.ToTable("SubCategories", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.SummarySource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<TimeSpan>("BussDate")
                        .HasColumnType("time");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("Nominal")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("Sandi")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("SourceData")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsActive");

                    b.ToTable("SummarySources", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.Timebucket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsActive");

                    b.ToTable("Timebuckets", (string)null);
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.CharacteristicFormula", b =>
                {
                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.Characteristic", "Characteristic")
                        .WithMany("CharacteristicFormulas")
                        .HasForeignKey("CharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Characteristic");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.CharacteristicTimebucket", b =>
                {
                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.Characteristic", "Characteristic")
                        .WithMany("characteristicTimebuckets")
                        .HasForeignKey("CharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.Timebucket", "Timebucket")
                        .WithMany("CharacteristicTimebuckets")
                        .HasForeignKey("TimebucketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Characteristic");

                    b.Navigation("Timebucket");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.LiquidityGap", b =>
                {
                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.SheetItem", "SheetItem")
                        .WithMany()
                        .HasForeignKey("SheetItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SheetItem");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.LiquidityGapBucket", b =>
                {
                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.LiquidityGap", "LiquidityGap")
                        .WithMany("LiquidityGapBuckets")
                        .HasForeignKey("LiquidityGapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.Timebucket", "Timebucket")
                        .WithMany("LiquidityGapBuckets")
                        .HasForeignKey("TimeBucketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LiquidityGap");

                    b.Navigation("Timebucket");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.SheetItem", b =>
                {
                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.DataSource", "DataSource")
                        .WithMany()
                        .HasForeignKey("DataSourceId");

                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.SheetItem", null)
                        .WithMany("SheetChildItems")
                        .HasForeignKey("SheetItemParentId");

                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.SubCategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DataSource");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.SheetItemCharacteristic", b =>
                {
                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.Characteristic", "Characteristic")
                        .WithMany("SheetItemCharacteristics")
                        .HasForeignKey("CharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.SheetItem", "SheetItem")
                        .WithMany("SheetItemCharacteristics")
                        .HasForeignKey("SheetItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Characteristic");

                    b.Navigation("SheetItem");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.SubCategory", b =>
                {
                    b.HasOne("Bjb.LiquidityGap.Domain.Entities.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.Characteristic", b =>
                {
                    b.Navigation("CharacteristicFormulas");

                    b.Navigation("SheetItemCharacteristics");

                    b.Navigation("characteristicTimebuckets");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.LiquidityGap", b =>
                {
                    b.Navigation("LiquidityGapBuckets");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.SheetItem", b =>
                {
                    b.Navigation("SheetChildItems");

                    b.Navigation("SheetItemCharacteristics");
                });

            modelBuilder.Entity("Bjb.LiquidityGap.Domain.Entities.Timebucket", b =>
                {
                    b.Navigation("CharacteristicTimebuckets");

                    b.Navigation("LiquidityGapBuckets");
                });
#pragma warning restore 612, 618
        }
    }
}
