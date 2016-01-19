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
using ABB.Flow;
using ABB.Flow.Inventory.FG;
using ABB.Global;

public partial class WH_Transaction_StockinReturn : System.Web.UI.Page
{
    #region Variables & Properties

    private StockinReturnFlow _flow;
    private StockinReturnItemWH item;

    public StockinReturnFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockinReturnFlow(); return _flow; }
    }

    public StockinReturnItemWH ItemObj
    {
        get { if (item == null) item = new StockinReturnItemWH(); return item; }
    }

    #endregion

    #region Methods

    #region Others

    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        if (grvItem.Rows.Count > 0)
        {
            this.grvItem.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code);
            this.grvItem.Visible = true;
            this.grvItem.ShowFooter = (status == Constz.Requisition.Status.Waiting.Code);
            this.grvItemNew.Visible = false;
        }
        else
        {
            this.grvItem.Visible = (status != Constz.Requisition.Status.Waiting.Code);
            this.grvItemNew.Visible = true;
        }

    }

    private void ResetState(double loid)
    {
        // Get Data From LOID (existing)

        //ItemObj.ClearSession();
        //SetData(FlowObj.GetAllData(loid));

        if (loid != 0)
        {
            txtLOID.Text = loid.ToString();
            txtRequisitionCode.ReadOnly = true;
            txtRequisitionCode.CssClass = "zTextbox-View";
            btnSearch.Visible = false;
            //this.txtRefLoid = data.REFLOID;
            //SetDataPD(FlowObj.GetRefData(data.REFLOID));
        }
        ItemObj.ClearSession();
        SetData(FlowObj.GetData(loid));
    }

    private void ResetState1(double loid)
    {
        // Get Data From LOT (new one)

        ItemObj.ClearSession();
        SetData(FlowObj.GetData(loid));
    }

    #endregion

    #region Data

    private void SetData(StockinReturnData data)
    {
        // Setdata from Old REQUISITION
        if (data.RECEIVEDATE.Year == 1) data.RECEIVEDATE = DateTime.Now.Date;

        this.txtLOID.Text = data.LOID.ToString(); //######
        this.txtStatus.Text = (data.STATUS != "" ? data.STATUS : txtStatus.Text);
        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.Approved.Code ? Constz.Requisition.Status.Approved.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : (data.STATUS == Constz.Requisition.Status.QC.Code ? Constz.Requisition.Status.QC.Name : (data.STATUS == Constz.Requisition.Status.Finish.Code ? Constz.Requisition.Status.Finish.Name : Constz.Requisition.Status.Waiting.Name))));
        this.txtWareHouse.Text = (data.WAREHOUSE != 0 ? data.WAREHOUSE.ToString() : txtWareHouse.Text);
        this.txtCode.Text = data.CODE;
        this.txtRemark.Text = data.REMARK;
        this.txtRefLoid.Text = data.REFLOID.ToString();
        SetPDdata(data.REFLOID);

        SetGrvItem(this.txtStatus.Text);

        if (data.STATUS == Constz.Requisition.Status.Finish.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockinReturnMaterial, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
    }
    private void SetPDdata(double loid)
    {
        StockinReturnData data = FlowObj.GetPDData(loid);
        this.txtPDCode.Text = data.PDBARCODE;
        this.txtPDName.Text = data.PDNAME;
        this.txtQty.Text = data.QTY.ToString();
        this.txtUnit.Text = data.UNAME;
        this.txtRequisitionCode.Text = data.CODE;
        this.ctlReqdate.DateValue = data.REQDATE;
        this.txtPPLoid.Text = data.PPLOID.ToString();

    }

    private StockinReturnData GetData()
    {
        StockinReturnData data = new StockinReturnData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.DOCTYPE = Constz.DocType.RetMaterial.LOID;
        data.WAREHOUSE = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);
        data.REMARK = this.txtRemark.Text.Trim(); data.STATUS = this.txtStatus.Text.Trim();
        data.REFTABLE = "REQUISITION";
        data.REFLOID = Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text);
        data.RECEIVEDATE = ctlReceiveDate.DateValue;
        data.ITEM = ItemObj.GetItemList();
        data.STATUS = txtStatus.Text.Trim();
        data.CODE = txtCode.Text.Trim();
        data.RECEIVER = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);
        data.SENDER = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);

        return data;
    }


    #endregion

    #region GridView

    private void NewRowDataBound(GridViewRow gRow)
    {
        //ComboSource.BuildCombo((DropDownList)gRow.Cells[3].FindControl("cmbProductNew"), "STOCKINITEM", "PRODUCTNAME", "PDLOID", "PRODUCTNAME", "LOID = " + this.txtRefLoid.Text.Trim() + " ", "เลือก", "0");
        SetProduct((DropDownList)gRow.Cells[3].FindControl("cmbProductNew"));
        SetProductStock((DropDownList)gRow.Cells[4].FindControl("cmbLotNoNew"), Convert.ToDouble(((DropDownList)gRow.Cells[3].FindControl("cmbProductNew")).SelectedItem.Value));

       // ControlUtil.SetIntTextBox((TextBox)gRow.Cells[5].FindControl("txtNewQty"));
        //string script = "document.getElementById('" + ((TextBox)gRow.Cells[8].FindControl("txtNewNetPrice")).ClientID + "').value = ";
        //script += "document.getElementById('" + ((TextBox)gRow.Cells[5].FindControl("txtNewQty")).ClientID + "').value * ";
        //script += "document.getElementById('" + ((TextBox)gRow.Cells[7].FindControl("txtNewPrice")).ClientID + "').value";
        //((TextBox)gRow.Cells[4].FindControl("txtNewQty")).Attributes.Add("onchange", script);
    }

    #endregion

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            txtWareHouse.Text = Authz.CurrentUserInfo.Warehouse.ToString();
            txtCreateBy.Text = Authz.CurrentUserInfo.UserID;
            ctlReceiveDate.DateValue = DateTime.Today;


            string script = "";
            script += "document.getElementById('" + this.txtRefLoid.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopUpStockinReturnWH.aspx' + (document.getElementById('" + this.txtRequisitionCode.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtRequisitionCode.ClientID + "').value)), '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + this.txtRefLoid.ClientID + "').value || '' == document.getElementById('" + this.txtRefLoid.ClientID + "').value) ";
            script += "{ return false; } ";

            this.btnSearch.OnClientClick = script;
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันทำรายการใช่หรือไม่?');";

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetPDdata(Convert.ToDouble(this.txtRefLoid.Text.Trim()));
        ItemObj.ClearSession();
        SetGrvItem(this.txtStatus.Text);
    }

    private void SetProductDetail(StockinReturnItemData data, GridViewRow gRow, TextBox txtBarcode, DropDownList cmbProduct, DropDownList cmbLotNo, TextBox txtUnit, Label lblUnitName, TextBox txtPrice, TextBox txtQty, TextBox txtRef)
    {
        txtBarcode.Text = data.BARCODE;
        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.PRODUCT.ToString()));
        txtUnit.Text = data.UNIT.ToString();
        lblUnitName.Text = data.UNITNAME;
        txtPrice.Text = data.PRICE.ToString(Constz.DblFormat);
        txtQty.Text = data.QTY.ToString(Constz.IntFormat);
        txtRef.Text = data.REFLOID.ToString();
        SetProductStock(cmbLotNo, data.PRODUCT);


        //if (txtBarcode.ID == "txtBarcodeNew" && txtBarcode.Text != "")
        //    InsertData(gRow);
    }

    #region grvItem "Insert"

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvItemNew.Rows[0]);
        }
    }

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NewRowDataBound(e.Row);
        }
    }

    protected void txtBarcodeNew_TextChanged(object sender, EventArgs e)
    {
        txtBarcode_TextChanged((TextBox)sender, this.grvItemNew.Rows[0], "cmbProductNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
    }

    protected void cmbProductNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbProduct_SelectedIndexChanged((DropDownList)sender, this.grvItemNew.Rows[0], "txtBarcodeNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
    }
    protected void cmbLotNoNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbLotNo_SelectedIndexChanged((DropDownList)sender, this.grvItemNew.Rows[0], "txtBarcodeNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
    }

    private void txtBarcode_TextChanged(TextBox txtBarcode, GridViewRow gRow, string ctlProductName, string ctlLotNoName, string ctlQtyName, string ctlUnitName, string ctlUnitNameName, string ctlPriceName, string ctlRefLOIDName)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl(ctlProductName);
        TextBox txtQty = (TextBox)gRow.Cells[5].FindControl(ctlQtyName);
        TextBox txtUnit = (TextBox)gRow.Cells[6].FindControl(ctlUnitName);
        Label lblUnitName = (Label)gRow.Cells[6].FindControl(ctlUnitNameName);
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl(ctlPriceName);
        TextBox txtRefLOID = (TextBox)gRow.Cells[6].FindControl(ctlRefLOIDName);
        DropDownList cmbLotNo = (DropDownList)gRow.Cells[4].FindControl(ctlLotNoName);


        //if (FlowObj.GetReqItemProductData(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text), Convert.ToDouble(this.txtPD.Text == "" ? "0" : this.txtPD.Text), txtBarcode.Text.Trim(), cmbDocType.SelectedValue))
        //{
        StockinReturnItemData data = FlowObj.GetReqmaterialBarcode(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text), txtBarcode.Text.Trim());
        SetProductDetail(data, gRow, txtBarcode, cmbProduct, cmbLotNo, txtUnit, lblUnitName, txtPrice, txtQty, txtRefLOID);
        //}
    }

    private void cmbProduct_SelectedIndexChanged(DropDownList cmbProduct, GridViewRow gRow, string ctlBarcodeName, string ctlLotNoName, string ctlQtyName, string ctlUnitName, string ctlUnitNameName, string ctlPriceName, string ctlRefLOIDName)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[2].FindControl(ctlBarcodeName);
        TextBox txtQty = (TextBox)gRow.Cells[5].FindControl(ctlQtyName);
        TextBox txtUnit = (TextBox)gRow.Cells[6].FindControl(ctlUnitName);
        Label lblUnitName = (Label)gRow.Cells[6].FindControl(ctlUnitNameName);
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl(ctlPriceName);
        TextBox txtRefLOID = (TextBox)gRow.Cells[6].FindControl(ctlRefLOIDName);
        DropDownList cmbLotNo = (DropDownList)gRow.Cells[4].FindControl(ctlLotNoName);


        StockinReturnItemData data = FlowObj.GetReqmaterialProduct(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text), Convert.ToDouble(cmbProduct.SelectedItem.Value));

        SetProductDetail(data, gRow, txtBarcode, cmbProduct, cmbLotNo, txtUnit, lblUnitName, txtPrice, txtQty, txtRefLOID);

    }

    private void cmbLotNo_SelectedIndexChanged(DropDownList cmbLotNo, GridViewRow gRow, string ctlBarcodeName, string ctlProduct, string ctlQtyName, string ctlUnitName, string ctlUnitNameName, string ctlPriceName, string ctlRefLOIDName)
    {
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl(ctlPriceName);
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl(ctlProduct);
        double price = FlowObj.GetPrice(cmbLotNo.SelectedValue, Convert.ToDouble(cmbProduct.SelectedValue));
        txtPrice.Text = price.ToString(Constz.DblFormat);
    }
    #endregion

    #region grvItem

    private void SetProductStock(DropDownList cmbLotNo, double product)
    {
        DataTable dt = FlowObj.GetProductStock(Convert.ToDouble(this.txtPPLoid.Text == "" ? "0" : this.txtPPLoid.Text), product);
        cmbLotNo.Items.Clear();
        cmbLotNo.DataSource = dt;
        cmbLotNo.DataTextField = "LOTNO";
        cmbLotNo.DataValueField = "LOTNO";
        cmbLotNo.DataBind();
        cmbLotNo.Items.Insert(0, new ListItem("เลือก", ""));
    }

    private void SetProduct(DropDownList cmbProduct)
    {
        DataTable dt = FlowObj.GetProduct(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text));
        cmbProduct.Items.Clear();
        cmbProduct.DataSource = dt;
        cmbProduct.DataTextField = "PRODUCTNAME";
        cmbProduct.DataValueField = "PRODUCT";
        cmbProduct.DataBind();
        cmbProduct.Items.Insert(0, new ListItem("เลือก", "0"));
    }


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
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProduct");
                DropDownList cmbLotNo = (DropDownList)e.Row.Cells[4].FindControl("cmbLotNo");

               // ComboSource.BuildCombo(cmbProduct, "V_MATERIAL_RETURN_POPUP_LIST", "PRODUCTNAME", "PDLOID", "PRODUCTNAME", "LOID = " + this.txtRefLoid.Text.Trim() + " ");
                SetProduct(cmbProduct);  
                SetProductStock(cmbLotNo, Convert.ToDouble(drow["PRODUCT"]));

                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                cmbLotNo.SelectedIndex = cmbLotNo.Items.IndexOf(cmbLotNo.Items.FindByValue(drow["LOTNO"].ToString()));

               // ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[5].FindControl("txtQty"));
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                imbDelete.Attributes.Add("onclick", "return confirm('ยืนยันการลบสินค้า?');");
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            NewRowDataBound(e.Row);
        }
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        TextBox txtBarcode = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txtBarcode.Parent.Parent).RowIndex;
        txtBarcode_TextChanged(txtBarcode, this.grvItem.Rows[rowIndex], "cmbProduct", "cmbLotNo", "txtQty", "txtUnit", "lblUnitName", "txtPrice", "txtRefLOID");
    }

    protected void txtBarcodeNew_TextChanged1(object sender, EventArgs e)
    {
        TextBox txtBarcode = (TextBox)sender;
        txtBarcode_TextChanged(txtBarcode, this.grvItem.FooterRow, "cmbProductNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmbProduct = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmbProduct.Parent.Parent).RowIndex;
        cmbProduct_SelectedIndexChanged(cmbProduct, this.grvItem.Rows[rowIndex], "txtBarcode", "cmbLotNo", "txtQty", "txtUnit", "lblUnitName", "txtPrice", "txtRefLOID");
    }
    protected void cmbLotNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmbLotNo = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmbLotNo.Parent.Parent).RowIndex;
        cmbLotNo_SelectedIndexChanged(cmbLotNo, this.grvItem.Rows[rowIndex], "txtBarcode", "cmbProduct", "txtQty", "txtUnit", "lblUnitName", "txtPrice", "txtRefLOID");
    }

    protected void cmbProductNew_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmbProduct = (DropDownList)sender;
        cmbProduct_SelectedIndexChanged(cmbProduct, this.grvItem.FooterRow, "txtBarcodeNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
    }
    protected void cmbLotNoNew_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmbLotNo = (DropDownList)sender;
        cmbLotNo_SelectedIndexChanged(cmbLotNo, this.grvItem.FooterRow, "txtBarcodeNew", "cmbProductNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
    }

    protected void grvItem_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        e.ExceptionHandled = (e.Exception != null);
        if (e.ExceptionHandled)
        {
            e.KeepInEditMode = true;
            Appz.ClientAlert(this, e.Exception.InnerException.Message);
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

        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtBarcode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("txtBarcode");
        DropDownList cmbLotNo = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("cmbLotNo");
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("cmbProduct");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtQty");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtUnit");
        Label lblUnitName = (Label)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("lblUnitName");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtPrice");
        TextBox txtRefLOID = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtRefLOID");
        StockinReturnItemData data = new StockinReturnItemData();
        data.BARCODE = txtBarcode.Text.Trim();
        data.LOTNO = cmbLotNo.SelectedItem.Value;
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.REFLOID = Convert.ToDouble(txtRefLOID.Text == "" ? "0" : txtRefLOID.Text);
        data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
        data.UNITNAME = lblUnitName.Text.Trim();

        e.NewValues["BARCODE"] = data.BARCODE;
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["LOTNO"] = data.LOTNO;
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["UNIT"] = data.UNIT.ToString();
        e.NewValues["PRICE"] = data.PRICE.ToString();
        e.NewValues["UNITNAME"] = data.UNITNAME;
        e.NewValues["REFLOID"] = data.REFLOID;

    }

    private void InsertData(GridViewRow gRow)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[2].FindControl("txtBarcodeNew");
        DropDownList cmbLotNo = (DropDownList)gRow.Cells[4].FindControl("cmbLotNoNew");
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl("cmbProductNew");
        TextBox txtQty = (TextBox)gRow.Cells[6].FindControl("txtQtyNew");
        TextBox txtUnit = (TextBox)gRow.Cells[7].FindControl("txtUnitNew");
        Label lblUnitName = (Label)gRow.Cells[7].FindControl("lblUnitNameNew");
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl("txtPriceNew");
        TextBox txtRefLOID = (TextBox)gRow.Cells[7].FindControl("txtRefLOIDNew");

        StockinReturnItemData data = new StockinReturnItemData();
        data.BARCODE = txtBarcode.Text.Trim();
        data.LOTNO = cmbLotNo.SelectedItem.Value;
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.REFLOID = Convert.ToDouble(txtRefLOID.Text == "" ? "0" : txtRefLOID.Text);
        data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
        data.UNITNAME = lblUnitName.Text.Trim();
 //       data.REQUISITION = Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text);

        if (ItemObj.InsertItem(data))
        {
            SetGrvItem(this.txtStatus.Text);

        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    #endregion

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockinReturnSearch.aspx");
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
        StockinReturnData data = GetData();
        data.STATUS = Constz.Requisition.Status.Finish.Code;
        data.APPROVER = FlowObj.GetApprover(Authz.CurrentUserInfo.UserID);
        data.APPROVEDATE = DateTime.Now.Date;
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ยืนยันทำรายการเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    #endregion

    #endregion
}
