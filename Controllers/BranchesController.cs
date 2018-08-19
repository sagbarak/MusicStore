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

namespace MusicStore.Controllers
{
    public class BranchesController : Controller
    {
        private MusicStoreDbContext db = new MusicStoreDbContext();

        // GET: Branches
        public ActionResult Index()
        {
            return View(db.branches.ToList());
        }
        public string getLargestBranch()
        {
            var BranchList = (from branches in db.branches
                              group branches.items by branches.StoreName into b
                              select new { Names = b.Key, AlbumsNum = b.ToList() });

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(BranchList);
            return json;
        }
        public string GetSearchingResults(string Name, long Phone, string City)
        {


            var branchdb = db.branches;
            List<Branch> ItemList = new List<Branch>();
            if (Name == "" && Phone == 0 && City == "")
            {
                ItemList = (from branches in branchdb
                            select branches).ToList<Branch>();
            }
            else
            {
                ItemList = (from branches in branchdb
                            where (branches.StoreName.Contains(Name) && branches.PhoneNumber == Phone && branches.City.Contains(City)) ||
                            (Name == "" && branches.PhoneNumber == Phone && branches.City.Contains(City)) ||
                            (branches.StoreName.Contains(Name) && Phone == 0 && branches.City.Contains(City)) ||
                            (branches.StoreName.Contains(Name) && branches.PhoneNumber == Phone && branches.City == "") ||
                            (branches.StoreName.Contains(Name) && Phone == 0 && City == "") ||
                            (Name == "" && Phone == 0 && branches.City.Contains(City)) ||
                            (Name == "" && branches.PhoneNumber == Phone && City == "")
                            select branches).ToList<Branch>();
            }
            List<Branch> items = new List<Branch>();
            foreach (Branch branch in ItemList)
            {
                items.Add(branch);
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(items);
            return json;

        }
        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Branches/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoreName,PhoneNumber,Email,Address,City")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        // GET: Branches/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoreName,PhoneNumber,Email,Address,City")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: Branches/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.branches.Find(id);
            db.branches.Remove(branch);
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
