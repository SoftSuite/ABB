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
public partial class Master_Division : System.Web.UI.Page
{
    private DivisionFlow _flow;
    private DivisionFlow FlowObj
    {
        get { if (_flow == null) { _flow = new DivisionFlow(); } return _flow; }
    }

    //private void ClearData()
    //{
    //    this.txtLOID.Text = "";
    //    this.txtCode.Text = "";
    //    this.txtName.Text = "";

    //}

    private void ResetState(double LOID)
    {
        DivisionData data = FlowObj.GetData(LOID);
        SetData(data);
    }

    private DivisionData GetData()
    {
        DivisionData data = new DivisionData();
        data.CODE = this.txtCode.Text.Trim();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.TNAME = this.txtName.Text.Trim();
        data.EFDATE = this.ctlEFDate.DateValue;
        data.ABBNAME = this.txtAbbName.Text.Trim();
        data.DEPARTMENT = Convert.ToDouble(this.cmbDepartment.SelectedItem.Value);
        return data;
    }

    private void SetData(DivisionData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE.Trim();
        this.txtName.Text = data.TNAME.Trim();
        if (data.EFDATE.Year == 1)
            this.ctlEFDate.DateValue = DateTime.Now.Date;
        else
            this.ctlEFDate.DateValue = data.EFDATE;
        this.txtAbbName.Text = data.ABBNAME;
        this.cmbDepartment.SelectedIndex = this.cmbDepartment.Items.IndexOf(this.cmbDepartment.Items.FindByValue(data.DEPARTMENT.ToString()));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbDepartment, "DEPARTMENT", "TNAME", "LOID", "TNAME", "");
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/DivisionSearch.aspx");
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


