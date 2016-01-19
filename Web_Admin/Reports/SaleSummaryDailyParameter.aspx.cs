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
using ABB.Data.Search;
using ABB.Global;
using ABB.Flow.Reports;

public partial class Reports_SaleSummaryDailyParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlUtil.SetYearTextbox(this.txtYear);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (txtYear.Text == "")
        {
            Appz.ClientAlert(Page, "กรุณากรอกปีที่ต้องการ");
            return;
        }

        else
        {
            if (ReportsFlow.CheckSaleSummaryDailyReport(txtYear.Text.Trim()) == true)
            {
                string temp = "";
                temp = "paramfield1=year";
                temp += "&paramvalue1=" + txtYear.Text.Trim();


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("SaleSummaryDailyReport", temp), true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("SaleSummaryReport", temp), true);

            }
            else
            {
                Appz.ClientAlert(Page, "ไม่พบข้อมูล");
                return;
            }
        }
    }
}
