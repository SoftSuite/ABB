using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow.Sales;
using System.Collections;
using ABB.Global;

namespace ABBClient.Master
{
    public partial class ProductSearch : Form
    {       
        public ProductSearch()
        {
            InitializeComponent();
        }

        private void ProductSearch_Load(object sender, EventArgs e)
        {
            this.grvProductSearch.EnableHeadersVisualStyles = false;
            this.grvProductSearch.AlternatingRowsDefaultCellStyle = Appz.AlternateRowStyle;
            this.grvProductSearch.ColumnHeadersDefaultCellStyle = Appz.HeaderStyle;
            txtTmp.Visible = false;
            LoadData();
        }

        private void LoadData()
        {
            Appz.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME","ACTIVE = '1'");
            Appz.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '1'");
            this.grvProductSearch.DataSource = ProductFlow.GetSearchProduct(cmbProductType.SelectedValue.ToString(), cmbProductGroup.SelectedValue.ToString(), "", "",Authz.CurrentUserInfo.Warehouse.ToString());

        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            ArrayList arr = ProductFlow.GetSearchProduct(cmbProductType.SelectedValue.ToString(), cmbProductGroup.SelectedValue.ToString(), txtBarcode.Text.ToString(), txtProductName.Text.ToString(), Authz.CurrentUserInfo.Warehouse.ToString());
      
            if (arr.Count > 0)
            {
                this.grvProductSearch.DataSource = arr;
                this.grvProductSearch.Columns["ORDERNO"].HeaderText = "ลำดับ";
                this.grvProductSearch.Columns["BARCODE"].HeaderText = "บาร์โค้ด";
                this.grvProductSearch.Columns["PNAME"].HeaderText = "ชื่อสินค้า";
                this.grvProductSearch.Columns["ORDERNO"].DisplayIndex = 0;
                this.grvProductSearch.Columns["BARCODE"].DisplayIndex = 1;
                this.grvProductSearch.Columns["PNAME"].DisplayIndex = 2;
                
            }
            else
            {
                this.grvProductSearch.DataSource = arr;
                this.grvProductSearch.Columns["ORDERNO"].HeaderText = "ลำดับ";
                this.grvProductSearch.Columns["BARCODE"].HeaderText = "บาร์โค้ด";
                this.grvProductSearch.Columns["PNAME"].HeaderText = "ชื่อสินค้า";
            }

        }

        private void grvProductSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                txtTmp.Text = grvProductSearch.Rows[e.RowIndex].Cells["BARCODE"].Value.ToString();
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            ABBClient.Master.ControlStock.BARCODE = txtTmp.Text.Trim() ;
            this.Close();
        }
    }
}