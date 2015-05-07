using EliaStore.Models;
using EliaStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliaStore.Controllers
{
    public class ShowroomController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Details(string brandname, string modelname, int year)
        {
            var showroom = new ShowroomViewModel();
            showroom.shirt = db.shirts.Where(c => c.brand.name == brandname && c.model.name == modelname ).FirstOrDefault();

            showroom.related_shirts = db.shirts.Where(c => c.brand.name == brandname && c.model.name != modelname).ToList();
            ViewBag.showroom = showroom;
            return View();
        }

    }

}