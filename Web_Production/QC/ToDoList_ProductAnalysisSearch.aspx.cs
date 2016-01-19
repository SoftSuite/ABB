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

public partial class QC_ToDoList_ProductAnalysisSearch : System.Web.UI.Page
{
    private TodoListProductAnalysisFlow _flow;

    public TodoListProductAnalysisFlow FlowObj
    {
        get { if (_flow == null) _flow = new TodoListProductAnalysisFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvPDReserve.ClientID + "_ctl', '_chkItem')"; }
    }

    private QCAnalysisSearchData GetData()
    {
        QCAnalysisSearchData data = new QCAnalysisSearchData();
        data.QCCODE = this.txtCode.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.CODE = this.txtLotNo.Text.Trim();
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        return data;
    }

    private void Search()
    {
        this.grvPDReserve.DataSource = FlowObj.GetPDRequestList(GetData());
        this.grvPDReserve.DataBind();
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPDReserve.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPDReserve.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(this.grvPDReserve.Rows[i].Cells[3].Text + "#" + this.grvPDReserve.Rows[i].Cells[13].Text); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildStatusRankComboQC(this.cmbStatusFrom);
            ComboSource.BuildStatusRankComboQC(this.cmbStatusTo);
            Search();

            //this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบบันทึกรายการเพื่อการร้องขอเบิกวัตถุดิบและบรรจุภัณฑ์ใช่หรือไม่?');";
        }
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.SubmitQCStockIn(GetChecked(), Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "ยืนยันการแจ้งผลการตรวจ");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void grvPDReserve_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");
            HyperLink lnkAnalysis = (HyperLink)e.Row.Cells[4].FindControl("lnkAnalysis");

            //btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.Purchase, Convert.ToDouble(drow["STLOID"])) + "return false;";

            lnkAnalysis.Text = drow["QCCODE"].ToString();
            if (drow["TABLENAME"].ToString() == "STOCKIN")
                lnkAnalysis.NavigateUrl = Constz.HomeFolder + "QC/Transaction/QCAnalysis_PO.aspx?loid=" + drow["STLOID"].ToString();
            else
                lnkAnalysis.NavigateUrl = Constz.HomeFolder + "QC/Transaction/QCAnalysis_PD.aspx?loid=" + drow["STLOID"].ToString();

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.SendQC.Rank);

        }
    }
}


