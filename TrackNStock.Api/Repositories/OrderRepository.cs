using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackNStock.Api.Data;
using TrackNStock.Api.Models.DomainModel;

namespace TrackNStock.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TrackNStockDbContext _dbContext;

        public OrderRepository(TrackNStockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var OrderDomains = await _dbContext.Orders.Include(order=>order.ShopOwner).Include(order=>order.Product).ToListAsync();
            return OrderDomains;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var OrderDomain = await _dbContext.Orders.SingleOrDefaultAsync(Order => Order.Id == id);
            if (OrderDomain is null)
            {
                return null;
            }
            return OrderDomain;
        }

        public async Task<Order> CreateOrderAsync(Order OrderDomain)
        {
            await _dbContext.Orders.AddAsync(OrderDomain);
            await _dbContext.SaveChangesAsync();
            return OrderDomain;
        }

        public async Task<Order?> UpdateOrderByIdAsync(int id, Order updatedOrderDomain)
        {
            var existingOrderDomain = await _dbContext.Orders.SingleOrDefaultAsync(Order => Order.Id == id);
            if (existingOrderDomain is null)
            {
                return null;
            }

            existingOrderDomain.OrderDate   = updatedOrderDomain.OrderDate;
            existingOrderDomain.Status      = updatedOrderDomain.Status;
            existingOrderDomain.Quantity    = updatedOrderDomain.Quantity;
            existingOrderDomain.ShopOwnerId = updatedOrderDomain.ShopOwnerId;
            existingOrderDomain.ProductId   = updatedOrderDomain.ProductId;

            await _dbContext.SaveChangesAsync();
            return existingOrderDomain;
        }

        public async Task<List<Order>?> DeleteOrderByIdAsync(int id)
        {
            var existingOrderDomain = await _dbContext.Orders.SingleOrDefaultAsync(Order => Order.Id == id);
            if (existingOrderDomain is null)
            {
                return null;
            }

            _dbContext.Orders.Remove(existingOrderDomain);
            await _dbContext.SaveChangesAsync();

            var OrderDomains = await _dbContext.Orders.ToListAsync();
            return OrderDomains;
        }
    }
}