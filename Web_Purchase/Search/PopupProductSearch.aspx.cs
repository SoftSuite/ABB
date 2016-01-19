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
using ABB.Data.Search;
using ABB.Flow.Search;
using ABB.Global;

public partial class Search_PopupProductSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Expires = 0;
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";

            ComboSource.BuildComboDistinct(this.cmbProductType, "V_PRODUCT_PR_LIST", "PTNAME", "PRODUCTTYPE", "PTNAME", "", "ทั้งหมด", "0");
            SetProductGroup();
            if (Request["barcode"] != null) this.txtBarcode.Text = Request["barcode"];
            //Search();
            this.btnClose.OnClientClick = "window.close(); return false;";
        }
    }

    private SearchProductData GetSearchData()
    {
        SearchProductData data = new SearchProductData();
        data.CODE = this.txtBarcode.Text.Trim();
        data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
        data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value);
        data.NAME = this.txtProductName.Text.Trim();
        return data;
    }

    private void Search()
    {
        SearchFlow sFlow = new SearchFlow();
        this.grvProduct.DataSource = sFlow.GetProductPRList(GetSearchData());
        this.grvProduct.DataBind();
    }

    private void SetProductGroup()
    {
        ComboSource.BuildComboDistinct(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "PRODUCTTYPE = " + this.cmbProductType.SelectedItem.Value + " AND ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetProductGroup();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void grvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ImageButton imbSelect = (ImageButton)e.Row.Cells[0].FindControl("imbSelect");
            imbSelect.OnClientClick = "window.returnValue='" + e.Row.Cells[1].Text + "'; window.close();";
        }
    }

}
