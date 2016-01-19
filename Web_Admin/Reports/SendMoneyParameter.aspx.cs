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

public partial class Reports_SendMoneyParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (txtCodeFrom.Text.Trim() == "" || txtCodeTo.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "กรุณากรอกเลขที่ใบเสร็จให้ครบถ้วน");
            return;
        }

        else
        {
            if (ReportsFlow.CheckSendMoneyReport(txtCodeFrom.Text.Trim(),txtCodeTo.Text.Trim()) == true)
            {
                string temp = "";
                temp = "paramfield1=codefrom";
                temp += "&paramvalue1=" + txtCodeFrom.Text.Trim();
                temp += "&paramfield2=codeto";
                temp += "&paramvalue2=" + txtCodeTo.Text.Trim();

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript(Request["reportname"], temp), true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("SendMoneyReport", temp), true);
            }
            else
            {
                Appz.ClientAlert(Page, "ไม่พบข้อมูล");
                return;
            }
        }
    }
}
