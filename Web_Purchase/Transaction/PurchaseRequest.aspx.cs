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
using ABB.Global;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Purchase;
using ABB.Flow;
using ABB.Flow.Purchase;

public partial class Transaction_PurchaseRequest : System.Web.UI.Page
{
    private PurchaseRequestFlow _flow;
    private PRItem item;

    public PurchaseRequestFlow FlowObj
    {
        get { if (_flow == null) _flow = new PurchaseRequestFlow(); return _flow; }
    }

    public PRItem ItemObj
    {
        get { if (item == null) item = new PRItem(); return item; }
    }

    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        if (grvItem.Rows.Count > 0)
        {
            this.grvItem.ShowFooter = (status == Constz.Requisition.Status.Waiting.Code || (status == Constz.Requisition.Status.SP.Code && Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID));
            this.grvItem.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code || (status == Constz.Requisition.Status.SP.Code && Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID));
            this.grvItem.Visible = true;
            this.grvItemNew.Visible = false;
        }
        else
        {
            this.grvItem.Visible = !(status == Constz.Requisition.Status.Waiting.Code || (status == Constz.Requisition.Status.SP.Code && Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID));
            this.grvItemNew.Visible = (status == Constz.Requisition.Status.Waiting.Code || (status == Constz.Requisition.Status.SP.Code && Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID));
        }
    }

    private void SetOfficerData(double officer, bool isSearch)
    {
        OfficerData data = FlowObj.GetOfficerData(officer);
        this.txtRequestBy.Text = data.TNAME + " " + data.LASTNAME;
        this.txtDivision.Text = data.DIVISION.ToString();
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        PurchaseRequestData data = FlowObj.GetData(loid);
        if (loid == 0)
        {
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.CODE = "";
            data.REQUSETDATE = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.REQUESTBY = Authz.CurrentUserInfo.OfficerID;
            data.DIVISION = Authz.CurrentUserInfo.DivisionID;
            this.cmbPurchaseType.Enabled = true;
        }
        SetData(data);
    }

    private void SetData(PurchaseRequestData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtRequestByID.Text = data.REQUESTBY.ToString();
        this.txtDivision.Text = data.DIVISION.ToString();

        this.cmbPurchaseType.SelectedIndex = this.cmbPurchaseType.Items.IndexOf(this.cmbPurchaseType.Items.FindByValue(data.PURCHASETYPE.ToString()));
        this.txtRequirement.Text = data.REQUIREMENT;
        this.txtReason.Text = data.REASON;
        this.txtFromCompany.Text = data.FROMCOMPANY;
        this.txtPDRequestCode.Text = data.CODE;
        this.ctlRequestDate.DateValue = data.REQUSETDATE;

        SetOfficerData(data.REQUESTBY, false);
        this.txtStatusName.Text = Appz.GetStatusName(data.STATUS);
        
        this.txtRemark.Text = data.REMARK;
        
        SetGrvItem(data.STATUS);
        this.btnVoid.Visible = (data.STATUS == Constz.Requisition.Status.SP.Code && (Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID || Authz.CurrentUserInfo.DivisionID == Constz.AdminDepartment.LOID));
        this.btnCancelPR.Visible = (data.STATUS == Constz.Requisition.Status.SP.Code && (Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID || Authz.CurrentUserInfo.DivisionID == Constz.AdminDepartment.LOID));

        if (data.STATUS == Constz.Requisition.Status.SP.Code)
        {
            if (Authz.CurrentUserInfo.DivisionID != Constz.PurchaseDepartment.LOID)
            {
                if (Authz.CurrentUserInfo.DivisionID != Constz.AdminDepartment.LOID)
                {
                    this.ctlToolbar.BtnSaveShow = false;
                    this.ctlToolbar.BtnCancelShow = false;
                    this.ctlToolbar.BtnSubmitShow = false;
                }
            }
        }
        else if (data.STATUS == Constz.Requisition.Status.Approved.Code || data.STATUS == Constz.Requisition.Status.Void.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }

        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.Purchase, data.LOID) + " return false;";
    }

    private PurchaseRequestData GetData()
    {
        PurchaseRequestData data = new PurchaseRequestData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtPDRequestCode.Text.Trim();
        data.REQUSETDATE = this.ctlRequestDate.DateValue;
        data.ORDERTYPE = Constz.OrderType.PO.Code;
        data.PURCHASETYPE = Convert.ToDouble(this.cmbPurchaseType.SelectedItem.Value);
        data.REQUESTBY = Convert.ToDouble(this.txtRequestByID.Text == "" ? "0" : this.txtRequestByID.Text);
        data.DIVISION = Convert.ToDouble(this.txtDivision.Text == "" ? "0" : this.txtDivision.Text);
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.STATUS = this.txtStatus.Text.Trim();
        data.REQUIREMENT = this.txtRequirement.Text.Trim();
        data.REASON = this.txtReason.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.FROMCOMPANY = this.txtFromCompany.Text.Trim();
        data.ITEM = ItemObj.GetItemList();

        return data;
    }

    private PurchaseRequestData GetRecentCancelPRData()
    {
        PurchaseRequestData data = new PurchaseRequestData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtPDRequestCode.Text.Trim();
        data.REQUSETDATE = this.ctlRequestDate.DateValue;
        data.ORDERTYPE = Constz.OrderType.PO.Code;
        data.PURCHASETYPE = Convert.ToDouble(this.cmbPurchaseType.SelectedItem.Value);
        data.REQUESTBY = Convert.ToDouble(this.txtRequestByID.Text == "" ? "0" : this.txtRequestByID.Text);
        data.DIVISION = Convert.ToDouble(this.txtDivision.Text == "" ? "0" : this.txtDivision.Text);
        data.APPROVER = Authz.CurrentUserInfo.UserID;
        data.APPROVEDATE = DateTime.Now.Date;
        data.APPROVE = "Y";
        data.ACTIVE = Constz.ActiveStatus.Active;
        if (Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID)
        {
            data.STATUS = (Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID ? Constz.Requisition.Status.Approved.Code : Constz.Requisition.Status.SP.Code);
        }
        else if (Authz.CurrentUserInfo.DivisionID == Constz.AdminDepartment.LOID)
        {
            data.STATUS = Constz.Requisition.Status.Void.Code;
        }
        data.REQUIREMENT = this.txtRequirement.Text.Trim();
        data.REASON = this.txtReason.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.FROMCOMPANY = this.txtFromCompany.Text.Trim();
        data.ITEM = ItemObj.GetRecentItemList();

        return data;
    }
    private PurchaseRequestData GetRecentData()
    {
        PurchaseRequestData data = new PurchaseRequestData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtPDRequestCode.Text.Trim();
        data.REQUSETDATE = this.ctlRequestDate.DateValue;
        data.ORDERTYPE = Constz.OrderType.PO.Code;
        data.PURCHASETYPE = Convert.ToDouble(this.cmbPurchaseType.SelectedItem.Value);
        data.REQUESTBY = Convert.ToDouble(this.txtRequestByID.Text == "" ? "0" : this.txtRequestByID.Text);
        data.DIVISION = Convert.ToDouble(this.txtDivision.Text == "" ? "0" : this.txtDivision.Text);
        data.APPROVER = Authz.CurrentUserInfo.UserID;
        data.APPROVEDATE = DateTime.Now.Date;
        data.APPROVE = "Y";
        data.ACTIVE = Constz.ActiveStatus.Active;
        if (Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID)
        {
            data.STATUS =  Constz.Requisition.Status.Approved.Code;
        }
        else if (Authz.CurrentUserInfo.DivisionID == Constz.AdminDepartment.LOID)
        {
            data.STATUS = Constz.Requisition.Status.SP.Code;
            this.ctlToolbar.BtnSubmitShow = false;
        }
        else 
        {
            data.STATUS = Constz.Requisition.Status.SP.Code;
        }
        data.REQUIREMENT = this.txtRequirement.Text.Trim();
        data.REASON = this.txtReason.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.FROMCOMPANY = this.txtFromCompany.Text.Trim();
        data.ITEM = ItemObj.GetRecentItemList();

        return data;
    }

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnVoid.Text = "<img src='" + Constz.ImageFolder + "icn_delete.gif' border='0' align='AbsMiddle'> ไม่อนุมัติรายการ";
            btnVoid.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnVoid.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
            this.btnVoid.OnClientClick = "return confirm('ต้องการไม่อนุมัติรายการนี้ใช่หรือไม่?');";

            btnCancelPR.Text = "<img src='" + Constz.ImageFolder + "icn_delete.gif' border='0' align='AbsMiddle'> ยกเลิก PR";
            btnCancelPR.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnCancelPR.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
            this.btnCancelPR.OnClientClick = "return confirm('ยืนยันการยกเลิก PR ใช่หรือไม่?');";

            ComboSource.BuildCombo(this.cmbPurchaseType, "PURCHASETYPE", "NAME", "LOID", "NAME", "");
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));

            if (Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID)
            {
                this.ctlToolbar.NameBtnSubmit = "อนุมัติรายการ";
                this.ctlToolbar.SetButtonText();
                this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการอนุมัติรายการ');";
                this.btnCancelPR.Visible = true;
                this.btnVoid.Visible = false;
            }
            else if (Authz.CurrentUserInfo.DivisionID == Constz.AdminDepartment.LOID && this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
            {
                this.btnCancelPR.Visible = true;
                this.ctlToolbar.NameBtnSubmit = "ส่งให้จัดซื้อ";
                this.ctlToolbar.SetButtonText();
                this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งให้จัดซื้อ');";
            }
            else
            {
                this.ctlToolbar.NameBtnSubmit = "ส่งให้จัดซื้อ";
                this.ctlToolbar.SetButtonText();
                this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งให้จัดซื้อ');";
                this.btnCancelPR.Visible = false;
            }

            if (this.cmbPurchaseType.SelectedValue == "6" && this.txtStatus.Text != Constz.Requisition.Status.Approved.Code)
            {
                this.ctlRequestDate.Enabled = true;
            }
            else
            {
                this.ctlRequestDate.Enabled = false;
            }
        }
    }

    protected void cmbPurchaseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        if (cmb.SelectedValue == "6")
        {
            this.ctlRequestDate.Enabled = true;
        }
        else
        {
            this.ctlRequestDate.Enabled = false;
            this.ctlRequestDate.DateValue = DateTime.Now.Date;
        }
    }

    #region grvItemNew

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            Int16 rowIndex = 0;
            CheckBox chkUrgent = (CheckBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("chkUrgent");
            CheckBox chkMaterial = (CheckBox)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("chkMaterial");
            //DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("cmbNewProduct");
            TextBox txtProduct = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewProduct");
            TextBox txtProductName = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewProductName");
            TextBox txtBarcode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewBarCode");
            TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewQty");
            //DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("cmbNewUnit");
            TextBox txtUnit = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewUnit");
            TextBox txtUnitName = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewUnitName");
            TextBox txtOldPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewOldPrice");
            TextBox txtCurPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("txtNewCurPrice");
            TextBox txtMinPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[10].FindControl("txtNewMinPrice");
            TextBox txtMinStock = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[11].FindControl("txtNewMinStock");
            TextBox txtMaxStock = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[12].FindControl("txtNewMaxStock");
            TextBox txtStock = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[13].FindControl("txtNewStock");
            TextBox txtLast3Mon = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[14].FindControl("txtNewLast3Mon");
            TextBox txtLastYear = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[15].FindControl("txtNewLastYear");
            Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItemNew.Rows[rowIndex].Cells[16].FindControl("ctlNewDueDate");
            PRItemData data = new PRItemData();

            data.BARCODE = txtBarcode.Text.Trim();
            data.PRODUCT = Convert.ToDouble(txtProduct.Text == "" ? "0" : txtProduct.Text);
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.PRODUCTNAME = txtProductName.Text;
            data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
            data.OLDPRICE = Convert.ToDouble(txtOldPrice.Text);
            data.CURPRICE = Convert.ToDouble(txtCurPrice.Text == "" ? "0" : txtCurPrice.Text);
            data.MINPRICE = Convert.ToDouble(txtMinPrice.Text);
            data.MINSTOCK = Convert.ToDouble(txtMinStock.Text);
            data.MAXSTOCK = Convert.ToDouble(txtMaxStock.Text);
            data.STOCK = Convert.ToDouble(txtStock.Text);
            data.LAST3MON = Convert.ToDouble(txtLast3Mon.Text);
            data.LASTYEAR = Convert.ToDouble(txtLastYear.Text);
            data.DUEDATE = ctlDueDate.DateValue;
            if (chkUrgent.Checked)
            {
                data.URGENT = "Y";
            }
            else
            {
                data.URGENT = "N";
            }

            if (chkMaterial.Checked)
            {
                data.ISMATERIAL = "Y";
            }
            else
            {
                data.ISMATERIAL = "N";
            }

            if (ItemObj.InsertPRItem(data))
            {
                this.grvItem.DataBind();
                this.grvItemNew.DataBind();
                SetGrvItem(this.txtStatus.Text);
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imbSearch = (ImageButton)e.Row.Cells[3].FindControl("imbNewSearch");
            TextBox txtProduct = (TextBox)e.Row.Cells[3].FindControl("txtNewProduct");
            TextBox txtBarcode = (TextBox)e.Row.Cells[3].FindControl("txtNewBarCode");
            ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[5].FindControl("txtNewQty"));
            ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[8].FindControl("txtNewCurPrice"));

            string script = "";
            script += "document.getElementById('" + txtProduct.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupProductSearch.aspx?barcode=' + document.getElementById('" + txtBarcode.ClientID + "').value, '600', '450');";
            script += "if ('undefined' !=  document.getElementById('" + txtProduct.ClientID + "').value && '' != document.getElementById('" + txtProduct.ClientID + "').value) return true; else {;  ";
            script += "document.getElementById('" + txtProduct.ClientID + "').value = ''; return false; }";
            imbSearch.OnClientClick = script;

         //   ComboSource.BuildCombo((DropDownList)e.Row.Cells[4].FindControl("cmbNewProduct"), "V_PRODUCT_PR_LIST", "PDNAME", "LOID", "PDNAME", "", "เลือก", "0");
         //   ComboSource.BuildCombo((DropDownList)e.Row.Cells[6].FindControl("cmbNewUnit"), "UNIT", "NAME", "LOID", "NAME", "", "เลือก", "0");

        }
    }

    protected void txtNewBarCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("cmbNewProduct");
        DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("cmbNewUnit");
        TextBox txtOldPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewOldPrice");
        TextBox txtMinPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("txtNewMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[10].FindControl("txtNewMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[11].FindControl("txtNewMaxStock");
        TextBox txtStock = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[12].FindControl("txtNewStock");
        TextBox txtLast3Mon = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[13].FindControl("txtNewLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[14].FindControl("txtNewLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItemNew.Rows[rowIndex].Cells[15].FindControl("ctlNewDueDate");

        PRItemData data = FlowObj.GetRecentPRItem(txt.Text.Trim());

        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByText(data.PRODUCTNAME));
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(data.UNITNAME));
        txtOldPrice.Text = data.OLDPRICE.ToString();
        txtMinPrice.Text = data.MINPRICE.ToString();
        txtMinStock.Text = data.MINSTOCK.ToString();
        txtMaxStock.Text = data.MAXSTOCK.ToString();
        txtStock.Text = data.STOCK.ToString();
        txtLast3Mon.Text = data.LAST3MON.ToString();
        txtLastYear.Text = data.LASTYEAR.ToString();
        ctlDueDate.DateValue = data.DUEDATE;
    }

    protected void cmbNewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        TextBox txtCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewBarCode");
        DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("cmbNewUnit");
        TextBox txtOldPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewOldPrice");
        TextBox txtMinPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("txtNewMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[10].FindControl("txtNewMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[11].FindControl("txtNewMaxStock");
        TextBox txtStock = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[12].FindControl("txtNewStock");
        TextBox txtLast3Mon = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[13].FindControl("txtNewLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[14].FindControl("txtNewLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItemNew.Rows[rowIndex].Cells[15].FindControl("ctlNewDueDate");

        PRItemData data = FlowObj.GetRecentPRItem(Convert.ToDouble(cmb.SelectedItem.Value));

        txtCode.Text = data.BARCODE;
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(data.UNITNAME));
        txtOldPrice.Text = data.OLDPRICE.ToString();
        txtMinPrice.Text = data.MINPRICE.ToString();
        txtMinStock.Text = data.MINSTOCK.ToString();
        txtMaxStock.Text = data.MAXSTOCK.ToString();
        txtStock.Text = data.STOCK.ToString();
        txtLast3Mon.Text = data.LAST3MON.ToString();
        txtLastYear.Text = data.LASTYEAR.ToString();
        ctlDueDate.DateValue = data.DUEDATE;
    }

    #endregion

    #region grvItem

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            CheckBox chkUrgent = (CheckBox)this.grvItem.FooterRow.Cells[2].FindControl("chkUrgent");
            CheckBox chkMaterial = (CheckBox)this.grvItem.FooterRow.Cells[3].FindControl("chkMaterial");
            //DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[5].FindControl("cmbNewProduct");
            TextBox txtProduct = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewProduct");
            TextBox txtProductName = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtNewProductName");
            TextBox txtBarcode = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewBarCode");
            TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewQty");
            //DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[7].FindControl("cmbNewUnit");
            TextBox txtUnit = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewUnit");
            TextBox txtUnitName = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewUnitName");
            TextBox txtOldPrice = (TextBox)this.grvItem.FooterRow.Cells[8].FindControl("txtNewOldPrice");
            TextBox txtCurPrice = (TextBox)this.grvItem.FooterRow.Cells[9].FindControl("txtNewCurPrice");
            TextBox txtMinPrice = (TextBox)this.grvItem.FooterRow.Cells[10].FindControl("txtNewMinPrice");
            TextBox txtMinStock = (TextBox)this.grvItem.FooterRow.Cells[11].FindControl("txtNewMinStock");
            TextBox txtMaxStock = (TextBox)this.grvItem.FooterRow.Cells[12].FindControl("txtNewMaxStock");
            TextBox txtStock = (TextBox)this.grvItem.FooterRow.Cells[13].FindControl("txtNewStock");
            TextBox txtLast3Mon = (TextBox)this.grvItem.FooterRow.Cells[14].FindControl("txtNewLast3Mon");
            TextBox txtLastYear = (TextBox)this.grvItem.FooterRow.Cells[15].FindControl("txtNewLastYear");
            Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.FooterRow.Cells[16].FindControl("ctlNewDueDate");
            PRItemData data = new PRItemData();

            data.PRODUCT = Convert.ToDouble(txtProduct.Text == "" ? "0" : txtProduct.Text);
            data.PRODUCTNAME = txtProductName.Text;
            data.BARCODE = txtBarcode.Text.Trim();
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
            data.UNITNAME = txtUnitName.Text;
            data.OLDPRICE = Convert.ToDouble(txtOldPrice.Text);
            data.CURPRICE = Convert.ToDouble(txtCurPrice.Text == "" ? "0" : txtCurPrice.Text);
            data.MINPRICE = Convert.ToDouble(txtMinPrice.Text);
            data.MINSTOCK = Convert.ToDouble(txtMinStock.Text);
            data.MAXSTOCK = Convert.ToDouble(txtMaxStock.Text);
            data.STOCK = Convert.ToDouble(txtStock.Text);
            data.LAST3MON = Convert.ToDouble(txtLast3Mon.Text);
            data.LASTYEAR = Convert.ToDouble(txtLastYear.Text);
            data.DUEDATE = ctlDueDate.DateValue;
            if (chkUrgent.Checked)
            {
                data.URGENT = "Y";
            }
            else
            {
                data.URGENT = "N";
            }

            if (chkMaterial.Checked)
            {
                data.ISMATERIAL = "Y";
            }
            else
            {
                data.ISMATERIAL = "N";
            }

            if (ItemObj.InsertPRItem(data))
            {
                SetGrvItem(this.txtStatus.Text);
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
               // DropDownList cmbProduct = (DropDownList)e.Row.Cells[5].FindControl("cmbProduct");
              //  DropDownList cmbUnit = (DropDownList)e.Row.Cells[7].FindControl("cmbUnit");
                CheckBox chkUrgent = (CheckBox)e.Row.Cells[2].FindControl("chkUrgent");
                CheckBox chkMaterial = (CheckBox)e.Row.Cells[3].FindControl("chkMaterial");
                ImageButton imbSearch = (ImageButton)e.Row.Cells[4].FindControl("imbSearch");
                TextBox txtProduct = (TextBox)e.Row.Cells[4].FindControl("txtProduct");
                TextBox txtBarcode = (TextBox)e.Row.Cells[4].FindControl("txtBarCode");
                ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[6].FindControl("txtQty"));
                ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[9].FindControl("txtCurPrice"));

                string script = "";
                script += "document.getElementById('" + txtProduct.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupProductSearch.aspx?barcode=' + document.getElementById('" + txtBarcode.ClientID + "').value, '600', '450');";
                script += "if ('undefined' !=  document.getElementById('" + txtProduct.ClientID + "').value && '' != document.getElementById('" + txtProduct.ClientID + "').value) return true; else {;  ";
                script += "document.getElementById('" + txtProduct.ClientID + "').value = ''; return false; }";
                imbSearch.OnClientClick = script;

               // ComboSource.BuildCombo(cmbProduct, "V_PRODUCT_PR_LIST", "PDNAME", "LOID", "PDNAME", "");
               // ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");

                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
              //  cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
              //  cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));
                if (drow["URGENT"].ToString() == "Y")
                {
                    chkUrgent.Checked = true;
                }
                else
                {
                    chkUrgent.Checked = false;
                }
                chkUrgent.Enabled = true;

                if (drow["ISMATERIAL"].ToString() == "Y")
                {
                    chkMaterial.Checked = true;
                }
                else
                {
                    chkMaterial.Checked = false;
                }
                chkMaterial.Enabled = true;

                ImageButton imbCancel = (ImageButton)e.Row.FindControl("imbCancel");

                imbCancel.OnClientClick = "return confirm('ยืนยันการยกเลิกรายการ');";
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
              //  DropDownList cmbProduct = (DropDownList)e.Row.Cells[5].FindControl("cmbProductView");
              //  DropDownList cmbUnit = (DropDownList)e.Row.Cells[7].FindControl("cmbUnitView");
                CheckBox chkUrgent = (CheckBox)e.Row.Cells[2].FindControl("chkUrgent");
                CheckBox chkMaterial = (CheckBox)e.Row.Cells[3].FindControl("chkMaterial");

             //   ComboSource.BuildCombo(cmbProduct, "V_PRODUCT_PR_LIST", "PDNAME", "LOID", "PDNAME", "");
             //   ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");

                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            //    cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
            //    cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));
                if (drow["URGENT"].ToString() == "Y")
                {
                    chkUrgent.Checked = true;
                }
                else
                {
                    chkUrgent.Checked = false;
                }
                chkUrgent.Enabled = false;

                if (drow["ISMATERIAL"].ToString() == "Y")
                {
                    chkMaterial.Checked = true;
                }
                else
                {
                    chkMaterial.Checked = false;
                }
                chkMaterial.Enabled = false;

                ImageButton imbDelete = (ImageButton)e.Row.FindControl("imbDelete");

                imbDelete.OnClientClick = "return confirm('ยืนยันการลบรายการ');";
            }
            
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ImageButton imbSearch = (ImageButton)e.Row.Cells[4].FindControl("imbNewSearch");
            TextBox txtProduct = (TextBox)e.Row.Cells[4].FindControl("txtNewProduct");
            TextBox txtBarcode = (TextBox)e.Row.Cells[4].FindControl("txtNewBarCode");
            ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[6].FindControl("txtNewQty"));
            ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[9].FindControl("txtNewCurPrice"));

            string script = "";
            script += "document.getElementById('" + txtProduct.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupProductSearch.aspx?barcode=' + document.getElementById('" + txtBarcode.ClientID + "').value, '600', '450');";
            script += "if ('undefined' !=  document.getElementById('" + txtProduct.ClientID + "').value && '' != document.getElementById('" + txtProduct.ClientID + "').value) return true; else {;  ";
            script += "document.getElementById('" + txtProduct.ClientID + "').value = ''; return false; }";
            imbSearch.OnClientClick = script;

          //  ComboSource.BuildCombo((DropDownList)e.Row.Cells[4].FindControl("cmbNewProduct"), "V_PRODUCT_PR_LIST", "PDNAME", "LOID", "PDNAME", "", "เลือก", "0");
          //  ComboSource.BuildCombo((DropDownList)e.Row.Cells[7].FindControl("cmbNewUnit"), "UNIT", "NAME", "LOID", "NAME", "", "เลือก", "0");
        }
    }

    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[rowIndex].Cells[4].FindControl("cmbProduct");
        DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[rowIndex].Cells[6].FindControl("cmbUnit");
        TextBox txtOldPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[7].FindControl("txtOldPrice");
        TextBox txtMinPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[9].FindControl("txtMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItem.Rows[rowIndex].Cells[10].FindControl("txtMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItem.Rows[rowIndex].Cells[11].FindControl("txtMaxStock");
        TextBox txtStock = (TextBox)this.grvItem.Rows[rowIndex].Cells[12].FindControl("txtStock");
        TextBox txtLast3Mon = (TextBox)this.grvItem.Rows[rowIndex].Cells[13].FindControl("txtLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItem.Rows[rowIndex].Cells[14].FindControl("txtLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.Rows[rowIndex].Cells[15].FindControl("ctlDueDate");

        PRItemData data = FlowObj.GetRecentPRItem(txt.Text.Trim());

        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByText(data.PRODUCTNAME));
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(data.UNITNAME));
        txtOldPrice.Text = data.OLDPRICE.ToString();
        txtMinPrice.Text = data.MINPRICE.ToString();
        txtMinStock.Text = data.MINSTOCK.ToString(Constz.DblFormat);
        txtMaxStock.Text = data.MAXSTOCK.ToString(Constz.DblFormat);
        txtStock.Text = data.STOCK.ToString();
        txtLast3Mon.Text = data.LAST3MON.ToString();
        txtLastYear.Text = data.LASTYEAR.ToString();
        ctlDueDate.DateValue = data.DUEDATE;
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        TextBox txtCode = (TextBox)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtBarCode");
        DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[rowIndex].Cells[6].FindControl("cmbUnit");
        TextBox txtOldPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[7].FindControl("txtOldPrice");
        TextBox txtMinPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[9].FindControl("txtMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItem.Rows[rowIndex].Cells[10].FindControl("txtMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItem.Rows[rowIndex].Cells[11].FindControl("txtMaxStock");
        TextBox txtStock = (TextBox)this.grvItem.Rows[rowIndex].Cells[12].FindControl("txtStock");
        TextBox txtLast3Mon = (TextBox)this.grvItem.Rows[rowIndex].Cells[13].FindControl("txtLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItem.Rows[rowIndex].Cells[14].FindControl("txtLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.Rows[rowIndex].Cells[15].FindControl("ctlDueDate");

        PRItemData data = FlowObj.GetRecentPRItem(Convert.ToDouble(cmb.SelectedItem.Value));

        txtCode.Text = data.BARCODE;
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(data.UNITNAME));
        txtOldPrice.Text = data.OLDPRICE.ToString();
        txtMinPrice.Text = data.MINPRICE.ToString();
        txtMinStock.Text = data.MINSTOCK.ToString(Constz.DblFormat);
        txtMaxStock.Text = data.MAXSTOCK.ToString(Constz.DblFormat);
        txtStock.Text = data.STOCK.ToString();
        txtLast3Mon.Text = data.LAST3MON.ToString();
        txtLastYear.Text = data.LASTYEAR.ToString();
        ctlDueDate.DateValue = data.DUEDATE;
    }

    protected void txtNewBarCode_TextChanged1(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[4].FindControl("cmbNewProduct");
        DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[6].FindControl("cmbNewUnit");
        TextBox txtOldPrice = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewOldPrice");
        TextBox txtMinPrice = (TextBox)this.grvItem.FooterRow.Cells[9].FindControl("txtNewMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItem.FooterRow.Cells[10].FindControl("txtNewMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItem.FooterRow.Cells[11].FindControl("txtNewMaxStock");
        TextBox txtStock = (TextBox)this.grvItem.FooterRow.Cells[12].FindControl("txtNewStock");
        TextBox txtLast3Mon = (TextBox)this.grvItem.FooterRow.Cells[13].FindControl("txtNewLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItem.FooterRow.Cells[14].FindControl("txtNewLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.FooterRow.Cells[15].FindControl("ctlNewDueDate");

        PRItemData data = FlowObj.GetRecentPRItem(txt.Text.Trim());

        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByText(data.PRODUCTNAME));
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(data.UNITNAME));
        txtOldPrice.Text = data.OLDPRICE.ToString();
        txtMinPrice.Text = data.MINPRICE.ToString();
        txtMinStock.Text = data.MINSTOCK.ToString(Constz.DblFormat);
        txtMaxStock.Text = data.MAXSTOCK.ToString(Constz.DblFormat);
        txtStock.Text = data.STOCK.ToString();
        txtLast3Mon.Text = data.LAST3MON.ToString();
        txtLastYear.Text = data.LASTYEAR.ToString();
        ctlDueDate.DateValue = data.DUEDATE;
    }

    protected void cmbNewProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        TextBox txtCode = (TextBox)this.grvItem.FooterRow.Cells[3].FindControl("txtNewBarCode");
        DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[6].FindControl("cmbNewUnit");
        TextBox txtOldPrice = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewOldPrice");
        TextBox txtMinPrice = (TextBox)this.grvItem.FooterRow.Cells[9].FindControl("txtNewMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItem.FooterRow.Cells[10].FindControl("txtNewMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItem.FooterRow.Cells[11].FindControl("txtNewMaxStock");
        TextBox txtStock = (TextBox)this.grvItem.FooterRow.Cells[12].FindControl("txtNewStock");
        TextBox txtLast3Mon = (TextBox)this.grvItem.FooterRow.Cells[13].FindControl("txtNewLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItem.FooterRow.Cells[14].FindControl("txtNewLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.FooterRow.Cells[15].FindControl("ctlNewDueDate");

        PRItemData data = FlowObj.GetRecentPRItem(Convert.ToDouble(cmb.SelectedItem.Value));

        txtCode.Text = data.BARCODE;
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(data.UNITNAME));
        txtOldPrice.Text = data.OLDPRICE.ToString();
        txtMinPrice.Text = data.MINPRICE.ToString();
        txtMinStock.Text = data.MINSTOCK.ToString(Constz.DblFormat);
        txtMaxStock.Text = data.MAXSTOCK.ToString(Constz.DblFormat);
        txtStock.Text = data.STOCK.ToString();
        txtLast3Mon.Text = data.LAST3MON.ToString();
        txtLastYear.Text = data.LASTYEAR.ToString();
        ctlDueDate.DateValue = data.DUEDATE;
    }

    protected void grvItem_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        //e.ExceptionHandled = (e.Exception != null);
        //if (e.ExceptionHandled)
        //{
        //    e.KeepInEditMode = true;
        //    Appz.ClientAlert(this, e.Exception.InnerException.Message);
        //}
    }

    protected void grvItem_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.ExceptionHandled)
        {
            Appz.ClientAlert(this, e.Exception.Message);
        }
        else
        {
            SetGrvItem(this.txtStatus.Text);
        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        CheckBox chkUrgent = (CheckBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("chkUrgent");
        CheckBox chkMaterial = (CheckBox)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("chkMaterial");
        //DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("cmbProduct");
        TextBox txtProduct = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtProduct");
       // TextBox txtProductName = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("txtProductName");
        TextBox txtBarcode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtBarCode");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtQty");
       // DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("cmbUnit");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtUnit");
        TextBox txtOldPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[8].FindControl("txtOldPrice");
        TextBox txtCurPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[9].FindControl("txtCurPrice");
        TextBox txtMinPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[10].FindControl("txtMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[11].FindControl("txtMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[12].FindControl("txtMaxStock");
        TextBox txtStock = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[13].FindControl("txtStock");
        TextBox txtLast3Mon = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[14].FindControl("txtLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[15].FindControl("txtLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.Rows[e.RowIndex].Cells[16].FindControl("ctlDueDate");

        PRItemData data = new PRItemData();

        data.BARCODE = txtBarcode.Text.Trim();
        data.PRODUCT = Convert.ToDouble(txtProduct.Text == "" ? "0" : txtProduct.Text);
        //data.PRODUCTNAME = txtProductName.Text;
        //data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
        data.OLDPRICE = Convert.ToDouble(txtOldPrice.Text);
        data.CURPRICE = Convert.ToDouble(txtCurPrice.Text);
        data.MINPRICE = Convert.ToDouble(txtMinPrice.Text);
        data.MINSTOCK = Convert.ToDouble(txtMinStock.Text);
        data.MAXSTOCK = Convert.ToDouble(txtMaxStock.Text);
        data.STOCK = Convert.ToDouble(txtStock.Text);
        data.LAST3MON = Convert.ToDouble(txtLast3Mon.Text);
        data.LASTYEAR = Convert.ToDouble(txtLastYear.Text);
        data.DUEDATE = ctlDueDate.DateValue;
        if (chkUrgent.Checked)
        {
            data.URGENT = "Y";
        }
        else
        {
            data.URGENT = "N";
        }

        if (chkMaterial.Checked)
        {
            data.ISMATERIAL = "Y";
        }
        else
        {
            data.ISMATERIAL = "N";
        }

        e.NewValues["LOID"] = this.grvItem.Rows[e.RowIndex].Cells[16].Text;
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
       // e.NewValues["PRODUCTNAME"] = data.PRODUCTNAME;
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["UNIT"] = data.UNIT.ToString();
        e.NewValues["MINSTOCK"] = data.MINSTOCK.ToString(Constz.DblFormat);
        e.NewValues["MAXSTOCK"] = data.MAXSTOCK.ToString(Constz.DblFormat);
        e.NewValues["STOCK"] = data.STOCK.ToString();
        e.NewValues["OLDPRICE"] = data.OLDPRICE.ToString();
        e.NewValues["CURPRICE"] = data.CURPRICE.ToString();
        e.NewValues["MINPRICE"] = data.MINPRICE.ToString();
        e.NewValues["LAST3MON"] = data.LAST3MON.ToString();
        e.NewValues["LASTYEAR"] = data.LASTYEAR.ToString();
        e.NewValues["DUEDATE"] = data.DUEDATE.ToString();
        e.NewValues["URGENT"] = data.URGENT;
        e.NewValues["ISMATERIAL"] = data.ISMATERIAL;
        e.NewValues["BARCODE"] = data.BARCODE;

    }

    #endregion

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseRequestSearch.aspx");
    }

    protected void btnCancelPR_Click(object sender, EventArgs e)
    {
        if (FlowObj.VoidData(Authz.CurrentUserInfo.UserID, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)))
        {
            ResetState(FlowObj.LOID);
            //Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetRecentData()))
        {
            ResetState(FlowObj.LOID);
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
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

    protected void PrintClick(object sender, EventArgs e)
    {
    }

    #endregion

    protected void btnVoid_Click(object sender, EventArgs e)
    {
        if (FlowObj.VoidData(Authz.CurrentUserInfo.UserID, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void imbSearch_Click(object sender, ImageClickEventArgs e)
    {
        int rowindex = 0;
        rowindex = (Int16)((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
        TextBox txtProduct = (TextBox)this.grvItem.Rows[rowindex].Cells[4].FindControl("txtProduct");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[rowindex].Cells[4].FindControl("txtUnit");
        TextBox txtCode = (TextBox)this.grvItem.Rows[rowindex].Cells[4].FindControl("txtBarCode");
        //DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[rowindex].Cells[4].FindControl("cmbProduct");
        TextBox txtProductName = (TextBox)this.grvItem.Rows[rowindex].Cells[5].FindControl("txtProductName");
        TextBox txtUnitName = (TextBox)this.grvItem.Rows[rowindex].Cells[7].FindControl("txtUnitName");
        DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[rowindex].Cells[7].FindControl("cmbUnit");
        TextBox txtOldPrice = (TextBox)this.grvItem.Rows[rowindex].Cells[8].FindControl("txtOldPrice");
        TextBox txtMinPrice = (TextBox)this.grvItem.Rows[rowindex].Cells[10].FindControl("txtMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItem.Rows[rowindex].Cells[11].FindControl("txtMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItem.Rows[rowindex].Cells[12].FindControl("txtMaxStock");
        TextBox txtStock = (TextBox)this.grvItem.Rows[rowindex].Cells[13].FindControl("txtStock");
        TextBox txtLast3Mon = (TextBox)this.grvItem.Rows[rowindex].Cells[14].FindControl("txtLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItem.Rows[rowindex].Cells[15].FindControl("txtLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.Rows[rowindex].Cells[15].FindControl("ctlDueDate");

        PRItemData data = FlowObj.GetRecentPRItem(Convert.ToDouble(txtProduct.Text == "" ? "0" : txtProduct.Text));

        txtProduct.Text = data.PRODUCT.ToString();
        txtProductName.Text = data.PRODUCTNAME;
        txtCode.Text = data.BARCODE;
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(data.UNITNAME));
        txtOldPrice.Text = data.OLDPRICE.ToString();
        txtMinPrice.Text = data.MINPRICE.ToString();
        txtMinStock.Text = data.MINSTOCK.ToString(Constz.DblFormat);
        txtMaxStock.Text = data.MAXSTOCK.ToString(Constz.DblFormat);
        txtStock.Text = data.STOCK.ToString();
        txtLast3Mon.Text = data.LAST3MON.ToString();
        txtLastYear.Text = data.LASTYEAR.ToString();
        ctlDueDate.DateValue = data.DUEDATE;
    }

    protected void imbNewSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.grvItem.FooterRow.Cells[1].Text = (this.grvItem.Rows.Count + 1).ToString();
        TextBox txtUnit = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewUnit");
        TextBox txtProduct = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewProduct");
        TextBox txtCode = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewBarCode");
        //DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[4].FindControl("cmbNewProduct");
        TextBox txtProductName = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewProductName");
        //DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[6].FindControl("cmbNewUnit");
        TextBox txtUnitName = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewUnitName");
        TextBox txtOldPrice = (TextBox)this.grvItem.FooterRow.Cells[8].FindControl("txtNewOldPrice");
        TextBox txtMinPrice = (TextBox)this.grvItem.FooterRow.Cells[10].FindControl("txtNewMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItem.FooterRow.Cells[11].FindControl("txtNewMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItem.FooterRow.Cells[12].FindControl("txtNewMaxStock");
        TextBox txtStock = (TextBox)this.grvItem.FooterRow.Cells[13].FindControl("txtNewStock");
        TextBox txtLast3Mon = (TextBox)this.grvItem.FooterRow.Cells[14].FindControl("txtNewLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItem.FooterRow.Cells[15].FindControl("txtNewLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.FooterRow.Cells[16].FindControl("ctlNewDueDate");

        PRItemData data = FlowObj.GetRecentPRItem(Convert.ToDouble(txtProduct.Text == "" ? "0" : txtProduct.Text));

        txtProduct.Text = data.PRODUCT.ToString();
        txtProductName.Text = data.PRODUCTNAME;
        //cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.PRODUCT.ToString()));
        txtCode.Text = data.BARCODE;
        //cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(data.UNITNAME));
        txtUnitName.Text = data.UNITNAME;
        txtUnit.Text = data.UNIT.ToString();
        txtOldPrice.Text = data.OLDPRICE.ToString();
        txtMinPrice.Text = data.MINPRICE.ToString();
        txtMinStock.Text = data.MINSTOCK.ToString(Constz.DblFormat);
        txtMaxStock.Text = data.MAXSTOCK.ToString(Constz.DblFormat);
        txtStock.Text = data.STOCK.ToString();
        txtLast3Mon.Text = data.LAST3MON.ToString();
        txtLastYear.Text = data.LASTYEAR.ToString();
        ctlDueDate.DateValue = data.DUEDATE;
    }

    protected void imbNewSearchNew_Click(object sender, ImageClickEventArgs e)
    {
        int rowindex = 0;
        this.grvItemNew.Rows[rowindex].Cells[1].Text = (this.grvItem.Rows.Count + 1).ToString();
        TextBox txtProduct = (TextBox)this.grvItemNew.Rows[rowindex].Cells[4].FindControl("txtNewProduct");
        TextBox txtUnit = (TextBox)this.grvItemNew.Rows[rowindex].Cells[4].FindControl("txtNewUnit");
        TextBox txtCode = (TextBox)this.grvItemNew.Rows[rowindex].Cells[4].FindControl("txtNewBarCode");
        //DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowindex].Cells[4].FindControl("cmbNewProduct");
        TextBox txtProductName = (TextBox)this.grvItemNew.Rows[rowindex].Cells[5].FindControl("txtNewProductName");
        TextBox txtUnitName = (TextBox)this.grvItemNew.Rows[rowindex].Cells[7].FindControl("txtNewUnitName");
        // DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowindex].Cells[7].FindControl("cmbNewUnit");
        TextBox txtOldPrice = (TextBox)this.grvItemNew.Rows[rowindex].Cells[8].FindControl("txtNewOldPrice");
        TextBox txtMinPrice = (TextBox)this.grvItemNew.Rows[rowindex].Cells[10].FindControl("txtNewMinPrice");
        TextBox txtMinStock = (TextBox)this.grvItemNew.Rows[rowindex].Cells[11].FindControl("txtNewMinStock");
        TextBox txtMaxStock = (TextBox)this.grvItemNew.Rows[rowindex].Cells[12].FindControl("txtNewMaxStock");
        TextBox txtStock = (TextBox)this.grvItemNew.Rows[rowindex].Cells[13].FindControl("txtNewStock");
        TextBox txtLast3Mon = (TextBox)this.grvItemNew.Rows[rowindex].Cells[14].FindControl("txtNewLast3Mon");
        TextBox txtLastYear = (TextBox)this.grvItemNew.Rows[rowindex].Cells[15].FindControl("txtNewLastYear");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItemNew.Rows[rowindex].Cells[16].FindControl("ctlNewDueDate");

        PRItemData data = FlowObj.GetRecentPRItem(Convert.ToDouble(txtProduct.Text == "" ? "0" : txtProduct.Text));

        txtProduct.Text = data.PRODUCT.ToString();
        txtProductName.Text = data.PRODUCTNAME;
        //cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.PRODUCT.ToString()));
        txtCode.Text = data.BARCODE;
        //cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(data.UNITNAME));
        txtUnit.Text = data.UNIT.ToString();
        txtUnitName.Text = data.UNITNAME;
        txtOldPrice.Text = data.OLDPRICE.ToString();
        txtMinPrice.Text = data.MINPRICE.ToString();
        txtMinStock.Text = data.MINSTOCK.ToString(Constz.DblFormat);
        txtMaxStock.Text = data.MAXSTOCK.ToString(Constz.DblFormat);
        txtStock.Text = data.STOCK.ToString();
        txtLast3Mon.Text = data.LAST3MON.ToString();
        txtLastYear.Text = data.LASTYEAR.ToString();
        ctlDueDate.DateValue = data.DUEDATE;
    }
}
