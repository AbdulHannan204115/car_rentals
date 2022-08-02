using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using car_rental.Models;

namespace car_rental.Controllers
{
    public class rentsController : Controller
    {
        private context db = new context();

        // GET: rents
        public ActionResult Index()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("login","admin");
            }
            var rents = db.rent.Include(r => r.admin).Include(r => r.car).Include(r => r.client);
            return View(rents.ToList());
        }

        // GET: rents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rent rent = db.rent.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

      
        public ActionResult Create()
        {
            ViewBag.admin_id = new SelectList(db.admin, "admin_id", "name");
            ViewBag.car_id = new SelectList(db.car, "car_id", "name");
            ViewBag.client_id = new SelectList(db.client, "client_id", "name");
            return View();
        }

     
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "rent_id,admin_id,car_id,client_id,rent_date")] rent rent)
        {
            if (ModelState.IsValid)
            {
                db.rent.Add(rent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.admin_id = new SelectList(db.admin, "admin_id", "name", rent.admin_id);
            ViewBag.car_id = new SelectList(db.car, "car_id", "name", rent.car_id);
            ViewBag.client_id = new SelectList(db.client, "client_id", "name", rent.client_id);
            return View(rent);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rent rent = db.rent.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            ViewBag.admin_id = new SelectList(db.admin, "admin_id", "name", rent.admin_id);
            ViewBag.car_id = new SelectList(db.car, "car_id", "name", rent.car_id);
            ViewBag.client_id = new SelectList(db.client, "client_id", "name", rent.client_id);
            return View(rent);
        }

        // POST: rents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rent_id,admin_id,car_id,client_id,rent_date")] rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.admin_id = new SelectList(db.admin, "admin_id", "name", rent.admin_id);
            ViewBag.car_id = new SelectList(db.car, "car_id", "name", rent.car_id);
            ViewBag.client_id = new SelectList(db.client, "client_id", "name", rent.client_id);
            return View(rent);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rent rent = db.rent.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(int id)
        {
            rent rent = db.rent.Find(id);
            db.rent.Remove(rent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult logout()
        {
            if (Session["name"] == null)
            {
                Session.Abandon();
                db.SaveChanges();

                return RedirectToAction("login","admin");
            }
            return RedirectToAction("login", "admin");
        }

    }
}
