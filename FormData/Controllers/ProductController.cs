﻿using System.Linq;
using System.Web.Mvc;
using FormData.DataLayer;

namespace FormData.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(int? id, string SearchString)
        {
            ViewBag.Filter = "Products";

            using (var db = new NorthwndEntities())
            {
                var products = db.Products.Where(p => !p.Discontinued);

                // apply searchstring
                if (!string.IsNullOrEmpty(SearchString))
                {
                    ViewBag.Filter += " Matching " + SearchString;
                    products = products.Where(p => p.ProductName.Contains(SearchString));
                }

                // apply id
                if (id != null)
                {
                    products = products.Where(p => p.ProductID == id);
                }

                // retrieve list of products
                return View(products.OrderBy(p => p.ProductName).ToList());
            }
        }

        public ActionResult ProductByCategory(int? id, string SearchString)
        {
            ViewBag.Filter = "Products";

            using (var db = new NorthwndEntities())
            {
                var products = db.Products
                    .Where(p => !p.Discontinued);

                // apply searchstring
                if (!string.IsNullOrEmpty(SearchString))
                {
                    ViewBag.Filter += " Matching " + SearchString;
                    products = products.Where(p => p.ProductName.Contains(SearchString));
                }

                // apply id
                if (id != null)
                {
                    ViewBag.Filter += " in Category " + db.Categories.Find(id)?.CategoryName;
                    products = products.Where(p => p.CategoryID == id);
                }

                // retrieve list of products
                return View("Index", products.OrderBy(p => p.ProductName).ToList());
            }
        }
    }
}