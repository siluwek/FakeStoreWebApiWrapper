using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeStoreWebApi.Models
{
    /// <summary>
    /// Wraps the responses for Products 
    /// </summary>
    public class ProductResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public  Product Product{ get; set; }
        public List<Product> Products { get; set; }
        public List<string> Errors { get; set; }
    }
}
