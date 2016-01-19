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
    public partial class ProductStockInShopPopup : Form
    {
        private StockInShopFlow _flow;

        private StockInShopFlow FlowObj
        {
            get { if (_flow == null) { _flow = new StockInShopFlow(); } return _flow; }
        }

        public double StockOutID
        {
            get { return Convert.ToDouble(this.txtStockOut.Text == "" ? "0" : this.txtStockOut.Text); }
        }

        public ProductStockInShopPopup()
        {
            InitializeComponent();
        }

        public ProductStockInShopPopup(double currentStockIn)
        {
            InitializeComponent();
            this.txtStockIn.Text = currentStockIn.ToString();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvProductStockInShop, false, false, true);
        }

        private void ProductStockInShopPopup_Load(object sender, EventArgs e)
        {
            FormatGridView();
            SearchData();
        }

        private ABB.Data.Search.StockOutProductSearchData GetSearchData()
        {
            ABB.Data.Search.StockOutProductSearchData data = new ABB.Data.Search.StockOutProductSearchData();
            data.REQUISITIONCODEFROM = this.txtCodeFrom.Text.Trim();
            data.REQUISITIONCODETO = this.txtCodeTo.Text.Trim();
            data.RESERVEDATEFROM = this.dtpDateFrom.Value.Date;
            data.RESERVEDATETO = this.dtpDateTo.Value.Date;
            return data;
        }

        private void SearchData()
        {
            this.grvProductStockInShop.DataSource = FlowObj.GetStockOutList(GetSearchData(), Convert.ToDouble(this.txtStockIn.Text == "" ? "0" : this.txtStockIn.Text));
            if (this.grvProductStockInShop.Rows.Count > 0)
            {
                this.grvProductStockInShop[0, 0].Selected = true;
                this.txtStockOut.Text = this.grvProductStockInShop[1, 0].Value.ToString();
            }
            else
                this.txtStockOut.Text = "0";
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (StockOutID == 0)
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

        private void grvProductStockInShop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {
                this.txtStockOut.Text = grvProductStockInShop.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void grvProductStockInShop_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.txtStockOut.Text = grvProductStockInShop.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}