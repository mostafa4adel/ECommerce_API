﻿// <auto-generated />
using ECommerce_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerce_API.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ECommerce_API.Data.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            CategoryName = "Laptops"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Computers"
                        },
                        new
                        {
                            Id = 1,
                            CategoryName = "Electronics"
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "HP"
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Mobiles"
                        },
                        new
                        {
                            Id = 6,
                            CategoryName = "Apple"
                        },
                        new
                        {
                            Id = 7,
                            CategoryName = "Samsung"
                        },
                        new
                        {
                            Id = 8,
                            CategoryName = "TVs"
                        });
                });

            modelBuilder.Entity("ECommerce_API.Data.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProductName = "HP Laptop 15"
                        },
                        new
                        {
                            Id = 2,
                            ProductName = "iPhone 15"
                        },
                        new
                        {
                            Id = 3,
                            ProductName = "Samsung 23"
                        },
                        new
                        {
                            Id = 4,
                            ProductName = "Samsung LED Screen 32"
                        });
                });

            modelBuilder.Entity("ECommerce_API.Data.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("productCategories");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 4
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 5
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 6
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 5
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 8
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 1
                        });
                });

            modelBuilder.Entity("ECommerce_API.Data.ProductCategory", b =>
                {
                    b.HasOne("ECommerce_API.Data.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECommerce_API.Data.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ECommerce_API.Data.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("ECommerce_API.Data.Product", b =>
                {
                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
