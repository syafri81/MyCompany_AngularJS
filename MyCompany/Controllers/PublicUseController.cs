using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCompany.Controllers
{
    public class PublicUseController : Controller
    {
        public JsonResult AutoCompleteProduct(string prefix, int idSupplier)
        {
            var activeUser = Service.GetActiveUser(Session);

            using (var ctx = new MyEntities())
            {
                var result = (from m in ctx.tblProduct
                              where m.ProductName.StartsWith(prefix) && m.IDSupplier == idSupplier
                              select new AutocompleteTemplate { val = m.IDProduct, label = m.ProductName, price = m.Price }).ToList();
                return Json(result);
            }
        }
        public JsonResult ComboSupplier(int campaignID)
        {
            var activeUser = Service.GetActiveUser(Session);
            using (var ctx = new MyEntities())
            {
                var result = Service.DataSupplier(campaignID, activeUser.UserLevel);

                return Json(result);
            }
        }
    }
}