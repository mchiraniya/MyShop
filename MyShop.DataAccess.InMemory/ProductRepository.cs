using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
                products = new List<Product>();
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(x => x.Id == product.Id);
            if (productToUpdate != null)
                productToUpdate = product;
            else
                throw new Exception("Product not found!");
        }

        public Product Find(string id)
        {
            Product productToFind = products.Find(x => x.Id == id);
            if (productToFind != null)
                return productToFind;
            else
                throw new Exception("Product not found!");
        }

        public void Delete(string id)
        {
            Product productToDelete = products.Find(x => x.Id == id);
            if (productToDelete != null)
                products.Remove(productToDelete);
            else
                throw new Exception("Product not found!");
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
    }
}
