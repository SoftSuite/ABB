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
    public partial class RepSaleSummaryBill : Form
    {
        public RepSaleSummaryBill()
        {
            InitializeComponent();
        }



        private void RepSaleSummaryBill_Load(object sender, EventArgs e)
        {
            dpDateFrom.Value = DateTime.Today;
            dpDateTo.Value = DateTime.Today;
            Appz.BuildCombo(cmbCustomer, "V_CUSTOMER", "CUSTOMERNAME", "LOID", "CUSTOMERNAME", "", "ทั้งหมด", "0");
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ReportParameterData repData1 = new ReportParameterData();
            ReportParameterData repData2 = new ReportParameterData();
            ReportParameterData repData3 = new ReportParameterData();
            ReportParameterData repData4 = new ReportParameterData();
            ReportParameterData repData5 = new ReportParameterData();
            ReportParameterData repData6 = new ReportParameterData();

            ArrayList arr = new ArrayList();

            repData1.PARAMETERNAME = "DATEFROM";
            repData1.PARAMETERVALUE = dpDateFrom.Value.ToString("dd/MM/") + dpDateFrom.Value.Year.ToString();
            arr.Add(repData1);

            repData2.PARAMETERNAME = "DATETO";
            repData2.PARAMETERVALUE = dpDateTo.Value.ToString("dd/MM/") + dpDateTo.Value.Year.ToString();
            arr.Add(repData2);

            repData3.PARAMETERNAME = "INVCODETO";
            repData3.PARAMETERVALUE = txtCodeTo.Text.Trim();
            arr.Add(repData3);

            repData4.PARAMETERNAME = "INVCODEFROM";
            repData4.PARAMETERVALUE = txtCodeFrom.Text.Trim();
            arr.Add(repData4);

            repData5.PARAMETERNAME = "CUSTOMER";
            repData5.PARAMETERVALUE = cmbCustomer.SelectedValue.ToString();
            arr.Add(repData5);

            repData6.PARAMETERNAME = "WAREHOUSE";
            repData6.PARAMETERVALUE = Appz.CurrentUserData.Warehouse.ToString();
            arr.Add(repData6);

            Reports.PreviewReport frmPreviewReport = new Reports.PreviewReport("SaleSummaryBillReport", arr);
            frmPreviewReport.ShowDialog(this);
        }

    }
}