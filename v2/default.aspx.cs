using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class v2_default : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CommonUtils.SetWebPageTitle("Home");
        TS_PageLoad(); //do common things, LogVisit etc...
        if (!SecurityCheck("sales"))
            return;
        //Pdfhelper.test();


        //MailData md = new MailData()
        //{
        //    From= "support@gpos.co.nz",
        //    Content= ReadSitePage("email_invoice_content"),
        //    Subject ="qunfaceshi",
        //    MailTextType=MediaTextType.Html

        //};
        //md.ToCollection.Add("alex.x@gpos.co.nz");
        //md.ToCollection.Add("xutieyuan314@gmail.com");
        //md.ToCollection.Add("xutieyuan314@163.com");
        //md.Attachments.Add(HttpContext.Current.Server.MapPath("~/export/pdf/invoice/ddasa1.pdf"));
        //md.Attachments.Add(HttpContext.Current.Server.MapPath("~/export/pdf/invoice/ddasa.pdf"));
        //SmtpClient client = SmtpClientService.GoogleSmtpClient("support@gpos.co.nz","9aysdata");
        //Mailhelper.SendMail(client, md);
  
        //client.Send(msg);
    }
}