
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using TuesPechkin;


/// <summary>
/// 当然，在实际环境里，如果你使用IIS，并且希望通过ASP.NET生成PDF，需要注意权限，
/// 首先，找到应用程序所使用的应用程序池，点击“应用程序池”上的高级，有一个“标识”，
/// 将默认的ApplicationPoolIdentity修改为LocalSystem。否则，可能因为权限不足而调用exe失败。
/// </summary>
public class Pdfhelper 
{
    public Pdfhelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public static void BuildPdf()
    //{
    //    string page = "~/v2/default.aspx";
    //    string pdf = System.Web.Hosting.HostingEnvironment.MapPath("~/pdf/wkhtmltox/bin/wkhtmltopdf.exe");
    //    //string pdf = Server.MapPath("~/Upload/wkhtmltox/wkhtmltox/bin/wkhtmltopdf.exe");//转换器路径
    //    string pdfpath = "~/pdf/invoice.pdf";//生成的PDF文件路径                                  //参数中\"表示字符"，注意参数"生成的PDF文件路径"要保留双引号
    //    Process p = System.Diagnostics.Process.Start(pdf, " -O Landscape " + page + " \"" + System.Web.Hosting.HostingEnvironment.MapPath(pdfpath) + "\"");//-O 指定页面布局为横向或纵向
    //    p.WaitForExit();
     
    //}
    public static void BuildInvoicePdf(string invoice)
    {
        string host = HttpContext.Current.Request.Url.Authority;
        string page = "http://"+host+"/admin/invoice.aspx?id="+invoice;
        string sessionId = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value;
        //string pdf = System.Web.Hosting.HostingEnvironment.MapPath("~/pdf/wkhtmltox/bin/wkhtmltopdf.exe");
        string pdf = HttpContext.Current.Server.MapPath("~/export/pdf/wkhtmltox/bin/wkhtmltopdf.exe");//转换器路径
        string pdfpath = HttpContext.Current.Server.MapPath("~/export/pdf/invoice/inv#" +invoice+".pdf");//生成的PDF文件路径                                  //参数中\"表示字符"，注意参数"生成的PDF文件路径"要保留双引
        Process p = System.Diagnostics.Process.Start(pdf, "-q --cookie \"ASP.NET_SessionId\" \"" + sessionId + "\" " + page + " \"" + pdfpath + "\"");//-O 指定页面布局为横向或纵向

   
    }
    public static void Pdf()
    {
        string host = HttpContext.Current.Request.Url.Authority;
        //string page = "http://" + host + "/admin/invoice.aspx?id=";
        string page = "http://gpos.gposnz.com/admin/invoice.aspx?id=18591&r=44209.5632964236";
        //string sessionId = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value;
        string sessionId = "zpj0ll45rmd3kfir0zfs5ovi";
        //string pdf = System.Web.Hosting.HostingEnvironment.MapPath("~/pdf/wkhtmltox/bin/wkhtmltopdf.exe");
        string pdf = HttpContext.Current.Server.MapPath("~/export/pdf/wkhtmltox/bin/wkhtmltopdf.exe");//转换器路径
        string pdfpath = HttpContext.Current.Server.MapPath("~/export/pdf/invoice/inv#454665465464.pdf");//生成的PDF文件路径   
        ProcessStartInfo startInfo = new ProcessStartInfo();

        startInfo.UseShellExecute = false;

        startInfo.RedirectStandardOutput = true;

        startInfo.RedirectStandardInput = true;

        startInfo.RedirectStandardError = true;

        startInfo.CreateNoWindow = true;

        startInfo.FileName = HttpContext.Current.Server.MapPath("~/export/pdf/wkhtmltox/bin/wkhtmltopdf.exe");
        startInfo.Arguments = " --cookie \"ASP.NET_SessionId\" \"" + sessionId + "\" " + page + " \"" + pdfpath + "\"";
        Process myProcess = Process.Start(startInfo);
        string output = myProcess.StandardOutput.ReadToEnd();
        byte[] buffer = myProcess.StandardOutput.CurrentEncoding.GetBytes(output);
  
        myProcess.WaitForExit();
      

        myProcess.Close();
        HttpContext.Current.Response.Write("sdfsdfsdfsdfsdfsdfsdfsdfsdfsdfsdfsdfsdf");
        HttpContext.Current.Response.Write(output);
        HttpContext.Current.Response.BinaryWrite(buffer);
        //HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
        //Response.Clear();

        //Response.AddHeader("content-disposition", "attachment;filename=abc.pdf");

        //Response.ContentType = "application/pdf";

        //Response.WriteFile(destinationFile);
        //Response.End();
    }
    public static void test()
    {
        string sessionId = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value;
 
        LoadSettings loadSettings = new LoadSettings();
        loadSettings.Cookies.Add("ASP.NET_SessionId", sessionId);

        var document = new HtmlToPdfDocument
        {
         
            GlobalSettings ={
                    ProduceOutline = true,
                    DocumentTitle = "retty Website",
            
                    PaperSize = PaperKind.A4, // Implicit conversion to PechkinPaperSize
                    Margins =
                    {
                    All = 1.375,
                    Unit = Unit.Centimeters
                    }
             },
            Objects = {
  
            new ObjectSettings { PageUrl = "www.google.com"
            ,LoadSettings=loadSettings
                }
            }
         };
     
        IConverter converter =
      new ThreadSafeConverter(
          new RemotingToolset<PdfToolset>(
              new Win32EmbeddedDeployment(
                  new TempFolderDeployment())));

        // Keep the converter somewhere static, or as a singleton instance!
        // Do NOT run the above code more than once in the application lifecycle!

        byte[] result = converter.Convert(document);
        FileStream pFileStream = null;
        string pdfpath = HttpContext.Current.Server.MapPath("~/export/pdf/invoice/ddasa.pdf");//生成的PDF文件路径  
        pFileStream = new FileStream(pdfpath, FileMode.OpenOrCreate);
            pFileStream.Write(result, 0, result.Length);



        }
}