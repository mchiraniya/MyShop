using MyShop.core.Interfaces;
using MyShop.core.Models;
using MyShop.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Product> ProductContext;
        IRepository<Basket> BasketContext;
        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> productContext, IRepository<Basket> basketContext)
        {
            this.BasketContext = basketContext;
            this.ProductContext = productContext;
        }

        private Basket GetBasket(HttpContextBase httpContextBase, bool createIfNull)
        {
            HttpCookie cookie = httpContextBase.Request.Cookies.Get(BasketSessionName);
            Basket basket = new Basket();
            if(cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                    basket = BasketContext.Find(basketId);
                else
                {
                    if (createIfNull)
                        basket = CreateNewBasket(httpContextBase);
                }
            }
            else
            {
                if (createIfNull)
                    basket = CreateNewBasket(httpContextBase);
            }
            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContextBase)
        {
            Basket basket = new Basket();
            BasketContext.Insert(basket);
            BasketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContextBase.Request.Cookies.Add(cookie);
            return basket;
        }

        public void AddToBasket(HttpContextBase httpContextBase, string productId)
        {
            Basket basket = GetBasket(httpContextBase, true);
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);

            if (basketItem == null)
            {
                basketItem = new BasketItem()
                {
                    ProductId = productId,
                    BasketId = basket.Id,
                    Quantity = 1
                };
                basket.BasketItems.Add(basketItem);
            }
            else
                basketItem.Quantity = basketItem.Quantity++;

            BasketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContextBase, string basketItemId)
        {
            Basket basket = GetBasket(httpContextBase, true);
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

            if (basketItem != null)
            {
                basket.BasketItems.Remove(basketItem);
                BasketContext.Commit();
            }
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContextBase)
        {
            Basket basket = GetBasket(httpContextBase, false);
            if (basket != null)
            {
                var result = (from b in basket.BasketItems
                              join p in ProductContext.Collection() on b.ProductId equals p.Id
                              select new BasketItemViewModel()
                              {
                                  Id = b.Id,
                                  Quantity = b.Quantity,
                                  ProductName = p.Name,
                                  Image = p.Image,
                                  Price = p.Price
                              }).ToList();
                return result;
            }
            else
                return new List<BasketItemViewModel>();
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContextBase)
        {
            Basket basket = GetBasket(httpContextBase, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);
            if (basket != null)
            {
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();
                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in ProductContext.Collection() on item.ProductId equals p.Id
                                        select item.Quantity * p.Price).Sum();
                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero;
            }
            return model;
        }
    }
}
