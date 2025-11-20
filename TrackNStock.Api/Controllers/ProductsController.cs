using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackNStock.Api.Models.DomainModel;
using TrackNStock.Api.Models.DTOs;
using TrackNStock.Api.Repositories;

namespace TrackNStock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAllProducts()
        {
            var productDomains = await _productRepository.GetAllProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(productDomains);

            return Ok(productDtos);
        }

        [HttpGet]
        [Route("Sold")]
        public async Task<IActionResult> GetAllSoldProducts()
        {
            var productDomains = await _productRepository.GetAllSoldProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(productDomains);

            return Ok(productDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var productDomain = await _productRepository.GetProductByIdAsync(id);
            if (productDomain is null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(productDomain);

            return Ok(productDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var productDomain = await _productRepository.DeleteProductByIdAsync(id);
            if (productDomain is null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<List<ProductDto>>(productDomain);

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(AddProductRequestDto newProduct)
        {
            var productDomain = _mapper.Map<Product>(newProduct);
            productDomain = await _productRepository.CreateProductAsync(productDomain);
            var productDto = _mapper.Map<ProductDto>(productDomain);
            return CreatedAtAction(nameof(GetProductById), new { Id = productDomain.Id }, productDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequestDto updatedProduct)
        {
            var productDomain = _mapper.Map<Product>(updatedProduct);
            productDomain = await _productRepository.UpdateProductByIdAsync(id, productDomain);
            if (productDomain is null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(productDomain);
            return Ok(productDto);
        }

        [HttpPatch("{id}/sell")]
        public async Task<IActionResult> SellProduct(int id)
        {
            await _productRepository.SellProductByIdAsync(id);

            return NoContent();
        }
    }
}