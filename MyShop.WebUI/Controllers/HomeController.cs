using MyShop.core.Interfaces;
using MyShop.core.Models;
using MyShop.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        public ActionResult Index(string category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();
            if (category == null)
                products = context.Collection().ToList();
            else
                products = context.Collection().Where(item => item.Category == category).ToList();

            ProductListViewModel listModel = new ProductListViewModel();
            listModel.Products = products;
            listModel.ProductCategories = categories;

            return View(listModel);
        }

        public ActionResult Details(string id)
        {
            Product product = context.Find(id);
            if(product == null)
                return HttpNotFound();
            else
            {
                return View(product);
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}