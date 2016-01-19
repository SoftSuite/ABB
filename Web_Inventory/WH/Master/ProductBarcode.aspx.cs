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
using ABB.Flow.Inventory.WH;
using ABB.Global;

public partial class WH_Master_ProductBarcode : System.Web.UI.Page
{
    private ProductFlow _flow;
    private ProductBarcodeItem item;

    public ProductFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductFlow(); return _flow; }
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
        TextBox txtStdPrice = (TextBox)gRow.Cells[8].FindControl("txtNewStdPrice");
        TextBox txtPackSize = (TextBox)gRow.Cells[9].FindControl("txtNewPackSize");
        DropDownList cmbUnitPack = (DropDownList)gRow.Cells[10].FindControl("cmbNewUnitPack");
        CheckBox chkIsActive = (CheckBox)gRow.Cells[11].FindControl("chkNewIsActive");

        ProductBarcodeData data = new ProductBarcodeData();
        data.UNITMASTER = FlowObj.GetUnitName(this.txtMasterUnit.Text);
        data.UNITPACK = Convert.ToDouble(cmbUnitPack.SelectedValue);
        //data.UNITPACK = Convert.ToDouble(FlowObj.GetUnitPackName(cmbUnitPack.SelectedValue));
        data.BARCODE = txtBarcode.Text.Trim();
        data.ABBNAME = txtAbbname.Text.Trim();
        data.MULTIPLY = Convert.ToDouble(txtMultiply.Text == "" ? "0" : txtMultiply.Text);
        data.UNIT = Convert.ToDouble(cmbUnit.SelectedValue);
        data.COST = Convert.ToDouble(txtCost.Text == "" ? "0" : txtCost.Text);
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.STDPRICE = Convert.ToDouble(txtStdPrice.Text == "" ? "0" : txtStdPrice.Text);
        data.PACKSIZE = Convert.ToDouble(txtPackSize.Text == "" ? "0" : txtPackSize.Text);
        if (chkIsActive.Checked == true)
        {
            data.ACTIVE = 1;
        }

        if (ItemObj.InsertItem(data))
        {
            this.grvItem.DataBind();
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void NewRowDataBound(GridViewRow gRow)
    {
        ComboSource.BuildCombo((DropDownList)gRow.Cells[5].FindControl("cmbNewUnit"), "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'WH' OR TYPE = 'AL'", "เลือก", "0");
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[3].FindControl("txtNewMultiply"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[6].FindControl("txtNewCost"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[7].FindControl("txtNewPrice"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[8].FindControl("txtNewStdPrice"));
        ControlUtil.SetIntTextBox((TextBox)gRow.Cells[9].FindControl("txtNewPackSize"));
        ComboSource.BuildCombo((DropDownList)gRow.Cells[10].FindControl("cmbNewUnitPack"), "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'WH' OR TYPE = 'AL'", "เลือก", "0");
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
                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'WH' OR TYPE = 'AL'", "เลือก", "0");
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));

                DropDownList cmbUnitPack = (DropDownList)e.Row.Cells[10].FindControl("cmbUnitPack");
                ComboSource.BuildCombo(cmbUnitPack, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'WH' OR TYPE = 'AL'", "เลือก", "0");
                cmbUnitPack.SelectedIndex = cmbUnitPack.Items.IndexOf(cmbUnitPack.Items.FindByValue(drow["UNITPACK"].ToString()));

                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[3].FindControl("txtMultiply"));
                ControlUtil.SetDblTextBox((TextBox)e.Row.Cells[6].FindControl("txtCost"));
                ControlUtil.SetDblTextBox((TextBox)e.Row.Cells[7].FindControl("txtPrice"));
                ControlUtil.SetDblTextBox((TextBox)e.Row.Cells[8].FindControl("txtStdPrice"));
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[9].FindControl("txtPackSize"));
                
                if (e.Row.RowIndex == 0)
                {
                    CheckBox chkIsDefault = (CheckBox)e.Row.Cells[12].FindControl("chkIsDefault");
                    chkIsDefault.Checked = true;
                }
                CheckBox chkIsActive = (CheckBox)e.Row.Cells[11].FindControl("chkIsActive");

                if (drow["ACTIVE"].ToString() == "1")
                {
                    chkIsActive.Checked = true;
                }
                else
                {
                    chkIsActive.Checked = false;
                }
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                //ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                //imbDelete.Attributes.Add("onclick", "return confirm('ยืนยันการลบสินค้า?');");

                DropDownList cmbUnit = (DropDownList)e.Row.Cells[5].FindControl("cmbUnitView");
                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1 AND TYPE = 'WH' OR TYPE = 'AL'", "เลือก", "0");
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));

                DropDownList cmbUnitPack = (DropDownList)e.Row.Cells[10].FindControl("cmbUnitPackView");
                ComboSource.BuildCombo(cmbUnitPack, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1", "เลือก", "0");
                cmbUnitPack.SelectedIndex = cmbUnitPack.Items.IndexOf(cmbUnitPack.Items.FindByValue(drow["UNITPACK"].ToString()));
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
                    
                    CheckBox chkIsDefault = (CheckBox)e.Row.Cells[12].FindControl("chkIsDefaultView");
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
        TextBox txtStdPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[8].FindControl("txtStdPrice");
        TextBox txtPackSize = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[9].FindControl("txtPackSize");
        DropDownList cmbUnitPack = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[10].FindControl("cmbUnitPack");
        CheckBox chkIsActive = (CheckBox)this.grvItem.Rows[e.RowIndex].Cells[11].FindControl("chkIsActive");

        ProductBarcodeData data = new ProductBarcodeData();
        data.UNITMASTER = FlowObj.GetUnitName(this.txtMasterUnit.Text);
        data.UNITPACK = Convert.ToDouble(cmbUnitPack.SelectedValue);
        data.BARCODE = txtBarcode.Text.Trim();
        data.ABBNAME = txtAbbname.Text.Trim();
        data.MULTIPLY = Convert.ToDouble(txtMultiply.Text == "" ? "0" : txtMultiply.Text);
        data.UNIT = Convert.ToDouble(cmbUnit.SelectedValue);
        //data.UNITPACK = Convert.ToDouble(FlowObj.GetUnitPackName(cmbUnitPack.SelectedValue));
        data.COST = Convert.ToDouble(txtCost.Text == "" ? "0" : txtCost.Text);
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.STDPRICE = Convert.ToDouble(txtStdPrice.Text == "" ? "0" : txtStdPrice.Text);
        data.PACKSIZE = Convert.ToDouble(txtPackSize.Text == "" ? "0" : txtPackSize.Text);
        if (chkIsActive.Checked == true)
        {
            data.ACTIVE = 1;
        }
        else
        {
            data.ACTIVE = 0;
        }

        e.NewValues["UNITMASTER"] = data.UNITMASTER;
        e.NewValues["UNITPACK"] = data.UNITPACK;
        e.NewValues["BARCODE"] = data.BARCODE;
        e.NewValues["ABBNAME"] = data.ABBNAME;
        e.NewValues["MULTIPLY"] = data.MULTIPLY.ToString();
        e.NewValues["UNIT"] = data.UNIT.ToString();
        e.NewValues["COST"] = data.COST.ToString();
        e.NewValues["PRICE"] = data.PRICE.ToString();
        e.NewValues["STDPRICE"] = data.STDPRICE.ToString();
        e.NewValues["PACKSIZE"] = data.PACKSIZE;
        e.NewValues["ACTIVE"] = data.ACTIVE;

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
        Response.Redirect(Constz.HomeFolder + "WH/Master/ProductSerach.aspx");
    }

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
            Response.Redirect(Constz.HomeFolder + "WH/Master/Product.aspx?loid=" + this.txtLOID.Text);
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }
}
