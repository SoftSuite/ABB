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
using ABB.Data.Sales;
using ABB.Flow;
using ABB.Flow.Sales;
using ABB.Global;
using ABB.DAL;
using ABB.DAL.Sales;

public partial class Transaction_ProductInvoice : System.Web.UI.Page
{
    #region Variables & Properties

    private InvoiceFlow _flow;
    private SaleFlow _sFlow;
    private InvoiceItem item;
    private int indexBUTTON = 0;
    private int indexRANK = 1;
    private int indexBARCODE = 2;
    private int indexPRODUCT = 3;
    private int indexQTY = 4;
    private int indexUNIT = 5;
    private int indexPRICE = 6;
    private int indexNETPRICE = 7;
    private int indexNORMALDISCOUNT = 8;
    private int indexISVAT = 9;
    private int indexLOID = 10;

    public InvoiceFlow FlowObj
    {
        get { if (_flow == null) _flow = new InvoiceFlow(); return _flow; }
    }

    private SaleFlow SaleObj
    {
        get { if (_sFlow == null) _sFlow = new SaleFlow(); return _sFlow; }
    }

    public InvoiceItem ItemObj
    {
        get { if (item == null) item = new InvoiceItem(); return item; }
    }

    #endregion

    #region Methods

    #region Others

    private void CalculateDiscount()
    {
        ItemObj.CalculateDiscount(Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text), Convert.ToDouble(this.txtVat.Text));
        this.txtTotal.Text = ItemObj.TOTAL.ToString(Constz.DblFormat);
        this.txtTotalDiscount.Text = ItemObj.TOTALDISCOUNT.ToString(Constz.DblFormat);
        this.txtTotalVat.Text = ItemObj.TOTALVAT.ToString(Constz.DblFormat);
        this.txtGrandTotal.Text = ItemObj.GRANDTOTAL.ToString(Constz.DblFormat);
        this.txtNet.Text = ItemObj.GRANDTOTAL.ToString(Constz.IntFormat);
        this.txtDiscount.Text = ItemObj.DISCOUNTPERCENT.ToString(Constz.IntFormat);
        this.grvItem.DataBind();
    }

    private void SetGrvItem(string status)
    {
        SetGrvItem(status, false);
    }

    private void SetGrvItem(string status, bool isBind)
    {
        if (isBind)
            this.txtNewBind.Text = "1";
        else
            this.txtNewBind.Text = "0";
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        this.grvItemNew.Visible = false;
        this.grvItem.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code);
    }

    private void SetCustomerData(double customer, bool isSearch)
    {
        CustomerSaleData data = SaleObj.GetCustomerData(customer);
        this.txtCustomerCode.Text = data.CODE;
        this.txtCustomerName.Text = data.CUSTOMERNAME;
        this.cmbCondition.SelectedIndex = this.cmbCondition.Items.IndexOf(this.cmbCondition.Items.FindByValue(data.PAYMENT.ToString()));
        //this.txtCondition.Text = data.PAYMENT;
        if (data.PAYMENT == "CA" || data.PAYMENT == "CC")
        {
            this.ctlCreditDay.DateValue = DateTime.Now.Date;
        }
        else
        {
            this.ctlCreditDay.DateValue = DateTime.Now.AddDays(data.CREDITDAY);
        }
        if (isSearch)
        {
            this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.CTITLE.ToString()));
            this.txtName.Text = data.CNAME;
            this.txtLastName.Text = data.CLASTNAME;
            this.txtAddress.Text = data.CADDRESS;
            this.txtTel.Text = data.CTEL;
            this.txtFax.Text = data.CFAX;
        }
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        SetData(FlowObj.GetData(loid));
    }

    private void ClearPayment()
    {
        rbtPayment1.Checked = false;
        rbtPayment2.Checked = false;
        rbtPayment3.Checked = false;
        rbtPayment4.Checked = false;
        cmbCreditType.Enabled = false;
        cmbCreditType.SelectedIndex = 0;
        txtCreditID.Text = "";
        txtCreditID.Enabled = false;
        txtCheque.Text = "";
        txtCheque.Enabled = false;
        ctlChequeDate.DateValue = new DateTime(1, 1, 1);
        ctlChequeDate.Enabled = false;
        cmbBank.SelectedIndex = 0;
        cmbBank.Enabled = false;
        txtBranch.Text = "";
        txtBranch.Enabled = false;
        txtReason.Text = "";
        txtReason.Enabled = false;
    }

    #endregion

    #region Data

    private void SetData(ProductReserveData data)
    {
        if (data.LOID == 0)
        {
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
            data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.VAT = Convert.ToDouble(SysConfigFlow.GetValue(Constz.ConfigName.VAT));
            //    data.RESERVEDATE = DateTime.Today;
            //    this.cmbRequisitionType.Enabled = true;
        }
        //if (data.STATUS == Constz.Requisition.Status.Finish.Code)
        //{
        //    this.txtRefNo.ReadOnly = true;
        //    this.txtRefNo.CssClass = "zTextbox-View";
        //    this.btnSearch.Visible = false;
        //}
        this.txtReference.Text = data.REFNO;
        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtStatusName.Text = Appz.GetStatusName(data.STATUS);
        this.txtCustomer.Text = data.CUSTOMER.ToString();

        this.txtRequisitionType.Text = data.REQUISITIONTYPE.ToString();
        this.cmbRefType.SelectedIndex = this.cmbRefType.Items.IndexOf(this.cmbRefType.Items.FindByValue(data.REFTYPELOID.ToString()));
        if (this.cmbRefType.SelectedValue == Constz.Requisition.RequisitionType.REQ01.ToString())
        {
            this.txtRefNo.Text = data.CODE;
            this.txtCode.Text = data.CODE;
        }
        else
        {
            this.txtRefNo.Text = "";
            this.txtCode.Text = "";
        }

        this.txtCustomerCode.Text = "";
        this.txtCustomerName.Text = "";
        this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.CTITLE.ToString()));
        this.txtName.Text = data.CNAME;
        this.txtLastName.Text = data.CLASTNAME;
        this.txtAddress.Text = data.CADDRESS;
        this.txtTel.Text = data.CTEL;
        this.txtFax.Text = data.CFAX;
        this.ctlReqDate.DateValue = data.REQDATE.Year == 1 ? DateTime.Now : data.REQDATE;
        this.ctlRecieveDate.DateValue = data.RESERVEDATE;
        this.ctlSendDate.DateValue = data.RESERVEDATE.AddDays(14);
        this.txtCode.Text = this.txtRefNo.Text;
        this.ctlCreditDay.DateValue = data.CREDITDATE;
        this.txtInvCode.Text = data.INVCODE;
        this.cmbCondition.SelectedIndex = this.cmbCondition.Items.IndexOf(this.cmbCondition.Items.FindByText(data.PAYMENTCONDITION.ToString()));
        //this.txtCondition.Text = data.PAYMENTCONDITION;
        // this.txtRefNo.Text = data.CODE;
        // this.txtCode.Text = data.CODE;
        this.cmbDelivery.SelectedIndex = this.cmbDelivery.Items.IndexOf(this.cmbDelivery.Items.FindByValue(data.CDELIVERY.ToString()));
        //this.txtCondition.Text = data.PAYMENTCONDITION;
        //this.ctlCreditDay.DateValue = data.CREDITDATE;
        if (data.PAYMENT == Constz.Payment.Cash.Code)
        {
            this.rbtPayment1.Checked = true;
        }
        if (data.PAYMENT == Constz.Payment.CreditCard.Code)
        {
            this.rbtPayment2.Checked = true;
            this.cmbCreditType.SelectedIndex = this.cmbCreditType.Items.IndexOf(this.cmbCreditType.Items.FindByValue(data.CREDITTYPE.ToString()));
            this.cmbCreditType.Enabled = true;
            this.txtCreditID.Text = data.CREDITCARDID;
            this.txtCreditID.Enabled = true;
        }
        if (data.PAYMENT == Constz.Payment.Cheque.Code)
        {
            this.rbtPayment3.Checked = true;
            this.txtCheque.Text = data.CHEQUE;
            this.txtCheque.Enabled = true;
            this.ctlChequeDate.DateValue = data.CHEQUEDATE;
            this.ctlChequeDate.Enabled = true;
            this.cmbBank.SelectedIndex = this.cmbBank.Items.IndexOf(this.cmbBank.Items.FindByValue(data.BANK.ToString()));
            this.cmbBank.Enabled = true;
            this.txtBranch.Text = data.BANKBRANCH;
            this.txtBranch.Enabled = true;
        }
        if (data.PAYMENT == Constz.Payment.Others.Code)
        {
            this.rbtPayment4.Checked = true;
            this.txtReason.Text = data.REASON;
            this.txtReason.Enabled = true;

        }
        if (data.REQDATE.Year != 1)
        {
            double day = Convert.ToDouble(SysConfigFlow.GetValue(Constz.ConfigName.PERIOD));
        }

        this.ctlDueDate.DateValue = data.DUEDATE;
        this.txtRemark.Text = data.REMARK;
        this.txtCreateBy.Text = data.CREATEBY;
        this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
        this.txtTotalDiscount.Text = data.TOTDIS.ToString(Constz.DblFormat);
        this.txtVat.Text = data.VAT.ToString();
        this.txtTotalVat.Text = data.TOTVAT.ToString(Constz.DblFormat);
        this.txtGrandTotal.Text = data.GRANDTOT.ToString(Constz.DblFormat);
        this.txtWareHouse.Text = data.WAREHOUSE.ToString();
        this.txtNet.Text = data.GRANDTOT.ToString(Constz.IntFormat);
        SetCustomerData(data.CUSTOMER, false);
        this.txtOther.Text = data.OTHER;

        this.txtPopup.Text = FlowObj.GetUsedProduct(data.LOID);
        this.txtNewPopup.Text = "";
        //this.txtNewPopup.Text = FlowObj.GetUsedProduct(data.LOID);

        SetGrvItem(data.STATUS);

        if (data.STATUS == Constz.Requisition.Status.Approved.Code || data.STATUS == Constz.Requisition.Status.Finish.Code || data.STATUS == Constz.Requisition.Status.Void.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
            this.btnSearch.Visible = false;
        }

        //if (this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
        //{
        //    CalculateDiscount();
        //}

        if (data.STATUS == Constz.Requisition.Status.Reserve.Code)
        {
            this.btnSearch.Visible = false;
        }

        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.InvoiceFull, data.LOID) + " return false;";
    }

    private ProductReserveData GetData()
    {
        ProductReserveData data = new ProductReserveData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.REFNO = this.txtReference.Text.Trim();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CODE = this.txtRefNo.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        data.CLASTNAME = this.txtLastName.Text.Trim();
        data.CNAME = this.txtName.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CTITLE = Convert.ToDouble(this.cmbTitle.SelectedItem.Value);
        data.PAYMENT = this.cmbCondition.SelectedValue;
        data.CUSTOMER = Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text);
        data.DUEDATE = this.ctlDueDate.DateValue;
        data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
        data.ITEM = ItemObj.GetItemList();
        data.REMARK = this.txtRemark.Text.Trim();
        data.REQDATE = this.ctlReqDate.DateValue;
        data.RESERVEDATE = this.ctlRecieveDate.DateValue;
        data.REFTYPELOID = Convert.ToDouble(this.cmbRefType.SelectedItem.Value);
        data.REFTYPETABLE = FlowObj.GetRefTypeTable(data.REFTYPELOID);
        data.STATUS = this.txtStatus.Text.Trim();
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.TOTDIS = Convert.ToDouble(this.txtTotalDiscount.Text == "" ? "0" : this.txtTotalDiscount.Text);
        data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
        data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
        data.WAREHOUSE = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);
        data.CDELIVERY = this.cmbDelivery.SelectedValue;
        data.OTHER = this.txtOther.Text.Trim();
        data.INVCODE = this.txtInvCode.Text.Trim();
        data.CREDITDATE = this.ctlCreditDay.DateValue;
        data.PAYMENTCONDITION = (this.cmbCondition.SelectedItem.Value == "0" ? "" : this.cmbCondition.SelectedItem.Text);
        //  data.REQUISITIONTYPE = Convert.ToDouble(this.cmbRequisitionType.SelectedItem.Value);


        if (this.rbtPayment1.Checked == true)
        {
            data.PAYMENT = Constz.Payment.Cash.Code;
        }
        if (this.rbtPayment2.Checked == true)
        {
            data.PAYMENT = Constz.Payment.CreditCard.Code;
            data.CREDITTYPE = Convert.ToDouble(this.cmbCreditType.SelectedValue);
            data.CREDITCARDID = this.txtCreditID.Text.Trim();
        }
        if (this.rbtPayment3.Checked == true)
        {
            data.PAYMENT = Constz.Payment.Cheque.Code;
            data.CHEQUE = this.txtCheque.Text.Trim();
            data.CHEQUEDATE = this.ctlChequeDate.DateValue;
            data.BANK = Convert.ToDouble(this.cmbBank.SelectedValue);
            data.BANKBRANCH = this.txtBranch.Text.Trim();
        }
        if (this.rbtPayment4.Checked == true)
        {
            data.PAYMENT = Constz.Payment.Others.Code;
            data.REASON = this.txtReason.Text.Trim();
        }

        return data;
    }

    #endregion

    #region GridView

    private void SetProductDetail(ProductSaleData data, GridViewRow gRow, TextBox txtBarcode, DropDownList cmbProduct, TextBox txtUnit, Label lblUnitName, TextBox txtPrice,
        TextBox txtNetPrice, TextBox txtQty, Label lblNormalDiscount, Label lblIsVat)
    {
        txtBarcode.Text = data.BARCODE;
        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.PRODUCT.ToString()));
        txtUnit.Text = data.UNIT.ToString();
        lblUnitName.Text = data.UNITNAME;
        txtPrice.Text = SaleObj.CalculateUnitPrice(data.UNITPRICE, Convert.ToDouble(this.txtVat.Text), data.ISVAT).ToString(Constz.DblFormat);
        txtNetPrice.Text = SaleObj.CalcucateProductTotalItem(Convert.ToDouble(txtPrice.Text), Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text), 0).ToString(Constz.DblFormat);
        lblNormalDiscount.Text = data.DISCOUNT.ToString(Constz.DblFormat);
        lblIsVat.Text = data.ISVAT;

        //if (txtBarcode.ID == "txtNewBarCode" && txtBarcode.Text != "")
        //    InsertData(gRow);
    }

    private void txtBarcode_TextChanged(TextBox txtBarcode, GridViewRow gRow, string cmbProductName, string txtQtyName, string txtUnitName, string lblUnitName,
        string txtPriceName, string txtNetPriceName, string lblNormalDiscountName, string lblIsVatName)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[indexPRODUCT].FindControl(cmbProductName);
        TextBox txtQty = (TextBox)gRow.Cells[indexQTY].FindControl(txtQtyName);
        Label lblUnit = (Label)gRow.Cells[indexUNIT].FindControl(lblUnitName);
        TextBox txtUnit = (TextBox)gRow.Cells[indexUNIT].FindControl(txtUnitName);
        TextBox txtPrice = (TextBox)gRow.Cells[indexPRICE].FindControl(txtPriceName);
        TextBox txtNetPrice = (TextBox)gRow.Cells[indexNETPRICE].FindControl(txtNetPriceName);
        Label lblNormalDiscount = (Label)gRow.Cells[indexNORMALDISCOUNT].FindControl(lblNormalDiscountName);
        Label lblIsVat = (Label)gRow.Cells[indexISVAT].FindControl(lblIsVatName);

        ProductSaleData data = SaleObj.GetProductPromotion(txtBarcode.Text.Trim(), Convert.ToDouble(this.txtWareHouse.Text));
        SetProductDetail(data, gRow, txtBarcode, cmbProduct, txtUnit, lblUnit, txtPrice, txtNetPrice, txtQty, lblNormalDiscount, lblIsVat);
    }

    private void cmbProduct_SelectedIndexChanged(DropDownList cmbProduct, GridViewRow gRow, string txtBarcodeName, string txtQtyName, string txtUnitName, string lblUnitName,
        string txtPriceName, string txtNetPriceName, string lblNormalDiscountName, string lblIsVatName)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[indexBARCODE].FindControl(txtBarcodeName);
        TextBox txtQty = (TextBox)gRow.Cells[indexQTY].FindControl(txtQtyName);
        Label lblUnit = (Label)gRow.Cells[indexUNIT].FindControl(lblUnitName);
        TextBox txtUnit = (TextBox)gRow.Cells[indexUNIT].FindControl(txtUnitName);
        TextBox txtPrice = (TextBox)gRow.Cells[indexPRICE].FindControl(txtPriceName);
        TextBox txtNetPrice = (TextBox)gRow.Cells[indexNETPRICE].FindControl(txtNetPriceName);
        Label lblNormalDiscount = (Label)gRow.Cells[indexNORMALDISCOUNT].FindControl(lblNormalDiscountName);
        Label lblIsVat = (Label)gRow.Cells[indexISVAT].FindControl(lblIsVatName);

        ProductSaleData data = SaleObj.GetProductPromotion(Convert.ToDouble(cmbProduct.SelectedItem.Value), Convert.ToDouble(this.txtWareHouse.Text));
        SetProductDetail(data, gRow, txtBarcode, cmbProduct, txtUnit, lblUnit, txtPrice, txtNetPrice, txtQty, lblNormalDiscount, lblIsVat);
    }

    private void InsertData(GridViewRow gRow)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[indexPRODUCT].FindControl("cmbNewProduct");
        TextBox txtBarcode = (TextBox)gRow.Cells[indexBARCODE].FindControl("txtNewBarcode");
        TextBox txtQty = (TextBox)gRow.Cells[indexQTY].FindControl("txtNewQty");
        Label lblUnit = (Label)gRow.Cells[indexUNIT].FindControl("lblNewUnit");
        TextBox txtUnit = (TextBox)gRow.Cells[indexUNIT].FindControl("txtNewUnit");
        TextBox txtPrice = (TextBox)gRow.Cells[indexPRICE].FindControl("txtNewPrice");
        TextBox txtNetPrice = (TextBox)gRow.Cells[indexNETPRICE].FindControl("txtNewNetPrice");
        Label lblNormalDiscount = (Label)gRow.Cells[indexNORMALDISCOUNT].FindControl("lblNewNormalDiscount");
        Label lblIsVat = (Label)gRow.Cells[indexISVAT].FindControl("lblNewIsVat");

        RequisitionItemData data = new RequisitionItemData();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.DISCOUNT = Convert.ToDouble(lblNormalDiscount.Text == "" ? "0" : lblNormalDiscount.Text);
        data.PRICE = Convert.ToDouble(txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.ProductName = cmbProduct.SelectedItem.Text;
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
        data.UnitName = lblUnit.Text.Trim();
        data.NETPRICE = Convert.ToDouble(txtNetPrice.Text); ;
        data.BarCode = txtBarcode.Text;
        data.ISVAT = lblIsVat.Text;

        if (ItemObj.InsertRequisitionItem(data))
        {
            SetGrvItem(this.txtStatus.Text);
            CalculateDiscount();
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void NewRowDataBound(GridViewRow gRow)
    {
        ComboSource.BuildCombo((DropDownList)gRow.Cells[indexPRODUCT].FindControl("cmbNewProduct"), "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "", "เลือก", "0");
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[indexQTY].FindControl("txtNewQty"));
        string script = "document.getElementById('" + ((TextBox)gRow.Cells[indexNETPRICE].FindControl("txtNewNetPrice")).ClientID + "').value = ";
        script += "parseFloat(document.getElementById('" + ((TextBox)gRow.Cells[indexQTY].FindControl("txtNewQty")).ClientID + "').value) * ";
        script += "parseFloat(document.getElementById('" + ((TextBox)gRow.Cells[indexPRICE].FindControl("txtNewPrice")).ClientID + "').value); document.getElementById('" + ((TextBox)gRow.Cells[indexBARCODE].FindControl("txtNewBarCode")).ClientID + "').focus() ";
        ((TextBox)gRow.Cells[indexQTY].FindControl("txtNewQty")).Attributes.Add("onchange", script);
    }

    #endregion

    #endregion

    #region Event

    #region Others

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbRefType, "V_REQTYPE_INVOICE", "NAME", "LOID", "NAME", "");
            ComboSource.BuildCombo(this.cmbTitle, "TITLE", "NAME", "LOID", "NAME", "");
            ComboSource.BuildCombo(this.cmbCreditType, "CREDITCARD", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ComboSource.BuildCombo(this.cmbBank, "BANK", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ControlUtil.SetIntTextBox(this.txtDiscount);
            ControlUtil.SetIntTextBox(this.txtVat);
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            // this.txtStatus.Text = Constz.Requisition.Status.Approved.Code;
            //this.txtCreateBy.Text = Authz.CurrentUserInfo.UserID;

            string scriptRefNo = "";
            scriptRefNo += "document.getElementById('" + this.txtRefLoid.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/ReserveProduct.aspx?code=' + document.getElementById('" + this.txtRefNo.ClientID + "').value+'&popup=' + document.getElementById('" + this.txtPopup.ClientID + "').value+'&requsitiontype='+document.getElementById('" + this.cmbRefType.ClientID + "').value+'&customer=' + document.getElementById('" + this.txtCustomer.ClientID + "').value, '600', '550');";
            scriptRefNo += "if ('undefined' ==  document.getElementById('" + this.txtRefLoid.ClientID + "').value || '' == document.getElementById('" + this.txtRefLoid.ClientID + "').value) ";
            scriptRefNo += "{ return false; }";

            this.btnSearch.OnClientClick = scriptRefNo;
            //this.btnSearchCust.OnClientClick = script;
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันส่งคลังสำเร็จรูป?');";
        }

    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {

        foreach (GridViewRow row in grvItem.Rows)
        {
            double PRODUCT = Convert.ToDouble(((Label)row.Cells[indexPRODUCT].FindControl("lblProduct")).Text);
            double PRICE = Convert.ToDouble(((Label)row.Cells[indexPRICE].FindControl("lblPriceView")).Text);
            double QTY = Convert.ToDouble(((TextBox)row.Cells[indexQTY].FindControl("txtQty")).Text);
            ItemObj.UpdateItem(PRODUCT, QTY, SaleObj.CalcucateProductTotalItem(PRICE, QTY, 0));

        }

        txtNewPopup.Text = "";
        string[] var = txtRefLoid.Text.Split('/');
        cmbRefType.SelectedValue = var[0];
        txtRequisitionType.Text = var[0];
        txtCustomer.Text = var[1];
        txtNewPopup.Text = var[2];

        if (txtPopup.Text != txtNewPopup.Text)
            txtPopup.Text += ((txtPopup.Text == "" || txtNewPopup.Text == "") ? "" : ",") + txtNewPopup.Text;

        SetCustomerData(Convert.ToDouble(txtCustomer.Text), true);
        SetGrvItem(txtStatus.Text, true);
        txtNewBind.Text = "0";
        CalculateDiscount();
    }


    protected void rbtPayment1_CheckedChanged(object sender, EventArgs e)
    {
        ClearPayment();
        rbtPayment1.Checked = true;
    }

    protected void rbtPayment2_CheckedChanged(object sender, EventArgs e)
    {
        ClearPayment();
        rbtPayment2.Checked = true;
        cmbCreditType.Enabled = true;
        txtCreditID.Enabled = true;
    }

    protected void rbtPayment3_CheckedChanged(object sender, EventArgs e)
    {
        ClearPayment();
        rbtPayment3.Checked = true;
        txtCheque.Enabled = true;
        ctlChequeDate.Enabled = true;
        cmbBank.Enabled = true;
        txtBranch.Enabled = true;
    }

    protected void rbtPayment4_CheckedChanged(object sender, EventArgs e)
    {
        ClearPayment();
        rbtPayment4.Checked = true;
        txtReason.Enabled = true;
    }

    protected void txtRefNo_TextChanged(object sender, EventArgs e)
    {
        //if (FlowObj.RequisitionLOID(this.txtRefNo.Text) != 0)
        //{
        ResetState(FlowObj.RequisitionLOID(this.txtRefNo.Text));
        //}
    }

    protected void btnSearchCust_Click(object sender, ImageClickEventArgs e)
    {
        SetCustomerData(Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text), true);
        CalculateDiscount();

    }

    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        CalculateDiscount();
    }

    protected void txtVat_TextChanged(object sender, EventArgs e)
    {
        CalculateDiscount();
    }

    #endregion

    #region grvItem "Insert"

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NewRowDataBound(e.Row);
        }
    }

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvItemNew.Rows[0]);
        }
    }

    protected void txtNewBarCode_TextChanged(object sender, EventArgs e)
    {
        txtBarcode_TextChanged((TextBox)sender, this.grvItemNew.Rows[0], "cmbNewProduct", "txtNewQty", "txtNewUnit", "lblNewUnit", "txtNewPrice", "txtNewNetPrice", "lblNewNormalDiscount", "lblNewIsVat");
    }

    protected void cmbNewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbProduct_SelectedIndexChanged((DropDownList)sender, this.grvItemNew.Rows[0], "txtNewBarCode", "txtNewQty", "txtNewUnit", "lblNewUnit", "txtNewPrice", "txtNewNetPrice", "lblNewNormalDiscount", "lblNewIsVat");
    }

    #endregion

    #region grvItem

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvItem.FooterRow);
        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[indexPRODUCT].FindControl("cmbProduct");
                ComboSource.BuildCombo(cmbProduct, "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "");
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));

                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[indexQTY].FindControl("txtQty"));
                string script = "document.getElementById('" + ((TextBox)e.Row.Cells[indexNETPRICE].FindControl("txtNetPrice")).ClientID + "').value = ";
                script += "parseFloat(document.getElementById('" + ((TextBox)e.Row.Cells[indexQTY].FindControl("txtQty")).ClientID + "').value) * ";
                script += "parseFloat(document.getElementById('" + ((TextBox)e.Row.Cells[indexPRICE].FindControl("txtPrice")).ClientID + "').value)";
                ((TextBox)e.Row.Cells[indexQTY].FindControl("txtQty")).Attributes.Add("onchange", script);
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[indexBUTTON].FindControl("imbDelete");
                imbDelete.OnClientClick = "return confirm('ต้องการลบรายการสินค้า " + drow["PRODUCTNAME"].ToString() + " ใช่หรือไม่ ?')";
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            NewRowDataBound(e.Row);
        }
    }

    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txtBarcode = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txtBarcode.Parent.Parent).RowIndex;
        txtBarcode_TextChanged(txtBarcode, this.grvItem.Rows[rowIndex], "cmbProduct", "txtQty", "txtUnit", "lblUnit", "txtPrice", "txtNetPrice", "lblNormalDiscount", "lblIsVat");
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmbProduct = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmbProduct.Parent.Parent).RowIndex;
        cmbProduct_SelectedIndexChanged(cmbProduct, this.grvItem.Rows[rowIndex], "txtBarcode", "txtQty", "txtUnit", "lblUnit", "txtPrice", "txtNetPrice", "lblNormalDiscount", "lblIsVat");
    }

    protected void txtNewBarCode_TextChanged1(object sender, EventArgs e)
    {
        TextBox txtBarcode = (TextBox)sender;
        txtBarcode_TextChanged(txtBarcode, this.grvItem.FooterRow, "cmbNewProduct", "txtNewQty", "txtNewUnit", "lblNewUnit", "txtNewPrice", "txtNewNetPrice", "lblNewNormalDiscount", "lblNewIsVat");
    }

    protected void cmbNewProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmbProduct = (DropDownList)sender;
        cmbProduct_SelectedIndexChanged(cmbProduct, this.grvItem.FooterRow, "txtNewBarcode", "txtNewQty", "txtNewUnit", "lblNewUnit", "txtNewPrice", "txtNewNetPrice", "lblNewNormalDiscount", "lblNewIsVat");
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        double PRODUCT = Convert.ToDouble(((Label)this.grvItem.Rows[rowIndex].Cells[indexPRODUCT].FindControl("lblProduct")).Text);
        double PRICE = Convert.ToDouble(((Label)this.grvItem.Rows[rowIndex].Cells[indexPRICE].FindControl("lblPriceView")).Text);
        double QTY = Convert.ToDouble(txt.Text);
        ItemObj.UpdateItem(PRODUCT, QTY, SaleObj.CalcucateProductTotalItem(PRICE, QTY, 0));

        CalculateDiscount();
        this.grvItem.DataBind();
    }

    protected void grvItem_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        e.ExceptionHandled = (e.Exception != null);
        if (e.ExceptionHandled)
        {
            e.KeepInEditMode = true;
            Appz.ClientAlert(this, e.Exception.InnerException.Message);
        }
        else
        {
            CalculateDiscount();
        }
    }

    protected void grvItem_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.ExceptionHandled)
        {
            Appz.ClientAlert(this, e.Exception.Message);
        }
        else
        {
            if (grvItem.Rows.Count > 1)
            {
                SetGrvItem(this.txtStatus.Text);
                CalculateDiscount();
                this.txtPopup.Text = "";

                foreach (GridViewRow row in grvItem.Rows)
                {
                    this.txtPopup.Text += (this.txtPopup.Text == "" ? "" : ",") + row.Cells[10].Text;
                }
            }
            else
            {
                this.txtNewPopup.Text = "0";
                //SetGrvItem(this.txtStatus.Text);
            }
        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gRow = this.grvItem.Rows[e.RowIndex];
        DropDownList cmbProduct = (DropDownList)gRow.Cells[indexPRODUCT].FindControl("cmbProduct");
        TextBox txtBarcode = (TextBox)gRow.Cells[indexBARCODE].FindControl("txtBarcode");
        TextBox txtQty = (TextBox)gRow.Cells[indexQTY].FindControl("txtQty");
        Label lblUnit = (Label)gRow.Cells[indexUNIT].FindControl("lblUnit");
        TextBox txtUnit = (TextBox)gRow.Cells[indexUNIT].FindControl("txtUnit");
        TextBox txtPrice = (TextBox)gRow.Cells[indexPRICE].FindControl("txtPrice");
        TextBox txtNetPrice = (TextBox)gRow.Cells[indexNETPRICE].FindControl("txtNetPrice");
        Label lblNormalDiscount = (Label)gRow.Cells[indexNORMALDISCOUNT].FindControl("lblNormalDiscount");
        Label lblIsVat = (Label)gRow.Cells[indexISVAT].FindControl("lblIsVat");

        RequisitionItemData data = new RequisitionItemData();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.PRICE = Convert.ToDouble(txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.ProductName = cmbProduct.SelectedItem.Text;
        data.DISCOUNT = Convert.ToDouble(lblNormalDiscount.Text == "" ? "0" : lblNormalDiscount.Text);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
        data.UnitName = lblUnit.Text.Trim();
        data.NETPRICE = Convert.ToDouble(txtNetPrice.Text == "" ? "0" : txtNetPrice.Text);
        data.ISVAT = lblIsVat.Text.Trim();
        data.BarCode = txtBarcode.Text.Trim();

        e.NewValues["LOID"] = (this.grvItem.Rows[e.RowIndex].Cells[indexLOID].Text == "" ? "0" : this.grvItem.Rows[e.RowIndex].Cells[indexLOID].Text);
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["UNIT"] = data.UNIT.ToString();
        e.NewValues["PRICE"] = data.PRICE.ToString();
        e.NewValues["NORMALDISCOUNT"] = data.DISCOUNT.ToString();
        e.NewValues["NETPRICE"] = data.NETPRICE.ToString();
        e.NewValues["ISVAT"] = data.ISVAT;
        e.NewValues["BARCODE"] = data.BarCode;
        e.NewValues["RANK"] = 0;
        e.NewValues["PRODUCTNAME"] = data.ProductName;
        e.NewValues["UNITNAME"] = data.UnitName;
    }

    #endregion

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductInvoiceSearch.aspx");
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

    protected void SubmitClick(object sender, EventArgs e)
    {
        ProductReserveData data = GetData();
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ส่งคลังสำเร็จรูปเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }
    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
    }

    #endregion

    #endregion

}

