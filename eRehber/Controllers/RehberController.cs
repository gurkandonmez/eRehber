using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using eRehber.Entities;
using eRehber.BusinessLayer;

namespace eRehber.Controllers
{
    public class RehberController : Controller
    {
        // GET: Rehber
        public ActionResult Index()
        {
            return View();
        }

   
    }
}