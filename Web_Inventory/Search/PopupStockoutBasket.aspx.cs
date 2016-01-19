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
using ABB.Data.Search;
using ABB.Flow.Search;

public partial class Search_PopupStockoutBasket : System.Web.UI.Page
{
    private void SearchData()
    {
        SearchFlow flow = new SearchFlow();
        PopupStockoutBasketData data = new PopupStockoutBasketData();
        data.BARCODE = this.txtBarCode.Text.Trim();
        data.NAME = this.txtBasketName.Text.Trim();

        this.grvReserve.DataSource = flow.GetBasketList(data);
        this.grvReserve.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Expires = 0;
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";

            this.btnClose.OnClientClick = "window.close(); return false;";
            SearchData();
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvReserve_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["LOID"].ToString() + "'; window.close(); return false;";
        }
    }
}
