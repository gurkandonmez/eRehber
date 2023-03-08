using eRehber.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRehber.Controllers
{
    public class YonetimController : Controller
    {
        // GET: Yonetim
        public ActionResult Index()
        {
            if (Session["login"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Giris");
            }
        }
    }
}