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

public partial class Search_PopupStockinReturnWH : System.Web.UI.Page
{
    private void SearchData()
    {
        SearchFlow flow = new SearchFlow();
        PopupStockinReturnData data = new PopupStockinReturnData();
        data.BARCODE = this.txtBarcode.Text.Trim();
        data.PRODUCTNAME = this.txtBarcode.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.LOTNO = this.txtLot.Text.Trim();


        this.grvItem.DataSource = flow.GetStockinReturnWHList(data);
        this.grvItem.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            this.btnClose.OnClientClick = "window.close(); return false;";

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            //((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["PCODE"].ToString() + "|" + drow["PDNAME"].ToString() + "|" + drow["QTY"].ToString() + "|" + drow["UNAME"].ToString() + "|" + drow["CURPRICE"].ToString() + "|" + drow["DUEDATE"].ToString() + "|" + drow["PRCODE"].ToString() + "|" + drow["ISVAT"].ToString() + "|" + drow["LOID"].ToString() + "|" + drow["UNIT"].ToString() + "|" + drow["PRITEM"].ToString() + "'; window.close(); return false;";
            ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["LOID"].ToString() + "'; window.close(); return false;";
        }
    }
}
