using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assessment7.Models;

namespace Assessment7.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
       [Authorize]
        public ActionResult Index()
        {
            ShopEntities ORM = new ShopEntities();

            ViewBag.Inventory = ORM.Inventories.ToList();

            return View();
        }

        public ActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItem(Inventory Item)
        {
            ShopEntities ORM = new ShopEntities();
            ORM.Inventories.Add(Item);
            ORM.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditItem(int id)
        {
            ShopEntities ORM = new ShopEntities();
            Inventory found = ORM.Inventories.Find(id);
            return View(found);
        }

        public ActionResult SaveItem(Inventory updateItem)
        {
            ShopEntities ORM = new ShopEntities();
            Inventory oldItem = ORM.Inventories.Find(updateItem.ItemID);

            oldItem.ItemID = updateItem.ItemID;
            oldItem.Name = updateItem.Name;
            oldItem.Description = updateItem.Description;
            oldItem.Quantity = updateItem.Quantity;
            oldItem.Price = updateItem.Price;

            ORM.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult DeleteItem(int id)
        {
            ShopEntities ORM = new ShopEntities();
            Inventory found = ORM.Inventories.Find(id);
            ORM.Inventories.Remove(found);
            ORM.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}