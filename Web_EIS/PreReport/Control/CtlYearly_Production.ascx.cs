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

public partial class PreReport_Control_CtlYearly_Production : System.Web.UI.UserControl
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
        lblHead.Text = "รายงานจำนวนสินค้าที่ผลิตได้";
        lblSubHead.Text = "รายงานจำนวนสินค้าที่ผลิตได้ เปรียบเทียบปี";
    }

    private void LoadCombo()
    {
        ComboSource.BuildCombo(cmbProduceGroup, "PRODUCEGROUP", "NAME", "LOID", "LOID", "ACTIVE = '1' ");
        ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND TYPE = '" + rbtType.SelectedValue + "' AND PRODUCEGROUP = '" + cmbProduceGroup.SelectedValue + "'", "เลือก", "0");

    }


    protected void cmbProduceGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
            ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND PRODUCEGROUP = " + cmbProduceGroup.SelectedItem.Value, "เลือก", "0");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (txtYearFrom.Text.Trim() == "" || txtYearTo.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุปีในการค้นหาให้ครบถ้วน");
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
        return "รายงานจำนวนสินค้าที่ผลิตได้ เปรียบเทียบปี";

    }

    private void AddData(LineLayer layer, int yearFrom, int yearTo)
    {
        DataTable dTable = null;

        dTable = EISReportProductionFlow.GetYearlyPublished(yearFrom, yearTo, cmbProduceGroup.SelectedItem.Value, cmbProduct.SelectedItem.Value);

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
        ComboSource.BuildCombo(cmbProduct, "PRODUCT", "PRODUCTNAME", "PRODUCTMASTER", "PRODUCTNAME", "ACTIVE = '1' AND TYPE ='" + rbtType.SelectedValue + "' AND PRODUCEGROUP = '" + cmbProduceGroup.SelectedValue + "'", "เลือก", "0");

    }
}
