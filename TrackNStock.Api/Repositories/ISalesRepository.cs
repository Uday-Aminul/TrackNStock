using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackNStock.Api.Models.DomainModel;

namespace TrackNStock.Api.Repositories
{
    public interface ISalesRepository
    {
        Task<List<Sales>> GetAllSalesAsync();
        Task<Sales?> GetSalesByIdAsync(int Id);
        Task<Sales> CreateSalesAsync(Sales SalesDomain);
        Task<Sales?> UpdateSalesByIdAsync(int id, Sales updatedSalesDomain);
        Task<List<Sales>?> DeleteSalesByIdAsync(int id);
    }
}