using FakeStoreWebApi.Models;
using FakeStoreWebApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FakeStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// Get product 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> GetProductAsync(int productId)
        {
            var product = await _productService.GetProductAsync(productId);
            if (!product.Success)
                return NotFound(product);
            return Ok(product);
        }
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productService.GetProductsAsync();
            if (!products.Success)
                return NotFound(products);
            return Ok(products);
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Some properties are not valid");
            var result = await _productService.CreateProductAsync(product);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditProduct")]
        public async Task<IActionResult> EditProductAsync(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Some properties are not valid");
            var result = await _productService.UpdateProductAsync(id,product);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
