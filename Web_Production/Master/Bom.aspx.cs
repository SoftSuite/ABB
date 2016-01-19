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
using ABB.Flow.Production;
using ABB.Data;
using ABB.Data.Production;
using ABB.DAL.Production;
using ABB.Global;

public partial class Master_Bom : System.Web.UI.Page
{
    private BomFlow _flow;
    private BomItem _item;
    private int indexBUTTON = 0;
    private int indexRANK = 1;
    private int indexBARCODE = 2;
    private int indexMATERIAL = 3;
    private int indexPRODUCTTYPE = 4;
    private int indexMASTER = 5;
    private int indexUNIT = 6;

    private BomFlow FlowObj
    {
        get { if (_flow == null) { _flow = new BomFlow(); } return _flow; }
    }
    private BomItem ItemObj
    {
        get { if (_item == null) { _item = new BomItem(); } return _item; }
    }

    #region GridView

    private void SetProductDetail(BomProductData data, GridViewRow gRow, TextBox txtBarcode, DropDownList cmbMaterial, Label lblProductType, TextBox txtUnit, Label lblUnitName)
    {
        txtBarcode.Text = data.BARCODE;
        cmbMaterial.SelectedIndex = cmbMaterial.Items.IndexOf(cmbMaterial.Items.FindByValue(data.LOID.ToString()));
        lblProductType.Text = data.PRODUCTTYPENAME;
        txtUnit.Text = data.UNIT.ToString();
        lblUnitName.Text = data.UNITNAME;

        //if (txtBarcode.ID == "txtNewBarCode" && txtBarcode.Text != "")
        //    InsertData(gRow);
    }

    private void txtBarcode_TextChanged(TextBox txtBarcode, GridViewRow gRow, string cmbMaterialName, string lblProductTypeName, string txtUnitName, string lblUnitName)
    {
        DropDownList cmbMaterial = (DropDownList)gRow.Cells[indexMATERIAL].FindControl(cmbMaterialName);
        Label lblProductType = (Label)gRow.Cells[indexPRODUCTTYPE].FindControl(lblProductTypeName);
        TextBox txtUnit = (TextBox)gRow.Cells[indexUNIT].FindControl(txtUnitName);
        Label lblUnit = (Label)gRow.Cells[indexUNIT].FindControl(lblUnitName);

        BomProductData data = FlowObj.GetProductData(0, txtBarcode.Text.Trim());
        SetProductDetail(data, gRow, txtBarcode, cmbMaterial, lblProductType, txtUnit, lblUnit);
    }

    private void cmbMaterial_SelectedIndexChanged(DropDownList cmbMaterial, GridViewRow gRow, string txtBarcodeName, string lblProductTypeName, string txtUnitName, string lblUnitName)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[indexBARCODE].FindControl(txtBarcodeName);
        Label lblProductType = (Label)gRow.Cells[indexPRODUCTTYPE].FindControl(lblProductTypeName);
        TextBox txtUnit = (TextBox)gRow.Cells[indexUNIT].FindControl(txtUnitName);
        Label lblUnit = (Label)gRow.Cells[indexUNIT].FindControl(lblUnitName);

        BomProductData data = FlowObj.GetProductData(Convert.ToDouble(cmbMaterial.SelectedItem.Value), "");
        SetProductDetail(data, gRow, txtBarcode, cmbMaterial, lblProductType, txtUnit, lblUnit);
    }

    private void InsertData(GridViewRow gRow)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[indexBARCODE].FindControl("txtBarcodeNew");
        DropDownList cmbMaterial = (DropDownList)gRow.Cells[indexMATERIAL].FindControl("cmbMaterialNew");
        Label lblProductType = (Label)gRow.Cells[indexPRODUCTTYPE].FindControl("txtProductTypeNew");
        TextBox txtUnit = (TextBox)gRow.Cells[indexUNIT].FindControl("txtUnitNew");
        Label lblUnit = (Label)gRow.Cells[indexUNIT].FindControl("txtUnitNameNew");
        TextBox txtMaster = (TextBox)gRow.Cells[indexMASTER].FindControl("txtMasterNew");

        if (ItemObj.InsertBomItem(txtBarcode.Text.Trim(), cmbMaterial.SelectedItem.Text, Convert.ToDouble(txtMaster.Text=="" ? "0" : txtMaster.Text), lblProductType.Text.Trim(), lblUnit.Text.Trim(), Convert.ToDouble(txtUnit.Text == "" ? "0 " : txtUnit.Text), Convert.ToDouble(cmbMaterial.SelectedItem.Value) ))
        {
            SetGrvItem();
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void NewRowDataBound(GridViewRow gRow)
    {
        ComboSource.BuildCombo((DropDownList)gRow.Cells[indexMATERIAL].FindControl("cmbMaterialNew"), "PRODUCT", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND PRODUCTGROUP IN (SELECT PG.LOID FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE WHERE PT.TYPE = '" + Constz.ProductType.Type.WH.Code + "') ", "เลือก", "0");
        ControlUtil.SetDblTextBox5((TextBox)gRow.Cells[indexMASTER].FindControl("txtMasterNew"));
    }

    #endregion

    private void SetGrvItem()
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        this.grvItem.Visible = (grvItem.Rows.Count > 0);
        this.grvItemNew.Visible = (grvItem.Rows.Count <= 0);
    }

    private void SetMainProduct(double productBarcode, string barcode)
    {
        BomProductData data = FlowObj.GetProductData(productBarcode, barcode);
        this.txtBarcode.Text = data.BARCODE;
        this.cmbProduct.SelectedIndex = this.cmbProduct.Items.IndexOf(this.cmbProduct.Items.FindByValue(data.LOID.ToString()));
        this.cmbProductType.SelectedIndex = this.cmbProductType.Items.IndexOf(this.cmbProductType.Items.FindByValue(data.PRODUCTTYPE.ToString()));
        this.cmbProductGroup.SelectedIndex = this.cmbProductGroup.Items.IndexOf(this.cmbProductGroup.Items.FindByValue(data.PRODUCTGROUP.ToString()));
    }

    private void SetData(double mainProduct)
    {
        ItemObj.ClearSession();
        BomSearchData data = FlowObj.GetBomData(mainProduct);
        this.txtProduct.Text = data.MAINPRODUCT.ToString();
        this.txtBarcode.Text = data.BARCODE;
        this.cmbProduct.SelectedIndex = this.cmbProduct.Items.IndexOf(this.cmbProduct.Items.FindByValue(data.MAINPRODUCT.ToString()));
        this.cmbProductType.SelectedIndex = this.cmbProductType.Items.IndexOf(this.cmbProductType.Items.FindByValue(data.PRODUCTTYPE.ToString()));
        this.cmbProductGroup.SelectedIndex = this.cmbProductGroup.Items.IndexOf(this.cmbProductGroup.Items.FindByValue(data.PRODUCTGROUP.ToString()));
        this.txtProcess.Text = data.PROCESS;
        this.Radiation.Checked = (data.RADIATION != Constz.Radiation.No);
        this.NonRadiation.Checked = (data.RADIATION == Constz.Radiation.No);
        this.chkActive.Checked = (data.ACTIVE != Constz.ActiveStatus.InActive);
        SetGrvItem();
    }

    private BomSearchData GetData()
    {
        BomSearchData data = new BomSearchData();
        data.ACTIVE = (this.chkActive.Checked ? Constz.ActiveStatus.Active : Constz.ActiveStatus.InActive);
        data.BARCODE = this.txtBarcode.Text.Trim();
        data.MAINPRODUCT = Convert.ToDouble(this.cmbProduct.SelectedItem.Value);
        data.OLDMAINPRODUCT = Convert.ToDouble(this.txtProduct.Text == "" ? "0" : this.txtProduct.Text);
        data.PROCESS = this.txtProcess.Text.Trim();
        data.RADIATION = (this.Radiation.Checked ? Constz.Radiation.Yes : Constz.Radiation.No);
        data.ITEM = ItemObj.GetItemList();
        return data;
    }

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
            ComboSource.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
            ComboSource.BuildCombo(this.cmbProduct, "V_PRODUCT_PRODUCE", "NAME", "LOID", "NAME", "ORDERTYPE = '" + Constz.OrderType.PD.Code + "' OR TYPE ='" + Constz.OrderType.AR.Code + "'", "เลือก", "0");

            SetData(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }

    #region Toolbar

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            SetData(Convert.ToDouble(this.cmbProduct.SelectedItem.Value));
            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อย");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect("BomSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        SetData(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
    }

    #endregion

    #region grvItem "Insert"

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NewRowDataBound(e.Row);
        }
    }

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvItemNew.Rows[0]);
        }
    }

    protected void txtBarcodeNew_TextChanged(object sender, EventArgs e)
    {
        txtBarcode_TextChanged((TextBox)sender, this.grvItemNew.Rows[0], "cmbMaterialNew", "txtProductTypeNew", "txtUnitNew", "txtUnitNameNew");
    }

    protected void cmbMaterialNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbMaterial_SelectedIndexChanged((DropDownList)sender, this.grvItemNew.Rows[0], "txtBarcodeNew", "txtProductTypeNew", "txtUnitNew", "txtUnitNameNew");
    }

    #endregion

    #region grvItem

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                DropDownList cmbMaterial = (DropDownList)e.Row.Cells[indexMATERIAL].FindControl("cmbMaterial");
                ComboSource.BuildCombo(cmbMaterial, "PRODUCT", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND PRODUCTGROUP IN (SELECT PG.LOID FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE WHERE PT.TYPE = '" + Constz.ProductType.Type.WH.Code + "') ", "เลือก", "0");
                cmbMaterial.SelectedIndex = cmbMaterial.Items.IndexOf(cmbMaterial.Items.FindByValue(drow["LOID"].ToString()));
                ControlUtil.SetDblTextBox5((TextBox)e.Row.Cells[indexMASTER].FindControl("txtMaster"));
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[indexBUTTON].FindControl("imbDelete");
                imbDelete.OnClientClick = "return confirm('ต้องการลบรายการวัตถุดิบ " + drow["NAME"].ToString() + " ใช่หรือไม่ ?')";
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            NewRowDataBound(e.Row);
        }
    }

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvItem.FooterRow);
        }
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        txtBarcode_TextChanged(txt, this.grvItem.Rows[rowIndex], "cmbMaterial", "txtProductType", "txtUnit", "txtUnitName");
    }

    protected void cmbMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        cmbMaterial_SelectedIndexChanged(cmb, this.grvItem.Rows[rowIndex], "txtBarcode", "txtProductType", "txtUnit", "txtUnitName");
    }

    protected void txtBarcodeNew_TextChanged1(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        txtBarcode_TextChanged(txt, this.grvItem.FooterRow, "cmbMaterialNew", "txtProductTypeNew", "txtUnitNew", "txtUnitNameNew");
    }

    protected void cmbMaterialNew_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        cmbMaterial_SelectedIndexChanged(cmb, this.grvItem.FooterRow, "txtBarcodeNew", "txtProductTypeNew", "txtUnitNew", "txtUnitNameNew");
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
            SetGrvItem();
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
        TextBox txtBarcode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[indexBARCODE].FindControl("txtBarcode");
        DropDownList cmbMaterial = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[indexMATERIAL].FindControl("cmbMaterial");
        TextBox txtMaster = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[indexMASTER].FindControl("txtMaster");
        Label txtProductType = (Label)this.grvItem.Rows[e.RowIndex].Cells[indexPRODUCTTYPE].FindControl("txtProductType");
        Label lblUnitName = (Label)this.grvItem.Rows[e.RowIndex].Cells[indexUNIT].FindControl("txtUnitName");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[indexUNIT].FindControl("txtUnit");

        e.NewValues["RANK"] = 0;
        e.NewValues["BARCODE"] = txtBarcode.Text.Trim();
        e.NewValues["NAME"] = cmbMaterial.SelectedItem.Text.Trim();
        e.NewValues["MASTER"] = txtMaster.Text;
        e.NewValues["PRODUCTTYPE"] = txtProductType.Text.Trim();
        e.NewValues["UNITNAME"] = lblUnitName.Text.Trim();
        e.NewValues["UNIT"] = txtUnit.Text;
        e.NewValues["LOID"] = cmbMaterial.SelectedItem.Value.ToString();
    }

    #endregion

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMainProduct(Convert.ToDouble(this.cmbProduct.SelectedItem.Value), "");
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetMainProduct(0, this.txtBarcode.Text.Trim());
    }

    #endregion
}
