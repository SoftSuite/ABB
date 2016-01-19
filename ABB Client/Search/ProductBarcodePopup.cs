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
    public partial class ProductBarcodePopup : Form
    {
        public ProductBarcodePopup()
        {
            InitializeComponent();
        }

        public ProductBarcodePopup(string barcode, double warehouse)
        {
            InitializeComponent();
            this.txtCode.Text = barcode;
            this.txtWarehouse.Text = warehouse.ToString();
        }

        public ProductBarcodePopup(string barcode, double warehouse, double zone)
        {
            InitializeComponent();
            this.txtCode.Text = barcode;
            this.txtWarehouse.Text = warehouse.ToString();
            this.txtZone.Text = zone.ToString();
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
            this.txtZone.Text = data.ZONE.ToString();
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
            data.ZONE = Convert.ToDouble(this.txtZone.Text == "" ? "0" : this.txtZone.Text);
            return data;
        }

        private void SearchData()
        {
            this.grvProduct.DataSource = FlowObj.GetProductList(GetData());
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

        private void SetProductGroup()
        {
            double productType = 0;
            if (this.cmbProductType.SelectedValue.GetType() != typeof(DataRowView)) productType = Convert.ToDouble(this.cmbProductType.SelectedValue);
            this.cmbProductGroup.DataSource = null;
            Appz.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND PRODUCTTYPE = " + productType.ToString() + " ", "เลือก", "0");
        }

        private void ProductBarcodePopup_Load(object sender, EventArgs e)
        {
            Appz.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND TYPE ='" + Constz.ProductType.Type.FG.Code + "' ", "เลือก", "0");
            //SetProductGroup();
            FormatGridView();
            SearchData();
        }

        private void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetProductGroup();
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void grvProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectProduct();
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

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13)) SearchData();
        }

    }
}