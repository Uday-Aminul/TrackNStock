using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackNStock.Api.Data;
using TrackNStock.Api.Models.DomainModel;

namespace TrackNStock.Api.Repositories
{
    public class SalesRepository:ISalesRepository
    {
        private readonly TrackNStockDbContext _dbContext;

        public SalesRepository(TrackNStockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Sales>> GetAllSalesAsync()
        {
            var SalesDomains = await _dbContext.Sales.Include(sales=>sales.Order).ToListAsync();
            return SalesDomains;
        }

        public async Task<Sales?> GetSalesByIdAsync(int id)
        {
            var SalesDomain = await _dbContext.Sales.SingleOrDefaultAsync(Sales => Sales.Id == id);
            if (SalesDomain is null)
            {
                return null;
            }
            return SalesDomain;
        }

        public async Task<Sales> CreateSalesAsync(Sales SalesDomain)
        {
            await _dbContext.Sales.AddAsync(SalesDomain);
            await _dbContext.SaveChangesAsync();
            return SalesDomain;
        }

        public async Task<Sales?> UpdateSalesByIdAsync(int id, Sales updatedSalesDomain)
        {
            var existingSalesDomain = await _dbContext.Sales.SingleOrDefaultAsync(Sales => Sales.Id == id);
            if (existingSalesDomain is null)
            {
                return null;
            }

            existingSalesDomain.SalesDate = updatedSalesDomain.SalesDate;
            existingSalesDomain.Quantity  = updatedSalesDomain.Quantity;
            existingSalesDomain.UnitPrice = updatedSalesDomain.UnitPrice;
            existingSalesDomain.OrderId   = updatedSalesDomain.OrderId;

            await _dbContext.SaveChangesAsync();
            return existingSalesDomain;
        }

        public async Task<List<Sales>?> DeleteSalesByIdAsync(int id)
        {
            var existingSalesDomain = await _dbContext.Sales.SingleOrDefaultAsync(Sales => Sales.Id == id);
            if (existingSalesDomain is null)
            {
                return null;
            }

            _dbContext.Sales.Remove(existingSalesDomain);
            await _dbContext.SaveChangesAsync();

            var SalesDomains = await _dbContext.Sales.ToListAsync();
            return SalesDomains;
        }
    }
}