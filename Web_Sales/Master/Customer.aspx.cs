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
using ABB.Flow.Sales;
using ABB.Global;

/// <summary>
/// Create by: Pom
/// Create Date: 13 Dec 2007
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>


public partial class Master_Customer : System.Web.UI.Page
{
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            return new SessionPageStatePersister(this);
        }
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ControlUtil.SetIntTextBox(txtCreditPeriod);
        ControlUtil.SetDblTextBox(txtCreditAmount);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            SetComboSource();

            if (Request.QueryString["LOID"] != null)
            {
                LoadData(Request.QueryString["LOID"].ToString());
                CheckRadio();
            }
            else
            {
                radPersonal.Checked = true;
                CheckRadio();
            }              
        }
    }

    private void SetComboSource()
    {
        this.radOrganize.Text = Constz.CustomerType.Government.Name;
        this.radPersonal.Text = Constz.CustomerType.Personal.Name;
        this.radPrivate.Text = Constz.CustomerType.Company.Name;

        ComboSource.BuildPaymentTypeCombo(this.cmbPaymentCondition, "���͡", "0");

        ComboSource.BuildCombo(cmbTitle, "TITLE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active +  "' ", "���͡", "0");
        ComboSource.BuildCombo(cmbCTitle, "TITLE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "'", "���͡", "0");
        ComboSource.BuildCombo(cmbMemberType, "MEMBERTYPE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "'", "���͡", "0");
        // Combo Cus
        ComboSource.BuildCombo(cmbCusProvince, "PROVINCE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "'", "���͡", "0");
        SetComboAmphur(cmbCusProvince, cmbCusAmphur);
        SetComboDistrict(cmbCusAmphur, cmbCusDistrict);
        // Combo Contact
        ComboSource.BuildCombo(cmbContactProvince, "PROVINCE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "'", "���͡", "0");
        SetComboAmphur(cmbContactProvince, cmbContactAmphur);
        SetComboDistrict(cmbContactAmphur, cmbContactDistrict);
        // Combo Delivery
        ComboSource.BuildCombo(cmbDeliveryProvince, "PROVINCE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "'", "���͡", "0");
        SetComboAmphur(cmbDeliveryProvince, cmbDeliveryAmphur);
        SetComboDistrict(cmbDeliveryAmphur, cmbDeliveryDistrict);
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

    protected void cmbCusProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboAmphur(cmbCusProvince, cmbCusAmphur);
        SetComboDistrict(cmbCusAmphur, cmbCusDistrict); 
    }

    protected void cmbCusAmphur_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboDistrict(cmbCusAmphur, cmbCusDistrict);
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

    protected void cmbDeliveryProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboAmphur(cmbDeliveryProvince, cmbDeliveryAmphur);
        SetComboDistrict(cmbDeliveryAmphur, cmbDeliveryDistrict);
    }

    protected void cmbDeliveryAmphur_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboDistrict(cmbDeliveryAmphur, cmbDeliveryDistrict);
    }

    private void LoadData(string LOID)
    {
        DataTable dt = CustomerFlow.GetCustomer(LOID);
        if (dt.Rows.Count > 0)
        {
            txtLOID.Text = dt.Rows[0]["LOID"].ToString();
            //-------------------- ���ͺ���ѷ/�١��� ---------------------------------------
            txtCusCode.Text = dt.Rows[0]["CODE"].ToString();

            if (dt.Rows[0]["CUSTOMERTYPE"].ToString() == Constz.CustomerType.Personal.Code)
            {
                radPersonal.Checked = true;
                txtPersonID.Text = dt.Rows[0]["IDENTITY"].ToString();
                cmbTitle.SelectedIndex = cmbTitle.Items.IndexOf(cmbTitle.Items.FindByValue(dt.Rows[0]["TITLE"].ToString()));
                txtFirstname.Text = dt.Rows[0]["NAME"].ToString();
                txtLastname.Text = dt.Rows[0]["LASTNAME"].ToString();
            }
            else if (dt.Rows[0]["CUSTOMERTYPE"].ToString() == Constz.CustomerType.Company.Code)
            {
                radPrivate.Checked = true;
                txtTaxNumber.Text = dt.Rows[0]["IDENTITY"].ToString();
                txtPrivateName.Text = dt.Rows[0]["NAME"].ToString();
            }
            else if (dt.Rows[0]["CUSTOMERTYPE"].ToString() == Constz.CustomerType.Government.Code)
            {
                radOrganize.Checked = true;
                txtOrganizeName.Text = dt.Rows[0]["NAME"].ToString();
            }

            cmbMemberType.SelectedIndex = cmbMemberType.Items.IndexOf(cmbMemberType.Items.FindByValue(dt.Rows[0]["MEMBERTYPE"].ToString()));
            dtpEFDate.DateValue = Convert.ToDateTime(dt.Rows[0]["EFDATE"]);
            dtpEPDate.DateValue = Convert.ToDateTime(dt.Rows[0]["EPDATE"]);

            //-------------------- ���͹䢡�ê����Թ ---------------------------------------
            cmbPaymentCondition.SelectedIndex = cmbPaymentCondition.Items.IndexOf(cmbPaymentCondition.Items.FindByValue(dt.Rows[0]["PAYMENT"].ToString()));
            txtCreditPeriod.Text = dt.Rows[0]["CREDITDAY"].ToString();
            txtCreditAmount.Text = dt.Rows[0]["CREDITAMOUNT"].ToString();

            //-------------------- ����������ѷ/�١���/��Ҫԡ ---------------------------------------
            txtCusAddress.Text = dt.Rows[0]["BILLADDRESS"].ToString();
            txtCusRoad.Text = dt.Rows[0]["BILLROAD"].ToString();
            cmbCusProvince.SelectedIndex = cmbCusProvince.Items.IndexOf(cmbCusProvince.Items.FindByValue(dt.Rows[0]["BILLPROVINCE"].ToString()));
            SetComboAmphur(cmbCusProvince, cmbCusAmphur);
            cmbCusAmphur.SelectedIndex = cmbCusAmphur.Items.IndexOf(cmbCusAmphur.Items.FindByValue(dt.Rows[0]["BILLAMPHUR"].ToString()));
            SetComboDistrict(cmbCusAmphur, cmbCusDistrict);
            cmbCusDistrict.SelectedIndex = cmbCusDistrict.Items.IndexOf(cmbCusDistrict.Items.FindByValue(dt.Rows[0]["BILLTAMBOL"].ToString()));
            txtCusZipCode.Text = dt.Rows[0]["BILLZIPCODE"].ToString();
            txtCusTel.Text = dt.Rows[0]["BILLTEL"].ToString();
            txtCusFax.Text = dt.Rows[0]["BILLFAX"].ToString();
            txtCusEmail.Text = dt.Rows[0]["BILLEMAIL"].ToString();

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

            //-------------------- ʶҹ������Թ��� ---------------------------------------
            txtDeliveryPlace.Text = dt.Rows[0]["SENDPLACE"].ToString();
            cmbDeliveryBy.SelectedIndex = cmbDeliveryBy.Items.IndexOf(cmbDeliveryBy.Items.FindByValue(dt.Rows[0]["DELIVERTYPE"].ToString()));
            txtDeliveryAddress.Text = dt.Rows[0]["SENDADDRESS"].ToString();
            txtDeliveryRoad.Text = dt.Rows[0]["SENDROAD"].ToString();
            cmbDeliveryProvince.SelectedIndex = cmbDeliveryProvince.Items.IndexOf(cmbDeliveryProvince.Items.FindByValue(dt.Rows[0]["SENDPROVINCE"].ToString()));
            SetComboAmphur(cmbDeliveryProvince, cmbDeliveryAmphur);            
            cmbDeliveryAmphur.SelectedIndex = cmbDeliveryAmphur.Items.IndexOf(cmbDeliveryAmphur.Items.FindByValue(dt.Rows[0]["SENDAMPHUR"].ToString()));
            SetComboDistrict(cmbDeliveryAmphur, cmbDeliveryDistrict);
            cmbDeliveryDistrict.SelectedIndex = cmbDeliveryDistrict.Items.IndexOf(cmbDeliveryDistrict.Items.FindByValue(dt.Rows[0]["SENDTAMBOL"].ToString()));
            txtDeliveryZipCode.Text = dt.Rows[0]["SENDZIPCODE"].ToString();
            txtDeliveryTel.Text = dt.Rows[0]["SENDTEL"].ToString();
            txtDeliveryFax.Text = dt.Rows[0]["SENDFAX"].ToString();
            txtDeliveryEmail.Text = dt.Rows[0]["SENDEMAIL"].ToString();

            //-------------------- �����˵� ---------------------------------------
            txtRemark.Text = dt.Rows[0]["REMARK"].ToString();            
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect("CustomerSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ClearControls();
        if (txtLOID.Text != "")
        {
            LoadData(txtLOID.Text);
            CheckRadio();
        }
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (ValidateInput() == true)
        {
            if (txtLOID.Text.Trim() == "")
            {
                InsertData();
                LoadData(txtLOID.Text);
            }
            else
                UpdateData(); 
           
        }        
    }

    private bool ValidateInput()
    {
        if (txtCusCode.Text == "")
        {
            Appz.ClientAlert(Page, "��س��к������١���");
            txtCusCode.Focus();
            return false;
        }
        // -------------- �ؤ�� -------------------------------------------
        if (radPersonal.Checked == true)
        {
            if (txtPersonID.Text.Trim() == "")
            {
                Appz.ClientAlert(Page, "��س��к����ʻ�Шӵ�ǻ�ЪҪ�");
                txtPersonID.Focus();
                return false;
            }
            if (cmbTitle.SelectedIndex == 0)
            {
                Appz.ClientAlert(Page, "��س��кؤӹ�˹�Ҫ���");
                return false;
            }
            if (txtFirstname.Text.Trim() == "")
            {
                Appz.ClientAlert(Page, "��س��кت����١���");
                return false;
            }
            if (txtLastname.Text.Trim() == "")
            {
                Appz.ClientAlert(Page, "��س��кع��ʡ���١���");
                return false;
            }

        }

        // -------------- �͡�� -------------------------------------------
        if (radPrivate.Checked == true)
        {
            if (txtTaxNumber.Text.Trim() == "")
            {
                Appz.ClientAlert(Page, "��س��к��Ţ��Шӵ�Ǽ����������");
                return false;
            }
            if (txtPrivateName.Text.Trim() == "")
            {
                Appz.ClientAlert(Page, "��س��кت��ͺ���ѷ");
                return false;
            }
        }

        // -------------- ͧ���/˹��§ҹ�Ѱ -------------------------------------------
        if (radOrganize.Checked == true)
        {
            if (txtOrganizeName.Text.Trim() == "")
            {
                Appz.ClientAlert(Page, "��س��кت���ͧ���/˹��§ҹ�Ѱ");
                return false;
            }
        }

        if (cmbMemberType.SelectedIndex == 0)
        {
            Appz.ClientAlert(Page, "��س��кػ������١���");
            return false;
        }
        if (dtpEFDate.DateValue.Year == 1)
        {
            Appz.ClientAlert(Page, "��س��к��ѹ�������Ҫԡ");
            return false;
        }
        if (dtpEPDate.DateValue.Year == 1)
        {
            Appz.ClientAlert(Page, "��س��к��ѹ����������");
            return false;
        }
        if (dtpEPDate.DateValue.CompareTo(dtpEFDate.DateValue) <= 0)
        {
            Appz.ClientAlert(Page, "�ѹ������١��ͧ");
            return false;
        }
        if (cmbPaymentCondition.SelectedIndex == 0)
        {
            Appz.ClientAlert(Page, "��س��к����͹䢡�ê����Թ");
            return false;
        }
        if (txtCusZipCode.Text.Length > 5 || txtDeliveryZipCode.Text.Length > 5 || txtContactZipCode.Text.Length > 5)
        {
            Appz.ClientAlert(Page, "������ɳ������١��ͧ");
            return false;
        }

        CustomerFlow cusFlow = new CustomerFlow();

        if (cusFlow.GetLOIDbyIDENTITY(txtPersonID.Text) != 0 && cusFlow.GetLOIDbyIDENTITY(txtPersonID.Text) != Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text))
        {
            Appz.ClientAlert(Page, "�Ţ�ѵû�ЪҪ����");
            return false;
        }

        double LOID = cusFlow.GetLOID(txtCusCode.Text);

        if (LOID != 0 && LOID != Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text))
        {
            Appz.ClientAlert(Page, "�����١��ҫ��");
            return false;
        }

        LOID = cusFlow.GetLOIDbyIDENTITY(txtTaxNumber.Text);

        if (LOID != 0 && LOID != Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text))
        {
            Appz.ClientAlert(Page, "�Ţ��Шӵ�Ǽ���������ի��");
            return false;
        }

        return true;

    }

    private void InsertData()
    {
        bool ret = true;
        CustomerFlow cusFlow = new CustomerFlow();
        CustomerData cusData = new CustomerData();

        //cusData.CODE = CustomerFlow.GenerateCusCode();
        cusData.CODE = txtCusCode.Text.Trim();
        GetData(cusData);

        ret = cusFlow.InsertData(Authz.CurrentUserInfo.UserID, cusData);

        if (ret == false)
            Appz.ClientAlert(Page, cusFlow.ErrorMessage);
        else
        {
            ClearControls();
            double LOID = cusFlow.GetLOID(cusData.CODE);
            LoadData(LOID.ToString());
            Appz.ClientAlert(Page, "�ӡ�èѴ�红������١������º����");
        }
    }

    private void UpdateData()
    {
        bool ret = true;
        CustomerFlow cusFlow = new CustomerFlow();
        CustomerData cusData = new CustomerData();

        cusData.CODE = txtCusCode.Text.Trim();
        GetData(cusData);

        ret = cusFlow.UpdateData(Authz.CurrentUserInfo.UserID, cusData);

        if (ret == false)
            Appz.ClientAlert(Page, cusFlow.ErrorMessage);
        else
        {
            ClearControls();
            LoadData(cusData.LOID.ToString());
            Appz.ClientAlert(Page, "�ӡ����䢢������١������º����");
        }
    }

    private void GetData(CustomerData cusData)
    {
        cusData.LOID = Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text);

        //-------------------- ���ͺ���ѷ/�١��� ---------------------------------------

        if (radPersonal.Checked == true)
        {
            cusData.CUSTOMERTYPE = Constz.CustomerType.Personal.Code;
            cusData.IDENTITY = txtPersonID.Text.Trim();
            cusData.TITLE = Convert.ToDouble(cmbTitle.SelectedItem.Value);
            cusData.NAME = txtFirstname.Text.Trim();
            cusData.LASTNAME = txtLastname.Text.Trim();
        } 
        else if (radPrivate.Checked == true)
        {
            cusData.CUSTOMERTYPE = Constz.CustomerType.Company.Code;
            cusData.IDENTITY = txtTaxNumber.Text.Trim();
            cusData.NAME = txtPrivateName.Text.Trim();
        }
        else if (radOrganize.Checked == true)
        {
            cusData.CUSTOMERTYPE = Constz.CustomerType.Government.Code;
            cusData.NAME = txtOrganizeName.Text.Trim();
        }

        cusData.MEMBERTYPE = Convert.ToDouble(cmbMemberType.SelectedItem.Value);
        cusData.EFDATE = dtpEFDate.DateValue;
        cusData.EPDATE = dtpEPDate.DateValue;

        //-------------------- ���͹䢡�ê����Թ ---------------------------------------
        cusData.PAYMENT = cmbPaymentCondition.SelectedItem.Value;
        if (txtCreditPeriod.Text.Trim() != "")
            cusData.CREDITDAY = Convert.ToDouble(txtCreditPeriod.Text.Trim());
        if (txtCreditAmount.Text.Trim() != "")
            cusData.CREDITAMOUNT = Convert.ToDouble(txtCreditAmount.Text.Trim());

        //-------------------- ����������ѷ/�١���/��Ҫԡ ---------------------------------------
        cusData.BILLADDRESS = txtCusAddress.Text.Trim();
        cusData.BILLROAD = txtCusRoad.Text.Trim();
        cusData.BILLPROVINCE = Convert.ToDouble(cmbCusProvince.SelectedItem.Value);
        cusData.BILLAMPHUR = Convert.ToDouble(cmbCusAmphur.SelectedItem.Value);
        cusData.BILLTAMBOL = Convert.ToDouble(cmbCusDistrict.SelectedItem.Value);
        cusData.BILLZIPCODE = txtCusZipCode.Text.Trim();
        cusData.BILLTEL = txtCusTel.Text.Trim();
        cusData.BILLFAX = txtCusFax.Text.Trim();
        cusData.BILLEMAIL = txtCusEmail.Text.Trim();

        //-------------------- ���ͼ��Դ��� ---------------------------------------
        cusData.CTITLE = Convert.ToDouble(cmbCTitle.SelectedItem.Value);
        cusData.CNAME = txtContactFirstname.Text.Trim();
        cusData.CLASTNAME = txtContactLastname.Text.Trim();
        cusData.CTEL = txtContactTel.Text.Trim();
        cusData.CMOBILE = txtContactMobile.Text.Trim();
        cusData.CEMAIL = txtContactEmail.Text.Trim();
        cusData.CADDRESS = txtContactAddress.Text.Trim();
        cusData.CROAD = txtContactRoad.Text.Trim();
        cusData.CPROVINCE = Convert.ToDouble(cmbContactProvince.SelectedItem.Value);
        cusData.CAMPHUR = Convert.ToDouble(cmbContactAmphur.SelectedItem.Value);
        cusData.CTAMBOL = Convert.ToDouble(cmbContactDistrict.SelectedItem.Value);
        cusData.CZIPCODE = txtContactZipCode.Text.Trim();

        //-------------------- ʶҹ������Թ��� ---------------------------------------
        cusData.SENDPLACE = txtDeliveryPlace.Text.Trim();
        cusData.DELIVERTYPE = cmbDeliveryBy.SelectedItem.Value;
        cusData.SENDADDRESS = txtDeliveryAddress.Text.Trim();
        cusData.SENDROAD = txtDeliveryRoad.Text.Trim();
        cusData.SENDPROVINCE = Convert.ToDouble(cmbDeliveryProvince.SelectedItem.Value);
        cusData.SENDAMPHUR = Convert.ToDouble(cmbDeliveryAmphur.SelectedItem.Value);
        cusData.SENDTAMBOL = Convert.ToDouble(cmbDeliveryDistrict.SelectedItem.Value);
        cusData.SENDZIPCODE = txtDeliveryZipCode.Text.Trim();
        cusData.SENDTEL = txtDeliveryTel.Text.Trim();
        cusData.SENDFAX = txtDeliveryFax.Text.Trim();
        cusData.SENDEMAIL = txtDeliveryEmail.Text.Trim();

        //-------------------- �����˵� ---------------------------------------
        cusData.REMARK = txtRemark.Text.Trim();
    }

    protected void btnContactAddress_Click(object sender, EventArgs e)
    {
        txtContactTel.Text = txtCusTel.Text.Trim();
        txtContactEmail.Text = txtCusEmail.Text.Trim();
        txtContactAddress.Text = txtCusAddress.Text.Trim();
        txtContactRoad.Text = txtCusRoad.Text.Trim();
        cmbContactProvince.SelectedIndex = cmbContactProvince.Items.IndexOf(cmbContactProvince.Items.FindByValue(cmbCusProvince.SelectedItem.Value));
        SetComboAmphur(cmbContactProvince, cmbContactAmphur);      
        cmbContactAmphur.SelectedIndex = cmbContactAmphur.Items.IndexOf(cmbContactAmphur.Items.FindByValue(cmbCusAmphur.SelectedItem.Value));
        SetComboDistrict(cmbContactAmphur, cmbContactDistrict);
        cmbContactDistrict.SelectedIndex = cmbContactDistrict.Items.IndexOf(cmbContactDistrict.Items.FindByValue(cmbCusDistrict.SelectedItem.Value));
        txtContactZipCode.Text = txtCusZipCode.Text.Trim();
    }

    protected void btnDeliveryAddress_Click(object sender, EventArgs e)
    {
        txtDeliveryAddress.Text = txtCusAddress.Text.Trim();
        txtDeliveryRoad.Text = txtCusRoad.Text.Trim();
        cmbDeliveryProvince.SelectedIndex = cmbDeliveryProvince.Items.IndexOf(cmbDeliveryProvince.Items.FindByValue(cmbCusProvince.SelectedItem.Value));
        SetComboAmphur(cmbDeliveryProvince, cmbDeliveryAmphur);
        cmbDeliveryAmphur.SelectedIndex = cmbDeliveryAmphur.Items.IndexOf(cmbDeliveryAmphur.Items.FindByValue(cmbCusAmphur.SelectedItem.Value));
        SetComboDistrict(cmbDeliveryAmphur, cmbDeliveryDistrict);
        cmbDeliveryDistrict.SelectedIndex = cmbDeliveryDistrict.Items.IndexOf(cmbDeliveryDistrict.Items.FindByValue(cmbCusDistrict.SelectedItem.Value));
        txtDeliveryZipCode.Text = txtCusZipCode.Text.Trim();
        txtDeliveryTel.Text = txtCusTel.Text.Trim();
        txtDeliveryFax.Text = txtCusFax.Text.Trim();
        txtDeliveryEmail.Text = txtCusEmail.Text.Trim();
    }

    private void ClearControls()
    {
        //-------------------- ���ͺ���ѷ/�١��� ---------------------------------------

        txtCusCode.Text = "";
        txtPersonID.Text = "";
        cmbTitle.SelectedIndex = -1;
        txtFirstname.Text = "";
        txtLastname.Text = "";
        txtTaxNumber.Text = "";
        txtPrivateName.Text = "";
        txtOrganizeName.Text = "";
        cmbMemberType.SelectedIndex = -1;
        dtpEFDate.DateValue = new DateTime();
        dtpEPDate.DateValue = new DateTime();

        //-------------------- ���͹䢡�ê����Թ ---------------------------------------
        cmbPaymentCondition.SelectedIndex = -1;
        txtCreditPeriod.Text = "";
        txtCreditAmount.Text = "";

        //-------------------- ����������ѷ/�١���/��Ҫԡ ---------------------------------------
        txtCusAddress.Text = "";
        txtCusRoad.Text = "";
        cmbCusProvince.SelectedIndex = -1;
        SetComboAmphur(cmbCusProvince, cmbCusAmphur);
        SetComboDistrict(cmbCusAmphur, cmbCusDistrict);
        txtCusZipCode.Text = "";
        txtCusTel.Text = "";
        txtCusFax.Text = "";
        txtCusEmail.Text = "";

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

        //-------------------- ʶҹ������Թ��� ---------------------------------------
        txtDeliveryPlace.Text = "";
        cmbDeliveryBy.SelectedIndex = -1;
        txtDeliveryAddress.Text = "";
        txtDeliveryRoad.Text = "";
        cmbDeliveryProvince.SelectedIndex = -1;
        SetComboAmphur(cmbDeliveryProvince, cmbDeliveryAmphur);
        SetComboDistrict(cmbDeliveryAmphur, cmbDeliveryDistrict);
        txtDeliveryZipCode.Text = "";
        txtDeliveryTel.Text = "";
        txtDeliveryFax.Text = "";
        txtDeliveryEmail.Text = "";

        //-------------------- �����˵� ---------------------------------------
        txtRemark.Text = "";
    }

    private void CheckRadio()
    {
        if (radPersonal.Checked == true)
        {
            trPSName.Visible = true;
            trPSNumber.Visible = true;
            trPrivateTaxNumber.Visible = false;
            trPrivateName.Visible = false;
            trORGName.Visible = false;
        }
        if (radPrivate.Checked == true)
        {
            trPSName.Visible = false;
            trPSNumber.Visible = false;
            trPrivateTaxNumber.Visible = true;
            trPrivateName.Visible = true;
            trORGName.Visible = false;
        }
        if (radOrganize.Checked == true)
        {
            trPSName.Visible = false;
            trPSNumber.Visible = false;
            trPrivateTaxNumber.Visible = false;
            trPrivateName.Visible = false;
            trORGName.Visible = true;
        }

    }
    protected void radPersonal_CheckedChanged(object sender, EventArgs e)
    {
        CheckRadio();
    }
    protected void radPrivate_CheckedChanged(object sender, EventArgs e)
    {
        CheckRadio();
    }
    protected void radOrganize_CheckedChanged(object sender, EventArgs e)
    {
        CheckRadio();
    }
    
    
}
