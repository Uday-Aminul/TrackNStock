using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackNStock.Api.Data;
using TrackNStock.Api.Models.DomainModel;

namespace TrackNStock.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TrackNStockDbContext _dbContext;

        public ProductRepository(TrackNStockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var productDomains = await _dbContext.Products.ToListAsync();
            return productDomains;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var productDomain = await _dbContext.Products.SingleOrDefaultAsync(product => product.Id == id);
            if (productDomain is null)
            {
                return null;
            }
            return productDomain;
        }

        public async Task<List<Product>> CreateProductAsync(Product productDomain)
        {
            await _dbContext.Products.AddAsync(productDomain);
            await _dbContext.SaveChangesAsync();
            var productDomains = await _dbContext.Products.ToListAsync();
            return productDomains;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product updatedProductDomain)
        {
            var existingProductDomain = await _dbContext.Products.SingleOrDefaultAsync(product => product.Id == id);
            if (existingProductDomain is null)
            {
                return null;
            }

            existingProductDomain.Name = updatedProductDomain.Name;
            existingProductDomain.UnitPrice = updatedProductDomain.UnitPrice;
            existingProductDomain.Quantity = updatedProductDomain.Quantity;
            existingProductDomain.BoughtPrice = updatedProductDomain.BoughtPrice;
            existingProductDomain.Description = updatedProductDomain.Description;

            await _dbContext.SaveChangesAsync();
            return existingProductDomain;
        }

        public async Task<List<Product>?> DeleteProductByIdAsync(int id)
        {
            var existingProductDomain = await _dbContext.Products.SingleOrDefaultAsync(product => product.Id == id);
            if (existingProductDomain is null)
            {
                return null;
            }

            _dbContext.Products.Remove(existingProductDomain);
            await _dbContext.SaveChangesAsync();

            var productDomains = await _dbContext.Products.ToListAsync();
            return productDomains;
        }
    }
}