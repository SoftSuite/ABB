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
using ABB.Flow.Inventory;
using ABB.Data.Inventory;
using ABB.Global;
using ABB.Flow.Reports;
using ABB.Data;

public partial class Transaction_StockMovementSearch : System.Web.UI.Page
{
    private StockMovementSearchFlow _flow;
    public StockMovementSearchFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockMovementSearchFlow(); return _flow; }
    }

    private StockMovementSearchData GetData()
    {
        StockMovementSearchData data = new StockMovementSearchData();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
        data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value);
        data.PRODUCTNAME = this.txtProductName.Text.Trim();
        data.ZONE = Convert.ToDouble(this.cmbWarehouse.SelectedItem.Value);
        data.ZONEFROM = Convert.ToDouble(this.cmbWarehouseFrom.SelectedItem.Value);
        data.ZONETO = Convert.ToDouble(this.cmbWarehouseTo.SelectedItem.Value);
        return data;
    }

    private void Search()
    {
        this.grvStockMovementItem.DataSource = FlowObj.GetStockMovementItemList(GetData());
        this.grvStockMovementItem.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbWarehouseFrom, "ZONE", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbWarehouseTo, "ZONE", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");  
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void PrintClick(object sender, EventArgs e)
    {
        if (ctlDateFrom.DateValue.Year == 1 && ctlDateTo.DateValue.Year != 1)
        {
            Appz.ClientAlert(Page, "กรุณากรอกวันที่ให้ครบถ้วน");
            return;
        }

        else if (ctlDateFrom.DateValue.Year != 1 && ctlDateTo.DateValue.Year == 1)
        {
            Appz.ClientAlert(Page, "กรุณากรอกวันที่ให้ครบถ้วน");
            return;
        }
        else
        {
            if (ReportsFlow.StockMovementReport(ctlDateFrom.DateValue.ToString("dd/MM/") + ctlDateFrom.DateValue.Year.ToString(), ctlDateTo.DateValue.ToString("dd/MM/") + ctlDateTo.DateValue.Year.ToString(), cmbProductType.SelectedValue.ToString(), cmbProductGroup.SelectedValue.ToString(), txtProductName.Text.Trim(), cmbWarehouse.SelectedValue.ToString(), cmbWarehouseFrom.SelectedValue.ToString(), cmbWarehouseTo.SelectedValue.ToString()) == true)
            {
                string temp = "";
                temp = "paramfield1=DATEFROM";
                temp += "&paramvalue1=" + ctlDateFrom.DateValue.ToString("dd/MM/") + ctlDateFrom.DateValue.Year;
                temp += "&paramfield2=DATETO";
                temp += "&paramvalue2=" + ctlDateTo.DateValue.ToString("dd/MM/") + ctlDateTo.DateValue.Year;
                temp += "&paramfield3=producttype";
                temp += "&paramvalue3=" + cmbProductType.SelectedValue.ToString();
                temp += "&paramfield4=productgroup";
                temp += "&paramvalue4=" + cmbProductGroup.SelectedValue.ToString();
                temp += "&paramfield5=product";
                temp += "&paramvalue5=" + txtProductName.Text.Trim();
                temp += "&paramfield6=warehouse";
                temp += "&paramvalue6=" + cmbWarehouse.SelectedValue.ToString();
                temp += "&paramfield7=FromZone";
                temp += "&paramvalue7=" + cmbWarehouseFrom.SelectedValue.ToString();
                temp += "&paramfield8=ToZone";
                temp += "&paramvalue8=" + cmbWarehouseTo.SelectedValue.ToString();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("StockMovementRepot", temp), true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("ProductSaleSummaryReport", temp), true);

            }
            else
            {
                Appz.ClientAlert(Page, "ไม่พบข้อมูล");
                return;
            }
        }
    }
}
