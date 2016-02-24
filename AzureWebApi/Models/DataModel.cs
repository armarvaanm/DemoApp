using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCatalogue.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ProductSpecification> ProductSpecifications { get; set; }
        public string GroupNumber { get; set; }
        public string Brand { get; set; }
        public string ProductColor { get; set; }
    }

    public class ProductSpecification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? AllowComparision { get; set; }
        public bool? AllowFiltering { get; set; }
    }

    public class ProductCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public List<ProductSubCategory> SubCategoryItems { get; set; }
    }

    public class ProductSubCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int ProductCount { get; set; }
    }

    
}
