using MyShop.core.Interfaces;
using MyShop.core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagementController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagementController(IRepository<ProductCategory> productCategoryContext)
        {
            this.context = productCategoryContext;
        }
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
                return View(productCategory);
            else
            {
                context.Insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productCategory = context.Find(id);
            if (productCategory == null)
                return HttpNotFound();
            else
                return View(productCategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productToEdit = context.Find(id);
            if (productToEdit == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                    return View(productCategory);
                else
                {
                    productToEdit.Category = productCategory.Category;

                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string id)
        {
            ProductCategory productToDelete = context.Find(id);
            if (productToDelete == null)
                return HttpNotFound();
            else
                return View(productToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(ProductCategory productCategory, string id)
        {
            ProductCategory productToDelete = context.Find(id);
            if (productToDelete == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                    return View(productCategory);
                else
                {
                    context.Delete(id);

                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}