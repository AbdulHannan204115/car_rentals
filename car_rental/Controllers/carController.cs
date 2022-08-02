using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using car_rental.Models;
using System.Data.Entity;

namespace car_rental.Controllers
{
    public class carController : Controller
    {
        // GET: car
        context obj = new context();
        public ActionResult Index()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            var a = obj.car.ToList();
            return View(a);
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(car ca)
        {
            var b = obj.car.Add(ca);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult edit(int id)
        {
            var row = obj.car.Where(model => model.car_id == id).FirstOrDefault();

            return View(row);
        }
        [HttpPost]
        public ActionResult edit(car ca)
        {
            obj.Entry(ca).State = EntityState.Modified;
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult delete(int id)
        {
            var row = obj.car.Where(model => model.car_id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                obj.Entry(row).State = EntityState.Deleted;
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        public ActionResult detail(int id)
        {
            var row = obj.car.Where(model => model.car_id == id).FirstOrDefault();

            return View(row);
        }
        public ActionResult logout()
        {
            if (Session["name"] == null)
            {
                Session.Abandon();
                obj.SaveChanges();

                return RedirectToAction("login","admin");
            }
            return RedirectToAction("login", "admin");
        }
    }
}
