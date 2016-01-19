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
using ABB.Flow.Reports;
using ABB.Global;

public partial class Transaction_ScheduleProduceParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            ControlUtil.SetIntTextBox(txtYear);        
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ReportsFlow.CheckScheduleProduce(int.Parse(txtYear.Text)-543,cmbMonth.SelectedValue,txtProduct.Text.Trim()) == true)
        {
            string temp = "";
            temp = "paramfield1=MONTH";
            temp += "&paramvalue1=" + cmbMonth.SelectedValue;
            temp += "&paramfield2=product";
            temp += "&paramvalue2=" + Server.UrlEncode(txtProduct.Text.Trim());
            temp += "&paramfield3=year";
            temp += "&paramvalue3=" + (int.Parse(txtYear.Text) - 543).ToString();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("ScheduleProduceReport", temp), true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("ProductSaleSummaryReport", temp), true);

        }
        else
        {
            Appz.ClientAlert(Page, "ไม่พบข้อมูล");
            return;
        }

    }

}

