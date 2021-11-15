using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.OraLounge.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public List<CategoryViewModel> Children { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}