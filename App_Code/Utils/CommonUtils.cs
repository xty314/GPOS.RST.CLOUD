using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommonUtils
/// </summary>
public class CommonUtils
{
    public CommonUtils()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string GetWebPageTitle()
    {
        string title = (string)HttpContext.Current.Items["Title"];
        if (String.IsNullOrEmpty(title))
        {
            title = Settings.GetSetting("DBname");
        }
        return title;
    }
    public static void SetWebPageTitle(String Title)
    {
        HttpContext.Current.Items["Title"] = Title;

    }
}