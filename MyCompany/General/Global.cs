using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Global
{
    public static string UserID = "UserID";
    public static string UserName = "UserName";
    public static string UserLevel = "UserLevel";
    public static string DisplayName = "DisplayName";
    public static string CampaignID = "CampaignID";
    public static string EditID = "EditID";
}

public enum UserlevelEnum
{
    Nothing = 0,
    Counter = 1,
    Admin = 9999
}

public class UserTemplate
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public int UserLevel { get; set; }
    public int CampaignID { get; set; }
}

public class ProductTemplate
{
    public List<View_Product> products;
    public List<AutocompleteTemplate> campaigns;
}

public class ExpendTemplate
{
    public List<View_Expend> expends;
    public List<AutocompleteTemplate> campaigns;
}