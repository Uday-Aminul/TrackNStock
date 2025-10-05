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
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        public SalesController(ISalesRepository SalesRepository, IMapper mapper)
        {
            _salesRepository = SalesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSaless()
        {
            var SalesDomains = await _salesRepository.GetAllSalesAsync();
            var SalesDtos = _mapper.Map<List<SalesDto>>(SalesDomains);

            return Ok(SalesDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSalesById(int id)
        {
            var SalesDomain = await _salesRepository.GetSalesByIdAsync(id);
            if (SalesDomain is null)
            {
                return NotFound();
            }
            var SalesDto = _mapper.Map<SalesDto>(SalesDomain);

            return Ok(SalesDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSalesById(int id)
        {
            var SalesDomains = await _salesRepository.DeleteSalesByIdAsync(id);
            if (SalesDomains is null)
            {
                return NotFound();
            }
            var SalesDto = _mapper.Map<List<SalesDto>>(SalesDomains);

            return Ok(SalesDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSales(AddSalesRequestDto newSales)
        {
            var SalesDomain = _mapper.Map<Sales>(newSales);
            SalesDomain = await _salesRepository.CreateSalesAsync(SalesDomain);
            var SalesDto = _mapper.Map<SalesDto>(SalesDomain);
            return CreatedAtAction(nameof(GetSalesById), new { Id = SalesDomain.Id }, SalesDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateSales(int id, UpdateSalesRequestDto updatedSales)
        {
            var SalesDomain = _mapper.Map<Sales>(updatedSales);
            SalesDomain = await _salesRepository.UpdateSalesByIdAsync(id, SalesDomain);
            if (SalesDomain is null)
            {
                return NotFound();
            }
            var SalesDto = _mapper.Map<SalesDto>(SalesDomain);
            return Ok(SalesDto);
        }
    }
}