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
using ABB.Flow.Common;
using ABB.Global;

public partial class Master_PromotionSales : System.Web.UI.Page
{
    private PromotionSaleFlow _flow;
    private PromotionItem item;

    private PromotionSaleFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PromotionSaleFlow(); } return _flow; }
    }


    public PromotionItem ItemObj
    {
        get { if (item == null) item = new PromotionItem(); return item; }
    }

    private void SetGrvItem()
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        if (grvItem.Rows.Count > 0)
        {
            this.grvItem.Visible = true;
            this.grvItemNew.Visible = false;
        }
        else
        {
            this.grvItem.Visible = false;
            this.grvItemNew.Visible = true;
        }
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        PromotionSaleData data = FlowObj.GetData(loid);
        SetData(data);
    }

    private PromotionSaleData GetData()
    {
        PromotionSaleData data = new PromotionSaleData();
        data.CODE = this.txtCode.Text.Trim();
        data.LOID = this.txtLOID.Text == "" ? 0 : Convert.ToDouble(this.txtLOID.Text);
        data.NAME = this.txtName.Text.Trim();
        data.CREATEON = this.ctlCreateOn.DateValue;
        data.WAREHOUSE = Convert.ToDouble(this.cmbWarehouse.SelectedItem.Value);
        data.ZONE = Convert.ToDouble(this.cmbZone.SelectedItem.Value);
        data.EFDATEFROM = this.ctlEFDate.DateValue;
        data.EPDATEFROM = this.ctlEPDate.DateValue;
        data.DISCOUNT = Convert.ToDouble(this.txtDISCOUNT.Text.Trim() == "" ? "0" : this.txtDISCOUNT.Text);
        data.LOWERPRICE = Convert.ToDouble(this.txtLowerPrice.Text.Trim() == "" ? "0" : this.txtLowerPrice.Text);
        data.ITEM = ItemObj.GetItemList();
        return data;
    }

    private void SetData(PromotionSaleData data)
    {
        if (data.LOID == 0)
            data.CREATEON = DateTime.Today;
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE.Trim();
        this.txtName.Text = data.NAME.Trim();
        this.cmbWarehouse.SelectedIndex = this.cmbWarehouse.Items.IndexOf(this.cmbWarehouse.Items.FindByValue(data.WAREHOUSE.ToString()));
        SetZoneCombo();
        //this.cmbZone.SelectedIndex = this.cmbZone.Items.IndexOf(this.cmbZone.Items.FindByValue(data.ZONE.ToString()));
        this.ctlCreateOn.DateValue = data.CREATEON;
        this.ctlEFDate.DateValue = data.EFDATEFROM.Date;
        this.ctlEPDate.DateValue = data.EPDATEFROM.Date;
        this.txtDISCOUNT.Text = data.DISCOUNT.ToString(Constz.IntFormat);
        this.txtLowerPrice.Text = data.LOWERPRICE.ToString(Constz.IntFormat);
        //add
        SetGrvItem();
    }

    private void SetZoneCombo()
    {
        ComboSource.BuildCombo(this.cmbZone, "ZONE", "NAME", "LOID", "NAME", "ACTIVE='" + Constz.ActiveStatus.Active + "' AND WAREHOUSE = " + this.cmbWarehouse.SelectedItem.Value + " ");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnNewAll.Text = "<img src='" + Constz.ImageFolder + "icn_new_all.gif' border='0' align='AbsMiddle'> เพิ่มสินค้าทั้งหมด";
            btnNewAll.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnNewAll.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
            this.btnNewAll.OnClientClick = "return confirm('ต้องการเพิ่มรายการสินค้าทั้งหมดใช่หรือไม่?');";

            ComboSource.BuildCombo(this.cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "TYPE = '" + Constz.Warehouse.Type.FG.Code + "' AND ACTIVE='" + Constz.ActiveStatus.Active + "' ");
            SetZoneCombo();
            ControlUtil.SetIntTextBox(this.txtDISCOUNT);
            ControlUtil.SetIntTextBox(this.txtLowerPrice);

            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.Promotion, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        }
    }

    private double CalNewPrice(double price)
    {
        double discountPercent = Convert.ToDouble(this.txtDISCOUNT.Text == "" ? "0" : this.txtDISCOUNT.Text);
        return Convert.ToDouble((price - (price * discountPercent / 100)).ToString(Constz.DblFormat));
    }

    private void SetProductDetail(ProductSearchData data, GridViewRow gRow, TextBox txtBarcode, DropDownList cmbProduct, TextBox txtUnit, TextBox txtOldPrice, TextBox txtNewPrice)
    {
        txtBarcode.Text = data.BARCODE;
        txtUnit.Text = FlowObj.GetUnitName(data.UNIT);
        txtOldPrice.Text = data.PRICE.ToString(Constz.DblFormat);
        txtNewPrice.Text = CalNewPrice(data.PRICE).ToString(Constz.DblFormat);
        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.LOID.ToString()));

        if (txtBarcode.ID == "txtNewBarCode" && txtBarcode.Text != "")
            InsertData(gRow);
    }

    private void txtBarcode_TextChanged(TextBox txt, GridViewRow gRow, string ctlProductName, string ctlUnitName, string ctlOldPriceName, string ctlNewPriceName)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl(ctlProductName);
        TextBox txtUnit = (TextBox)gRow.Cells[4].FindControl(ctlUnitName);
        TextBox txtOldPrice = (TextBox)gRow.Cells[5].FindControl(ctlOldPriceName);
        TextBox txtNewPrice = (TextBox)gRow.Cells[6].FindControl(ctlNewPriceName);

        ProductSearchData data = FlowObj.GetProductData(txt.Text.Trim());
        SetProductDetail(data, gRow, txt, cmbProduct, txtUnit, txtOldPrice, txtNewPrice);
    }

    private void cmbProduct_SelectedIndexChanged(DropDownList cmb, GridViewRow gRow, string ctlBarcodeName, string ctlUnitName, string ctlOldPriceName, string ctlNewPriceName)
    {
        TextBox txtCode = (TextBox)gRow.Cells[2].FindControl(ctlBarcodeName);
        TextBox txtUnit = (TextBox)gRow.Cells[4].FindControl(ctlUnitName);
        TextBox txtOldPrice = (TextBox)gRow.Cells[5].FindControl(ctlOldPriceName);
        TextBox txtNewPrice = (TextBox)gRow.Cells[6].FindControl(ctlNewPriceName);

        ProductSearchData data = FlowObj.GetProductData(Convert.ToDouble(cmb.SelectedItem.Value));
        SetProductDetail(data, gRow, txtCode, cmb, txtUnit, txtOldPrice, txtNewPrice);
    }

    private void InsertData(GridViewRow gRow)
    {
        TextBox txtCode = (TextBox)gRow.Cells[2].FindControl("txtNewBarCode");
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl("cmbNewProduct");
        TextBox txtUnit = (TextBox)gRow.Cells[4].FindControl("txtNewUnit");
        TextBox txtOldPrice = (TextBox)gRow.Cells[5].FindControl("txtNewOldPrice");
        TextBox txtNewPrice = (TextBox)gRow.Cells[6].FindControl("txtNewNewPrice");

        PromotionSalesItemData data = new PromotionSalesItemData();
        data.NAME = Convert.ToString(cmbProduct.SelectedItem.Text);
        data.UNAME = Convert.ToString(txtUnit.Text);
        data.PRICEOLD = Convert.ToDouble(txtOldPrice.Text == "" ? "0" : txtOldPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.PRICENEW = Convert.ToDouble(txtNewPrice.Text == "" ? "0" : txtNewPrice.Text);
        data.BARCODE = txtCode.Text;

        if (ItemObj.InsertPromotionSalesItem(data))
        {
            SetGrvItem();
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        double discountPercent = Convert.ToDouble(this.txtDISCOUNT.Text == "" ? "0" : this.txtDISCOUNT.Text);
        foreach (DataRow dRow in ItemObj.GetPromotionItem(Convert.ToDouble(this.txtLOID.Text== "" ? "0" : this.txtLOID.Text)).Rows)
        {
            dRow["PRICENEW"] = CalNewPrice(Convert.ToDouble(dRow["PRICEOLD"])).ToString(Constz.DblFormat);
        }
        this.grvItem.DataBind();
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
            ComboSource.BuildCombo((DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct"), "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "", "เลือก", "0");
        }
    }

    protected void txtNewBarCode_TextChanged(object sender, EventArgs e)
    {
        txtBarcode_TextChanged((TextBox)sender, this.grvItemNew.Rows[0], "cmbNewProduct", "txtNewUnit", "txtNewOldPrice", "txtNewNewPrice");
    }

    protected void cmbNewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbProduct_SelectedIndexChanged((DropDownList)sender, this.grvItemNew.Rows[0], "txtNewBarcode", "txtNewUnit", "txtNewOldPrice", "txtNewNewPrice");
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
                ComboSource.BuildCombo(cmbProduct, "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "");
                ControlUtil.SetDblTextBox((TextBox)e.Row.Cells[6].FindControl("txtNewPrice"));
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                if (cmbProduct != null)
                {
                    ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");
                }
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                imbDelete.OnClientClick = "return confirm('ต้องการลบรายการสินค้า " + cmbProduct.SelectedItem.Text + " ใช่หรือไม่ ?')";
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ComboSource.BuildCombo((DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct"), "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "", "เลือก", "0");
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
            SetGrvItem();
        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtBarcode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("txtBarcode");
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("cmbProduct");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtUnit");
        TextBox txtOldPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("txtOldPrice");
        TextBox txtNewPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtNewPrice");
        PromotionSalesItemData data = new PromotionSalesItemData();
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.NAME = Convert.ToString(cmbProduct.SelectedItem.Text);
        data.UNAME = Convert.ToString(txtUnit.Text);
        data.BARCODE = txtBarcode.Text.Trim();
        data.PRICEOLD = Convert.ToDouble(txtOldPrice.Text == "" ? "0" : txtOldPrice.Text);
        data.PRICENEW = Convert.ToDouble(txtNewPrice.Text == "" ? "0" : txtNewPrice.Text);

        e.NewValues["PRODUCT"] = data.PRODUCT;
        e.NewValues["NAME"] = data.NAME.ToString();
        e.NewValues["PRICEOLD"] = data.PRICEOLD;
        e.NewValues["UNAME"] = data.UNAME.ToString();
        e.NewValues["PRICENEW"] = data.PRICENEW;
        e.NewValues["BARCODE"] = data.BARCODE.ToString(); 
    }

    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        txtBarcode_TextChanged(txt, this.grvItem.Rows[rowIndex], "cmbProduct", "txtUnit", "txtOldPrice", "txtNewPrice");
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        cmbProduct_SelectedIndexChanged(cmb, this.grvItem.Rows[rowIndex], "txtBarCode", "txtUnit", "txtOldPrice", "txtNewPrice");
    }

    protected void txtNewBarCode_TextChanged1(object sender, EventArgs e)
    {
        txtBarcode_TextChanged((TextBox)sender, this.grvItem.FooterRow, "cmbNewProduct", "txtNewUnit", "txtNewOldPrice", "txtNewNewPrice");
    }

    protected void cmbNewProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        cmbProduct_SelectedIndexChanged((DropDownList)sender, this.grvItem.FooterRow, "txtNewBarCode", "txtNewUnit", "txtNewOldPrice", "txtNewNewPrice");
    }

    #endregion

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/PromotionSalesSearch.aspx");
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

    protected void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetZoneCombo();
    }

    protected void btnNewAll_Click(object sender, EventArgs e)
    {
        ItemObj.SetAllProductList(FlowObj.GetAllProductList(Convert.ToDouble(this.txtDISCOUNT.Text == "" ? "0" : this.txtDISCOUNT.Text)));
        this.grvItem.DataBind();
    }
}
