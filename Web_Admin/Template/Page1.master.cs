using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ABB.Data;
using ABB.Global;

public partial class Template_Page1 : System.Web.UI.MasterPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            if (Page.User.Identity.Name == "") FormsAuthentication.RedirectToLoginPage();
            this.lblUser.Text = Page.User.Identity.Name + " [" + Authz.CurrentUserInfo.Name + "] ";
            this.LoginStatus1.LogoutText = "<img src='" + Constz.ImageFolder + "Logout.png" + "' border='0' align='AbsMiddle'> ออกจากระบบ";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

}
