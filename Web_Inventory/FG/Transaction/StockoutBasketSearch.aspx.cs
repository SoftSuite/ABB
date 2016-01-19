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

public partial class FG_Transaction_StockoutBasketSearch : System.Web.UI.Page
{
    private StockoutBasketFlow _flow;
    public StockoutBasketFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockoutBasketFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvItem.ClientID + "_ctl', '_chkItem')"; }
    }

    private StockoutBasketSearchData GetData()
    {
        StockoutBasketSearchData data = new StockoutBasketSearchData();
        data.CODE = this.txtBasketCode.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PDNAME = this.txtBasketName.Text.Trim();
        data.CREATEBY = this.txtCreateBy.Text.Trim();
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        return data;
    }

    private void Search()
    {
        this.grvItem.DataSource = FlowObj.GetBasketList(GetData());
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
            ComboSource.BuildStatusRankBasketCombo(this.cmbStatusFrom);
            ComboSource.BuildStatusRankBasketCombo(this.cmbStatusTo);
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบเบิก/คืนกระเช้า');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันใบเบิก/คืนกระเช้า');";
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
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockoutBasket.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateBasketStatus(GetChecked(), Authz.CurrentUserInfo.UserID))
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
            btnPrint.OnClientClick = Appz.ReportScript(Constz.Report.StockoutBasket, Convert.ToDouble(drow["LOID"])) + " return false;";
            btnPrint.CommandArgument = drow["LOID"].ToString();

            chk.Enabled = (drow["RANK"].ToString() == Constz.StockinReturn.Status.Waiting.Rank);

        }
    }
}
