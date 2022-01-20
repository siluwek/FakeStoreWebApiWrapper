using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FakeStoreWebApi.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string  Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Image { get; set; }
        public Rating Rating { get; set; }

    }
}
