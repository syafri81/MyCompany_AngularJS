using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCompany.Controllers
{
    public class SupplierController : Controller
    {
        public JsonResult GetIndex()
        {
            var activeUser = Service.GetActiveUser(Session);
            using (var ctx = new MyEntities())
            {
                var result = ctx.tblSupplier.Where(m => m.IDCampaign == activeUser.CampaignID).ToList();
                return Json(result);
            }
        }

        public JsonResult AutoComplete(string prefix)
        {
            var activeUser = Service.GetActiveUser(Session);

            using (var ctx = new MyEntities())
            {
                var result = (from m in ctx.tblSupplier
                              where m.SupplierName.StartsWith(prefix) && m.IDCampaign == activeUser.CampaignID
                              select new AutocompleteTemplate { val = m.IDSupplier, label = m.SupplierName }).ToList();
                return Json(result);
            }
        }

        // GET: Supplier
        public ActionResult Index()
        {
            Session[Global.EditID] = null;

            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.createPage = "/" + controllerName + "/Create";
            return View();
        }

        [HttpPost]
        public JsonResult Edit(int id)
        {
            Session[Global.EditID] = id.ToString();
            return Json("success");
        }

        public ActionResult Create()
        {
            var model = new tblSupplier();

            if (Session[Global.EditID] != null)
            {
                using (var ctx = new MyEntities())
                {
                    var id = Convert.ToInt32(Session[Global.EditID]);
                    model = ctx.tblSupplier.Where(m => m.IDSupplier == id).FirstOrDefault();
                }
            }

            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.backPage = "/" + controllerName;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(tblSupplier model)
        {
            try
            {
                var activeUser = Service.GetActiveUser(Session);
                model.SupplierName = model.SupplierName.ToUpper();

                if (string.IsNullOrEmpty(model.SupplierName))
                    throw new Exception("Supplier name is required.");
                if (string.IsNullOrEmpty(model.PhoneNumber))
                    throw new Exception("Phone number is required.");

                using (var ctx = new MyEntities())
                {
                    var data = ctx.tblSupplier.Where(m => m.IDSupplier == model.IDSupplier).FirstOrDefault();
                    if (data == null)
                    {
                        model.IsActive = true;
                        model.IDCampaign = activeUser.CampaignID;
                        model.Created = DateTime.Now;
                        model.CreatedBy = activeUser.UserID;

                        ctx.tblSupplier.Add(model);
                    }
                    else
                    {
                        data.SupplierName = model.SupplierName;
                        data.PhoneNumber = model.PhoneNumber;
                        data.IsActive = model.IsActive;
                        data.IDCampaign = activeUser.CampaignID;
                        data.Modified = DateTime.Now;
                        data.ModifiedBy = activeUser.UserID;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var dmr = new DMR();
            try
            {
                //throw new Exception("Cannot delete this data, it is being used.");

                using (var ctx = new MyEntities())
                {
                    
                    int del = ctx.Database.ExecuteSqlCommand("Delete from tblSupplier where IDSupplier = " + id);
                    dmr.messages.Add("success");
                }
            }
            catch (Exception ex)
            {
                dmr.isSuccess = false;
                dmr.messages.Add(ex.Message);
            }
            return Json(dmr);
        }

        public JsonResult MakeActive(int id, int isActive)
        {
            using (var ctx = new MyEntities())
            {
                var update = ctx.Database.ExecuteSqlCommand("Update tblSupplier set IsActive=" + isActive + " where IDSupplier=" + id);
                return Json("success");
            }
        }
    }
}