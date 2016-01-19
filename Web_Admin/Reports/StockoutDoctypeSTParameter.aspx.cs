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

public partial class Reports_StockOutDocTypeParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(cmbRequisition, "DOCTYPE", "DOCNAME", "LOID", "DOCNAME", "TYPE= 'O' AND WAREHOUSE<>3", "ทั้งหมด", "0");
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
            if (ReportsFlow.CheckStockOutDocTypeSTReport(dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), dpTo.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), cmbRequisition.SelectedValue, txtInvcodeFrom.Text.Trim(), txtInvcodeTo.Text.Trim(), cmbCustomer.SelectedValue, Authz.CurrentUserInfo.Warehouse.ToString()) == true)
            {
                string temp = "";
                temp = "paramfield1=datefrom";
                temp += "&paramvalue1=" + dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString();
                temp += "&paramfield2=dateto";
                temp += "&paramvalue2=" + dpTo.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString();
                temp += "&paramfield3=type";
                temp += "&paramvalue3=" + cmbRequisition.SelectedValue;
                temp += "&paramfield4=invfrom";
                temp += "&paramvalue4=" + txtInvcodeFrom.Text.Trim();
                temp += "&paramfield5=invto";
                temp += "&paramvalue5=" + txtInvcodeTo.Text.Trim();
                temp += "&paramfield6=customer";
                temp += "&paramvalue6=" + cmbCustomer.SelectedValue;
                temp += "&paramfield7=warehouse";
                temp += "&paramvalue7=" + Authz.CurrentUserInfo.Warehouse.ToString();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockoutDoctypeSTReport", temp), true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockoutDoctypeReport", temp), true);

            }
            else
            {
                Appz.ClientAlert(Page, "ไม่พบข้อมูล");
                return;
            }
        }
    }
}
