using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MusicStore.Models;
using Microsoft.AspNet.Identity;

namespace MusicStore.Controllers
{
    public class CustomersController : Controller
    {
        private MusicStoreDbContext db = new MusicStoreDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            //
            return View(db.customers.ToList());
        }
        public ActionResult List()
        {
            return View(db.customers.ToList());
        }

        public string GetSearchingResults(string Name, long Phone, string City)
        {


            var customerdb = db.customers;
            List<Customer> ItemList = new List<Customer>();
            if (Name == "" && Phone == 0 && City == "")
            {
                      ItemList = (from customers in customerdb
                                  select customers).ToList<Customer>();
            }
            else
            {
                     ItemList = (from customers in customerdb
                                    where (customers.Name.Contains(Name) && customers.PhoneNumber == Phone && customers.City.Contains(City)) ||
                                    (Name == "" && customers.PhoneNumber == Phone && customers.City.Contains(City)) ||
                                    (customers.Name.Contains(Name) && Phone == 0 && customers.City.Contains(City)) ||
                                    (customers.Name.Contains(Name) && customers.PhoneNumber == Phone && customers.City == "") ||
                                    (customers.Name.Contains(Name) && Phone == 0 && City == "") ||
                                    (Name == "" && Phone == 0 && customers.City.Contains(City)) ||
                                    (Name == "" && customers.PhoneNumber == Phone && City == "")
                                    select customers).ToList<Customer>();
            }
            List<Customer> items = new List<Customer>();
            foreach (Customer customer in ItemList) { 
                items.Add(customer);
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(items);
            return json;
        
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateFromUser()
        {
            return View();
        }
        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,PhoneNumber,Email,Address,City")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                
                customer.LogId = User.Identity.GetUserId();
                db.customers.Add(customer);
                db.SaveChanges();
               
                    return RedirectToAction("Index");
              
            }

            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromUser([Bind(Include = "Id,Name,Age,PhoneNumber,Email,Address,City")] Customer customer)
        {
            if (ModelState.IsValid)
            {
               
                customer.LogId = User.Identity.GetUserId();
                db.customers.Add(customer);
               
                db.SaveChanges();
                
                return Redirect("/Home/Index");
            }

            return View(customer);
        }
        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,PhoneNumber,Email,Address,City")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
