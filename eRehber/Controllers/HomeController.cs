using eRehber.BusinessLayer;
using eRehber.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRehber.Controllers
{
    public class HomeController : Controller
    {
        PersonelManager pm = new PersonelManager();
        // GET: Home
        public ActionResult Index()
        {

            return View(pm.List(x => x.Durum == true && x.IlAdi == "Afyonkarahisar"));
        }
        [HttpPost]
        public ActionResult Index(string IlAdi, string SubeAdi)
        {
            if (ModelState.IsValid)
            {
                if (IlAdi != null && SubeAdi != "Şube Seçiniz")
                {

                    List<Personel> data = pm.List(x => x.IlAdi == IlAdi && x.SubeAdi == SubeAdi).ToList();
                    return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
                    //return Json('1')
                }
                else if (IlAdi != null && SubeAdi == "Şube Seçiniz")
                {
                    List<Personel> data = pm.List(x => x.IlAdi == IlAdi).ToList();
                    return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
                }

                return View();
            }

            return Json('0');
            

        }
    }
}