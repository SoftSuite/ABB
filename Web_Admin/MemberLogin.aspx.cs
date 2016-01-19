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
using ABB.Flow;
using ABB.Global;

public partial class MemberLogin : System.Web.UI.Page
{
    private DropDownList Warehouse
    {
        get { return (DropDownList)this.ctlLogin.Controls[0].FindControl("Warehouse"); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(Warehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ");
        }
    }

    protected void ctlLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        AppFlow uFlow = new AppFlow();
        e.Authenticated = uFlow.IsAuthenticated(this.ctlLogin.UserName, this.ctlLogin.Password, Convert.ToDouble(Warehouse.SelectedItem.Value));
        if (e.Authenticated)
        {
            HttpContext.Current.Response.Cookies.Clear();

            FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1, this.ctlLogin.UserName, DateTime.Now, DateTime.Now.AddDays(1), true, "");
            HttpCookie cookie = new HttpCookie(".SingleSignOn");
            cookie.Value = FormsAuthentication.Encrypt(fat);
            cookie.Expires = fat.Expiration;
            HttpContext.Current.Response.Cookies.Add(cookie);

            Authz.SetUserLogIn(uFlow.Data);
        }
        else
        {
            this.ctlLogin.FailureText = uFlow.ErrorMessage;
        }
    }

    protected void ctlLogin_LoggedIn(object sender, EventArgs e)
    {
        //Response.Redirect(FormsAuthentication.GetRedirectUrl(this.ctlLogin.UserName, true));
        if (Request["ReturnUrl"] == null || Request["ReturnUrl"] == "")
            Response.Redirect(Constz.HomeFolder + "Message/Message.aspx");
        else
            Response.Redirect(Request["ReturnUrl"]);
    }
}
