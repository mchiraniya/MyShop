using MyShop.core.Interfaces;
using MyShop.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
                items = new List<T>();
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T p)
        {
            items.Add(p);
        }

        public void Update(T t)
        {
            T itemToUpdate = items.Find(x => x.Id == t.Id);
            if (itemToUpdate != null)
                itemToUpdate = t;
            else
                throw new Exception("Item not found!");
        }

        public T Find(string id)
        {
            T itemToFind = items.Find(x => x.Id == id);
            if (itemToFind != null)
                return itemToFind;
            else
                throw new Exception("Item not found!");
        }

        public void Delete(string id)
        {
            T itemToDelete = items.Find(x => x.Id == id);
            if (itemToDelete != null)
                items.Remove(itemToDelete);
            else
                throw new Exception("Item not found!");
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
    }
}
