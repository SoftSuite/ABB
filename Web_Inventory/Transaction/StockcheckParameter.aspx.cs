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
using ABB.Global;
using ABB.Flow.Reports;

public partial class Reports_StockcheckParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(cmbBatchNo, "STOCKCHECK", "BATCHNO", "LOID", "BATCHNO", "", "ทั้งหมด", "0");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ReportsFlow.StockCheckBatchNoIncreaseReport(cmbBatchNo.SelectedValue) == true)
        {
            string temp = "";
            temp = "paramfield1=stockcheck";
            temp += "&paramvalue1=" + cmbBatchNo.SelectedItem.Value;
            if (RdIncrease.Checked == true)
            {
                //temp += "&paramfield2=I";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("Stockimproveincrease", temp), true);
            }
            if (RdDecrease.Checked == true)
            {
                //temp += "&paramvalue2=D";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockRemainReport", temp), true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("Stockimprovedecrease", temp), true);
            }
        }
        else if (ReportsFlow.StockCheckBatchNoDecreaseReport(cmbBatchNo.SelectedValue) == true)
        {
            string temp = "";
            temp = "paramfield1=stockcheck";
            temp += "&paramvalue1=" + cmbBatchNo.SelectedItem.Value;
            if (RdIncrease.Checked == true)
            {
                //temp += "&paramfield2=I";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("Stockimproveincrease", temp), true);
            }
            if (RdDecrease.Checked == true)
            {
                //temp += "&paramvalue2=D";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockRemainReport", temp), true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("Stockimprovedecrease", temp), true);
            }
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่พบข้อมูล");
            return;
        }
    }
    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbBatchNo, "STOCKCHECK", "BATCHNO", "LOID", "", "BATCHNO = " + cmbBatchNo.SelectedValue + "", "ทั้งหมด", "0");
    }
}
