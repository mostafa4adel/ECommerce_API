using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECommerce_API.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=ecommerce_db;user=root;password=root;", new MySqlServerVersion(new Version(8, 2, 0)));
            }
        }


        public DatabaseContext() : this(
            new DbContextOptionsBuilder<DatabaseContext>()
                .UseMySql("server=localhost;database=ecommerce_db;user=root;password=root;", new MySqlServerVersion(new Version(8, 2, 0)))
                .Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
  
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            SeedData(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> productCategories { get; set; }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed categories
            var electronicsCategory = new Category { Id = 1, CategoryName = "Electronics" };
            var laptopsCategory = new Category { Id = 2, CategoryName = "Laptops" };
            var computersCategory = new Category { Id = 3, CategoryName = "Computers" };
            var hpCategory = new Category { Id = 4, CategoryName = "HP" };
            var mobileCategory = new Category { Id = 5, CategoryName = "Mobiles" };
            var appleCategory = new Category { Id = 6, CategoryName = "Apple" };
            var samsungCategory = new Category { Id = 7, CategoryName = "Samsung" };
            var tvsCategory = new Category { Id = 8, CategoryName = "TVs" };


            modelBuilder.Entity<Category>().HasData(laptopsCategory, computersCategory, electronicsCategory, hpCategory, mobileCategory
                , appleCategory, samsungCategory, tvsCategory);

            // Seed product
            var hpLaptopProduct = new Product { Id = 1, ProductName = "HP Laptop 15" };
            var iphoneProduct = new Product { Id = 2, ProductName = "iPhone 15" };
            var samsungProduct = new Product { Id = 3, ProductName = "Samsung 23" };
            var samsungTVProduct = new Product { Id = 4, ProductName = "Samsung LED Screen 32" };

            modelBuilder.Entity<Product>().HasData(hpLaptopProduct,iphoneProduct, samsungProduct, samsungTVProduct);

            // Establish many-to-many relationships
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductId = hpLaptopProduct.Id, CategoryId = laptopsCategory.Id },
                new ProductCategory { ProductId = hpLaptopProduct.Id, CategoryId = computersCategory.Id },
                new ProductCategory { ProductId = hpLaptopProduct.Id, CategoryId = electronicsCategory.Id },
                new ProductCategory { ProductId = hpLaptopProduct.Id, CategoryId = hpCategory.Id }
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductId = iphoneProduct.Id, CategoryId = mobileCategory.Id },
                new ProductCategory { ProductId = iphoneProduct.Id, CategoryId = appleCategory.Id },
                new ProductCategory { ProductId = iphoneProduct.Id, CategoryId = electronicsCategory.Id }
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductId = samsungProduct.Id, CategoryId = mobileCategory.Id },
                new ProductCategory { ProductId = samsungProduct.Id, CategoryId = samsungProduct.Id },
                new ProductCategory { ProductId = samsungProduct.Id, CategoryId = electronicsCategory.Id }
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductId = samsungTVProduct.Id, CategoryId = tvsCategory.Id },
                new ProductCategory { ProductId = samsungTVProduct.Id, CategoryId = samsungProduct.Id },
                new ProductCategory { ProductId = samsungTVProduct.Id, CategoryId = electronicsCategory.Id }
            );


        }
    }

    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseMySql("server=localhost;database=ecommerce_db;user=root;password=root;", new MySqlServerVersion(new Version(8, 2, 0)));

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
