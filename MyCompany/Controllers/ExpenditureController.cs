using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCompany.Controllers
{
    public class ExpenditureController : Controller
    {
        public JsonResult GetIndex()
        {
            var result = new ExpendTemplate();
            using (var ctx = new MyEntities())
            {
                var expends = ctx.Database.SqlQuery<View_Expend>("Select * from View_Expend where Month(Created) = " + DateTime.Now.Month +
                    " And Year(Created) = " + DateTime.Now.Year).ToList();
                result.expends = expends;
            }

            var campaigns = Service.DataCampaign();
            result.campaigns = campaigns;

            return Json(result);
        }

        // GET: Expenditure
        public ActionResult Index()
        {
            Session[Global.EditID] = null;

            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.createPage = "/" + controllerName + "/Create";
            return View();
        }

        public JsonResult GetDetail(int idExpend)
        {
            using (var ctx = new MyEntities())
            {
                var detail = ctx.Database.SqlQuery<View_ExpendDetail>("Select * from View_ExpendDetail where IDExpend = " + idExpend).ToList();

                return Json(detail);
            }
        }

        public JsonResult RemoveDetail(int idDetail, int idExpend)
        {
            using (var ctx = new MyEntities())
            {
                int del = ctx.Database.ExecuteSqlCommand("Delete from inExpendDetail where IDDetail = " + idDetail);

                var exist = ctx.inExpendDetail.Where(m => m.IDExpend == idExpend).Count();
                if (exist == 0)
                    del = ctx.Database.ExecuteSqlCommand("Delete from inExpend where IDExpend = " + idExpend);

                return Json("Removed");
            }
        }

        [HttpPost]
        public JsonResult Edit(int id)
        {
            Session[Global.EditID] = id.ToString();
            return Json("success");
        }

        public ActionResult Create()
        {
            var activeUser = Service.GetActiveUser(Session);
            var model = new inExpend();

            if (Session[Global.EditID] != null)
            {
                using (var ctx = new MyEntities())
                {
                    var id = Convert.ToInt64(Session[Global.EditID]);
                    model = ctx.inExpend.Where(m => m.IDExpend == id).FirstOrDefault();

                    ////for edit mode that user removes all details, need to go back to home
                    if (model == null)
                        return RedirectToAction("Index");

                }
            }

            //model.IDFaktur = "000.002.18";

            if (model.Created == DateTime.MinValue)
                model.Created = DateTime.Now;

            ViewBag.CreatedDay = model.Created.ToString("dd-MM-yyyy");
            ViewData["IDSupplier"] = Service.ComboSupplier(activeUser.UserLevel, activeUser.CampaignID, model.IDSupplier);

            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.backPage = "/" + controllerName;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(inExpend model, FormCollection f)
        {
            try
            {
                var activeUser = Service.GetActiveUser(Session);
                ViewBag.CreatedDay = model.Created.ToString("dd-MM-yyyy");
                ViewData["IDSupplier"] = Service.ComboSupplier(activeUser.UserLevel, model.IDSupplier);

                var details = new List<inExpendDetail>();
                var maxID = Convert.ToInt32(f["maxID"]);
                for (int i = 1; i <= maxID; i++)
                {
                    try
                    {
                        var d = new inExpendDetail();
                        d.IDProduct = Convert.ToInt32(f["idproduct_" + i]);
                        d.Volume = Convert.ToInt32(f["volume_" + i]);
                        d.Price = Convert.ToDecimal(f["price_" + i]);
                        d.Amount = Convert.ToDecimal(f["amount_" + i]);
                        d.Created = DateTime.Now;
                        d.CreatedBy = activeUser.UserID;

                        if (d.IDProduct > 0)
                            details.Add(d);                        
                    }
                    catch
                    {
                        if (i == 1)
                            throw new Exception("Expenditure detail is required.");
                        break;
                    }
                }
                
                if (model.IDSupplier == 0)
                    throw new Exception("Supplier is required.");

                using (var ctx = new MyEntities())
                {
                    var data = ctx.inExpend.Where(m => m.IDExpend == model.IDExpend).FirstOrDefault();
                    if (data == null)
                    {
                        model.Amount = details.Select(m => m.Amount).Sum();
                        model.Created = DateTime.Now;
                        model.CreatedBy = activeUser.UserID;
                        ctx.inExpend.Add(model);
                    }

                    ctx.SaveChanges();

                    foreach(var d in details)
                    {
                        d.IDExpend = model.IDExpend;
                        ctx.inExpendDetail.Add(d);
                        ctx.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

            return RedirectToAction("Index");

        }

        public JsonResult Delete(long id)
        {
            var dmr = new DMR();
            try
            {
                //throw new Exception("Cannot delete this data, it is being used.");

                using (var ctx = new MyEntities())
                {

                    int del = ctx.Database.ExecuteSqlCommand("Delete from inExpendDetail where IDExpend = " + id);
                    del = ctx.Database.ExecuteSqlCommand("Delete from inExpend where IDExpend = " + id);
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

        public JsonResult Detail(long id)
        {
            using (var ctx = new MyEntities())
            {
                var result = new ExpendDetail();
                var expend = ctx.View_Expend.Where(m => m.IDExpend == id).FirstOrDefault();
                result.IDFaktur = expend.IDFaktur;
                result.SupplierName = expend.SupplierName;
                result.FakturDate = expend.Created.ToString("dd-MM-yyyy");
                result.Amount = expend.Amount;
                result.Details = ctx.View_ExpendDetail.Where(m => m.IDExpend == id).ToList();

                return Json(result);
            }
        }
    }
}