using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Data;
using System.Collections;

namespace ABBClient.PreReport
{
    public partial class RepStockoutDoctype : Form
    {
        public RepStockoutDoctype()
        {
            InitializeComponent();
        }

        private void RepStockoutDoctype_Load(object sender, EventArgs e)
        {
            dpDateFrom.Value = DateTime.Today;
            dpDateTo.Value = DateTime.Today;
            Appz.BuildCombo(cmbCustomer, "V_CUSTOMER", "CUSTOMERNAME", "LOID", "CUSTOMERNAME", "", "ทั้งหมด", "0");
            Appz.BuildCombo(cmbDocType, "V_REQTYPE_RESERVE ", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ReportParameterData repData1 = new ReportParameterData();
            ReportParameterData repData2 = new ReportParameterData();
            ReportParameterData repData3 = new ReportParameterData();
            ReportParameterData repData4 = new ReportParameterData();
            ReportParameterData repData5 = new ReportParameterData();
            ReportParameterData repData6 = new ReportParameterData();
            ReportParameterData repData7 = new ReportParameterData();

            ArrayList arr = new ArrayList();

            repData1.PARAMETERNAME = "REQUISITION";
            repData1.PARAMETERVALUE = cmbDocType.SelectedValue.ToString();
            arr.Add(repData1);

            //repData2.PARAMETERNAME = "DATEFROM";
            //repData2.PARAMETERVALUE = dpDateFrom.Value.ToString("dd/MM/")+dpDateFrom.Value.Year.ToString();
            //arr.Add(repData2);

            //repData3.PARAMETERNAME = "DATETO";
            //repData3.PARAMETERVALUE = dpDateTo.Value.ToString("dd/MM/") + dpDateTo.Value.Year.ToString();
            //arr.Add(repData3);

            repData2.PARAMETERNAME = "DATEFROM";
            repData2.PARAMETERVALUE = dpDateFrom.Value.Year.ToString() + dpDateFrom.Value.ToString("MMdd");
            arr.Add(repData2);

            repData3.PARAMETERNAME = "DATETO";
            repData3.PARAMETERVALUE = dpDateTo.Value.Year.ToString() + dpDateTo.Value.ToString("MMdd");
            arr.Add(repData3);

            repData4.PARAMETERNAME = "INVCODETO";
            repData4.PARAMETERVALUE = txtCodeTo.Text.Trim();
            arr.Add(repData4);

            repData5.PARAMETERNAME = "INVCODEFROM";
            repData5.PARAMETERVALUE = txtCodeFrom.Text.Trim();
            arr.Add(repData5);

            repData6.PARAMETERNAME = "CUSTOMER";
            repData6.PARAMETERVALUE = cmbCustomer.SelectedValue.ToString();
            arr.Add(repData6);

            repData7.PARAMETERNAME = "WAREHOUSE";
            repData7.PARAMETERVALUE = Appz.CurrentUserData.Warehouse.ToString();
            arr.Add(repData7);

            Reports.PreviewReport frmPreviewReport = new Reports.PreviewReport("StockoutDoctypeReport", arr);
            frmPreviewReport.ShowDialog(this);
        }
    }
}