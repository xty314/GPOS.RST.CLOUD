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

    public string m_sSite = "www";
    public  string m_sRoot = "";
    public  bool m_bCheckLogin = false;
    public bool m_bDealerArea = false;
}