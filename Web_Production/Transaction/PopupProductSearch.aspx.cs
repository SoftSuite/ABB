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

public partial class Transaction_PopupProductSearch : System.Web.UI.Page
{
    private PopupProductFlow _flow;
    private PopupProductFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PopupProductFlow(); } return _flow; }
    }

    private void ResetSate(string sWhere)
    {
        this.grvRequisition.DataSource = FlowObj.GetDataList(sWhere);
        this.grvRequisition.DataBind();
    }
    private void SearchData1(string code)
    {
        //    SearchFlow flow = new SearchFlow();
        //    SearchSaleData data = new SearchSaleData();
        //    data.CUSTOMERCODE = this.txtMemberCode.Text.Trim();
        //    data.CUSTOMERNAME = this.txtMemberName.Text.Trim();
        //    data.INVCODE = this.txtInvcode.Text.Trim();
        //    SearchData();
        //SearchFlow flow = new SearchFlow();
        //SearchCustomerData data = new SearchCustomerData();
        //FlowObj.ClearSession();
        //SetData(FlowObj.GetCustomerList(code));
    }
    private void SetData(PDReserveData data)
    {
        this.txtLotno.Text = data.LOTNO;
        //this.PDDateFrom.Text = data.MFGDATE;
        //this.PDDateTo.Text = data.MFGDATE;
        this.txtPDName.Text = data.PDNAME;
        SearchData();
    }
    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvRequisition.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvRequisition.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvRequisition.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvRequisition.Rows[i].Cells[2].Text)); }
        }
        return arrLOID;
    }

    private void SearchData()
    {
        string sWhere = "";
        if (this.txtLotno.Text.Trim() != "")
        {
            sWhere += (sWhere == "" ? "" : " AND ") + "LOTNO LIKE '%" + this.txtLotno.Text.Trim() + "%'";
        }
        if (this.PDDateFrom.DateValue.Year != 1)
        {
            sWhere += (sWhere == "" ? "" : "AND ") + "MFGDATE >= " + this.PDDateFrom.DateValue + " ";
        }
        if (this.PDDateTo.DateValue.Year != 1)
        {
            sWhere += (sWhere == "" ? "" : "AND ") + "MFGDATE <= " + this.PDDateTo.DateValue + " ";
        }
        if (this.txtPDName.Text.Trim() != "")
        {
            sWhere += (sWhere == "" ? "" : " AND ") + "PDNAME LIKE '%" + this.txtPDName.Text.Trim() + "%'";
        }
        ViewState["sWhere"] = (sWhere == "" ? "" : " WHERE ") + sWhere;
        ResetSate(sWhere);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["code"] != null) this.txtLotno.Text = Request["code"];
            SearchData();
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        //Response.Redirect(Constz.HomeFolder + "Master/Unit.aspx");
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        try
        {
            FlowObj.DeleteData(GetChecked());
            ResetSate((string)ViewState["sWhere"]);
        }
        catch (Exception ex)
        {
            Appz.ClientAlert(this, ex.Message);
        }
    }

    protected void grvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((Label)e.Row.Cells[1].FindControl("lblNo")).Text = (e.Row.RowIndex + 1).ToString();
            ((ImageButton)e.Row.Cells[0].FindControl("imbSelect")).OnClientClick = "window.returnValue = '" + drow["LOTNO"].ToString() + "';window.close(true);";
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }
}
