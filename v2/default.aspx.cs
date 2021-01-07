using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}