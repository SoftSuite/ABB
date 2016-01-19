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
using ABB.Data.Inventory.WH;
using ABB.Data.Sales;
using ABB.Flow;
using ABB.Flow.Inventory.WH;
using ABB.Global;
using ABB.DAL;
using ABB.DAL.Inventory;
using System.IO;

public partial class WH_Transaction_Stockout : System.Web.UI.Page
{
    private StockoutWHFlow _flow2;
    private StockOutWHItem item;

    public StockoutWHFlow FlowObj
    {
        get { if (_flow2 == null) _flow2 = new StockoutWHFlow(); return _flow2; }
    }

    public StockOutWHItem ItemObj
    {
        get { if (item == null) item = new StockOutWHItem(); return item; }
    }

    #region GridView

    private void SetProductStock(DropDownList cmbLotNo, double product)
    {
        DataTable dt = FlowObj.GetProductStock(Authz.CurrentUserInfo.Warehouse, product);
        cmbLotNo.Items.Clear();
        cmbLotNo.DataSource = dt;
        cmbLotNo.DataTextField = "LOTNO";
        cmbLotNo.DataValueField = "LOTNO";
        cmbLotNo.DataBind();
        cmbLotNo.Items.Insert(0, new ListItem("เลือก", ""));
    }

    private void SetProductDetail(StockOutWHItemData data, GridViewRow gRow, TextBox txtBarcode, DropDownList cmbProduct, DropDownList cmbLotNo, TextBox txtUnit, Label lblUnitName, TextBox txtPrice, TextBox txtQty, TextBox txtRef)
    {
        txtBarcode.Text = data.BARCODE;
        cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.PRODUCT.ToString()));
        SetProductStock(cmbLotNo, data.PRODUCT);
        txtUnit.Text = data.UNIT.ToString();
        lblUnitName.Text = data.UNITNAME;
        txtPrice.Text = data.PRICE.ToString(Constz.DblFormat);
        txtQty.Text = data.QTY.ToString(Constz.IntFormat);
        txtRef.Text = data.REFLOID.ToString();

        //if (txtBarcode.ID == "txtBarcodeNew" && txtBarcode.Text != "")
        //    InsertData(gRow);
    }

    private void txtBarcode_TextChanged(TextBox txtBarcode, GridViewRow gRow, string ctlProductName, string ctlLotNoName, string ctlQtyName, string ctlUnitName, string ctlUnitNameName, string ctlPriceName, string ctlRefLOIDName)
    {
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl(ctlProductName);
        DropDownList cmbLotNo = (DropDownList)gRow.Cells[4].FindControl(ctlLotNoName);
        TextBox txtQty = (TextBox)gRow.Cells[5].FindControl(ctlQtyName);
        TextBox txtUnit = (TextBox)gRow.Cells[6].FindControl(ctlUnitName);
        Label lblUnitName = (Label)gRow.Cells[6].FindControl(ctlUnitNameName);
        TextBox txtPrice = (TextBox)gRow.Cells[6].FindControl(ctlPriceName);
        TextBox txtRefLOID = (TextBox)gRow.Cells[6].FindControl(ctlRefLOIDName);

        if (FlowObj.GetReqItemProductData(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text), Convert.ToDouble(this.txtPD.Text == "" ? "0" : this.txtPD.Text), txtBarcode.Text.Trim(),cmbDocType.SelectedValue))
        {
            StockOutWHItemData data = FlowObj.ReqItemProductData;
            SetProductDetail(data, gRow, txtBarcode, cmbProduct, cmbLotNo, txtUnit, lblUnitName, txtPrice, txtQty, txtRefLOID);
        }
    }

    private void cmbProduct_SelectedIndexChanged(DropDownList cmbProduct, GridViewRow gRow, string ctlBarcodeName, string ctlLotNoName, string ctlQtyName, string ctlUnitName, string ctlUnitNameName, string ctlPriceName, string ctlRefLOIDName)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[2].FindControl(ctlBarcodeName);
        DropDownList cmbLotNo = (DropDownList)gRow.Cells[4].FindControl(ctlLotNoName);
        TextBox txtQty = (TextBox)gRow.Cells[6].FindControl(ctlQtyName);
        TextBox txtUnit = (TextBox)gRow.Cells[7].FindControl(ctlUnitName);
        Label lblUnitName = (Label)gRow.Cells[7].FindControl(ctlUnitNameName);
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl(ctlPriceName);
        TextBox txtRefLOID = (TextBox)gRow.Cells[7].FindControl(ctlRefLOIDName);


        if (FlowObj.GetReqItemProductData(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text), Convert.ToDouble(this.txtPD.Text == "" ? "0" : this.txtPD.Text), Convert.ToDouble(cmbProduct.SelectedItem.Value),this.cmbDocType.SelectedValue))
            {
                StockOutWHItemData data = FlowObj.ReqItemProductData;
                SetProductDetail(data, gRow, txtBarcode, cmbProduct, cmbLotNo, txtUnit, lblUnitName, txtPrice, txtQty, txtRefLOID);
            }
    }

    protected void cmbLotNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        TextBox txtRemainQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[5].FindControl("txtRemainQty");
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[rowIndex].Cells[3].FindControl("cmbProduct");

        txtRemainQty.Text = FlowObj.GetRemainQTYStock(cmb.SelectedItem.Value, Convert.ToDouble(cmbProduct.SelectedValue)).ToString();



    }

    protected void cmbLotNoNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;

        TextBox txtRemainQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtRemainQtyNew");
        DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("cmbProductNew");

        txtRemainQty.Text = FlowObj.GetRemainQTYStock(cmb.SelectedItem.Value, Convert.ToDouble(cmbProduct.SelectedValue)).ToString();
    }

    protected void cmbLotNoNew_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmb = (DropDownList)sender;
        TextBox txtRemainQty = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtRemainQtyNew");
        DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[3].FindControl("cmbProductNew");

        txtRemainQty.Text = FlowObj.GetRemainQTYStock(cmb.SelectedItem.Value, Convert.ToDouble(cmbProduct.SelectedValue)).ToString();
    }

    private void InsertData(GridViewRow gRow)
    {
        TextBox txtBarcode = (TextBox)gRow.Cells[2].FindControl("txtBarcodeNew");
        DropDownList cmbProduct = (DropDownList)gRow.Cells[3].FindControl("cmbProductNew");
        DropDownList cmbLotNo = (DropDownList)gRow.Cells[4].FindControl("cmbLotNoNew");
        TextBox txtQty = (TextBox)gRow.Cells[6].FindControl("txtQtyNew");
        TextBox txtUnit = (TextBox)gRow.Cells[7].FindControl("txtUnitNew");
        Label lblUnitName = (Label)gRow.Cells[7].FindControl("lblUnitNameNew");
        TextBox txtPrice = (TextBox)gRow.Cells[7].FindControl("txtPriceNew");
        TextBox txtRefLOID = (TextBox)gRow.Cells[7].FindControl("txtRefLOIDNew");
        TextBox txtRemainQty = (TextBox)gRow.Cells[5].FindControl("txtRemainQtyNew");

        StockOutWHItemData data = new StockOutWHItemData();
        data.BARCODE = txtBarcode.Text;
        data.LOTNO = cmbLotNo.SelectedItem.Value;
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.PRODUCTNAME = cmbProduct.SelectedItem.Text;
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.REMAIN = Convert.ToDouble(txtRemainQty.Text == "" ? "0" : txtRemainQty.Text);
        data.REFLOID = Convert.ToDouble(txtRefLOID.Text == "" ? "0" : txtRefLOID.Text);
        data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
        data.UNITNAME = lblUnitName.Text.Trim();
        data.REQUISITION = Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text);

        if (ItemObj.InsertStockOutItem(data))
        {
            SetGrvItem(this.txtStatus.Text);
            Calculation();
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void NewRowDataBound(GridViewRow gRow)
    {
        if (this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPO.LOID.ToString())
        {
            ComboSource.BuildCombo((DropDownList)gRow.Cells[3].FindControl("cmbProductNew"), "PRODUCT", "NAME", "LOID", "NAME", "LOID IN (SELECT MATERIAL FROM BOM WHERE MAINPRODUCT = " + (this.txtPD.Text == "" ? "0 " : this.txtPD.Text) + ") ", "เลือก", "0");
            SetProductStock((DropDownList)gRow.Cells[4].FindControl("cmbLotNoNew"), Convert.ToDouble(((DropDownList)gRow.Cells[3].FindControl("cmbProductNew")).SelectedItem.Value));
            ControlUtil.SetIntTextBox((TextBox)gRow.Cells[6].FindControl("txtQtyNew"));
        }
        else if (this.cmbDocType.SelectedValue == "24")
        {
            ComboSource.BuildCombo((DropDownList)gRow.Cells[3].FindControl("cmbProductNew"), "PRODUCT", "NAME", "LOID", "NAME", "TYPE ='WH'", "เลือก", "0");
            SetProductStock((DropDownList)gRow.Cells[4].FindControl("cmbLotNoNew"), Convert.ToDouble(((DropDownList)gRow.Cells[3].FindControl("cmbProductNew")).SelectedItem.Value));
            ControlUtil.SetIntTextBox((TextBox)gRow.Cells[6].FindControl("txtQtyNew"));
        }
        else
        {            
            ComboSource.BuildCombo((DropDownList)gRow.Cells[3].FindControl("cmbProductNew"), "PRODUCT", "NAME", "LOID", "NAME", "LOID IN (SELECT PRODUCT FROM REQMATERIAL WHERE REQUISITION = " + (this.txtRefLoid.Text == "" ? "0 " : this.txtRefLoid.Text) + ") ", "เลือก", "0");
            SetProductStock((DropDownList)gRow.Cells[4].FindControl("cmbLotNoNew"), Convert.ToDouble(((DropDownList)gRow.Cells[3].FindControl("cmbProductNew")).SelectedItem.Value));
            ControlUtil.SetIntTextBox((TextBox)gRow.Cells[6].FindControl("txtQtyNew"));
        }
        
    }

    #endregion

    private void Calculation()
    {
        //double total = 0;
        //ArrayList arr = ItemObj.GetItemList();
        //for (int i = 0; i < arr.Count; ++i)
        //{
        //    StockOutItemData data = (StockOutItemData)arr[i];
        //    total += (data.QTY * data.PRICE);
        //}
        //this.txtTotal.Text = total.ToString(Constz.DblFormat);

    }

    private double GetNetPrice()
    {
        //double netprice = 0;
        //if (ViewState["STOCKITEM"] != null)
        //{
        //    DataTable dt = (DataTable)ViewState["STOCKITEM"];
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        netprice += Convert.ToDouble(dt.Rows[i]["PRICE"]) * Convert.ToDouble(dt.Rows[i]["QTY"]);
        //    }
        //}

        //return netprice;
        return ItemObj.GetNetPrice();
    }


    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        if (this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPO.LOID.ToString())
        {
            SetPDorder(FlowObj.GetProductData(loid));
        }
        else
        {
            SetPDorder(FlowObj.GetReqProductData(loid));
        }
    }

    private void ResetState2(double loid)
    {
        ItemObj.ClearSession();
        SetData2(FlowObj.GetData(loid));
    }

    private void SetPDorder(StockoutWHData data)
    {
        //this.txtWareHouse.Text = data.WAREHOUSE.ToString();
        this.txtPD.Text = data.PRODUCT.ToString();
        this.txtPDCode.Text = data.PRODUCTCODE;
        this.txtPDName.Text = data.PRODUCTNAME;
        this.txtAmount.Text = data.QTY.ToString();
        this.cmbDivision.SelectedValue = data.DIVISION.ToString();
        this.txtSupportCause.Text = data.SUPPORTCAUSE;
        this.txtSupportRefCode.Text = data.SUPPORTREFCODE;
        this.txtUnit.Text = data.UNIT;
        this.txtStatus.Text = Constz.Requisition.Status.Waiting.Code;
        this.txtRefNo.Text = data.CODE;
        this.ctlDueDate.DateValue = data.DUEDATE;
        this.ctlReqDate.DateValue = data.REQDATE;
        this.txtCustomer.Text = data.CUSTOMER.ToString();
        this.txtStatusName.Text = Constz.Requisition.Status.Waiting.Name;
        this.ctlCreateDate.DateValue = DateTime.Today;
        this.txtRef1.Text = data.LOID.ToString();
        this.txtRef2.Text = data.REFPROD.ToString();
        if (this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPO.LOID.ToString())
        {
            DataTable dtStock = FlowObj.GetProductLotWH(data.PRODUCT, Authz.CurrentUserInfo.Warehouse);
            ItemObj.CopyItem(dtStock);
            //ViewState["STOCKITEM"] = dtStock;
        }
        else
        {
            DataTable dtStock = FlowObj.GetReqProductLotWH(data.LOID, Authz.CurrentUserInfo.Warehouse);
            ItemObj.CopyItem(dtStock);
            //ViewState["STOCKITEM"] = dtStock;
        }


        SetGrvItem(this.txtStatus.Text);
        this.txtTotal.Text = GetNetPrice().ToString();
    }

    private void SetProduct(StockoutWHData data)
    {
        this.txtPD.Text = data.PRODUCT.ToString();
        this.txtPDCode.Text = data.PRODUCTCODE;
        this.txtPDName.Text = data.PRODUCTNAME;
        this.txtAmount.Text = data.QTY.ToString();
        this.txtUnit.Text = data.UNIT;
    }


    private void SetData2(StockoutWHData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtRefLoid.Text = data.REFLOID.ToString();
        this.txtStockCode.Text = data.CODE.ToString();
        this.txtStatus.Text = data.STATUS;
        this.ctlCreateDate.DateValue = data.CREATEON;
        this.txtCreateBy.Text = data.CREATEBY;
        this.ctlDueDate.DateValue = data.DUEDATE;
        this.ctlReqDate.DateValue = data.REQDATE;
        this.cmbDocType.SelectedIndex = this.cmbDocType.Items.IndexOf(this.cmbDocType.Items.FindByValue(data.REQUISITIONTYPE.ToString()));
        this.cmbDivision.SelectedValue = data.DIVISION.ToString();
        this.txtSupportCause.Text = data.SUPPORTCAUSE;
        this.txtSupportRefCode.Text = data.SUPPORTREFCODE;
        this.txtCustomer.Text = data.CUSTOMER.ToString();
        this.txtRefNo.Text = data.REQCODE;
        this.txtWareHouse.Text = data.WAREHOUSE.ToString();
        this.txtRef1.Text = data.REFLOID.ToString();
        this.txtRef2.Text = data.REFPROD.ToString();
        this.txtRemark.Text = data.REMARK;
        //this.txtCreateBy.Text = data.CREATEBY;
        // print 
        if (this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPO.LOID.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockoutExportMaterial, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        else if (this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPD.LOID.ToString())
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockoutMaterialWH, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";

        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.Approved.Code ? Constz.Requisition.Status.Approved.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : Constz.Requisition.Status.Waiting.Name));
        this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);

        if (this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPO.LOID.ToString())
        {
            SetProduct(FlowObj.GetProductData(data.REFPROD));
        }
        else
        {
            SetProduct(FlowObj.GetReqProductData(data.REFLOID));
        }

        //DataTable dtStock = FlowObj.GetStockOutItem(data.LOID.ToString());
        //ViewState["STOCKITEM"] = dtStock;
        //grvItem.DataSource = ViewState["STOCKITEM"];
        // grvItem.DataBind();
        SetGrvItem(this.txtStatus.Text);

        if (data.STATUS == Constz.Requisition.Status.Approved.Code || data.STATUS == Constz.Requisition.Status.Void.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
            this.ctlToolbar.BtnCancelShow = false;
        }
        this.txtTotal.Text = GetNetPrice().ToString();
    }

    private StockoutWHData GetData()
    {
        StockoutWHData data = new StockoutWHData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.REMARK = this.txtRemark.Text.Trim();
        data.DIVISION = Convert.ToDouble(this.cmbDivision.SelectedValue);
        data.DOCTYPE = Convert.ToDouble(this.cmbDocType.SelectedValue);
        data.SUPPORTCAUSE = this.txtSupportCause.Text ;
        data.SUPPORTREFCODE =this.txtSupportRefCode.Text ;
        data.REQUISITIONTYPE = Convert.ToDouble(this.cmbDocType.SelectedItem.Value);
        data.STATUS = this.txtStatus.Text.Trim();
        data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
        data.STOCKOUTITEM = ItemObj.GetItemList(); //FlowObj.GetProductLot(Convert.ToDouble(this.txtRefLoid.Text));
        data.DUEDATE = this.ctlDueDate.DateValue;
        data.REFLOID = Convert.ToDouble(this.txtRef1.Text.Trim());
        data.REQDATE = this.ctlReqDate.DateValue;
        data.INVCODE = this.txtInvCode.Text.Trim();
        data.CUSTOMER = Convert.ToDouble(this.txtCustomer.Text.Trim());
        data.SENDER = Authz.CurrentUserInfo.Warehouse;
        data.REFPROD = Convert.ToDouble(this.txtRef2.Text.Trim());
        if (data.REQUISITIONTYPE == Constz.DocType.ReqRawPO.LOID)
        {
            data.REFTABLE = "PDORDER";
            data.PRODUCTREF = "POITEM";
        }
        else
        {
            data.REFTABLE = "REQUISITION";
            data.PRODUCTREF = "PDPRODUCT";
        }
        return data;
    }

    #region Event


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbDocType, "DOCTYPE", "DOCNAME", "LOID", "DOCNAME", "LOID IN (" + Constz.DocType.ReqRawPD.LOID.ToString() + "," + Constz.DocType.ReqRawPO.LOID.ToString() + "," + Constz.DocType.RetSMaterial.LOID.ToString() + ")");
            ComboSource.BuildCombo(this.cmbDivision, "DIVISION", "TNAME", "LOID", "TNAME", "", "เลือก", "0");
            // ComboSource.BuildCombo(this.cmbTitle, "TITLE", "NAME", "LOID", "NAME", "");
           
            ResetState2(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));

        this.txtStatus.Text = Constz.Requisition.Status.Waiting.Code;
        this.txtCreateBy.Text = Authz.CurrentUserInfo.UserID;

        if (this.cmbDocType.SelectedValue == "24")
        {
            trName.Visible = false;
            trAmount.Visible = false;
            txtRefNo.Visible = false;
            btnSearch.Visible = false;
            trDivision.Visible = true;
            trSupportCause.Visible = true;
            trSupportRefCode.Visible = true;
        }
        else
        {
            trName.Visible = true;
            trAmount.Visible = true;
            txtRefNo.Visible = true;
            btnSearch.Visible = true;
            trDivision.Visible = false;
            trSupportCause.Visible = false;
            trSupportRefCode.Visible = false;
        }
        string scriptRefNo = "";
        scriptRefNo += "document.getElementById('" + this.txtRefLoid.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/ProductionSearch.aspx?type=' + document.getElementById('" + this.cmbDocType.ClientID + "').value + (document.getElementById('" + this.txtRefNo.ClientID + "').value == '' ? '' : '&code=' + escape(document.getElementById('" + this.txtRefNo.ClientID + "').value)) , '600', '550');";
        scriptRefNo += "if ('undefined' ==  document.getElementById('" + this.txtRefLoid.ClientID + "').value || '' == document.getElementById('" + this.txtRefLoid.ClientID + "').value) ";
        scriptRefNo += "{ return false; } ";

        this.btnSearch.OnClientClick = scriptRefNo;
        this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันรายการใช่หรือไม่?');";
}

    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        ResetState(Convert.ToDouble(this.txtRefLoid.Text));
    }


    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockoutSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text));
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (CheckSave())
        {
            if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
            {
                ResetState2(FlowObj.LOID);
                Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }

    private bool CheckSave()
    {
        bool bRet = true;
        if (this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPO.LOID.ToString() || this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPD.LOID.ToString())
        {
        
        DataTable dtStockItem = ItemObj.GetItemList();
        for (int i = 0; i < dtStockItem.Rows.Count; i++)
        {
            double product = Convert.ToDouble(dtStockItem.Rows[i]["PRODUCT"]);
            double Qty = 0;
            DataRow[] dr = dtStockItem.Select("PRODUCT = " + product.ToString());
            for (int j = 0; j < dr.Length; j++)
            {
                Qty = Qty + Convert.ToDouble(dr[j]["QTY"]);
            }

            DataTable dtItem;
            if (this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPO.LOID.ToString())
                dtItem = FlowObj.GetBOMItem(Convert.ToDouble(this.txtPD.Text));
            else 
                dtItem = FlowObj.GetReqmaterialItem(Convert.ToDouble(this.txtRefLoid.Text));

            DataRow[] drItem = dtItem.Select("PRODUCT = " + product.ToString());
            double allQty = Convert.ToDouble(drItem[0]["QTY"]);

            if (Qty > allQty)
            {
                bRet = false;
                Appz.ClientAlert(this, "จำนวนรวมของสินค้ามากกว่าใบขอเบิก");
                break;
            }
        }
        
        }return bRet;
    }

    protected void PrintClick(object sender, EventArgs e)
    {

    }

    #endregion



    protected void txtRefNo_TextChanged(object sender, EventArgs e)
    {
        //if (FlowObj.RequisitionLOID(this.txtRefNo.Text) != 0)
        //{
        //    ResetState(FlowObj.RequisitionLOID(this.txtRefNo.Text));
        //}
    }


    protected void SubmitClick(object sender, EventArgs e)
    {
        if (CheckSave())
        {
            //if (this.txtLOID.Text == "" || this.txtLOID.Text == "0")
            //{
            //    StockoutData data = new StockoutData();
            //    data.ACTIVE = Constz.ActiveStatus.Active;
            //    data.CODE = "";
            //    data.STATUS = Constz.Requisition.Status.Waiting.Code;
            //    data.REFLOID = 0;
            //    data.REQUISITIONTYPE = 11;

            //    if (FlowObj.NewRequisition(Authz.CurrentUserInfo.UserID, data))
            //        this.txtLOID.Text = FlowObj.LOID.ToString();
            //    else
            //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
            //}

            //if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData2()))
            //{
            //    ResetState2(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
            //    Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
            //}

            //else
            //    Appz.ClientAlert(this, FlowObj.ErrorMessage);

            StockoutWHData data = GetData();
            data.STATUS = Constz.Requisition.Status.Approved.Code;
            data.APPROVER = FlowObj.GetApprover(Authz.CurrentUserInfo.UserID);
            data.APPROVEDATE = DateTime.Now.Date;
            if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
            {

                ResetState2(FlowObj.LOID);
                Appz.ClientAlert(this, "อนุมัติรายการเรียบร้อยแล้ว");

            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }

        //if (FlowObj.UpdateRequisitionStatus2(Convert.ToDouble(txtLOID.Text), Constz.Requisition.Status.Approved.Code))
        //{
        //    Appz.ClientAlert(this, "ส่งข้อมูลเรียบร้อยแล้ว");
        //}
        //else
        //    Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    private void SetGrvItem(string status)
    {
        //this.txtTotal.Text = GetNetPrice().ToString();
        //this.grvItem.DataSource = ViewState["STOCKITEM"];
        //this.grvItemNew.DataSource = ReqFlowObj.GetRequisitionItemBlank();
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
            this.grvItem.Visible = (status != Constz.Requisition.Status.Waiting.Code && status != "");
            this.grvItemNew.Visible = (status == Constz.Requisition.Status.Waiting.Code || status == "");
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

    protected void txtBarcodeNew_TextChanged(object sender, EventArgs e)
    {
        txtBarcode_TextChanged((TextBox)sender, this.grvItemNew.Rows[0], "cmbProductNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
    }

    protected void cmbProductNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbProduct_SelectedIndexChanged((DropDownList)sender, this.grvItemNew.Rows[0], "txtBarcodeNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
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
                DropDownList cmbLotNo = (DropDownList)e.Row.Cells[4].FindControl("cmbLotNo");
                if (this.cmbDocType.SelectedValue == Constz.DocType.ReqRawPO.LOID.ToString())
                {
                    ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "LOID IN (SELECT MATERIAL FROM BOM WHERE MAINPRODUCT = " + (this.txtPD.Text == "" ? "0 " : this.txtPD.Text) + ") ");
                }
                else
                {
                    ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "LOID IN (SELECT PRODUCT FROM REQMATERIAL WHERE REQUISITION = " + (this.txtRefLoid.Text == "" ? "0 " : this.txtRefLoid.Text) + ") ");
                }
                SetProductStock(cmbLotNo, Convert.ToDouble(drow["PRODUCT"]));
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                cmbLotNo.SelectedIndex = cmbLotNo.Items.IndexOf(cmbLotNo.Items.FindByValue(drow["LOTNO"].ToString()));

                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[6].FindControl("txtQty"));
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                imbDelete.Attributes.Add("onclick", "return confirm('ยืนยันการลบสินค้า?');");
                //DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                //ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");
                //cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            NewRowDataBound(e.Row);
        }
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        TextBox txtBarcode = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txtBarcode.Parent.Parent).RowIndex;
        txtBarcode_TextChanged(txtBarcode, this.grvItem.Rows[rowIndex], "cmbProduct", "cmbLotNo", "txtQty", "txtUnit", "lblUnitName", "txtPrice", "txtRefLOID");
    }

    protected void txtBarcodeNew_TextChanged1(object sender, EventArgs e)
    {
        TextBox txtBarcode = (TextBox)sender;
        txtBarcode_TextChanged(txtBarcode, this.grvItem.FooterRow, "cmbProductNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cmbProduct = (DropDownList)sender;
        Int16 rowIndex = (Int16)((GridViewRow)cmbProduct.Parent.Parent).RowIndex;
        cmbProduct_SelectedIndexChanged(cmbProduct, this.grvItem.Rows[rowIndex], "txtBarcode", "cmbLotNo", "txtQty", "txtUnit", "lblUnitName", "txtPrice", "txtRefLOID");
    }

    protected void cmbProductNew_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList cmbProduct = (DropDownList)sender;
        cmbProduct_SelectedIndexChanged(cmbProduct, this.grvItem.FooterRow, "txtBarcodeNew", "cmbLotNoNew", "txtQtyNew", "txtUnitNew", "lblUnitNameNew", "txtPriceNew", "txtRefLOIDNew");
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
            Calculation();
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
        TextBox txtBarcode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("txtBarcode");
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("cmbProduct");
        DropDownList cmbLotNo = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("cmbLotNo");
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtQty");
        TextBox txtUnit = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtUnit");
        Label lblUnitName = (Label)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("lblUnitName");
        TextBox txtPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtPrice");
        TextBox txtRefLOID = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtRefLOID");
        StockOutWHItemData data = new StockOutWHItemData();
        data.BARCODE = txtBarcode.Text.Trim();
        data.LOTNO = cmbLotNo.SelectedItem.Value;
        data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.PRODUCTNAME = cmbProduct.SelectedItem.Text;
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.REFLOID = Convert.ToDouble(txtRefLOID.Text == "" ? "0" : txtRefLOID.Text);
        data.UNIT = Convert.ToDouble(txtUnit.Text == "" ? "0" : txtUnit.Text);
        data.UNITNAME = lblUnitName.Text.Trim();

        e.NewValues["NO"] = "0";
        e.NewValues["BARCODE"] = data.BARCODE;
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["PRODUCTNAME"] = data.PRODUCTNAME;
        e.NewValues["LOTNO"] = data.LOTNO;
        e.NewValues["QTY"] = data.QTY.ToString();
        e.NewValues["UNIT"] = data.UNIT.ToString();
        e.NewValues["PRICE"] = data.PRICE.ToString();
        e.NewValues["UNITNAME"] = data.UNITNAME;
        e.NewValues["REFLOID"] = data.REFLOID;
        e.NewValues["REQUISITION"] = (this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text);
    }

    #endregion

    private DataTable OrderDataTable(DataTable dt)
    {
        int i = 1;
        foreach (DataRow dr in dt.Rows)
        {
            dr["Rank"] = i;
            i++;
        }

        return dt;
    }

    protected void cmbDocType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        this.txtRefLoid.Text = "0";
        ResetState(Convert.ToDouble(this.txtRefLoid.Text));
        if (this.cmbDocType.SelectedValue == "24")
        {
            trName.Visible = false;
            trAmount.Visible = false;
            txtRefNo.Visible = false;
            btnSearch.Visible = false;
            trDivision.Visible = true;
            trSupportCause.Visible = true;
            trSupportRefCode.Visible = true;
        }
        else
        {
            trName.Visible = true;
            trAmount.Visible = true;
            txtRefNo.Visible = true;
            btnSearch.Visible = true;
            trDivision.Visible = false;
            trSupportCause.Visible = false;
            trSupportRefCode.Visible = false;
        }
    }
}
