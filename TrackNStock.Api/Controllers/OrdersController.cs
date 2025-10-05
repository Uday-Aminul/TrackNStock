using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrackNStock.Api.Models.DomainModel;
using TrackNStock.Api.Models.DTOs;
using TrackNStock.Api.Repositories;

namespace TrackNStock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IMapper _mapper;
        public OrdersController(IOrderRepository OrderRepository, IMapper mapper)
        {
            _OrderRepository = OrderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var OrderDomains = await _OrderRepository.GetAllOrdersAsync();
            var OrderDtos = _mapper.Map<List<OrderDto>>(OrderDomains);

            return Ok(OrderDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var OrderDomain = await _OrderRepository.GetOrderByIdAsync(id);
            if (OrderDomain is null)
            {
                return NotFound();
            }
            var OrderDto = _mapper.Map<OrderDto>(OrderDomain);

            return Ok(OrderDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            var OrderDomain = await _OrderRepository.DeleteOrderByIdAsync(id);
            if (OrderDomain is null)
            {
                return NotFound();
            }
            var OrderDto = _mapper.Map<OrderDto>(OrderDomain);

            return Ok(OrderDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(AddOrderRequestDto newOrder)
        {
            var OrderDomain = _mapper.Map<Order>(newOrder);
            OrderDomain = await _OrderRepository.CreateOrderAsync(OrderDomain);
            var OrderDto = _mapper.Map<OrderDto>(OrderDomain);
            return Ok(OrderDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequestDto updatedOrder)
        {
            var OrderDomain = _mapper.Map<Order>(updatedOrder);
            OrderDomain = await _OrderRepository.UpdateOrderByIdAsync(id, OrderDomain);
            if (OrderDomain is null)
            {
                return NotFound();
            }
            var OrderDto = _mapper.Map<OrderDto>(OrderDomain);
            return Ok(OrderDto);
        }
    }
}