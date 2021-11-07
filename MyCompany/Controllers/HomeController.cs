using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCompany.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult GetIndex()
        {
            using (var ctx = new MyEntities())
            {
                var result = (from m in ctx.tblCampaign
                              where m.IDCampaign > 0
                              select new AutocompleteTemplate { val = m.IDCampaign, label = m.CampaignName }).ToList();
                return Json(result);
            }
        }

        public ActionResult Index()
        {
            //if (Session[Global.UserID] == null)
            //    return RedirectToAction("Login", "Account");

            return View();
        }

        public JsonResult Logoff()
        {
            Session.RemoveAll();
            return Json("Logoff");
        }

        public JsonResult GoToCampaign(int id)
        {
            Session[Global.CampaignID] = id;
            return Json("success");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}