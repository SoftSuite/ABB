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

public partial class Transaction_ReturnProduct : System.Web.UI.Page
{
    #region Variables & Properties

    private ReturnProductFlow _flow;
    private ReturnProductItem item;

    public ReturnProductFlow FlowObj
    {
        get { if (_flow == null) _flow = new ReturnProductFlow(); return _flow; }
    }

    public ReturnProductItem ItemObj
    {
        get { if (item == null) item = new ReturnProductItem(); return item; }
    }

    #endregion

    #region Methods

    #region GridView

    private void SetProductDetail(ProductSearchData data, GridViewRow gRow, TextBox txtBarcode, DropDownList cmbProduct, TextBox txtUnit, TextBox txtPrice, TextBox txtNetPrice, TextBox txtPDQty, TextBox txtQty)
    {
        txtBarcode.Text = data.BARCODE;
        txtPDQty.Text = Convert.ToString(data.PDQTY);
        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.LOID.ToString()));
        txtUnit.Text = Convert.ToString(data.UNITNAME);
        txtPrice.Text = data.PRICE.ToString(Constz.DblFormat);
        txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString(Constz.DblFormat);
        gRow.Cells[16].Text = data.UNIT.ToString() ;
     }

    private void txtBarcode_TextChanged(TextBox txt, GridViewRow gRow, string ctlProductName, string ctlPDQtyName, string ctlQtyName, string ctlUnitName, string ctlPriceName, string ctlNetPriceName)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl(ctlProductName);
        TextBox txtPDQty = (TextBox)gRow.Cells[4].FindControl(ctlPDQtyName);
        TextBox txtQty = (TextBox)gRow.Cells[5].FindControl(ctlQtyName);
        TextBox txtUnit = (TextBox)gRow.Cells[6].FindControl(ctlUnitName);
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl(ctlPriceName);
        TextBox txtNetPrice = (TextBox)gRow.Cells[8].FindControl(ctlNetPriceName);

        ProductSearchData data = FlowObj.GetProductData(txt.Text.Trim());
        SetProductDetail(data, gRow, txt, cmbProduct, txtUnit, txtPrice, txtNetPrice, txtPDQty, txtQty);
    }

    private void cmbProduct_SelectedIndexChanged(DropDownList cmb, GridViewRow gRow, string ctlBarcodeName, string ctlPDQtyName, string ctlQtyName, string ctlUnitName, string ctlPriceName, string ctlNetPriceName)
    {
        TextBox txtCode = (TextBox)gRow.Cells[2].FindControl(ctlBarcodeName);
        TextBox txtPDQty = (TextBox)gRow.Cells[4].FindControl(ctlPDQtyName);
        TextBox txtQty = (TextBox)gRow.Cells[5].FindControl(ctlQtyName);
        TextBox txtUnit = (TextBox)gRow.Cells[6].FindControl(ctlUnitName);
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl(ctlPriceName);
        TextBox txtNetPrice = (TextBox)gRow.Cells[8].FindControl(ctlNetPriceName);

        ProductSearchData data = FlowObj.GetProductData(Convert.ToDouble(cmb.SelectedItem.Value));
        SetProductDetail(data, gRow, txtCode, cmb, txtUnit, txtPrice, txtNetPrice, txtPDQty, txtQty);
    }

    private void InsertData(GridViewRow gRow)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl("cmbNewProduct");
        TextBox txtPDQty = (TextBox)gRow.Cells[4].FindControl("txtNewPDQty");
        TextBox txtQty = (TextBox)gRow.Cells[5].FindControl("txtNewQty");
        TextBox txtUnit = (TextBox)gRow.Cells[6].FindControl("txtNewUnit");
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl("txtNewPrice");
        TextBox txtNetPrice = (TextBox)gRow.Cells[8].FindControl("txtNewNetPrice");
        RequisitionItemData data = new RequisitionItemData();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.DISCOUNT = 0;
        data.PRICE = Convert.ToDouble(txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.PDQTY = Convert.ToDouble(txtPDQty.Text);
        data.UnitName = txtUnit.Text;
        data.NETPRICE = data.QTY * data.PRICE;
        data.UNIT = Convert.ToDouble(gRow.Cells[16].Text);

        if (ItemObj.InsertRequisitionItem(data))
        {
            SetGrvItem(this.txtStatus.Text);
            Calculation();
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void NewRowDataBound(GridViewRow gRow)
    {
        ComboSource.BuildComboDistinct((DropDownList)gRow.Cells[3].FindControl("cmbNewProduct"), "V_PRODUCT_RETURNREQUEST", "NAME", "LOID", "NAME", "CULOID =" + txtCustomer.Text.Trim(), "เลือก", "0");
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[4].FindControl("txtNewPDQty"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[5].FindControl("txtNewQty"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[6].FindControl("txtNewUnit"));
        string script = "document.getElementById('" + ((TextBox)gRow.Cells[8].FindControl("txtNewNetPrice")).ClientID + "').value = ";
        script += "document.getElementById('" + ((TextBox)gRow.Cells[5].FindControl("txtNewQty")).ClientID + "').value * ";
        script += "document.getElementById('" + ((TextBox)gRow.Cells[7].FindControl("txtNewPrice")).ClientID + "').value";
        ((TextBox)gRow.Cells[4].FindControl("txtNewQty")).Attributes.Add("onchange", script);
    }

    #endregion

    #region Data

    private void SetData(ProductReserveData data)
    {
        //if (data.LOID != 0)
        //    this.cmbRequisitionType.Enabled = false;
        //else
        //    this.cmbRequisitionType.Enabled = true;
        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtCustomer.Text = data.CUSTOMER.ToString();
        this.txtCustomerCode.Text = "";
        this.txtCustomerName.Text = "";
        this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.CTITLE.ToString()));
        this.txtName.Text = data.CNAME;
        this.txtLastName.Text = data.CLASTNAME;
        this.txtAddress.Text = data.CADDRESS;
        this.txtTel.Text = data.CTEL;
        this.txtFax.Text = data.CFAX;
        this.txtRequisitionCode.Text = data.CODE;
        this.ctlReserveDate.DateValue = data.REQDATE;
        //if (data.REQDATE.Year != 1)
        //{
        //    double day = Convert.ToDouble(SysConfigFlow.GetValue(Constz.ConfigName.PERIOD));
        //    this.ctlReserveDate.DateValue = data.REQDATE.AddDays(day);
        //}
        this.ctlDueDate.DateValue = data.DUEDATE;
        this.txtRemark.Text = data.REMARK;
        this.txtCreateBy.Text = data.CREATEBY;
        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.ApproveWH.Code ? Constz.Requisition.Status.ApproveWH.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : (data.STATUS == Constz.Requisition.Status.Finish.Code ? Constz.Requisition.Status.Finish.Name : Constz.Requisition.Status.Waiting.Name)));
        this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
        this.txtGrandTotal.Text = data.GRANDTOT.ToString(Constz.DblFormat);
        this.txtWareHouse.Text = data.WAREHOUSE.ToString();
        SetCustomerData(data.CUSTOMER, false);

        SetGrvItem(data.STATUS);

        if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
            this.btnSearch.Visible = false;
        }
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockInReturnProduct, data.LOID) + " return false;";

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
        data.REQDATE = DateTime.Now.Date;
        data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ12;
        data.STATUS = this.txtStatus.Text.Trim();
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.WAREHOUSE = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);
        return data;
    }

    #endregion

    #region Others

    private void Calculation()
    {
        double price = 0;
        //double vat = 0;
        foreach (DataRow dRow in ItemObj.GetRequisitionItem(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text), this.txtStatus.Text).Rows)
        {
            double itmPrice = Convert.ToDouble(dRow["NETPRICE"]);
            //double vatcal = 0;
            //string isVat = dRow["ISVAT"].ToString();

            price += itmPrice;
            //if (isVat != Constz.VAT.Included.Code)
            //{
            //    vat += Convert.ToDouble(Convert.ToDouble((itmPrice * vatcal) / 100).ToString(Constz.DblFormat));
            //}
        }
        this.txtTotal.Text = price.ToString();

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

    private void SetCustomerData(double customer, bool isSearch)
    {
        CustomerData data = FlowObj.GetCustomerData(customer);
        TitleData title = FlowObj.GetTitleData(data.TITLE);
        this.txtCustomerCode.Text = data.CODE;
        this.txtCustomerName.Text = title.NAME + data.NAME + " " + data.LASTNAME;
        MemberTypeData tData = FlowObj.GetMemberTypeData(data.MEMBERTYPE);
        if (isSearch)
        {
            this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.CTITLE.ToString()));
            this.txtName.Text = data.CNAME;
            this.txtLastName.Text = data.CLASTNAME;
            this.txtAddress.Text = data.BILLADDRESS;
            this.txtTel.Text = data.BILLTEL;
            this.txtFax.Text = data.BILLFAX;
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
            data.REQDATE = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
            data.VAT = Convert.ToDouble(SysConfigFlow.GetValue(Constz.ConfigName.VAT));
        }
        else
        {
            this.btnSearch.Visible = false;

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
            ComboSource.BuildCombo(this.cmbTitle, "TITLE", "NAME", "LOID", "NAME", "");
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
           
        

        string script = "";
        script += "document.getElementById('" + this.txtCustomer.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/CustRetProductSearch.aspx' + (document.getElementById('" + this.txtCustomerCode.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtCustomerCode.ClientID + "').value)), '600', '550');";
        script += "if ('undefined' ==  document.getElementById('" + this.txtCustomer.ClientID + "').value || '' == document.getElementById('" + this.txtCustomer.ClientID + "').value) ";
        script += "{ return false; } ";

        this.btnSearch.OnClientClick = script;
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetCustomerData(Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text), true);
        ItemObj.ClearSession();
        SetGrvItem(this.txtStatus.Text);

    }

    protected void txtVat_TextChanged(object sender, EventArgs e)
    {
        Calculation();
    }

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ReturnProductSearch.aspx");
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
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ยืนยันการส่งคลัง");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
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
        txtBarcode_TextChanged((TextBox)sender, this.grvItemNew.Rows[0], "cmbNewProduct", "txtNewPDQty", "txtNewQty", "txtNewUnit", "txtNewPrice", "txtNewNetPrice");
    }

    protected void cmbNewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbProduct_SelectedIndexChanged((DropDownList)sender, this.grvItemNew.Rows[0], "txtNewBarCode", "txtNewPDQty", "txtNewQty", "txtNewUnit", "txtNewPrice", "txtNewNetPrice");
    }

    #endregion

    #region grvItem

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProduct");
                ComboSource.BuildComboDistinct(cmbProduct, "V_PRODUCT_RETURNREQUEST", "NAME", "LOID", "NAME", "CULOID =" + txtCustomer.Text.Trim());
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[4].FindControl("txtPDQty"));
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[7].FindControl("txtQty"));
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[6].FindControl("txtUnit"));

                string script = "document.getElementById('" + ((TextBox)e.Row.Cells[7].FindControl("txtNetPrice")).ClientID + "').value = ";
                script += "document.getElementById('" + ((TextBox)e.Row.Cells[4].FindControl("txtQty")).ClientID + "').value * ";
                script += "document.getElementById('" + ((TextBox)e.Row.Cells[6].FindControl("txtPrice")).ClientID + "').value";
                ((TextBox)e.Row.Cells[5].FindControl("txtQty")).Attributes.Add("onchange", script);
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                ComboSource.BuildComboDistinct(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
             }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            NewRowDataBound(e.Row);
        }
    }

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvItem.FooterRow);
        }
    }

    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        txtBarcode_TextChanged(txt, this.grvItem.Rows[rowIndex], "cmbProduct", "txtPDQty", "txtQty", "txtUnit", "txtPrice", "txtNetPrice");
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        cmbProduct_SelectedIndexChanged(cmb, this.grvItem.Rows[rowIndex], "txtBarCode", "txtPDQty", "txtQty", "txtUnit", "txtPrice", "txtNetPrice");
    }

    protected void txtNewBarCode_TextChanged1(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        txtBarcode_TextChanged(txt, this.grvItem.FooterRow, "cmbNewProduct", "txtNewPDQty", "txtNewQty", "txtNewUnit", "txtNewPrice", "txtNewNetPrice");
    }

    protected void cmbNewProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        cmbProduct_SelectedIndexChanged(cmb, this.grvItem.FooterRow, "txtNewBarCode", "txtNewPDQty", "txtNewQty", "txtNewUnit", "txtNewPrice", "txtNewNetPrice");
    }

    protected void grvItem_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.ExceptionHandled)
        {
            e.KeepInEditMode = true;
            Appz.ClientAlert(this, e.Exception.Message);
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
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("cmbProduct");
        TextBox txtPDQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtPDQty");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("txtQty");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtUnit");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtPrice");
        RequisitionItemData data = new RequisitionItemData();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.DISCOUNT = 0;
        data.PRICE = Convert.ToDouble(txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.PDQTY = data.PDQTY;
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.UnitName = data.UnitName;
        data.UNIT = data.UNIT;
        data.NETPRICE = data.QTY * data.PRICE;

        e.NewValues["LOID"] = this.grvItem.Rows[e.RowIndex].Cells[8].Text;
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["PDQTY"] = data.PDQTY.ToString();
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["UNITNAME"] = data.UnitName.ToString();
        e.NewValues["PRICE"] = data.PRICE.ToString();
        e.NewValues["DISCOUNT"] = "0";
        e.NewValues["NETPRICE"] = data.NETPRICE.ToString();
    }

    #endregion

    #endregion
}

