using MyShop.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.core.Interfaces
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContextBase, string productId);
        void RemoveFromBasket(HttpContextBase httpContextBase, string basketItemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContextBase);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContextBase);
    }
}
