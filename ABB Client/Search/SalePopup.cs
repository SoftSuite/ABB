using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Data;
using ABB.Data.Search;
using ABB.Flow.Search;

namespace ABBClient.Search
{
    public partial class SalePopup : Form
    {
        private double _LOID = 0;
        private string _CODE = "";
        private SearchFlow _flow;

        public SearchFlow FlowObj
        {
            get { if (_flow == null) _flow = new SearchFlow(); return _flow; }
        }

        public double RequisitionID
        {
            get { return _LOID; }
        }

        public string RequisitionCode
        {
            get { return _CODE; }
        }

        public SalePopup()
        {
            InitializeComponent();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvRequisition, false, false, true);
        }

        public void SetData(SearchSaleData data)
        {
            if (data.REQDATEFROM.Year != 1)
                this.dtpDateFrom.Value = data.REQDATEFROM;
            else
                this.dtpDateFrom.Value = DateTime.Now.Date;

            if (data.REQDATETO.Year != 1)
                this.dtpDateTo.Value = data.REQDATETO;
            else
                this.dtpDateTo.Value = DateTime.Now.Date;

            this.txtCodeFrom.Text = data.CODEFROM.Trim();
            this.txtCodeTo.Text = data.CODETO.Trim();
            this.txtCustomerName.Text = data.CUSTOMERNAME.Trim();
            this.cmbProduct.SelectedValue = data.PRODUCT;
        }

        private SearchSaleData GetData()
        {
            SearchSaleData data = new SearchSaleData();
            data.REQDATEFROM = dtpDateFrom.Value;
            data.REQDATETO = dtpDateTo.Value;
            data.CODEFROM = this.txtCodeFrom.Text.Trim();
            data.CODETO = this.txtCodeTo.Text.Trim();
            data.CUSTOMERNAME = this.txtCustomerName.Text.Trim();
            data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedValue);
            data.STATUS = Constz.Requisition.Status.Void.Code;
            return data;
        }

        private void SearchData()
        {
            this.grvRequisition.DataSource = FlowObj.GetSaleList(GetData());
            this.btnSelect.Enabled = (this.grvRequisition.Rows.Count > 0);
        }

        private void SalePopup_Load(object sender, EventArgs e)
        {
            Appz.BuildCombo(this.cmbProduct, "V_PRODUCT_LIST_REQUISITION", "PNAME", "LOID", "PNAME", "", "เลือก", "0");
            FormatGridView();
            SearchData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            _LOID = Convert.ToDouble(this.grvRequisition.CurrentRow.Cells["LOID"].Value);
            _CODE = this.grvRequisition.CurrentRow.Cells["CODE"].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}