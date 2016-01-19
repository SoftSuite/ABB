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
using ABB.Flow.Admin;
using ABB.Data.Admin;
using ABB.Global;
using ABB.Data;

public partial class Master_CreditCard : System.Web.UI.Page
{
    private CreditCardFlow _flow;
    private CreditCardFlow FlowObj
    {
        get { if (_flow == null) { _flow = new CreditCardFlow(); } return _flow; }
    }

    private void ResetState(double LOID)
    {
        CreditCardData data = FlowObj.GetData(LOID);
        if (LOID == 0)
        {
            data.CODE = FlowObj.GenNewCode();
        }
        SetData(data);
    }

    private CreditCardData GetData()
    {
        CreditCardData data = new CreditCardData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtCode.Text.Trim();
        data.NAME = this.txtName.Text.Trim();
        data.CHARGE = Convert.ToDouble(this.txtCharge.Text == "" ? "0" : this.txtCharge.Text);
        return data;
    }

    private void SetData(CreditCardData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE.Trim();
        this.txtName.Text = data.NAME.Trim();
        this.txtCharge.Text = data.CHARGE.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/CreditCardSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);

    }
}
