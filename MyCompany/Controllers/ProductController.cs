using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCompany.Controllers
{
    public class ProductController : Controller
    {
        public JsonResult GetIndex()
        {
            var result = new ProductTemplate();
            var activeUser = Service.GetActiveUser(Session);
            using (var ctx = new MyEntities())
            {
                var products = new List<View_Product>();                
                if (activeUser.UserLevel == Convert.ToInt32(UserlevelEnum.Admin))
                {
                    products = ctx.View_Product.ToList();
                }
                else
                {
                    products = ctx.View_Product.Where(m => m.IDCampaign == activeUser.CampaignID).ToList();
                }                    

                var campaigns = Service.DataCampaign();

                result.products = products;
                result.campaigns = campaigns;

                return Json(result);
            }
        }

        // GET: Product
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
            var model = new tblProduct();

            if (Session[Global.EditID] != null)
            {
                using (var ctx = new MyEntities())
                {
                    var id = Convert.ToInt32(Session[Global.EditID]);
                    model = ctx.tblProduct.Where(m => m.IDProduct == id).FirstOrDefault();
                }
            }
                        
            ViewData["IDCampaign"] = Service.ComboCampaign(model.IDCampaign, true);

            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.backPage = "/" + controllerName;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(tblProduct model)
        {
            try
            {
                ViewData["IDCampaign"] = Service.ComboCampaign(model.IDCampaign, true);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                ViewBag.backPage = "/" + controllerName;

                var activeUser = Service.GetActiveUser(Session);
                if (!string.IsNullOrEmpty(model.ProductName))
                    model.ProductName = model.ProductName.ToUpper();

                if (string.IsNullOrEmpty(model.ProductName))
                    throw new Exception("Product name is required.");
                if (model.IDCampaign == 0)
                    throw new Exception("Campaign is required.");
                if (model.IDSupplier == 0)
                    throw new Exception("Supplier is required.");

                using (var ctx = new MyEntities())
                {
                    var data = ctx.tblProduct.Where(m => m.IDProduct == model.IDProduct).FirstOrDefault();
                    if (data == null)
                    {
                        model.IsActive = true;
                        model.Created = DateTime.Now;
                        model.CreatedBy = activeUser.UserID;

                        ctx.tblProduct.Add(model);
                    }
                    else
                    {
                        data.ProductName = model.ProductName;
                        data.Price = model.Price;
                        data.Weight = model.Weight;
                        data.Size = model.Size;
                        data.IsActive = model.IsActive;
                        data.IDCampaign = model.IDCampaign;
                        data.IDSupplier = model.IDSupplier;
                        data.Modified = DateTime.Now;
                        data.ModifiedBy = activeUser.UserID;
                    }

                    ctx.SaveChanges();

                    //save this price history
                    ProductClass.SavePriceHistory(model.IDProduct, model.Price, ctx);
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

                    int del = ctx.Database.ExecuteSqlCommand("Delete from tblProduct where IDProduct = " + id);
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
                var update = ctx.Database.ExecuteSqlCommand("Update tblProduct set IsActive=" + isActive + " where IDProduct=" + id);
                return Json("success");
            }
        }
    }
}