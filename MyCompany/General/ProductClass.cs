using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ProductClass
{
    public static void SavePriceHistory(int idProduct, decimal price, MyEntities ctx)
    {
        var model = new tblProductHistory();
        model.IDProduct = idProduct;
        model.Price = price;
        model.Created = DateTime.Now;
                
        var lastPrice = ctx.tblProductHistory.Where(m => m.IDProduct == idProduct).OrderByDescending(m => m.IDHistory).FirstOrDefault();
        if (lastPrice == null)
        {
            ctx.tblProductHistory.Add(model);
        }
        else
        {
            if (lastPrice.Price != price)
            {
                ctx.tblProductHistory.Add(model);
            }
        }

        ctx.SaveChanges();
    }
}