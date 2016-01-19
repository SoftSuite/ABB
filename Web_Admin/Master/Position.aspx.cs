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
using ABB.Data.Admin;
using ABB.Global;
using ABB.Flow.Common;
using ABB.Flow.Admin;

public partial class Master_Position : System.Web.UI.Page
{
    private PositionFlow _flow;
    private PositionFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PositionFlow(); } return _flow; }
    }

    //private void ClearData()
    //{
    //    this.txtLOID.Text = "";
    //    this.txtCode.Text = "";
    //    this.txtName.Text = "";

    //}

    private void ResetState(double LOID)
    {
        PositionData data = FlowObj.GetData(LOID);
        SetData(data);
    }

    private PositionData GetData()
    {
        PositionData data = new PositionData();
        data.CODE = this.txtCode.Text.Trim();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.NAME = this.txtName.Text.Trim();
        return data;
    }

    private void SetData(PositionData data)
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
        Response.Redirect(Constz.HomeFolder + "Master/PositionSearch.aspx");
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
            Appz.ClientAlert(this, "�ѹ�֡���������º��������");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);

    }

}

