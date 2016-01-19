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
using ABB.Global;
using ABB.Data.Search;
using ABB.Flow.Search;

public partial class Search_InvoiceRequestSearch : System.Web.UI.Page
{
    private SearchFlow _flow;

    private SearchFlow FlowObj
    {
        get { if (_flow == null) { _flow = new SearchFlow(); } return _flow; }
    }

    private InvoiceRequestSearchData GetSearchData()
    {
        InvoiceRequestSearchData data = new InvoiceRequestSearchData();
        data.CUSTOMERCODE = this.txtMemberCode.Text.Trim();
        data.CUSTOMERNAME = this.txtMemberName.Text.Trim();
        data.INVCODE = this.txtInvcode.Text.Trim();
        data.PRODUCTNAME = this.txtProduct.Text.Trim();
        return data;
    }

    private void SearchData()
    {
        this.grvRequisition.DataSource = FlowObj.GetInvoiceRequestList(GetSearchData(), Convert.ToDouble(this.txtCurrentInvoice.Text == "" ? "0" : this.txtCurrentInvoice.Text));
        this.grvRequisition.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("cache-control", "private");
        Response.CacheControl = "no-cache";

        if (!IsPostBack)
        {
            if (Request["invoice"] != null) this.txtCurrentInvoice.Text = Request["invoice"];
        }
    }

    protected void grvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((Label)e.Row.Cells[1].FindControl("lblNo")).Text = (e.Row.RowIndex + 1).ToString();
            ((ImageButton)e.Row.Cells[0].FindControl("imbSelect")).OnClientClick = "window.returnValue = '" + drow["LOID"].ToString() + "';window.close(true);";
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

}
