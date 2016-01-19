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
using ABB.Data.Inventory.FG;
using ABB.Flow.Inventory.FG;
using ABB.Global;

public partial class FG_ToDoList_Controls_ExpireControl : System.Web.UI.UserControl
{
    private ToDoListFlow _flow;
    private int indexLOID = 0;
    private int indexBarcode = 1;
    private int indexProductName = 2;
    private int indexLotno = 3;
    private int indexQty = 4;
    private int indexUnitName = 5;
    private int indexExpdate = 6;
    private int indexLoid = 7;

    public ToDoListFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ToDoListFlow(); } return _flow; }
    }

    private ToDoListExpireData GetSearchData()
    {
        ToDoListExpireData data = new ToDoListExpireData();
        data.PRODUCTNAME = this.txtProductName.Text.Trim();
        data.TIME = Convert.ToDouble(txtTime.Text == "" ? "0" : txtTime.Text);
        return data;
    }

    private void SearchData()
    {
        this.grvRequisition.DataSource = FlowObj.GetExpireList(GetSearchData());
        this.grvRequisition.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtTime.Text = "1";
            ControlUtil.SetIntTextBox(txtTime);
            SearchData();
            
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((Label)e.Row.Cells[0].FindControl("lblNo")).Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    protected void grvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "new")
        {
            double requisition = Convert.ToDouble(e.CommandArgument);
            if (FlowObj.NewStockOut(Authz.CurrentUserInfo.UserID, requisition, Authz.CurrentUserInfo.Warehouse))
                Response.Redirect(Constz.HomeFolder + "FG/Transaction/Expire.aspx?loid=" + FlowObj.LOID.ToString());
            else
                Appz.ClientAlert(this.Page, FlowObj.ErrorMessage);
        }
    }
}
