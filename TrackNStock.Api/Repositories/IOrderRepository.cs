using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackNStock.Api.Models.DomainModel;

namespace TrackNStock.Api.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int Id);
        Task<Order> CreateOrderAsync(Order orderDomain);
        Task<Order?> UpdateOrderByIdAsync(int id, Order updatedOrderDomain);
        Task<List<Order>?> DeleteOrderByIdAsync(int id);
    }
}