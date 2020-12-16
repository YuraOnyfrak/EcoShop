﻿// <auto-generated />
using System;
using EcoShop.Entrepreneur.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EcoShop.Entrepreneur.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EcoShop.Entrepreneur.Domain.Entity.DocumentProvingQuality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateExpireValidDocument")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfIssueDocument")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdSupplier")
                        .HasColumnType("int");

                    b.Property<string>("NameResponsibleOrganisation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("EcoShop.Entrepreneur.Domain.Entity.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdSupplier")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("EcoShop.Entrepreneur.Domain.Entity.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdSupplier")
                        .HasColumnType("int");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("EcoShop.Entrepreneur.Domain.Entity.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActualAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LegalAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SuppliersTrademark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("EcoShop.Entrepreneur.Domain.Entity.DocumentProvingQuality", b =>
                {
                    b.HasOne("EcoShop.Entrepreneur.Domain.Entity.Supplier", "Supplier")
                        .WithMany("Documents")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("EcoShop.Entrepreneur.Domain.Entity.Manager", b =>
                {
                    b.HasOne("EcoShop.Entrepreneur.Domain.Entity.Supplier", "Supplier")
                        .WithMany("Managers")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("EcoShop.Entrepreneur.Domain.Entity.Photo", b =>
                {
                    b.HasOne("EcoShop.Entrepreneur.Domain.Entity.Supplier", "Supplier")
                        .WithMany("Photos")
                        .HasForeignKey("SupplierId");
                });
#pragma warning restore 612, 618
        }
    }
}
