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

public partial class FG_Transaction_PopupStockinReturn : System.Web.UI.Page
{
    private void SearchData()
    {
        SearchFlow flow = new SearchFlow();
        PopupStockinReturnData data = new PopupStockinReturnData();
        data.CODEFROM = this.txtCodeFrom.Text.Trim();
        data.CODETO = this.txtCodeTo.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.CUSTOMERNAME = this.txtCustomerName.Text.Trim();
        data.REFLOID = Convert.ToDouble(this.cmbRefType.SelectedValue);

        this.grvStockinReturn.DataSource = flow.GetStockinReturnList(data);
        this.grvStockinReturn.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbRefType, "V_RETURNTYPE_FG", "DOCNAME", "DOCTYPE", "SORTORDER", "");

            this.btnClose.OnClientClick = "window.close(); return false;";

            if (Request["type"] != null)
            {
                int value =  Convert.ToInt32(Request["type"]);

                    this.cmbRefType.SelectedValue = value.ToString();
            }
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvStickinReturn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((Label)e.Row.Cells[1].FindControl("lblNo")).Text = (e.Row.RowIndex + 1).ToString();
            //((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["PCODE"].ToString() + "|" + drow["PDNAME"].ToString() + "|" + drow["QTY"].ToString() + "|" + drow["UNAME"].ToString() + "|" + drow["CURPRICE"].ToString() + "|" + drow["DUEDATE"].ToString() + "|" + drow["PRCODE"].ToString() + "|" + drow["ISVAT"].ToString() + "|" + drow["LOID"].ToString() + "|" + drow["UNIT"].ToString() + "|" + drow["PRITEM"].ToString() + "'; window.close(); return false;";
            ((ImageButton)e.Row.Cells[0].FindControl("imbSelect")).OnClientClick = "window.returnValue='" + drow["REFLOID"].ToString() + "'; window.close(); return false;";
        }
    }
}
