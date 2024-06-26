﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using investmentOrders.DataAccess;

#nullable disable

namespace InvestmentOrdersAPI.Migrations
{
    [DbContext(typeof(DataAccessContext))]
    partial class DataAccessContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InvestmentOrdersAPI.DataAccess.Models.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("AssetTypeId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("InvestmentOrdersAPI.DataAccess.Models.AssetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AssetTypes");
                });

            modelBuilder.Entity("InvestmentOrdersAPI.DataAccess.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("OrderStateId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("OrderStateId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("InvestmentOrdersAPI.DataAccess.Models.OrderState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStates");
                });

            modelBuilder.Entity("InvestmentOrdersAPI.DataAccess.Models.Asset", b =>
                {
                    b.HasOne("InvestmentOrdersAPI.DataAccess.Models.AssetType", "AssetType")
                        .WithMany("Assets")
                        .HasForeignKey("AssetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetType");
                });

            modelBuilder.Entity("InvestmentOrdersAPI.DataAccess.Models.Order", b =>
                {
                    b.HasOne("InvestmentOrdersAPI.DataAccess.Models.Asset", "Asset")
                        .WithMany("Orders")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentOrdersAPI.DataAccess.Models.OrderState", "OrderState")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("OrderState");
                });

            modelBuilder.Entity("InvestmentOrdersAPI.DataAccess.Models.Asset", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("InvestmentOrdersAPI.DataAccess.Models.AssetType", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("InvestmentOrdersAPI.DataAccess.Models.OrderState", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
