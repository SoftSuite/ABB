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
using ABB.Data.Inventory.FG;
using ABB.Data.Search;
using ABB.Flow.Search;

public partial class Search_ProductionSearch : System.Web.UI.Page
{
    private void SearchData()
    {
        this.txtRefNo.Text = "";
        SearchFlow flow = new SearchFlow();
        StockoutSearchData data = new StockoutSearchData();
        data.REQCODETO = this.txtCodeTo.Text.Trim();
        data.REQCODEFROM = this.txtCodeFrom.Text.Trim();
        //data.REQUISITIONTYPE = Convert.ToDouble(this.cmbRequisitionType.SelectedValue);
        data.REQUESTDATEFROM = this.ctlDateFrom.DateValue;
        data.REQUESTDATETO = this.ctlDateTo.DateValue;
        data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedValue);

        string sType = Request["type"];
        if (sType == "12")
        {
            this.grvReserve.DataSource = flow.GetProductionList(data);
        }
        else
        {
            this.grvReserve.DataSource = flow.GetReqProductionList(data);
        }
        this.grvReserve.DataBind();
        // this.btnSelect.Visible = (this.grvReserve.Rows.Count > 0);
        if (this.grvReserve.SelectedValue == null)
            this.txtRefNo.Text = "";
        else
            this.txtRefNo.Text = this.grvReserve.SelectedValue.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");

            //this.cmbRequisitionType.DataBind();

            // this.btnSelect.OnClientClick = "window.returnValue=document.getElementById('" + this.txtRefNo.ClientID + "').value; window.close(); return false;";
            this.btnClose.OnClientClick = "window.returnValue=document.getElementById('" + this.txtRefNo.ClientID + "').value; window.close(); return false;";

            if (Request["code"] != null) this.txtRefNo.Text = Request["code"];
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
            ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["REFLOID"].ToString() + "'; window.close(); return false;";
        }
    }
}
