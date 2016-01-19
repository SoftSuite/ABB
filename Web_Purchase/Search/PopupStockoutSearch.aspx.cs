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
using ABB.Data.Sales;
using ABB.Data.Search;
using ABB.Flow.Search;
public partial class Search_PopupStockoutSearch : System.Web.UI.Page
{
    private void SearchData()
    {
        SearchFlow flow = new SearchFlow();
        PopupStockoutSearchData data = new PopupStockoutSearchData();
        data.CODEFROM = this.txtCodeFrom.Text.Trim();
        data.CODETO = this.txtCodeTo.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PRODUCTNAME = this.txtProductName.Text.Trim();

        this.grvReserve.DataSource = flow.GetProductReturnList(data);
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

            if (Request["code"] != null)
            {
                this.txtCodeFrom.Text = Request["code"];
                this.txtCodeTo.Text = Request["code"];
            }
            SearchData();

            Response.Expires = 0;
            //Response.Expiresabsolute = DateTime.Now.AddDays(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";
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
            //((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["PCODE"].ToString() + "|" + drow["PDNAME"].ToString() + "|" + drow["QTY"].ToString() + "|" + drow["UNAME"].ToString() + "|" + drow["CURPRICE"].ToString() + "|" + drow["DUEDATE"].ToString() + "|" + drow["PRCODE"].ToString() + "|" + drow["ISVAT"].ToString() + "|" + drow["LOID"].ToString() + "|" + drow["UNIT"].ToString() + "|" + drow["PRITEM"].ToString() + "'; window.close(); return false;";
            ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["LOID"].ToString() + "'; window.close(); return false;";
        }
    }
}

