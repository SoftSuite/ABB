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

public partial class Reports_PurchaseOrderDetailParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", "", "ทั้งหมด", "0");
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

        else if (txtCodeFrom.Text.Trim() == "" && txtCodeTo.Text.Trim() != "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุช่วงเลขที่ใบสั่งซื้อให้ครบถ้วน");
            return;
        }

        else if (txtCodeFrom.Text.Trim() != "" && txtCodeTo.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุช่วงเลขที่ใบสั่งซื้อให้ครบถ้วน");
            return;
        }

        else
        {
            if (ReportsFlow.CheckPurchaseOrderDetailReport(dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), dpTo.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), txtCodeFrom.Text.Trim(), txtCodeTo.Text.Trim(), cmbSupplier.SelectedValue) == true)
            {
                string temp = "";
                temp = "paramfield1=datefrom";
                temp += "&paramvalue1=" + dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString();
                temp += "&paramfield2=dateto";
                temp += "&paramvalue2=" + dpTo.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString();
                temp += "&paramfield3=codefrom";
                temp += "&paramvalue3=" + txtCodeFrom.Text.Trim();
                temp += "&paramfield4=codeto";
                temp += "&paramvalue4=" + txtCodeTo.Text.Trim();
                temp += "&paramfield5=supplier";
                temp += "&paramvalue5=" + cmbSupplier.SelectedValue;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("PurchaseOrderDetaillReport", temp), true);
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
