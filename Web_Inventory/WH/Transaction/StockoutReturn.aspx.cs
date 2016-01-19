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
using ABB.Data.Inventory.FG;
using ABB.Data.Sales;
using ABB.Flow;
using ABB.Flow.Sales;
using ABB.Flow.Inventory.FG;
using ABB.Global;


public partial class WH_Transaction_StockoutReturn : System.Web.UI.Page
{
    private StockoutFlow _flow;
    private ProductReserveFlow _flow2;
    private StockOutReturnitem item;

    public StockoutFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockoutFlow(); return _flow; }
    }

    public ProductReserveFlow FlowObj2
    {
        get { if (_flow2 == null) _flow2 = new ProductReserveFlow(); return _flow2; }
    }

    public StockOutReturnitem ItemObj
    {
        get { if (item == null) item = new StockOutReturnitem(Authz.CurrentUserInfo.Warehouse); return item; }
    }

    private void Calculation()
    {
        double price = 0;
        //double vat = 0;
        foreach (DataRow dRow in ItemObj.GetStockOutItem(this.txtLOID.Text, this.txtStatus.Text).Rows)
        {
            double itmPrice = Convert.ToDouble(dRow["NETPRICE"]);
            //double vatcal = 0;
            //vatcal = Convert.ToDouble(txtVat.Text == "" ? "0" : txtVat.Text);
            //string isVat = dRow["ISVAT"].ToString();

            price += itmPrice;
            //if (isVat != Constz.VAT.Included.Code)
            //{
            //    vat += Convert.ToDouble(Convert.ToDouble((itmPrice * vatcal) / 100).ToString(Constz.DblFormat));
            //}
        }
        this.txtTotal.Text = price.ToString();
        //this.txtTotalVat.Text = vat.ToString();

        // CalculateDiscount();
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

    private void SetCustomerData(double customer)
    {
        SupplierData data = FlowObj.GetSenderData(customer);
        this.txtCustomerCode.Text = data.CODE;
        this.txtCustomerName.Text = data.NAME;

        ItemObj.ClearSession();
        SetGrvItem(this.txtStatus.Text);
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        StockoutFGData data = FlowObj.GetData(loid);
        if (loid == 0)
        {
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.CODE = "";
            data.CREATEON = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
            data.SENDER = Authz.CurrentUserInfo.Warehouse;
        }
        SetData(data);
    }

    private void SetData(StockoutFGData data)
    {

        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtCustomer.Text = data.RECEIVER.ToString();
        this.txtSender.Text = data.SENDER.ToString();
        this.ctlCreateDate.DateValue = data.CREATEON;
        this.txtCustomerCode.Text = "";
        this.txtCustomerName.Text = "";
        this.txtCode.Text = data.CODE;
        this.txtReason.Text = data.REASON;
        this.txtRemark.Text = data.REMARK;
        this.txtCreateBy.Text = data.CREATEBY;
        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.Approved.Code ? Constz.Requisition.Status.Approved.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : Constz.Requisition.Status.Waiting.Name));
        this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
        SetCustomerData(data.RECEIVER);

        SetGrvItem(data.STATUS);
        Calculation();
        if (data.STATUS == Constz.Requisition.Status.Approved.Code || data.STATUS == Constz.Requisition.Status.Void.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
        this.ctlToolbar.ClientClickPrint = Appz.ReportScript(Constz.Report.ProductReturn, data.LOID) + " return false;";
    }

    private StockoutFGData GetData()
    {
        StockoutFGData data = new StockoutFGData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.REASON = this.txtReason.Text.Trim();
        data.CODE = this.txtCode.Text.Trim();
        data.RECEIVER = Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text);
        data.SENDER = Convert.ToDouble(this.txtSender.Text == "" ? "0" : this.txtSender.Text);
        data.ITEM = ItemObj.GetItemList();
        data.REMARK = this.txtRemark.Text.Trim();
        data.STATUS = this.txtStatus.Text.Trim();
        data.DOCTYPE = Constz.DocType.RetRaw.LOID;
        return data;
    }

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));



            string script = "";
            script += "document.getElementById('" + this.txtCustomer.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/SupplierSearch.aspx' + (document.getElementById('" + this.txtCustomerCode.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtCustomerCode.ClientID + "').value)), '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + this.txtCustomer.ClientID + "').value || '' == document.getElementById('" + this.txtCustomer.ClientID + "').value) ";
            script += "{ return false; } ";

            this.btnSearch.OnClientClick = script;
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันส่งให้จัดซื้อใช่หรือไม่?');";
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetCustomerData(Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text));

    }

    #region grvItem "Insert"

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            Int16 rowIndex = 0;
            DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("cmbNewProduct");
            TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewQty");
            DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("cmbNewUnit");
            DropDownList cmbLot = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("cmbNewLot");
            TextBox txtInvNo = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewInvNo");
            TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewPrice");
            TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("txtNewNetPrice");
            RequisitionItemData data = new RequisitionItemData();
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.DISCOUNT = 0;
            data.PRICE = Convert.ToDouble(txtPrice.Text);
            data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
            data.INVNO = txtInvNo.Text.Trim();
            data.LOTNO = cmbLot.SelectedItem.Value;
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.UNIT = Convert.ToDouble(cmbUnit.SelectedItem.Value);
            data.NETPRICE = data.QTY * data.PRICE;

            if (ItemObj.InsertStockOutItem(data))
            {
                this.grvItem.DataBind();
                this.grvItemNew.DataBind();
                SetGrvItem(this.txtStatus.Text);
                Calculation();
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ComboSource.BuildComboDistinct((DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct"), "V_PRODUCT_SUPPLIER", "NAME", "LOID", "NAME", " TYPE = 'WH' AND ZONE = '4' AND SPLOID = '" + this.txtCustomer.Text + "' AND WAREHOUSE = " + Authz.CurrentUserInfo.Warehouse + " ", "เลือก", "0");
            ComboSource.BuildCombo((DropDownList)e.Row.Cells[6].FindControl("cmbNewUnit"), "UNIT", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[5].FindControl("txtNewQty"));
            string script = "document.getElementById('" + ((TextBox)e.Row.Cells[9].FindControl("txtNewNetPrice")).ClientID + "').value = ";
            script += "document.getElementById('" + ((TextBox)e.Row.Cells[6].FindControl("txtNewQty")).ClientID + "').value * ";
            script += "document.getElementById('" + ((TextBox)e.Row.Cells[8].FindControl("txtNewPrice")).ClientID + "').value";
            ((TextBox)e.Row.Cells[6].FindControl("txtNewQty")).Attributes.Add("onchange", script);
        }
    }

    protected void txtNewBarCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("cmbNewProduct");
        TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewQty");
        TextBox txtInvNo = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewInvNo");
        DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("cmbNewUnit");
        TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewPrice");
        TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("txtNewNetPrice");
        DropDownList cmbLot = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("cmbNewLot");

        ProductSearchData data = FlowObj.GetProductData(txt.Text.Trim());

        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.LOID.ToString()));
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        txtPrice.Text = data.PRICE.ToString();
        txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString();
        txtInvNo.Text = "";
        cmbLot.Items.Clear();
        DataTable dtLot = FlowObj2.GetProductStock(Convert.ToDouble(cmbProduct.SelectedItem.Value), Authz.CurrentUserInfo.Warehouse);
        for (int i = 0; i < dtLot.Rows.Count; i++)
        {
            cmbLot.Items.Add(new ListItem(dtLot.Rows[i]["LOTNO"].ToString(), dtLot.Rows[i]["LOTNO"].ToString()));
        }
        //   cmbLot.SelectedIndex = cmbLot.Items.IndexOf(cmbLot.Items.FindByValue(drow["LOTNO"].ToString()));
    }

    protected void cmbNewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        TextBox txtCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewBarCode");
        TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewQty");
        TextBox txtInvNo = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewInvNo");
        DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("cmbNewUnit");
        TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewPrice");
        TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("txtNewNetPrice");
        DropDownList cmbLot = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("cmbNewLot");
        cmbLot.Items.Clear();
        cmbLot.Items.Add(new ListItem("เลือก", ""));
        DataTable dtLot = FlowObj2.GetProductStock(Convert.ToDouble(cmb.SelectedItem.Value), Authz.CurrentUserInfo.Warehouse);
        for (int i = 0; i < dtLot.Rows.Count; i++)
        {
            cmbLot.Items.Add(new ListItem(dtLot.Rows[i]["LOTNO"].ToString(), dtLot.Rows[i]["LOTNO"].ToString()));
        }

        txtInvNo.Text = "";
        ProductSearchData data = FlowObj.GetProductBarcode(Convert.ToDouble(cmb.SelectedItem.Value));

        txtCode.Text = data.BARCODE;
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
     //   txtPrice.Text = data.PRICE.ToString();
     //   txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString();
    }

    protected void cmbNewLot_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        TextBox txtInvNo = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewInvNo");
        DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("cmbNewProduct");
        TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewPrice");
        TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("txtNewNetPrice");
        TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewQty");

        txtInvNo.Text = FlowObj2.GetInvNo(cmb.SelectedItem.Value, Authz.CurrentUserInfo.Warehouse, cmbProduct.SelectedValue);
        string price = FlowObj2.GetPrice(cmb.SelectedItem.Value, Authz.CurrentUserInfo.Warehouse, cmbProduct.SelectedValue);
        txtPrice.Text = price;
        txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * Convert.ToDouble(price) ).ToString();

    }

    #endregion

    #region grvItem

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[3].FindControl("cmbNewProduct");
            TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewQty");
            DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[7].FindControl("cmbNewUnit");
            DropDownList cmbLot = (DropDownList)this.grvItem.FooterRow.Cells[4].FindControl("cmbNewLot");
            TextBox txtPrice = (TextBox)this.grvItem.FooterRow.Cells[8].FindControl("txtNewPrice");
            TextBox txtInvNo = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtNewInvNo");
            TextBox txtNetPrice = (TextBox)this.grvItem.FooterRow.Cells[9].FindControl("txtNewNetPrice");
            RequisitionItemData data = new RequisitionItemData();
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.DISCOUNT = 0;
            data.PRICE = Convert.ToDouble(txtPrice.Text);
            data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
            data.INVNO = txtInvNo.Text.Trim();
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.UNIT = Convert.ToDouble(cmbUnit.SelectedItem.Value);
            data.LOTNO = cmbLot.SelectedItem.Value;
            data.NETPRICE = data.QTY * data.PRICE;
            if (ItemObj.InsertStockOutItem(data))
            {
                SetGrvItem(this.txtStatus.Text);
                Calculation();
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[1].Text = (e.Row.RowIndex +1).ToString();

            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProduct");
                DropDownList cmbUnit = (DropDownList)e.Row.Cells[7].FindControl("cmbUnit");
                DropDownList cmbLot = (DropDownList)e.Row.Cells[4].FindControl("cmbLot");

                if (this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
                    ComboSource.BuildComboDistinct(cmbProduct, "V_PRODUCT_SUPPLIER", "NAME", "LOID", "NAME", " TYPE = 'WH' AND ZONE = '4' AND SPLOID = '" + this.txtCustomer.Text + "' AND WAREHOUSE = " + Authz.CurrentUserInfo.Warehouse + " ");
                else
                    ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");

                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[6].FindControl("txtQty"));
                string script = "document.getElementById('" + ((TextBox)e.Row.Cells[9].FindControl("txtNetPrice")).ClientID + "').value = ";
                script += "document.getElementById('" + ((TextBox)e.Row.Cells[6].FindControl("txtQty")).ClientID + "').value * ";
                script += "document.getElementById('" + ((TextBox)e.Row.Cells[8].FindControl("txtPrice")).ClientID + "').value";
                ((TextBox)e.Row.Cells[6].FindControl("txtQty")).Attributes.Add("onchange", script);

                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));

                cmbLot.Items.Clear();
                DataTable dtLot = FlowObj2.GetProductStock(Convert.ToDouble(cmbProduct.SelectedItem.Value), Authz.CurrentUserInfo.Warehouse);
                for (int i = 0; i < dtLot.Rows.Count; i++)
                {
                    cmbLot.Items.Add(new ListItem(dtLot.Rows[i]["LOTNO"].ToString(), dtLot.Rows[i]["LOTNO"].ToString()));
                }
                cmbLot.SelectedIndex = cmbLot.Items.IndexOf(cmbLot.Items.FindByValue(drow["LOTNO"].ToString()));
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                imbDelete.Attributes.Add("onclick", "return confirm('ยืนยันการลบสินค้า?');");

                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                DropDownList cmbUnit = (DropDownList)e.Row.Cells[7].FindControl("cmbUnitView");
                DropDownList cmbLot = (DropDownList)e.Row.Cells[4].FindControl("cmbLotView");

                if (this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
                    ComboSource.BuildComboDistinct(cmbProduct, "V_PRODUCT_SUPPLIER", "NAME", "LOID", "NAME", " TYPE = 'WH' AND ZONE = '4' AND SPLOID = '" + this.txtCustomer.Text + "' AND WAREHOUSE = " + Authz.CurrentUserInfo.Warehouse + " ");
                else
                    ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");

                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");
                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));

                cmbLot.Items.Clear();
                DataTable dtLot = FlowObj2.GetProductStock(Convert.ToDouble(cmbProduct.SelectedItem.Value), Authz.CurrentUserInfo.Warehouse);
                for (int i = 0; i < dtLot.Rows.Count; i++)
                {
                    cmbLot.Items.Add(new ListItem(dtLot.Rows[i]["LOTNO"].ToString(), dtLot.Rows[i]["LOTNO"].ToString()));
                }
                cmbLot.SelectedIndex = cmbLot.Items.IndexOf(cmbLot.Items.FindByValue(drow["LOTNO"].ToString()));
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ComboSource.BuildComboDistinct((DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct"), "V_PRODUCT_SUPPLIER", "NAME", "LOID", "NAME", " TYPE = 'WH' AND ZONE = '4' AND SPLOID = '" + this.txtCustomer.Text + "' AND WAREHOUSE = " + Authz.CurrentUserInfo.Warehouse + " ", "เลือก", "0");
            ComboSource.BuildCombo((DropDownList)e.Row.Cells[7].FindControl("cmbNewUnit"), "UNIT", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[6].FindControl("txtNewQty"));
            string script = "document.getElementById('" + ((TextBox)e.Row.Cells[9].FindControl("txtNewNetPrice")).ClientID + "').value = ";
            script += "document.getElementById('" + ((TextBox)e.Row.Cells[6].FindControl("txtNewQty")).ClientID + "').value * ";
            script += "document.getElementById('" + ((TextBox)e.Row.Cells[8].FindControl("txtNewPrice")).ClientID + "').value";
            ((TextBox)e.Row.Cells[6].FindControl("txtNewQty")).Attributes.Add("onchange", script);
        }
    }

    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[rowIndex].Cells[3].FindControl("cmbProduct");
        TextBox txtQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[6].FindControl("txtQty");
        TextBox txtInvNo = (TextBox)this.grvItem.Rows[rowIndex].Cells[5].FindControl("txtInvNo");
        DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[rowIndex].Cells[7].FindControl("cmbUnit");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[8].FindControl("txtPrice");
        TextBox txtNetPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[9].FindControl("txtNetPrice");

        ProductSearchData data = FlowObj.GetProductData(txt.Text.Trim());

        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.LOID.ToString()));
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        txtPrice.Text = data.PRICE.ToString();
        txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString();
        txtInvNo.Text = "";
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        TextBox txtCode = (TextBox)this.grvItem.Rows[rowIndex].Cells[2].FindControl("txtBarCode");
        TextBox txtQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[6].FindControl("txtQty");
        TextBox txtInvNo = (TextBox)this.grvItem.Rows[rowIndex].Cells[5].FindControl("txtInvNo");
        DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[rowIndex].Cells[7].FindControl("cmbUnit");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[8].FindControl("txtPrice");
        TextBox txtNetPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[7].FindControl("txtNetPrice");
        DropDownList cmbLot = (DropDownList)this.grvItem.Rows[rowIndex].Cells[4].FindControl("cmbLot");

        ProductSearchData data = FlowObj.GetProductBarcode(Convert.ToDouble(cmb.SelectedItem.Value));

        txtCode.Text = data.BARCODE;
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        txtPrice.Text = data.PRICE.ToString();
        txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString();
        cmbLot.Items.Clear();
        cmbLot.Items.Add(new ListItem("เลือก", ""));
        DataTable dtLot = FlowObj2.GetProductStock(Convert.ToDouble(cmb.SelectedItem.Value), Authz.CurrentUserInfo.Warehouse);
        for (int i = 0; i < dtLot.Rows.Count; i++)
        {
            cmbLot.Items.Add(new ListItem(dtLot.Rows[i]["LOTNO"].ToString(), dtLot.Rows[i]["LOTNO"].ToString()));
        }
        txtInvNo.Text = "";
    }

    protected void cmbLot_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        TextBox txtInvNo = (TextBox)this.grvItem.Rows[rowIndex].Cells[5].FindControl("txtInvNo");
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[rowIndex].Cells[3].FindControl("cmbProduct");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[8].FindControl("txtPrice");
        TextBox txtQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[6].FindControl("txtQty");
        TextBox txtNetPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[7].FindControl("txtNetPrice");

        txtInvNo.Text = FlowObj2.GetInvNo(cmb.SelectedItem.Value, Authz.CurrentUserInfo.Warehouse, cmbProduct.SelectedValue);
        string price = FlowObj2.GetPrice(cmb.SelectedItem.Value, Authz.CurrentUserInfo.Warehouse, cmbProduct.SelectedValue);
        txtPrice.Text = price;
        txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * Convert.ToDouble(price)).ToString();

    }


    protected void txtNewBarCode_TextChanged1(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[3].FindControl("cmbNewProduct");
        TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewQty");
        TextBox txtInvNo = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtNewInvNo");
        DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[7].FindControl("cmbNewUnit");
        TextBox txtPrice = (TextBox)this.grvItem.FooterRow.Cells[8].FindControl("txtNewPrice");
        TextBox txtNetPrice = (TextBox)this.grvItem.FooterRow.Cells[9].FindControl("txtNewNetPrice");
        DropDownList cmbLot = (DropDownList)this.grvItem.FooterRow.Cells[4].FindControl("cmbNewLot");

        ProductSearchData data = FlowObj.GetProductData(txt.Text.Trim());

        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.LOID.ToString()));
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        txtPrice.Text = data.PRICE.ToString();
        txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString();
        txtInvNo.Text = "";
        cmbLot.Items.Clear();
        DataTable dtLot = FlowObj2.GetProductStock(Convert.ToDouble(cmbProduct.SelectedItem.Value), Authz.CurrentUserInfo.Warehouse);
        for (int i = 0; i < dtLot.Rows.Count; i++)
        {
            cmbLot.Items.Add(new ListItem(dtLot.Rows[i]["LOTNO"].ToString(), dtLot.Rows[i]["LOTNO"].ToString()));
        }
        //cmbLot.SelectedIndex = cmbLot.Items.IndexOf(cmbLot.Items.FindByValue(drow["LOTNO"].ToString()));

    }

    protected void cmbNewProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        TextBox txtCode = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewBarCode");
        TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewQty");
        TextBox txtInvNo = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtNewInvNo");
        DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[7].FindControl("cmbNewUnit");
        TextBox txtPrice = (TextBox)this.grvItem.FooterRow.Cells[8].FindControl("txtNewPrice");
        TextBox txtNetPrice = (TextBox)this.grvItem.FooterRow.Cells[9].FindControl("txtNewNetPrice");
        DropDownList cmbLot = (DropDownList)this.grvItem.FooterRow.Cells[4].FindControl("cmbNewLot");
        cmbLot.Items.Clear();
        cmbLot.Items.Add(new ListItem("เลือก", ""));
        DataTable dtLot = FlowObj2.GetProductStock(Convert.ToDouble(cmb.SelectedItem.Value), Authz.CurrentUserInfo.Warehouse);
        for (int i = 0; i < dtLot.Rows.Count; i++)
        {
            cmbLot.Items.Add(new ListItem(dtLot.Rows[i]["LOTNO"].ToString(), dtLot.Rows[i]["LOTNO"].ToString()));
        }

        txtInvNo.Text = "";
        ProductSearchData data = FlowObj.GetProductBarcode(Convert.ToDouble(cmb.SelectedItem.Value));

        txtCode.Text = data.BARCODE;
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
     //   txtPrice.Text = data.PRICE.ToString();
    //    txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString();
    }

    protected void cmbNewLot_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        TextBox txtInvNo = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtNewInvNo");
        TextBox txtPrice = (TextBox)this.grvItem.FooterRow.Cells[8].FindControl("txtNewPrice");
        TextBox txtNetPrice = (TextBox)this.grvItem.FooterRow.Cells[9].FindControl("txtNewNetPrice");
        TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewQty");

        DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[3].FindControl("cmbNewProduct");

        txtInvNo.Text = FlowObj2.GetInvNo(cmb.SelectedItem.Value, Authz.CurrentUserInfo.Warehouse, cmbProduct.SelectedValue);
        string price = FlowObj2.GetPrice(cmb.SelectedItem.Value, Authz.CurrentUserInfo.Warehouse, cmbProduct.SelectedValue);
        txtPrice.Text = price;
        txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * Convert.ToDouble(price)).ToString();

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
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("cmbProduct");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtQty");
        DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("cmbUnit");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[8].FindControl("txtPrice");
        DropDownList cmbLot = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("cmbLot");
        RequisitionItemData data = new RequisitionItemData();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.DISCOUNT = 0;
        data.PRICE = Convert.ToDouble(txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.UNIT = Convert.ToDouble(cmbUnit.SelectedItem.Value);
        data.NETPRICE = data.QTY * data.PRICE;
        data.LOTNO = cmbLot.SelectedItem.Value;

        e.NewValues["LOID"] = this.grvItem.Rows[e.RowIndex].Cells[8].Text;
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["UNIT"] = data.UNIT.ToString();
        e.NewValues["PRICE"] = data.PRICE.ToString();
        e.NewValues["DISCOUNT"] = "0";
        e.NewValues["NETPRICE"] = data.NETPRICE.ToString();
        e.NewValues["LOTNO"] = data.LOTNO.ToString();
    }

    #endregion

    //protected void txtDiscount_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateDiscount();
    //}

    //protected void txtVat_TextChanged(object sender, EventArgs e)
    //{
    //    Calculation();
    //}

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockoutReturnSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (txtReason.Text == "")
            Appz.ClientAlert(this, "กรุณาระบุสาเหตุการส่งคืน");
        else
        {
            try
            {
                if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
                {
                    ResetState(FlowObj.LOID);
                    Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }
            catch (Exception ex)
            {
                Appz.ClientAlert(this, ex.Message);
            }
        }
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (txtReason.Text == "")
            Appz.ClientAlert(this, "กรุณาระบุสาเหตุการส่งคืน");
        else
        {
            StockoutFGData data = GetData();
            data.STATUS = Constz.Requisition.Status.Approved.Code;
            if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
            {
                ResetState(FlowObj.LOID);
                Appz.ClientAlert(this, "ส่งให้จัดซื้อเรียบร้อยแล้ว");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }

    }

    #endregion
}
