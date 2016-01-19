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
using ABB.Data.Purchase;
using ABB.Flow.Purchase;
using ABB.Global;

/// <summary>
/// Create by: Ta
/// Create Date: 10 Jan 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

public partial class Master_Supplier : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            SetComboSource();

            if (Request.QueryString["SupplierLOID"] != null)
            {
                txtLOID.Text = Request.QueryString["SupplierLOID"].ToString();
                LoadData(txtLOID.Text);
            }
        }
    }

    private void SetComboSource()
    {
        ComboSource.BuildCombo(cmbCTitle, "TITLE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "���͡", "0");
        // Combo Sup
        ComboSource.BuildCombo(cmbSupProvince, "PROVINCE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "'", "���͡", "0");
        SetComboAmphur(cmbSupProvince, cmbSupAmphur);
        SetComboDistrict(cmbSupAmphur, cmbSupDistrict);
        // Combo Contact
        ComboSource.BuildCombo(cmbContactProvince, "PROVINCE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "'", "���͡", "0");
        SetComboAmphur(cmbContactProvince, cmbContactAmphur);
        SetComboDistrict(cmbContactAmphur, cmbContactDistrict);
    }

    private void SetComboAmphur(DropDownList cmbProvince, DropDownList cmbAmphur)
    {
        string whr = "";
        whr = "PROVINCE = " + cmbProvince.SelectedItem.Value + " AND ACTIVE = '" + Constz.ActiveStatus.Active + "'";
        ComboSource.BuildCombo(cmbAmphur, "AMPHUR", "NAME", "LOID", "", whr, "���͡", "0");
    }

    private void SetComboDistrict(DropDownList cmbAmphur, DropDownList cmbDistrict)
    {
        string whr = "";
        whr = "AMPHUR = " + cmbAmphur.SelectedItem.Value + " AND ACTIVE = '" + Constz.ActiveStatus.Active + "'";
        ComboSource.BuildCombo(cmbDistrict, "TAMBOL", "NAME", "LOID", "", whr, "���͡", "0");
    }

    protected void cmbSupProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboAmphur(cmbSupProvince, cmbSupAmphur);
        SetComboDistrict(cmbSupAmphur, cmbSupDistrict);
    }

    protected void cmbSupAmphur_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboDistrict(cmbSupAmphur, cmbSupDistrict);
    }

    protected void cmbContactProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboAmphur(cmbContactProvince, cmbContactAmphur);
        SetComboDistrict(cmbContactAmphur, cmbContactDistrict);
    }

    protected void cmbContactAmphur_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboDistrict(cmbContactAmphur, cmbContactDistrict);
    }

    private void LoadData(string SupLOID)
    {
        DataTable dt = SupplierFlow.GetSupplier(SupLOID);
        if (dt.Rows.Count > 0)
        {
            //-------------------- ���ͺ���ѷ/����˹��� ---------------------------------------
            txtCode.Text = dt.Rows[0]["CODE"].ToString();
            txtSupplierName.Text = dt.Rows[0]["SUPPLIERNAME"].ToString();
            txtTaxNumber.Text = dt.Rows[0]["TAXID"].ToString();
            chkActive.Checked = (dt.Rows[0]["ACTIVE"].ToString() == Constz.ActiveStatus.Active);

            //-------------------- ����������ѷ/����˹��� ---------------------------------------
            txtSupAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
            txtSupRoad.Text = dt.Rows[0]["ROAD"].ToString();
            cmbSupProvince.SelectedIndex = cmbSupProvince.Items.IndexOf(cmbSupProvince.Items.FindByValue(dt.Rows[0]["PROVINCE"].ToString()));
            SetComboAmphur(cmbSupProvince, cmbSupAmphur);
            cmbSupAmphur.SelectedIndex = cmbSupAmphur.Items.IndexOf(cmbSupAmphur.Items.FindByValue(dt.Rows[0]["AMPHUR"].ToString()));
            SetComboDistrict(cmbSupAmphur, cmbSupDistrict);
            cmbSupDistrict.SelectedIndex = cmbSupDistrict.Items.IndexOf(cmbSupDistrict.Items.FindByValue(dt.Rows[0]["TAMBOL"].ToString()));
            txtSupZipCode.Text = dt.Rows[0]["ZIPCODE"].ToString();
            txtSupTel.Text = dt.Rows[0]["TEL"].ToString();
            txtSupFax.Text = dt.Rows[0]["FAX"].ToString();
            txtSupEmail.Text = dt.Rows[0]["EMAIL"].ToString();

            //-------------------- ���ͼ��Դ��� ---------------------------------------
            cmbCTitle.SelectedIndex = cmbCTitle.Items.IndexOf(cmbCTitle.Items.FindByValue(dt.Rows[0]["CTITLE"].ToString()));
            txtContactFirstname.Text = dt.Rows[0]["CNAME"].ToString();
            txtContactLastname.Text = dt.Rows[0]["CLASTNAME"].ToString();
            txtContactTel.Text = dt.Rows[0]["CTEL"].ToString();
            txtContactMobile.Text = dt.Rows[0]["CMOBILE"].ToString();
            txtContactEmail.Text = dt.Rows[0]["CEMAIL"].ToString();
            txtContactAddress.Text = dt.Rows[0]["CADDRESS"].ToString();
            txtContactRoad.Text = dt.Rows[0]["CROAD"].ToString();
            cmbContactProvince.SelectedIndex = cmbContactProvince.Items.IndexOf(cmbContactProvince.Items.FindByValue(dt.Rows[0]["CPROVINCE"].ToString()));
            SetComboAmphur(cmbContactProvince, cmbContactAmphur);
            cmbContactAmphur.SelectedIndex = cmbContactAmphur.Items.IndexOf(cmbContactAmphur.Items.FindByValue(dt.Rows[0]["CAMPHUR"].ToString()));
            SetComboDistrict(cmbContactAmphur, cmbContactDistrict);
            cmbContactDistrict.SelectedIndex = cmbContactDistrict.Items.IndexOf(cmbContactDistrict.Items.FindByValue(dt.Rows[0]["CTAMBOL"].ToString()));
            txtContactZipCode.Text = dt.Rows[0]["CZIPCODE"].ToString();

            //-------------------- �����˵� ---------------------------------------
            txtRemark.Text = dt.Rows[0]["REMARK"].ToString();
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect("SupplierSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ClearControls();
        if (txtLOID.Text != "")
        {
            LoadData(txtLOID.Text);
        }
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (ValidateInput() == true)
        {
            UpdateData();
        }
    }

    private bool ValidateInput()
    {
        // -------------- ���ͺ���ѷ/����˹��� -------------------------------------------
        if (txtCode.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "��س��к����ʼ���˹���");
            return false;
        }
        if (txtTaxNumber.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "��س��к��Ţ��Шӵ�Ǽ�����������ҡ�");
            return false;
        }
        if (txtSupplierName.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "��س��кت��ͼ���˹���");
            return false;
        }

        // -------------- ����������ѷ/����˹��� -------------------------------------------
        if (txtSupAddress.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "��س��кط������");
            return false;
        }
        if (txtSupRoad.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "��س��кض��");
            return false;
        }
        if (cmbSupProvince.SelectedIndex == 0)
        {
            Appz.ClientAlert(Page, "��س��кبѧ��Ѵ");
            return false;
        }
        if (cmbSupAmphur.SelectedIndex == 0)
        {
            Appz.ClientAlert(Page, "��س��к������");
            return false;
        }
        if (txtSupZipCode.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "��س��к�������ɳ���");
            return false;
        }
        if (txtSupTel.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "��س��к��������Ѿ��");
            return false;
        }

        SupplierFlow supFlow = new SupplierFlow();

        if (supFlow.GetLOIDbyCODE(txtCode.Text) != 0 && supFlow.GetLOIDbyCODE(txtCode.Text) != Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text))
        {
            Appz.ClientAlert(Page, "���ʼ���˹��«��");
            return false;
        }

        if (supFlow.GetLOIDbyTAXID(txtTaxNumber.Text) !=  0 && supFlow.GetLOIDbyTAXID(txtTaxNumber.Text) != Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text))
        {
            Appz.ClientAlert(Page, "�Ţ��Шӵ�Ǽ���������ի��");
            return false;
        }

        if (supFlow.GetLOIDbySUPPLIERNAME(txtSupplierName.Text) != 0 && supFlow.GetLOIDbySUPPLIERNAME(txtSupplierName.Text) != Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text))
        {
            Appz.ClientAlert(Page, "���ͼ���˹��«��");
            return false;
        }

        return true;

    }

    private void UpdateData()
    {
        bool ret = true;
        SupplierFlow supFlow = new SupplierFlow();
        SupplierData supData = new SupplierData();

        GetData(supData);

        ret = supFlow.UpdateData(Authz.CurrentUserInfo.UserID, supData);

        if (ret == false)
            Appz.ClientAlert(Page, supFlow.ErrorMessage);
        else
        {
            Appz.ClientAlert(Page, "�ӡ�èѴ�红����ż���˹������º����");
            txtLOID.Text = supFlow.GetLOIDbyCODE(supData.CODE).ToString();
        }
    }

    private void GetData(SupplierData supData)
    {
        //-------------------- ���ͺ���ѷ/����˹��� ---------------------------------------
        supData.LOID = txtLOID.Text;
        supData.CODE = txtCode.Text.Trim();
        supData.TAXID = txtTaxNumber.Text.Trim();
        supData.SUPPLIERNAME = txtSupplierName.Text.Trim();
        supData.ACTIVE = (chkActive.Checked ? Constz.ActiveStatus.Active : Constz.ActiveStatus.InActive);

        //-------------------- ����������ѷ/����˹��� ---------------------------------------
        supData.ADDRESS = txtSupAddress.Text.Trim();
        supData.ROAD = txtSupRoad.Text.Trim();
        supData.PROVINCE = Convert.ToDouble(cmbSupProvince.SelectedItem.Value);
        supData.AMPHUR = Convert.ToDouble(cmbSupAmphur.SelectedItem.Value);
        supData.TAMBOL = Convert.ToDouble(cmbSupDistrict.SelectedItem.Value);
        supData.ZIPCODE = txtSupZipCode.Text.Trim();
        supData.TEL = txtSupTel.Text.Trim();
        supData.FAX = txtSupFax.Text.Trim();
        supData.EMAIL = txtSupEmail.Text.Trim();

        //-------------------- ���ͼ��Դ��� ---------------------------------------
        supData.CTITLE = Convert.ToDouble(cmbCTitle.SelectedItem.Value);
        supData.CNAME = txtContactFirstname.Text.Trim();
        supData.CLASTNAME = txtContactLastname.Text.Trim();
        supData.CTEL = txtContactTel.Text.Trim();
        supData.CMOBILE = txtContactMobile.Text.Trim();
        supData.CEMAIL = txtContactEmail.Text.Trim();
        supData.CADDRESS = txtContactAddress.Text.Trim();
        supData.CROAD = txtContactRoad.Text.Trim();
        supData.CPROVINCE = Convert.ToDouble(cmbContactProvince.SelectedItem.Value);
        supData.CAMPHUR = Convert.ToDouble(cmbContactAmphur.SelectedItem.Value);
        supData.CTAMBOL = Convert.ToDouble(cmbContactDistrict.SelectedItem.Value);
        supData.CZIPCODE = txtContactZipCode.Text.Trim();

        //-------------------- �����˵� ---------------------------------------
        supData.REMARK = txtRemark.Text.Trim();
    }

    private void ClearControls()
    {
        //-------------------- ���ͺ���ѷ/����˹��� ---------------------------------------
        txtCode.Text = "";
        txtTaxNumber.Text = "";
        txtSupplierName.Text = "";
        chkActive.Checked = false;

        //-------------------- ����������ѷ/���Ѵ��˹��� ---------------------------------------
        txtSupAddress.Text = "";
        txtSupRoad.Text = "";
        cmbSupProvince.SelectedIndex = -1;
        SetComboAmphur(cmbSupProvince, cmbSupAmphur);
        SetComboDistrict(cmbSupAmphur, cmbSupDistrict);
        txtSupZipCode.Text = "";
        txtSupTel.Text = "";
        txtSupFax.Text = "";
        txtSupEmail.Text = "";

        //-------------------- ���ͼ��Դ��� ---------------------------------------
        cmbCTitle.SelectedIndex = -1;
        txtContactFirstname.Text = "";
        txtContactLastname.Text = "";
        txtContactTel.Text = "";
        txtContactMobile.Text = "";
        txtContactEmail.Text = "";
        txtContactAddress.Text = "";
        txtContactRoad.Text = "";
        cmbContactProvince.SelectedIndex = -1;
        SetComboAmphur(cmbContactProvince, cmbContactAmphur);
        SetComboDistrict(cmbContactAmphur, cmbContactDistrict);
        txtContactZipCode.Text = "";

        //-------------------- �����˵� ---------------------------------------
        txtRemark.Text = "";
    }

    protected void btnContactAddress_Click(object sender, EventArgs e)
    {
        txtContactTel.Text = txtSupTel.Text.Trim();
        txtContactEmail.Text = txtSupEmail.Text.Trim();
        txtContactAddress.Text = txtSupAddress.Text.Trim();
        txtContactRoad.Text = txtSupRoad.Text.Trim();
        cmbContactProvince.SelectedIndex = cmbContactProvince.Items.IndexOf(cmbContactProvince.Items.FindByValue(cmbSupProvince.SelectedItem.Value));
        SetComboAmphur(cmbContactProvince, cmbContactAmphur);
        cmbContactAmphur.SelectedIndex = cmbContactAmphur.Items.IndexOf(cmbContactAmphur.Items.FindByValue(cmbSupAmphur.SelectedItem.Value));
        SetComboDistrict(cmbContactAmphur, cmbContactDistrict);
        cmbContactDistrict.SelectedIndex = cmbContactDistrict.Items.IndexOf(cmbContactDistrict.Items.FindByValue(cmbSupDistrict.SelectedItem.Value));
        txtContactZipCode.Text = txtSupZipCode.Text.Trim();
    }
}
