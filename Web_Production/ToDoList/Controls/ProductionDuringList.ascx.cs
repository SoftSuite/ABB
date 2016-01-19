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
using ABB.Data.Production;
using ABB.Flow;
using ABB.Flow.Production;
using ABB.Global;

public partial class ToDoList_Controls_ProductionDuringList : System.Web.UI.UserControl
{
    private void SearchData()
    {
        ProductionToDoListFlow flow = new ProductionToDoListFlow();
        ProductionDuringListSearchData data = new ProductionDuringListSearchData();
        data.LOTNO = this.txtLotNo.Text.Trim();
        data.DATEFROM = this.dtpDateFrom.DateValue;
        data.DATETO = this.dtpDateTo.DateValue;
        data.PDNAME = this.txtPDName.Text.Trim();

        this.grvProductionDuring.DataSource = flow.GetProductionDuringList(data);
        this.grvProductionDuring.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvProductionDuring_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            HyperLink lnkLotNo = (HyperLink)e.Row.Cells[3].FindControl("lnkLotNo");
            lnkLotNo.Text = drow["LOTNO"].ToString();
            lnkLotNo.NavigateUrl = Constz.HomeFolder + "Transaction/Production.aspx?PDPLOID=" + drow["PDPLOID"].ToString() + "&PDLOID=" + drow["POLOID"].ToString();
        }
    }
}
