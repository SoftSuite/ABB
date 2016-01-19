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

public partial class Reports_StockinDoctypeParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(cmbDocType, "DOCTYPE ", "DOCNAME", "LOID", "DOCNAME", "TYPE='I' AND WAREHOUSE<>3", "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbCustomer, "V_CUSTOMER", "CUSTOMERNAME", "LOID", "CUSTOMERNAME", "", "ทั้งหมด", "0");
        }

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (dpFrom.DateValue.Year == 1 && dpTo.DateValue.Year != 1)
        {
            Appz.ClientAlert(Page, "กรุณากรอกวันที่ให้ครบถ้วน");
            return;
        }

        else if (dpFrom.DateValue.Year != 1 && dpTo.DateValue.Year == 1)
        {
            Appz.ClientAlert(Page, "กรุณากรอกวันที่ให้ครบถ้วน");
            return;
        }

        else
        {
            if (ReportsFlow.CheckStockinDocTypeReport(dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), dpTo.DateValue.ToString("dd/MM/") + dpTo.DateValue.Year.ToString(), cmbDocType.SelectedValue, txtInvcodeFrom.Text.Trim(), txtInvcodeTo.Text.Trim(), cmbCustomer.SelectedValue) == true)
            {
                string temp = "";
                temp = "paramfield1=datefrom";
                temp += "&paramvalue1=" + dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString();
                temp += "&paramfield2=dateto";
                temp += "&paramvalue2=" + dpTo.DateValue.ToString("dd/MM/") + dpTo.DateValue.Year.ToString();
                temp += "&paramfield3=doctype";
                temp += "&paramvalue3=" + cmbDocType.SelectedValue;
                temp += "&paramfield4=invcodefrom";
                temp += "&paramvalue4=" + txtInvcodeFrom.Text.Trim();
                temp += "&paramfield5=invcodeto";
                temp += "&paramvalue5=" + txtInvcodeTo.Text.Trim();
                temp += "&paramfield6=customer";
                temp += "&paramvalue6=" + cmbCustomer.SelectedValue;


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockinDoctypeReport", temp), true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockinDoctypeReport", temp), true);

            }
            else
            {
                Appz.ClientAlert(Page, "ไม่พบข้อมูล");
                return;
            }
        }

    }
}
