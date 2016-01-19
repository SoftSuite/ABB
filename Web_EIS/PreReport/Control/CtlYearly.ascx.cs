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

public partial class PreReport_Control_CtlYearly : System.Web.UI.UserControl
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
            pnlGraph.Visible = false;
        }
    }

    private void SetHead()
    { 
        switch (Request.QueryString["type"])
        {
            case "sale":
                lblHead.Text = "รายงานจำนวนสินค้าที่ขายได้";
                lblSubHead.Text = "รายงานจำนวนสินค้าที่ขายได้ เปรียบเทียบปี";
                break;

            case "stockin":
                lblHead.Text = "รายงานจำนวนสินค้าที่รับเข้า";
                lblSubHead.Text = "รายงานจำนวนสินค้าที่รับเข้า เปรียบเทียบปี";
                break;

            case "stockout":
                lblHead.Text = "รายงานจำนวนสินค้าที่จ่ายออก";
                lblSubHead.Text = "รายงานจำนวนสินค้าที่จ่ายออก เปรียบเทียบปี";
                break;

            case "support":
                lblHead.Text = "รายงานจำนวนสินค้าที่สนับสนุน";
                lblSubHead.Text = "รายงานจำนวนสินค้าที่สนับสนุน เปรียบเทียบปี";
                break;

            case "return":
                lblHead.Text = "รายงานจำนวนสินค้าที่รับคืน";
                lblSubHead.Text = "รายงานจำนวนสินค้าที่รับคืน เปรียบเทียบปี";
                break;

            case "sendback":
                lblHead.Text = "รายงานจำนวนสินค้าที่ส่งคืน";
                lblSubHead.Text = "รายงานจำนวนสินค้าที่ส่งคืน เปรียบเทียบปี";
                break;
        }
    }

    private void LoadCombo()
    {
        ComboSource.BuildCombo(cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "ACTIVE = '1'", "เลือก", "0");
        ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE = '" + rbtType.SelectedValue + "'", "เลือก", "0");
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE IN (SELECT LOID FROM PRODUCTTYPE WHERE TYPE = '" + rbtType.SelectedValue + "')", "เลือก", "0");
        ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT PG.LOID FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID WHERE PT.TYPE = '" + rbtType.SelectedValue + "')", "เลือก", "0");

    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbProductType.SelectedItem.Value == "0")
        {
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE IN (SELECT LOID FROM PRODUCTTYPE WHERE TYPE = '" + rbtType.SelectedValue + "')", "เลือก", "0");
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT PG.LOID FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID WHERE PT.TYPE = '" + rbtType.SelectedValue + "')", "เลือก", "0");
        }
        else
        {
            ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE =" + cmbProductType.SelectedItem.Value, "เลือก", "0");
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT LOID FROM PRODUCTGROUP WHERE PRODUCTTYPE = " + cmbProductType.SelectedItem.Value + ")", "เลือก", "0");
        }
    }

    protected void cmbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbProductGroup.SelectedItem.Value == "0")
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT LOID FROM PRODUCTGROUP WHERE PRODUCTTYPE = " + cmbProductType.SelectedItem.Value + ")", "เลือก", "0");
        else
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP = " + cmbProductGroup.SelectedItem.Value, "เลือก", "0");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (txtYearFrom.Text.Trim() == "" || txtYearTo.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุปีในการค้นหาให้ครบถ้วน");
            return;
        }

        if (cmbWarehouse.SelectedItem.Value == "0")
        {
            Appz.ClientAlert(Page, "กรุณาเลือกคลังสินค้า");
            return;
        }

        if (cmbProductType.SelectedItem.Value == "0")
        {
            Appz.ClientAlert(Page, "กรุณาเลือกประเภทสินค้า/วัตถุดิบ");
            return;
        }

        if (cmbProductGroup.SelectedItem.Value == "0")
        {
            Appz.ClientAlert(Page, "กรุณาเลือกกลุ่มสินค้า/วัตถุดิบ");
            return;
        }

        if (cmbProduct.SelectedItem.Value == "0")
        {
            Appz.ClientAlert(Page, "กรุณาเลือกชื่อสินค้า/วัตถุดิบ");
            return;
        }

        SetYearlyGraph();
        pnlGraph.Visible = true;
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

        string[] labels = { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };

        XYChart c = new XYChart(750, 380, 15663086, 14540253, 0);
        string title = GetTitle() + (yearFrom == yearTo ? "ในปี พ.ศ. " + yearFrom.ToString() : " ตั้งแต่ปี พ.ศ. " + yearFrom.ToString() + "-" + yearTo.ToString());
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
        c.yAxis().setTitle("จำนวน", "Tahoma Bold", 10);
        c.xAxis().setTitle("เดือน", "Tahoma Bold", 10);

        LineLayer layer = c.addLineLayer();
        layer.setLineWidth(1);
        AddData(layer, yearFrom - 543, yearTo - 543);
        vwChart.Image = c.makeWebImage(Chart.PNG);
    }

    private string GetTitle()
    {
        switch (Request.QueryString["type"])
        {
            case "sale":
                return "รายงานจำนวนสินค้าที่ขายได้ เปรียบเทียบปี";

            case "stockin":
                return "รายงานจำนวนสินค้าที่รับเข้า  เปรียบเทียบปี";

            case "stockout":
                return "รายงานจำนวนสินค้าที่จ่ายออก เปรียบเทียบปี";

            case "support":
                return "รายงานจำนวนสินค้าที่สนับสนุน เปรียบเทียบปี";

            case "return":
                return "รายงานจำนวนสินค้าที่รับคืน เปรียบเทียบปี";

            case "sendback":
                return "รายงานจำนวนสินค้าที่ส่งคืน เปรียบเทียบปี";

            default:
                return "";
        }
    }

    private void AddData(LineLayer layer, int yearFrom, int yearTo)
    {
        DataTable dTable = null;

        switch (Request.QueryString["type"])
        {
            case "sale":
                dTable = EISReportSaleFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "stockin":
                dTable = EISReportStockinFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "stockout":
                dTable = EISReportStockoutFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "support":
                dTable = EISReportSupportFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "return":
                dTable = EISReportReturnFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;

            case "sendback":
                dTable = EISReportSendbackFlow.GetYearlyPublished(yearFrom, yearTo, cmbWarehouse.SelectedItem.Value, cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);
                break;
        }

        if (dTable.Rows.Count > 0)
        {
            ArrayList arrData = new ArrayList();
            ArrayList arrLabel = new ArrayList();
            string[] labels = { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };

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
    protected void rbtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1' AND TYPE = '" + rbtType.SelectedValue + "'", "เลือก", "0");
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1' AND PRODUCTTYPE IN (SELECT LOID FROM PRODUCTTYPE WHERE TYPE = '" + rbtType.SelectedValue + "')", "เลือก", "0");
        ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCTGROUP IN (SELECT PG.LOID FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID WHERE PT.TYPE = '" + rbtType.SelectedValue + "')", "เลือก", "0");

    }
}
