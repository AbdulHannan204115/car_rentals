using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using car_rental.Models;

namespace car_rental.Controllers
{
    public class clientController : Controller
    {
        // GET: client
        context obj = new context();
        public ActionResult Index()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("login","admin");
            }
            var a = obj.client.ToList();
            return View(a);
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(client cl)
        {
            var b = obj.client.Add(cl);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult edit(int id)
        {
            var row = obj.client.Where(model => model.client_id == id).FirstOrDefault();

            return View(row);
        }
        [HttpPost]
        public ActionResult edit(client cl)
        {
            obj.Entry(cl).State = EntityState.Modified;
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult delete(int id)
        {
            var row = obj.client.Where(model => model.client_id == id).FirstOrDefault();
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
            var row = obj.client.Where(model => model.client_id == id).FirstOrDefault();

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