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
using ABB.Data.Sales;
using ABB.Flow.Common;
using ABB.Flow.Sales;
using ABB.Global;

public partial class Master_Unit : System.Web.UI.Page
{
    private UnitFlow _flow;
    private UnitFlow FlowObj
    {
        get { if (_flow == null) { _flow = new UnitFlow(); } return _flow; }
    }

    private void ClearData()
    {
        this.txtCode.Text = "";
        this.txtName.Text = "";
        this.txtEName.Text = "";
        this.rbtIsType.SelectedIndex = 0;
        this.chkActive.Checked = true;
    }

    private void ResetState(double LOID)
    {
        if (LOID != 0)
        {
            UnitSearchData data = FlowObj.GetData(LOID);
            SetData(data);
        }
        else
        {
            this.rbtIsType.SelectedIndex = 0;
            this.chkActive.Checked = true;
        }
    }

    private UnitSearchData GetData()
    {
        UnitSearchData data = new UnitSearchData();
        data.ACTIVE = (this.chkActive.Checked ? Constz.ActiveStatus.Active : Constz.ActiveStatus.InActive);
        data.CODE = txtCode.Text.Trim();
        data.LOID = this.txtLOID.Text == "" ? 0 : Convert.ToDouble(this.txtLOID.Text);
        data.NAME = this.txtName.Text.Trim();
        data.ENAME = this.txtEName.Text.Trim();
        data.TYPE = this.rbtIsType.SelectedItem.Value.Trim();
        return data;
    }

    private void SetData(UnitSearchData data)
    {
        if (data.LOID == 0)
        {
            this.rbtIsType.SelectedIndex = 0;
            this.chkActive.Checked = true;
        }
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE;
        this.txtName.Text = data.NAME.Trim();
        this.txtEName.Text = data.ENAME.Trim();
        this.rbtIsType.SelectedIndex = this.rbtIsType.Items.IndexOf(this.rbtIsType.Items.FindByValue(data.TYPE));
        this.chkActive.Checked = (data.ACTIVE == Constz.ActiveStatus.Active);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.rbtIsType.Items.Clear();
            this.rbtIsType.Items.Add(new ListItem(Constz.UnitType.ALL.Name, Constz.UnitType.ALL.Code));
            this.rbtIsType.Items.Add(new ListItem(Constz.UnitType.FG.Name, Constz.UnitType.FG.Code));
            this.rbtIsType.Items.Add(new ListItem(Constz.UnitType.WH.Name, Constz.UnitType.WH.Code));

            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            //this.rbtIsType.SelectedIndex = 0;
            //this.chkActive.Checked = true;
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/UnitSearch.aspx");
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
