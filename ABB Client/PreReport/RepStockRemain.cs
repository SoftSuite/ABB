using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Data;
using System.Collections;
using ABB.Global;


namespace ABBClient.PreReport
{
    public partial class RepStockRemain : Form
    {
        private int firstloadflag = 0;
        public RepStockRemain()
        {
            InitializeComponent();
        }

        private void RepStockRemain_Load(object sender, EventArgs e)
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            ReportParameterData repData1 = new ReportParameterData();
            ReportParameterData repData2 = new ReportParameterData();
            ReportParameterData repData3 = new ReportParameterData();
            ReportParameterData repData4 = new ReportParameterData();

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

            repData4.PARAMETERNAME = "WAREHOUSE";
            repData4.PARAMETERVALUE = Appz.CurrentUserData.Warehouse.ToString();
            arr.Add(repData4);

            Reports.PreviewReport frmPreviewReport = new Reports.PreviewReport("StockRemainReport", arr);
            frmPreviewReport.ShowDialog(this);
        }


    }
}