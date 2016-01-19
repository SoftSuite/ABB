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
    public partial class RepProductSaleSummary : Form
    {
        private int firstloadflag = 0;
        public RepProductSaleSummary()
        {
            InitializeComponent();
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

            repData1.PARAMETERNAME = "PRODUCTTYPE";
            repData1.PARAMETERVALUE = cmbProductType.SelectedValue.ToString();
            arr.Add(repData1);

            repData2.PARAMETERNAME = "PRODUCTGROUP";
            repData2.PARAMETERVALUE = cmbProductGroup.SelectedValue.ToString();
            arr.Add(repData2);

            repData3.PARAMETERNAME = "PRODUCT";
            repData3.PARAMETERVALUE = cmbProduct.SelectedValue.ToString();
            arr.Add(repData3);

            repData4.PARAMETERNAME = "DATEFROM";
            repData4.PARAMETERVALUE = dpDateFrom.Value.ToString("dd/MM/") + dpDateFrom.Value.Year.ToString();
            arr.Add(repData4);

            repData5.PARAMETERNAME = "DATETO";
            repData5.PARAMETERVALUE = dpDateTo.Value.ToString("dd/MM/") + dpDateTo.Value.Year.ToString();
            arr.Add(repData5);

            repData6.PARAMETERNAME = "WAREHOUSE";
            repData6.PARAMETERVALUE = Appz.CurrentUserData.Warehouse.ToString();
            arr.Add(repData6);

            repData7.PARAMETERNAME = "CUSTOMER";
            repData7.PARAMETERVALUE = cmbCustomer.SelectedValue.ToString();
            arr.Add(repData7);

            Reports.PreviewReport frmPreviewReport = new Reports.PreviewReport("ProductSaleSummaryReport", arr);
            frmPreviewReport.ShowDialog(this);
        }

        private void RepProductSaleSummary_Load(object sender, EventArgs e)
        {
            firstloadflag = 1;
            LoadCombo();
            firstloadflag = 0;
        }

        private void LoadCombo()
        {
            Appz.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND TYPE ='" + Constz.ProductType.Type.FG.Code + "' ", "ทั้งหมด", "0");
            Appz.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND PRODUCTTYPE = " + (cmbProductType.SelectedValue == "0" ? 0 : Convert.ToDouble(cmbProductType.SelectedValue)) + " ", "ทั้งหมด", "0");
            Appz.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "PRODUCTGROUP = " + (cmbProductGroup.SelectedValue.ToString() == "0" ? 0 : Convert.ToDouble(cmbProductGroup.SelectedValue)) + " ", "ทั้งหมด", "0");
            Appz.BuildCombo(this.cmbCustomer, "V_CUSTOMER", "CUSTOMERNAME", "LOID", "CUSTOMERNAME", "1=1 ", "ทั้งหมด", "0");
        }

        private void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstloadflag == 0)
                Appz.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND PRODUCTTYPE = " + (cmbProductType.SelectedValue == "0" ? 0 : Convert.ToDouble(cmbProductType.SelectedValue)) + " ", "ทั้งหมด", "0");
        }

        private void cmbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstloadflag == 0)
                Appz.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "PRODUCTGROUP = " + (cmbProductGroup.SelectedValue.ToString() == "0" ? 0 : Convert.ToDouble(cmbProductGroup.SelectedValue)) + " ", "ทั้งหมด", "0");
        }

    }
}