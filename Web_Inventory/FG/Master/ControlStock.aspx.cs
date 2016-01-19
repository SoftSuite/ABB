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
using ABB.Flow.Inventory.FG.Master;
using ABB.Data.Inventory.FG.Master;

/// <summary>
/// Create by: Pom
/// Create Date: 18 Dec 2007
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

public partial class FG_Master_ControlStock : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ControlUtil.SetDblTextBox(txtStock);
        ControlUtil.SetDblTextBox(txtMinimum);
        ControlUtil.SetDblTextBox(txtMaximum);
        SetScript();
    }

    private void SetScript()
    {
        string eventtxtBarcode = "";
        eventtxtBarcode = @"
                            { document.getElementById('" + txtName.ClientID + @"').value = '';
                              document.getElementById('" + txtStock.ClientID + @"').value = '';
                              document.getElementById('" + txtMinimum.ClientID + @"').value = '';
                              document.getElementById('" + txtMaximum.ClientID + @"').value = '';
                            }";

        txtBarCode.Attributes["onKeyDown"] = eventtxtBarcode;
        txtBarCode.Attributes["onChange"] = eventtxtBarcode;

        //============================
        string script = "";
        script = @"if(document.getElementById('" + txtStock.ClientID + @"').value == '' || document.getElementById('" + txtStock.ClientID + @"').value == 0 )
                   { alert('กรุณาระบุปริมาณคงที่'); return false; }
                   else if(document.getElementById('" + txtMinimum.ClientID + @"').value == '' || document.getElementById('" + txtMinimum.ClientID + @"').value == 0)
                   { alert('กรุณาระบุปริมาณต่ำสุด'); return false; }     
                   else if(document.getElementById('" + txtMaximum.ClientID + @"').value == '' || document.getElementById('" + txtMaximum.ClientID + @"').value == 0)
                   { alert('กรุณาระบุปริมาณสูงสุด'); return false; } 
                   ";
        ToolbarControl1.ClientClickSave = script;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.txtWarehouse.Text = Request["warehouse"];
            txtWHName.Text = ControlStockFlow.GetWarehouseName(Convert.ToDouble(this.txtWarehouse.Text));
            if (Request.QueryString["Barcode"] != null)
            {
                txtBarCode.Enabled = false;
                imbSearch.Enabled = false;
                txtBarCode.Text = Request.QueryString["Barcode"].ToString();
                GetProduct(Request.QueryString["Barcode"].ToString());
                GetStocks(Request.QueryString["Barcode"].ToString());
            }
        }
    }

    protected void imbSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchProductDetails();       
    }

    private void SearchProductDetails()
    {
        ClearControls();
        string barcode = txtBarCode.Text.Trim();

        if (GetProduct(barcode) == true)
        {
            GetStocks(barcode);
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่พบสินค้า/วัตถุดิบตามบาร์โค้ดที่ระบุ");
        }
    }

    private bool GetProduct(string Barcode)
    {
        DataTable dt = ControlStockFlow.GetProduct(Barcode, Convert.ToDouble(this.txtWarehouse.Text).ToString());
        if (dt.Rows.Count > 0)
        {
            txtName.Text = dt.Rows[0]["NAME"].ToString();
            txtUnit.Text = dt.Rows[0]["UNIT"].ToString();
            txtUnitMaster.Text = dt.Rows[0]["UNITMASTER"].ToString();
            txtUnitStock.Text = dt.Rows[0]["UNITMASTER"].ToString();
            txtUnitMin.Text = dt.Rows[0]["UNITMASTER"].ToString();
            txtUnitMax.Text = dt.Rows[0]["UNITMASTER"].ToString();
            return true;
        }
        else
            return false;
    }

    private void GetStocks(string Barcode)
    {
        DataTable dt = ControlStockFlow.GetProductMinMax(Barcode, Convert.ToDouble(this.txtWarehouse.Text).ToString());
        if (dt.Rows.Count > 0)
        {
            txtStock.Text = dt.Rows[0]["STANDARD"].ToString();
            txtMinimum.Text = dt.Rows[0]["MINIMUM"].ToString();
            txtMaximum.Text = dt.Rows[0]["MAXIMUM"].ToString();
        }          
    }

    private void ClearControls()
    {
        txtName.Text = "";
        txtStock.Text = "";
        txtMinimum.Text = "";
        txtMaximum.Text = "";
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        if (txtBarCode.Enabled == true)
        {
            txtBarCode.Text = "";
            txtName.Text = "";
            txtStock.Text = "";
            txtMinimum.Text = "";
            txtMaximum.Text = "";
        }
        else
        {

            //txtStock.Text = "";
            //txtMinimum.Text = "";
            //txtMaximum.Text = "";
            GetProduct(txtBarCode.Text.Trim());
            GetStocks(txtBarCode.Text.Trim());
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect("ControlStockSearch.aspx?warehouse=" + this.txtWarehouse.Text);
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (ControlStockFlow.CheckProductExist(txtBarCode.Text.Trim()) == true)
        {
            if (txtName.Text.Trim() != "")
            {
                if (ControlStockFlow.CheckProductMinMaxExist(txtBarCode.Text.Trim(), Convert.ToDouble(this.txtWarehouse.Text).ToString()) == true)
                {
                    UpdateData();
                }
                else
                {
                    InsertData();
                }
            }
            else
            {
                ClearControls();
                Appz.ClientAlert(Page, "กรุณาทำการค้นหาสินค้า/วัตถุดิบที่ต้องการก่อนทำการบันทึก");      
            }
        }
        else
        {
            ClearControls();
            Appz.ClientAlert(Page, "ไม่พบสินค้า/วัตถุดิบตามบาร์โค้ดที่ระบุ");               
        }            
    }

    private void InsertData()
    {
        bool ret = true;
        ControlStockFlow csFlow = new ControlStockFlow();
        ControlStockData csData = new ControlStockData();

        csData.WAREHOUSE = Convert.ToDouble(this.txtWarehouse.Text);
        csData.PRODUCT = ControlStockFlow.GetProductLOID(txtBarCode.Text.Trim());
        if (txtStock.Text.Trim() != "")
            csData.STANDARD = Convert.ToDouble(txtStock.Text.Trim());
        if (txtMinimum.Text.Trim() != "")
            csData.MINIMUM = Convert.ToDouble(txtMinimum.Text.Trim());
        if (txtMaximum.Text.Trim() != "")
            csData.MAXIMUM = Convert.ToDouble(txtMaximum.Text.Trim());

        ret = csFlow.InsertData(Authz.CurrentUserInfo.UserID, csData);

        if (ret == false)
            Appz.ClientAlert(Page, csFlow.ErrorMessage);
        else
        {
            ClearControls();
            SearchProductDetails();
            Appz.ClientAlert(Page, "ทำการจัดเก็บข้อมูลควบคุมปริมาณสินค้า/วัตถุดิบเรียบร้อย");
        }
    }

    private void UpdateData()
    {
        bool ret = true;
        ControlStockFlow csFlow = new ControlStockFlow();
        ControlStockData csData = new ControlStockData();

        csData.LOID = ControlStockFlow.GetProductMinMaxLOID(txtBarCode.Text.Trim(), Convert.ToDouble(this.txtWarehouse.Text).ToString());
        if (txtStock.Text.Trim() != "")
            csData.STANDARD = Convert.ToDouble(txtStock.Text.Trim());
        if (txtMinimum.Text.Trim() != "")
            csData.MINIMUM = Convert.ToDouble(txtMinimum.Text.Trim());
        if (txtMaximum.Text.Trim() != "")
            csData.MAXIMUM = Convert.ToDouble(txtMaximum.Text.Trim());

        ret = csFlow.UpdateData(Authz.CurrentUserInfo.UserID, csData);

        if (ret == false)
            Appz.ClientAlert(Page, csFlow.ErrorMessage);
        else
        {
            ClearControls();
            SearchProductDetails();
            Appz.ClientAlert(Page, "ทำการแก้ไขข้อมูลควบคุมปริมาณสินค้า/วัตถุดิบเรียบร้อย");
        }
    }
}
