using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackNStock.Api.Models.DomainModel;

namespace TrackNStock.Api.Data
{
    public class TrackNStockDbContext : DbContext
    {
        public TrackNStockDbContext(DbContextOptions<TrackNStockDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ShopOwner> ShopOwners { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Sales> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var productDemo = new Product()
            {
                Id = 8801,
                Name = "Product Name Demo",
                BoughtPrice = 100,
                UnitPrice = 110,
                Quantity = 1,
            };
            modelBuilder.Entity<Product>().HasData(productDemo);

            var shopOwnerDemo = new ShopOwner()
            {
                Id = 9901,
                ShopName = "Shop Name Demo",
                OwnerName = "Owner Name Demo",
                Location = "Location Demo",
                PhoneNumber = "Phone Number Demo"
            };
            modelBuilder.Entity<ShopOwner>().HasData(shopOwnerDemo);

            var orderDemo = new Order()
            {
                Id = 7701,
                OrderDate = new DateTime(2025, 10, 1, 14, 30, 0),
                Status = "Pending",
                Quantity = 1,
                ShopOwnerId = 9901,
                ProductId = 8801
            };
            modelBuilder.Entity<Order>().HasData(orderDemo);

            var salesDemo = new Sales()
            {
                Id = 6601,
                Quantity = 1,
                UnitPrice = 110,
                SalesDate = new DateTime(2025, 10, 1, 15, 0, 0),
                ProductId = 8801,
                ShopOwnerId = 9901,
                OrderId = 7701
            };
        }
    }
}