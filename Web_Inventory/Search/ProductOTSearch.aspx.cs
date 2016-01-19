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

public partial class Search_ProductWHSearch : System.Web.UI.Page
{
    private void SearchData()
    {
        this.txtRefNo.Text = "";
        SearchFlow flow = new SearchFlow();
        StockInFGData data = new StockInFGData();
        data.CODETO = this.txtCodeTo.Text.Trim();
        data.CODEFROM = this.txtCodeFrom.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedValue);
        data.SENDER = Convert.ToDouble(this.cmbSupplier.SelectedValue);

        this.grvReserve.DataSource = flow.GetProductOTList(data);

        this.grvReserve.DataBind();
        // this.btnSelect.Visible = (this.grvReserve.Rows.Count > 0);
        if (this.grvReserve.SelectedValue == null)
            this.txtRefNo.Text = "";
        else
            this.txtRefNo.Text = this.grvReserve.SelectedValue.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbProduct, "V_RAW_LIST", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            this.btnClose.OnClientClick = "window.returnValue=document.getElementById('" + this.txtRefNo.ClientID + "').value + '|' + document.getElementById('" + this.txtPDLoid.ClientID + "').value ; window.close(); return false;";

            if (Request["code"] != null) this.txtRefNo.Text = Request["code"];
            if (Request["sender"] != null)
                ComboSource.BuildCombo(this.cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", " LOID = " + Request["sender"]);
            else
                ComboSource.BuildCombo(this.cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", "", "ทั้งหมด", "0");


            SearchData();
        }

    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    //protected void grvReserve_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtRefNo.Text = grvReserve.SelectedDataKey["LOID"].ToString();
    //    txtPDLoid.Text = grvReserve.SelectedDataKey["PDLOID"].ToString();
    //}

    protected void grvReserve_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["PDLOID"].ToString() + "|" + drow["BARCODE"].ToString() + "|" + drow["CODE"].ToString() + "|" + drow["QTY"].ToString() + "|" + drow["REMAIN"].ToString() + "|" + drow["UNIT"].ToString() + "|" + drow["LOID"].ToString() + "|" + drow["PRICE"].ToString() + "'; window.close(); return false;";
        }
    }
}

