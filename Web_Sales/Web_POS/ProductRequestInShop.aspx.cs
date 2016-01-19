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
using ABB.DAL;
using ABB.Flow.Sales;
using ABB.Global;

public partial class Web_POS_ProductRequestInShop : System.Web.UI.Page
{
    private ProductRequestInFlow _flow;
    private SaleFlow _sFlow;
    private RequisitionItemReserve item;
    private int indexCHECk = 0;
    private int indexRANK = 1;
    private int indexBARCODE = 2;
    private int indexPRODUCTNAME = 3;
    private int indexQTY = 4;
    private int indexUNIT = 5;
    private int indexPRICE = 6;
    private int indexNETPRICE = 7;
    private int indexSTOCKQTY = 8;
    private int indexNORMALDISCOUNT = 9;
    private int indexISVAT = 10;
    private int indexLOID = 11;
    private int indexPRODUCT = 12;

    public ProductRequestInFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductRequestInFlow(); return _flow; }
    }

    private SaleFlow SaleObj
    {
        get { if (_sFlow == null) _sFlow = new SaleFlow(); return _sFlow; }
    }

    private RequisitionItemReserve ItemObj
    {
        get { if (item == null) item = new RequisitionItemReserve(); return item; }
    }

    private void SetCheckBoxScript()
    {
        string script = "";
        if (this.grvItem.Rows.Count > 0)
        {
            CheckBox chkAll = (CheckBox)this.grvItem.HeaderRow.Cells[0].FindControl("chkAll");
            foreach (GridViewRow gRow in this.grvItem.Rows)
            {
                CheckBox chkItem = (CheckBox)gRow.Cells[0].FindControl("chkItem");
                script += "if (document.getElementById('" + chkItem.ClientID + "').disabled == '') document.getElementById('" + chkItem.ClientID + "').checked = document.getElementById('" + chkAll.ClientID + "').checked;";
            }
            chkAll.Attributes.Add("onclick", script);
        }
    }

    #region Data

    private void SetData(ProductOrderData data)
    {
        if (data.LOID == 0)
        {
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
            data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
            data.RESERVEDATE = DateTime.Today;
            data.REFWAREHOUSE = Constz.ReadyMadeDepartment.LOID;
            WarehouseDAL itemDAL = new WarehouseDAL();
            itemDAL.GetDataByLOID(Authz.CurrentUserInfo.Warehouse, null);
            data.WAREHOUSENAME = itemDAL.NAME;
        }
        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtRequisitionCode.Text = data.CODE;
        this.ctlReqDate.DateValue = data.RESERVEDATE.Year == 1 ? DateTime.Now : data.RESERVEDATE;
        this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
        this.txtRemark.Text = data.REMARK;
        this.txtCreateBy.Text = data.CREATEBY;
        this.txtStatusName.Text = Appz.GetStatusName(data.STATUS);
        this.txtWarehouse.Text = data.WAREHOUSE.ToString();
        this.cmbRefWarehouse.SelectedIndex = this.cmbRefWarehouse.Items.IndexOf(this.cmbRefWarehouse.Items.FindByValue(data.REFWAREHOUSE.ToString()));
        this.txtCustomer.Text = data.CUSTOMER.ToString();
        this.txtWarehouseName.Text = data.WAREHOUSENAME;
        this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
        this.txtTotalDiscount.Text = data.TOTDIS.ToString(Constz.DblFormat);
        this.txtVat.Text = data.VAT.ToString();
        this.txtTotalVat.Text = data.TOTVAT.ToString(Constz.DblFormat);
        this.txtGrandTotal.Text = data.GRANDTOT.ToString(Constz.DblFormat);
        this.txtNet.Text = data.GRANDTOT.ToString(Constz.IntFormat);
        //if (this.cmbRefWarehouse.SelectedItem.Value == "0")
        //{
        //    data.REFWAREHOUSE = Constz.ReadyMadeDepartment.LOID;
        //    this.cmbRefWarehouse.SelectedIndex = this.cmbRefWarehouse.Items.IndexOf(this.cmbRefWarehouse.Items.FindByValue(data.REFWAREHOUSE.ToString()));
        //}

        SetGrvItem(data.STATUS);

        if (data.STATUS == Constz.Requisition.Status.Approved.Code || data.STATUS == Constz.Requisition.Status.Void.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
        this.grvItem.Columns[indexCHECk].Visible = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductRequestInShop, data.LOID) + " return false; ";
        this.ctlToolbarItem.Visible = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
    }

    private ProductOrderData GetData()
    {
        ProductOrderData data = new ProductOrderData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.CODE = this.txtRequisitionCode.Text.Trim();
       // data.CUSTOMER = Constz.ReadyMadeDepartment.LOID;
        data.CUSTOMER = Convert.ToDouble(this.txtWarehouse.Text == "" ? "0" : this.txtWarehouse.Text);
        data.ITEM = ItemObj.GetItemList();
        data.REMARK = this.txtRemark.Text.Trim();
        data.RESERVEDATE = this.ctlReqDate.DateValue;
        data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ06;
        data.STATUS = this.txtStatus.Text.Trim();
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.WAREHOUSE = Convert.ToDouble(this.txtWarehouse.Text == "" ? "0" : this.txtWarehouse.Text);
        data.REFWAREHOUSE = Convert.ToDouble(this.cmbRefWarehouse.SelectedItem.Value);
        data.CUSTOMER = Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text);
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.TOTDIS = Convert.ToDouble(this.txtTotalDiscount.Text == "" ? "0" : this.txtTotalDiscount.Text);
        data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
        data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
        data.WAREHOUSENAME = this.txtWarehouseName.Text.Trim();
        return data;
    }

    #endregion

    #region Others

    //private void CalculateTotal()
    //{
    //    this.txtTotal.Text = ItemObj.CalculateTotal().ToString(Constz.DblFormat);
    //}

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvItem.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvItem.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvItem.Rows[i].Cells[indexCHECk].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvItem.Rows[i].Cells[indexLOID].Text)); }
        }
        return arrLOID;
    }

    private void CalculateDiscount()
    {
        ItemObj.CalculateDiscount(Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text), Convert.ToDouble(this.txtVat.Text));
        this.txtTotal.Text = ItemObj.TOTAL.ToString(Constz.DblFormat);
        this.txtTotalDiscount.Text = ItemObj.TOTALDISCOUNT.ToString(Constz.DblFormat);
        this.txtTotalVat.Text = ItemObj.TOTALVAT.ToString(Constz.DblFormat);
        this.txtGrandTotal.Text = ItemObj.GRANDTOTAL.ToString(Constz.DblFormat);
        this.txtNet.Text = ItemObj.GRANDTOTAL.ToString(Constz.IntFormat);
        this.txtDiscount.Text = ItemObj.DISCOUNTPERCENT.ToString(Constz.IntFormat);
        this.grvItem.DataBind();
        SetCheckBoxScript();
    }
    
    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();
        SetCheckBoxScript();
        this.txtSelectProduct.Text = "";
        foreach (GridViewRow gRow in this.grvItem.Rows)
        {
            this.txtSelectProduct.Text += (this.txtSelectProduct.Text == "" ? "" : ", ") + gRow.Cells[indexPRODUCT].Text;
        }
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        SetData(FlowObj.GetData(loid));
    }

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string script = "";
            script += "document.getElementById('" + this.txtProduct.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/Product.aspx?product=' + document.getElementById('" + this.txtSelectProduct.ClientID + "').value, '600', '450');";
            script += "if ('undefined' !=  document.getElementById('" + this.txtProduct.ClientID + "').value && '' != document.getElementById('" + this.txtProduct.ClientID + "').value) return true; else {;  ";
            script += "document.getElementById('" + this.txtProduct.ClientID + "').value = ''; return false; }";
            this.ctlToolbarItem.ClientClickNew = script;

            ControlUtil.SetIntTextBox(this.txtDiscount);
            ControlUtil.SetIntTextBox(this.txtVat);
            ComboSource.BuildCombo(this.cmbRefWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            if (this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
            {
                CalculateDiscount();
            }
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันส่งคลังสำเร็จรูปใช่หรือไม่?');";
            this.ctlToolbarItem.ClientClickDelete = "return confirm('ต้องการลบรายการสินค้าที่เลือกใช่หรือไม่?');";
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Web_POS/ProductRequestInShopSearch.aspx");
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
            Appz.ClientAlert(this, "ส่งคลังสำเร็จรูปเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    #region ToolbarItem

    protected void ItemNewClick(object sender, EventArgs e)
    {
        if (this.txtProduct.Text != "")
        {
            DataTable dt = SaleObj.GetProductPromotionList(this.txtProduct.Text, Convert.ToDouble(this.txtWarehouse.Text));
            ItemObj.InsertRequisitionItem(dt);
            CalculateDiscount();
            SetGrvItem(this.txtStatus.Text);
        }
    }

    protected void ItemDeleteClick(object sender, EventArgs e)
    {
        ItemObj.DeleteRequisitionItem(GetChecked());
        CalculateDiscount();
        SetGrvItem(this.txtStatus.Text);
    }

    #endregion

    #region grvItem

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    CheckBox chk = (CheckBox)e.Row.Cells[indexCHECk].FindControl("chkAll");
        //    chk.Attributes.Add("onclick", CheckAll);
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtQty = (TextBox)e.Row.Cells[indexQTY].FindControl("txtQty");
            ControlUtil.SetIntTextBox(txtQty);
            if (this.txtStatus.Text != Constz.Requisition.Status.Waiting.Code)
            {
                txtQty.ReadOnly = true;
                txtQty.CssClass = "zTextboxR-View";
            }
        }
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        TextBox txtQty = (TextBox)sender;
        ItemObj.UpdateRequisition(Convert.ToDouble(((GridViewRow)txtQty.Parent.Parent).Cells[indexLOID].Text), Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text));
        CalculateDiscount();
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        CalculateDiscount();
    }
    protected void txtVat_TextChanged(object sender, EventArgs e)
    {
        CalculateDiscount();
    }
    #endregion

    #endregion
}
