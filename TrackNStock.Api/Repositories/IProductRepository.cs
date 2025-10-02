using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackNStock.Api.Models.DomainModel;

namespace TrackNStock.Api.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int Id);
        Task<Product> CreateProductAsync(Product productDomain);
        Task<Product?> UpdateProductAsync(int id, Product updatedProductDomain);
        Task<List<Product>?> DeleteProductByIdAsync(int id);
    }
}