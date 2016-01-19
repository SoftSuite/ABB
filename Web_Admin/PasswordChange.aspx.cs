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

public partial class PasswordChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.lblUserText.Text = Authz.CurrentUserInfo.UserID; //((Label)this.ctlChangePassword.Controls[0].FindControl("lblUserText")).Text = Authz.CurrentUserInfo.UserID;
    }

    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        this.FailureText.Text = "";
        AppFlow uFlow = new AppFlow();
        if (uFlow.IsValidPassword(Authz.CurrentUserInfo.UserID, this.CurrentPassword.Text.Trim()))
        {
            if (!uFlow.ChangePassword(Authz.CurrentUserInfo.OfficerID, this.NewPassword.Text.Trim()))
            {
                this.FailureText.Text = uFlow.ErrorMessage;
            }
            else
                Appz.ClientAlert(this.Page, "เปลี่ยนรหัสผ่านเรียบร้อยแล้ว");
        }
        else
        {
            this.FailureText.Text = uFlow.ErrorMessage;
        }
    }

    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Message/Message.aspx");
    }
}
