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

public partial class FG_ToDoList_Controls_StockOutControl : System.Web.UI.UserControl
{
    private ToDoListFlow _flow;
    private int indexLOID = 0;
    private int indexNew = 1;
    private int indexTypeName = 2;
    private int indexCode = 3;
    private int indexReqDate = 4;
    private int indexSenderName = 5;
    private int indexStatus = 6;
    private int indexLink = 7;
    private int indexCreateOn = 8;
    private int indexStoID = 9;
    private int indexSender = 10;

    public ToDoListFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ToDoListFlow(); } return _flow; }
    }

    private ToDoListStockOutData GetSearchData()
    {
        ToDoListStockOutData data = new ToDoListStockOutData();
        data.CODE = this.txtCode.Text.Trim();
        data.REQDATE = this.dtpReqDate.DateValue;
        data.REQUISITIONTYPE = Convert.ToDouble(this.cmbRequisitionType.SelectedItem.Value);
        data.PRODUCTNAME = this.txtProductName.Text.Trim();
        data.STATUS = this.cmbStatus.SelectedItem.Value;
        data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
        return data;
    }

    private void SearchData()
    {
        this.grvRequisition.DataSource = FlowObj.GetStockOutkList(GetSearchData());
        this.grvRequisition.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbRequisitionType, "V_REQTYPE_STOCKOUT", "REQUISITIONTYPENAME", "REQUISITIONTYPE", "REQUISITIONTYPENAME", "", "ทั้งหมด", "0");
            ComboSource.BuildStatusCombo(this.cmbStatus, "ทั้งหมด", "");
            this.cmbStatus.SelectedIndex = this.cmbStatus.Items.IndexOf(this.cmbStatus.Items.FindByValue(Constz.Requisition.Status.Approved.Code));
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
            HyperLink lnkRequisition = (HyperLink)e.Row.Cells[indexLink].FindControl("lnkStockOut");
            if (!Convert.IsDBNull(drow["STOCODE"]))
                lnkRequisition.Text = drow["STOCODE"].ToString();
            else
                lnkRequisition.Text = "";
            lnkRequisition.NavigateUrl = Constz.HomeFolder + "FG/Transaction/StockOut.aspx?loid=" + drow["STOID"].ToString();

            if (lnkRequisition.Text != "") ((ImageButton)e.Row.Cells[indexNew].FindControl("btnNew")).Visible = false;

            ((ImageButton)e.Row.Cells[indexNew].FindControl("btnNew")).CommandArgument = drow["LOID"].ToString();

        }
    }

    protected void grvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "new")
        {
            double requisition = Convert.ToDouble(e.CommandArgument);
            if (FlowObj.NewStockOut(Authz.CurrentUserInfo.UserID, requisition, Authz.CurrentUserInfo.Warehouse))
                Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockOut.aspx?loid=" + FlowObj.LOID.ToString());
            else
                Appz.ClientAlert(this.Page, FlowObj.ErrorMessage);
        }
    }
}
