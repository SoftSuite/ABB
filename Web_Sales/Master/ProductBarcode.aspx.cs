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
using ABB.Data.Sales;
using ABB.Flow.Sales;
using ABB.Global;

public partial class Master_ProductBarcode : System.Web.UI.Page
{
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            return new SessionPageStatePersister(this);
        }
    }
    private ProductMasterFlow _flow;
    private ProductBarcodeItem item;

    public ProductMasterFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductMasterFlow(); return _flow; }
    }

    public ProductBarcodeItem ItemObj
    {
        get { if (item == null) item = new ProductBarcodeItem(); return item; }
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        SetData(FlowObj.GetData(loid));
    }

    private void SetData(ProductSearchData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtMasterUnit.Text = data.UNIT.ToString();
        this.txtPackSizeUint.Text = data.PACKSIZEUNIT.ToString();
        
        this.txtName.Text = data.NAME;
        this.grvItem.DataBind();
    }

    private ProductSearchData GetData()
    {
        ProductSearchData data = FlowObj.GetData(Convert.ToDouble(this.txtLOID.Text));
        data.ITEM = ItemObj.GetItemList();
        return data;
    }


    private void InsertData(GridViewRow gRow)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[1].FindControl("txtNewBarcode");
        TextBox txtAbbname = (TextBox)gRow.Cells[2].FindControl("txtNewAbbname");
        TextBox txtMultiply = (TextBox)gRow.Cells[3].FindControl("txtNewMultiply");
        DropDownList cmbUnit = (DropDownList)gRow.Cells[5].FindControl("cmbNewUnit");
        TextBox txtCost = (TextBox)gRow.Cells[6].FindControl("txtNewCost");
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl("txtNewPrice");
        CheckBox chkIsVAT = (CheckBox)gRow.Cells[8].FindControl("chkNewIsVAT");
        CheckBox chkIsDiscount = (CheckBox)gRow.Cells[9].FindControl("chkNewIsDiscount");
        TextBox txtPackSize = (TextBox)gRow.Cells[10].FindControl("txtNewPackSize");
        DropDownList cmbUnitPack = (DropDownList)gRow.Cells[11].FindControl("cmbNewUnitPack");

        ProductBarcodeData data = new ProductBarcodeData();
        data.UNITMASTER = FlowObj.GetUnitName(this.txtMasterUnit.Text);
        //data.UNITPACK = FlowObj.GetUnitName(this.txtPackSizeUint.Text);
        data.BARCODE = txtBarcode.Text.Trim();
        data.ABBNAME = txtAbbname.Text.Trim();
        data.MULTIPLY = Convert.ToDouble(txtMultiply.Text == "" ? "0" : txtMultiply.Text);
        data.UNIT = Convert.ToDouble(cmbUnit.SelectedValue);
        data.UNITPACK = Convert.ToDouble(cmbUnitPack.SelectedValue);
        data.COST = Convert.ToDouble(txtCost.Text == "" ? "0" : txtCost.Text);
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.ISVAT = (chkIsVAT.Checked ? "1" : "0");
        data.ISDISCOUNT = (chkIsDiscount.Checked ? "1" : "0");
        data.PACKSIZE = Convert.ToDouble(txtPackSize.Text == "" ? "0" : txtPackSize.Text);

        if (ItemObj.InsertItem(data))
        {
            this.grvItem.DataBind();
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void NewRowDataBound(GridViewRow gRow)
    {
        ComboSource.BuildCombo((DropDownList)gRow.Cells[5].FindControl("cmbNewUnit"), "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'FG' OR TYPE = 'AL'", "เลือก", "0");
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[3].FindControl("txtNewMultiply"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[6].FindControl("txtNewCost"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[7].FindControl("txtNewPrice"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[10].FindControl("txtNewPackSize"));
        CheckBox chkIsVAT = (CheckBox)gRow.Cells[8].FindControl("chkNewIsVAT");
        CheckBox chkIsDiscount = (CheckBox)gRow.Cells[9].FindControl("chkNewIsDiscount");
        ComboSource.BuildCombo((DropDownList)gRow.Cells[11].FindControl("cmbNewUnitPack"), "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'FG' OR TYPE = 'AL'", "เลือก", "0");
        chkIsVAT.Checked = true;
        chkIsDiscount.Checked = true;
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
                DropDownList cmbUnit = (DropDownList)e.Row.Cells[5].FindControl("cmbUnit");
                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'FG' OR TYPE = 'AL'", "เลือก", "0");
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));

                DropDownList cmbUnitPack = (DropDownList)e.Row.Cells[10].FindControl("cmbUnitPack");
                ComboSource.BuildCombo(cmbUnitPack, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'FG' OR TYPE = 'AL'", "เลือก", "0");
                cmbUnitPack.SelectedIndex = cmbUnitPack.Items.IndexOf(cmbUnitPack.Items.FindByValue(drow["UNITPACK"].ToString()));

                CheckBox chkIsVAT = (CheckBox)e.Row.Cells[8].FindControl("chkIsVAT");
                CheckBox chkIsDiscount = (CheckBox)e.Row.Cells[9].FindControl("chkIsDiscount");

                chkIsVAT.Checked = (drow["ISVAT"].ToString() == "1" ? true : false);
                chkIsDiscount.Checked = (drow["ISDISCOUNT"].ToString() == "1" ? true : false);
                CheckBox chkIsActive = (CheckBox)e.Row.Cells[11].FindControl("chkIsActive");

                if (drow["ACTIVE"].ToString() == "1")
                {
                    chkIsActive.Checked = true;
                }
                else
                {
                    chkIsActive.Checked = false;
                }
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[3].FindControl("txtMultiply"));
                ControlUtil.SetDblTextBox((TextBox)e.Row.Cells[6].FindControl("txtCost"));
                ControlUtil.SetDblTextBox((TextBox)e.Row.Cells[7].FindControl("txtPrice"));
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[10].FindControl("txtPackSize"));
                if (e.Row.RowIndex == 0)
                {
                    CheckBox chkIsDefault = (CheckBox)e.Row.Cells[13].FindControl("chkIsDefault");
                    chkIsDefault.Checked = true;
                }
                
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                //ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                //imbDelete.Attributes.Add("onclick", "return confirm('ยืนยันการลบสินค้า?');");

                DropDownList cmbUnit = (DropDownList)e.Row.Cells[5].FindControl("cmbUnitView");
                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'FG' OR TYPE = 'AL'", "เลือก", "0");
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));

                DropDownList cmbUnitPack = (DropDownList)e.Row.Cells[10].FindControl("cmbUnitPackView");
                ComboSource.BuildCombo(cmbUnitPack, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'FG' OR TYPE = 'AL'", "เลือก", "0");
                cmbUnitPack.SelectedIndex = cmbUnitPack.Items.IndexOf(cmbUnitPack.Items.FindByValue(drow["UNITPACK"].ToString()));

                CheckBox chkIsVAT = (CheckBox)e.Row.Cells[8].FindControl("chkIsVATView");
                CheckBox chkIsDiscount = (CheckBox)e.Row.Cells[9].FindControl("chkIsDiscountView");

                chkIsVAT.Checked = (drow["ISVAT"].ToString() == "1" ? true : false);
                chkIsDiscount.Checked = (drow["ISDISCOUNT"].ToString() == "1" ? true : false);


                CheckBox chkIsActive = (CheckBox)e.Row.Cells[11].FindControl("chkIsActiveView");

                if (drow["ACTIVE"].ToString() == "1")
                {
                    chkIsActive.Checked = true;
                }
                else
                {
                    chkIsActive.Checked = false;
                }

                if (e.Row.RowIndex == 0)
                {
                    CheckBox chkIsDefault = (CheckBox)e.Row.Cells[13].FindControl("chkIsDefaultView");
                    chkIsDefault.Checked = true;
                    ImageButton imbEdit = (ImageButton)e.Row.Cells[0].FindControl("imbEdit");

                    //imbDelete.Visible = false;
                    imbEdit.Visible = false;
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            NewRowDataBound(e.Row);
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
            this.grvItem.DataBind();
        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtBarcode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[1].FindControl("txtBarcode");
        TextBox txtAbbname = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("txtAbbname");
        TextBox txtMultiply = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("txtMultiply");
        DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("cmbUnit");
        TextBox txtCost = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtCost");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtPrice");
        CheckBox chkIsVAT = (CheckBox)this.grvItem.Rows[e.RowIndex].Cells[8].FindControl("chkIsVAT");
        CheckBox chkIsDiscount = (CheckBox)this.grvItem.Rows[e.RowIndex].Cells[9].FindControl("chkIsDiscount");
        TextBox txtPackSize = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[9].FindControl("txtPackSize");
        DropDownList cmbUnitPack = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[10].FindControl("cmbUnitPack");

        ProductBarcodeData data = new ProductBarcodeData();
        data.UNITMASTER = FlowObj.GetUnitName(this.txtMasterUnit.Text);
        //data.UNITPACK = FlowObj.GetUnitName(this.txtPackSizeUint.Text);
        data.BARCODE = txtBarcode.Text.Trim();
        data.ABBNAME = txtAbbname.Text.Trim();
        data.MULTIPLY = Convert.ToDouble(txtMultiply.Text == "" ? "0" : txtMultiply.Text);
        data.UNIT = Convert.ToDouble(cmbUnit.SelectedValue);
        data.UNITPACK = Convert.ToDouble(cmbUnitPack.SelectedValue);
        data.COST = Convert.ToDouble(txtCost.Text == "" ? "0" : txtCost.Text);
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.ISVAT = (chkIsVAT.Checked ? "1" : "0");
        data.ISDISCOUNT = (chkIsDiscount.Checked ? "1" : "0");
        data.PACKSIZE = Convert.ToDouble(txtPackSize.Text == "" ? "0" : txtPackSize.Text);

        e.NewValues["UNITMASTER"] = data.UNITMASTER;
        e.NewValues["UNITPACK"] = data.UNITPACK;
        e.NewValues["BARCODE"] = data.BARCODE;
        e.NewValues["ABBNAME"] = data.ABBNAME;
        e.NewValues["MULTIPLY"] = data.MULTIPLY.ToString();
        e.NewValues["UNIT"] = data.UNIT.ToString();
        e.NewValues["COST"] = data.COST.ToString();
        e.NewValues["PRICE"] = data.PRICE.ToString();
        e.NewValues["ISVAT"] = data.ISVAT;
        e.NewValues["ISDISCOUNT"] = data.ISDISCOUNT;
        e.NewValues["PACKSIZE"] = data.PACKSIZE;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/ProductSearch.aspx");
    }

    //protected void CancelClick(object sender, EventArgs e)
    //{
    //    ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
    //}

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateItemData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
            ResetState(FlowObj.LOID);
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void ReturnClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateItemData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            Response.Redirect(Constz.HomeFolder + "Master/Product.aspx?loid=" + this.txtLOID.Text);
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }
}
