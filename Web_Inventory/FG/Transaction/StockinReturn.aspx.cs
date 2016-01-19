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
using ABB.Global;
using ABB.Data.Inventory.FG;
using ABB.Data.Sales;
using ABB.Flow;
using ABB.Flow.Inventory.FG;

public partial class FG_Transaction_StockinReturn : System.Web.UI.Page
{
    private StockinReturnFlow _flow;
    public StockinReturnFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockinReturnFlow(); return _flow; }
    }

    private StockinReturnItem item;
    public StockinReturnItem ItemObj
    {
        get { if (item == null) item = new StockinReturnItem(); return item; }
    }

    private void Calculation()
    {
        double price = 0;
        foreach (DataRow dRow in ItemObj.GetItem(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text), this.txtStatus.Text).Rows)
        {
            double itmPrice = Convert.ToDouble(dRow["NETPRICE"]);
            price += itmPrice;
        }
        double vatcal = Convert.ToDouble(txtVat.Text == "" ? "0" : txtVat.Text);
        this.txtGrandTotal.Text = price.ToString(Constz.DblFormat);
        this.txtTotal.Text = Convert.ToDouble((price * (100 - vatcal)) / 100).ToString(Constz.DblFormat);
        double vat = price - Convert.ToDouble((price * (100 - vatcal)) / 100);
        this.txtTotalVat.Text = vat.ToString(Constz.DblFormat);
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
        
        StockinReturnData data = FlowObj.GetData(loid);
        if (loid == 0)
        {
            data.RECEIVEDATE = DateTime.Now.Date;
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
        }
        SetData(data);
    }

    private void SetData(StockinReturnData data)
    {
        if (data.LOID != 0)
            this.cmbDocType.Enabled = false;
        else
            this.cmbDocType.Enabled = true;
        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtSender.Text = data.SENDER.ToString();

        this.cmbDocType.SelectedIndex = this.cmbDocType.Items.IndexOf(this.cmbDocType.Items.FindByValue(data.DOCTYPE.ToString()));
        //print
        if (data.DOCTYPE.ToString() == Constz.DocType.RetInReduce.LOID.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockinReturnPDRequest, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        else if (data.DOCTYPE.ToString() == Constz.DocType.RetDistribute.LOID.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.FGStockInReturnProduct, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        else if (data.DOCTYPE.ToString() == Constz.DocType.RetFair.LOID.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockInReturn, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        else if (data.DOCTYPE.ToString() == Constz.DocType.RetInSample.LOID.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockinReturnPDExam, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";

        DataTable dt = FlowObj.GetRefData(data.REFLOID.ToString());
        if (dt.Rows.Count > 0)
        {
            this.txtRefCode.Text = dt.Rows[0]["DOCCODE"].ToString();
        }
        this.txtCustomerCode.Text = "";
        this.txtCustomerName.Text = "";
        this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.CTITLE.ToString()));
        this.txtName.Text = data.CNAME;
        this.txtLastName.Text = data.CLASTNAME;
        this.txtAddress.Text = data.CADDRESS;
        this.txtTel.Text = data.CTEL;
        this.txtFax.Text = data.CFAX;

        this.txtCode.Text = data.CODE;
        this.ctlReceiveDate.DateValue = data.RECEIVEDATE;

        this.txtRemark.Text = data.REMARK;

        this.txtCreateBy.Text = data.CREATEBY;
        this.txtStatusName.Text = Appz.GetStatusName(data.STATUS);

        this.txtVat.Text = SysConfigFlow.GetValue(Constz.ConfigName.VAT);
        this.txtGrandTotal.Text = data.GRANDTOT.ToString(Constz.DblFormat);
        SetCustomerData(data.SENDER, false);

        SetGrvItem(data.STATUS);
        Calculation();

        if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
        {
            this.btnSearch.Visible = false;
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
    }

    private StockinReturnData GetData()
    {
        StockinReturnData data = new StockinReturnData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtCode.Text.Trim();
        data.RECEIVEDATE = this.ctlReceiveDate.DateValue;
        data.SENDER = Convert.ToDouble(this.txtSender.Text == "" ? "0" : this.txtSender.Text);
        data.DOCTYPE = Convert.ToDouble(this.cmbDocType.SelectedItem.Value);
        data.RECEIVER = Authz.CurrentUserInfo.Warehouse;
        data.STATUS = this.txtStatus.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.ITEM = ItemObj.GetItemList();
        data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CTITLE = Convert.ToDouble(this.cmbTitle.SelectedItem.Value);
        data.CNAME = this.txtName.Text.Trim();
        data.CLASTNAME = this.txtLastName.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        data.REFLOID = Convert.ToDouble(this.txtRefLOID.Text == "" ? "0" : this.txtRefLOID.Text);
        data.REFTABLE = this.txtRefTable.Text.Trim();
        return data;
    }

    private void ClearAllData()
    {
        this.txtRefCode.Text = "";
        this.txtRefLOID.Text = "0";
        this.txtRefTable.Text = "";
        this.txtSender.Text = "0";
        this.txtCustomerCode.Text = "";
        this.txtCustomerName.Text = "";
        this.cmbTitle.SelectedIndex = 0;
        this.txtName.Text = "";
        this.txtLastName.Text = "";
        this.txtAddress.Text = "";
        this.txtTel.Text = "";
        this.txtFax.Text = "";
        this.txtTotal.Text = "0.00";
        this.txtTotalVat.Text = "0.00";
        this.txtGrandTotal.Text = "0.00";
        ItemObj.DeleteItemAll();
        SetGrvItem(this.txtStatus.Text);
    }

    private void SetCustomerData(double sender, bool isSearch)
    {
        DataTable dt = FlowObj.GetCustomerData(sender.ToString());
        if (dt.Rows.Count > 0)
        {
            this.txtCustomerCode.Text = dt.Rows[0]["CODE"].ToString();
            this.txtCustomerName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
            this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(dt.Rows[0]["CTITLE"].ToString()));
            this.txtName.Text = dt.Rows[0]["CNAME"].ToString();
            this.txtLastName.Text = dt.Rows[0]["CLASTNAME"].ToString();
            this.txtAddress.Text = dt.Rows[0]["BILLADDRESSFULL"].ToString();
            this.txtTel.Text = dt.Rows[0]["BILLTEL"].ToString();
            this.txtFax.Text = dt.Rows[0]["BILLFAX"].ToString();
        }
    }

    private void SetWareHouseData(double sender, bool isSearch)
    {
        DataTable dt = FlowObj.GetWareHouseData(sender.ToString());
        if (dt.Rows.Count > 0)
        {
            this.txtCustomerCode.Text = dt.Rows[0]["CODE"].ToString();
            this.txtCustomerName.Text = dt.Rows[0]["NAME"].ToString();
            this.cmbTitle.SelectedIndex = 0;
            this.txtName.Text = "";
            this.txtLastName.Text = "";
            this.txtAddress.Text = "";
            this.txtTel.Text = "";
            this.txtFax.Text = "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbDocType, "V_RETURNTYPE_FG", "DOCNAME", "DOCTYPE", "SORTORDER", "");
            ComboSource.BuildCombo(this.cmbTitle, "TITLE", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }

        string script = "";
        script += "document.getElementById('" + this.txtSender.ClientID + "').value = OpenNewModalDialog('" + ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_SALES] + "Search/Customer.aspx' + (document.getElementById('" + this.txtCustomerCode.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtCustomerCode.ClientID + "').value)), '600', '550');";
        script += "if ('undefined' ==  document.getElementById('" + this.txtSender.ClientID + "').value || '' == document.getElementById('" + this.txtSender.ClientID + "').value) ";
        script += "{ return false; } ";

        this.btnCustomerSearch.OnClientClick = script;

        script = "";
        script += "document.getElementById('" + this.txtRefLOID.ClientID + "').value = OpenNewModalDialog('PopupStockinReturn.aspx' + (document.getElementById('" + this.cmbDocType.ClientID + "').value == '' ? '' : '?type=' + escape(document.getElementById('" + this.cmbDocType.ClientID + "').value)), '600', '550');";
        script += "if ('undefined' ==  document.getElementById('" + this.txtRefLOID.ClientID + "').value || '' == document.getElementById('" + this.txtRefLOID.ClientID + "').value) ";
        script += "{ document.getElementById('" + this.txtRefLOID.ClientID + "').value = 0; return false; } ";

        this.btnSearch.OnClientClick = script;
    }

    protected void btnCustomerSearch_Click(object sender, ImageClickEventArgs e)
    {
        ItemObj.DeleteItemAll();
        SetGrvItem(this.txtStatus.Text);

        SetCustomerData(Convert.ToDouble(this.txtSender.Text == "" ? "0" : this.txtSender.Text), true);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        ItemObj.DeleteItemAll();
        SetGrvItem(this.txtStatus.Text);

        DataTable dt = FlowObj.GetRefData(txtRefLOID.Text);
        if (dt.Rows.Count > 0)
        {
            this.txtRefCode.Text = dt.Rows[0]["DOCCODE"].ToString();
            this.txtSender.Text = dt.Rows[0]["CUSTOMER"].ToString();
            this.txtRefTable.Text = dt.Rows[0]["REFTABLE"].ToString();
        }

        if (cmbDocType.SelectedIndex == 2)
        {
            SetWareHouseData(Convert.ToDouble(this.txtSender.Text == "" ? "0" : this.txtSender.Text), true);
        }
        else
        {
            SetCustomerData(Convert.ToDouble(this.txtSender.Text == "" ? "0" : this.txtSender.Text), true);
        }
    }

    protected void cmbDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAllData();
        DropDownList cmb = (DropDownList)sender;
        if (cmb.SelectedValue == "18")
        {
            btnSearch.Visible = false;
            btnCustomerSearch.Visible = true;
        }
        else
        {
            btnSearch.Visible = true;
            btnCustomerSearch.Visible = false;
        }
    }

    #region grvItemNew

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int16 rowIndex = 0;
        TextBox txtBarCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewBarCode");
        TextBox txtProduct = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewProduct");
        TextBox txtLotNo = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewLotNo");
        TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewQty");
        TextBox txtLostQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtLostNewQty");
        TextBox txtUnit = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewUnit");
        TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewPrice");
        TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("txtNewNetPrice");
        TextBox txtOldQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[10].FindControl("txtOldNewQty");

        if (e.CommandName == "Insert")
        {
            StockinReturnItemData data = new StockinReturnItemData();
            data.BARCODE = txtBarCode.Text;
            data.PDNAME = txtProduct.Text;
            data.LOTNO = txtLotNo.Text;
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.QTYLOST = Convert.ToDouble(txtLostQty.Text == "" ? "0" : txtLostQty.Text);
            data.UNITNAME = txtUnit.Text;
            data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
            data.NETPRICE = data.PRICE * (data.QTY + data.QTYLOST);
            data.OLDQTY = Convert.ToDouble(txtOldQty.Text == "" ? "0" : txtOldQty.Text);
            data.PRODUCT = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[12].Text);
            data.UNIT = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[13].Text);
            data.REFLOID = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[14].Text);
            data.REFTABLE = this.grvItemNew.Rows[rowIndex].Cells[15].Text;
            data.STATUS = this.grvItemNew.Rows[rowIndex].Cells[16].Text;

            if (ItemObj.InsertItem(data))
            {
                SetGrvItem(this.txtStatus.Text);
                Calculation();
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
        else if (e.CommandName == "Search")
        {
            TextBox txtGetData = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtGetData");
            string[] str = txtGetData.Text.Split(';');
            DataTable dt = new DataTable();
            if (cmbDocType.SelectedValue == Constz.DocType.RetInSample.LOID.ToString())
            {
                dt = FlowObj.GetReturnWaitListRefLoid(str[0], str[1]);
            }
            else
            {
                dt = FlowObj.GetViewReturnWaitList(str[0], str[1]);
            }
            if (dt.Rows.Count > 0)
            {
                txtBarCode.Text = dt.Rows[0]["BARCODE"].ToString();
                txtProduct.Text = dt.Rows[0]["PDNAME"].ToString();
                txtLotNo.Text = dt.Rows[0]["LOTNO"].ToString();
                txtQty.Text = dt.Rows[0]["QTY"].ToString();
                txtLostQty.Text = dt.Rows[0]["QTYLOST"].ToString();
                txtUnit.Text = dt.Rows[0]["UNAME"].ToString();
                txtPrice.Text = dt.Rows[0]["PRICE"].ToString();
                txtNetPrice.Text = dt.Rows[0]["NETPRICE"].ToString();
                txtOldQty.Text = dt.Rows[0]["OLDQTY"].ToString();
                this.grvItemNew.Rows[rowIndex].Cells[12].Text = str[0];
                this.grvItemNew.Rows[rowIndex].Cells[13].Text = dt.Rows[0]["ULOID"].ToString();
                this.grvItemNew.Rows[rowIndex].Cells[14].Text = dt.Rows[0]["REFLOID"].ToString();
                this.grvItemNew.Rows[rowIndex].Cells[15].Text = dt.Rows[0]["REFTABLE"].ToString();
                this.grvItemNew.Rows[rowIndex].Cells[16].Text = Constz.Requisition.Status.Waiting.Code;
            }
        }
    }

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnNewSearch = (ImageButton)e.Row.FindControl("btnNewSearch");
            TextBox txtGetData = (TextBox)e.Row.Cells[2].FindControl("txtGetData");
            TextBox txtLotNo = (TextBox)e.Row.Cells[4].FindControl("txtNewLotNo");

            if (cmbDocType.SelectedValue == Constz.DocType.RetInSample.LOID.ToString())
            {
                txtLotNo.CssClass = "zTextbox";
                txtLotNo.ReadOnly = false;
            }
            else
            {
                txtLotNo.CssClass = "zTextbox-View";
                txtLotNo.ReadOnly = true;
            }
            string script = "";
            script += "document.getElementById('" + txtGetData.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupStockinReturnSearch.aspx?REFLOID=' + document.getElementById('" + this.txtRefLOID.ClientID + "').value + '&CUSTOMER=' + document.getElementById('" + this.txtSender.ClientID + "').value + '&DOCTYPE=' + document.getElementById('" + this.cmbDocType.ClientID + "').value, '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + txtGetData.ClientID + "').value || '' == document.getElementById('" + txtGetData.ClientID + "').value) ";
            script += "{ return false; } ";

            btnNewSearch.OnClientClick = script;
        }
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
        TextBox txtLotNo = (TextBox)commandRow.Cells[4].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtLotNo" : "txtNewLotNo");
        TextBox txtQty = (TextBox)commandRow.Cells[5].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtQty" : "txtNewQty");
        TextBox txtLostQty = (TextBox)commandRow.Cells[6].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtLostQty" : "txtLostNewQty");
        TextBox txtUnit = (TextBox)commandRow.Cells[7].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtUnit" : "txtNewUnit");
        TextBox txtPrice = (TextBox)commandRow.Cells[8].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtPrice" : "txtNewPrice");
        TextBox txtNetPrice = (TextBox)commandRow.Cells[9].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtNetPrice" : "txtNewNetPrice");
        TextBox txtOldQty = (TextBox)commandRow.Cells[10].FindControl(commandRow.RowType == DataControlRowType.DataRow ? "txtOldQty" : "txtOldNewQty");

        if (e.CommandName == "Insert")
        {
            StockinReturnItemData data = new StockinReturnItemData();
            data.BARCODE = txtBarCode.Text;
            data.PDNAME = txtProduct.Text;
            data.LOTNO = txtLotNo.Text;
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.QTYLOST = Convert.ToDouble(txtLostQty.Text == "" ? "0" : txtLostQty.Text);
            data.UNITNAME = txtUnit.Text;
            data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
            data.NETPRICE = data.PRICE * (data.QTY+ data.QTYLOST);
            data.OLDQTY = Convert.ToDouble(txtOldQty.Text == "" ? "0" : txtOldQty.Text);
            data.PRODUCT = Convert.ToDouble(commandRow.Cells[12].Text);
            data.UNIT = Convert.ToDouble(commandRow.Cells[13].Text);
            data.REFLOID = Convert.ToDouble(commandRow.Cells[14].Text);
            data.REFTABLE = commandRow.Cells[15].Text;
            data.STATUS = commandRow.Cells[16].Text;

            if (ItemObj.InsertItem(data))
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
            string[] str = txtGetData.Text.Split(';');
            DataTable dt = new DataTable();
            if (cmbDocType.SelectedValue == Constz.DocType.RetInSample.LOID.ToString())
            {
                dt = FlowObj.GetReturnWaitListRefLoid(str[0], str[1]);
            }
            else
            {
                dt = FlowObj.GetViewReturnWaitList(str[0], str[1]);
            }
            if (dt.Rows.Count > 0)
            {
                txtBarCode.Text = dt.Rows[0]["BARCODE"].ToString();
                txtProduct.Text = dt.Rows[0]["PDNAME"].ToString();
                txtLotNo.Text = dt.Rows[0]["LOTNO"].ToString();
                txtQty.Text = dt.Rows[0]["QTY"].ToString();
                txtLostQty.Text = dt.Rows[0]["QTYLOST"].ToString();
                txtUnit.Text = dt.Rows[0]["UNAME"].ToString();
                txtPrice.Text = dt.Rows[0]["PRICE"].ToString();
                txtNetPrice.Text = dt.Rows[0]["NETPRICE"].ToString();
                txtOldQty.Text = dt.Rows[0]["OLDQTY"].ToString();
                commandRow.Cells[12].Text = str[0];
                commandRow.Cells[13].Text = dt.Rows[0]["ULOID"].ToString();
                commandRow.Cells[14].Text = dt.Rows[0]["REFLOID"].ToString();
                commandRow.Cells[15].Text = dt.Rows[0]["REFTABLE"].ToString();
                commandRow.Cells[16].Text = Constz.Requisition.Status.Waiting.Code;
            }
        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnNewSearch = (ImageButton)e.Row.Cells[2].FindControl("btnNewSearch");
            TextBox txtGetData = (TextBox)e.Row.Cells[2].FindControl("txtGetData");

            if (e.Row.RowType == DataControlRowType.Footer)
            {
            TextBox txtLotNo = (TextBox)e.Row.Cells[4].FindControl("txtNewLotNo");
                if (cmbDocType.SelectedValue == Constz.DocType.RetInSample.LOID.ToString())
                {
                    txtLotNo.CssClass = "zTextbox";
                    txtLotNo.ReadOnly = false;
                }
                else
                {
                    txtLotNo.CssClass = "zTextbox-View";
                    txtLotNo.ReadOnly = true;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit))
            {
                TextBox txtLotNo = (TextBox)e.Row.Cells[4].FindControl("txtLotNo");

                if (cmbDocType.SelectedValue == Constz.DocType.RetInSample.LOID.ToString())
                {
                    txtLotNo.CssClass = "zTextbox";
                    txtLotNo.ReadOnly = false;
                }
                else
                {
                    txtLotNo.CssClass = "zTextbox-View";
                    txtLotNo.ReadOnly = true;
                }
            }


            if (txtGetData != null)
            {
                string script = "";
                script += "document.getElementById('" + txtGetData.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupStockinReturnSearch.aspx?REFLOID=' + document.getElementById('" + this.txtRefLOID.ClientID + "').value + '&CUSTOMER=' + document.getElementById('" + this.txtSender.ClientID + "').value + '&DOCTYPE=' + document.getElementById('" + this.cmbDocType.ClientID + "').value, '600', '550');";
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
        TextBox txtBarCode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("txtBarCode");
        TextBox txtProduct = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("txtProduct");
        TextBox txtLotNo = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtLotNo");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("txtQty");
        TextBox txtLostQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtLostQty");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtUnit");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtPrice");
        TextBox txtOldQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[10].FindControl("txtOldQty");

        StockinReturnItemData data = new StockinReturnItemData();

        data.BARCODE = txtBarCode.Text;
        data.PDNAME = txtProduct.Text;
        data.LOTNO = txtLotNo.Text;
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.QTYLOST = Convert.ToDouble(txtLostQty.Text == "" ? "0" : txtLostQty.Text);
        data.UNITNAME = txtUnit.Text;
        data.PRICE = Convert.ToDouble(txtPrice.Text);
        data.NETPRICE = (data.QTY + data.QTYLOST)* data.PRICE;
        data.OLDQTY = Convert.ToDouble(txtOldQty.Text == "" ? "0" : txtOldQty.Text);

        e.NewValues["BARCODE"] = data.BARCODE;
        e.NewValues["PDNAME"] = data.PDNAME;
        e.NewValues["LOTNO"] = data.LOTNO;
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["QTYLOST"] = data.QTYLOST.ToString();
        e.NewValues["UNITNAME"] = data.UNITNAME;
        e.NewValues["PRICE"] = data.PRICE.ToString();
        e.NewValues["NETPRICE"] = data.NETPRICE.ToString();
        e.NewValues["OLDQTY"] = data.OLDQTY.ToString();
        if (this.grvItem.Rows[e.RowIndex].Cells[12].Text != "")
        {
            e.NewValues["PRODUCT"] = this.grvItem.Rows[e.RowIndex].Cells[12].Text;
            e.NewValues["UNIT"] = this.grvItem.Rows[e.RowIndex].Cells[13].Text;
            e.NewValues["REFLOID"] = this.grvItem.Rows[e.RowIndex].Cells[14].Text;
            e.NewValues["REFTABLE"] = this.grvItem.Rows[e.RowIndex].Cells[15].Text;
            e.NewValues["STATUS"] = this.grvItem.Rows[e.RowIndex].Cells[16].Text;
        }
        Calculation();
    }

    #endregion

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockinReturnSearch.aspx");
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
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }
}
