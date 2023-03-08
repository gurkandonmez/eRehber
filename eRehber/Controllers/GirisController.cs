using eRehber.BusinessLayer;
using eRehber.BusinessLayer.Results;
using eRehber.Entities;
using eRehber.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRehber.Controllers
{
    public class GirisController : Controller
    {
        KullaniciManager km = new KullaniciManager();
        Kullanici k = new Kullanici();
        // GET: Giris
        public ActionResult Index()
        {
            Kullanici k = Session["login"] as Kullanici;

            if (k != null)
            {
                return RedirectToAction("Index", "Yonetim");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
      
            if (ModelState.IsValid)
            {

                BusinessLayerResult<Kullanici> res = km.LoginUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                }
                else
                {
                    Session["login"] = res.Result;
                    return RedirectToAction("Index", "Yonetim");
                }

            }


            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");

        }
    }
}