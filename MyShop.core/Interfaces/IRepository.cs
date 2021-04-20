using MyShop.core.Models;
using System.Linq;

namespace MyShop.core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string id);
        T Find(string id);
        void Insert(T p);
        void Update(T t);
    }
}