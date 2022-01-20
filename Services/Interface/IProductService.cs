using FakeStoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeStoreWebApi.Services.Interface
{
    public interface IProductService
    {
        Task<ProductResponse> GetProductsAsync();
        Task<ProductResponse> GetProductAsync(int productId);
        Task<ProductResponse> CreateProductAsync(Product product);
        Task<ProductResponse> UpdateProductAsync(int id,Product product);
    }
}
