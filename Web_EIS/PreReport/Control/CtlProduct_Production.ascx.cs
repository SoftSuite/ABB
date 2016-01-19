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

public partial class PreReport_Control_CtlProduct_Production : System.Web.UI.UserControl
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
            if(rbtYear.Checked == true && rbtDate.Checked == false)
            {
                tdDate1.Visible = false;

                SetHead();
                LoadComboYear();
                if (Request["yearfrom"] != null) this.txtYearFrom.Text = Request["yearfrom"];
                if (Request["yearto"] != null) this.txtYearTo.Text = Request["yearto"];
                if (Request["BarcodeFrom"] != null) this.txtBarcodeFrom.Text = Request["BarcodeFrom"];
                if (Request["BarcodeTo"] != null) this.txtBarcodeTo.Text = Request["BarcodeTo"];
                if (Request["currentyear"] != null)
                {
                    TextBox1.Text = Request["currentyear"].ToString();
                    this.chkOption.Checked = false;
                    this.pnlConstraints.Visible = this.chkOption.Checked; 
                    ComboSource.BuildCombo(cmbProduceGroup, "PRODUCEGROUP", "NAME", "LOID", "LOID", "ACTIVE = '1' ");
                    this.cmbProduceGroup.SelectedIndex = this.cmbProduceGroup.Items.IndexOf(this.cmbProduceGroup.Items.FindByValue(Request["producegroup"]));
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
                    //SetMonthlyGraph();
                }
                else if (Request["currentmonth"] != null && Request["currentdatayear"] != null)
                {
                    TextBox1.Text = Request["currentdatayear"].ToString();
                    this.chkOption.Checked = false;
                    this.pnlConstraints.Visible = this.chkOption.Checked;
                    ComboSource.BuildCombo(cmbProduceGroup, "PRODUCEGROUP", "NAME", "LOID", "LOID", "ACTIVE = '1' ");
                    this.cmbProduceGroup.SelectedIndex = this.cmbProduceGroup.Items.IndexOf(this.cmbProduceGroup.Items.FindByValue(Request["producegroup"]));
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
                    SetMonthlyGraph();
                }
                else
                {
                    Session["group"] = null;
                }
            }
            else if (rbtDate.Checked == true && rbtYear.Checked == false)
            {

                SetHead();
                LoadComboDate();
                //if (Request["yearfrom"] != null) this.txtYearFrom.Text = Request["yearfrom"];
                //if (Request["yearto"] != null) this.txtYearTo.Text = Request["yearto"];
                ComboSource.BuildCombo(cmbProduceGroup, "PRODUCEGROUP", "NAME", "LOID", "LOID", "ACTIVE = '1' ");
                this.cmbProduceGroup.SelectedIndex = this.cmbProduceGroup.Items.IndexOf(this.cmbProduceGroup.Items.FindByValue(Request["producegroup"]));
                BindCheckboxList();
            }
        }
    }

    #region "Common"

    private void SetHead()
    {
                lblHead.Text = "รายงานจำนวนสินค้าที่ผลิตได้";
                lblSubHead.Text = "รายงานจำนวนสินค้าที่ผลิตได้ เปรียบเทียบสินค้า";
    }

    private string GetReportPath()
    {
        string path = "";
        path = ABB.Data.Constz.HomeFolder + "PreReport/Production_Month_Product.aspx";

        return path;
    }

    private void LoadComboYear()
    {
        ComboSource.BuildCombo(cmbProduceGroup, "PRODUCEGROUP", "NAME", "LOID", "LOID", "ACTIVE = '1' ");
        BindCheckboxList();
        string scripts = "";
        for (int i = 0; i < this.chklist.Items.Count; ++i)
        {
            scripts += (scripts == "" ? "" : " || ") + "document.getElementById('" + this.chklist.ClientID + "_" + i.ToString() + "').checked";
        }
        scripts = "if (!(" + scripts + ")) {alert('กรุณาเลือกสินค้าหรือวัตถุดิบ'); return false; }";
        this.btnReport.OnClientClick = (scripts == "" ? "" : scripts + " else ") + "if (document.getElementById('" + this.txtYearFrom.ClientID + "').value == '' || document.getElementById('" + this.txtYearTo.ClientID + "').value == '') {alert('กรุณาระบุช่วงเวลาให้ครบถ้วน'); return false;} else if (document.getElementById('" + this.txtYearFrom.ClientID + "').value.length <4 || document.getElementById('" + this.txtYearTo.ClientID + "').value.length<4) {alert('กรุณาระบุเลขปีให้ถูกต้อง'); return false;}";
    }
    private void LoadComboDate()
    {
        ComboSource.BuildCombo(cmbProduceGroup, "PRODUCEGROUP", "NAME", "LOID", "LOID", "ACTIVE = '1' ");
        BindCheckboxList();
        string scripts = "";
        for (int i = 0; i < this.chklist.Items.Count; ++i)
        {
            scripts += (scripts == "" ? "" : " || ") + "document.getElementById('" + this.chklist.ClientID + "_" + i.ToString() + "').checked";
        }
        scripts = "if (!(" + scripts + ")) {alert('กรุณาเลือกสินค้าหรือวัตถุดิบ'); return false; }";
        this.btnReport.OnClientClick = (scripts == "" ? "" : scripts + " else ") + "if (document.getElementById('" + this.ctlDateFrom.ClientID + "').value == '' || document.getElementById('" + this.ctlDateTo.ClientID + "').value == '') {alert('กรุณาระบุช่วงวันที่ให้ครบถ้วน'); return false;}";
    }

    protected void cmbProduceGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCheckboxList();
    }

    private void BindCheckboxList()
    {
        string where = "P.ACTIVE = '" + ABB.Data.Constz.ActiveStatus.Active + "' AND P.TYPE = '" + rbtType.SelectedValue + "' AND PM.ORDERTYPE IN ('PD','AL')";
        //if (this.cmbProduceGroup.SelectedItem.Value.ToString() != "0")
            where += (where == "" ? "" : " AND ") + "P.PRODUCEGROUP = " + this.cmbProduceGroup.SelectedItem.Value.ToString() + " ";

            DataTable dt = EISReportProductionFlow.GetProductList("PRODUCT P INNER JOIN PRODUCTMASTER PM ON P.PRODUCTMASTER = PM.LOID ", "P.PRODUCTNAME", "P.PRODUCTMASTER", "P.PRODUCTNAME", where);
      chklist.DataSource = dt;
      chklist.DataTextField = "PRODUCTNAME";
      chklist.DataValueField = "PRODUCTMASTER";
      chklist.DataBind();
    }

    private string GetReportTitle()
    {
        return "รายงานจำนวนสินค้าที่ผลิตได้ เปรียบเทียบสินค้า";
    }

    private DataTable GetTable(int yearFrom, int yearTo, string BarcodeFrom, string BarcodeTo)
    {
        DataTable dTable = new DataTable();
        dTable = EISReportProductionFlow.GetProductPublishedGroupByYear(yearFrom, yearTo,BarcodeFrom,BarcodeTo, cmbProduceGroup.SelectedItem.Value, GetSelectedProduct());

        return dTable;
    }
    private DataTable GetTableMonth(int year, string BarcodeFrom, string BarcodeTo)
    {
        DataTable dTable = new DataTable();
        dTable = EISReportProductionFlow.GetProductPublishedGroupByMonth(year,BarcodeFrom,BarcodeTo, GetSelectedProduct());

        return dTable;
    }

    private DataTable GetTableMonth(int yearFrom, int yearTo, string BarcodeFrom, string BarcodeTo)
    {
        DataTable dTable = new DataTable();

        dTable = EISReportProductionFlow.GetProductPublishedGroupByDay(yearFrom, yearTo,BarcodeFrom,BarcodeTo, GetSelectedProduct());

        return dTable;
    }
    private DataTable GetTableDay(int year, int month)
    {
        DataTable dTable = new DataTable();

        dTable = EISReportProductionFlow.GetProductPublishedGroupByDay1(year, month, GetSelectedProduct());


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

    //private string[] GetXLabelMonth()
    //{
    //    string[] labels = { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
    //    return labels;
    //}

    private void AddDataMonth(LineLayer layer, int year, string BarcodeFrom, string BarcodeTo)
    {
        DataTable dTable = GetTableMonth(year, BarcodeFrom, BarcodeTo);
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
            layer.setDataLabelFormat("{value|0}");
        }
    }

    private void SetMonthlyProductGraph()
    {
        int yearFrom = Convert.ToInt32(Request["yearfrom"]);
        int yearTo = Convert.ToInt32(Request["yearto"]);
        int currentYear = Convert.ToInt32(Request["currentyear"]);
        string BarcodeFrom = Request["BarcodeFrom"];
        string BarcodeTo = Request["BarcodeTo"];
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
        c.yAxis().setTitle("จำนวน", "Tahoma Bold", 10);
        c.xAxis().setTitle("เดือน", "Tahoma Bold", 10);

        LineLayer layer = c.addLineLayer();
        layer.setLineWidth(1);
        AddDataMonth(layer, currentYear - 543, BarcodeFrom, BarcodeTo);
        vwChart.Image = c.makeWebImage(Chart.PNG);
        //vwChart.ImageMap = c.getHTMLImageMap(GetReportPath() + "?type=" + "&currentmount={xLabel}&yearfrom=" + yearFrom.ToString(), "", "title='{dataSetName} ในเดือน {xLabel}\r\n{value|,} รายการ ({percent}%)'");
        vwChart.ImageMap = c.getHTMLImageMap(GetReportPath() + "?type=" + Request["type"] + "&producegroup=" + this.cmbProduceGroup.SelectedItem.Value.ToString() + "&BarcodeFrom="+ this.txtBarcodeFrom.Text + "&BarcodeTo=" + this.txtBarcodeTo.Text + "&currentdatayear="+ TextBox1.Text.ToString() + "&currentmonth={xLabel}&yearfrom=" + yearFrom.ToString() + "&yearto=" + yearTo.ToString(), "",
           "title='{dataSetName} ในเดือน {xLabel}\r\n{value|,} รายการ ({percent}%)'");
        this.pnlChart.Visible = true;
    }

    #endregion


    #region Month
    private void AddDataMonth(LineLayer layer, int yearFrom, int yearTo, string BarcodeFrom, string BarcodeTo)
    {

        DataTable dTable = GetTableMonth(yearFrom, yearTo,BarcodeFrom, BarcodeTo);

        if (dTable.Rows.Count > 0)
        {
            ArrayList arrData = new ArrayList();
            ArrayList arrLabel = new ArrayList();
            string[] labels = { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };

            for (int i = yearFrom; i <= yearTo; i++)
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

            int[] color = { 0xcf4040, 0x40cf40, 0x4040cf, 0xcfcf40, 0x40cfcf, 0xcc3535, 0x35cc35, 0x3535cc, 0xcccc35, 0x35cccc };
            int[] symbol = { Chart.SquareSymbol, Chart.DiamondSymbol, Chart.TriangleSymbol, Chart.CircleSymbol, Chart.Cross2Symbol, Chart.RightTriangleSymbol };

            for (int i = 0; i < arrData.Count; i++)
            {
                layer.addDataSet((double[])arrData[i], color[i % 10], arrLabel[i].ToString()).setDataSymbol(
                    symbol[i % 6], 7);
            }
            layer.setDataLabelFormat("{value|0}");

        }
    }
    //private string[] GetXLabel(int yearFrom, int yearTo)
    //{
    //    int diff = yearTo - yearTo;
    //    string[] xLabel = new string[diff];

    //    for (int i = 0; i < diff; i++)
    //    {
    //        xLabel[i] = (i + yearFrom).ToString();
    //    }
    //    return xLabel;
    //}

    private void SetYearlyGraph()
    {
        int yearFrom = Convert.ToInt32((txtYearFrom.Text.Trim() == "" ? "0" : txtYearFrom.Text.Trim())) - 543;
        int yearTo = Convert.ToInt32((txtYearTo.Text.Trim() == "" ? "0" : txtYearTo.Text.Trim())) - 543;
        string BarcodeFrom = this.txtBarcodeFrom.Text;
        string BarcodeTo = this.txtBarcodeTo.Text;
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
        string title = GetReportTitle() + (yearFrom == yearTo ? "ในปี พ.ศ. " + yearFrom.ToString() : " ตั้งแต่ปี พ.ศ. " + yearFrom.ToString() + "-" + yearTo.ToString());
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
        AddDataMonth(layer, yearFrom - 543, yearTo - 543,BarcodeFrom,BarcodeTo);
        vwChart.Image = c.makeWebImage(Chart.PNG);
        //vwChart.ImageMap = c.getHTMLImageMap(GetReportPath() + "?type=" + Request["type"] + "&warehouse=" + this.cmbWarehouse.SelectedItem.Value.ToString() + "&producttype=" + this.cmbProductType.SelectedItem.Value.ToString() + "&productgroup=" + this.cmbProductGroup.SelectedItem.Value.ToString() + "&product=" + this.cmbProduct.SelectedItem.Value.ToString() + "&currentyear={xLabel}&yearfrom=" + yearFrom.ToString() + "&yearto=" + yearTo.ToString(), "",
            //"title='ปี {dataSetName} เดือน {xLabel}\r\n{value|,} รายการ ({percent}%)'");
        vwChart.ImageMap = c.getHTMLImageMap("", "", "title='{dataSetName} ในเดือน {xLabel}\r\n{value|,} รายการ ({percent}%)'");
    }
    #endregion

    #region Day
    private void AddDataDay(LineLayer layer, int year, int month)
    {
        DataTable dTable = GetTableDay(year, month);

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
        int currentyear = Convert.ToInt32((TextBox1.Text.Trim() == "" ? "0" : TextBox1.Text.Trim())) - 543;
        currentyear += 543;

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

        switch (Request["currentmonth"])
        {
            case "ม.ค.":
                currentmonth = 1;
                break;

            case "ก.พ.":
                currentmonth = 2;
                break;

            case "มี.ค.":
                currentmonth = 3;
                break;

            case "เม.ย.":
                currentmonth = 4;
                break;

            case "พ.ค.":
                currentmonth = 5;
                break;

            case "มิ.ย.":
                currentmonth = 6;
                break;

            case "ก.ค.":
                currentmonth = 7;
                break;

            case "ส.ค.":
                currentmonth = 8;
                break;

            case "ก.ย.":
                currentmonth = 9;
                break;

            case "ต.ค.":
                currentmonth = 10;
                break;

            case "พ.ย.":
                currentmonth = 11;
                break;

            case "ธ.ค.":
                currentmonth = 12;
                break;
        }
        this.lnkYear.Text = GetReportTitle() + (yearFrom == yearTo ? "ในปี พ.ศ. " + yearFrom.ToString() : " เดือน " + Request["currentmonth"] + " ในปี พ.ศ. " + yearTo.ToString());
        this.lblMonth.Text = ">> เดือน " + Request["currentmonth"].ToString();
        this.pnlMonth.Visible = true;
        string[] labels = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

        XYChart c = new XYChart(750, 380, 15663086, 14540253, 0);
        string title = GetReportTitle() + (yearFrom == yearTo ? "ในปี พ.ศ. " + yearFrom.ToString() : " เดือน " + Request["currentmonth"] + " ในปี พ.ศ. " + yearTo.ToString());
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
        c.xAxis().setTitle("วันที่", "Tahoma Bold", 10);

        LineLayer layer = c.addLineLayer();
        layer.setLineWidth(1);
        //AddData(layer, yearFrom - 543, yearTo - 543, currentmonth);
        AddDataDay(layer, currentyear - 543, currentmonth);
        vwChart.Image = c.makeWebImage(Chart.PNG);
        vwChart.ImageMap = c.getHTMLImageMap("", "", "title='วันที่ {xLabel}\r\n{value|,} รายการ ({percent}%)'");
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

    private void AddData(LineLayer layer, int yearFrom, int yearTo,string BarcodeFrom,string BarcodeTo)
    {
        DataTable dTable = GetTable(yearFrom, yearTo, BarcodeFrom, BarcodeTo);
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
            layer.setDataLabelFormat("{value|0}");
        }
    }
    
    private void SetYearlyProductGraph()
    {
        this.pnlMonth.Visible = false;
        int yearFrom = Convert.ToInt32(this.txtYearFrom.Text == "" ? "0" : this.txtYearFrom.Text)-543;
        int yearTo = Convert.ToInt32(this.txtYearTo.Text == "" ? "0" : this.txtYearTo.Text)-543;
        string BarcodeFrom = this.txtBarcodeFrom.Text;
        string BarcodeTo = this.txtBarcodeTo.Text;
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
        c.yAxis().setTitle("จำนวนสินค้า/วัตถุดิบ", "Tahoma Bold", 10);
        c.xAxis().setTitle("ปี พ.ศ.", "Tahoma Bold", 10);

        LineLayer layer = c.addLineLayer();
        layer.setLineWidth(1);
        AddData(layer, yearFrom - 543, yearTo - 543,BarcodeFrom,BarcodeTo);
        vwChart.Image = c.makeWebImage(Chart.PNG);
        vwChart.ImageMap = c.getHTMLImageMap(GetReportPath()+ "?type=" + Request["type"] +  "&producegroup=" + this.cmbProduceGroup.SelectedItem.Value.ToString() + "&BarcodeFrom =" + this.txtBarcodeFrom.Text + "&BarcodeTo =" + this.txtBarcodeTo.Text+ "&currentyear={xLabel}&yearfrom=" + yearFrom.ToString() + "&yearto=" + yearTo.ToString(), "",
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
        if (rbtYear.Checked == true)
        {
            this.chkOption.Checked = false;
            this.pnlConstraints.Visible = false;
            SetYearlyProductGraph();
            this.pnlChart.Visible = true;
        }
        else 
        {
            this.chkOption.Checked = false;
            this.pnlConstraints.Visible = false;
            SetDatelyProductGraph();
            this.pnlChart.Visible = true;
        }
    }
    protected void rbtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCheckboxList();
    }
    protected void rbtYear_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtYear.Checked == true)
        {
            rbtDate.Checked = false;
            txtYearFrom.Visible = true;
            txtYearTo.Visible = true;
            Label1.Visible = true;
            //tdDate.Visible = false;
            tdDate1.Visible = false;
            rbtDate1.Checked = false;
        }
    }
    protected void rbtDate_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtDate.Checked == true)
        {
            Label1.Visible = false;
            tdDate1.Visible = false;
            rbtYear.Checked = false;
            txtYearFrom.Visible = false;
            txtYearTo.Visible = false;
        }
    }
    protected void rbtDate1_CheckedChanged(object sender, EventArgs e)
    {
        tdDate1.Visible = false;
        rbtDate.Checked = true;
        rbtYear.Checked = false;
        rbtDate.Checked = true;
        txtYearFrom.Visible = false;
        txtYearTo.Visible = false;
        Label1.Visible = false;
    }
    #region Date
    private string[] GetDateXLabel(DateTime DateFrom, DateTime DateTo)
    {
        TimeSpan diff = DateTo - DateFrom;
        int days = diff.Days;
        string[] xLabel = new string[days + 1];
        for (int iday = 0; iday <= days; iday++)
        {
            xLabel[iday] = DateFrom.AddDays(iday).ToString("dd/MM/yyyy");

        }

        return xLabel;
    }
    private void AddDateData(LineLayer layer, DateTime DateFrom, DateTime DateTo, string BarcodeFrom, string BarcodeTo)
    {
        DataTable dTable = GetDateTable(DateFrom, DateTo, BarcodeFrom, BarcodeTo);
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
                layer.setDataLabelFormat("{value|0}");
            }
        }
    }

    private DataTable GetDateTable(DateTime DateFrom, DateTime DateTo, string BarcodeFrom, string BarcodeTo)
    {
        DataTable dTable = new DataTable();
        dTable = EISReportProductionFlow.GetProductPublishedGroupByDate(DateFrom, DateTo, BarcodeFrom, BarcodeTo, cmbProduceGroup.SelectedItem.Value, GetSelectedProduct());

        return dTable;
    }
    private void SetDatelyProductGraph()
    {
        this.pnlMonth.Visible = false;
        DateTime DateFrom = this.ctlDateFrom.DateValue;
        DateTime DateTo = this.ctlDateTo.DateValue;
        string BarcodeFrom = this.txtBarcodeFrom.Text;
        string BarcodeTo = this.txtBarcodeTo.Text;
        DateTime temp = DateFrom;
        if (DateFrom > DateTo)
        {
            DateFrom = DateTo;
            DateTo = temp;
        }
        string[] labels = GetDateXLabel(DateFrom, DateTo);
        XYChart c = new XYChart(750, 380, 15663086, 14540253, 0);
        string title = GetReportTitle() + (DateFrom == DateTo ? "ในวันที่ " + DateFrom.ToString() : " ตั้งแต่วันที่ " + DateFrom.ToString().Substring(0, 9) + "-" + DateTo.ToString().Substring(0, 9));

        c.addTitle(title, "Tahoma Bold", 12);
        c.setPlotArea(70, 80, 640, 230, c.gradientColor(0, 60, 0, 350, 16777215, 11189196), -1, Chart.Transparent, 1111);
        c.addLegend(30, 25, false, "Tahoma Bold", 8).setBackground(Chart.Transparent); c.xAxis().setLabels(labels);
        c.yAxis().setTickDensity(30);
        c.xAxis().setLabelStyle("Tahoma", 8, 001122, 90);
        c.yAxis().setLabelStyle("Tahoma", 8);
        c.yAxis().setLabelFormat("{value|0,}");
        c.xAxis().setWidth(2);
        c.yAxis().setWidth(2);
        c.yAxis().setTitle("จำนวนสินค้า/วัตถุดิบ", "Tahoma Bold", 10);
        c.xAxis().setTitle("วันที่", "Tahoma Bold", 10);

        c.xAxis().setLabels(labels);


        LineLayer layer = c.addLineLayer();
        layer.setLineWidth(1);
        AddDateData(layer, DateFrom, DateTo, BarcodeFrom, BarcodeTo);
        vwChart.Image = c.makeWebImage(Chart.PNG);
        vwChart.ImageMap = c.getHTMLImageMap("", "",
            "title='{dataSetName} ในวันที่ {xLabel}\r\n{value|,} รายการ ({percent}%)'");
    }
    #endregion
}
