using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ABB.Data;
using ABB.Flow;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Message/Message.aspx");
    }

    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        this.txtResult.Text = AppFlow.Encrypt(this.txtKey.Text.Trim());
    }

    protected void btnDecrypt_Click(object sender, EventArgs e)
    {
        this.txtResult.Text = AppFlow.Decrypt(this.txtKey.Text.Trim());
    }
}