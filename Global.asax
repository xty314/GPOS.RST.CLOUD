<%@ Language="C#"%>
<%-- <%@ Import Namespace="JinianNet.JNTemplate" %> --%>
<script runat="server">
    void Application_Start(Object Src, EventArgs E)
    {

        UpdateDatabase();


    }
    void Application_End(object sender, EventArgs e)
    {

    }

    void Application_BeginRequest(object sender,EventArgs e)
    {




    }
    protected void Session_Start(object sender,EventArgs e)
    {

    }

    private void UpdateDatabase(){
        DBhelper dbhelper=new DBhelper();
        // execute all .sql files under the sql folder.
        System.IO.DirectoryInfo sqlDir = new System.IO.DirectoryInfo(Server.MapPath("~/sql/"));
        System.IO.FileInfo[] sqlFiles=sqlDir.GetFiles();
        for(int i=0;i<sqlFiles.Length;i++){
            string type = sqlFiles[i].FullName.Substring(sqlFiles[i].FullName.LastIndexOf(".") + 1).ToLower();
            if(type=="sql"){
                string content = System.IO.File.ReadAllText(sqlFiles[i].FullName);
                dbhelper.ExecuteNonQuery(content);
            }

        }
    }
    void Application_Error(object sender, EventArgs e)
    {




        //HttpException error = Server.GetLastError() as HttpException;

        //if (error != null)
        //{
        //    var statusCode = error.GetHttpCode();
        //    if (statusCode == 500)
        //    {
        //        Response.StatusCode = 500;
        //        Server.ClearError();
        //        Application["error"] = error.GetHtmlErrorMessage();
        //        //Response.Redirect("/v2/500.aspx");
        //    }
        //    else if (statusCode == 404)
        //    {
        //        Response.StatusCode = 404;
        //        Server.ClearError();
        //        Response.Write(error.Message);
        //        Server.Transfer("/v2/error.aspx", false);
        //    }

        //}

    }

</script>
