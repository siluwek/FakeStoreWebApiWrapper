using FakeStoreWebApi.Models;
using FakeStoreWebApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace FakeStoreWebApi.Services
{
    /// <summary>
    /// Serves requests pertaining to Product
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ProductResponse> GetProductAsync(int productId)
        {

            var result = await _httpClient.GetStringAsync("Products/" + productId);
            if (result == "null")
                return new ProductResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    Message = "Record not found"
                };
            var productResult = JsonConvert.DeserializeObject<Product>(result);

            return new ProductResponse
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK.ToString(),
                Product = productResult
            };
        }

        public async Task<ProductResponse> GetProductsAsync()
        {
            var result = await _httpClient.GetStringAsync("Products/");
            if (result == "null")
                return new ProductResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    Message = "Records not found"
                };
            var productResult = JsonConvert.DeserializeObject<List<Product>>(result);

            return new ProductResponse
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK.ToString(),
                Products = productResult
            };
        }

        public async Task<ProductResponse> CreateProductAsync(Product product)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Products", httpContent);
            var content = await response.Content.ReadAsStringAsync();
            dynamic value = JsonConvert.DeserializeObject(content);
            product.Id = value.id;

            return new ProductResponse
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode.ToString(),
                Message = response.IsSuccessStatusCode ? "Record created successfully" : "Record creation failed",
                Product = product
            };
        }

        public async Task<ProductResponse> UpdateProductAsync(int id, Product product)
        {
            var result = await _httpClient.GetStringAsync("Products/" + id);
            if (result == "null")
                return new ProductResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    Message = "Record not found"
                };
            //
            var productResult = JsonConvert.DeserializeObject<Product>(result);

            productResult.Title = product.Title;
            productResult.Price = product.Price;
            productResult.Description = product.Description;
            productResult.Category = product.Category;
            productResult.Image = product.Image;
            if (product.Rating == null)
            {
                product.Rating = new Rating { Count = 0, Rate = 0 };
            }

           productResult.Rating = product.Rating;

            var httpContent = new StringContent(JsonConvert.SerializeObject(productResult), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("Products/" + id, httpContent);
            var content = await response.Content.ReadAsStringAsync();
         
            return new ProductResponse
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode.ToString(),
                Message = response.IsSuccessStatusCode ? "Record Updated successfully" : "Record update failed",
                Product = productResult

            };
        }
    }
}
