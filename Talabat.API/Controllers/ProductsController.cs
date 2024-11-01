﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo, IMapper mapper)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
        }

        // Get All Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var Spec = new ProductWithBrandAndTypeSpecifications();

            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);
            //OkObjectResult result = new OkObjectResult(Products);
            //return result;

            return Ok(MappedProducts);

        }

        // Get Product By Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);

            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);
            if(Product is null)  return NotFound(new ApiResponse(404));
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);
            return Ok(MappedProduct);
        }

    }
}
