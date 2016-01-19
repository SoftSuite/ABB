using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow.Sales;
using ABB.Data.Sales;

namespace ABBClient.Search
{
    public partial class ReturnPopup : Form
    {
        private StockInReturnFlow _flow;

        private StockInReturnFlow FlowObj
        {
            get { if (_flow == null) { _flow = new StockInReturnFlow(); } return _flow; }
        }

        public double RequisitionID
        {
            get { return Convert.ToDouble(this.txtRequisition.Text == "" ? "0" : this.txtRequisition.Text); }
        }

        public ReturnPopup()
        {
            InitializeComponent();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvRequisition, false, false, true);
        }

        private void ReturnPopup_Load(object sender, EventArgs e)
        {
            Appz.BuildCombo(this.cmbProduct, "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "ACTIVE = 1","ทั้งหมด","0");
            FormatGridView();
            SearchData();
        }

        private ABB.Data.Search.InvoiceForReturnSearchData GetSearchData()
        {
            ABB.Data.Search.InvoiceForReturnSearchData data = new ABB.Data.Search.InvoiceForReturnSearchData();
            data.CODEFROM = this.txtCodeFrom.Text.Trim();
            data.CODETO = this.txtCodeTo.Text.Trim();
            data.CUSTOMERCODE = this.txtCustomerCode.Text.Trim();
            data.DATEFROM = this.dtpDateFrom.Value.Date;
            data.DATETO = this.dtpDateTo.Value.Date;
            data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedValue);
            return data;
        }

        private void SearchData()
        {
            this.grvRequisition.DataSource = FlowObj.GetInvoiceList(GetSearchData());
            if (this.grvRequisition.Rows.Count > 0)
            {
                this.grvRequisition[0, 0].Selected = true;
                this.txtRequisition.Text = this.grvRequisition[6, 0].Value.ToString();
            }
            else
                this.txtRequisition.Text = "0";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (RequisitionID == 0)
                Appz.OpenErrorDialog("กรุณาเลือกเลขที่รับคำสั่งซื้อ");
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvRequisition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.txtRequisition.Text = grvRequisition.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
        }

        private void grvRequisition_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.txtRequisition.Text = grvRequisition.Rows[e.RowIndex].Cells[6].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}