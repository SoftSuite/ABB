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
using ABB.DAL;
using ABB.DAL.Inventory;

public partial class FG_Transaction_Stockout : System.Web.UI.Page
{
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            return new SessionPageStatePersister(this);
        }
    }
    private StockOutFGFlow _flow;
    private StockOutFGItem item;

    public StockOutFGFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockOutFGFlow(); return _flow; }
    }

    public StockOutFGItem ItemObj
    {
        get { if (item == null) item = new StockOutFGItem(); return item; }
    }

    #region GridView

    private void SetProductStock(DropDownList cmbLotNo, double product)
    {
        DataTable dt = FlowObj.GetProductStock(Convert.ToDouble(this.txtRefWarehouse.Text == "" ? "0" : this.txtRefWarehouse.Text), product);
        cmbLotNo.Items.Clear();
        cmbLotNo.DataSource = dt;
        cmbLotNo.DataTextField = "LOTNO";
        cmbLotNo.DataValueField = "LOTNO";
        cmbLotNo.DataBind();
        cmbLotNo.Items.Insert(0, new ListItem("เลือก", ""));
    }

    private void SetProductDetail(StockOutFGItemData data, GridViewRow gRow, TextBox txtBarcode, DropDownList cmbProduct, DropDownList cmbLotNo, TextBox txtUnit, Label lblUnitName, TextBox txtPrice, TextBox txtQty, TextBox txtRef)
    {
        txtBarcode.Text = data.BARCODE;
        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.PRODUCT.ToString()));
        SetProductStock(cmbLotNo, data.PRODUCT);
        txtUnit.Text = data.UNIT.ToString();
        lblUnitName.Text = data.UNITNAME;
        txtPrice.Text = data.PRICE.ToString(Constz.DblFormat);
        txtQty.Text = data.QTY.ToString(Constz.IntFormat);
        txtRef.Text = data.REFLOID.ToString();

        //if (txtBarcode.ID == "txtBarcodeNew" && txtBarcode.Text != "")
        //    InsertData(gRow);
    }

    private void txtBarcode_TextChanged(TextBox txtBarcode, GridViewRow gRow, string ctlProductName, string ctlLotNoName, string ctlQtyName, string ctlUnitName, string ctlUnitNameName, string ctlPriceName, string ctlRefLOIDName)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl(ctlProductName);
        DropDownList cmbLotNo = (DropDownList)gRow.Cells[4].FindControl(ctlLotNoName);
        TextBox txtQty = (TextBox)gRow.Cells[5].FindControl(ctlQtyName);
        TextBox txtUnit = (TextBox)gRow.Cells[6].FindControl(ctlUnitName);
        Label lblUnitName = (Label)gRow.Cells[6].FindControl(ctlUnitNameName);
        TextBox txtPrice = (TextBox)gRow.Cells[6].FindControl(ctlPriceName);
        TextBox txtRefLOID = (TextBox)gRow.Cells[6].FindControl(ctlRefLOIDName);

        if (FlowObj.GetReqItemProductData(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text), txtBarcode.Text.Trim()))
        {
            StockOutFGItemData data = FlowObj.ReqItemProductData;
            SetProductDetail(data, gRow, txtBarcode, cmbProduct, cmbLotNo, txtUnit, lblUnitName, txtPrice, txtQty, txtRefLOID);
        }
    }

    private void cmbProduct_SelectedIndexChanged(DropDownList cmbProduct, GridViewRow gRow, string ctlBarcodeName, string ctlLotNoName, string ctlQtyName, string ctlUnitName, string ctlUnitNameName, string ctlPriceName, string ctlRefLOIDName)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[2].FindControl(ctlBarcodeName);
        DropDownList cmbLotNo = (DropDownList)gRow.Cells[4].FindControl(ctlLotNoName);
        TextBox txtQty = (TextBox)gRow.Cells[5].FindControl(ctlQtyName);
        TextBox txtUnit = (TextBox)gRow.Cells[6].FindControl(ctlUnitName);
        Label lblUnitName = (Label)gRow.Cells[6].FindControl(ctlUnitNameName);
        TextBox txtPrice = (TextBox)gRow.Cells[6].FindControl(ctlPriceName);
        TextBox txtRefLOID = (TextBox)gRow.Cells[6].FindControl(ctlRefLOIDName);

        if (FlowObj.GetReqItemProductData(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text), Convert.ToDouble(cmbProduct.SelectedItem.Value)))
        {
            StockOutFGItemData data = FlowObj.ReqItemProductData;
            SetProductDetail(data, gRow, txtBarcode, cmbProduct, cmbLotNo, txtUnit, lblUnitName, txtPrice, txtQty, txtRefLOID);
        }
    }
    protected void cmbLotNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        TextBox txtRemainQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[5].FindControl("txtRemainQty");
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[rowIndex].Cells[3].FindControl("cmbProduct");

        txtRemainQty.Text = FlowObj.GetRemainQTYStock(cmb.SelectedItem.Value,Convert.ToDouble(cmbProduct.SelectedValue)).ToString();



    }

    private void InsertData(GridViewRow gRow)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[2].FindControl("txtBarcodeNew");
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl("cmbProductNew");
        DropDownList cmbLotNo = (DropDownList)gRow.Cells[4].FindControl("cmbLotNoNew");
        TextBox txtQty = (TextBox)gRow.Cells[6].FindControl("txtQtyNew");
        TextBox txtUnit = (TextBox)gRow.Cells[7].FindControl("txtUnitNew");
        Label lblUnitName = (Label)gRow.Cells[7].FindControl("lblUnitNameNew");
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl("txtPriceNew");
        TextBox txtRefLOID = (TextBox)gRow.Cells[7].FindControl("txtRefLOIDNew");
        TextBox txtRemainQty = (TextBox)gRow.Cells[5].FindControl("txtRemainQtyNew");


        StockOutFGItemData data = new StockOutFGItemData();
        data.BARCODE = txtBarcode.Text;
        data.LOTNO = cmbLotNo.SelectedItem.Value;
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.REMAIN = Convert.ToDouble(txtRemainQty.Text == "" ? "0" : txtRemainQty.Text);
        data.REFLOID = Convert.ToDouble(txtRefLOID.Text == "" ? "0" : txtRefLOID.Text);
        data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
        data.UNITNAME = lblUnitName.Text.Trim();
        data.REQUISITION = Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text);

        if (ItemObj.InsertStockOutItem(data))
        {
            SetGrvItem(this.txtStatus.Text);
            Calculation();
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void NewRowDataBound(GridViewRow gRow)
    {
        ComboSource.BuildCombo((DropDownList)gRow.Cells[3].FindControl("cmbProductNew"), "PRODUCT", "NAME", "LOID", "NAME", "LOID IN (SELECT PRODUCT FROM REQUISITIONITEM WHERE REQUISITION = " + (this.txtRefLoid.Text == "" ? "0 " : this.txtRefLoid.Text) + ") ", "เลือก", "0");
        SetProductStock((DropDownList)gRow.Cells[4].FindControl("cmbLotNoNew"), Convert.ToDouble(((DropDownList)gRow.Cells[3].FindControl("cmbProductNew")).SelectedItem.Value));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[5].FindControl("txtQtyNew"));
    }

    #endregion

    #region Data

    private void SetData(StockoutFGData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        SetDocTypeName(data.DOCTYPE);
        this.txtRefNo.Text = data.REQUISITIONCODE;

        this.txtCustomerCode.Text = data.CUSTOMERCODE;
        this.txtCustomerName.Text = data.CUSTOMERNAME;
        this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.CTITLE.ToString()));
        this.txtName.Text = data.CNAME;
        this.txtLastName.Text = data.CLASTNAME;
        this.txtAddress.Text = data.CADDRESS;
        this.txtTel.Text = data.CTEL;
        this.txtFax.Text = data.CFAX;
        this.txtRefLoid.Text = data.REFLOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtStatusName.Text = Appz.GetStatusName(data.STATUS);
        this.txtSender.Text = data.SENDER.ToString();
        this.txtReceiver.Text = data.RECEIVER.ToString();
        this.txtCode.Text = data.CODE;
        this.ctlCreateOn.DateValue = data.CREATEON;
        this.ctlReserveDate.DateValue = data.RESERVEDATE;
        this.txtCreateBy.Text = data.CREATEBY;
        this.txtRemark.Text = data.REMARK;
        this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);

        this.btnSearch.Visible = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
        this.ctlToolbar.BtnSaveShow = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
        this.ctlToolbar.BtnSubmitShow = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
        SetPeriod();
        SetGrvItem(data.STATUS);

        //print

        if (data.DOCTYPE == Constz.DocType.ReqSupport.LOID)
            this.ctlToolbar.BtnPrintShow = false;
        else if (data.DOCTYPE == Constz.DocType.ReqFair.LOID)
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockOutBorrow, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";

        else if (data.DOCTYPE == Constz.DocType.ReqDistribute.LOID || data.DOCTYPE == Constz.DocType.ReqProduct.LOID || data.DOCTYPE == Constz.DocType.Reserve.LOID)
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockOut, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        
        else if (data.DOCTYPE == Constz.DocType.ReqOrgSupport.LOID)
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockOutSupport, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";

        else
            this.ctlToolbar.ClientClickPrint = "return false; ";

        if (data.DOCTYPE == Constz.DocType.ReqProduct.LOID)
        {
            this.txtCustomerCode.Text = data.WAREHOUSECODE;
            this.txtCustomerName.Text = data.WAREHOUSENAME;
        }
    }

    private StockoutFGData GetData()
    {
        StockoutFGData data = new StockoutFGData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text);
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        data.CLASTNAME = this.txtLastName.Text.Trim();
        data.CNAME = this.txtName.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CTITLE = Convert.ToDouble(this.cmbTitle.SelectedItem.Value);
        data.DOCTYPE = Convert.ToDouble(this.txtDocType.Text == "" ? "0" : this.txtDocType.Text);
        data.RECEIVER = Convert.ToDouble(this.txtReceiver.Text == "" ? "0" : this.txtReceiver.Text);
        data.REFLOID = Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text);
        data.REFTABLE = "REQUISITION";
        data.REMARK = this.txtRemark.Text.Trim();
        data.SENDER = Convert.ToDouble(this.txtSender.Text == "" ? "0" : this.txtSender.Text);
        data.STATUS = this.txtStatus.Text.Trim();
        data.ITEM = ItemObj.GetItemList();
        return data;
    }

    #endregion

    #region Others

    private void SetDocTypeName(double docType)
    {
        string ret = "";
        this.txtDocType.Text = docType.ToString();
        if (docType == Constz.DocType.Reserve.LOID)
            ret = Constz.DocType.Reserve.NAME;
        else if (docType == Constz.DocType.ReqSupport.LOID)
            ret = Constz.DocType.ReqSupport.NAME;
        else if (docType == Constz.DocType.ReqOrgSupport.LOID)
            ret = Constz.DocType.ReqOrgSupport.NAME;
        else if (docType == Constz.DocType.ReqFair.LOID)
            ret = Constz.DocType.ReqFair.NAME;
        else if (docType == Constz.DocType.ReqDistribute.LOID)
            ret = Constz.DocType.ReqDistribute.NAME;
        else if (docType == Constz.DocType.ReqProduct.LOID)
            ret = Constz.DocType.ReqProduct.NAME;
        txtDocTypeName.Text = ret;
    }

    private void Calculation()
    {
        double total = 0;
        ArrayList arr = ItemObj.GetItemList();
        for (int i = 0; i < arr.Count; ++i)
        {
            StockOutItemData data = (StockOutItemData)arr[i];
            total += (data.QTY * data.PRICE);
        }
        this.txtTotal.Text = total.ToString(Constz.DblFormat);
    }

    private void ResetState(double stockOut)
    {
        ItemObj.ClearSession();
        StockoutFGData data = FlowObj.GetData(stockOut);
        if (stockOut == 0)
        {
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.SENDER = Authz.CurrentUserInfo.Warehouse;
            data.CREATEON = DateTime.Now.Date;
        }
        SetData(data);
    }

    private void SetRequisition(double requisition)
    {
        StockOutFGReqData data = FlowObj.GetRequisitionData(requisition);
        this.txtRefWarehouse.Text = data.REFWAREHOUSE.ToString();
        this.txtRefLoid.Text = data.REQUISITION.ToString();
        this.txtRefNo.Text = data.REQUISITIONCODE;
        SetDocTypeName(data.DOCTYPE);
        if (data.DOCTYPE == Constz.DocType.ReqProduct.LOID)
        {
            this.txtReceiver.Text = data.WAREHOUSE.ToString();
            this.txtCustomerCode.Text = data.WAREHOUSECODE;
            this.txtCustomerName.Text = data.WAREHOUSENAME;
          //  this.txtName.Text = data.WAREHOUSENAME;
        }
        else
        {
            this.txtReceiver.Text = data.CUSTOMER.ToString();
            this.txtCustomerCode.Text = data.CUSTOMERCODE;
            this.txtCustomerName.Text = data.CUSTOMERNAME;
            this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.CTITLE.ToString()));
            this.txtAddress.Text = data.CADDRESS;
            this.txtTel.Text = data.CTEL;
            this.txtFax.Text = data.CFAX;
            this.txtName.Text = data.CNAME;
            this.txtLastName.Text = data.CLASTNAME;
        }
        this.ctlReserveDate.DateValue = data.RESERVEDATE;
        this.txtSender.Text = data.REFWAREHOUSE.ToString();
        ItemObj.CopyItem(data.REQUISITIONITEM);
        SetGrvItem(this.txtStatus.Text);
        SetPeriod();
        Calculation();
    }

    private void SetPeriod()
    {
        if (this.ctlReserveDate.DateValue.Year != 1)
        {
            string period = SysConfigFlow.GetValue(Constz.ConfigName.PERIOD);
            this.ctlDueDate.DateValue = this.ctlReserveDate.DateValue.AddDays(Convert.ToDouble(period == "" ? "0" : period));
        }
        else
            this.ctlDueDate.DateValue = new DateTime(1, 1, 1);
    }

    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        CheckSave();
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

    private bool CheckSave()
    {
        bool bRet = true;
        DataTable dtItem = FlowObj.GetReserveItemList(Convert.ToDouble(this.txtRefLoid.Text));
        int SaveType = ItemObj.CheckSave(dtItem); // 0 : เท่ากัน , 1 :Qty > allQty, 2 : Qty < allQty
        if (SaveType == 1)
        {
            bRet = false;
            this.ctlToolbar.ClientClickSave = "alert('จำนวนรวมของสินค้ามากกว่าใบขอเบิก');";
        }
        else if (SaveType == 2)
        {
            this.ctlToolbar.ClientClickSave = "return confirm('มีบางรายการที่จำนวนไม่ตรงกับที่ขอเบิก ต้องการทำรายการต่อหรือไม่?');";
        }
        else
            this.ctlToolbar.ClientClickSave = "return true;";
        return bRet;
    }

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbTitle, "TITLE", "NAME", "LOID", "NAME", "");
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        

        this.txtStatus.Text = Constz.Requisition.Status.Waiting.Code;
        this.txtCreateBy.Text = Authz.CurrentUserInfo.UserID;
        this.txtWarehouse.Text = Authz.CurrentUserInfo.Warehouse.ToString();
        this.txtRefWarehouse.Text = Authz.CurrentUserInfo.Warehouse.ToString();

        string scriptRefNo = "";
        scriptRefNo += "document.getElementById('" + this.txtRefLoid.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/RequestSearch.aspx', '600', '550');";
        scriptRefNo += "if ('undefined' ==  document.getElementById('" + this.txtRefLoid.ClientID + "').value || '' == document.getElementById('" + this.txtRefLoid.ClientID + "').value) ";
        scriptRefNo += "{ return false; } ";

        this.btnSearch.OnClientClick = scriptRefNo;
        this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการเบิกออกใช่หรือไม่?');";
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetRequisition(Convert.ToDouble(this.txtRefLoid.Text));
    }

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockoutSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (CheckSave())
        {
            if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
            {
                this.txtLOID.Text = FlowObj.LOID.ToString();
                ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
                Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (CheckSave())
        {
            StockoutFGData data = GetData();
            data.APPROVEDATE = DateTime.Today;
            data.APPROVER = Authz.CurrentUserInfo.OfficerID;
            if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
            {
                ResetState(FlowObj.LOID);
                Appz.ClientAlert(this, "เบิกออกเรียบร้อยแล้ว");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }

    #endregion

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
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;

        TextBox txtRemainQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtRemainQtyNew");
        DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("cmbProductNew");

        txtRemainQty.Text = FlowObj.GetRemainQTYStock(cmb.SelectedItem.Value,Convert.ToDouble(cmbProduct.SelectedValue)).ToString();
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
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProduct");
                DropDownList cmbLotNo = (DropDownList)e.Row.Cells[4].FindControl("cmbLotNo");
                ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "LOID IN (SELECT PRODUCT FROM REQUISITIONITEM WHERE REQUISITION = " + (this.txtRefLoid.Text == "" ? "0 " : this.txtRefLoid.Text) + ") ");
                SetProductStock(cmbLotNo, Convert.ToDouble(drow["PRODUCT"]));
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                cmbLotNo.SelectedIndex = cmbLotNo.Items.IndexOf(cmbLotNo.Items.FindByValue(drow["LOTNO"].ToString()));

                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[6].FindControl("txtQty"));
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                imbDelete.Attributes.Add("onclick", "return confirm('ยืนยันการลบสินค้า?');");

                //DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                //ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");
                //cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
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

    protected void cmbProductNew_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmbProduct = (DropDownList)sender;
        cmbProduct_SelectedIndexChanged(cmbProduct, this.grvItem.FooterRow, "txtBarcodeNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
    }
    protected void cmbLotNoNew_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        TextBox txtRemainQty = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtRemainQtyNew");
        DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[3].FindControl("cmbProductNew");

        txtRemainQty.Text = FlowObj.GetRemainQTYStock(cmb.SelectedItem.Value,Convert.ToDouble(cmbProduct.SelectedValue)).ToString();
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
        TextBox txtBarcode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("txtBarcode");
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("cmbProduct");
        DropDownList cmbLotNo = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("cmbLotNo");
        TextBox txtRemainQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("txtRemainQty");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtQty");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtUnit");
        Label lblUnitName = (Label)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("lblUnitName");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtPrice");
        TextBox txtRefLOID = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtRefLOID");
        StockOutFGItemData data = new StockOutFGItemData();
        data.BARCODE = txtBarcode.Text.Trim();
        data.LOTNO = cmbLotNo.SelectedItem.Value;
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.REMAIN = Convert.ToDouble(txtRemainQty.Text == "" ? "0" : txtRemainQty.Text);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.REFLOID = Convert.ToDouble(txtRefLOID.Text == "" ? "0" : txtRefLOID.Text);
        data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
        data.UNITNAME = lblUnitName.Text.Trim();

        e.NewValues["NO"] = "0";
        e.NewValues["BARCODE"] = data.BARCODE;
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["LOTNO"] = data.LOTNO;
        e.NewValues["REMAINQTY"] = data.REMAIN.ToString();
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["UNIT"] = data.UNIT.ToString();
        e.NewValues["PRICE"] = data.PRICE.ToString();
        e.NewValues["UNITNAME"] = data.UNITNAME;
        e.NewValues["REFLOID"] = data.REFLOID;
        e.NewValues["REQUISITION"] = (this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text);
    }

    #endregion

    #endregion

}
