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
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;


namespace MusicStore.Controllers
{
    public class ItemsController : Controller
    {
        private MusicStoreDbContext db = new MusicStoreDbContext();

        // GET: Items
        public ActionResult Index(string SearchBy, string Search)
        {
            Console.WriteLine(":)");
            var items = db.items.Include(i => i.branch);
            if (SearchBy == "Name")
            {
                return View(items.Where(x => x.Name.Contains(Search) || Search == null).ToList());
            }
            else
            {
                return View(items.Where(x => x.Artist.Contains(Search) || Search == null).ToList());
            }
        }
        public string getCheapestStore()
        {

            var itemsdb = db.items.Include(i => i.branch);

            var ItemList = (from item in itemsdb
                            join branch in db.branches
                            on item.branch.StoreName equals branch.StoreName
                            group new { item.Price, branch } by new {branch.City} into g
                            select new{
                                Citys = g.Key,
                                Prices = g.ToList()
                                
                            });
           
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ItemList);
            return json;
        }
        public string getRecomendedAlbum(string CitySr,int Age)
        {

            var itemsdb = db.items.Include(i => i.branch);
            
            var ItemList = (from item in itemsdb
                            join branch in db.branches
                            on item.branch.StoreName equals branch.StoreName
                            where branch.City==CitySr
                            select new
                            {
                                Names = item.Name,
                                Years = item.Year,
                                Citys = branch.City
                            });
            var birthYear = DateTime.Now.Year - Age;
   
 
            foreach(var i in ItemList)
            {
                if (i.Citys.Equals(CitySr))
                {
                    Console.WriteLine(i);

                }
            }
            
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ItemList);
            
            return json;
        }


            public string getBestAlbum()
        {

           var itemsdb = db.items.Include(i => i.branch);
           
           var ItemList= (from item in itemsdb
                        group item.Sales by item.Name into ib
                        select new { Names = ib.Key , Sales = ib.ToList()});
            List<string> lst = new List<string>();
    
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ItemList);
            return json;
          
        }
        [HttpPost]
        public string GetSearchingResults(string Artist,string Genre, int Year)
        {


           var itemsdb = db.items.Include(i => i.branch);

            List<Item> ItemList = new List<Item>();
            if (Artist == "" && Genre == "" && Year == 0)
            {
                ItemList = (from albums in itemsdb
                            select albums).ToList<Item>();
            }
            else
            {
                ItemList = (from albums in itemsdb
                            where (albums.Artist.Contains(Artist) && albums.Genre.Contains(Genre) && albums.Year == Year) ||
                            (Artist == "" && albums.Genre.Contains(Genre) && albums.Year == Year) ||
                            (albums.Artist.Contains(Artist) && Genre == "" && albums.Year == Year) ||
                            (albums.Artist.Contains(Artist) && albums.Genre.Contains(Genre) && Year == 0) ||
                            (albums.Artist.Contains(Artist) && Genre == "" && Year == 0) ||
                            (Artist == "" && Genre == "" && albums.Year == Year) ||
                            (Artist == "" && albums.Genre.Contains(Genre) && Year == 0)
                            select albums).ToList<Item>();
            }
            List<Item> items = new List<Item>();
            foreach(Item item in ItemList)
            {
                var bName = item.branch.StoreName;
                var bId = item.BranchId;
                item.StoreName = bName;
                item.branch = null;
                items.Add(item);
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(items);
            return json;
        }
    
        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.branches, "Id", "StoreName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SerialNumber,Name,Artist,Genre,Year,Description,Price,BranchId,Sales")] Item item)
        {
            if (ModelState.IsValid)
            {
                
                db.items.Add(item);
                db.SaveChanges();
            
                return RedirectToAction("Index");
            }

            ViewBag.BranchId = new SelectList(db.branches, "Id", "Name", item.BranchId);
            return View(item);
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = new SelectList(db.branches, "Id", "StoreName", item.BranchId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SerialNumber,Name,Artist,Genre,Year,Description,Price,BranchId,Sales")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchId = new SelectList(db.branches, "Id", "Name", item.BranchId);
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.items.Find(id);
            db.items.Remove(item);
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
