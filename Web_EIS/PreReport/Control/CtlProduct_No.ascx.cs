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
using ABB.Flow.EIS;
using ChartDirector;

public partial class PreReport_Control_CtlProduct_No : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ControlUtil.SetYearTextbox(txtYearFrom);
        ControlUtil.SetYearTextbox(txtYearTo);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetHead();
            LoadCombo();
            if (Request["yearfrom"] != null) this.txtYearFrom.Text = Request["yearfrom"];
            if (Request["yearto"] != null) this.txtYearTo.Text = Request["yearto"];
            if (Request["currentyear"] != null)
            {
                this.chkOption.Checked = false;
                this.pnlConstraints.Visible = this.chkOption.Checked; this.cmbWarehouse.SelectedIndex = this.cmbWarehouse.Items.IndexOf(this.cmbWarehouse.Items.FindByValue(Request["warehouse"]));
              //  this.cmbCustomer.SelectedIndex = this.cmbCustomer.Items.IndexOf(this.cmbCustomer.Items.FindByValue(Request["cmbCustomer"]));
                this.cmbProductType.SelectedIndex = this.cmbProductType.Items.IndexOf(this.cmbProductType.Items.FindByValue(Request["producttype"]));
                ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedItem.Value, "ทั้งหมด", "0");
                this.cmbProductGroup.SelectedIndex = this.cmbProductGroup.Items.IndexOf(this.cmbProductGroup.Items.FindByValue(Request["productgroup"]));
                BindCheckboxList();
                if (Session["group"] != null)
                {
                    ArrayList arr = (ArrayList)Session["group"];
                    for (int i = 0; i < this.chklist.Items.Count; ++i)
                    {
                        for (int k = 0; k < arr.Count; ++k)
                        {
                            if (arr[k].ToString() == chklist.Items[i].Value)
                            {
                                chklist.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }
                SetMonthlyProductGraph();
            }
            else
            {
                Session["group"] = null;
            }
        }
    }

    #region "Common"

    private void SetHead()
    {
        lblHead.Text = "รายงานยอดขาย";
        lblSubHead.Text = "รายงานยอดขาย เปรียบเทียบสินค้า";
        //switch (Request.QueryString["type"])
        //{
        //    case "sale":
        //        lblHead.Text = "รายงานจำนวนสินค้าที่ขายได้";
        //        lblSubHead.Text = "รายงานจำนวนสินค้าที่ขายได้ เปรียบเทียบสินค้า";
        //        break;

        //    case "stockin":
        //        lblHead.Text = "รายงานจำนวนสินค้าที่รับเข้า";
        //        lblSubHead.Text = "รายงานจำนวนสินค้าที่รับเข้า เปรียบเทียบสินค้า";
        //        break;

        //    case "stockout":
        //        lblHead.Text = "รายงานจำนวนสินค้าที่จ่ายออก";
        //        lblSubHead.Text = "รายงานจำนวนสินค้าที่จ่ายออก เปรียบเทียบสินค้า";
        //        break;

        //    case "support":
        //        lblHead.Text = "รายงานจำนวนสินค้าที่สนับสนุน";
        //        lblSubHead.Text = "รายงานจำนวนสินค้าที่สนับสนุน เปรียบเทียบสินค้า";
        //        break;

        //    case "return":
        //        lblHead.Text = "รายงานจำนวนสินค้าที่รับคืน";
        //        lblSubHead.Text = "รายงานจำนวนสินค้าที่รับคืน เปรียบเทียบสินค้า";
        //        break;

        //    case "sendback":
        //        lblHead.Text = "รายงานจำนวนสินค้าที่ส่งคืน";
        //        lblSubHead.Text = "รายงานจำนวนสินค้าที่ส่งคืน เปรียบเทียบสินค้า";
        //        break;
        //}
    }

    private string GetReportPath()
    {
        string path = "";
        path = ABB.Data.Constz.HomeFolder + "PreReport/SalePrice_Month_Product.aspx";
        //switch (Request.QueryString["type"])
        //{
        //    case "sale":
        //        path = ABB.Data.Constz.HomeFolder + "PreReport/Sale_Month_Product.aspx";
        //        break;

        //    case "stockin":
        //        path = ABB.Data.Constz.HomeFolder + "PreReport/StockIn_Month_Product.aspx";
        //        break;

        //    case "stockout":
        //        path = ABB.Data.Constz.HomeFolder + "PreReport/StockOut_Month_Product.aspx";
        //        break;

        //    case "support":
        //        path = ABB.Data.Constz.HomeFolder + "PreReport/Support_Month_Product.aspx";
        //        break;

        //    case "return":
        //        path = ABB.Data.Constz.HomeFolder + "PreReport/Return_Month_Product.aspx";
        //        break;

        //    case "sendback":
        //        path = ABB.Data.Constz.HomeFolder + "PreReport/Sendback_Month_Product.aspx";
        //        break;
        //}
        return path;
    }

    private void LoadCombo()
    {
        ComboSource.BuildCombo(cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "ACTIVE = '1'", "ทั้งหมด", "0");
        ComboSource.BuildCombo(cmbCustomer, "CUSTOMER", "NAME", "LOID", "NAME", "ACTIVE = '1'", "ทั้งหมด", "0");
        ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE = '" + rbtType.SelectedValue + "'", "ทั้งหมด", "0");
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE IN (SELECT LOID FROM PRODUCTTYPE WHERE TYPE = '" + rbtType.SelectedValue + "')", "ทั้งหมด", "0");
        BindCheckboxList();
        string scripts = "";
        for (int i = 0; i < this.chklist.Items.Count; ++i)
        {
            scripts += (scripts == "" ? "" : " && ") + "!document.getElementById('" + this.chklist.ClientID + "_" + i.ToString() + "').checked";
        }
        scripts = "if (" + scripts + ") {alert('กรุณาเลือกสินค้าหรือวัตถุดิบ'); return false; }";
        this.btnReport.OnClientClick = (scripts == "" ? "" : scripts + " else ") + "if (document.getElementById('" + this.txtYearFrom.ClientID + "').value == '' || document.getElementById('" + this.txtYearTo.ClientID + "').value == '') {alert('กรุณาระบุช่วงเวลาให้ครบถ้วน'); return false;} else if (document.getElementById('" + this.txtYearFrom.ClientID + "').value.length <4 || document.getElementById('" + this.txtYearTo.ClientID + "').value.length<4) {alert('กรุณาระบุเลขปีให้ถูกต้อง'); return false;}";
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbProductType.SelectedItem.Value == "0")
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE IN (SELECT LOID FROM PRODUCTTYPE WHERE TYPE = '" + rbtType.SelectedValue + "')", "ทั้งหมด", "0");
        else
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedItem.Value, "ทั้งหมด", "0");
        
        BindCheckboxList(); 
    }

    protected void cmbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCheckboxList();
    }

    private void BindCheckboxList()
    {
        string where = "ACTIVE = '" + ABB.Data.Constz.ActiveStatus.Active + "' AND PRODUCTGROUP IN (SELECT PG.LOID FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID WHERE PT.TYPE = '" + rbtType.SelectedValue + "')";
        if (this.cmbProductGroup.SelectedItem.Value.ToString() != "0")
            where += (where == "" ? "" : " AND ") + "PRODUCTGROUP = " + this.cmbProductGroup.SelectedItem.Value.ToString() + " ";
        if (this.cmbProductType.SelectedItem.Value.ToString() != "0")
            where += (where == "" ? "" : " AND ") + "PRODUCTGROUP IN (SELECT LOID FROM PRODUCTGROUP WHERE PRODUCTTYPE = " + this.cmbProductType.SelectedItem.Value.ToString() + ") ";

      DataTable dt = EISReportSaleFlow.GetProductList("PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", where);
      chklist.DataSource = dt;
      chklist.DataTextField = "PRODUCTNAME";
      chklist.DataValueField = "PRODUCTMASTER";
      chklist.DataBind();
    }

    private string GetReportTitle()
    {
        return "รายงานยอดขายเปรียบเทียบสินค้า";
        //switch (Request.QueryString["type"])
        //{
        //    case "sale":
        //        return "รายงานจำนวนสินค้าที่ขายได้ เปรียบเทียบสินค้า";

        //    case "stockin":
        //        return "รายงานจำนวนสินค้าที่รับเข้า  เปรียบเทียบสินค้า";

        //    case "stockout":
        //        return "รายงานจำนวนสินค้าที่จ่ายออก เปรียบเทียบสินค้า";

        //    case "support":
        //        return "รายงานจำนวนสินค้าที่สนับสนุน เปรียบเทียบสินค้า";

        //    case "return":
        //        return "รายงานจำนวนสินค้าที่รับคืน เปรียบเทียบสินค้า";

        //    case "sendback":
        //        return "รายงานจำนวนสินค้าที่ส่งคืน เปรียบเทียบสินค้า";

        //    default:
        //        return "";
        //}
    }

    private DataTable GetTable(int yearFrom, int yearTo)
    {
        DataTable dTable = new DataTable();
        dTable = EISReportPriceSaleFlow.GetProductPublishedGroupByYear(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbCustomer.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, GetSelectedProduct());

        //switch (Request.QueryString["type"])
        //{
        //    case "sale":
        //        dTable = EISReportSaleFlow.GetProductPublishedGroupByYear(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "stockin":
        //        dTable = EISReportStockinFlow.GetProductPublishedGroupByYear(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "stockout":
        //        dTable = EISReportStockoutFlow.GetProductPublishedGroupByYear(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "support":
        //        dTable = EISReportSupportFlow.GetProductPublishedGroupByYear(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "return":
        //        dTable = EISReportReturnFlow.GetProductPublishedGroupByYear(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "sendback":
        //        dTable = EISReportSendbackFlow.GetProductPublishedGroupByYear(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, GetSelectedProduct());
        //        break;
        //}

        return dTable;
    }

    private DataTable GetTableMonth(int year)
    {
        DataTable dTable = new DataTable();
        dTable = EISReportPriceSaleFlow.GetProductPublishedGroupByMonth(year, cmbWarehouse.SelectedItem.Value, cmbCustomer.SelectedItem.Value, GetSelectedProduct());
        //switch (Request.QueryString["type"])
        //{
        //    case "sale":
        //        dTable = EISReportSaleFlow.GetProductPublishedGroupByMonth(year, cmbWarehouse.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "stockin":
        //        dTable = EISReportStockinFlow.GetProductPublishedGroupByMonth(year, cmbWarehouse.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "stockout":
        //        dTable = EISReportStockoutFlow.GetProductPublishedGroupByMonth(year, cmbWarehouse.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "support":
        //        dTable = EISReportSupportFlow.GetProductPublishedGroupByMonth(year, cmbWarehouse.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "return":
        //        dTable = EISReportReturnFlow.GetProductPublishedGroupByMonth(year, cmbWarehouse.SelectedItem.Value, GetSelectedProduct());
        //        break;

        //    case "sendback":
        //        dTable = EISReportSendbackFlow.GetProductPublishedGroupByMonth(year, cmbWarehouse.SelectedItem.Value, GetSelectedProduct());
        //        break;
        //}

        return dTable;
    }

    private ArrayList GetSelectedProduct()
    {
        ArrayList arr = new ArrayList();

        for (int i = 0; i < chklist.Items.Count; i++)
        {
            if (chklist.Items[i].Selected == true)
                arr.Add(chklist.Items[i].Value);
        }
        Session["group"] = arr;
        return arr;
    }

    #endregion

    #region Month

    private string[] GetXLabel()
    {
        string[] labels = { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
        return labels;
    }

    private void AddData(LineLayer layer, int year)
    {
        DataTable dTable = GetTableMonth(year);
        for (int i = 0; i < this.chklist.Items.Count; ++i)
        {
            if (this.chklist.Items[i].Selected)
            {
                string product = this.chklist.Items[i].Value;
                foreach (DataRow dRow in dTable.Rows)
                {
                    if (Convert.ToDouble(dRow["LOID"]).ToString() == product)
                    {
                        double[] data = new double[dRow.Table.Columns.Count - 2];
                        for (int k = 0; k < dRow.Table.Columns.Count - 2; ++k)
                        {
                            data[k] = Convert.ToDouble(dRow[k + 2]);
                        }
                        layer.addDataSet(data, -1, this.chklist.Items[i].Text).setDataSymbol(Chart.DiamondShape, 9);
                    }
                }
            }
        }
    }
    
    private void SetMonthlyProductGraph()
    {
        int yearFrom = Convert.ToInt32(Request["yearfrom"]);
        int yearTo = Convert.ToInt32(Request["yearto"]);
        int currentYear = Convert.ToInt32(Request["currentyear"]);
        //int yearFrom = 2551;
        //int yearTo = 2551;
        //int currentYear = 2551;
        this.lnkYear.Text = GetReportTitle() + (yearFrom == yearTo ? "ในปี พ.ศ. " + yearFrom.ToString() : " ตั้งแต่ปี พ.ศ. " + yearFrom.ToString() + "-" + yearTo.ToString());
        this.lblMonth.Text = ">> ปี พ.ศ. " + currentYear.ToString();
        this.pnlMonth.Visible = true;
        string[] labels = { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
        XYChart c = new XYChart(750, 380, 15663086, 14540253, 0);
        string title = GetReportTitle() + "ในปี พ.ศ. " + currentYear.ToString();

        c.addTitle(title, "Tahoma Bold", 12);
        c.setPlotArea(70, 80, 640, 230, c.gradientColor(0, 60, 0, 350, 16777215, 11189196), -1, Chart.Transparent, 1111);
        c.addLegend(30, 25, false, "Tahoma Bold", 8).setBackground(Chart.Transparent); 
        c.xAxis().setLabels(labels);
        c.yAxis().setTickDensity(30);
        c.xAxis().setLabelStyle("Tahoma", 8, 001122, 90);
        c.yAxis().setLabelStyle("Tahoma", 8);
        c.yAxis().setLabelFormat("{value|0,}");
        c.xAxis().setWidth(2);
        c.yAxis().setWidth(2);
        c.yAxis().setTitle("ราคา", "Tahoma Bold", 10);
        c.xAxis().setTitle("เดือน", "Tahoma Bold", 10);

        LineLayer layer = c.addLineLayer();
        layer.setLineWidth(1);
        AddData(layer, currentYear - 543);
        vwChart.Image = c.makeWebImage(Chart.PNG);
        vwChart.ImageMap = c.getHTMLImageMap("", "", "title='{dataSetName} ในเดือน {xLabel}\r\n{value|,} รายการ ({percent}%)'");
        this.pnlChart.Visible = true;
    }

    #endregion

    #region Year

    private string[] GetXLabel(int yearFrom, int yearTo)
    {
        int diff = yearTo - yearFrom;
        string[] xLabel = new string[diff+1];

        for (int i = 0; i < diff + 1; i++)
        {
            xLabel[i] = (i + yearFrom).ToString();
        }
        return xLabel;
    }

    private void AddData(LineLayer layer, int yearFrom, int yearTo)
    {
        DataTable dTable = GetTable(yearFrom, yearTo);
        for (int i = 0; i < this.chklist.Items.Count; ++i)
        {
            if (this.chklist.Items[i].Selected)
            {
                string product = this.chklist.Items[i].Value;
                foreach(DataRow dRow in dTable.Rows)
                {
                    if (Convert.ToDouble(dRow["LOID"]).ToString() == product)
                    {
                        double[] data = new double[dRow.Table.Columns.Count - 2];
                        for (int k = 0; k < dRow.Table.Columns.Count - 2; ++k)
                        {
                            data[k] = Convert.ToDouble(dRow[k + 2]);
                        }
                        layer.addDataSet(data, -1, this.chklist.Items[i].Text).setDataSymbol(Chart.DiamondShape, 9);
                    }
                }
            }
        }
    }
    
    private void SetYearlyProductGraph()
    {
        this.pnlMonth.Visible = false;
        int yearFrom = Convert.ToInt32(this.txtYearFrom.Text == "" ? "0" : this.txtYearFrom.Text)-543;
        int yearTo = Convert.ToInt32(this.txtYearTo.Text == "" ? "0" : this.txtYearTo.Text)-543;
        if (yearFrom <0) yearFrom =0;
        if (yearTo < 0) yearTo = 0;
        int temp = yearFrom;
        if (yearFrom > yearTo)
        {
            yearFrom = yearTo;
            yearTo = temp;
        }
        yearFrom += 543;
        yearTo += 543;
        string[] labels = GetXLabel(yearFrom, yearTo);
        XYChart c = new XYChart(750, 380, 15663086, 14540253, 0);
        string title = GetReportTitle() + (yearFrom == yearTo ? "ในปี พ.ศ. " + yearFrom.ToString() : " ตั้งแต่ปี พ.ศ. " + yearFrom.ToString() + "-" + yearTo.ToString());

        c.addTitle(title, "Tahoma Bold",12);
        c.setPlotArea(70, 80, 640, 230, c.gradientColor(0, 60, 0, 350, 16777215, 11189196), -1, Chart.Transparent, 1111);
        c.addLegend(30, 25, false, "Tahoma Bold", 8).setBackground(Chart.Transparent);c.xAxis().setLabels(labels);
        c.yAxis().setTickDensity(30);
        c.xAxis().setLabelStyle("Tahoma", 8, 001122, 90);
        c.yAxis().setLabelStyle("Tahoma", 8);
        c.yAxis().setLabelFormat("{value|0,}");
        c.xAxis().setWidth(2);
        c.yAxis().setWidth(2);
        c.yAxis().setTitle("ราคา", "Tahoma Bold", 10);
        c.xAxis().setTitle("ปี พ.ศ.", "Tahoma Bold", 10);

        LineLayer layer = c.addLineLayer();
        layer.setLineWidth(1);
        AddData(layer, yearFrom - 543, yearTo - 543);
        vwChart.Image = c.makeWebImage(Chart.PNG);
        vwChart.ImageMap = c.getHTMLImageMap(GetReportPath() + "?type=" + Request["type"] + "&warehouse=" + this.cmbWarehouse.SelectedItem.Value.ToString() + "&customer=" + this.cmbCustomer.SelectedItem.Value.ToString() + "&producttype=" + this.cmbProductType.SelectedItem.Value.ToString() + "&productgroup=" + this.cmbProductGroup.SelectedItem.Value.ToString() + "&currentyear={xLabel}&yearfrom=" + yearFrom.ToString() + "&yearto=" + yearTo.ToString(), "",
            "title='{dataSetName} ในปี {xLabel}\r\n{value|,} รายการ ({percent}%)'");
    }

    #endregion

    protected void chkOption_CheckedChanged(object sender, EventArgs e)
    {
        this.pnlConstraints.Visible = this.chkOption.Checked;
        this.pnlChart.Visible = false;
    }

    protected void lnkYear_Click(object sender, EventArgs e)
    {
        this.txtYearFrom.Text = Request["yearfrom"];
        this.txtYearTo.Text = Request["yearto"];
        SetYearlyProductGraph();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        this.chkOption.Checked = false;
        this.pnlConstraints.Visible = false;
        SetYearlyProductGraph();
        this.pnlChart.Visible = true;
    }
    protected void rbtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE = '" + rbtType.SelectedValue + "'", "ทั้งหมด", "0");
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE IN (SELECT LOID FROM PRODUCTTYPE WHERE TYPE = '" + rbtType.SelectedValue + "')", "ทั้งหมด", "0");
        BindCheckboxList(); 
    }
}
