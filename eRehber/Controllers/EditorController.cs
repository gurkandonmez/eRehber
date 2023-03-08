using eRehber.BusinessLayer;
using eRehber.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRehber.Controllers
{
    public class EditorController : Controller
    {
        KullaniciManager km = new KullaniciManager();
        Kullanici k = null;

        // GET: Editor
        public ActionResult Index()
        {
            k = Session["login"] as Kullanici;
            if (k != null  && k.Yonetim==true)
            {
                return View(km.List(x => x.durum == false).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Giris");
            }
        }
        public ActionResult Sil(int id)

        {
            ViewBag.Id = id;
            Kullanici k = km.Find(x => x.ID == id);
            if (k != null)
            {
                return PartialView("_PartialKullaniciSil", k);

            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return Json("0");
            }
            Kullanici k = km.Find(x => x.ID == id);
            if (k == null)
            {
                return Json("0");
            }

            if (km.Delete(k) > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        public ActionResult Guncelle(int id)

        {
            ViewBag.Id = id;
            Kullanici k = km.Find(x => x.ID == id);
            if (k != null)
            {
                return PartialView("_PartialKullaniciOnayla", k);

            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Guncelle(int? id)
        {
            if (id == null)
            {
                return Json("0");
            }
            Kullanici k = km.Find(x => x.ID == id);
            if (k == null)
            {
                return Json("0");
            }

            k.durum = true;

            if (km.Update(k) > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }

    }
}