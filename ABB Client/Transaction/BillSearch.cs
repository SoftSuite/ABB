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

namespace ABBClient.Transaction
{
    public partial class BillSearch : Form
    {
        private SearchFlow _flow;
        private Bill _billForm;

        public Bill BillForm
        {
            get { if (_billForm == null) { _billForm = new Bill(); } return _billForm; }
        }

        public SearchFlow FlowObj
        {
            get { if (_flow == null) _flow = new SearchFlow(); return _flow; }
        }

        public BillSearch()
        {
            InitializeComponent();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvRequisition, false, false, true);
        }

        private SearchSaleData GetData()
        {
            SearchSaleData data = new SearchSaleData();
            data.REQDATEFROM = dtpDateFrom.Value;
            data.REQDATETO = dtpDateTo.Value;
            data.CODEFROM = this.txtCodeFrom.Text.Trim();
            data.CODETO = this.txtCodeTo.Text.Trim();
            data.CUSTOMER = Convert.ToDouble(this.cmbCustomer.SelectedValue);
            data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedValue);
            return data;
        }

        private void SearchData()
        {
            this.grvRequisition.DataSource = FlowObj.GetSaleList(GetData());
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void BillSearch_Load(object sender, EventArgs e)
        {
            Appz.BuildComboDistinct(this.cmbProduct, "V_PRODUCT_LIST_REQUISITION", "PNAME", "LOID", "PNAME","ACTIVE = '1'AND WAREHOUSE =" + Appz.CurrentUserData.Warehouse +" ", "เลือก", "0");
            Appz.BuildComboDistinct(this.cmbCustomer, "V_CUSTOMER", "CUSTOMERNAME", "LOID", "CUSTOMERNAME", "", "ทั้งหมด", "0");
            FormatGridView();
            SearchData();
        }

        private void grvRequisition_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _billForm = new Bill(Convert.ToDouble(this.grvRequisition[8, e.RowIndex].Value));
                _billForm.MdiParent = this.MdiParent;
                _billForm.Show();
            }
        }

    }
}