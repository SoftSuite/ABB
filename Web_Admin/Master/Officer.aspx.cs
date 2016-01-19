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

public partial class Master_Officer : System.Web.UI.Page
{
    private OfficerFlow _flow;
    private OfficerFlow FlowObj
    {
        get { if (_flow == null) { _flow = new OfficerFlow(); } return _flow; }
    }

    private void ResetState(double LOID)
    {
        OfficerData data = FlowObj.GetData(LOID);
        if (LOID == 0)
        {
            data.PROVINCE = 30;
            data.AMPHUR = 3001;
            data.EFDATE = DateTime.Now.Date;
        }
        SetData(data);
    }

    private OfficerData GetData()
    {
        OfficerData data = new OfficerData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.TNAME = this.txtName.Text.Trim();
        data.LASTNAME = this.txtLastname.Text.Trim();
        data.DIVISION = Convert.ToDouble(this.cmbDivision.SelectedItem.Value);
        data.USERID = this.txtUserID.Text.Trim();
        if (this.txtPassword.Text == "")
        {
            data.PASSWORD = this.txtHidPass.Text.Trim();
        }
        else
        {
            data.PASSWORD = this.txtPassword.Text.Trim();
        }
        if (this.txtPassConfirm.Text == "")
        {
            data.PASSCONFIRM = this.txtHidPass.Text.Trim();
        }
        else
        {
            data.PASSCONFIRM = this.txtPassConfirm.Text.Trim();
        }
        data.EFDATE = this.ctlEFDate.DateValue;
        data.NICKNAME = this.txtNickname.Text.Trim();
        data.BIRTHDATE = this.ctlBirthdate.DateValue;
        data.TEL = this.txtTel.Text.Trim();
        data.EMAIL = this.txtEMail.Text.Trim();
        data.ADDRESS = this.txtAddress.Text.Trim();
        data.ROAD = this.txtRoad.Text.Trim();
        data.PROVINCE = Convert.ToDouble(this.cmbProvince.SelectedItem.Value);
        data.AMPHUR = Convert.ToDouble(this.cmbAmphur.SelectedItem.Value);
        data.TAMBOL = Convert.ToDouble(this.cmbDistrict.SelectedItem.Value);
        data.ZIPCODE = this.txtZipcode.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.TITLE = Convert.ToDouble(this.cmbTitle.SelectedItem.Value);
        return data;
    }

    private void SetData(OfficerData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtName.Text = data.TNAME.Trim();
        this.txtLastname.Text = data.LASTNAME.Trim();
        this.cmbDivision.SelectedIndex = this.cmbDivision.Items.IndexOf(this.cmbDivision.Items.FindByValue(data.DIVISION.ToString()));
        this.txtUserID.Text = data.USERID.Trim();
        if (data.LOID != 0)
        {
            this.txtHidPass.Text = data.PASSWORD;
        }
        this.ctlEFDate.DateValue = data.EFDATE;
        this.txtNickname.Text = data.NICKNAME.Trim();
        this.ctlBirthdate.DateValue = data.BIRTHDATE;
        this.txtTel.Text = data.TEL.Trim();
        this.txtEMail.Text = data.EMAIL.Trim();
        this.txtAddress.Text = data.ADDRESS.Trim();
        this.txtRoad.Text = data.ROAD.Trim();
        this.cmbProvince.SelectedIndex = this.cmbProvince.Items.IndexOf(this.cmbProvince.Items.FindByValue(data.PROVINCE.ToString()));
        SetComboAmphur(cmbProvince, cmbAmphur);
        this.cmbAmphur.SelectedIndex = this.cmbAmphur.Items.IndexOf(this.cmbAmphur.Items.FindByValue(data.AMPHUR.ToString()));
        SetComboDistrict(cmbAmphur, cmbDistrict);
        this.cmbDistrict.SelectedIndex = this.cmbDistrict.Items.IndexOf(this.cmbDistrict.Items.FindByValue(data.TAMBOL.ToString()));
        this.txtZipcode.Text = data.ZIPCODE.Trim();
        this.txtRemark.Text = data.REMARK.Trim();
        this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.TITLE.ToString()));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetComboSource();
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }
    private void SetComboSource()
    {
        ComboSource.BuildCombo(cmbTitle, "TITLE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
        ComboSource.BuildCombo(cmbDivision, "DIVISION", "TNAME", "LOID", "", "", "เลือก", "0");
        ComboSource.BuildCombo(cmbProvince, "PROVINCE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "'");
        SetComboAmphur(cmbProvince, cmbAmphur);
        SetComboDistrict(cmbAmphur, cmbDistrict);
    }

    private void SetComboAmphur(DropDownList cmbProvince, DropDownList cmbAmphur)
    {
        string whr = "";
        whr = "PROVINCE = " + cmbProvince.SelectedItem.Value + " AND ACTIVE = '" + Constz.ActiveStatus.Active + "'";
        ComboSource.BuildCombo(cmbAmphur, "AMPHUR", "NAME", "LOID", "", whr);
    }

    private void SetComboDistrict(DropDownList cmbAmphur, DropDownList cmbDistrict)
    {
        string whr = "";
        whr = "AMPHUR = " + cmbAmphur.SelectedItem.Value + " AND ACTIVE = '" + Constz.ActiveStatus.Active + "'";
        ComboSource.BuildCombo(cmbDistrict, "TAMBOL", "NAME", "LOID", "", whr, "เลือก", "0");
    }

    protected void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboAmphur(cmbProvince, cmbAmphur);
        SetComboDistrict(cmbAmphur, cmbDistrict);
    }

    protected void cmbAmphur_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboDistrict(cmbAmphur, cmbDistrict);
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/OfficerSearch.aspx");
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
