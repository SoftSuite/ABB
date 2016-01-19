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
using ABB.Data;
using ABB.Flow.Search;

public partial class Search_PopupStockinReturnSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Expires = 0;
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";

            this.btnClose.OnClientClick = "window.close(); return false;";
            SearchFlow flow = new SearchFlow();
            PopupStockinReturnSearchData data = new PopupStockinReturnSearchData();

            if (Request.QueryString["DOCTYPE"] != null)
            {
                data.DOCTYPE = Convert.ToDouble(Request.QueryString["DOCTYPE"].ToString() == "" ? "0" : Request.QueryString["DOCTYPE"].ToString());
            }
            if (Request.QueryString["CUSTOMER"] != null)
            {
                data.CUSTOMER = Convert.ToDouble(Request.QueryString["CUSTOMER"].ToString() == "" ? "0" : Request.QueryString["CUSTOMER"].ToString());
            }
            if (Request.QueryString["REFLOID"] != null)
            {
                data.REFLOID = Convert.ToDouble(Request.QueryString["REFLOID"].ToString() == "" ? "0" : Request.QueryString["REFLOID"].ToString());
            }
            this.grvItem.DataSource = flow.GetStockinReturnSearchList(data);
            this.grvItem.DataBind();
        }
    }
    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((Label)e.Row.Cells[1].FindControl("lblNo")).Text = (e.Row.RowIndex + 1).ToString();
            if (Convert.ToDouble(Request.QueryString["DOCTYPE"].ToString() == "" ? "0" : Request.QueryString["DOCTYPE"].ToString()) == Constz.DocType.RetInSample.LOID)
            {
                ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["PDLOID"].ToString() + ";" + drow["REFLOID"].ToString() + "'; window.close(); return false;";

            }
            else
            {
                ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["PDLOID"].ToString() + ";" + drow["LOTNO"].ToString() + "'; window.close(); return false;";
            }
        }
    }
}
