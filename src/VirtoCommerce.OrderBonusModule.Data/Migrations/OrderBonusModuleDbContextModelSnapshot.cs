﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VirtoCommerce.OrderBonusModule.Data.Repositories;

#nullable disable

namespace VirtoCommerce.OrderBonusModule.Data.Migrations
{
    [DbContext(typeof(OrderBonusModuleDbContext))]
    partial class OrderBonusModuleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VirtoCommerce.OrderBonusModule.Data.Models.BonusEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<decimal>("BonusTotal")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("BonusType")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Bonuses", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
