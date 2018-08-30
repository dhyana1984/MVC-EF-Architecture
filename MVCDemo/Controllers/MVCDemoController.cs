using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class MVCDemoController : Controller
    {
        // GET: MVCDemo
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ShowWidget()
        {
            return PartialView("~/Views/Shared/_PartialPageWidget.cshtml");
        }

        public ActionResult SharedDateDemo()
        {
            ViewBag.DateTime = DateTime.Now;
            return View();
        }
        [ChildActionOnly]
        public ActionResult PartialViewDate()
        {
            ViewBag.DateTime = DateTime.Now.AddMinutes(10);
            return PartialView("_PartialPageDateTime");
        }
    }
}