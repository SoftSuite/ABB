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
using ABB.Flow;
using ABB.Flow.Inventory.FG;
using ABB.Global;

public partial class FG_Transaction_StockinReturnSearch : System.Web.UI.Page
{
    private StockinReturnFlow _flow;
    public StockinReturnFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockinReturnFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvItem.ClientID + "_ctl', '_chkItem')"; }
    }

    private StockinReturnSearchData GetData()
    {
        StockinReturnSearchData data = new StockinReturnSearchData();
        data.CODE = this.txtCode.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.DOCTYPE = Convert.ToDouble(this.cmbDocType.SelectedItem.Value);
        data.CUSTOMER = Convert.ToDouble(this.cmbCustomer.SelectedItem.Value);
        data.NAME = this.txtCName.Text.Trim();
        data.LNAME = this.txtCLName.Text.Trim();
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        return data;
    }

    private void Search()
    {
        this.grvItem.DataSource = FlowObj.GetItemList(GetData());
        this.grvItem.DataBind();
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvItem.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvItem.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvItem.Rows[i].Cells[3].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildStockinReturnStatusCombo(this.cmbStatusFrom);
            ComboSource.BuildStockinReturnStatusCombo(this.cmbStatusTo);
            ComboSource.BuildCombo(this.cmbDocType, "V_RETURNTYPE_FG", "DOCNAME", "DOCTYPE", "SORTORDER", "", "เลือก", "0");
            ComboSource.BuildCombo(this.cmbCustomer, "V_CUSTOMER", "CUSTOMERNAME", "LOID", "CUSTOMERNAME", "", "เลือก", "0");
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบรับคืนสินค้า');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันใบรับคืนสินค้า');";
            this.trCustomer.Visible = false;
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        if (FlowObj.DeleteData(GetChecked()))
            Search();
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockinReturn.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateStockinReturnStatus(GetChecked(), Authz.CurrentUserInfo.UserID))
        {
            Search();
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {

        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");
            //btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.Purchase, Convert.ToDouble(drow["LOID"])) + "return false;";
            if (drow["DOCTYPE"].ToString() == Constz.DocType.RetInReduce.LOID.ToString())
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockinReturnPDRequest, Convert.ToDouble(drow["LOID"])) + " return false;";
            else if (drow["DOCTYPE"].ToString() == Constz.DocType.RetDistribute.LOID.ToString())
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.FGStockInReturnProduct, Convert.ToDouble(drow["LOID"])) + " return false;";
            else if (drow["DOCTYPE"].ToString() == Constz.DocType.RetFair.LOID.ToString())
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockInReturn, Convert.ToDouble(drow["LOID"])) + " return false;";
            else if (drow["DOCTYPE"].ToString() == Constz.DocType.RetInSample.LOID.ToString())
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockinReturnPDExam, Convert.ToDouble(drow["LOID"])) + " return false;";

            chk.Enabled = (drow["RANK"].ToString() == Constz.StockinReturn.Status.Waiting.Rank);

        }
    }
}
