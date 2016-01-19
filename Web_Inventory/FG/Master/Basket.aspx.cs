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
using ABB.Global;
using ABB.Data;
using ABB.Flow.Inventory.FG.Master;
using ABB.Data.Inventory.FG.Master;

public partial class FG_Master_Basket : System.Web.UI.Page
{
    private DataTable tempTable = null;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        SetScript();
        CreateTempTable();
        ControlUtil.SetDblTextBox(txtCost);
        ControlUtil.SetDblTextBox(txtPrice);
        ControlUtil.SetDblTextBox(txtSTDPrice);
        ControlUtil.SetIntTextBox(txtAddQuantity);
    }

    private void CreateTempTable()
    {
        tempTable = new DataTable();
        DataColumn dcORDERNO = new DataColumn("ORDERNO");
        DataColumn dcBARCODE = new DataColumn("BARCODE");
        DataColumn dcNAME = new DataColumn("PRODUCTNAME");
        DataColumn dcQUANTITY = new DataColumn("QUANTITY");
        DataColumn dcUNIT = new DataColumn("UNIT");
        DataColumn dcCOST = new DataColumn("COST");
        DataColumn dcPRICE = new DataColumn("PRICE");
        DataColumn dcSTDPRICE= new DataColumn("STDPRICE");
        DataColumn dcLOID = new DataColumn("LOID");
        DataColumn dcPUNIT = new DataColumn("PUNIT");

        tempTable.Columns.Add(dcORDERNO);
        tempTable.Columns.Add(dcBARCODE);
        tempTable.Columns.Add(dcNAME);
        tempTable.Columns.Add(dcQUANTITY);
        tempTable.Columns.Add(dcUNIT);
        tempTable.Columns.Add(dcCOST);
        tempTable.Columns.Add(dcPRICE);
        tempTable.Columns.Add(dcSTDPRICE);
        tempTable.Columns.Add(dcLOID);
        tempTable.Columns.Add(dcPUNIT);
    }

    private void SetScript()
    {
        string script = "";
        script = @"if(document.getElementById('" + txtCode.ClientID + @"').value == '')
                   { alert('กรุณาระบุรหัสกระเช้า'); return false; }
                   else if(document.getElementById('" + txtBasketName.ClientID + @"').value == '')
                   { alert('กรุณาระบุชื่อกระเช้า'); return false; }     
                   else if(document.getElementById('" + txtABBName.ClientID + @"').value == '')
                   { alert('กรุณาระบุชื่อย่อ'); return false; } 
                   else if(document.getElementById('" + txtBarcode.ClientID + @"').value == '')
                   { alert('กรุณาระบุบาร์โค้ด'); return false; } 
                   else if(document.getElementById('" + cmbUnit.ClientID + @"').value == '0')
                   { alert('กรุณาเลือกหน่วยนับ'); return false; } 
                   else if(document.getElementById('" + cmbProductType.ClientID + @"').value == '0')
                   { alert('กรุณาเลือกประเภทสินค้า'); return false; } 
                   else if(document.getElementById('" + cmbProductGroup.ClientID + @"').value == '0')
                   { alert('กรุณาเลือกกลุ่มสินค้า'); return false; } 
                   else if(document.getElementById('" + txtCost.ClientID + @"').value == '')
                   { alert('กรุณาระบุราคาทุน'); return false; } 
                   else if(document.getElementById('" + txtPrice.ClientID + @"').value == '')
                   { alert('กรุณาระบุราคาขาย'); return false; }
                   else if(document.getElementById('" + txtSTDPrice.ClientID + @"').value == '')
                   { alert('กรุณาระบุราคากลาง'); return false; }
                   ";
        ToolbarControl1.ClientClickSave = script;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetComboSource();
            //if (Request.QueryString["Barcode"] != null)
            //{
                //txtCode.Enabled = false;
                //txtBarcode.Enabled = false;
            LoadData(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            //}
        }
        lblOrderNo.Text = Convert.ToString(gvResult.Rows.Count + 1);
    }

    private void SetComboSource()
    {
        ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
        ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND TYPE = 'FG' ", "เลือก", "0");
        ComboSource.BuildCombo(cmbAddProduct, "V_PRODUCT_LIST", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "'", "เลือก", "0");
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");

    }

    private void LoadData(double loid)
    {
        double producttype = 0;
        DataTable dt = BasketFlow.GetProductByLoid(loid);
        if (dt.Rows.Count > 0)
        {
            txtLOID.Text = dt.Rows[0]["LOID"].ToString();
            txtCode.Text = dt.Rows[0]["CODE"].ToString();
            txtBasketName.Text = dt.Rows[0]["NAME"].ToString();
            txtABBName.Text = dt.Rows[0]["ABBNAME"].ToString();
            txtBarcode.Text = dt.Rows[0]["BARCODE"].ToString();
            cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(dt.Rows[0]["UNIT"].ToString()));
            producttype = BasketFlow.GetProductType(dt.Rows[0]["PRODUCTGROUP"].ToString());
            cmbProductType.SelectedIndex = cmbProductType.Items.IndexOf(cmbProductType.Items.FindByValue(producttype.ToString()));
            SetComboProductGroup();
            cmbProductGroup.SelectedIndex = cmbProductGroup.Items.IndexOf(cmbProductGroup.Items.FindByValue(dt.Rows[0]["PRODUCTGROUP"].ToString()));
            txtCost.Text = dt.Rows[0]["COST"].ToString();
            txtPrice.Text = dt.Rows[0]["PRICE"].ToString();
            txtSTDPrice.Text = dt.Rows[0]["STDPRICE"].ToString();
            txtProductMaster.Text = dt.Rows[0]["PRODUCTMASTER"].ToString();
            txtEname.Text = dt.Rows[0]["ENAME"].ToString();

            if (dt.Rows[0]["ACTIVE"].ToString() == "0")
                chkStatus.Checked = false;
            else if (dt.Rows[0]["ACTIVE"].ToString() == "1")
                chkStatus.Checked = true;

            if (dt.Rows[0]["ISVAT"].ToString() == Constz.VAT.Included.Code)
            {
                radVAT.Checked = true;
                radNoVAT.Checked = false;
            }
            else if (dt.Rows[0]["ISVAT"].ToString() == Constz.VAT.NotIncluded.Code)
            {
                radVAT.Checked = false;
                radNoVAT.Checked = true;
            }

            if (dt.Rows[0]["ISDISCOUNT"].ToString() == Constz.Discount.Calculated.Code)
            {
                radDiscount.Checked = true;
                radNoDiscount.Checked = false;
            }
            else if (dt.Rows[0]["ISDISCOUNT"].ToString() == Constz.Discount.NotCalculated.Code)
            {
                radDiscount.Checked = false;
                radNoDiscount.Checked = true;
            }

            
            if (dt.Rows[0]["ISREFUND"].ToString() == Constz.Refund.Yes.Code)
            {
                radRefund.Checked = true;
                radNoRefund.Checked = false;
            }
            else if (dt.Rows[0]["ISREFUND"].ToString() == Constz.Refund.No.Code)
            {
                radRefund.Checked = false;
                radNoRefund.Checked = true;
            }

            DataTable dtPackage = BasketFlow.GetProductInPackage(dt.Rows[0]["LOID"].ToString());

            tempTable.Rows.Clear();
            for (int i = 0; i < dtPackage.Rows.Count; i++)
            {
                DataRow dr = tempTable.Rows.Add();
                dr["LOID"] = dtPackage.Rows[i]["LOID"].ToString();
                dr["ORDERNO"] = Convert.ToString(i + 1);
                dr["BARCODE"] = dtPackage.Rows[i]["BARCODE"].ToString();
                dr["PRODUCTNAME"] = dtPackage.Rows[i]["PRODUCTNAME"].ToString();
                dr["QUANTITY"] = dtPackage.Rows[i]["QUANTITY"].ToString();
                dr["UNIT"] = dtPackage.Rows[i]["UNIT"].ToString();
                dr["PUNIT"] = dtPackage.Rows[i]["PUNIT"].ToString();
                dr["COST"] = dtPackage.Rows[i]["COST"].ToString();
                dr["PRICE"] = dtPackage.Rows[i]["PRICE"].ToString();
                dr["STDPRICE"] = dtPackage.Rows[i]["STDPRICE"].ToString();
            }

            Session["tempTab"] = tempTable;
            gvResult.DataSource = tempTable;
            gvResult.DataBind();
            txtRemark.Text = dt.Rows[0]["REMARK"].ToString();
            setHeader();
        }
        else
        {
            ClearControls();
            cmbAddProduct.SelectedIndex = -1;
            txtAddBarcode.Text = "";
            txtAddQuantity.Text = "1";
            txtAddUnit.Text = "";
            txtAddCost.Text = "";
            txtAddPrice.Text = "";
            txtAddSTDPrice.Text = "";
            txtAddLOID.Text = "";
            txtAddUnitLOID.Text = "";
            txtEname.Text = "";
        }
    }

    private void setHeader()
    {
        if (gvResult.Rows.Count == 0)
            PnlHeader.Visible = true;
        else
            PnlHeader.Visible = false;
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Session["tempTab"] = null;
        Response.Redirect("BasketSearch.aspx");
    }
   
    protected void CancelClick(object sender, EventArgs e)
    {
        LoadData(Convert.ToDouble(txtLOID.Text.Trim() == "" ? "0" : txtLOID.Text.Trim()));
    }

    private void ClearControls()
    {
        if (Request.QueryString["Barcode"] == null)
        {
            txtCode.Text = "";
            txtBarcode.Text = "";
        }

        txtBasketName.Text = "";
        txtABBName.Text = "";
        txtEname.Text = "";
        chkStatus.Checked = false;
        cmbUnit.SelectedIndex = -1;
        cmbProductType.SelectedIndex = -1;
        cmbProductGroup.Items.Clear(); 
        txtCost.Text = "";
        txtPrice.Text = "";
        txtSTDPrice.Text = "";
        radVAT.Checked = true;
        radNoVAT.Checked = false;
        radDiscount.Checked = true;
        radNoDiscount.Checked = false;
        gvResult.DataSource = "";
        gvResult.DataBind();
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (this.txtLOID.Text == "")
            InsertData();
        else
            UpdateData();
    }

    private void InsertData()
    {
        bool ret = true;
        BasketData bkData = new BasketData();
        BasketFlow bkFlow = new BasketFlow();
        bkData.LOID = Convert.ToDouble(txtLOID.Text.Trim() == "" ? "0" : txtLOID.Text.Trim());
        bkData.CODE = txtCode.Text.Trim();
        bkData.NAME = txtBasketName.Text.Trim();
        bkData.ABBNAME = txtABBName.Text.Trim();
        bkData.BARCODE = txtBarcode.Text.Trim();
        bkData.ENAME = txtEname.Text.Trim();
        bkData.UNITBASKET = Convert.ToDouble(cmbUnit.SelectedItem.Value);
        bkData.PRODUCTGROUP = Convert.ToDouble(cmbProductGroup.SelectedItem.Value);
        bkData.COST = Convert.ToDouble(txtCost.Text.Trim());
        bkData.PRICE = Convert.ToDouble(txtPrice.Text.Trim());
        bkData.STDPRICE = Convert.ToDouble(txtSTDPrice.Text.Trim());
        
        if (chkStatus.Checked == true )
            bkData.ACTIVE = "1";
        else
            bkData.ACTIVE = "0";
        
        if (radVAT.Checked == true)
            bkData.ISVAT = Constz.VAT.Included.Code;
        else if (radNoVAT.Checked == true)
            bkData.ISVAT = Constz.VAT.NotIncluded.Code;

        if (radDiscount.Checked == true)
            bkData.ISDISCOUNT = Constz.Discount.Calculated.Code;
        else if (radNoDiscount.Checked == true)
            bkData.ISDISCOUNT = Constz.Discount.NotCalculated.Code;

        if (radRefund.Checked == true)
            bkData.ISREFUND = Constz.Refund.Yes.Code;
        else if (radNoRefund.Checked == true)
            bkData.ISREFUND = Constz.Refund.No.Code;

        bkData.REMARK = txtRemark.Text.Trim();

        if (Session["tempTab"] != null)
            tempTable = (DataTable)Session["tempTab"];

        if (bkFlow.CheckCode(bkData.LOID, bkData.CODE.Trim()) == false)
            Appz.ClientAlert(Page, "รหัสกระเช้าซ้ำกับที่มีอยู่แล้ว");
        if (bkFlow.CheckName(bkData.LOID, bkData.NAME.Trim()) == false)
            Appz.ClientAlert(Page, "ชื่อกระเช้าซ้ำกับที่มีอยู่แล้ว");
        if (bkFlow.CheckBarcode(bkData.LOID, bkData.BARCODE.Trim()) == false)
            Appz.ClientAlert(Page, "บาร์โค้ดกระเช้าซ้ำกับที่มีอยู่แล้ว");

        ret = bkFlow.InsertData(Authz.CurrentUserInfo.UserID, bkData, tempTable);

        if (ret == false)
            Appz.ClientAlert(Page, bkFlow.ErrorMessage);
        else
        {
            LoadData(bkFlow.PBLOID);
            Appz.ClientAlert(Page, "ทำการบันทึกข้อมูลกระเช้าเรียบร้อย");
        } 
    }

    private void UpdateData()
    {
        bool ret = true;
        BasketData bkData = new BasketData();
        BasketFlow bkFlow = new BasketFlow();
        bkData.LOID = Convert.ToDouble(txtLOID.Text.Trim() == "" ? "0" : txtLOID.Text.Trim());
        bkData.CODE = txtCode.Text.Trim();
        bkData.NAME = txtBasketName.Text.Trim();
        bkData.ABBNAME = txtABBName.Text.Trim();
        bkData.BARCODE = txtBarcode.Text.Trim();
        bkData.UNITBASKET = Convert.ToDouble(cmbUnit.SelectedItem.Value);
        bkData.PRODUCTGROUP = Convert.ToDouble(cmbProductGroup.SelectedItem.Value);
        bkData.COST = Convert.ToDouble(txtCost.Text.Trim());
        bkData.PRICE = Convert.ToDouble(txtPrice.Text.Trim());
        bkData.STDPRICE = Convert.ToDouble(txtSTDPrice.Text.Trim());
        bkData.PRODUCTMASTER = Convert.ToDouble(txtProductMaster.Text.Trim());
        bkData.ENAME = txtEname.Text.Trim();

        if (chkStatus.Checked == true)
            bkData.ACTIVE = "1";
        else
            bkData.ACTIVE = "0";

        if (radVAT.Checked == true)
            bkData.ISVAT = Constz.VAT.Included.Code;
        else if (radNoVAT.Checked == true)
            bkData.ISVAT = Constz.VAT.NotIncluded.Code;

        if (radDiscount.Checked == true)
            bkData.ISDISCOUNT = Constz.Discount.Calculated.Code;
        else if (radNoDiscount.Checked == true)
            bkData.ISDISCOUNT = Constz.Discount.NotCalculated.Code;

        if (radRefund.Checked == true)
            bkData.ISREFUND = Constz.Refund.Yes.Code;
        else if (radNoRefund.Checked == true)
            bkData.ISREFUND = Constz.Refund.No.Code;

        bkData.REMARK = txtRemark.Text.Trim();

        if (Session["tempTab"] != null)
            tempTable = (DataTable)Session["tempTab"];

        if (bkFlow.CheckCode(bkData.LOID, bkData.CODE.Trim()) == false)
            Appz.ClientAlert(Page, "รหัสกระเช้าซ้ำกับที่มีอยู่แล้ว");
        if (bkFlow.CheckName(bkData.LOID, bkData.NAME.Trim()) == false)
            Appz.ClientAlert(Page, "ชื่อกระเช้าซ้ำกับที่มีอยู่แล้ว");

        if (bkFlow.CheckBarcode(bkData.LOID, bkData.BARCODE.Trim()) == false)
            Appz.ClientAlert(Page, "บาร์โค้ดกระเช้าซ้ำกับที่มีอยู่แล้ว");
        
        ret = bkFlow.UpdateData(Authz.CurrentUserInfo.UserID, bkData, tempTable);

        if (ret == false)
            Appz.ClientAlert(Page, bkFlow.ErrorMessage);
        else
        {
            //ClearControls();
            //LoadData(bkData.BARCODE);
            Appz.ClientAlert(Page, "ทำการแก้ไขข้อมูลกระเช้าเรียบร้อย");
            LoadData(bkFlow.PBLOID);
        }
    }

    protected void cmbAddProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbAddProduct.SelectedItem.Value == "0")
        {
            txtAddBarcode.Text = "";
            txtAddQuantity.Text = "1";
            txtAddUnit.Text = "";
            txtAddCost.Text = "";
            txtAddPrice.Text = "";
            txtAddSTDPrice.Text = "";
            txtAddLOID.Text = "";
            txtAddUnitLOID.Text = "";
        }
        else
        {
            SetAddProduct(); 
        }
    }

    private void SetAddProduct()
    {
        DataTable dt = BasketFlow.GetProduct(cmbAddProduct.SelectedItem.Value);
        if (dt.Rows.Count > 0)
        {
            txtAddBarcode.Text = dt.Rows[0]["BARCODE"].ToString();
            txtAddUnit.Text = dt.Rows[0]["UNAME"].ToString();
            txtAddCost.Text = dt.Rows[0]["COST"].ToString();
            txtAddPrice.Text = dt.Rows[0]["PRICE"].ToString();
            txtAddSTDPrice.Text = dt.Rows[0]["STDPRICE"].ToString();
            txtAddLOID.Text = dt.Rows[0]["LOID"].ToString();
            txtAddUnitLOID.Text = dt.Rows[0]["PUNIT"].ToString();
        }
        else
        {
            txtAddBarcode.Text = "";
            txtAddQuantity.Text = "1";
            txtAddUnit.Text = "";
            txtAddCost.Text = "";
            txtAddPrice.Text = "";
            txtAddSTDPrice.Text = "";
            txtAddLOID.Text = "";
            txtAddUnitLOID.Text = "";
        }
    }

    protected void imbAddSave_Click(object sender, ImageClickEventArgs e)
    {
        if (cmbAddProduct.SelectedItem.Value == "0")
        {
            Appz.ClientAlert(Page, "กรุณาเลือกสินค้าที่ต้องการก่อนทำการบันทึก");
            return;
        }
        else if (txtAddQuantity.Text.Trim() == "0" || txtAddQuantity.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุปริมาณการใช้ก่อนทำการบันทึก");
            return;
        }
        else
        {
            if (CheckProductExist() == false)
            {
                if (Session["tempTab"] != null)
                    tempTable = (DataTable)Session["tempTab"];

                DataRow dr = tempTable.Rows.Add();
                dr["LOID"] = txtAddLOID.Text.Trim();
                dr["ORDERNO"] = lblOrderNo.Text.Trim();
                dr["BARCODE"] = txtAddBarcode.Text.Trim();
                dr["PRODUCTNAME"] = cmbAddProduct.SelectedItem.Text.Trim();
                dr["QUANTITY"] = txtAddQuantity.Text.Trim();
                dr["UNIT"] = txtAddUnit.Text.Trim();
                dr["PUNIT"] = txtAddUnitLOID.Text.Trim();
                dr["COST"] = txtAddCost.Text.Trim();
                dr["PRICE"] = txtAddPrice.Text.Trim();
                dr["STDPRICE"] = txtAddSTDPrice.Text.Trim();
                Session["tempTab"] = tempTable;
                gvResult.DataSource = tempTable;
                gvResult.DataBind();
                SumAll(tempTable);
            }
        }
        cmbAddProduct.SelectedItem.Value = "0";
        cmbAddProduct.SelectedValue = "0";
        SetAddProduct(); 
        setHeader();
    }

    private void SumAll(DataTable tempTable)
    {
        //รวมราคา
        double cost = 0;
        double price = 0;
        double stdprice = 0;
        for (int i = 0; i < tempTable.Rows.Count; i++)
        {
            cost = cost + (Convert.ToDouble(tempTable.Rows[i]["QUANTITY"]) * Convert.ToDouble(tempTable.Rows[i]["COST"]));
            price = price + (Convert.ToDouble(tempTable.Rows[i]["QUANTITY"]) * Convert.ToDouble(tempTable.Rows[i]["PRICE"]));
            stdprice = stdprice + (Convert.ToDouble(tempTable.Rows[i]["QUANTITY"]) * Convert.ToDouble(tempTable.Rows[i]["STDPRICE"]));
        }
        txtCost.Text = cost.ToString();
        txtPrice.Text = price.ToString();
        txtSTDPrice.Text = stdprice.ToString();
    }

    private bool CheckProductExist()
    {
        for (int i = 0; i < gvResult.Rows.Count; i++)
        {
            Label lblBarCode = (Label)gvResult.Rows[i].FindControl("lblBarCode");

            if (txtAddBarcode.Text.Trim() == lblBarCode.Text.Trim())
            {
                Appz.ClientAlert(Page, "สินค้ามีอยู่ในรายการสินค้าแล้ว");
                return true;
            }
        }
        return false;
    }

    protected void gvResult_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gvResult = (GridView)sender;
        gvResult.EditIndex = e.NewEditIndex;
        gvResult.DataSource = Session["tempTab"];
        gvResult.DataBind();
    }

    protected void gvResult_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gvResult = (GridView)sender;
        gvResult.EditIndex = -1;
        gvResult.DataSource = Session["tempTab"];
        gvResult.DataBind();
    }

    protected void gvResult_DataBound(object sender, EventArgs e)
    {
        lblOrderNo.Text = Convert.ToString(gvResult.Rows.Count + 1);
    }

    protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        tempTable = (DataTable)Session["tempTab"];
        GridView gvResult = (GridView)sender;
        Label lblBarCode = (Label)gvResult.Rows[e.RowIndex].FindControl("lblBarCode");

        for (int i = 0; i < tempTable.Rows.Count; i++)
        {
            if (tempTable.Rows[i]["BARCODE"].ToString() == lblBarCode.Text.Trim())
            {
                tempTable.Rows[i].Delete();
            }
        }

        //เรื่องลำดับหมายเลขใหม่หลังจากทำการลบ
        for (int i = 0; i < tempTable.Rows.Count; i++)
        {
            tempTable.Rows[i]["ORDERNO"] = i + 1;
        }

        Session["tempTab"] = tempTable;
        gvResult.DataSource = tempTable;
        gvResult.DataBind();
        SumAll(tempTable);
    }

    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton imbDelete = (ImageButton)e.Row.FindControl("imbDelete");
        if (imbDelete != null)
            imbDelete.OnClientClick = "return confirm('คุณต้องการลบรายการใช่หรือไม่');";     
    }

    protected void gvResult_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        tempTable = (DataTable)Session["tempTab"];
        GridView gvResult = (GridView)sender;
        Label lblBarCode = (Label)gvResult.Rows[e.RowIndex].FindControl("lblBarCode");
        TextBox txtQuantity = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtQuantity");

        for (int i = 0; i < tempTable.Rows.Count; i++)
        {
            if (tempTable.Rows[i]["BARCODE"].ToString() == lblBarCode.Text.Trim())
            {
                tempTable.Rows[i]["QUANTITY"] = txtQuantity.Text.Trim();
            }
        }

        Session["tempTab"] = tempTable;
        gvResult.EditIndex = -1;
        gvResult.DataSource = tempTable;
        gvResult.DataBind();
        SumAll(tempTable);
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetComboProductGroup();
    }

    private void SetComboProductGroup()
    {
        string whr = "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND PRODUCTTYPE = " + cmbProductType.SelectedItem.Value;
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "", whr, "เลือก", "0");
    }

    protected void txtAddBarcode_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = BasketFlow.GetProductByBarcode(txtAddBarcode.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                cmbAddProduct.SelectedItem.Value = dt.Rows[0]["PRODUCT"].ToString();
                txtAddBarcode.Text = dt.Rows[0]["BARCODE"].ToString();
                txtAddUnit.Text = dt.Rows[0]["UNAME"].ToString();
                txtAddCost.Text = dt.Rows[0]["COST"].ToString();
                txtAddPrice.Text = dt.Rows[0]["PRICE"].ToString();
                txtAddSTDPrice.Text = dt.Rows[0]["STDPRICE"].ToString();
                txtAddLOID.Text = dt.Rows[0]["LOID"].ToString();
                txtAddUnitLOID.Text = dt.Rows[0]["PUNIT"].ToString();
            } 
    }
    protected void txtAddBarcode_TextChanged1(object sender, EventArgs e)
    {
        DataTable dt = BasketFlow.GetProductByBarcode(txtAddBarcode.Text.Trim());
        if (dt.Rows.Count > 0)
        {
           // cmbNewProduct.SelectedIndex = cmbNewProduct.Items.IndexOf(cmbNewProduct.Items.FindByValue(data.LOID.ToString()));
            cmbAddProduct.SelectedIndex = cmbAddProduct.Items.IndexOf(cmbAddProduct.Items.FindByValue(dt.Rows[0]["LOID"].ToString()));
            txtAddBarcode.Text = dt.Rows[0]["BARCODE"].ToString();
            txtAddUnit.Text = dt.Rows[0]["UNAME"].ToString();
            txtAddCost.Text = dt.Rows[0]["COST"].ToString();
            txtAddPrice.Text = dt.Rows[0]["PRICE"].ToString();
            txtAddSTDPrice.Text = dt.Rows[0]["STDPRICE"].ToString();
            txtAddLOID.Text = dt.Rows[0]["LOID"].ToString();
            txtAddUnitLOID.Text = dt.Rows[0]["PUNIT"].ToString();
        }
    }
}
