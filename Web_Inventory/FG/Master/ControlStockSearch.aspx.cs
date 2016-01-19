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
using ABB.Flow.Inventory.FG.Master;
using ABB.Data.Inventory.FG.Master;
using ABB.Global;

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

public partial class FG_Master_ControlStockSearch : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ToolbarControl1.ClientClickDelete = "return confirm('คุณต้องการลบรายการสินค้า/วัตถุดิบที่เลือกใช่หรือไม่?');";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.txtWarehouse.Text = Request["warehouse"];
            txtWHName.Text = ControlStockSearchFlow.GetWarehouseName(Convert.ToDouble(this.txtWarehouse.Text));
        }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + gvResult.ClientID + "_ctl', '_chkItem')"; }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect("ControlStock.aspx?warehouse=" + this.txtWarehouse.Text);
    }

    protected void imbSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchProduct();
    }

    private void SearchProduct()
    {
        ControlStockSearchFlow csFlow = new ControlStockSearchFlow();
        ArrayList zArr = csFlow.GetSearchProduct(GetSearchData());
        gvResult.DataSource = zArr;
        gvResult.DataBind();
    }

    private ControlStockSearchData GetSearchData()
    {
        ControlStockSearchData data = new ControlStockSearchData();
        data.BARCODE = txtBarcode.Text.Trim();
        data.BARCODETO = txtBarcodeTo.Text.Trim();
        data.PRODUCTNAME = txtProductName.Text.Trim();
        data.WAREHOUSE = Convert.ToDouble(this.txtWarehouse.Text).ToString();
        return data;
    }

    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hplBarCode = (HyperLink)e.Row.FindControl("hplBarCode");
            hplBarCode.NavigateUrl = "ControlStock.aspx?warehouse=" + this.txtWarehouse.Text + "&Barcode=" + e.Row.Cells[4].Text.Trim();
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        DeleteData();
    }

    public void DeleteData()
    {
        double LOID;
        ArrayList arrLOID = new ArrayList();
        ControlStockSearchFlow csFlow = new ControlStockSearchFlow();
        bool ret = true;

        for (int i = 0; i < gvResult.Rows.Count; i++)
        {
            CheckBox chkItem = (CheckBox)gvResult.Rows[i].Cells[0].FindControl("chkItem");
            if (chkItem.Checked == true)
            {
                LOID = Convert.ToDouble(gvResult.Rows[i].Cells[1].Text);
                arrLOID.Add(LOID);
            }
        }
        if (arrLOID.Count != 0)
        {
            ret = csFlow.DeleteData(arrLOID);
        }

        if (ret == true)
        {
            Appz.ClientAlert(Page, "ทำการลบข้อมูลควบคุมปริมาณสินค้า/วัตถุดิบเรียบร้อย");
            SearchProduct();
        }
        else
            Appz.ClientAlert(Page, csFlow.ErrorMessage);
    }
}
