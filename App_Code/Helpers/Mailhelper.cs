using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Mailhelper
/// </summary>

public class Mailhelper
{



    public static void SendMail(SmtpClient client,MailData mailDate)
    {


     
            var mailMsg = new MailMessage();
            if (mailDate.ToCollection.Count < 1)
            {
                throw new Exception(" no receiver ");
            }
            foreach(string To in mailDate.ToCollection)
            {
                mailMsg.To.Add(new MailAddress(To));
            }
    
            mailMsg.From = new MailAddress(mailDate.From);
            // 邮件主题
            mailMsg.Subject = mailDate.Subject;
            // 邮件正文内容

            if (mailDate.MailTextType == MediaTextType.Text)
            {
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mailDate.Content, null,
                    MediaTypeNames.Text.Plain));
            }
            if (mailDate.MailTextType == MediaTextType.Html)
            {
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mailDate.Content, null,
                    MediaTypeNames.Text.Html));
            }
            foreach (string Attachment in mailDate.Attachments)
            {
                if (File.Exists(Attachment))
                {
                    var data = new Attachment(Attachment, MediaTypeNames.Application.Octet);
                    mailMsg.Attachments.Add(data);
                }
                else
                {
                    throw new Exception(Attachment + " not exist ");
                }
            
            }


            client.Send(mailMsg);

         
        }
   

    }


public class MailData
{
    public string From { get; set; }
    public List<string> ToCollection { get; private set; }
    public string Content { get; set; }
    public string Subject { get; set; }

    public MediaTextType MailTextType { get; set; }

    public MailData()
    {
        this.Attachments = new List<string>();
        this.ToCollection = new List<string>();
    }
    public List<string> Attachments { get; private set; }

}

public enum MediaTextType
{
    /// <summary>
    /// 文本格式
    /// </summary>
    Text = 1,
    /// <summary>
    /// 网页格式
    /// </summary>
    Html = 2,
}

public class SmtpClientService
{
    public static SmtpClient GoogleSmtpClient(string email,string password)
    {
        SmtpClient client =  new SmtpClient("smtp.gmail.com", 25);
        client.UseDefaultCredentials = false;
        NetworkCredential basicAuthenticationInfo = new NetworkCredential(email,password);
        client.Credentials = basicAuthenticationInfo;
        client.EnableSsl = true; 
        return client;
    }
    public static SmtpClient LocalSmtpClient()
    {
        SmtpClient client = new SmtpClient("localhost");

        return client;
    }


}
