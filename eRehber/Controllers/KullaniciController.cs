using eRehber.BusinessLayer;
using eRehber.Entities;
using eRehber.Entities.ValueObjects;
using eRehber.BusinessLayer.Results;
using eRehber.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRehber.Controllers
{
    public class KullaniciController : Controller
    {
        Kullanici k = null;
        KullaniciManager km = new KullaniciManager();
        // GET: Kullanici
        public ActionResult Index()
        {
            if (Session["login"] != null)
            {
                k = Session["login"] as Kullanici;
                return View(k);
            }
            else
            {
                return RedirectToAction("Index", "Giris");
            }

        }

        public ActionResult SifremiUnuttum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifremiUnuttum(Kullanici model, FormCollection form)
        {
            ModelState.Remove("Yonetim");
            ModelState.Remove("IlAdi");
            ModelState.Remove("Sifre");

            model.IlAdi = form["inputil"];
            if (ModelState.IsValid)
            {

                BusinessLayerResult<Kullanici> res = km.FPassword(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                }
                else
                {

                    Session["login"] = res.Result;
                    return RedirectToAction("Kullanici", "Yonetim");
                }

            }

            return View(model);
        }
   
        [HttpPost]
        public ActionResult Index(Kullanici kullanici)

        {

            ModelState.Remove("KullaniciAdi");
            ModelState.Remove("IlAdi");
            ModelState.Remove("Yonetim");
            ModelState.Remove("durum");

            if (ModelState.IsValid)
            {
                k = Session["login"] as Kullanici;
                k.Sifre = kullanici.Sifre;
                if (km.Update(k) > 0)
                {

                    RegViewModel regView = new RegViewModel()
                    {
                        Title = "Kayıt Başarılı",
                        RedirectingUrl = "/Yonetim/Index/",
                        IsRedirecting = true
                    };
                    regView.Items.Add("Şifreniz başarıyla güncellendi.");
                    return View("Reg", regView);
                }
          
            }
            return View();
        }

    }
}