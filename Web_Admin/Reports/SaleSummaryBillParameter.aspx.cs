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

public partial class Reports_SaleSummaryBillParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.trCustomer.Visible = false;
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


        //else if (txtInvcodeFrom.Text.Trim() == "" && txtInvcodeTo.Text.Trim() != "")
        //{
        //    Appz.ClientAlert(Page, "กรุณาระบุช่วงเลขที่เอกสารให้ครบถ้วน");
        //    return;
        //}

        //else if (txtInvcodeFrom.Text.Trim() != "" && txtInvcodeTo.Text.Trim() == "")
        //{
        //    Appz.ClientAlert(Page, "กรุณาระบุช่วงเลขที่เอกสารให้ครบถ้วน");
        //    return;
        //}

        else
        {
            if (ReportsFlow.CheckSaleSummaryBillReport(dpFrom.DateValue.Year.ToString() + "/" + dpFrom.DateValue.ToString("MM/dd") , dpFrom.DateValue.Year.ToString() + "/" + dpTo.DateValue.ToString("MM/dd"), txtInvcodeFrom.Text.Trim(), txtInvcodeTo.Text.Trim(), txtCustomer.Text.Trim(),Authz.CurrentUserInfo.Warehouse.ToString()) == true)
            {
                string temp = "";
                temp = "paramfield1=datefrom";
                temp += "&paramvalue1=" + dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString();
                temp += "&paramfield2=dateto";
                temp += "&paramvalue2=" + dpTo.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString();
                temp += "&paramfield3=invcodefrom";
                temp += "&paramvalue3=" + txtInvcodeFrom.Text.Trim();
                temp += "&paramfield4=invcodeto";
                temp += "&paramvalue4=" + txtInvcodeTo.Text.Trim();
                temp += "&paramfield5=customer";
                temp += "&paramvalue5=" + txtCustomer.Text.Trim();
                temp += "&paramfield6=warehouse";
                temp += "&paramvalue6=" + Authz.CurrentUserInfo.Warehouse.ToString();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("SaleSummaryBillReport", temp), true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("SaleSummaryBillReport", temp), true);

            }
            else
            {
                Appz.ClientAlert(Page, "ไม่พบข้อมูล");
                return;
            }
        }

    }
}
