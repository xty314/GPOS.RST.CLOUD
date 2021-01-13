<%@ WebHandler Language="C#" Class="ItemSelectHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class ItemSelectHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string kw = context.Request.QueryString["kw"].ToString();
        string sc;
        if (CommonUtils.IsNumeric(kw))
        {
            sc = "SELECT code,name,supplier_code FROM code_relations where code=@code or name like @kw or supplier_code like @kw " ;
        }
        else
        {
            sc = "SELECT code,name,supplier_code FROM code_relations where supplier_code like @kw or name like @kw";
        }

        DBhelper dbhelper = new DBhelper();
        SqlParameter[] pars = new SqlParameter[]
        {
            new SqlParameter("@code",kw),
            new SqlParameter("@kw","%"+kw+"%")
         };
        DataTable dt = dbhelper.ExecuteDataTable(sc, pars);
        context.Response.Write(dt.ToJson());

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}
class ItemDto
{
    public string name { get; set; }
    public string url { get; set; }
    public string picUrl { get; set; }
    public string MyProperty { get; set; }
}