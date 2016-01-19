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

public partial class Reports_ProductStockinParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(cmbDocType, "DOCTYPE", "DOCNAME", "LOID", "DOCNAME", "TYPE='I' AND WAREHOUSE='" + Authz.CurrentUserInfo.Warehouse + "'", "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE='" + (Authz.CurrentUserInfo.Warehouse== 1 ? "FG" : "WH") + "'  ", "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedValue, "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "PRODUCTGROUP =" + cmbProductGroup.SelectedValue, "ทั้งหมด", "0");
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
            if (ReportsFlow.CheckProductStockinReport(dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString(), dpTo.DateValue.ToString("dd/MM/") + dpTo.DateValue.Year.ToString(), cmbProductType.SelectedValue.ToString(), cmbProductGroup.SelectedValue.ToString(), cmbProduct.SelectedValue.ToString(), cmbDocType.SelectedValue,Authz.CurrentUserInfo.Warehouse.ToString()) == true)
            {
                string temp = "";
                temp = "paramfield1=producttype";
                temp += "&paramvalue1=" + cmbProductType.SelectedValue.ToString();
                temp += "&paramfield2=productgroup";
                temp += "&paramvalue2=" + cmbProductGroup.SelectedValue.ToString();
                temp += "&paramfield3=product";
                temp += "&paramvalue3=" + cmbProduct.SelectedValue.ToString();
                temp += "&paramfield4=doctype";
                temp += "&paramvalue4=" + cmbDocType.SelectedValue;
                temp += "&paramfield5=datefrom";
                temp += "&paramvalue5=" + dpFrom.DateValue.ToString("dd/MM/") + dpFrom.DateValue.Year.ToString();
                temp += "&paramfield6=dateto";
                temp += "&paramvalue6=" + dpTo.DateValue.ToString("dd/MM/") + dpTo.DateValue.Year.ToString();
                temp += "&paramfield7=warehouse";
                temp += "&paramvalue7=" + Authz.CurrentUserInfo.Warehouse.ToString();


                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript(Request["reportname"], temp), true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("ProductStockinReport", temp), true);

            }
            else
            {
                Appz.ClientAlert(Page, "ไม่พบข้อมูล");
                return;
            }
        }
    }
}
