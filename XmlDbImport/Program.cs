using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlDbImport
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDbInitializer dbi = new MyDbInitializer();
            dbi.InitializeDatabase(new EFContext());

            XDocument xdocProduct = XDocument.Load("..\\..\\product.xml");
            XNamespace ns = "http://schemas.datacontract.org/2004/07/WAAD.POC.ProductCatalog.DataModels";

            int i = 0;
            Product objProduct = new Product();
            List<Product> lstProduct
               = (from _product in xdocProduct.Element(ns + "ArrayOfProduct").Elements(ns + "Product")
                  select new Product
                  {
                      Brand = _product.Element(ns + "Brand").Value,
                      Category = _product.Element(ns + "Category").Value,
                      Description = _product.Element(ns + "Description").Value,
                      GroupNumber = _product.Element(ns + "GroupNumber").Value,
                      Id = _product.Element(ns + "Id").Value,
                      ImagePath = _product.Element(ns + "ImagePath").Value,
                      Name = _product.Element(ns + "Name").Value,
                      Price = Decimal.Parse(_product.Element(ns + "Price").Value),
                      ProductColor = _product.Element(ns + "ProductColor").Value,
                      SubCategory = _product.Element(ns + "SubCategory").Value,

                      ProductSpecifications = (from _productSpecifications in _product.Element(ns + "ProductSpecifications").Elements(ns + "ProductSpecification")
                                               select new ProductSpecification
                                               {
                                                   Id = ++i,
                                                   AllowComparision = Boolean.Parse(_productSpecifications.Element(ns + "AllowComparision").Value),
                                                   AllowFiltering = Boolean.Parse(_productSpecifications.Element(ns + "AllowFiltering").Value),
                                                   Name = _productSpecifications.Element(ns + "Name").Value,
                                                   Value = _productSpecifications.Element(ns + "Value").Value,
                                               }).ToList()

                  }).ToList();

            XDocument xdocProductCategory = XDocument.Load("..\\..\\category.xml");
            ProductCategory objProductCategory = new ProductCategory();
            List<ProductCategory> lstProductCategory
               = (from _productCategory in xdocProductCategory.Element(ns + "ArrayOfProductCategory").Elements(ns + "ProductCategory")
                  select new ProductCategory
                  {
                      Id = _productCategory.Element(ns + "Id").Value,
                      Name = _productCategory.Element(ns + "Name").Value,
                      ImagePath = _productCategory.Element(ns + "ImagePath").Value,
                      SubCategoryItems = (from _productSubCategory in _productCategory.Element(ns + "SubCategoryItems").Elements(ns + "ProductSubCategory")
                                          select new ProductSubCategory
                                          {
                                              Id = _productSubCategory.Element(ns + "Id").Value,
                                              Name = _productSubCategory.Element(ns + "Name").Value,
                                              ImagePath = _productSubCategory.Element(ns + "ImagePath").Value,
                                              ProductCount = Int32.Parse(_productSubCategory.Element(ns + "ProductCount").Value),
                                          }).ToList()
                  }).ToList();

            using (var ctx = new EFContext())
            {
                foreach (Product p in lstProduct)
                {
                    ctx.Products.Add(p);
                }
                foreach (ProductCategory pc in lstProductCategory)
                {
                    ctx.ProductCategories.Add(pc);
                }
                ctx.SaveChanges();
            }
        }

    }
}
