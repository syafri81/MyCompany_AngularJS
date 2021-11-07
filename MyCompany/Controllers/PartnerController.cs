using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCompany.Controllers
{
    public class PartnerController : Controller
    {
        public JsonResult GetIndex()
        {
            using (var ctx = new MyEntities())
            {
                var result = ctx.tblPartner.ToList();
                return Json(result);
            }
        }

        // GET: Partner
        public ActionResult Index()
        {
            Session[Global.EditID] = null;

            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.createPage = "/" + controllerName + "/Create";
            return View();
        }                

        [HttpPost]
        public JsonResult Edit(string id)
        {
            Session[Global.EditID] = id.ToString();
            return Json("success");
        }

        public ActionResult Create()
        {
            var model = new tblPartner();

            if(Session[Global.EditID] != null)
            {
                using (var ctx = new MyEntities())
                {
                    var id = Session[Global.EditID].ToString();
                    model = ctx.tblPartner.Where(m => m.IDNumber == id).FirstOrDefault();
                }
            }

            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.backPage = "/" + controllerName;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(tblPartner model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.IDNumber))
                    throw new Exception("ID number is required.");
                if (string.IsNullOrEmpty(model.Name))
                    throw new Exception("Partner name is required.");
                if (string.IsNullOrEmpty(model.PhoneNumber))
                    throw new Exception("Phone number is required.");

                using (var ctx = new MyEntities())
                {
                    var data = ctx.tblPartner.Where(m => m.IDPartner == model.IDPartner).FirstOrDefault();
                    if (data == null)
                    {
                        model.IsActive = true;
                        model.Created = DateTime.Now;
                        model.CreatedBy = 1;
                        ctx.tblPartner.Add(model);
                    }
                    else
                    {
                        if (model.IDPartner == 0)
                            throw new Exception("User with this ID Number already exist");

                        data.IDNumber = model.IDNumber;
                        data.Name = model.Name;
                        data.PhoneNumber = model.PhoneNumber;
                        data.Address1 = model.Address1;
                        data.Address2 = model.Address2;
                        data.Address3 = model.Address3;
                        data.IsActive = model.IsActive;
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

                    int del = ctx.Database.ExecuteSqlCommand("Delete from tblPartner where IDPartner = " + id);
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
                var update = ctx.Database.ExecuteSqlCommand("Update tblPartner set IsActive=" + isActive + " where IDPartner=" + id);
                return Json("success");
            }
        }
    }
}