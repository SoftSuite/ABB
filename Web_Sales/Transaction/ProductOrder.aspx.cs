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

public partial class Transaction_ProductOrder : System.Web.UI.Page
{
    #region Variables & Properties

    private ProductOrderFlow _flow;
    private RequisitionItemOrder item;

    public ProductOrderFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductOrderFlow(); return _flow; }
    }

    public RequisitionItemOrder ItemObj
    {
        get { if (item == null) item = new RequisitionItemOrder(); return item; }
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
        SetData(FlowObj.GetData(loid));
    }

    #endregion

    #region Data

    private void SetData(ProductOrderData data)
    {
        if (data.LOID == 0)
        {
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            this.txtCreateBy.Text = Authz.CurrentUserInfo.UserID;
            this.ctlReqDate.DateValue = DateTime.Now.Date;
            this.txtWarehouse.Text = Authz.CurrentUserInfo.Warehouse.ToString();
        }
        else
        {
            this.ctlReqDate.DateValue = data.REQDATE;
            this.txtCreateBy.Text = data.CREATEBY;
            this.txtWarehouse.Text = data.WAREHOUSE.ToString();
        }
        this.txtLOID.Text = data.LOID.ToString();
        this.txtRequisitionCode.Text = data.CODE;
        this.txtRemark.Text = data.REMARK;
        this.txtStatus.Text = data.STATUS;
        this.txtStatusName.Text = Appz.GetStatusName(data.STATUS);
        SetGrvItem(data.STATUS);

        if (data.STATUS == Constz.Requisition.Status.Approved.Code || data.STATUS == Constz.Requisition.Status.Void.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductOrder, data.LOID) + " return false;";
    }

    private ProductOrderData GetData()
    {
        ProductOrderData data = new ProductOrderData();
        data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ07;
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.CODE = this.txtRequisitionCode.Text.Trim();
        data.ITEM = ItemObj.GetItemList();
        data.REMARK = this.txtRemark.Text.Trim();
        data.REQDATE = this.ctlReqDate.DateValue;
        data.STATUS = this.txtStatus.Text.Trim();
        data.CUSTOMER = Constz.ProductionDepartment.LOID;
        data.WAREHOUSE = Convert.ToDouble(this.txtWarehouse.Text == "" ? "0" : this.txtWarehouse.Text);
        return data;
    }

    #endregion

    #region GridView

    private void SetProductDetail(ProductSearchData data, GridViewRow gRow, TextBox txtBarcode, DropDownList cmbProduct, DropDownList cmbUnit, Controls_DatePickerControl txtDate)
    {
        txtBarcode.Text = data.BARCODE;
        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.LOID.ToString()));
        cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        txtDate.DateValue = DateTime.Now.Date.AddDays(data.LEADTIMEPD);

        //if (txtBarcode.ID == "txtNewBarCode" && txtBarcode.Text != "")
        //    InsertData(gRow);
    }

    private void txtBarcode_TextChanged(GridViewRow gRow, TextBox txt, string ctlProductName, string ctlUnitName, string ctlDate)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl(ctlProductName);
        DropDownList cmbUnit = (DropDownList)gRow.Cells[5].FindControl(ctlUnitName);
        Controls_DatePickerControl txtDate = (Controls_DatePickerControl)gRow.Cells[6].FindControl(ctlDate);
        ProductSearchData data = FlowObj.GetProductData(txt.Text.Trim());
        SetProductDetail(data, gRow, txt, cmbProduct, cmbUnit, txtDate);
    }

    private void cmbProduct_SelectedIndexChanged(GridViewRow gRow, DropDownList cmb, string ctlBarcodeName, string ctlUnitName, string ctlDate)
    {
        TextBox txtCode = (TextBox)gRow.Cells[2].FindControl(ctlBarcodeName);
        DropDownList cmbUnit = (DropDownList)gRow.Cells[5].FindControl(ctlUnitName);
        Controls_DatePickerControl txtDate = (Controls_DatePickerControl)gRow.Cells[6].FindControl(ctlDate);
        ProductSearchData data = FlowObj.GetProductBarcode(Convert.ToDouble(cmb.SelectedItem.Value));
        SetProductDetail(data, gRow, txtCode, cmb, cmbUnit, txtDate);
    }

    private void InsertData(GridViewRow gRow)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl("cmbNewProduct");
        TextBox txtQty = (TextBox)gRow.Cells[4].FindControl("txtNewQty");
        DropDownList cmbUnit = (DropDownList)gRow.Cells[5].FindControl("cmbNewUnit");
        RequisitionItemData data = new RequisitionItemData();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.DISCOUNT = 0;
        data.PRICE = 0;
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.UNIT = Convert.ToDouble(cmbUnit.SelectedValue);
        data.NETPRICE = data.QTY * data.PRICE;
        data.DUEDATE = ((Controls_DatePickerControl)gRow.Cells[6].FindControl("txtNewDate")).DateValue;

        if (ItemObj.InsertRequisitionItem(data))
        {
            SetGrvItem(this.txtStatus.Text);
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void NewRowDataBound(GridViewRow gRow)
    {
        ComboSource.BuildCombo((DropDownList)gRow.Cells[3].FindControl("cmbNewProduct"), "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "", "เลือก", "0");
        ComboSource.BuildCombo((DropDownList)gRow.Cells[5].FindControl("cmbNewUnit"), "UNIT", "NAME", "LOID", "NAME", "", "เลือก", "0");
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[4].FindControl("txtNewQty"));
    }

    #endregion

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtCustomerCode.Text = Constz.ProductionDepartment.Code;
            this.txtCustomerName.Text = Constz.ProductionDepartment.Name;
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งฝ่ายผลิต?');";

        }
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

    protected void txtNewBarCode_TextChanged(object sender, EventArgs e)
    {
        txtBarcode_TextChanged(this.grvItemNew.Rows[0], (TextBox)sender, "cmbNewProduct", "cmbNewUnit", "txtNewDate");
    }

    protected void cmbNewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbProduct_SelectedIndexChanged(this.grvItemNew.Rows[0], (DropDownList)sender, "txtNewBarCode", "cmbNewUnit", "txtNewDate");
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
                DropDownList cmbUnit = (DropDownList)e.Row.Cells[5].FindControl("cmbUnit");
                Controls_DatePickerControl txtDate = (Controls_DatePickerControl)e.Row.Cells[6].FindControl("txtDate");
                ComboSource.BuildCombo(cmbProduct, "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "");
                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));

                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[4].FindControl("txtQty"));
                if (drow["DUEDATE"] != null)
                    txtDate.DateValue = Convert.ToDateTime(drow["DUEDATE"]);
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                imbDelete.Attributes.Add("onclick", "return confirm('ยืนยันการลบสินค้า?');");
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                DropDownList cmbUnit = (DropDownList)e.Row.Cells[5].FindControl("cmbUnitView");
                ComboSource.BuildCombo(cmbProduct, "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "");
                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            NewRowDataBound(e.Row);
        }
    }

    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        txtBarcode_TextChanged(this.grvItem.Rows[rowIndex], txt, "cmbProduct", "cmbUnit", "txtDate");
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        cmbProduct_SelectedIndexChanged(this.grvItem.Rows[rowIndex], cmb, "txtBarCode", "cmbUnit", "txtDate");
    }

    protected void txtNewBarCode_TextChanged1(object sender, EventArgs e)
    {
        txtBarcode_TextChanged(this.grvItem.FooterRow, (TextBox)sender, "cmbNewProduct", "cmbNewUnit", "txtNewDate");
    }

    protected void cmbNewProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        cmbProduct_SelectedIndexChanged(this.grvItem.FooterRow, (DropDownList)sender, "txtNewBarCode", "cmbNewUnit", "txtNewDate");
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
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("cmbProduct");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtQty");
        DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("cmbUnit");
        Controls_DatePickerControl txtDate = (Controls_DatePickerControl)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtDate");
        RequisitionItemData data = new RequisitionItemData();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.UNIT = Convert.ToDouble(cmbUnit.SelectedItem.Value);
        data.DUEDATE = txtDate.DateValue;

        e.NewValues["LOID"] = this.grvItem.Rows[e.RowIndex].Cells[8].Text;
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["UNIT"] = data.UNIT.ToString();
        e.NewValues["DUEDATE"] = data.DUEDATE;

    }

    #endregion

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductOrderSearch.aspx");
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
        ProductOrderData data = GetData();
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ส่งฝ่ายผลิตเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    #endregion

    #endregion
}

