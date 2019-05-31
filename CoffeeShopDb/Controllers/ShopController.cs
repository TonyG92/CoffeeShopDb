using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CofeeShopDB.Models;

namespace CofeeShopDB.Controllers
{
    public class ShopController : Controller
    {
        ShopDbEntities ORM = new ShopDbEntities();
        // GET: Shop
        public ActionResult Index()

        {
            ViewBag.ShopList = ORM.Shops.ToList();
            return View();
        }

        public ActionResult UpdateShop(int shopID)
        {
            Shop found = ORM.Shops.Find(shopID);

            return View(found);
        }

        public ActionResult DeleteProduct(int shopID)
        {
            Shop found = ORM.Shops.Find(shopID);

            ORM.Shops.Remove(found);

            ORM.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult SavedChanges(Shop updatedShop)
        {
            Shop originalShop = ORM.Shops.Find(updatedShop.Id);

            if(originalShop != null && ModelState.IsValid)
            {
                originalShop.Name = updatedShop.Title;
                originalShop.Description = updatedShop.Description;
                originalShop.Quantity = updatedShop.Quantity;
                originalShop.Price = updatedShop.Price;

                ORM.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.ErrorMessage = "Something did not get right. Try again";
                return RedirectToAction("UpdateShop", updatedShop.Id);
            }
        }
    }
}