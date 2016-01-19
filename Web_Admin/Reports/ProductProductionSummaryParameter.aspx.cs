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

public partial class Reports_ProductProductionSummaryParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ComboSource.BuildCombo(cmbProduceGroup, "PRODUCEGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' ", "ทั้งหมด", "99");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
            if (ReportsFlow.CheckProductProductionSummaryReport(dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), dpTo.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), cmbProduceGroup.SelectedValue.ToString()) == true)
            {
                string temp = "";
                temp = "paramfield1=DATEFROM";
                temp += "&paramvalue1=" + dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year;
                temp += "&paramfield2=DATETO";
                temp += "&paramvalue2=" + dpTo.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year;
                temp += "&paramfield3=PRODUCEGROUP";
                temp += "&paramvalue3=" + cmbProduceGroup.SelectedValue.ToString();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("ProductProductionSummaryReport", temp), true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("ProductSaleSummaryReport", temp), true);

            }
            else
            {
                Appz.ClientAlert(Page, "ไม่พบข้อมูล");
                return;
            }

    }

}
