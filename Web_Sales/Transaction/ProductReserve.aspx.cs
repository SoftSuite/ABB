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

public partial class Transaction_ProductReserve : System.Web.UI.Page
{
    #region Variables & Properties

    private ProductReserveFlow _flow;
    private SaleFlow _sFlow;
    private RequisitionItemReserve item;
    private int indexCHECk = 0;
    private int indexRANK = 1;
    private int indexBARCODE = 2;
    private int indexPRODUCTNAME = 3;
    private int indexQTY = 4;
    private int indexUNIT = 5;
    private int indexPRICE = 6;
    private int indexNETPRICE = 7;
    private int indexSTOCKQTY= 8;
    private int indexNORMALDISCOUNT = 9;
    private int indexISVAT = 10;
    private int indexLOID = 11;
    private int indexPRODUCT = 12;

    private ProductReserveFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductReserveFlow(); return _flow; }
    }

    private SaleFlow SaleObj
    {
        get { if (_sFlow == null) _sFlow = new SaleFlow(); return _sFlow; }
    }

    private RequisitionItemReserve ItemObj
    {
        get { if (item == null) item = new RequisitionItemReserve(); return item; }
    }

    #endregion

    #region Methods

    #region Data

    private void SetData(ProductReserveData data)
    {
        if (data.LOID != 0)
            this.cmbRequisitionType.Enabled = false;
        else
            this.cmbRequisitionType.Enabled = true;
        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtCustomer.Text = data.CUSTOMER.ToString();

        this.cmbRequisitionType.SelectedIndex = this.cmbRequisitionType.Items.IndexOf(this.cmbRequisitionType.Items.FindByValue(data.REQUISITIONTYPE.ToString()));
        //print
        //this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductReserve, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        if(data.REQUISITIONTYPE.ToString() == Constz.Requisition.RequisitionType.REQ01.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductReserve, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        else if(data.REQUISITIONTYPE.ToString() == Constz.Requisition.RequisitionType.REQ02.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductReserveSale, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        else if(data.REQUISITIONTYPE.ToString() == Constz.Requisition.RequisitionType.REQ03.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductRequestInShop, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        else if(data.REQUISITIONTYPE.ToString() == Constz.Requisition.RequisitionType.REQ10.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductRequestInShop, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";

        this.txtCustomerCode.Text = "";
        this.txtCustomerName.Text = "";
        this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.CTITLE.ToString()));
        this.txtName.Text = data.CNAME;
        this.txtLastName.Text = data.CLASTNAME;
        this.txtAddress.Text = data.CADDRESS;
        this.txtTel.Text = data.CTEL;
        this.txtFax.Text = data.CFAX;
        this.txtRequisitionCode.Text = data.CODE;
        this.ctlReserveDate.DateValue = data.RESERVEDATE;
        if (data.RESERVEDATE.Year != 1)
        {
            double day = Convert.ToDouble(SysConfigFlow.GetValue(Constz.ConfigName.PERIOD));
            this.ctlConfirmDate.DateValue = data.RESERVEDATE.AddDays(day);
        }
        this.ctlDueDate.DateValue = data.DUEDATE;
        this.txtRemark.Text = data.REMARK;
        this.txtCreateBy.Text = data.CREATEBY;
        this.txtStatusName.Text = Appz.GetStatusName(data.STATUS);
        this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
        this.txtTotalDiscount.Text = data.TOTDIS.ToString(Constz.DblFormat);
        this.txtVat.Text = data.VAT.ToString();
        this.txtTotalVat.Text = data.TOTVAT.ToString(Constz.DblFormat);
        this.txtGrandTotal.Text = data.GRANDTOT.ToString(Constz.DblFormat);
        this.txtWareHouse.Text = data.WAREHOUSE.ToString();
        this.txtNet.Text = data.GRANDTOT.ToString(Constz.IntFormat);
        SetCustomerData(data.CUSTOMER, false);

        SetGrvItem(data.STATUS);

        if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
        {
            this.txtCustomerCode.ReadOnly = true;
            this.txtCustomerCode.CssClass = "zTextbox-View";
            this.btnSearch.Visible = false;
            this.txtDiscount.ReadOnly = true;
            this.txtDiscount.CssClass = "zTextboxR-View";
            this.txtVat.ReadOnly = true;
            this.txtVat.CssClass = "zTextboxR-View";
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
            this.ctlDueDate.Enabled = false;
        }
        this.grvItem.Columns[indexCHECk].Visible = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
        this.ctlToolbarItem.Visible = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
    }

    private ProductReserveData GetData()
    {
        ProductReserveData data = new ProductReserveData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        data.CLASTNAME = this.txtLastName.Text.Trim();
        data.CNAME = this.txtName.Text.Trim();
        data.CODE = this.txtRequisitionCode.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CTITLE = Convert.ToDouble(this.cmbTitle.SelectedItem.Value);
        data.CUSTOMER = Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text);
        data.DUEDATE = this.ctlDueDate.DateValue;
        data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
        data.ITEM = ItemObj.GetItemList();
        data.REMARK = this.txtRemark.Text.Trim();
        data.RESERVEDATE = this.ctlReserveDate.DateValue;
        data.REQUISITIONTYPE = Convert.ToDouble(this.cmbRequisitionType.SelectedItem.Value);
        data.STATUS = this.txtStatus.Text.Trim();
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.TOTDIS = Convert.ToDouble(this.txtTotalDiscount.Text == "" ? "0" : this.txtTotalDiscount.Text);
        data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
        data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
        data.WAREHOUSE = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);
        return data;
    }

    #endregion

    #region Others

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvItem.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvItem.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvItem.Rows[i].Cells[indexCHECk].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvItem.Rows[i].Cells[indexLOID].Text)); }
        }
        return arrLOID;
    }

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
        this.grvItem.DataBind();
        this.txtSelectProduct.Text = "";
        foreach (GridViewRow gRow in this.grvItem.Rows)
        {
            this.txtSelectProduct.Text += (this.txtSelectProduct.Text == "" ? "" : ", ") + gRow.Cells[indexPRODUCT].Text;
        }
    }

    private void SetCustomerData(double customer, bool isSearch)
    {
        CustomerSaleData data = SaleObj.GetCustomerData(customer);
        this.txtOldCustomerCode.Text = data.CODE;
        this.txtCustomerCode.Text = data.CODE;
        this.txtCustomerName.Text = data.CUSTOMERNAME;
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

    private void SetCustomerData(string customerCode, bool isSearch)
    {
        CustomerSaleData data = SaleObj.GetCustomerData(customerCode);
        this.txtCustomer.Text = data.CUSTOMER.ToString();
        this.txtOldCustomerCode.Text = data.CODE;
        this.txtCustomerCode.Text = data.CODE;
        this.txtCustomerName.Text = data.CUSTOMERNAME;
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
        ProductReserveData data = FlowObj.GetData(loid);
        if (loid == 0)
        {
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.CODE = "";
            data.RESERVEDATE = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
            data.VAT = Convert.ToDouble(SysConfigFlow.GetValue(Constz.ConfigName.VAT));
        }
        SetData(data);
    }

    #endregion

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string script = "";
            script += "document.getElementById('" + this.txtProduct.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/Product.aspx?product=' + document.getElementById('" + this.txtSelectProduct.ClientID + "').value, '600', '450');";
            script += "if ('undefined' !=  document.getElementById('" + this.txtProduct.ClientID + "').value && '' != document.getElementById('" + this.txtProduct.ClientID + "').value) return true; else {;  ";
            script += "document.getElementById('" + this.txtProduct.ClientID + "').value = ''; return false; }";
            this.ctlToolbarItem.ClientClickNew = script;
            this.ctlToolbarItemBottom.ClientClickNew = script;

            script = "";
            script += "document.getElementById('" + this.txtCustomer.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/Customer.aspx' + (document.getElementById('" + this.txtCustomerCode.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtCustomerCode.ClientID + "').value)), '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + this.txtCustomer.ClientID + "').value || '' == document.getElementById('" + this.txtCustomer.ClientID + "').value) ";
            script += "{ return false; } ";
            this.btnSearch.OnClientClick = script;

            //script = "";
            //script += "if (document.getElementById('" + this.txtCustomerCode.ClientID + "').value != document.getElementById('" + this.txtOldCustomerCode.ClientID + "').value) { return true; } else { return false; }";
            //this.txtCustomerCode.Attributes.Add("onchange", script);

            ComboSource.BuildCombo(this.cmbRequisitionType, "V_REQTYPE_RESERVE", "NAME", "LOID", "NAME", "");
            ComboSource.BuildCombo(this.cmbTitle, "TITLE", "NAME", "LOID", "NAME", "", "เลือก", "0" );
            ControlUtil.SetIntTextBox(this.txtDiscount);
            ControlUtil.SetIntTextBox(this.txtVat);
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));

            if (this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
            {
                CalculateDiscount();
            }
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการสั่งซื้อ/สั่งจอง');";
            this.ctlToolbarItem.ClientClickDelete = "return confirm('ต้องการลบรายการสินค้าที่เลือกใช่หรือไม่?');";
            this.ctlToolbarItemBottom.ClientClickDelete = "return confirm('ต้องการลบรายการสินค้าที่เลือกใช่หรือไม่?');";
        }

    }

    protected void txtCustomerCode_TextChanged(object sender, EventArgs e)
    {
        string customerCode = this.txtCustomerCode.Text.Trim();
        SetCustomerData(this.txtCustomerCode.Text.Trim(), true);
        if (this.txtCustomerCode.Text == "" && customerCode != "")
        {
            this.txtCustomerCode.Text = customerCode;
            this.txtOldCustomerCode.Text = customerCode;
            Appz.ClientAlert(this, "ไม่พบลูกค้าตามรหัสที่ระบุ");
        }
        CalculateDiscount();
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
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

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductReserveSearch.aspx");
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

    protected void SubmitClick(object sender, EventArgs e)
    {
        ProductReserveData data = GetData();
        if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ยืนยันการสั่งซื้อ/สั่งจองแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    #endregion

    #region ToolbarItem

    protected void ItemNewClick(object sender, EventArgs e)
    {
        if (this.txtProduct.Text != "")
        {
            DataTable dt = SaleObj.GetProductPromotionList(this.txtProduct.Text, Convert.ToDouble(this.txtWareHouse.Text));
            ItemObj.InsertRequisitionItem(dt);
            CalculateDiscount();
            SetGrvItem(this.txtStatus.Text);
        }
    }

    protected void ItemDeleteClick(object sender, EventArgs e)
    {
        ItemObj.DeleteRequisitionItem(GetChecked());
        CalculateDiscount();
        SetGrvItem(this.txtStatus.Text);
    }

    #endregion

    #region grvItem

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[indexCHECk].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtQty = (TextBox)e.Row.Cells[indexQTY].FindControl("txtQty");
            ControlUtil.SetIntTextBox(txtQty);
            if (this.txtStatus.Text != Constz.Requisition.Status.Waiting.Code)
            {
                txtQty.ReadOnly = true;
                txtQty.CssClass="zTextboxR-View";
            }
        }
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        TextBox txtQty = (TextBox)sender;
        ItemObj.UpdateRequisition(Convert.ToDouble(((GridViewRow)txtQty.Parent.Parent).Cells[indexLOID].Text), Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text));
        CalculateDiscount();
    }

    #endregion

    #endregion

}
