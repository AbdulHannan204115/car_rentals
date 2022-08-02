using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using car_rental.Models;
using System.Data.Entity;

namespace car_rental.Controllers
{
    public class adminController : Controller
    {
        // GET: admin
        context obj = new context();
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string name,string pass)
        {
            Session["name"] = name;
            Session["pass"] = pass;
           
            if (name.Equals("")==true)
            {
                ModelState.AddModelError("name","Enter your Name");
            }
            else
            {
                if (pass.Equals("")==true)
                {
                    ModelState.AddModelError("pass", "Enter your Password");
                }
                bool isvalid = obj.admin.Any(model => model.user_name == name);
                if (isvalid)
                {

                    bool invalid = obj.admin.Any(model => model.password == pass);
                    if (invalid)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("pass", "INAVLID Password!");

                    }
                }
               
                else
                {
                    ModelState.AddModelError("name", "INVALID User-Name!!");
                }
        
            }
            

            return View();
        }
        public ActionResult Index()
        {
            if (Session["name"]==null)
            {
                return RedirectToAction("login");
            }
            var a = obj.admin.ToList();
            return View(a);
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(admin ad)
        {
            var b = obj.admin.Add(ad);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult edit(int id)
        {
            var row = obj.admin.Where(model => model.admin_id == id).FirstOrDefault();

            return View(row);
        }
        [HttpPost]
        public ActionResult edit(admin ad)
        {
            obj.Entry(ad).State = EntityState.Modified;
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult delete(int id)
        {
            var row = obj.admin.Where(model => model.admin_id == id).FirstOrDefault();
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
            var row = obj.admin.Where(model => model.admin_id == id).FirstOrDefault();
            
            return View(row);
        }
        public ActionResult logout() {
            if (Session["name"]==null)
            {
                Session.Abandon();
                obj.SaveChanges();
   
                return RedirectToAction("login");
            }
            return RedirectToAction("login");
                }
    }
}