using MyShop.core.Models;
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
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
                productCategories = new List<ProductCategory>();
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productToUpdate = productCategories.Find(x => x.Id == productCategory.Id);
            if (productToUpdate != null)
                productToUpdate = productCategory;
            else
                throw new Exception("Product Category not found!");
        }

        public ProductCategory Find(string id)
        {
            ProductCategory productToFind = productCategories.Find(x => x.Id == id);
            if (productToFind != null)
                return productToFind;
            else
                throw new Exception("Product Category not found!");
        }

        public void Delete(string id)
        {
            ProductCategory productToDelete = productCategories.Find(x => x.Id == id);
            if (productToDelete != null)
                productCategories.Remove(productToDelete);
            else
                throw new Exception("Product Category not found!");
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
    }
}
