using eRehber.BusinessLayer;
using eRehber.BusinessLayer.Results;
using eRehber.Entities;
using eRehber.Entities.ValueObjects;
using eRehber.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRehber.Controllers
{

    public class KayitController : Controller
    {

      private  KullaniciManager km = new KullaniciManager();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(RegisterViewModel model, FormCollection form)
       {
            ModelState.Remove("Yonetim");
            ModelState.Remove("IlAdi");
            model.IlAdi = form["inputil"].ToString();
            if (ModelState.IsValid)
            {

                BusinessLayerResult<Kullanici> res = km.RegisterUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                }
                else
                {

                    RegViewModel regView = new RegViewModel()
                    {
                        Title = "Kayıt Başarılı",
                        RedirectingUrl = "/Giris/Index/",
                        IsRedirecting = true
                    };
                    regView.Items.Add("Hesabınız başarıyla oluşturuldu onaylandıktan sonra giriş yapabilirsiniz.");
                    return View("Reg", regView);
                }

            }

            return View(model);
        }
    }
}