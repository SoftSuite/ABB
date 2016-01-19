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
    public partial class RepSendMoney : Form
    {
        public RepSendMoney()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ReportParameterData repData = new ReportParameterData();
            ReportParameterData repData1 = new ReportParameterData();
            ReportParameterData repData2 = new ReportParameterData();
            ArrayList arr = new ArrayList();
            
            repData.PARAMETERNAME = "CODEFROM";
            repData.PARAMETERVALUE = txtInvCodeFrom.Text.Trim();
            arr.Add(repData);

            repData1.PARAMETERNAME = "CODETO";
            repData1.PARAMETERVALUE = txtInvCodeTo.Text.Trim();
            arr.Add(repData1);

            repData2.PARAMETERNAME = "WAREHOUSE";
            repData2.PARAMETERVALUE = Appz.CurrentUserData.Warehouse.ToString();
            arr.Add(repData2);

            Reports.PreviewReport frmPreviewReport = new Reports.PreviewReport("SendMoneyReport", arr);
            frmPreviewReport.ShowDialog(this);
        }
    }
}