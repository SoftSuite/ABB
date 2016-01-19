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
using ABB.Flow.Inventory.FG;

public partial class FG_Transaction_StockoutBasket : System.Web.UI.Page
{
    private StockoutBasketFlow _flow;
    public StockoutBasketFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockoutBasketFlow(); return _flow; }
    }

    private BasketItem item;
    public BasketItem ItemObj
    {
        get { if (item == null) item = new BasketItem(); return item; }
    }

    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        if (grvItem.Rows.Count > 0)
        {
            this.grvItem.ShowFooter = (status == Constz.Basket.Status.Waiting.Code);
            this.grvItem.Columns[0].Visible = (status == Constz.Basket.Status.Waiting.Code);
            this.grvItem.Visible = true;
            this.grvItemNew.Visible = false;
        }
        else
        {
            this.grvItem.Visible = (status != Constz.Basket.Status.Waiting.Code);
            this.grvItemNew.Visible = (status == Constz.Basket.Status.Waiting.Code);
        }
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        StockoutBasketData data = FlowObj.GetData(loid);
        if (loid == 0)
        {
            data.CODE = "";
            data.CHECKDATE = DateTime.Now.Date;
            data.STATUS = Constz.Basket.Status.Waiting.Code;
            data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
        }
        SetData(data);
    }

    private void SetData(StockoutBasketData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtStatus.Text = data.STATUS;
        this.txtPDLOID.Text = data.PRODUCT.ToString();
        this.txtWareHouse.Text = data.WAREHOUSE.ToString();

        this.cmbBasketType.SelectedIndex = this.cmbBasketType.Items.IndexOf(this.cmbBasketType.Items.FindByValue(data.TYPE));

        if (this.txtPDLOID.Text != "")
            SetProductPackage(this.txtPDLOID.Text);
        this.txtBasketQty.Text = data.QTY.ToString();

        this.txtBasketCode.Text = data.CODE;
        this.ctlCheckDate.DateValue = data.CHECKDATE;
        this.txtStatusName.Text = (data.STATUS == Constz.Basket.Status.Approved.Code ? Constz.Basket.Status.Approved.Name : Constz.Basket.Status.Waiting.Name);
        if (data.STATUS == Constz.Basket.Status.Approved.Code)
        {
            this.cmbBasketType.Enabled = false;
            this.txtBasketQty.Enabled = false;
        }
        this.txtLotNo.Text = data.LOTNO;

        this.txtRemark.Text = data.REMARK;

        calPrice();
        SetGrvItem(data.STATUS);

        if (data.STATUS == Constz.Basket.Status.Approved.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
    }

    private void SetProductPackage(string loid)
    {
        DataTable dt = FlowObj.GetProductPackage(this.txtPDLOID.Text);
        if (dt.Rows.Count > 0)
        {
            this.txtBarCode.Text = dt.Rows[0]["BARCODE"].ToString();
            this.txtBasketName.Text = dt.Rows[0]["NAME"].ToString();
            this.txtBasketunit.Text = dt.Rows[0]["UNITNAME"].ToString();
            this.txtUnit.Text = dt.Rows[0]["UNIT"].ToString();
            this.txtPrice.Text = dt.Rows[0]["PRICE"].ToString();
        }
    }

    private StockoutBasketData GetData()
    {
        StockoutBasketData data = new StockoutBasketData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtBasketCode.Text.Trim();
        data.CHECKDATE = this.ctlCheckDate.DateValue;
        data.PRODUCT = Convert.ToDouble(this.txtPDLOID.Text == "" ? "0" : this.txtPDLOID.Text);
        data.QTY = Convert.ToDouble(this.txtBasketQty.Text == "" ? "0" : this.txtBasketQty.Text);
        data.STATUS = this.txtStatus.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.TYPE = this.cmbBasketType.SelectedItem.Value;
        data.UNIT = Convert.ToDouble(this.txtUnit.Text == "" ? "0" : this.txtUnit.Text);
        data.LOTNO = this.txtLotNo.Text.Trim();
        data.WAREHOUSE = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);
        data.ITEM = ItemObj.GetItemList();

        return data;
    }

    private void SetComboLotNo(DropDownList cmbLotNo, string subPDLOID)
    {
        string whereStr = "LOTNO IS NOT NULL AND MAINPRODUCT='" + this.txtPDLOID.Text + "' AND SUBPRODUCT='" + subPDLOID + "' AND WAREHOUSE='" + this.txtWareHouse.Text + "'";
        ComboSource.BuildCombo(cmbLotNo, "V_STOCKOUT_BASKET_PRODUCT", "LOTNO", "PRODUCTSTOCK", "LOTNO", whereStr, "เลือก", "0");
    }

    private void calPrice()
    {
        try
        {
            double Qty = Convert.ToDouble(txtBasketQty.Text);
            if (txtPrice.Text != "")
            {
                double ans = Qty * Convert.ToDouble(txtPrice.Text);
                txtShowPrice.Text = ans.ToString();
            }
        }
        catch (Exception)
        {
            Appz.ClientAlert(this, "ค่าจำนวนผิดพลาด");
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string script = "";
            script += "document.getElementById('" + this.txtPDLOID.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupStockoutBasket.aspx', '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + this.txtPDLOID.ClientID + "').value || '' == document.getElementById('" + this.txtPDLOID.ClientID + "').value) ";
            script += "{ return false; } ";

            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));

            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockoutBasket, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";

            if (this.txtStatus.Text != Constz.Basket.Status.Approved.Code)
            {
                this.btnSearch.OnClientClick = script;
            }
        }

    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if ((this.txtPDLOID.Text != "") && (this.txtStatus.Text != Constz.Basket.Status.Approved.Code))
        {
            SetProductPackage(this.txtPDLOID.Text);
            ItemObj.DeleteBasketItemAll();
            DataTable dt = new DataTable();
            dt = FlowObj.GetProductBasket(Convert.ToDouble(this.txtPDLOID.Text));
            ItemObj.CopyItem(dt);
            SetGrvItem(this.txtStatus.Text);
            string tableName = "(SELECT PD.NAME NAME, PK.SUBPRODUCT LOID , PK.MAINPRODUCT FROM PACKAGE PK INNER JOIN PRODUCT PD ON PK.SUBPRODUCT = PD.LOID WHERE MAINPRODUCT = '" + this.txtPDLOID.Text + "')";
            ComboSource.BuildCombo((DropDownList)this.grvItemNew.Rows[0].Cells[3].FindControl("cmbNewProduct"), tableName, "NAME", "LOID", "NAME", "", "เลือก", "0");
            calPrice();
        }
    }

    protected void txtBasketQty_TextChanged(object sender, EventArgs e)
    {
        calPrice();
        if (ItemObj.BasketItemQty(Convert.ToDouble(txtBasketQty.Text),Convert.ToDouble(this.txtPDLOID.Text)));
        SetGrvItem(this.txtStatus.Text);
    }

    #region grvItemNew

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int16 rowIndex = 0;
        TextBox txtBarCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewBarCode");
        DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("cmbNewProduct");
        DropDownList cmbLotNo = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("cmbNewLotNo");
        TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewQty");
        TextBox txtUnit = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewUnit");
        
        if (e.CommandName == "Insert")
        {
            BasketItemData data = new BasketItemData();
            data.BARCODE = txtBarCode.Text;
            data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
            data.LOTNO = cmbLotNo.SelectedItem.Text;
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.UNITNAME = txtUnit.Text;
            data.UNIT = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[9].Text);
            data.PACKAGE = Convert.ToDouble(this.grvItemNew.Rows[rowIndex].Cells[11].Text);
            data.PRODUCTSTOCK = Convert.ToDouble(cmbLotNo.SelectedItem.Value);
            
            if (ItemObj.InsertBasketItem(data))
            {
                SetGrvItem(this.txtStatus.Text);
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (this.txtPDLOID.Text != "0")
            {
                string tableName = "(SELECT PD.NAME NAME, PK.SUBPRODUCT LOID , PK.MAINPRODUCT FROM PACKAGE PK INNER JOIN PRODUCT PD ON PK.SUBPRODUCT = PD.LOID WHERE MAINPRODUCT = '" + this.txtPDLOID.Text + "')";
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct");
                ComboSource.BuildCombo(cmbProduct, tableName, "NAME", "LOID", "NAME", "", "เลือก", "0");
            }
        }
    }

    protected void cmbNewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        DropDownList cmbLotNo = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("cmbNewLotNo");
        SetComboLotNo(cmbLotNo, cmb.SelectedItem.Value.ToString());

        TextBox txtBarCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewBarCode");
        TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewQty");
        TextBox txtUnitName = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewUnit");
 
        DataTable dt = FlowObj.GetSubProductData(this.txtPDLOID.Text, cmb.SelectedItem.Value.ToString());
        if (dt.Rows.Count > 0)
        {
            double ans = Convert.ToDouble(dt.Rows[0]["QTY"]) * Convert.ToDouble(this.txtBasketQty.Text);
            txtBarCode.Text = dt.Rows[0]["BARCODE"].ToString();
            txtUnitName.Text = dt.Rows[0]["UNITNAME"].ToString();
            txtQty.Text = ans.ToString();
            this.grvItemNew.Rows[rowIndex].Cells[9].Text = dt.Rows[0]["UNIT"].ToString();
            this.grvItemNew.Rows[rowIndex].Cells[11].Text = dt.Rows[0]["PACKAGE"].ToString();
            //this.grvItemNew.Rows[rowIndex].Cells[12].Text = dt.Rows[0]["PRODUCTSTOCK"].ToString();
        }
    }

    #endregion

    #region grvItem

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        TextBox txtBarCode = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewBarCode");
        DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[3].FindControl("cmbNewProduct");
        DropDownList cmbLotNo = (DropDownList)this.grvItem.FooterRow.Cells[4].FindControl("cmbNewLotNo");
        TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtNewQty");
        TextBox txtUnit = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewUnit");

        if (e.CommandName == "Insert")
        {
            BasketItemData data = new BasketItemData();
            data.BARCODE = txtBarCode.Text;
            data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
            data.LOTNO = cmbLotNo.SelectedItem.Text;
            data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            data.UNITNAME = txtUnit.Text;
            data.UNIT = Convert.ToDouble(this.grvItem.FooterRow.Cells[9].Text);
            data.PACKAGE = Convert.ToDouble(this.grvItem.FooterRow.Cells[11].Text);
            data.PRODUCTSTOCK = Convert.ToDouble(cmbLotNo.SelectedItem.Value);

            if (ItemObj.InsertBasketItem(data))
            {
                SetGrvItem(this.txtStatus.Text);
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string tableName = "(SELECT PD.NAME NAME, PK.SUBPRODUCT LOID , PK.MAINPRODUCT FROM PACKAGE PK INNER JOIN PRODUCT PD ON PK.SUBPRODUCT = PD.LOID WHERE MAINPRODUCT = '" + this.txtPDLOID.Text + "')";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProduct");
                DropDownList cmbLotNo = (DropDownList)e.Row.Cells[4].FindControl("cmbLotNo");

                ComboSource.BuildCombo(cmbProduct, tableName, "NAME", "LOID", "NAME", "", "เลือก", "0");

                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));

                SetComboLotNo(cmbLotNo, drow["PRODUCT"].ToString());

                cmbLotNo.SelectedIndex = cmbLotNo.Items.IndexOf(cmbLotNo.Items.FindByText(drow["LOTNO"].ToString()));

                ImageButton imbCancel = (ImageButton)e.Row.FindControl("imbCancel");

                imbCancel.OnClientClick = "return confirm('ยืนยันการยกเลิกรายการ');";
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                DropDownList cmbLotNo = (DropDownList)e.Row.Cells[4].FindControl("cmbLotNoView");

                ComboSource.BuildCombo(cmbProduct, tableName, "NAME", "LOID", "NAME", "", "เลือก", "0");

                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));

                SetComboLotNo(cmbLotNo, drow["PRODUCT"].ToString());

                cmbLotNo.SelectedIndex = cmbLotNo.Items.IndexOf(cmbLotNo.Items.FindByText(drow["LOTNO"].ToString()));

                ImageButton imbDelete = (ImageButton)e.Row.FindControl("imbDelete");

                imbDelete.OnClientClick = "return confirm('ยืนยันการลบรายการ');";
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ComboSource.BuildCombo((DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct"), tableName, "NAME", "LOID", "NAME", "", "เลือก", "0");
        }
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        DropDownList cmbLotNo = (DropDownList)this.grvItem.Rows[rowIndex].Cells[4].FindControl("cmbLotNo");
        SetComboLotNo(cmbLotNo, cmb.SelectedItem.Value.ToString());

        TextBox txtBarCode = (TextBox)this.grvItem.Rows[rowIndex].Cells[2].FindControl("txtBarCode");
        TextBox txtQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[5].FindControl("txtQty");
        TextBox txtUnitName = (TextBox)this.grvItem.Rows[rowIndex].Cells[6].FindControl("txtUnit");

        DataTable dt = FlowObj.GetSubProductData(this.txtPDLOID.Text, cmb.SelectedItem.Value.ToString());
        if (dt.Rows.Count > 0)
        {
            double ans = Convert.ToDouble(dt.Rows[0]["QTY"]) * Convert.ToDouble(this.txtBasketQty.Text);
            txtBarCode.Text = dt.Rows[0]["BARCODE"].ToString();
            txtUnitName.Text = dt.Rows[0]["UNITNAME"].ToString();
            txtQty.Text = ans.ToString();
            this.grvItem.Rows[rowIndex].Cells[9].Text = dt.Rows[0]["UNIT"].ToString();
            this.grvItem.Rows[rowIndex].Cells[11].Text = dt.Rows[0]["PACKAGE"].ToString();
            //this.grvItem.Rows[rowIndex].Cells[12].Text = dt.Rows[0]["PRODUCTSTOCK"].ToString();
        }
    }

    protected void cmbNewProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        DropDownList cmbLotNo = (DropDownList)this.grvItem.FooterRow.Cells[4].FindControl("cmbNewLotNo");
        SetComboLotNo(cmbLotNo, cmb.SelectedItem.Value.ToString());

        TextBox txtBarCode = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewBarCode");
        TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtNewQty");
        TextBox txtUnitName = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewUnit");

        DataTable dt = FlowObj.GetSubProductData(this.txtPDLOID.Text, cmb.SelectedItem.Value.ToString());
        if (dt.Rows.Count > 0)
        {
            double ans = Convert.ToDouble(dt.Rows[0]["QTY"]) * Convert.ToDouble(this.txtBasketQty.Text);
            txtBarCode.Text = dt.Rows[0]["BARCODE"].ToString();
            txtUnitName.Text = dt.Rows[0]["UNITNAME"].ToString();
            txtQty.Text = ans.ToString();
            this.grvItem.FooterRow.Cells[9].Text = dt.Rows[0]["UNIT"].ToString();
            this.grvItem.FooterRow.Cells[11].Text = dt.Rows[0]["PACKAGE"].ToString();
            //this.grvItem.FooterRow.Cells[12].Text = dt.Rows[0]["PRODUCTSTOCK"].ToString();
        }
    }

    protected void grvItem_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.ExceptionHandled)
        {
            e.KeepInEditMode = true;
            Appz.ClientAlert(this, e.Exception.Message);
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
        TextBox txtBarCode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("txtBarCode");
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("cmbProduct");
        DropDownList cmbLotNo = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("cmbLotNo");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("txtQty");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtUnit");

        BasketItemData data = new BasketItemData();

        data.BARCODE = txtBarCode.Text;
        data.PDNAME = cmbProduct.SelectedItem.Text;
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.LOTNO = cmbLotNo.SelectedItem.Text;
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.UNITNAME = txtUnit.Text;
        data.PRODUCTSTOCK = Convert.ToDouble(cmbLotNo.SelectedItem.Value);

        e.NewValues["BARCODE"] = data.BARCODE;
        e.NewValues["PDNAME"] = data.PDNAME;
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["LOTNO"] = data.LOTNO;
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["UNITNAME"] = data.UNITNAME;
        e.NewValues["PRODUCTSTOCK"] = data.PRODUCTSTOCK;
    }

    #endregion

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockoutBasketSearch.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        StockoutBasketData data = new StockoutBasketData();
        data = GetData();
        data.STATUS = Constz.Basket.Status.Approved.Code;
        data.STOCKINDATE = DateTime.Now.Date;
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ยืนยันข้อมูลเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
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

    protected void PrintClick(object sender, EventArgs e)
    {

    }
}
