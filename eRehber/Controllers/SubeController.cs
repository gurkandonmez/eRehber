using eRehber.BusinessLayer;
using eRehber.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eRehber.Controllers
{
    public class SubeController : Controller
    {
        SubeManager sm = new SubeManager();
        // GET: Sube
        public ActionResult Index()
        {

            Kullanici k = Session["login"] as Kullanici;
     
            if (k != null && k.Yonetim == true)
            {
                return View(sm.List().OrderBy(x => x.Sira).ToList());

         
            }
            else
            {
                return RedirectToAction("Index", "Giris");
            }
           
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return PartialView("_PartialSubeEkle");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            ViewBag.Id = id;
            Sube sube = sm.Find(x => x.ID == id);
            if (sube != null)
            {
                return PartialView("_PartialSubeGuncelle", sube);

            }
            else
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult Guncelle(int? id,Sube sube)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          Sube db_sube = sm.Find(x => x.ID == id.Value);
            if (sube == null)
            {
                return HttpNotFound();
            }

            db_sube.Ad = sube.Ad;
            if (sm.Update(db_sube) > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }

        [HttpPost]
        public ActionResult Sil(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sube db_sube = sm.Find(x => x.ID == id.Value);
            if (db_sube == null)
            {
                return HttpNotFound();
            }

            if (sm.Delete(db_sube) > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
           
        }
        [HttpGet]
        public ActionResult Sil(int id)
        {
            ViewBag.Id = id;
            Sube sube = sm.Find(x => x.ID == id);
            if (sube != null)
            {
                return PartialView("_PartialSubeSil", sube);

            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Sirala(int? id,FormCollection form, Sube sube)
        {
            sube = sm.Find(x => x.ID == id);
            int Id = sube.ID;
        
            Sube db_sube = sm.Find(x => x.ID == Id);
            string[] BaslikId = form.GetValues("ID");
            string[] Sira = form.GetValues("Sira");
        IEnumerable<Sube> listSube = sm.List().OrderBy(x => x.Sira);
            for (int i = 0; i < BaslikId.Length; i++)
            {
                int BaslikId2 = Int32.Parse(BaslikId[i]);
                int sira2 = Int32.Parse(Sira[i]);
                Sube t_sube = sm.Find(x => x.ID == BaslikId2);
                t_sube.Sira = sira2;
                sm.Update(t_sube);
            }
             listSube = sm.List().OrderBy(x => x.Sira);
            return RedirectToAction("Index", "Sube");


            //  return RedirectToAction("Listele", "AdresTanim", new { @id = id });

        }

        [HttpPost]
        public ActionResult Ekle(Sube sube)
        {
            if (ModelState.IsValid)
            {
                if (sube.Ad != null)
                {
                    var durum = sm.Insert(sube);
                    if (durum == 1)
                    {
                        return Json("1");
                    }
                    else

                    {
                        return Json("0");
                    }
                }
                else
                {
                    return Json("0");
                }

            }
            return Json("0");

        }
    }
}