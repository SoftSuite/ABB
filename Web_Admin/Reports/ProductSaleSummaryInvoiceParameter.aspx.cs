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

public partial class Reports_ProductSaleSummaryParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE = 'FG' ", "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedValue, "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "PRODUCTGROUP =" + cmbProductGroup.SelectedValue, "ทั้งหมด", "0");
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
            if (ReportsFlow.CheckProductSaleSummaryInvoiceReport(dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), dpTo.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), cmbProductType.SelectedValue.ToString(), cmbProductGroup.SelectedValue.ToString(), cmbProduct.SelectedValue.ToString()) == true)
            {
                string temp = "";
                temp = "paramfield1=DATEFROM";
                temp += "&paramvalue1=" + dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year;
                temp += "&paramfield2=DATETO";
                temp += "&paramvalue2=" + dpTo.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year;
                temp += "&paramfield3=producttype";
                temp += "&paramvalue3=" + cmbProductType.SelectedValue.ToString();
                temp += "&paramfield4=productgroup";
                temp += "&paramvalue4=" + cmbProductGroup.SelectedValue.ToString();
                temp += "&paramfield5=product";
                temp += "&paramvalue5=" + cmbProduct.SelectedValue.ToString();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("ProductSaleSummaryInvoiceReport", temp), true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("ProductSaleSummaryReport", temp), true);

            }
            else
            {
                Appz.ClientAlert(Page, "ไม่พบข้อมูล");
                return;
            }
        }
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedValue, "ทั้งหมด", "0");
    }

    protected void cmbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "PRODUCTGROUP = " + cmbProductGroup.SelectedValue, "ทั้งหมด", "0");
    }
}
