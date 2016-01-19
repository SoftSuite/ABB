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
    public partial class ProductPopup : Form
    {
        public ProductPopup()
        {
            InitializeComponent();
        }

        public ProductPopup(string barcode, double warehouse)
        {
            InitializeComponent();
            this.txtCode.Text = barcode;
            this.txtWarehouse.Text = warehouse.ToString();
        }

        private double _LOID = 0;
        private SearchFlow _flow;

        public SearchFlow FlowObj
        {
            get { if (_flow == null) _flow = new SearchFlow(); return _flow; }
        }

        public double ProductID
        {
            get { return _LOID; }
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvProduct, false, false, true);
        }

        public void SetData(SearchProductData data)
        {
            this.txtCode.Text = data.CODE;
            this.txtName.Text = data.NAME;
            this.cmbProductGroup.SelectedValue = data.PRODUCTGROUP.ToString();
            this.cmbProductType.SelectedValue = data.PRODUCTTYPE.ToString();
            this.txtWarehouse.Text = data.WAREHOUSE.ToString();
        }

        private SearchProductData GetData()
        {
            SearchProductData data = new SearchProductData();
            data.CODE = this.txtCode.Text.Trim();
            data.NAME = this.txtName.Text.Trim();
            data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedValue);
            data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedValue);
            data.WAREHOUSE = Convert.ToDouble(this.txtWarehouse.Text == "" ? "0" : this.txtWarehouse.Text);
            data.TYPE = Constz.ProductType.Type.FG.Code;
            return data;
        }

        private void SearchData()
        {
            this.grvProduct.DataSource = FlowObj.GetCtlProductList(GetData());
            this.btnSelect.Enabled = (this.grvProduct.Rows.Count > 0);
        }

        private void SelectProduct()
        {
            if (this.grvProduct.Rows.Count > 0)
            {
                _LOID = Convert.ToDouble(this.grvProduct.CurrentRow.Cells["LOID"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectProduct();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ProductPopup_Load(object sender, EventArgs e)
        {
            Appz.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "'", "เลือก", "0");
            Appz.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND TYPE ='" + Constz.ProductType.Type.FG.Code + "' ", "เลือก", "0");
            FormatGridView();
            SearchData();
        }

        private void grvProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectProduct();
        }
    }
}