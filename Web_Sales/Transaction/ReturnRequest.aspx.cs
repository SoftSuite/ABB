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

public partial class Transaction_ReturnRequest : System.Web.UI.Page
{
    #region Variables & Properties

    private ReturnRequestFlow _flow;
    private RequisitionRequestItem item;
    private int indexBUTTON = 0;
    private int indexRANK = 1;
    private int indexBARCODE = 2;
    private int indexPRODUCT = 3;
    private int indexQTY = 4;
    private int indexUNIT = 5;
    private int indexPRICE = 6;
    private int indexNETPRICE = 8;
    private int indexDISCOUNT = 9;
    private int indexOLDQTY = 10;
    private int indexOLDDISCOUNT = 7;
    private int indexLOID = 11;
    private int indexREFLOID = 12;

    public ReturnRequestFlow FlowObj
    {
        get { if (_flow == null) _flow = new ReturnRequestFlow(); return _flow; }
    }

    public RequisitionRequestItem ItemObj
    {
        get { if (item == null) item = new RequisitionRequestItem(); return item; }
    }

    #endregion

    #region Methods

    #region Others

    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();
        this.grvItem.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code || status == Constz.Requisition.Status.Approved.Code);
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        SetData(FlowObj.GetData(loid));
    }

    private void Calculate()
    {
        double oldTotal = Convert.ToDouble(this.txtOldTotal.Text == "" ? "0" : this.txtOldTotal.Text);
        ItemObj.GetCorrectValue(Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text));
        double total = ItemObj.CORRECTVALUE;
        double vat = Convert.ToDouble(txtVat.Text);
        this.txtGrandTotal.Text = total.ToString(Constz.DblFormat);
        this.txtDifference.Text = ((total * 100) / (100 + vat)).ToString(Constz.DblFormat);
        this.txtTotalVat.Text = (total-((total * 100) / (100 + vat))).ToString(Constz.DblFormat);
        this.txtTotal.Text = (oldTotal - ((total * 100) / (100 + vat))).ToString(Constz.DblFormat);
        this.txtTotalDiscount.Text = ItemObj.DISCOUNT.ToString();


    }

    #endregion

    #region Data

    private void SetData(InvoiceRequestData data)
    {
        if (data.LOID == 0)
        {
            data.REQDATE = DateTime.Today;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
        }
        this.txtLOID.Text = data.LOID.ToString();
        this.txtAddress.Text = data.CADDRESS;
        this.txtFax.Text = data.CFAX;
        this.txtLastName.Text = data.CLASTNAME;
        this.txtName.Text = data.CNAME;
        this.txtRequisitionCode.Text = data.CODE;
        this.txtCreateBy.Text = data.CREATEBY;
        this.txtTel.Text = data.CTEL;
        this.cmbTitle.SelectedIndex = this.cmbTitle.Items.IndexOf(this.cmbTitle.Items.FindByValue(data.CTITLE.ToString()));
        this.txtCustomer.Text = data.CUSTOMER.ToString();
        this.txtGrandTotal.Text = data.GRANDTOT.ToString(Constz.DblFormat);
        this.txtOldTotal.Text = data.OLDTOTAL.ToString(Constz.DblFormat);
        this.txtReason.Text = data.REASON;
        this.txtRemark.Text = data.REMARK;
        this.txtRefLoid.Text = data.REFLOID.ToString();
        this.ctlReqDate.DateValue = data.REQDATE;
        this.txtStatus.Text = data.STATUS;
        this.txtStatusName.Text = Appz.GetStatusName(data.STATUS);
        this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
        this.txtTotalDiscount.Text = data.TOTDIS.ToString(Constz.DblFormat);
        this.txtTotalVat.Text = data.TOTVAT.ToString(Constz.DblFormat);
        this.txtVat.Text = data.VAT.ToString(Constz.IntFormat);
        this.txtWareHouse.Text = data.WAREHOUSE.ToString();
        this.txtDifference.Text = data.TOTDIS.ToString(Constz.DblFormat);
        this.txtInvoicecode.Text = data.INVCODE;
        this.txtCustomerCode.Text = data.CUSTOMERCODE;
        this.txtCustomerName.Text = data.CUSTOMERNAME;

        SetGrvItem(data.STATUS);

        if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ReturnRequest, data.LOID) + " return false;";
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
        data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
        data.REASON = this.txtReason.Text.Trim();
        data.REFLOID = Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text);
        data.REFTABLE = "REQUISITION";
        data.REMARK = this.txtRemark.Text.Trim();
        data.REQDATE = this.ctlReqDate.DateValue;
        data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ05;
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.TOTDIS = Convert.ToDouble(this.txtDifference.Text == "" ? "0" : this.txtDifference.Text);
        data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
        data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
        data.WAREHOUSE = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);
        data.STATUS = this.txtStatus.Text;
        data.ITEM = ItemObj.GetItemList();

        return data;
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
            this.ctlToolbar.ClientClickSubmit = "return confirm('ต้องการส่งคลังสำเร็จรูปใช่หรือไม่?');";

            string script = "";
            script += "document.getElementById('" + this.txtRefLoid.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/InvoiceRequestSearch.aspx?invoice=' + (document.getElementById('" + this.txtLOID.ClientID + "').value == '' ? '0' : document.getElementById('" + this.txtLOID.ClientID + "').value), '550', '500' ,'yes'); ";
            script += "if (document.getElementById('" + this.txtRefLoid.ClientID + "').value == 'undefined') { document.getElementById('" + this.txtRefLoid.ClientID + "').value = '0'; return false; }";
            this.btnSearch.OnClientClick = script;
        }
     }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        InvoiceRequestData data = FlowObj.GetInvoiceData(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text));
        data.REMARK = this.txtRemark.Text.Trim();
        data.REASON = this.txtReason.Text.Trim();
        data.REFLOID = data.LOID;
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtRequisitionCode.Text.Trim();
        data.CREATEBY = this.txtCreateBy.Text.Trim();
        data.REQDATE = this.ctlReqDate.DateValue;
        data.STATUS = this.txtStatus.Text.Trim();
        data.TOTAL = data.OLDTOTAL;
        data.TOTVAT = 0;
        data.TOTDIS = 0;
        data.GRANDTOT = 0;
        ItemObj.SetInvoiceItem(data.OLDITEMS);
        SetData(data);
        Calculate();
    }

    #region grvItem

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;

            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[indexQTY].FindControl("txtQty"));
            }
            else
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[indexBUTTON].FindControl("imbDelete");
                imbDelete.OnClientClick = "return confirm('ต้องการลบรายการสินค้า " + drow["PRODUCTNAME"].ToString() + "?');";
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
            Calculate();
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
            Calculate();
        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        e.NewValues["LOID"] = "0";
        e.NewValues["RANK"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexRANK].FindControl("lblNo")).Text.Trim();
        e.NewValues["BARCODE"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexBARCODE].FindControl("lblBarcode")).Text.Trim();
        e.NewValues["PRODUCT"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexPRODUCT].FindControl("lblProduct")).Text.Trim();
        e.NewValues["PRODUCTNAME"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexPRODUCT].FindControl("lblProductName")).Text.Trim();
        e.NewValues["QTY"] = ((TextBox)this.grvItem.Rows[e.RowIndex].Cells[indexQTY].FindControl("txtQty")).Text.Trim();
        e.NewValues["UNIT"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexUNIT].FindControl("lblUnit")).Text.Trim();
        e.NewValues["UNITNAME"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexUNIT].FindControl("lblUnitName")).Text.Trim();
        e.NewValues["PRICE"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexPRICE].FindControl("lblPrice")).Text.Trim();
        e.NewValues["NETPRICE"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexNETPRICE].FindControl("lblNetPrice")).Text.Trim();
        e.NewValues["DISCOUNT"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexDISCOUNT].FindControl("lblDiscount")).Text.Trim();
        e.NewValues["OLDQTY"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexOLDQTY].FindControl("lblOldQty")).Text.Trim();
        e.NewValues["OLDDISCOUNT"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexOLDDISCOUNT].FindControl("lblOldDiscount")).Text.Trim();
        e.NewValues["REFLOID"] = ((Label)this.grvItem.Rows[e.RowIndex].Cells[indexREFLOID].FindControl("lblRefLOID")).Text.Trim();
    }

    #endregion

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ReturnRequestSearch.aspx");
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
        if (FlowObj.SubmitReturnRequisition(Authz.CurrentUserInfo.UserID, GetData()))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ส่งคลังเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    #endregion

    #endregion

}
