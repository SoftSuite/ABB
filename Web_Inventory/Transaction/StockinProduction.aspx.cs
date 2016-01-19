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
using ABB.Data.Inventory;
using ABB.Data.Inventory.FG;
using ABB.Flow;
using ABB.Flow.Sales;
using ABB.Flow.Inventory;
using ABB.Global;

public partial class Transaction_StockinProduction : System.Web.UI.Page
{
    #region Variables & Properties

    private StockinProductionFlow _flow;
    private StockinProductItem item;

    public StockinProductionFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockinProductionFlow(); return _flow; }
    }

    public StockinProductItem ItemObj
    {
        get { if (item == null) item = new StockinProductItem(); return item; }
    }

    #endregion

    #region Methods

    #region GridView


    private void NewRowDataBound(GridViewRow gRow)
    {
        //ComboSource.BuildCombo((DropDownList)gRow.Cells[3].FindControl("cmbNewProduct"), "V_PRODUCT_RETURNREQUEST", "NAME", "LOID", "NAME", "", "เลือก", "0");
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[4].FindControl("txtNewPDQty"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[5].FindControl("txtNewQty"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[6].FindControl("txtNewUnit"));
        string script = "document.getElementById('" + ((TextBox)gRow.Cells[5].FindControl("txtNewQty")).ClientID + "').value * ";
        ((TextBox)gRow.Cells[4].FindControl("txtNewQty")).Attributes.Add("onchange", script);
    }

    #endregion

    #region Data

    private void SetData(StockinProductData data)
    {
        if (data.LOID == 0)
        {
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.CODE = "";
            data.STCREATEON = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
        }
        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtSender.Text = "";
        this.txtSenderCode.Text = "";
        this.txtSenderName.Text = Constz.ProductionDepartment.Name;
        this.txtStockinCode.Text = data.CODE;
        this.ctlReserveDate.DateValue = data.STCREATEON;
        this.txtRemark.Text = data.REMARK;
        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.Approved.Code ? Constz.Requisition.Status.Approved.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : (data.STATUS == Constz.Requisition.Status.Finish.Code ? Constz.Requisition.Status.Finish.Name : Constz.Requisition.Status.Waiting.Name)));

        SetToolbar(data.STATUS);
        SetGrvItem(data.STATUS);


        if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;

            if (data.STATUS == Constz.Requisition.Status.QC.Code || data.STATUS == Constz.Requisition.Status.Finish.Code)
            {
                this.ctlToolbar.BtnSubmitShow = false;
            }
        }
        string producetype = Request["producetype"];

        if (producetype == Constz.ProductType.Type.WH.Code)
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockinProductWH, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        else
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockInProduction, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";

    }

    private StockinProductData GetData()
    {
        StockinProductData data = new StockinProductData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.CODE = this.txtCode.Text;
        data.SENDER = Convert.ToDouble(this.txtSender.Text == "" ? "0" : this.txtSender.Text);
        data.RECEIVER = Authz.CurrentUserInfo.Warehouse;
        data.ITEM = ItemObj.GetItemList();
        data.REMARK = this.txtRemark.Text.Trim();
        data.STCREATEON = this.ctlReserveDate.DateValue;
        data.STATUS = this.txtStatus.Text.Trim();
        data.DOCTYPE = Constz.DocType.DelProduct.LOID;
        return data;
    }

    #endregion

    #region Others

    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        if (grvItem.Rows.Count > 0)
        {
            this.grvItem.ShowFooter = (status == Constz.Requisition.Status.Waiting.Code);
            this.grvItem.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code || status == Constz.Requisition.Status.Approved.Code);
            this.grvItem.Visible = true;
            this.grvItemNew.Visible = false;
        }
        else
        {
            this.grvItem.Visible = (status != Constz.Requisition.Status.Waiting.Code);
            this.grvItemNew.Visible = (status == Constz.Requisition.Status.Waiting.Code);
        }
    }
    private void SetSender(double sender)
    {
        SupplierData data = FlowObj.GetSenderData(sender);
        this.txtSenderCode.Text = data.CODE;
        this.txtSenderName.Text = data.NAME;

    }
    private void SetCustomerData(double customer, bool isSearch)
    {

    }
    private void SetToolbar(string status)
    {
        if (status == Constz.Requisition.Status.Approved.Code)
        {
            this.ctlToolbar.BtnSubmitShow = false;
        }
        else if (status == Constz.Requisition.Status.QC.Code || status == Constz.Requisition.Status.Finish.Code)
        {
            this.ctlToolbar.BtnSubmitShow = false;
        }
    }
    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        StockinProductData data = FlowObj.GetData(loid);
        SetData(data);
    }

    #endregion

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["producetype"] == Constz.ProductType.Type.WH.Code)
            {
                this.lblHeader.Text = Constz.DocType.DelRaw.NAME;
            }
            else
            {
                this.lblHeader.Text = Constz.DocType.DelProduct.NAME;
            }
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetCustomerData(Convert.ToDouble(this.txtSender.Text == "" ? "0" : this.txtSender.Text), true);
    }

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/StockinProductionSearch.aspx?producetype=" + Request["producetype"]);
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
        StockinProductData data = GetData();
        data.STATUS = Constz.Requisition.Status.Finish.Code;
        if (FlowObj.CommitQCData(Authz.CurrentUserInfo.UserID, data))
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
            ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[6].FindControl("txtNewQty"));

            ImageButton btnNewSearch = (ImageButton)e.Row.Cells[2].FindControl("btnNewSearch");
            TextBox txtNewBarCode = (TextBox)e.Row.Cells[2].FindControl("txtNewBarCode");
            TextBox txtGetData = (TextBox)e.Row.Cells[2].FindControl("txtGetData");
            TextBox txtRefLoid = (TextBox)e.Row.Cells[2].FindControl("txtRefLoid");
            TextBox txtNewProduct = (TextBox)e.Row.Cells[4].FindControl("txtNewProduct");
            TextBox txtNewPDQTY = (TextBox)e.Row.Cells[5].FindControl("txtNewPDQTY");//
            TextBox txtNewQty = (TextBox)e.Row.Cells[6].FindControl("txtNewQty");
            TextBox txtNewUnit = (TextBox)e.Row.Cells[7].FindControl("txtNewUnit");
            TextBox txtNewMfgdate = (TextBox)e.Row.Cells[3].FindControl("txtNewMfgdate");
            TextBox txtProductLOID = (TextBox)e.Row.Cells[2].FindControl("txtProductLOID");
            TextBox txtUnitLOID = (TextBox)e.Row.Cells[2].FindControl("txtUnitLOID");
            TextBox txtPDPLOID = (TextBox)e.Row.Cells[2].FindControl("txtPDPLOID");
            txtNewProduct.Attributes.Add("readonly", "readonly");
            txtNewPDQTY.Attributes.Add("readonly", "readonly");
            txtNewUnit.Attributes.Add("readonly", "readonly");
            txtNewMfgdate.Attributes.Add("readonly", "readonly");
            txtNewBarCode.Attributes.Add("readonly", "readonly");
            string script = "";
            script += "var tmp = OpenNewModalDialog('" + Constz.HomeFolder + "Transaction/PopupProductSearch.aspx?producetype=" + Request["producetype"] + "' + (document.getElementById('" + txtNewBarCode.ClientID + "').value == '' ? '' : '&code=' + escape(document.getElementById('" + txtNewBarCode.ClientID + "').value) + '&warehouse=" + Authz.CurrentUserInfo.Warehouse + "'), '600', '550'); ";
            script += "if ('undefined' ==  tmp || '' == tmp ) ";
            //script += "if ('undefined' ==  document.getElementById('" + txtGetData.ClientID + "').value || '' == document.getElementById('" + txtGetData.ClientID + "').value) ";
            script += "{ return false; } ";
            script += "else { ";
            script += "var sData = tmp.split('|'); ";
            script += "document.getElementById('" + txtNewProduct.ClientID + "').value = sData[0];";
            script += "document.getElementById('" + txtNewBarCode.ClientID + "').value = sData[1];";
            script += "document.getElementById('" + txtNewPDQTY.ClientID + "').value = sData[2];";
            script += "document.getElementById('" + txtNewUnit.ClientID + "').value = sData[3];";
            script += "document.getElementById('" + txtRefLoid.ClientID + "').value = sData[4];";
            script += "document.getElementById('" + txtNewMfgdate.ClientID + "').value = sData[5];";
            script += "document.getElementById('" + txtProductLOID.ClientID + "').value = sData[6];";
            script += "document.getElementById('" + txtUnitLOID.ClientID + "').value = sData[7];";
            script += "document.getElementById('" + txtPDPLOID.ClientID + "').value = sData[8];";
            script += "return false;";
            script += "}";

            btnNewSearch.OnClientClick = script;
        }
    }

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            Int16 rowIndex = 0;
            TextBox txtProduct = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewProduct");
            TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewQty");
            TextBox txtBarCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewBarCode");
            TextBox txtProductName = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewProduct");
            TextBox txtUnitName = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewUnit");
            TextBox txtUnit = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewUnit");
            TextBox txtNewPDQTY = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewPDQTY");
            TextBox txtMfgdate = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtMfgdate");
            TextBox txtRefLoid = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtRefLoid");
            TextBox txtProductLOID = (TextBox)grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtProductLOID");
            TextBox txtUnitLOID = (TextBox)grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtUnitLOID");
            TextBox txtPDPLOID = (TextBox)grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtPDPLOID");
            TextBox txtMFGDate = (TextBox)grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewMfgdate");
            TextBox txtPDQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewPDQTY");

            StockinProductData data = new StockinProductData();
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.REFLOID = Convert.ToDouble(txtRefLoid.Text == "" ? "0" : txtRefLoid.Text);
            data.LOTNO = txtBarCode.Text.Trim();
            data.PRODUCT = Convert.ToDouble("0" + txtProductLOID.Text);
            data.UNIT = Convert.ToDouble("0" + txtUnitLOID.Text);
            data.PDPRODUCT = Convert.ToDouble("0" + txtPDPLOID.Text);
            data.PRODUCTNAME = txtProductName.Text.Trim();
            data.UNITNAME = txtUnitName.Text.Trim();
            data.MFGDATE = Convert.ToDateTime(txtMFGDate.Text == "" ? new DateTime(1, 1, 1).ToString() : txtMFGDate.Text);
            data.PDQTY = Convert.ToDouble(txtPDQty.Text == "" ? "0" : txtPDQty.Text);

            if (ItemObj.InsertStokInItem(data))
            {
                this.grvItem.DataBind();
                this.grvItemNew.DataBind();
                SetGrvItem(this.txtStatus.Text);
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void txtNewBarCode_TextChanged(object sender, EventArgs e)
    {
        //txtBarcode_TextChanged((TextBox)sender, this.grvItemNew.Rows[0], "cmbNewProduct", "txtNewPDQty", "txtNewQty", "txtNewUnit", "txtNewPrice", "txtNewNetPrice");
    }

    protected void cmbNewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        //cmbProduct_SelectedIndexChanged((DropDownList)sender, this.grvItemNew.Rows[0], "txtNewBarCode", "txtNewPDQty", "txtNewQty", "txtNewUnit", "txtNewPrice", "txtNewNetPrice");
    }

    #endregion

    #region grvItem

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[6].FindControl("txtQty"));

                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;

                TextBox txtProduct = (TextBox)e.Row.Cells[4].FindControl("txtProduct");
                TextBox txtUnit = (TextBox)e.Row.Cells[7].FindControl("txtUnit");
                TextBox txtPDQty = (TextBox)e.Row.Cells[5].FindControl("txtPDQty");
                TextBox txtQty = (TextBox)e.Row.Cells[6].FindControl("txtQty");

            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                //DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                //DropDownList cmbUnit = (DropDownList)e.Row.Cells[9].FindControl("cmbUnitView");

                //if (this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
                //    ComboSource.BuildCombo(cmbProduct, "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "");
                //else
                //    ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");

                //ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");
                //DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
                //cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                //cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[6].FindControl("txtNewQty"));

            ImageButton btnNewSearch = (ImageButton)e.Row.Cells[2].FindControl("btnNewSearch");
            TextBox txtNewBarCode = (TextBox)e.Row.Cells[2].FindControl("txtNewBarCode");
            TextBox txtGetData = (TextBox)e.Row.Cells[2].FindControl("txtGetData");
            TextBox txtRefLoid = (TextBox)e.Row.Cells[2].FindControl("txtRefLoid");
            TextBox txtNewProduct = (TextBox)e.Row.Cells[4].FindControl("txtNewProduct");
            TextBox txtNewMfgdate = (TextBox)e.Row.Cells[3].FindControl("txtNewMfgdate");
            TextBox txtNewPDQty = (TextBox)e.Row.Cells[6].FindControl("txtNewPDQty");
            TextBox txtNewUnit = (TextBox)e.Row.Cells[7].FindControl("txtNewUnit");
            TextBox txtProductLOID = (TextBox)e.Row.Cells[2].FindControl("txtProductLOID");
            TextBox txtUnitLOID = (TextBox)e.Row.Cells[2].FindControl("txtUnitLOID");
            TextBox txtPDPLOID = (TextBox)e.Row.Cells[2].FindControl("txtPDPLOID");
            txtNewProduct.Attributes.Add("readonly", "readonly");
            txtNewPDQty.Attributes.Add("readonly", "readonly");
            txtNewUnit.Attributes.Add("readonly", "readonly");
            txtNewMfgdate.Attributes.Add("readonly", "readonly");
            txtNewBarCode.Attributes.Add("readonly", "readonly");

            string script = "";
            //script += "var tmp = OpenNewModalDialog('" + Constz.HomeFolder + "Transaction/PopupProductSearch.aspx' + (document.getElementById('" + txtNewBarCode.ClientID + "').value == '' ? '' : '?code=' + document.getElementById('" + txtNewBarCode.ClientID + "').value), '600', '550'); alert(tmp); document.getElementById('" + txtGetData.ClientID + "').value = tmp;";
            script += "var tmp = OpenNewModalDialog('" + Constz.HomeFolder + "Transaction/PopupProductSearch.aspx?producetype=" + Request["producetype"] + "' + (document.getElementById('" + txtNewBarCode.ClientID + "').value == '' ? '' : '&code=' + escape(document.getElementById('" + txtNewBarCode.ClientID + "').value) + '&warehouse=" + Authz.CurrentUserInfo.Warehouse + "'), '600', '550'); document.getElementById('" + txtGetData.ClientID + "').value = tmp;";
            script += "if ('undefined' ==  document.getElementById('" + txtGetData.ClientID + "').value || '' == document.getElementById('" + txtGetData.ClientID + "').value) ";
            script += "{ return false; } ";
            script += "else{ ";
            script += "var sData = tmp.split('|'); ";
            script += "document.getElementById('" + txtNewProduct.ClientID + "').value = sData[0];";
            script += "document.getElementById('" + txtNewBarCode.ClientID + "').value = sData[1];";
            script += "document.getElementById('" + txtNewPDQty.ClientID + "').value = sData[2];";
            script += "document.getElementById('" + txtNewUnit.ClientID + "').value = sData[3];";
            script += "document.getElementById('" + txtRefLoid.ClientID + "').value = sData[4];";
            script += "document.getElementById('" + txtNewMfgdate.ClientID + "').value = sData[5];";
            script += "document.getElementById('" + txtProductLOID.ClientID + "').value = sData[6];";
            script += "document.getElementById('" + txtUnitLOID.ClientID + "').value = sData[7];";
            script += "document.getElementById('" + txtPDPLOID.ClientID + "').value = sData[8];";
            script += "return false;";
            script += "};";

            btnNewSearch.OnClientClick = script;
        }
    }

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            TextBox txtProduct = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewProduct");
            TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewQty");
            TextBox txtBarCode = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewBarCode");
            TextBox txtProductName = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewProduct");
            TextBox txtUnitName = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewUnit");
            TextBox txtUnit = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewUnit");
            TextBox txtRefLoid = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtRefLoid");
            TextBox txtProductLOID = (TextBox)grvItem.FooterRow.Cells[2].FindControl("txtProductLOID");
            TextBox txtUnitLOID = (TextBox)grvItem.FooterRow.Cells[2].FindControl("txtUnitLOID");
            TextBox txtPDPLOID = (TextBox)grvItem.FooterRow.Cells[2].FindControl("txtPDPLOID");
            TextBox txtMFGDate = (TextBox)grvItem.FooterRow.Cells[3].FindControl("txtNewMfgdate");
            TextBox txtPDQty = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtNewPDQTY");

            StockinProductData data = new StockinProductData();
            //data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.LOTNO = txtBarCode.Text.Trim();
            data.REFLOID = Convert.ToDouble(txtRefLoid.Text == "" ? "0" : txtRefLoid.Text);
            data.PRODUCT = Convert.ToDouble("0" + txtProductLOID.Text);
            data.UNIT = Convert.ToDouble("0" + txtUnitLOID.Text);
            data.PDPRODUCT = Convert.ToDouble("0" + txtPDPLOID.Text);
            data.PRODUCTNAME = txtProductName.Text.Trim();
            data.UNITNAME = txtUnitName.Text.Trim();
            data.MFGDATE = Convert.ToDateTime(txtMFGDate.Text == "" ? new DateTime(1, 1, 1).ToString() : txtMFGDate.Text);
            data.PDQTY = Convert.ToDouble(txtPDQty.Text == "" ? "0" : txtPDQty.Text);

            if (ItemObj.InsertStokInItem(data))
            {
                SetGrvItem(this.txtStatus.Text);

            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        //TextBox txt = (TextBox)sender;
        //Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        //txtBarcode_TextChanged(txt, this.grvItem.Rows[rowIndex], "cmbProduct", "txtPDQty", "txtQty", "txtUnit", "txtPrice", "txtNetPrice");
    }

    protected void txtNewBarCode_TextChanged1(object sender, EventArgs e)
    {
        //TextBox txt = (TextBox)sender;
        //txtBarcode_TextChanged(txt, this.grvItem.FooterRow, "cmbNewProduct", "txtNewPDQty", "txtNewQty", "txtNewUnit", "txtNewPrice", "txtNewNetPrice");
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
        TextBox txtProduct = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtProduct");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtQty");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtUnit");

        StockInItemData data = new StockInItemData();
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);

        e.NewValues["LOID"] = this.grvItem.Rows[e.RowIndex].Cells[10].Text;
        //e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["QTY"] = data.QTY.ToString();
    }

    #endregion

    #endregion
}

