using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class Service
{
    public static UserTemplate GetActiveUser(HttpSessionStateBase session)
    {
        var user = new UserTemplate();
        user.UserID = Convert.ToInt32(session[Global.UserID].ToString());
        user.UserName = session[Global.UserName].ToString();
        user.DisplayName = session[Global.DisplayName].ToString();
        user.UserLevel = Convert.ToInt32(session[Global.UserLevel].ToString());
        user.CampaignID = Convert.ToInt32(session[Global.CampaignID].ToString());
        return user;
    }

    public static List<AutocompleteTemplate> DataCampaign(int campaignID = 0, bool isForCreate = false)
    {
        using (var ctx = new MyEntities())
        {
            var result = (from m in ctx.tblCampaign where m.IsActive == true
                          select new AutocompleteTemplate { val = m.IDCampaign, label = m.CampaignName }).ToList();

            if (isForCreate == true)
            {
                result[0].label = "-";
            }

            return result;
        }
    }

    public static List<AutocompleteTemplate> DataSupplier(int campaignID, int userLevel)
    {
        using (var ctx = new MyEntities())
        {
            var result = new List<AutocompleteTemplate>();
            //if (userLevel != Convert.ToInt32(UserlevelEnum.Admin))
            //{
            //    result = (from m in ctx.tblSupplier
            //              where m.IsActive == true && m.IDCampaign == campaignID
            //              select new AutocompleteTemplate { val = m.IDSupplier, label = m.SupplierName }).ToList();
            //}
            //else
            //{
            //    result = (from m in ctx.tblSupplier
            //              where m.IsActive == true
            //              select new AutocompleteTemplate { val = m.IDSupplier, label = m.SupplierName }).ToList();
            //}

            result = (from m in ctx.tblSupplier
                      where m.IsActive == true && m.IDCampaign == campaignID
                      select new AutocompleteTemplate { val = m.IDSupplier, label = m.SupplierName }).ToList();

            var noCombo = new AutocompleteTemplate();
            noCombo.val = 0;
            noCombo.label = "-";
            result.Insert(0, noCombo);

            return result;
        }
    }

    public static SelectList ComboCampaign(int campaignID = 0, bool isForCreate = false)
    {
        var result = DataCampaign(campaignID, isForCreate);

        return new SelectList(result, "val", "label", campaignID);
    }

    public static SelectList ComboSupplier(int userLevel, int campaignID, int supplierID = 0)
    {
        var result = DataSupplier(campaignID, userLevel);

        return new SelectList(result, "val", "label", supplierID);
    }
}
