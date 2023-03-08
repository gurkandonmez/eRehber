using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eRehber.Entities;
using eRehber.BusinessLayer;
using Excel = Microsoft.Office.Interop.Excel;
using eRehber.ViewModels;
using System.Net;
using System.Data.Entity;

namespace eRehber.Controllers
{
    public class PersonelController : Controller
    {
        Kullanici k = null;
        Sube s = new Sube();
        PersonelManager pm = new PersonelManager();
        List<Personel> per = new List<Personel>();
        SubeManager sm = new SubeManager();
        [HttpGet]
        public ActionResult Index()
        {

            Kullanici k = Session["login"] as Kullanici;

            if (k != null)
            {
                var per = pm.List(x => x.IlAdi == k.IlAdi);

                return View(per.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Giris");
            }
        }
        public ActionResult Ekle()
        {

            ViewBag.Sube = new SelectList(sm.List(), "Ad", "Ad", s.Ad);
            return PartialView("_PartialPersonelEkle");
        }
        [HttpPost]
        public ActionResult Ekle(Personel p)
        {
            if (ModelState.IsValid)
            {
                if (p.Ad != null && p.Soyad != null && p.Rutbe != null)
                {

                    k = Session["login"] as Kullanici;
                    p.Durum = true;
                    p.IlAdi = k.IlAdi;

                    var durum = pm.Insert(p);
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
            ViewBag.Sube = new SelectList(sm.List(), "Ad", "Ad", s.Ad);
            return View();
        }
        public ActionResult Sil(int? id)
        {


            ViewBag.Id = id;
            Personel p = pm.Find(x => x.ID == id);
            if (p != null)
            {
                return PartialView("_PartialPersonelSil", p);

            }
            else
            {
                return View();
            }


        }
        [HttpPost]
        public ActionResult Sil(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personel db_p = pm.Find(x => x.ID == id);
            if (db_p == null)
            {
                return HttpNotFound();
            }

            if (pm.Delete(db_p) > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            ViewBag.Id = id;
            Personel p = pm.Find(x => x.ID == id);
            if (p != null)
            {

                ViewBag.Sube = new SelectList(sm.List(), "Ad", "Ad", s.Ad);
                return PartialView("_PartialPersonelGuncelle", p);

            }
            else
            {

            }
            ViewBag.Sube = new SelectList(sm.List(), "Ad", "Ad", s.Ad);
            return View();
        }
        [HttpPost]
        public ActionResult Guncelle(int? id, Personel personel, FormCollection form)
        {
            if (ModelState.IsValid)
            {


                if (id == null)
                {
                    return Json("0");
                }
                Personel p = pm.Find(x => x.ID == id.Value);
                if (p == null)
                {
                    return Json("0");
                }
             
                p.Ad = personel.Ad;
                p.Soyad = personel.Soyad;
                p.Rutbe = personel.Rutbe;
                p.Gorev = personel.Gorev;
                p.DahiliNo = personel.DahiliNo;
                p.SubeAdi = personel.SubeAdi;
                p.IlAdi = personel.IlAdi;
                if (pm.Update(p) > 0)
                {
                    ViewBag.Sube = new SelectList(sm.List(), "Ad", "Ad", s.Ad);
                    return Json("1");
                }

            
         
            }
            else
            {
                return Json("0");
            }
            return Json("1");

        }

        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if (ModelState.IsValid)
            {
                if (excelfile == null || excelfile.ContentLength == 0)
                {
                    //Lutfen excel dosyasi secin
                    ErrorViewModel errorView = new ErrorViewModel()
                    {
                        Title = "Dosya Seçimi Yapılmadı.",
                        RedirectingUrl = "/Personel/Index/",
                        IsRedirecting = true
                    };
                    return View("Error", errorView);

                }
                else if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {

                    try
                    {
                        Excel.Application application = new Excel.Application();
                        string path = Request.MapPath("~/Content/" + excelfile.FileName);
                        if (System.IO.File.Exists(path))
                        {
                            application.Quit();
                            System.IO.File.Delete(path);
                        }

                        excelfile.SaveAs(path);
                        Excel.Workbook workbook = application.Workbooks.Open(path);
                        Excel.Worksheet worksheet = workbook.ActiveSheet;
                        Excel.Range range = worksheet.UsedRange;
                        List<Personel> listPersonel = new List<Personel>();
                        PersonelManager pm = new PersonelManager();
                        for (int i = 2; i <= range.Rows.Count; i++)
                        {
                            Personel p = new Personel();
                            p.Ad = ((Excel.Range)range.Cells[i, 1]).Text;
                            p.Soyad = ((Excel.Range)range.Cells[i, 2]).Text;
                            p.Rutbe = ((Excel.Range)range.Cells[i, 3]).Text;
                            p.Gorev = ((Excel.Range)range.Cells[i, 4]).Text;
                            p.DahiliNo = ((Excel.Range)range.Cells[i, 5]).Text;
                            p.IlAdi = ((Excel.Range)range.Cells[i, 6]).Text;
                            p.SubeAdi = ((Excel.Range)range.Cells[i, 7]).Text;



                            pm.Insert(p);

                            listPersonel.Add(p);

                        }
                        ViewBag.Personel = listPersonel;
                        workbook.Close(path);
                        application.Quit();
                        System.IO.File.Delete(path);
                        OkViewModel ok = new OkViewModel()
                        {
                            Title = "İşlem başarıyla tamamlandı!",
                            RedirectingUrl = "/Personel/Index",
                            IsRedirecting = true
                        };
                        ok.Items.Add("Excel Listesindeki veriler başarıyla eklendi");
                        return View("Ok", ok);
                        //return RedirectToAction("Index", "Personel");
                    }
                    catch (Exception ex)
                    {

                        ErrorViewModel errorView = new ErrorViewModel()
                        {
                            Title = "Geçersiz İşlem. Excel dosyanızın doğru yapıda olduğundan emin olunuz.",
                            RedirectingUrl = "/Personel/Index",
                            IsRedirecting = true
                        };
                        errorView.Items.Add("Bir hata oluştu");
                        return View("Error", errorView);
                    }
                }
                else
                {
                    ErrorViewModel errorView = new ErrorViewModel()
                    {
                        Title = "Sadece Excel Dosyası yükleyebilirsiniz.",
                        RedirectingUrl = "/Personel/Index",
                        IsRedirecting = true
                    };
                    errorView.Items.Add("Bir hata oluştu. ");
                    return View("Error", errorView);
                }

            }
            return View();

        }

    }
}