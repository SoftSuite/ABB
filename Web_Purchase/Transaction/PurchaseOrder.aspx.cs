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

public partial class Transaction_PurchaseOrder : System.Web.UI.Page
{
    private PurchaseOrderFlow _flow;
    private POItem item;

    public PurchaseOrderFlow FlowObj
    {
        get { if (_flow == null) _flow = new PurchaseOrderFlow(); return _flow; }
    }

    public POItem ItemObj
    {
        get { if (item == null) item = new POItem(); return item; }
    }

    private void Calculation()
    {
        double price = 0;
        double discount = 0;
        double vat = 0;
        foreach (DataRow dRow in ItemObj.GetPOItem(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text), this.txtStatus.Text).Rows)
        {
            double itmPrice = Convert.ToDouble(dRow["NETPRICE"]);
            double itmDiscount = Convert.ToDouble(dRow["DISCOUNT"].ToString() == "" ? "0" : dRow["DISCOUNT"]);
            double vatcal = 0;
            vatcal = Convert.ToDouble(txtVat.Text == "" ? "0" : txtVat.Text);
            string isVat = dRow["ISVAT"].ToString();

            if (isVat == "N")
            {
                price += itmPrice;
                vat += Convert.ToDouble(Convert.ToDouble((itmPrice * vatcal) / 100).ToString(Constz.DblFormat));
            }
            else
            {
                double tempvat = Convert.ToDouble(Convert.ToDouble((itmPrice * vatcal) / (100 + vatcal)).ToString(Constz.DblFormat));
                vat += tempvat;
                price += (itmPrice - tempvat);
            }
            discount += itmDiscount;
        }
        this.txtTotal.Text = price.ToString();
        this.txtTotalVat.Text = vat.ToString();
        this.txtTotalDiscount.Text = discount.ToString();
        double total = price + vat;
        this.txtGrandTotal.Text = total.ToString();
    }

    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        if (grvItem.Rows.Count > 0)
        {
            this.grvItem.ShowFooter = (status == Constz.Requisition.Status.Waiting.Code);
            this.grvItem.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code);
            this.grvItem.Visible = true;
            this.grvItemNew.Visible = false;
        }
        else
        {
            this.grvItem.Visible = (status != Constz.Requisition.Status.Waiting.Code);
            this.grvItemNew.Visible = (status == Constz.Requisition.Status.Waiting.Code);
        }
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        PurchaseOrderData data = FlowObj.GetData(loid);
        if (loid == 0)
        {
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.CODE = "";
            data.ORDERDATE = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.VAT = Convert.ToDouble(SysConfigFlow.GetValue(Constz.ConfigName.VAT));
            data.TYPE = "N";
        }
        SetData(data);
    }

    private void SetREFPO(string LOID)
    {
        DataTable dt = FlowObj.GetPOEditFromLOID(LOID);
        string poold = "";
        if (dt.Rows.Count > 0)
        {
            poold = dt.Rows[0]["POOLD"].ToString();
            this.txtPOEditCode.Text = dt.Rows[0]["CODE"].ToString();

            dt = FlowObj.GetCodeFromPOOLD(poold);
            if (dt.Rows.Count > 0)
            {
                this.txtPOOldCode.Text = dt.Rows[0]["CODE"].ToString();
            }
        }
    }

    private void SetData(PurchaseOrderData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtRefLOID.Text = data.REFLOID.ToString();
        this.txtRefTable.Text = data.REFTABLE;

        this.cmbType.SelectedIndex = cmbType.Items.IndexOf(cmbType.Items.FindByValue(data.TYPE));
        this.cmbSupplier.SelectedIndex = this.cmbSupplier.Items.IndexOf(this.cmbSupplier.Items.FindByValue(data.SUPPLIER.ToString()));
        this.txtCName.Text = data.CNAME;
        this.txtAddress.Text = data.CADDRESS;
        this.txtTel.Text = data.CTEL;
        this.txtFax.Text = data.CFAX;
        this.cmbPaymentType.SelectedIndex = this.cmbPaymentType.Items.IndexOf(this.cmbPaymentType.Items.FindByValue(data.PAYMENTTYPE.ToString()));
        this.txtPaymentDesc.Text = data.PAYMENTDESC;

        this.txtCode.Text = data.CODE;
        this.ctlOrderDate.DateValue = data.ORDERDATE;

        SetREFPO(data.LOID.ToString());

        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.Approved.Code ? Constz.Requisition.Status.Approved.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : Constz.Requisition.Status.Waiting.Name));

        this.txtVat.Text = data.VAT.ToString();
        this.txtTotal.Text = data.TOTAL.ToString();
        this.txtTotalVat.Text = data.TOTVAT.ToString();
        this.txtTotalDiscount.Text = data.TOTDIS.ToString();
        this.txtGrandTotal.Text = data.GRANDTOT.ToString();
        
        this.cmbDelivery.SelectedIndex = this.cmbDelivery.Items.IndexOf(this.cmbDelivery.Items.FindByValue(data.DELIVERY.ToString()));
        this.txtOther.Text = data.OTHER;
        this.txtRemark.Text = data.REMARK;
        
        
        SetGrvItem(data.STATUS);
        Calculation();

        if (data.STATUS == Constz.Requisition.Status.Approved.Code || data.STATUS == Constz.Requisition.Status.Void.Code)
        {
            this.ToolbarControlPO1.BtnSaveShow = false;
            this.ToolbarControlPO1.BtnSubmitShow = false;
            this.ctlOrderDate.Enabled = false;
        }
        if (data.STATUS == Constz.Requisition.Status.Approved.Code)
        {
            this.ToolbarControlPO1.BtnSentShow = true;
        }
        else this.ToolbarControlPO1.BtnSentShow = false;
        this.ToolbarControlPO1.ClientClickPrint = Appz.ReportScript(Constz.Report.PurchaseOrder, data.LOID) + "return false;";
    }

    private PurchaseOrderData GetData()
    {
        PurchaseOrderData data = new PurchaseOrderData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtCode.Text.Trim();
        data.ORDERDATE = this.ctlOrderDate.DateValue;
        data.ORDERTYPE = Constz.OrderType.PO.Code;
        data.SUPPLIER = Convert.ToDouble(this.cmbSupplier.SelectedItem.Value);
        data.CNAME = this.txtCName.Text.Trim();
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        data.PAYMENTTYPE = this.cmbPaymentType.SelectedItem.Value;
        data.PAYMENTDESC = this.txtPaymentDesc.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.REFLOID = Convert.ToDouble(this.txtRefLOID.Text == "" ? "0" : this.txtRefLOID.Text);
        data.REFTABLE = this.txtRefTable.Text;
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.TOTDIS = Convert.ToDouble(this.txtTotalDiscount.Text == "" ? "0" : this.txtTotalDiscount.Text);
        data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
        data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
        data.REFLOID = Convert.ToDouble(this.txtRefLOID.Text == "" ? "0" : this.txtRefLOID.Text);
        data.REFTABLE = this.txtRefTable.Text;
        data.STATUS = this.txtStatus.Text.Trim();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.DELIVERY = this.cmbDelivery.SelectedItem.Value;
        data.OTHER = this.txtOther.Text.Trim();
        data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
        data.TYPE = this.cmbType.SelectedItem.Value;
        data.ITEM = ItemObj.GetItemList();
        
        return data;
    }

    private PurchaseOrderData GetRecentData()
    {
        PurchaseOrderData data = new PurchaseOrderData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtCode.Text.Trim();
        data.ORDERDATE = this.ctlOrderDate.DateValue;
        data.ORDERTYPE = Constz.OrderType.PO.Code;
        data.SUPPLIER = Convert.ToDouble(this.cmbSupplier.SelectedItem.Value);
        data.CNAME = this.txtCName.Text.Trim();
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        data.APPROVER = Authz.CurrentUserInfo.UserID;
        data.APPROVEDATE = DateTime.Now.Date;
        data.REMARK = this.txtRemark.Text.Trim();
        data.REFLOID = Convert.ToDouble(this.txtRefLOID.Text == "" ? "0" : this.txtRefLOID.Text);
        data.REFTABLE = this.txtRefTable.Text;
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.TOTDIS = Convert.ToDouble(this.txtTotalDiscount.Text == "" ? "0" : this.txtTotalDiscount.Text);
        data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
        data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
        data.REFLOID = Convert.ToDouble(this.txtRefLOID.Text == "" ? "0" : this.txtRefLOID.Text);
        data.REFTABLE = this.txtRefTable.Text;
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.DELIVERY = this.cmbDelivery.SelectedItem.Value;
        data.OTHER = this.txtOther.Text.Trim();
        data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
        data.TYPE = this.cmbType.SelectedItem.Value;
        data.PAYMENTTYPE = this.cmbPaymentType.SelectedItem.Value;
        data.PAYMENTDESC = this.txtPaymentDesc.Text.Trim();
        data.ITEM = ItemObj.GetItemList();

        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string scripts = "if (document.getElementById('" + this.txtLOID.ClientID + "').value == '' || document.getElementById('" + this.txtLOID.ClientID + "').value == '0') " +
                "return false; " +
                "else {" +
                "window.open('" + Constz.HomeFolder + "Transaction/PDOrder.aspx?LOID=' + document.getElementById('" + this.txtLOID.ClientID + "').value, 'PPopup','width=550, height=500'); return false;" +
                "}";
            this.ToolbarControlPO1.ClientClickSent = scripts;
            ComboSource.BuildCombo(this.cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", " ACTIVE = 1", "เลือก", "0");
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));

            this.ToolbarControlPO1.ClientClickSubmit = "return confirm('ยืนยันการอนุมัติการจัดซื้อ');";

            if (Request["TYPE"] != null)
            {
                this.cmbType.SelectedIndex = cmbType.Items.IndexOf(cmbType.Items.FindByValue(Request["TYPE"].ToString()));
            }
            if (this.cmbType.SelectedValue == "B" && this.txtStatus.Text != Constz.Requisition.Status.Approved.Code)
            {
                this.ctlOrderDate.Enabled = true;
            }
            else
            {
                this.ctlOrderDate.Enabled = false;
            }
        }
        Session["pdorder"] = Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]);
    }

    protected void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        DataTable dt = FlowObj.GetSupplierData(cmb.SelectedItem.Value);
        if (dt.Rows.Count > 0)
        {
            this.txtCName.Text = dt.Rows[0]["TITLENAME"].ToString() + " " + dt.Rows[0]["CNAME"].ToString() + " " + dt.Rows[0]["CLASTNAME"].ToString();
            this.txtAddress.Text = dt.Rows[0]["CADDRESS"].ToString();// +" " + dt.Rows[0]["CROAD"].ToString() + " " + dt.Rows[0]["TAMBOLNAME"].ToString() + " " + dt.Rows[0]["AMPHURNAME"].ToString() + " " + dt.Rows[0]["PROVINCENAME"].ToString() + " " + dt.Rows[0]["ZIPCODE"].ToString();
            this.txtTel.Text = dt.Rows[0]["CTEL"].ToString();
            this.txtFax.Text = dt.Rows[0]["FAX"].ToString();
            this.cmbPaymentType.SelectedIndex = cmbPaymentType.Items.IndexOf(cmbPaymentType.Items.FindByValue(dt.Rows[0]["PAYMENTYPE"].ToString()));
        }
    }

    protected void cmbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        if (cmb.SelectedValue == "B")
        {
            this.ctlOrderDate.Enabled = true;
        }
        else
        {
            this.ctlOrderDate.Enabled = false;
            this.ctlOrderDate.DateValue = DateTime.Now.Date;
        }
    }

    #region grvItemNew

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int16 rowIndex = 0;
        TextBox txtBarCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewBarCode");
        TextBox txtProduct = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewProduct");
        TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewQty");
        TextBox txtUnit = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewUnit");
        TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewPrice");
        TextBox txtDiscount = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewDiscount");
        TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewNetPrice");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("ctlNewDueDate");
        TextBox txtPRItemCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[10].FindControl("txtNewPRItemCode");
        CheckBox chkVat = (CheckBox)this.grvItemNew.Rows[rowIndex].Cells[11].FindControl("chkVat");

        if (e.CommandName == "Insert")
        {
            POItemData data = new POItemData();
            data.BARCODE = txtBarCode.Text;
            data.PRODUCTNAME = txtProduct.Text;
            data.PRODUCT = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[14].Text);
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.UNITNAME = txtUnit.Text;
            data.UNIT = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[15].Text);
            data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
            data.DISCOUNT = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
            data.NETPRICE = Convert.ToDouble(txtNetPrice.Text == "" ? "0" : txtNetPrice.Text);
            data.DUEDATE = ctlDueDate.DateValue;
            data.PRITEMCODE = txtPRItemCode.Text;
            data.PRITEM = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[16].Text);
            data.PDORDER = Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]);
            if (chkVat.Checked)
            {
                data.ISVAT = "Y";
            }
            else
            {
                data.ISVAT = "N";
            }
            

            if (ItemObj.InsertPOItem(data))
            {
                //this.grvItem.DataBind();
                //this.grvItemNew.DataBind();
                SetGrvItem(this.txtStatus.Text);
                Calculation();
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
        else if (e.CommandName == "Search")
        {
            TextBox txtGetData = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtGetData");
            if (txtGetData.Text != "undefined")
            {
                DataTable dt = FlowObj.GetViewProductPOPopupList(Convert.ToDouble(txtGetData.Text));
                if (dt.Rows.Count > 0)
                {
                    //this.txtDebug.Text = txtGetData.Text;
                    txtBarCode.Text = dt.Rows[0]["BARCODE"].ToString();
                    txtProduct.Text = dt.Rows[0]["PDNAME"].ToString();
                    txtQty.Text = dt.Rows[0]["QTY"].ToString();
                    txtUnit.Text = dt.Rows[0]["UNAME"].ToString();
                    txtPrice.Text = dt.Rows[0]["CURPRICE"].ToString();
                    ctlDueDate.DateValue = Convert.ToDateTime(dt.Rows[0]["DUEDATE"]);
                    txtPRItemCode.Text = dt.Rows[0]["PRCODE"].ToString();
                    this.grvItemNew.Rows[rowIndex].Cells[13].Text = dt.Rows[0]["ISVAT"].ToString();
                    this.grvItemNew.Rows[rowIndex].Cells[14].Text = dt.Rows[0]["LOID"].ToString();
                    this.grvItemNew.Rows[rowIndex].Cells[15].Text = dt.Rows[0]["UNIT"].ToString();
                    this.grvItemNew.Rows[rowIndex].Cells[16].Text = dt.Rows[0]["PRITEM"].ToString();

                    if (dt.Rows[0]["ISVAT"].ToString() == "Y")
                    {
                        chkVat.Checked = true;
                    }
                    else
                    {
                        chkVat.Checked = false;
                    }

                    double qty = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
                    double price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
                    double discount = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
                    double ans = (qty * price) - discount;

                    txtNetPrice.Text = ans.ToString();
                }
            }
        }
    }

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnNewSearch = (ImageButton)e.Row.FindControl("btnNewSearch");
            TextBox txtGetData = (TextBox)e.Row.Cells[2].FindControl("txtGetData");
            Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)e.Row.Cells[9].FindControl("ctlNewDueDate");
            TextBox txtPRItemCode = (TextBox)e.Row.Cells[10].FindControl("txtNewPRItemCode");

            string script = "";
            script += "document.getElementById('" + txtGetData.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupProductPRSearch.aspx', '600', '550');";

            btnNewSearch.OnClientClick = script;
        }
    }

    protected void txtNewNetPrice_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;

        TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewQty");
        TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewPrice");
        TextBox txtDiscount = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewDiscount");
        TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewNetPrice");

        double qty = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        double price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        double discount = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
        double ans = (qty * price) - discount;

        txtNetPrice.Text = ans.ToString();
    }

    #endregion

    #region grvItem

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ImageButton img = (ImageButton)e.CommandSource;
        Int16 rowIndex = (Int16)((GridViewRow)img.Parent.Parent).RowIndex;
        GridViewRow commandRow;
        if (rowIndex >= 0)
        {
            commandRow = this.grvItem.Rows[rowIndex];
        }
        else
        {
            commandRow = this.grvItem.FooterRow;
        }
        TextBox txtBarCode = (TextBox)commandRow.Cells[2].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtBarCode" : "txtNewBarCode");
        TextBox txtProduct = (TextBox)commandRow.Cells[3].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtProduct" : "txtNewProduct");
        TextBox txtQty = (TextBox)commandRow.Cells[4].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtQty" : "txtNewQty");
        TextBox txtUnit = (TextBox)commandRow.Cells[5].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtUnit" : "txtNewUnit");
        TextBox txtPrice = (TextBox)commandRow.Cells[6].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtPrice" : "txtNewPrice");
        TextBox txtDiscount = (TextBox)commandRow.Cells[7].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtDiscount" : "txtNewDiscount");
        TextBox txtNetPrice = (TextBox)commandRow.Cells[8].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtNetPrice" : "txtNewNetPrice");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)commandRow.Cells[9].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "ctlDueDate" : "ctlNewDueDate");
        TextBox txtPRItemCode = (TextBox)commandRow.Cells[10].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtPRItemCode" : "txtNewPRItemCode");
        CheckBox chkVat = (CheckBox)commandRow.Cells[11].FindControl("chkVat");

        if (e.CommandName == "Insert")
        {
            POItemData data = new POItemData();

            data.BARCODE = txtBarCode.Text;
            data.PRODUCTNAME = txtProduct.Text;
            try
            {
                data.PRODUCT = Convert.ToDouble(commandRow.Cells[14].Text);
            }
            catch(Exception)
            {
                data.PRODUCT = 0;
            }
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.UNITNAME = txtUnit.Text;
            try
            {
                data.UNIT = Convert.ToDouble(commandRow.Cells[15].Text);
            }
            catch (Exception)
            {
                data.UNIT = 0;
            }
            data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
            data.DISCOUNT = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
            data.NETPRICE = Convert.ToDouble(txtNetPrice.Text == "" ? "0" : txtNetPrice.Text);
            data.DUEDATE = ctlDueDate.DateValue;
            data.PRITEMCODE = txtPRItemCode.Text;
            data.PDORDER = Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]);
            try
            {
                data.PRITEM = Convert.ToDouble(commandRow.Cells[16].Text);
            }
            catch (Exception)
            {
                data.PRITEM = 0;
            }
            if (chkVat.Checked)
            {
                data.ISVAT = "Y";
            }
            else
            {
                data.ISVAT = "N";
            }

            if (ItemObj.InsertPOItem(data))
            {
                SetGrvItem(this.txtStatus.Text);
                Calculation();
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
        else if (e.CommandName == "Search")
        {
            TextBox txtGetData = (TextBox)commandRow.Cells[2].FindControl("txtGetData");
            DataTable dt = FlowObj.GetViewProductPOPopupList(Convert.ToDouble(txtGetData.Text));
            if (dt.Rows.Count > 0)
            {
                //this.txtDebug.Text = txtGetData.Text;
                txtBarCode.Text = dt.Rows[0]["BARCODE"].ToString();
                txtProduct.Text = dt.Rows[0]["PDNAME"].ToString();
                txtQty.Text = dt.Rows[0]["QTY"].ToString();
                txtUnit.Text = dt.Rows[0]["UNAME"].ToString();
                txtPrice.Text = dt.Rows[0]["CURPRICE"].ToString();
                ctlDueDate.DateValue = Convert.ToDateTime(dt.Rows[0]["DUEDATE"]);
                txtPRItemCode.Text = dt.Rows[0]["PRCODE"].ToString();
                commandRow.Cells[13].Text = dt.Rows[0]["ISVAT"].ToString();
                commandRow.Cells[14].Text = dt.Rows[0]["LOID"].ToString();
                commandRow.Cells[15].Text = dt.Rows[0]["UNIT"].ToString();
                commandRow.Cells[16].Text = dt.Rows[0]["PRITEM"].ToString();

                if (dt.Rows[0]["ISVAT"].ToString() == "1")
                {
                    chkVat.Checked = true;
                }
                else
                {
                    chkVat.Checked = false;
                }

                double qty = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
                double price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
                double discount = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
                double ans = (qty * price) - discount;

                txtNetPrice.Text = ans.ToString();
            }
        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton btnNewSearch = (ImageButton)e.Row.Cells[2].FindControl("btnNewSearch");
            TextBox txtGetData = (TextBox)e.Row.Cells[2].FindControl("txtGetData");
            CheckBox chkVat = (CheckBox)e.Row.Cells[11].FindControl("chkVat");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
                if (drow["ISVAT"].ToString() == "Y")
                {
                    chkVat.Checked = true;
                }
                else
                {
                    chkVat.Checked = false;
                }
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    chkVat.Enabled = false;
                }
                else
                {
                    chkVat.Enabled = true;
                }
            }
            if (txtGetData != null)
            {
                string script = "";
                script += "document.getElementById('" + txtGetData.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupProductPRSearch.aspx', '600', '550'); ";
                script += "if ('undefined' ==  document.getElementById('" + txtGetData.ClientID + "').value || '' == document.getElementById('" + txtGetData.ClientID + "').value) ";
                script += "{ return false; } ";

                btnNewSearch.OnClientClick = script;
            }

            ImageButton imbDelete = (ImageButton)e.Row.FindControl("imbDelete");

            if (imbDelete != null)
                imbDelete.OnClientClick = "return confirm('ยืนยันการลบรายการ');";
        }
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
            Calculation();
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
            SetGrvItem(this.txtStatus.Text);
            Calculation();
        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtProduct = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("txtProduct");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtQty");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("txtUnit");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtPrice");
        TextBox txtDiscount = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtDiscount");
        TextBox txtNetPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[8].FindControl("txtNetPrice");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.Rows[e.RowIndex].Cells[9].FindControl("ctlDueDate");
        TextBox txtPRItemCode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[10].FindControl("txtPRItemCode");
        CheckBox chkVat = (CheckBox)this.grvItem.Rows[e.RowIndex].Cells[11].FindControl("chkVat");

        //e.NewValues["LOID"] = this.grvItem.Rows[e.RowIndex].Cells[11].Text;
        e.NewValues["PRODUCTNAME"] = txtProduct.Text;
        //e.NewValues["PRODUCT"] = this.grvItem.Rows[e.RowIndex].Cells[13].Text;
        e.NewValues["QTY"] = txtQty.Text;
        e.NewValues["UNITNAME"] = txtUnit.Text;
        //e.NewValues["UNIT"] = this.grvItem.Rows[e.RowIndex].Cells[14].Text;
        e.NewValues["PRICE"] = txtPrice.Text;
        e.NewValues["DISCOUNT"] = txtDiscount.Text;
        e.NewValues["NETPRICE"] = txtNetPrice.Text;
        e.NewValues["DUEDATE"] = ctlDueDate.DateValue.ToString();
        e.NewValues["CODE"] = txtPRItemCode.Text;
        e.NewValues["PDORDER"] = Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]);
        if (chkVat.Checked)
        {
            e.NewValues["ISVAT"] = "Y";
        }
        else
        {
            e.NewValues["ISVAT"] = "N";
        }
        //e.NewValues["PRITEM"] = this.grvItem.Rows[e.RowIndex].Cells[15].Text;
    }

    protected void txtNetPrice_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;

        TextBox txtQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[4].FindControl("txtQty");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[6].FindControl("txtPrice");
        TextBox txtDiscount = (TextBox)this.grvItem.Rows[rowIndex].Cells[7].FindControl("txtDiscount");
        TextBox txtNetPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[8].FindControl("txtNetPrice");

        double qty = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        double price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        double discount = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
        double ans = (qty * price) - discount;

        txtNetPrice.Text = ans.ToString();
    }

    protected void txtNetPrice_TextChanged1(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;

        TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewQty");
        TextBox txtPrice = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewPrice");
        TextBox txtDiscount = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewDiscount");
        TextBox txtNetPrice = (TextBox)this.grvItem.FooterRow.Cells[8].FindControl("txtNewNetPrice");

        double qty = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        double price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        double discount = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
        double ans = (qty * price) - discount;

        txtNetPrice.Text = ans.ToString();
    }

    #endregion

    protected void txtVat_TextChanged(object sender, EventArgs e)
    {
        Calculation();
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseOrderSearch.aspx");
    }

    protected void SentClick(object sender, EventArgs e)
    {
        //Response.Redirect(Constz.HomeFolder + "Transaction/PDOrder.aspx?LOID=" + (txtLOID.Text == "" ? "0" : txtLOID.Text));

    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetRecentData()))
            ResetState(FlowObj.LOID);
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

}
