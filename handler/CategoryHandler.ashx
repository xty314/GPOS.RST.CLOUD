<%@ WebHandler Language="C#" Class="CategoryHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class CategoryHandler : IHttpHandler {
    public   DataSet dst = new DataSet();
    public void ProcessRequest (HttpContext context) {

        var list = new System.Collections.Generic.List<Object>();
 
        string sc = "";
        sc += " select Row_Number() over (order by cat,s_cat,ss_cat ) as id,t.* from ( ";
        sc += @"SElect distinct cat,
				case when s_cat='zzzOthers' then ''  when s_cat is null then '' else s_cat  end as 's_cat',
				case when ss_cat='zzzOthers' then '' when ss_cat is null then '' else ss_cat  end as 'ss_cat'
				from catalog WHERE 1=1 AND cat!='Brands'";
        sc += "  ) as t ORDER by  cat, s_cat, ss_cat";
        DBhelper dbhelper = new DBhelper();
        DataTable dt = dbhelper.ExecuteDataTable(sc);
        DataRow dr =dt.Rows[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dr = dt.Rows[i];
            list.Add(new { cat = dr["cat"].ToString(), sCat = dr["s_cat"].ToString(), ssCat = dr["ss_cat"].ToString(), id = dr["id"].ToString() });
        }


        var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string myObjectJson = javaScriptSerializer.Serialize(list);
        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.Write(myObjectJson);

    }

    public bool IsReusable {
        get {
            return false;
        }
    }
  

}