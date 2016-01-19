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
using ABB.Data.Search;
using ABB.Flow.Search;

public partial class Search_SupplierSearch : System.Web.UI.Page
{
    private void SearchData()
    {
        this.txtCustomer.Text = "";
        SearchFlow flow = new SearchFlow();
        SearchCustomerData data = new SearchCustomerData();
        data.CODE = this.txtCode.Text.Trim();
        data.FULLNAME = this.txtFullName.Text.Trim();
        this.grvCustomer.DataSource = flow.GetSupplierList(data);
        this.grvCustomer.DataBind();
        if (this.grvCustomer.SelectedValue == null)
            this.txtCustomer.Text = "";
        else
            this.txtCustomer.Text = this.grvCustomer.SelectedValue.ToString();
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

            if (Request["code"] != null) this.txtCode.Text = Request["code"];
            SearchData();
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["LOID"].ToString() + "'; window.close(); return false;";
        }
    }
}