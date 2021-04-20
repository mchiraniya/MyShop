using MyShop.core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }
    }
}
