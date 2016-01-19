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
using System.Collections.Generic;

public partial class Reports_StockOutYearSummaryParameter : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE = 'FG' ", "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedValue, "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "PRODUCTGROUP =" + cmbProductGroup.SelectedValue, "ทั้งหมด", "0");
            RdPrice.Checked = true;
            for (int i = 0; i <= 10; i++)
            {
                string yy = "";
                yy = Convert.ToString(DateTime.Now.Year - i);
                cmbYear.Items.Add(yy);
            }
        }
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedValue, "ทั้งหมด", "0");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (cmbYear.SelectedValue == "" || cmbYear.SelectedValue == null)
        {
            Appz.ClientAlert(Page, "กรุณาระบุปี");
            return;
        }

        if (ReportsFlow.CheckStockOutYearSummaryReport(cmbYear.SelectedItem.ToString(), cmbProduct.SelectedValue, cmbProductType.SelectedValue, cmbProductGroup.SelectedValue, Authz.CurrentUserInfo.Warehouse.ToString()) == true)
        {
            string temp = "";
            temp = "paramfield1=year";
            temp += "&paramvalue1=" + cmbYear.SelectedItem.ToString();
            temp += "&paramfield2=producttype";
            temp += "&paramvalue2=" + cmbProductType.SelectedValue.ToString();
            temp += "&paramfield3=productgroup";
            temp += "&paramvalue3=" + cmbProductGroup.SelectedValue.ToString();
            temp += "&paramfield4=product";
            temp += "&paramvalue4=" + cmbProduct.SelectedValue.ToString();
            temp += "&paramfield5=showprice";
            if (RdPrice.Checked == true )
                temp += "&paramvalue5=P";
            
            if (RdCost.Checked == true)
                temp += "&paramvalue5=C";

            temp += "&paramfield6=warehouse";
            temp += "&paramvalue6=" + Authz.CurrentUserInfo.Warehouse.ToString();

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript(Request["reportname"], temp), true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockOutYearSummaryReport", temp), true);
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่พบข้อมูล");
            return;
        }
    }
    protected void cmbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "PRODUCTGROUP = " + cmbProductGroup.SelectedValue, "ทั้งหมด", "0");
    }
}
