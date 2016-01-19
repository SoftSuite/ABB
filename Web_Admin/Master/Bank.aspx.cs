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

public partial class Master_Bank : System.Web.UI.Page
{
    private BankFlow _flow;
    private BankFlow FlowObj
    {
        get { if (_flow == null) { _flow = new BankFlow(); } return _flow; }
    }

    private void ResetState(double LOID)
    {
        BankData data = FlowObj.GetData(LOID);
        if (LOID == 0)
        {
            data.CODE = FlowObj.GenNewCode();
        }
        SetData(data);
    }

    private BankData GetData()
    {
        BankData data = new BankData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtCode.Text.Trim();
        data.NAME = this.txtName.Text.Trim();
        return data;
    }

    private void SetData(BankData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE.Trim();
        this.txtName.Text = data.NAME.Trim();
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
        Response.Redirect(Constz.HomeFolder + "Master/BankSearch.aspx");
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
