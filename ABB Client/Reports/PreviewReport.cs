using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using ABB.Data;

namespace ABBClient.Reports
{
    public partial class PreviewReport : Form
    {
        //public PreviewReport()
        //{
        //    InitializeComponent();
        //}

        /// <summary>
        /// Preview report
        /// </summary>
        /// <param name="reportName">Report name</param>
        /// <param name="paramData">Array of ABB.Data.ReportParameterData</param>
        public PreviewReport(string reportName, ArrayList paramData)
        {
            InitializeComponent();
            ViewReport(reportName, paramData);
        }

        private void ViewReport(string reportName, ArrayList paramData)
        {
            ParameterDiscreteValue paramValue = new ParameterDiscreteValue();
            ParameterValues curValue = new ParameterValues();
            TableLogOnInfo logonInfo = new TableLogOnInfo();
            ReportClass reportObj = null;

            try
            {
                logonInfo.ConnectionInfo.ServerName = ConfigurationManager.AppSettings["DB_SERVER"].ToString().Trim();
                logonInfo.ConnectionInfo.UserID = ConfigurationManager.AppSettings["DB_USER"].ToString().Trim();
                logonInfo.ConnectionInfo.Password = ConfigurationManager.AppSettings["DB_PASSWORD"].ToString();

                switch (reportName)
                {
                    case Constz.Report.Invoice:
                        reportObj = new ABBClient.Reports.Invoice();
                        break;

                    case Constz.Report.SendMoneyReport:
                        reportObj = new ABBClient.Reports.SendMoneyReport();
                        break;

                    case Constz.Report.StockRemainReport:
                        reportObj = new ABBClient.Reports.StockRemainReport();
                        break;

                    case Constz.Report.StockoutDoctypeReport:
                        reportObj = new ABBClient.Reports.StockoutDoctypeReport();
                        break;

                    case Constz.Report.SaleSummaryBillReport:
                        reportObj = new ABBClient.Reports.SaleSummaryBillReport();
                        break;

                    case Constz.Report.ProductReturnSummaryReport:
                        reportObj = new ABBClient.Reports.ProductReturnSummaryReport();
                        break;

                    case Constz.Report.ProductSaleSummaryReport:
                        reportObj = new ABBClient.Reports.ProductSaleSummaryReport();
                        break;

                    case Constz.Report.Support:
                        reportObj = new ABBClient.Reports.Support();
                        break;

                    case Constz.Report.ReturnTester :
                        reportObj = new ABBClient.Reports.ReturnTester();
                        break;

                    case Constz.Report.StockInReturn :
                        reportObj = new ABBClient.Reports.StockinReturn();
                        break;

                    case Constz.Report.ProductStockInShop :
                        reportObj = new ABBClient.Reports.ProductStockInShop();
                        break;

                }

                if (reportObj != null)
                {
                    reportObj.Database.Tables[0].ApplyLogOnInfo(logonInfo);

                    for (int i = 0; i < paramData.Count; ++i)
                    {
                        ReportParameterData data = (ReportParameterData)paramData[i];
                        paramValue.Value = data.PARAMETERVALUE;
                        curValue = reportObj.ParameterFields[data.PARAMETERNAME].CurrentValues;
                        curValue.Add(paramValue);
                        reportObj.ParameterFields[data.PARAMETERNAME].CurrentValues = curValue;
                    }

                    this.ctlReportViewer.ReportSource = reportObj;
                    this.ctlReportViewer.Zoom(80);
                    this.ctlReportViewer.Show();
                }
            }
            catch (Exception ex)
            {
                Appz.OpenErrorDialog(ex.Message);
                this.Close();
            }
        }

        private void PreviewReport_Load(object sender, EventArgs e)
        {

        }
    }
}