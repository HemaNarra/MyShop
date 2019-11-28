using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productCategories = cache["ProductCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["ProductCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory ProductCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (ProductCategoryToUpdate != null)
            {
                ProductCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory ProductCategoryToFind = productCategories.Find(p => p.Id == Id);
            if (ProductCategoryToFind != null)
            {
                return ProductCategoryToFind;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(ProductCategory product)
        {
            ProductCategory ProductCategoryToDelete = productCategories.Find(p => p.Id == product.Id);
            if (ProductCategoryToDelete != null)
            {
                productCategories.Remove(ProductCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
    }
}
