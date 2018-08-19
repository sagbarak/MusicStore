using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            ViewBag.Message = "Choose you tune";

            return View();
            //return RedirectToAction("Index","Item");
        }
        public ActionResult Customer()
        {
            ViewBag.Message = "Customer";

            return View();
            //return RedirectToAction("Index","Item");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Statistics()
        {

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}