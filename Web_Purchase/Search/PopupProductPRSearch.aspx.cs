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

public partial class Search_PopupProductPRSearch : System.Web.UI.Page
{
    private void SearchData()
    {
        SearchFlow flow = new SearchFlow();
        PopupProductPRSearchData data = new PopupProductPRSearchData();
        data.PRCODEFROM = this.txtFromPRCode.Text.Trim();
        data.PRCODETO = this.txtToPRCode.Text.Trim();
        data.DUEDATEFROM = this.ctlFromDueDate.DateValue;
        data.DUEDATETO = this.ctlToDueDate.DateValue;
        if (Convert.ToDouble(this.cmbPurchaseType.SelectedValue) != 0)
        {
            data.PURCHASETYPE = this.cmbPurchaseType.SelectedItem.Text;
        }
        if (Convert.ToDouble(this.cmbProduct.SelectedValue) != 0)
        {
            data.PRODUCT = this.cmbProduct.SelectedItem.Text;
        }
        if (Convert.ToDouble(this.cmbDivision.SelectedValue) != 0)
        {
            data.DIVISION = this.cmbDivision.SelectedItem.Text;
        }

        this.grvReserve.DataSource = flow.GetProductPRList(data);
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

            ComboSource.BuildCombo(this.cmbPurchaseType, "PURCHASETYPE", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            //this.cmbPurchaseType.DataBind();
            ComboSource.BuildCombo(this.cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbDivision, "DIVISION", "TNAME", "LOID", "TNAME", "", "ทั้งหมด", "0");

            this.btnClose.OnClientClick = "window.close(); return false;";

            //if (Request["code"] != null) this.txtCode.Text = Request["code"];
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
            //((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["PCODE"].ToString() + "|" + drow["PDNAME"].ToString() + "|" + drow["QTY"].ToString() + "|" + drow["UNAME"].ToString() + "|" + drow["CURPRICE"].ToString() + "|" + drow["DUEDATE"].ToString() + "|" + drow["PRCODE"].ToString() + "|" + drow["ISVAT"].ToString() + "|" + drow["LOID"].ToString() + "|" + drow["UNIT"].ToString() + "|" + drow["PRITEM"].ToString() + "'; window.close(); return false;";
            ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["PRITEM"].ToString() + "'; window.close(); return false;";
        }
    }
}
