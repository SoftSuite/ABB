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

public partial class ToDoList_Controls_ProductionWaitList : System.Web.UI.UserControl
{
    private void SearchData()
    {
        ProductionToDoListFlow flow = new ProductionToDoListFlow();
        ProductionWaitListSearchData data = new ProductionWaitListSearchData();
        data.CODEFROM = this.txtCodeFrom.Text.Trim();
        data.CODETO = this.txtCodeTo.Text.Trim();
        data.DATEFROM = this.dtpDateFrom.DateValue;
        data.DATETO = this.dtpDateTo.DateValue;
        data.PDNAME = this.txtPDName.Text.Trim();

        this.grvProductionWait.DataSource = flow.GetProductionWaitList(data);
        this.grvProductionWait.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvProductionWait_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "NewProduction")
        {
            string[] codeStr = e.CommandArgument.ToString().Split(';');
            Response.Redirect(Constz.HomeFolder + "Transaction/Production.aspx?RQLOID=" + codeStr[0] + "&PDLOID=" + codeStr[1]);
        }
    }


    protected void grvProductionWait_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ImageButton btnNew = (ImageButton)e.Row.Cells[3].FindControl("imbNew");
            btnNew.CommandArgument = drow["RQLOID"].ToString() + ";" + drow["PDLOID"].ToString();
        }

    }

    }
