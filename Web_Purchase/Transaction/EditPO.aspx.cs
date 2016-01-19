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

public partial class Transaction_EditPO : System.Web.UI.Page
{
    private EditPOFlow _flow;
    private EditPOItem item;

    public EditPOFlow FlowObj
    {
        get { if (_flow == null) _flow = new EditPOFlow(); return _flow; }
    }

    public EditPOItem ItemObj
    {
        get { if (item == null) item = new EditPOItem(); return item; }
    }

    private void Calculation()
    {
        //double price = 0;
        //double discount = 0;
        //double vat = 0;
        //foreach (DataRow dRow in ItemObj.GetPOItem(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text),this.txtRefPoItem.Text, this.txtStatus.Text).Rows)
        //{
        //    double itmPrice = Convert.ToDouble(dRow["NETPRICE"]);
        //    double itmDiscount = Convert.ToDouble(dRow["DISCOUNT"].ToString() == "" ? "0" : dRow["DISCOUNT"]);
        //    double vatcal = 0;
        //    vatcal = Convert.ToDouble(txtVat.Text == "" ? "0" : txtVat.Text);
        //    string isVat = dRow["ISVAT"].ToString();

        //    price += itmPrice;
        //    vat += Convert.ToDouble(Convert.ToDouble((itmPrice * vatcal) / 100).ToString(Constz.DblFormat));
        //    discount += itmDiscount;
        //}
        //this.txtTotal.Text = price.ToString();
        //this.txtTotalVat.Text = vat.ToString();
        //this.txtTotalDiscount.Text = discount.ToString();
        //double total = price + vat - discount;
        //this.txtGrandTotal.Text = total.ToString();
        double price = 0;
        double discount = 0;
        double vat = 0;
        foreach (DataRow dRow in ItemObj.GetPOItem(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text), this.txtRefPoItem.Text, this.txtStatus.Text).Rows)
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
        //this.grvItemNew.DataBind();

        //if (grvItem.Rows.Count > 0)
        //{
        this.grvItem.ShowFooter = false;
        this.grvItem.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code);
        this.grvItem.Visible = true;
        //this.grvItemNew.Visible = false;
        //}
        //else
        //{
        //    this.grvItem.Visible = (status != Constz.Requisition.Status.Waiting.Code);
        //    //this.grvItemNew.Visible = (status == Constz.Requisition.Status.Waiting.Code);
        //}
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        PurchaseOrderData data = FlowObj.GetData(loid);
        //if (this.txtLOID.Text == "0")

        //    data.ACTIVE = Constz.ActiveStatus.Active;
        //    data.CODE = "";
        //    data.ORDERDATE = DateTime.Now.Date;
        data.STATUS = Constz.Requisition.Status.Waiting.Code;

        SetData(data);
    }

    private void ResetStateNew(double loid)
    {
        ItemObj.ClearSession();
        PurchaseOrderData data = FlowObj.GetData(loid);

        SetDataNew(data);
    }

    private void ResetState2(double loid)
    {
        ItemObj.ClearSession();
        POEditData data = FlowObj.GetDataEdit(loid);

        if (loid == 0)
        {
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.CODE = "";
            data.POEDITDATE = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            this.btnSearch.Visible = true;
        }
        SetDataEdit(data);
    }

    private void SetREFPO(string LOID, string table)
    {
        //DataTable dt = FlowObj.GetREFPOfromTable(LOID, table);
        //if (dt.Rows.Count > 0)
        //{
        //    this.txtPOOldCode.Text = dt.Rows[0]["POOLD"].ToString();
        //    this.txtPOEditCode.Text = dt.Rows[0]["CODE"].ToString();
        //}
    }

    private void SetData(PurchaseOrderData data)
    {
        this.txtPOOldCode.Text = data.CODE;
        this.txtOldLoid.Text = data.LOID.ToString();
        this.txtNewLoid.Text = data.LOID.ToString();
        //this.txtStatus.Text = data.STATUS;
        // this.txtRefLOID.Text = data.LOID.ToString();
        //this.txtRefTable.Text = data.REFTABLE;

        this.cmbSupplier.SelectedIndex = this.cmbSupplier.Items.IndexOf(this.cmbSupplier.Items.FindByValue(data.SUPPLIER.ToString()));
        this.txtCName.Text = data.CNAME;
        this.txtAddress.Text = data.CADDRESS;
        this.txtTel.Text = data.CTEL;
        this.txtFax.Text = data.CFAX;

        this.txtVat.Text = data.VAT.ToString();
        this.txtTotal.Text = data.TOTAL.ToString();
        this.txtTotalVat.Text = data.TOTVAT.ToString();
        this.txtTotalDiscount.Text = data.TOTDIS.ToString();
        this.txtGrandTotal.Text = data.GRANDTOT.ToString();

        this.cmbPaymentType.SelectedIndex = this.cmbPaymentType.Items.IndexOf(this.cmbPaymentType.Items.FindByValue(data.PAYMENTTYPE.ToString()));
        this.txtPaymentDesc.Text = data.PAYMENTDESC;
        this.cmbDelivery.SelectedIndex = this.cmbDelivery.Items.IndexOf(this.cmbDelivery.Items.FindByValue(data.DELIVERY.ToString()));
        this.txtOther.Text = data.OTHER;
        this.txtPOType.Text = data.TYPE;

        // this.ctlOrderDate.DateValue = data.ORDERDATE;


        // this.txtRemark.Text = data.REMARK;


        SetGrvItem(data.STATUS);
        Calculation();

    }

    private void SetDataNew(PurchaseOrderData data)
    {
        this.txtPONewCode.Text = data.CODE;
        this.txtNewLoid.Text = data.LOID.ToString();
        this.txtOldLoid.Text = data.REFLOID.ToString();
        //this.txtStatus.Text = data.STATUS;
        this.txtRefLOID.Text = data.REFLOID.ToString();
        this.txtRefTable.Text = data.REFTABLE;

        this.cmbSupplier.SelectedIndex = this.cmbSupplier.Items.IndexOf(this.cmbSupplier.Items.FindByValue(data.SUPPLIER.ToString()));
        this.txtCName.Text = data.CNAME;
        this.txtAddress.Text = data.CADDRESS;
        this.txtTel.Text = data.CTEL;
        this.txtFax.Text = data.CFAX;

        this.txtVat.Text = data.VAT.ToString();
        this.txtTotal.Text = data.TOTAL.ToString();
        this.txtTotalVat.Text = data.TOTVAT.ToString();
        this.txtTotalDiscount.Text = data.TOTDIS.ToString();
        this.txtGrandTotal.Text = data.GRANDTOT.ToString();

        this.cmbPaymentType.SelectedIndex = this.cmbPaymentType.Items.IndexOf(this.cmbPaymentType.Items.FindByValue(data.PAYMENTTYPE.ToString()));
        this.txtPaymentDesc.Text = data.PAYMENTDESC;
        this.cmbDelivery.SelectedIndex = this.cmbDelivery.Items.IndexOf(this.cmbDelivery.Items.FindByValue(data.DELIVERY.ToString()));
        this.txtOther.Text = data.OTHER;
        this.txtPOType.Text = data.TYPE;
        this.txtPOOldCode.Text = FlowObj.GetPOOldCode(data.REFLOID);
        SetGrvItem(this.txtStatus.Text.Trim());
        Calculation();

    }

    private void SetDataEdit(POEditData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.ctlOrderDate.DateValue = data.POEDITDATE;
        this.txtEditCode.Text = data.CODE;
        this.txtNewLoid.Text = data.PONEW.ToString();
        this.txtOldLoid.Text = data.POOLD.ToString();
        this.txtReason.Text = data.REASON;
        this.txtRemark.Text = data.REMARK;
        this.txtStatus.Text = data.STATUS;
        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.Approved.Code ? Constz.Requisition.Status.Approved.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : Constz.Requisition.Status.Waiting.Name));

        if (data.STATUS == Constz.Requisition.Status.Approved.Code || data.STATUS == Constz.Requisition.Status.Void.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }

    }

    private PurchaseOrderData GetData()
    {
        PurchaseOrderData data = new PurchaseOrderData();
        if (Convert.ToDouble(this.txtNewLoid.Text == "" ? "0" : this.txtNewLoid.Text) == Convert.ToDouble(this.txtOldLoid.Text == "" ? "0" : this.txtOldLoid.Text))
            data.LOID = 0;
        else
            data.LOID = Convert.ToDouble(this.txtNewLoid.Text == "" ? "0" : this.txtNewLoid.Text);

        data.CODE = this.txtPONewCode.Text.Trim();
        data.ORDERDATE = this.ctlOrderDate.DateValue;
        data.ORDERTYPE = Constz.OrderType.PO.Code;
        data.SUPPLIER = Convert.ToDouble(this.cmbSupplier.SelectedItem.Value);
        data.CNAME = this.txtCName.Text.Trim();
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        // data.REMARK = this.txtRemark.Text.Trim();
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.TOTDIS = Convert.ToDouble(this.txtTotalDiscount.Text == "" ? "0" : this.txtTotalDiscount.Text);
        data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
        data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
        data.DELIVERY = this.cmbDelivery.SelectedItem.Value;
        data.OTHER = this.txtOther.Text.Trim();
        data.PAYMENTDESC = this.txtPaymentDesc.Text;
        data.PAYMENTTYPE = this.cmbPaymentType.SelectedItem.Value;
        data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
        data.REFLOID = Convert.ToDouble(this.txtOldLoid.Text == "" ? "0" : this.txtOldLoid.Text);
        data.REFTABLE = "PDORDER";
        data.STATUS = this.txtStatus.Text.Trim();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.TYPE = this.txtPOType.Text.Trim();
        data.ITEM = ItemObj.GetItemList();

        return data;
    }

    private POEditData GetDataEdit()
    {
        POEditData data = new POEditData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtEditCode.Text.Trim();
        data.STATUS = this.txtStatus.Text.Trim();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.POEDITDATE = this.ctlOrderDate.DateValue;
        data.PONEW = Convert.ToDouble(this.txtNewLoid.Text == "" ? "0" : this.txtNewLoid.Text);
        data.POOLD = Convert.ToDouble(this.txtOldLoid.Text == "" ? "0" : this.txtOldLoid.Text);
        data.REASON = this.txtReason.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.TYPE = "1";
        return data;
    }

    private POEditData GetRecentDataEdit()
    {
        POEditData data = new POEditData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtEditCode.Text.Trim();
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.POEDITDATE = this.ctlOrderDate.DateValue;
        data.PONEW = Convert.ToDouble(this.txtNewLoid.Text == "" ? "0" : this.txtNewLoid.Text);
        data.POOLD = Convert.ToDouble(this.txtOldLoid.Text == "" ? "0" : this.txtOldLoid.Text);
        data.REASON = this.txtReason.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.APPROVEDATE = DateTime.Now.Date;
        data.APPROVER = Authz.CurrentUserInfo.UserID;
        data.TYPE = "1";
        return data;
    }

    private PurchaseOrderData GetRecentData()
    {
        PurchaseOrderData data = new PurchaseOrderData();
        if (Convert.ToDouble(this.txtNewLoid.Text == "" ? "0" : this.txtNewLoid.Text) == Convert.ToDouble(this.txtOldLoid.Text == "" ? "0" : this.txtOldLoid.Text))
            data.LOID = 0;
        else
            data.LOID = Convert.ToDouble(this.txtNewLoid.Text == "" ? "0" : this.txtNewLoid.Text);

        data.CODE = this.txtPONewCode.Text.Trim();
        data.ORDERDATE = this.ctlOrderDate.DateValue;
        data.ORDERTYPE = Constz.OrderType.PO.Code;
        data.SUPPLIER = Convert.ToDouble(this.cmbSupplier.SelectedItem.Value);
        data.CNAME = this.txtCName.Text.Trim();
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        // data.REMARK = this.txtRemark.Text.Trim();
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.TOTDIS = Convert.ToDouble(this.txtTotalDiscount.Text == "" ? "0" : this.txtTotalDiscount.Text);
        data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
        data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
        data.DELIVERY = this.cmbDelivery.SelectedItem.Value;
        data.OTHER = this.txtOther.Text.Trim();
        data.PAYMENTDESC = this.txtPaymentDesc.Text;
        data.PAYMENTTYPE = this.cmbPaymentType.SelectedItem.Value;
        data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
        data.REFLOID = Convert.ToDouble(this.txtOldLoid.Text == "" ? "0" : this.txtOldLoid.Text);
        data.REFTABLE = "PDORDER";
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.APPROVEDATE = DateTime.Now.Date;
        data.APPROVER = Authz.CurrentUserInfo.UserID;
        data.TYPE = this.txtPOType.Text.Trim();
        data.ITEM = ItemObj.GetItemList();
        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", "", "เลือก", "0");
            ResetState2(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            ResetStateNew(Convert.ToDouble(this.txtNewLoid.Text.Trim()));

            string scriptRefNo = "";
            scriptRefNo += "document.getElementById('" + this.txtOldLoid.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupPOSearch.aspx' + (document.getElementById('" + this.txtPOOldCode.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtPOOldCode.ClientID + "').value)), '600', '550');";
            scriptRefNo += "if ('undefined' ==  document.getElementById('" + this.txtOldLoid.ClientID + "').value || '' == document.getElementById('" + this.txtOldLoid.ClientID + "').value) ";
            scriptRefNo += "{ return false; } ";

            this.btnSearch.OnClientClick = scriptRefNo;
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.PurchaseOrder, Convert.ToDouble(this.txtNewLoid.Text == "" ? "0" : this.txtNewLoid.Text)) + " return false;";

        }
    }

    protected void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        DataTable dt = FlowObj.GetSupplierData(cmb.SelectedItem.Value);
        if (dt.Rows.Count > 0)
        {
            this.txtCName.Text = dt.Rows[0]["TITLENAME"].ToString() + " " + dt.Rows[0]["CNAME"].ToString() + " " + dt.Rows[0]["CLASTNAME"].ToString();
            this.txtAddress.Text = dt.Rows[0]["CADDRESS"].ToString(); //+ " " + dt.Rows[0]["CROAD"].ToString() + " " + dt.Rows[0]["TAMBOLNAME"].ToString() + " " + dt.Rows[0]["AMPHURNAME"].ToString() + " " + dt.Rows[0]["PROVINCENAME"].ToString() + " " + dt.Rows[0]["ZIPCODE"].ToString();
            this.txtTel.Text = dt.Rows[0]["CTEL"].ToString();
            this.txtFax.Text = dt.Rows[0]["FAX"].ToString();
        }
    }

    #region grvItemNew

    //protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    Int16 rowIndex = 0;
    //    TextBox txtBarCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewBarCode");
    //    TextBox txtProduct = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewProduct");
    //    TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewQty");
    //    TextBox txtUnit = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewUnit");
    //    TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewPrice");
    //    TextBox txtDiscount = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewDiscount");
    //    TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewNetPrice");
    //    Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("ctlNewDueDate");
    //    TextBox txtPRItemCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[10].FindControl("txtNewPRItemCode");

    //    if (e.CommandName == "Insert")
    //    {
    //        POItemData data = new POItemData();
    //        data.BARCODE = txtBarCode.Text;
    //        data.PRODUCTNAME = txtProduct.Text;
    //        data.PRODUCT = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[13].Text);
    //        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
    //        data.UNITNAME = txtUnit.Text;
    //        data.UNIT = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[14].Text);
    //        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
    //        data.DISCOUNT = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
    //        data.NETPRICE = Convert.ToDouble(txtNetPrice.Text == "" ? "0" : txtNetPrice.Text);
    //        data.DUEDATE = ctlDueDate.DateValue;
    //        data.PRITEMCODE = txtPRItemCode.Text;
    //        data.PRITEM = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[15].Text);
    //        data.ISVAT = this.grvItemNew.Rows[rowIndex].Cells[12].Text;

    //        if (ItemObj.InsertPOItem(data))
    //        {
    //            //this.grvItem.DataBind();
    //            //this.grvItemNew.DataBind();
    //            SetGrvItem(this.txtStatus.Text);
    //            Calculation();
    //        }
    //        else
    //            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    //    }
    //    else if (e.CommandName == "Search")
    //    {
    //        TextBox txtGetData = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtGetData");
    //        if (txtGetData.Text != "undefined")
    //        {
    //            DataTable dt = FlowObj.GetViewProductPOPopupList(Convert.ToDouble(txtGetData.Text));
    //            if (dt.Rows.Count > 0)
    //            {
    //                //this.txtDebug.Text = txtGetData.Text;
    //                txtBarCode.Text = dt.Rows[0]["PCODE"].ToString();
    //                txtProduct.Text = dt.Rows[0]["PDNAME"].ToString();
    //                txtQty.Text = dt.Rows[0]["QTY"].ToString();
    //                txtUnit.Text = dt.Rows[0]["UNAME"].ToString();
    //                txtPrice.Text = dt.Rows[0]["CURPRICE"].ToString();
    //                ctlDueDate.DateValue = Convert.ToDateTime(dt.Rows[0]["DUEDATE"]);
    //                txtPRItemCode.Text = dt.Rows[0]["PRCODE"].ToString();
    //                this.grvItemNew.Rows[rowIndex].Cells[12].Text = dt.Rows[0]["ISVAT"].ToString();
    //                this.grvItemNew.Rows[rowIndex].Cells[13].Text = dt.Rows[0]["LOID"].ToString();
    //                this.grvItemNew.Rows[rowIndex].Cells[14].Text = dt.Rows[0]["UNIT"].ToString();
    //                this.grvItemNew.Rows[rowIndex].Cells[15].Text = dt.Rows[0]["PRITEM"].ToString();

    //                double qty = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
    //                double price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
    //                double discount = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
    //                double ans = (qty * price) - discount;

    //                txtNetPrice.Text = ans.ToString();
    //            }
    //        }
    //    }
    //}

    //protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        ImageButton btnNewSearch = (ImageButton)e.Row.FindControl("btnNewSearch");
    //        TextBox txtGetData = (TextBox)e.Row.Cells[2].FindControl("txtGetData");
    //        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)e.Row.Cells[9].FindControl("ctlNewDueDate");
    //        TextBox txtPRItemCode = (TextBox)e.Row.Cells[10].FindControl("txtNewPRItemCode");

    //        string script = "";
    //        script += "document.getElementById('" + txtGetData.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupProductPRSearch.aspx', '600', '550');";

    //        btnNewSearch.OnClientClick = script;
    //    }
    //}

    //protected void txtNewNetPrice_TextChanged(object sender, EventArgs e)
    //{
    //    TextBox txt = (TextBox)sender;
    //    Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;

    //    TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewQty");
    //    TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewPrice");
    //    TextBox txtDiscount = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewDiscount");
    //    TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewNetPrice");

    //    double qty = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
    //    double price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
    //    double discount = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
    //    double ans = (qty * price) - discount;

    //    txtNetPrice.Text = ans.ToString();
    //}

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
        //    TextBox txtBarCode = (TextBox)commandRow.Cells[2].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtBarCode" : "txtNewBarCode");
        TextBox txtProduct = (TextBox)commandRow.Cells[2].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtProduct" : "txtNewProduct");
        TextBox txtQty = (TextBox)commandRow.Cells[3].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtQty" : "txtNewQty");
        TextBox txtReceiveQty = (TextBox)commandRow.Cells[4].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtReceiveQty" : "txtNewReceiveQty");
        TextBox txtUnit = (TextBox)commandRow.Cells[5].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtUnit" : "txtNewUnit");
        TextBox txtPrice = (TextBox)commandRow.Cells[6].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtPrice" : "txtNewPrice");
        TextBox txtDiscount = (TextBox)commandRow.Cells[7].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtDiscount" : "txtNewDiscount");
        TextBox txtNetPrice = (TextBox)commandRow.Cells[8].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtNetPrice" : "txtNewNetPrice");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)commandRow.Cells[9].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "ctlDueDate" : "ctlNewDueDate");
        TextBox txtPRItemCode = (TextBox)commandRow.Cells[10].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtPRItemCode" : "txtNewPRItemCode");

        if (e.CommandName == "Insert")
        {
            POItemData data = new POItemData();

            //   data.BARCODE = txtBarCode.Text;
            data.PRODUCTNAME = txtProduct.Text;
            try
            {
                data.PRODUCT = Convert.ToDouble(commandRow.Cells[13].Text);
            }
            catch (Exception ex)
            {
                data.PRODUCT = 0;
            }
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.RECEIVEQTY = Convert.ToDouble(txtReceiveQty.Text == "" ? "0" : txtReceiveQty.Text);
            data.UNITNAME = txtUnit.Text;
            try
            {
                data.UNIT = Convert.ToDouble(commandRow.Cells[14].Text);
            }
            catch (Exception ex)
            {
                data.UNIT = 0;
            }
            data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
            data.DISCOUNT = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
            data.NETPRICE = Convert.ToDouble(txtNetPrice.Text == "" ? "0" : txtNetPrice.Text);
            data.DUEDATE = ctlDueDate.DateValue;
            data.PRITEMCODE = txtPRItemCode.Text;
            try
            {
                data.PRITEM = Convert.ToDouble(commandRow.Cells[15].Text);
            }
            catch (Exception ex)
            {
                data.PRITEM = 0;
            }
            data.ISVAT = commandRow.Cells[12].Text;

            if (ItemObj.InsertPOItem(data))
            {
                SetGrvItem(this.txtStatus.Text);
                Calculation();
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
        //else if (e.CommandName == "Search")
        //{
        //    TextBox txtGetData = (TextBox)commandRow.Cells[2].FindControl("txtGetData");
        //    DataTable dt = FlowObj.GetViewProductPOPopupList(Convert.ToDouble(txtGetData.Text));
        //    if (dt.Rows.Count > 0)
        //    {
        //        //this.txtDebug.Text = txtGetData.Text;
        //        txtBarCode.Text = dt.Rows[0]["PCODE"].ToString();
        //        txtProduct.Text = dt.Rows[0]["PDNAME"].ToString();
        //        txtQty.Text = dt.Rows[0]["QTY"].ToString();
        //        txtUnit.Text = dt.Rows[0]["UNAME"].ToString();
        //        txtPrice.Text = dt.Rows[0]["CURPRICE"].ToString();
        //        ctlDueDate.DateValue = Convert.ToDateTime(dt.Rows[0]["DUEDATE"]);
        //        txtPRItemCode.Text = dt.Rows[0]["PRCODE"].ToString();
        //        commandRow.Cells[12].Text = dt.Rows[0]["ISVAT"].ToString();
        //        commandRow.Cells[13].Text = dt.Rows[0]["LOID"].ToString();
        //        commandRow.Cells[14].Text = dt.Rows[0]["UNIT"].ToString();
        //        commandRow.Cells[15].Text = dt.Rows[0]["PRITEM"].ToString();

        //        double qty = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        //        double price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        //        double discount = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
        //        double ans = (qty * price) - discount;

        //        txtNetPrice.Text = ans.ToString();
        //    }
        //}
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                    imbDelete.Attributes.Add("onclick", "return confirm('ยืนยันการลบสินค้า?');");
                }
                else if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
                {
                    TextBox txtQty = (TextBox)e.Row.Cells[3].FindControl("txtQty");
                    ControlUtil.SetDblTextBox(txtQty);
                }
            }
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
        TextBox txtProduct = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("txtProduct");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("txtQty");
        TextBox txtReceiveQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("txtReceiveQty");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("txtUnit");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtPrice");
        TextBox txtDiscount = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtDiscount");
        TextBox txtNetPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[8].FindControl("txtNetPrice");
        Controls_DatePickerControl ctlDueDate = (Controls_DatePickerControl)this.grvItem.Rows[e.RowIndex].Cells[9].FindControl("ctlDueDate");
        TextBox txtPRItemCode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[10].FindControl("txtPRItemCode");

        //e.NewValues["LOID"] = this.grvItem.Rows[e.RowIndex].Cells[11].Text;
        e.NewValues["PRODUCTNAME"] = txtProduct.Text;
        //e.NewValues["PRODUCT"] = this.grvItem.Rows[e.RowIndex].Cells[13].Text;
        e.NewValues["QTY"] = txtQty.Text;
        e.NewValues["RECEIVEQTY"] = txtReceiveQty.Text;
        e.NewValues["UNITNAME"] = txtUnit.Text;
        //e.NewValues["UNIT"] = this.grvItem.Rows[e.RowIndex].Cells[14].Text;
        e.NewValues["PRICE"] = txtPrice.Text;
        e.NewValues["DISCOUNT"] = txtDiscount.Text;
        e.NewValues["NETPRICE"] = txtNetPrice.Text;
        e.NewValues["DUEDATE"] = ctlDueDate.DateValue.ToString();
        e.NewValues["CODE"] = txtPRItemCode.Text;
        //e.NewValues["PRITEM"] = this.grvItem.Rows[e.RowIndex].Cells[15].Text;
    }

    protected void txtNetPrice_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;

        TextBox txtQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtQty");
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

        TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[3].FindControl("txtNewQty");
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
        Response.Redirect(Constz.HomeFolder + "Transaction/EditPOSearch.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetRecentData()))
        {
            ResetStateNew(FlowObj.LOID);

            if (FlowObj.UpdateDataPOEdit(Authz.CurrentUserInfo.UserID, GetRecentDataEdit()))
            {
                this.txtLOID.Text = FlowObj.LOIDEDIT.ToString();
                ResetState2(FlowObj.LOIDEDIT);
                ResetStateNew(FlowObj.LOID);

                if (FlowObj.UpdateStatusPOOld(Convert.ToDouble(this.txtOldLoid.Text), Constz.Requisition.Status.Void.Code, Authz.CurrentUserInfo.UserID))
                {
                    if (FlowObj.UpdateStockIn(Convert.ToDouble(this.txtOldLoid.Text), Convert.ToDouble(this.txtNewLoid.Text), Authz.CurrentUserInfo.UserID))
                        Appz.ClientAlert(this, "อนุมัติแก้ไขเรียบร้อยแล้ว");
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }

            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);

        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            this.txtRefPoItem.Text = "";        //item New PO
            ResetStateNew(FlowObj.LOID);

            if (FlowObj.UpdateDataPOEdit(Authz.CurrentUserInfo.UserID, GetDataEdit()))
            {
                this.txtLOID.Text = FlowObj.LOIDEDIT.ToString();
                ResetState2(FlowObj.LOIDEDIT);
                ResetStateNew(FlowObj.LOID);

                Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
            }

            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);

        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.txtRefPoItem.Text = "OLDPO";   // item Old PO
        ResetState(Convert.ToDouble(this.txtOldLoid.Text));
    }

}

