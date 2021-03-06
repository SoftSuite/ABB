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

public partial class PreReport_Control_CtlMonth : System.Web.UI.UserControl
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
            pnlChart.Visible = false;
            if (Request["yearfrom"] != null) this.txtYearFrom.Text = Request["yearfrom"];
            if (Request["yearto"] != null) this.txtYearTo.Text = Request["yearto"];
            if (Request["currentyear"] != null)
            {
                this.chkOption.Checked = false;
                this.pnlConstraints.Visible = this.chkOption.Checked; this.cmbWarehouse.SelectedIndex = this.cmbWarehouse.Items.IndexOf(this.cmbWarehouse.Items.FindByValue(Request["warehouse"]));
                ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE = '" + rbtType.SelectedValue + "'", "���͡", "0");
                this.cmbProductType.SelectedIndex = this.cmbProductType.Items.IndexOf(this.cmbProductType.Items.FindByValue(Request["producttype"])); 
                ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedItem.Value, "������", "0");
                this.cmbProductGroup.SelectedIndex = this.cmbProductGroup.Items.IndexOf(this.cmbProductGroup.Items.FindByValue(Request["productgroup"]));
                ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT LOID FROM PRODUCTGROUP WHERE PRODUCTTYPE = " + cmbProductType.SelectedItem.Value + ")", "���͡", "0");
                this.cmbProduct.SelectedIndex = this.cmbProduct.Items.IndexOf(this.cmbProduct.Items.FindByValue(Request["product"]));

                SetMonthlyGraph();
            }
        }
    }
    protected void chkOption_CheckedChanged(object sender, EventArgs e)
    {
        this.pnlConstraints.Visible = this.chkOption.Checked;
        this.pnlChart.Visible = false;
    }
    private void SetHead()
    { 
        switch (Request.QueryString["type"])
        {
            case "sale":
                lblHead.Text = "��§ҹ�ӹǹ�Թ��ҷ������";
                lblSubHead.Text = "��§ҹ�ӹǹ�Թ��ҷ������ ���º��º��";
                break;

            case "stockin":
                lblHead.Text = "��§ҹ�ӹǹ�Թ��ҷ���Ѻ���";
                lblSubHead.Text = "��§ҹ�ӹǹ�Թ��ҷ���Ѻ��� ���º��º��";
                break;

            case "stockout":
                lblHead.Text = "��§ҹ�ӹǹ�Թ��ҷ������͡";
                lblSubHead.Text = "��§ҹ�ӹǹ�Թ��ҷ������͡ ���º��º��";
                break;

            case "support":
                lblHead.Text = "��§ҹ�ӹǹ�Թ��ҷ��ʹѺʹع";
                lblSubHead.Text = "��§ҹ�ӹǹ�Թ��ҷ��ʹѺʹع ���º��º��";
                break;

            case "return":
                lblHead.Text = "��§ҹ�ӹǹ�Թ��ҷ���Ѻ�׹";
                lblSubHead.Text = "��§ҹ�ӹǹ�Թ��ҷ���Ѻ�׹ ���º��º��";
                break;

            case "sendback":
                lblHead.Text = "��§ҹ�ӹǹ�Թ��ҷ���觤׹";
                lblSubHead.Text = "��§ҹ�ӹǹ�Թ��ҷ���觤׹ ���º��º��";
                break;
        }
    }

    private void LoadCombo()
    {
        ComboSource.BuildCombo(cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "ACTIVE = '1'", "���͡", "0");
        ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE = '" + rbtType.SelectedValue + "'", "���͡", "0");
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE IN (SELECT LOID FROM PRODUCTTYPE WHERE TYPE = '" + rbtType.SelectedValue + "')", "���͡", "0");
        ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT PG.LOID FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID WHERE PT.TYPE = '" + rbtType.SelectedValue + "')", "���͡", "0");

    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbProductType.SelectedItem.Value == "0")
        {
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE IN (SELECT LOID FROM PRODUCTTYPE WHERE TYPE = '" + rbtType.SelectedValue + "')", "���͡", "0");
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT PG.LOID FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID WHERE PT.TYPE = '" + rbtType.SelectedValue + "')", "���͡", "0");
        }
        else
        {
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedItem.Value, "���͡", "0");
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT LOID FROM PRODUCTGROUP WHERE PRODUCTTYPE = " + cmbProductType.SelectedItem.Value + ")", "���͡", "0");
        }
    }

    protected void cmbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbProductGroup.SelectedItem.Value == "0")
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT LOID FROM PRODUCTGROUP WHERE PRODUCTTYPE = " + cmbProductType.SelectedItem.Value + ")", "���͡", "0");
        else
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP = " + cmbProductGroup.SelectedItem.Value, "���͡", "0");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (txtYearFrom.Text.Trim() == "" || txtYearTo.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "��س��кػ�㹡�ä������ú��ǹ");
            return;
        }

        if (cmbWarehouse.SelectedItem.Value == "0")
        {
            Appz.ClientAlert(Page, "��س����͡��ѧ�Թ���");
            return;
        }

        if (cmbProductType.SelectedItem.Value == "0")
        {
            Appz.ClientAlert(Page, "��س����͡�������Թ���/�ѵ�شԺ");
            return;
        }

        if (cmbProductGroup.SelectedItem.Value == "0")
        {
            Appz.ClientAlert(Page, "��س����͡������Թ���/�ѵ�شԺ");
            return;
        }

        if (cmbProduct.SelectedItem.Value == "0")
        {
            Appz.ClientAlert(Page, "��س����͡�����Թ���/�ѵ�شԺ");
            return;
        }

        SetYearlyGraph();
        pnlChart.Visible = true;
    }



    private string GetTitle()
    {
        switch (Request.QueryString["type"])
        {
            case "sale":
                return "��§ҹ�ӹǹ�Թ��ҷ������ ���º��º��";

            case "stockin":
                return "��§ҹ�ӹǹ�Թ��ҷ���Ѻ���  ���º��º��";

            case "stockout":
                return "��§ҹ�ӹǹ�Թ��ҷ������͡ ���º��º��";

            case "support":
                return "��§ҹ�ӹǹ�Թ��ҷ��ʹѺʹع ���º��º��";

            case "return":
                return "��§ҹ�ӹǹ�Թ��ҷ���Ѻ�׹ ���º��º��";

            case "sendback":
                return "��§ҹ�ӹǹ�Թ��ҷ���觤׹ ���º��º��";

            default:
                return "";
        }
    }
    private string GetReportPath()
    {
        string path = "";
        switch (Request.QueryString["type"])
        {
            case "sale":
                path = ABB.Data.Constz.HomeFolder + "PreReport/Sale_Day_Yearly.aspx";
                break;

            case "stockin":
                path = ABB.Data.Constz.HomeFolder + "PreReport/StockIn_Day_Yearly.aspx";
                break;

            case "stockout":
                path = ABB.Data.Constz.HomeFolder + "PreReport/StockOut_Day_Yearly.aspx";
                break;

            case "support":
                path = ABB.Data.Constz.HomeFolder + "PreReport/Support_Day_Yearly.aspx";
                break;

            case "return":
                path = ABB.Data.Constz.HomeFolder + "PreReport/Return_Day_Yearly.aspx";
                break;

            case "sendback":
                path = ABB.Data.Constz.HomeFolder + "PreReport/Sendback_Day_Yearly.aspx";
                break;
        }
        return path;
    }
    private DataTable GetTableMonth(int yearFrom, int yearTo)
    {
        DataTable dTable = new DataTable();

        switch (Request.QueryString["type"])
        {
            case "sale":
                dTable = EISReportSaleMonthFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "stockin":
                dTable = EISReportStockinMonthFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "stockout":
                dTable = EISReportStockoutMonthFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "support":
                dTable = EISReportSupportMonthFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "return":
                dTable = EISReportReturnMonthFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "sendback":
                dTable = EISReportSendbackMonthFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;
        }

        return dTable;
    }

    private DataTable GetTableDay(int yearFrom, int yearTo,int month)
    {
        DataTable dTable = new DataTable();

        switch (Request.QueryString["type"])
        {
            case "sale":
                dTable = EISReportSaleMonthFlow.GetMonthPublished(yearFrom, yearTo, month, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "stockin":
                dTable = EISReportStockinMonthFlow.GetMonthPublished(yearFrom, yearTo, month, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "stockout":
                dTable = EISReportStockoutMonthFlow.GetMonthPublished(yearFrom, yearTo, month, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "support":
                dTable = EISReportSupportMonthFlow.GetMonthPublished(yearFrom, yearTo, month, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "return":
                dTable = EISReportReturnMonthFlow.GetMonthPublished(yearFrom, yearTo, month, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "sendback":
                dTable = EISReportSendbackMonthFlow.GetMonthPublished(yearFrom, yearTo, month, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;
        }

        return dTable;
    }
    protected void lnkYear_Click(object sender, EventArgs e)
    {
        this.txtYearFrom.Text = Request["yearfrom"];
        this.txtYearTo.Text = Request["yearto"];
        SetYearlyGraph();
        lnkYear.Visible = false;
        lblMonth.Visible = false;
    }

    #region Month
    private void AddData(LineLayer layer, int yearFrom, int yearTo)
    {
        DataTable dTable = GetTableMonth(yearFrom, yearTo);

        if (dTable.Rows.Count > 0)
        {
            ArrayList arrData = new ArrayList();
            ArrayList arrLabel = new ArrayList();
            string[] labels = { "�.�.", "�.�.", "��.�.", "��.�.", "�.�.", "��.�.", "�.�.", "�.�.", "�.�.", "�.�.", "�.�.", "�.�." };

            for (int i = yearFrom ; i <= yearTo ; i++ )
            {
                double[] d = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                DataView dv = new DataView(dTable);
                dv.RowFilter = "REQYEAR = " + i.ToString();
                for (int j = 0; j < dv.Count; j++)
                {
                    d[Convert.ToInt32(dv[j]["MON"]) - 1] = Convert.ToDouble(dv[j]["QTY"]);
                }
                arrData.Add(d);
                arrLabel.Add(Convert.ToInt32(i) + 543);
            }

            //The data for the line chart
            //double[] data0 = { 60.2, 51.7, 81.3, 48.6, 56.2, 68.9, 52.8 };
            //double[] data1 = { 30.0, 32.7, 33.9, 29.5, 32.2, 28.4, 29.8 };

            int[] color = { 0xcf4040, 0x40cf40, 0x4040cf, 0xcfcf40, 0x40cfcf, 0xcc3535, 0x35cc35, 0x3535cc, 0xcccc35, 0x35cccc };
            int[] symbol = { Chart.SquareSymbol, Chart.DiamondSymbol, Chart.TriangleSymbol, Chart.CircleSymbol, Chart.Cross2Symbol, Chart.RightTriangleSymbol };

            for (int i = 0; i < arrData.Count; i++)
            {
                layer.addDataSet((double[])arrData[i], color[i%10], arrLabel[i].ToString()).setDataSymbol(
                    symbol[i%6], 7);
            }

            //Add the first line. Plot the points with a 7 pixel square symbol
            //layer.addDataSet(data0, 0xcf4040, "Peak").setDataSymbol(
            //    Chart.SquareSymbol, 7);

            //Add the second line. Plot the points with a 9 pixel dismond symbol
            //layer.addDataSet(data1, 0x40cf40, "Average").setDataSymbol(
            //    Chart.DiamondSymbol, 9);

            //Enable data label on the data points. Set the label format to nn%.
            layer.setDataLabelFormat("{value|0}");

         }
    }
    private string[] GetXLabel(int yearFrom, int yearTo)
    {
        int diff = yearTo - yearTo;
        string[] xLabel = new string[diff];

        for (int i = 0; i < diff; i++)
        {
            xLabel[i] = (i + yearFrom).ToString();
        }
        return xLabel;
    }

    private void SetYearlyGraph()
    {
        int yearFrom = Convert.ToInt32((txtYearFrom.Text.Trim() == "" ? "0" : txtYearFrom.Text.Trim())) - 543;
        int yearTo = Convert.ToInt32((txtYearTo.Text.Trim() == "" ? "0" : txtYearTo.Text.Trim())) - 543;
        if (yearFrom < 0)
            yearFrom = 0;
        if (yearTo < 0)
            yearTo = 0;
        int tmp = yearFrom;
        if (yearFrom > yearTo)
        {
            yearFrom = yearTo;
            yearTo = tmp;
        }
        yearFrom += 543;
        yearTo += 543;

        string[] labels = { "�.�.", "�.�.", "��.�.", "��.�.", "�.�.", "��.�.", "�.�.", "�.�.", "�.�.", "�.�.", "�.�.", "�.�." };

        XYChart c = new XYChart(750, 380, 15663086, 14540253, 0);
        string title = GetTitle() + (yearFrom == yearTo ? "㹻� �.�. " + yearFrom.ToString() : " ������ �.�. " + yearFrom.ToString() + "-" + yearTo.ToString());
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
        c.yAxis().setTitle("�ӹǹ", "Tahoma Bold", 10);
        c.xAxis().setTitle("��͹", "Tahoma Bold", 10);

        LineLayer layer = c.addLineLayer();
        layer.setLineWidth(1);
        AddData(layer, yearFrom - 543, yearTo - 543);
        vwChart.Image = c.makeWebImage(Chart.PNG);
        vwChart.ImageMap = c.getHTMLImageMap(GetReportPath() + "?type=" + Request["type"] + "&warehouse=" + this.cmbWarehouse.SelectedItem.Value.ToString() + "&producttype=" + this.cmbProductType.SelectedItem.Value.ToString() + "&productgroup=" + this.cmbProductGroup.SelectedItem.Value.ToString() + "&product=" + this.cmbProduct.SelectedItem.Value.ToString() + "&currentyear={xLabel}&yearfrom=" + yearFrom.ToString() + "&yearto=" + yearTo.ToString(), "",
            "title='�� {dataSetName} ��͹ {xLabel}\r\n{value|,} ��¡�� ({percent}%)'");
    }
    #endregion

    #region Day
    private void AddData(LineLayer layer, int yearFrom, int yearTo, int month)
    {
        DataTable dTable = GetTableDay(yearFrom, yearTo, month);
         
        if (dTable.Rows.Count > 0)
        {
            ArrayList arrData = new ArrayList();
            ArrayList arrLabel = new ArrayList();
            string monthName = "";
            string[] labels = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

            for (int i = yearFrom; i <= yearTo; i++)
            {
                double[] d = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                DataView dv = new DataView(dTable);
                dv.RowFilter = "REQYEAR = " + i.ToString();
                for (int j = 0; j < dv.Count; j++)
                {
                    d[Convert.ToInt32(dv[j]["REQDAY"]) - 1] = Convert.ToDouble(dv[j]["QTY"]);
                }
                arrData.Add(d);

                arrLabel.Add(Convert.ToInt32(i) + 543);
            }

            //The data for the line chart
            //double[] data0 = { 60.2, 51.7, 81.3, 48.6, 56.2, 68.9, 52.8 };
            //double[] data1 = { 30.0, 32.7, 33.9, 29.5, 32.2, 28.4, 29.8 };

            int[] color = { 0xcf4040, 0x40cf40, 0x4040cf, 0xcfcf40, 0x40cfcf, 0xcc3535, 0x35cc35, 0x3535cc, 0xcccc35, 0x35cccc };
            int[] symbol = { Chart.SquareSymbol, Chart.DiamondSymbol, Chart.TriangleSymbol, Chart.CircleSymbol, Chart.Cross2Symbol, Chart.RightTriangleSymbol };

            for (int i = 0; i < arrData.Count; i++)
            {
                layer.addDataSet((double[])arrData[i], color[i % 10], arrLabel[i].ToString()).setDataSymbol(
                    symbol[i % 6], 7);
            }

            //Add the first line. Plot the points with a 7 pixel square symbol
            //layer.addDataSet(data0, 0xcf4040, "Peak").setDataSymbol(
            //    Chart.SquareSymbol, 7);

            //Add the second line. Plot the points with a 9 pixel dismond symbol
            //layer.addDataSet(data1, 0x40cf40, "Average").setDataSymbol(
            //    Chart.DiamondSymbol, 9);

            //Enable data label on the data points. Set the label format to nn%.
            layer.setDataLabelFormat("{value|0}");

        }
    }
    private string[] GetXLabel(int yearFrom, int yearTo, int month)
    {
        int diff = yearTo - yearTo;
        string[] xLabel = new string[diff];

        for (int i = 0; i < diff; i++)
        {
            xLabel[i] = (i + yearFrom).ToString();
        }
        return xLabel;
    }

    private void SetMonthlyGraph()
    {
        int yearFrom = Convert.ToInt32((txtYearFrom.Text.Trim() == "" ? "0" : txtYearFrom.Text.Trim())) - 543;
        int yearTo = Convert.ToInt32((txtYearTo.Text.Trim() == "" ? "0" : txtYearTo.Text.Trim())) - 543;
        int currentmonth = 0;

        if (yearFrom < 0)
            yearFrom = 0;
        if (yearTo < 0)
            yearTo = 0;
        int tmp = yearFrom;
        if (yearFrom > yearTo)
        {
            yearFrom = yearTo;
            yearTo = tmp;
        }
        yearFrom += 543;
        yearTo += 543;

        switch (Request["currentyear"])
        {
            case "�.�.":
                currentmonth = 1;
                break;

            case "�.�.":
                currentmonth = 2;
                break;

            case "��.�.":
                currentmonth = 3;
                break;

            case "��.�.":
                currentmonth = 4;
                break;

            case "�.�.":
                currentmonth = 5;
                break;

            case "��.�.":
                currentmonth = 6;
                break;

            case "�.�.":
                currentmonth = 7;
                break;

            case "�.�.":
                currentmonth = 8;
                break;

            case "�.�.":
                currentmonth = 9;
                break;

            case "�.�.":
                currentmonth = 10;
                break;

            case "�.�.":
                currentmonth = 11;
                break;

            case "�.�.":
                currentmonth = 12;
                break;
        }
        this.lnkYear.Text = GetTitle() + (yearFrom == yearTo ? "㹻� �.�. " + yearFrom.ToString() : " ������ �.�. " + yearFrom.ToString() + "-" + yearTo.ToString());
        this.lblMonth.Text = ">> ��͹ " + Request["currentyear"].ToString();
        this.pnlMonth.Visible = true;
        string[] labels = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

        XYChart c = new XYChart(750, 380, 15663086, 14540253, 0);
        string title = GetTitle() + (yearFrom == yearTo ? "㹻� �.�. " + yearFrom.ToString() : " ������ �.�. " + yearFrom.ToString() + "-" + yearTo.ToString());
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
        c.yAxis().setTitle("�ӹǹ", "Tahoma Bold", 10);
        c.xAxis().setTitle("�ѹ���", "Tahoma Bold", 10);

        LineLayer layer = c.addLineLayer();
        layer.setLineWidth(1);
        AddData(layer, yearFrom - 543, yearTo - 543, currentmonth);
        vwChart.Image = c.makeWebImage(Chart.PNG);
        vwChart.ImageMap = c.getHTMLImageMap("", "", "title='�ѹ��� {xLabel}\r\n{value|,} ��¡�� ({percent}%)'");
        this.pnlChart.Visible = true;
    }
    #endregion

    protected void rbtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE = '" + rbtType.SelectedValue + "'", "���͡", "0");
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE IN (SELECT LOID FROM PRODUCTTYPE WHERE TYPE = '" + rbtType.SelectedValue + "')", "���͡", "0");
        ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT PG.LOID FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID WHERE PT.TYPE = '" + rbtType.SelectedValue + "')", "���͡", "0");

    }
}
