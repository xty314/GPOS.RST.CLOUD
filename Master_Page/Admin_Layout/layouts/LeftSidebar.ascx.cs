using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Page_LeftSidebar : System.Web.UI.UserControl
{
    protected DataTable menuTable;
    protected string sc;
    public DBhelper dbhelper = new DBhelper();
    public Common common;
    protected void Page_Load(object sender, EventArgs e)
    {

        common = new Common();
        //string accessLevel = Session[Company.m_sCompanyName + "AccessLevel"].ToString();
        sc = "select * from menu_admin_catalog order by seq";
        //if (accessLevel == "10")
        //{
        //    sc = "select * from menu_admin_catalog order by seq";
        //}
        //else
        //{
        //    sc = "select * from menu_admin_catalog order by seq";
        //}

        menuTable = dbhelper.ExecuteDataTable(sc);
    }
    protected DataTable getSubTable(string id)
    {
        //string accessLevel = Session[Company.m_sCompanyName + "AccessLevel"].ToString();
        string accessLevel = "10";
        if (accessLevel == "10")
        {
            sc = @"SELECT i.name,i.uri FROM menu_admin_sub s 
              JOIN  menu_admin_id i ON s.menu=i.id
           
                WHERE s.cat=" + id;
        }
        else
        {
            sc = @"SELECT i.name,i.uri FROM menu_admin_sub s 
              JOIN  menu_admin_id i ON s.menu=i.id
              JOIN menu_admin_access a on a.menu=i.id
                WHERE s.cat=" + id + " AND a.class=" + accessLevel;
        }

        //Response.Write(sc);
        DataTable menuSubTable = dbhelper.ExecuteDataTable(sc);
        return menuSubTable;
    }
    protected string GetFontawesome(int i)
    {
        string[] fontName = { "fa-tachometer-alt","fa-th","fa-copy" ,"fa-chart-pie","fa-tree","fa-edit","fa-table",
        "fa-calendar-alt","fa-image","fa-envelope","fa-book","fa-plus-square","fa-file"};

        int t = i % fontName.Length;
        return fontName[t];
    }
    protected string UseNewVersionMenu(string submenu)
    {
        string[] newMenu = { "eprice.aspx?", "ec.aspx", "login.aspx?logoff=true", "close.htm" ,"default.aspx","olist.aspx","olist.aspx?o=8"
                ,"pos.aspx?p=new&ft=order","checkorder.aspx","pos_retail.aspx?p=new","pe.aspx","ec.aspx","eprice.aspx?ph=1&d=&c="
        ,"stock_adj.aspx","stocktake.aspx","qpurchase.aspx?t=new","purchase.aspx","lowstock.aspx","stock.aspx","plist.aspx?t=2&s=1"
        ,"ebutton.aspx","extra_cat.aspx","copyitems.aspx","cashup.aspx","extra.aspx","setpayment_method.aspx","card.aspx?type=4"
        ,"ecard.aspx?n=employee&a=new","card.aspx?type=6","ecard.aspx?n=customer&a=new","ecard.aspx?n=dealer&a=new","ecard.aspx?n=member&a=new"
        ,"ecard.aspx?n=supplier&a=new"};
        if (Array.IndexOf(newMenu, submenu) != -1)
        {
            return "" + submenu;
        }
        else
        {
            return "./" + submenu;
        }

    }
}