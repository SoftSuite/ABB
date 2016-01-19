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

public partial class Reports_StockRemainParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RdPrice.Checked = true;
            ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "", "ACTIVE = '1'", "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "", "ACTIVE = '1' AND PRODUCTTYPE = " + cmbProductType.SelectedValue + "", "ทั้งหมด", "0");
            ComboSource.BuildCombo(cmbProduct, "PRODUCTMASTER", "NAME", "LOID", "", "", "ทั้งหมด", "0");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ReportsFlow.CheckStockRemainReportReport(cmbProductType.SelectedValue, cmbProductGroup.SelectedValue, cmbProduct.SelectedValue, Authz.CurrentUserInfo.Warehouse.ToString()) == true)
        {
            string temp = "";
            temp = "paramfield1=producttype";
            temp += "&paramvalue1=" + cmbProductType.SelectedItem.Value;
            temp += "&paramfield2=productgroup";
            temp += "&paramvalue2=" + cmbProductGroup.SelectedItem.Value;
            temp += "&paramfield3=product";
            temp += "&paramvalue3=" + cmbProduct.SelectedItem.Value;
            temp += "&paramfield4=warehouse";
            temp += "&paramvalue4=" + Authz.CurrentUserInfo.Warehouse.ToString();
            temp += "&paramfield5=showprice";
            if (RdPrice.Checked == true)
                temp += "&paramvalue5=P";

            if (RdCost.Checked == true)
                temp += "&paramvalue5=C";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockRemainReport", temp), true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockRemainReport", temp), true);
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่พบข้อมูล");
            return;
        }
    }
    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "", "ACTIVE = '1' AND PRODUCTTYPE = " + cmbProductType.SelectedValue + "", "ทั้งหมด", "0");
    }
    protected void cmbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbProduct, "PRODUCTMASTER", "NAME", "LOID", "", "PRODUCTGROUP = " + cmbProductGroup.SelectedValue + "", "ทั้งหมด", "0");
    }
}
