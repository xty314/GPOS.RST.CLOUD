using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using System.Data;
using System.Collections;
using System.Web.Caching;
using System.IO;
using System.Text;
using System.Globalization;
using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Web.Mail;
using System.Web.Security;
//using System.Web.Caching.Cache

/// <summary>

/// </summary>
public partial class Common : System.Web.UI.Page
{
    public string r = "r=";
    public string m_sAdminFooter = "</td></tr></table></td></tr><tr><td><a href=default.aspx?";
    public string m_sMenuTables = "";
    public bool m_bShowProgress = false;
    public void PrintSearchForm()
    {
        string kw = "";

        string kwCompare = "";
        if (Request.QueryString["kw"] != null && Request.QueryString["kw"] != "")
            kwCompare = Request.QueryString["kw"].ToString();
        if (Session["search_keyword"] != null)
        {
            if (Session["search_keyword"].ToString() == kwCompare)
                kw = Session["search_keyword"].ToString();
            else
                kw = kwCompare;
        }
        string frmSearch = ReadSitePage("admin_search");
        frmSearch = frmSearch.Replace("@@search_keyword", kw);
        Response.Write(frmSearch);
    }
}