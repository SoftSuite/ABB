using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow.Sales;
using ABB.Data.Sales;
using ABBClient;

namespace ABBClient.Master
{
    public partial class ControlStock : Form
    {
        private static string _BARCODE = "";

        public static string BARCODE
        {
            set { _BARCODE = value; }
        }

        public ControlStock()
        {
            InitializeComponent();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvControlStock, false, false, true);
        }

        private void ControlStock_Load(object sender, EventArgs e)
        {
            FormatGridView();
            LoadData();
        }

        private void LoadData()
        {
            this.grvControlStock.DataSource = ControlStockFlow.GetStockList(Appz.CurrentUserData.Warehouse);
            txtPLoid.Visible = false;
            txtPMLoid.Visible = false;
            txtWHLoid.Visible = false;
            txtWareHouse.Enabled = false;
            btnSearchProduct.Enabled = false;
        }

        private void grvControlStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                txtProductDetail.Text = grvControlStock.Rows[e.RowIndex].Cells["NAME"].Value.ToString();
                txtCode.Text = grvControlStock.Rows[e.RowIndex].Cells["BARCODEE"].Value.ToString();
                txtCode.Enabled = false;
                txtStandard.Text = grvControlStock.Rows[e.RowIndex].Cells["STANDARD"].Value.ToString();
                txtMinimum.Text = grvControlStock.Rows[e.RowIndex].Cells["MINIMUM"].Value.ToString();
                txtMaximum.Text = grvControlStock.Rows[e.RowIndex].Cells["MAXIMUM"].Value.ToString();
                txtPMLoid.Text = grvControlStock.Rows[e.RowIndex].Cells["PMLOID"].Value.ToString();
                txtWHLoid.Text = grvControlStock.Rows[e.RowIndex].Cells["WHLOID"].Value.ToString();
                txtWareHouse.Text = grvControlStock.Rows[e.RowIndex].Cells["WAREHOUSE"].Value.ToString();
                txtPLoid.Text = grvControlStock.Rows[e.RowIndex].Cells["PRODUCT"].Value.ToString();

                GetUnit();


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool ret = true;

            //Insert ข้อมูล
            //Check รหัสสินค้า
            if (txtCode.Text == "")
            {
                Appz.OpenWarningDialog("กรุณากรอกรหัสสินค้า");
            }
            //Check ชื่อสินค้า
            if (txtProductDetail.Text == "")
            {
                Appz.OpenWarningDialog("กรุณากรอกชื่อสินค้า");
            }
            //Check ปริมาณคงที่
            if (txtStandard.Text == "")
            {
                Appz.OpenWarningDialog("กรุณากรอกปริมาณคงที่");
                return;
            }
            //Check ปริมาณต่ำสุด
            if (txtMinimum.Text == "")
            {
                Appz.OpenWarningDialog("กรุณากรอกปริมาณต่ำสุด");
                return;
            }
            //Check ปริมาณสูงสุด
            if (txtMaximum.Text == "")
            {
                Appz.OpenWarningDialog("กรุณากรอกปริมาณสูงสุด");
                return;
            }

            // Check ปริมาณต่ำสุด < ปริมาณคงที่ < ปริมาณสูงสุด 
            if ((Convert.ToDouble(txtMinimum.Text) > Convert.ToDouble(txtStandard.Text)) || (Convert.ToDouble(txtMinimum.Text) > Convert.ToDouble(txtMaximum.Text)) || (Convert.ToDouble(txtStandard.Text) > Convert.ToDouble(txtMaximum.Text)))
            {
                Appz.OpenWarningDialog("ไม่สามารถทำรายการได้ กรุณาระบุ  ปริมาณต่ำสุด < ปริมาณคงที่ < ปริมาณสูงสุด");
                return;
            }

            //Check จำนวนที่กรอกในช่องปริมาณสูงสุดต้องน้อยกว่า 100,000,000.00
            if (Convert.ToDouble(txtMaximum.Text) > 10000000.00)
            {
                Appz.OpenWarningDialog("กรุณาระบุจำนวนตั้งแต่ 0.00 – 9,999,999,999.99 ");
                return;
            }

            //Check จำนวนที่กรอกในช่องปริมาณต่ำสุดต้องน้อยกว่า 100,000,000.00
            if (Convert.ToDouble(txtMinimum.Text) > 10000000.00)
            {
                Appz.OpenWarningDialog("กรุณาระบุจำนวนตั้งแต่ 0.00 – 9,999,999,999.99 ");
                return;
            }


            //Check จำนวนที่กรอกในช่องปริมาณคงที่ต้องน้อยกว่า 100,000,000.00
            if (Convert.ToDouble(txtStandard.Text) > 10000000.00)
            {
                Appz.OpenWarningDialog("กรุณาระบุจำนวนตั้งแต่ 0.00 – 9,999,999,999.99 ");
                return;
            }

            btnSearchProduct.Enabled = false;
            //update ข้อมูล
            if (!this.txtCode.Enabled)
            {
                ControlStockData CsData = new ControlStockData();
                ControlStockFlow csFlow = new ControlStockFlow();

                CsData.LOID = Convert.ToDouble(txtPMLoid.Text.Trim());
                CsData.STANDARD = txtStandard.Text;
                CsData.MINIMUM = txtMinimum.Text;
                CsData.MAXIMUM = txtMaximum.Text;
                CsData.WAREHOUSE = Convert.ToDouble(this.txtWHLoid.Text);
                ret = csFlow.UpdateData(txtPMLoid.Text.Trim(), CsData);

                if (!ret)
                    Appz.OpenErrorDialog(csFlow.ErrorMessage);
                else
                {
                    Appz.OpenInformationDialog("บันทึกข้อมูลเรียบร้อย");
                    LoadData();
                }
            }
            else
            {
                ControlStockData CsData = new ControlStockData();
                ControlStockFlow csFlow = new ControlStockFlow();

                CsData.PRODUCT = Convert.ToDouble(txtPLoid.Text);
                CsData.WAREHOUSE = Convert.ToDouble(txtWHLoid.Text);
                CsData.STANDARD = txtStandard.Text;
                CsData.MINIMUM = txtMinimum.Text;
                CsData.MAXIMUM = txtMaximum.Text;
                ret = csFlow.InsertData(Appz.CurrentUserData.UserID.ToString(), CsData);

                if (!ret)
                    Appz.OpenErrorDialog(csFlow.ErrorMessage);
                else
                {
                    txtPMLoid.Text = csFlow.GetLoid.ToString();
                    txtCode.Enabled = false;
                    Appz.OpenInformationDialog("บันทึกข้อมูลเรียบร้อย");
                    LoadData();
                }
                   
             }

        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            btnSearchProduct.Enabled  = true;
            txtCode.Text = "";
            txtCode.Enabled = true;
            txtProductDetail.Text = "";
            txtStandard.Text = "";
            txtMaximum.Text = "";
            txtMinimum.Text = "";
            txtUnit.Text = "";
            txtUnitMaster.Text = "";
            txtUnitMax.Text = "";
            txtUnitMin.Text = "";
            txtUnitStd.Text = "";
            txtWHLoid.Text = Appz.CurrentUserData.Warehouse.ToString();
            string name = ControlStockFlow.GetWarehouseName(txtWHLoid.Text.Trim());
            txtWareHouse.Text  = name;   
        }

        private void GetUnit()
        {
            DataTable dt = ControlStockFlow.GetProduct(Convert.ToDouble(txtPLoid.Text));
            if (dt.Rows.Count > 0)
            {
                txtUnit.Text = dt.Rows[0]["UNIT"].ToString();
                txtUnitMaster.Text = dt.Rows[0]["UNITMASTER"].ToString();
                txtUnitStd.Text = dt.Rows[0]["UNITMASTER"].ToString();
                txtUnitMin.Text = dt.Rows[0]["UNITMASTER"].ToString();
                txtUnitMax.Text = dt.Rows[0]["UNITMASTER"].ToString();
            }

        }

        private void GetMinMax()
        {
            DataTable dt = ControlStockFlow.GetMinMax(Convert.ToDouble(txtPLoid.Text), Convert.ToDouble(this.txtWHLoid.Text));
            if (dt.Rows.Count > 0)
            {
                txtMaximum.Text = dt.Rows[0]["MAXIMUM"].ToString();
                txtMinimum.Text = dt.Rows[0]["MINIMUM"].ToString();
                txtStandard.Text = dt.Rows[0]["STANDARD"].ToString();
                txtPMLoid.Text = dt.Rows[0]["LOID"].ToString();
                this.txtCode.Enabled = false;
            }
            else
                this.txtCode.Enabled = true;

        }

        private void GetProductDetail()
        {
            DataTable dt = ControlStockFlow.GetProduct(Convert.ToDouble(txtPLoid.Text));
            if (dt.Rows.Count > 0)
            {
                txtCode.Text = dt.Rows[0]["BARCODE"].ToString();
                txtProductDetail.Text = dt.Rows[0]["NAME"].ToString();
                txtUnit.Text = dt.Rows[0]["UNIT"].ToString();
                txtUnitMaster.Text = dt.Rows[0]["UNITMASTER"].ToString();
                txtUnitStd.Text = dt.Rows[0]["UNITMASTER"].ToString();
                txtUnitMin.Text = dt.Rows[0]["UNITMASTER"].ToString();
                txtUnitMax.Text = dt.Rows[0]["UNITMASTER"].ToString();
            }
            else
            {
                txtCode.Text = "";
                txtProductDetail.Text = "";
                txtUnit.Text = "0";
                txtUnitMaster.Text = "0";
                txtUnitStd.Text = "";
                txtUnitMin.Text = "";
                txtUnitMax.Text = "";
                if (this.txtCode.Text != "") Appz.OpenErrorDialog("กรุณากรอกข้อมูลใหม่");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Appz.OpenQuestionDialog("ต้องการยกเลิกรายการใช่หรือไม่?") == DialogResult.OK)
            {
                txtWareHouse.Text = "";
                txtCode.Text = "";
                txtCode.Enabled = true;
                txtProductDetail.Text = "";
                txtStandard.Text = "";
                txtMaximum.Text = "";
                txtMinimum.Text = "";
                txtPLoid.Text = "";
                txtPMLoid.Text = "";
                txtWHLoid.Text = "";
                txtUnit.Text = "";
                txtUnitMaster.Text = "";
                txtUnitMax.Text = "";
                txtUnitMin.Text = "";
                txtUnitStd.Text = "";
               
                LoadData();
            }
            btnSearchProduct.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Appz.OpenQuestionDialog("ต้องการลบข้อมูลหรือไม่?") == DialogResult.OK)
            {
                bool ret = true;
                ControlStockData CsData = new ControlStockData();
                ControlStockFlow csFlow = new ControlStockFlow();
                CsData.LOID = Convert.ToDouble(txtPMLoid.Text);
                ret = csFlow.DeleteData("test", CsData);

                if (!ret)
                    MessageBox.Show(csFlow.ErrorMessage);
                else
                    LoadData();
            }
            btnSearchProduct.Enabled = false;
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            Search.ProductMasterPopup frmProductSearch = new Search.ProductMasterPopup(this.txtCode.Text.Trim(), Appz.CurrentUserData.Warehouse);
            ABB.Data.Search.SearchProductData data = new ABB.Data.Search.SearchProductData();
            data.CODE = this.txtCode.Text.Trim();
            frmProductSearch.SetData(data);
            frmProductSearch.ShowDialog(this);
            if (frmProductSearch.DialogResult == DialogResult.OK)
            {
                this.txtPLoid.Text = frmProductSearch.ProductID.ToString();
                GetProductDetail();
                GetMinMax();
            } 
        }

        private void txtStandard_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetDBlTextBox(sender, e);
        }

        private void txtMinimum_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetDBlTextBox(sender, e);
        }

        private void txtMaximum_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetDBlTextBox(sender, e);
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                this.btnSearchProduct.Focus();
            }

            //if (e.KeyCode == Keys.Back)
            //{
            //    txtProductDetail.Text = "";
            //    txtPLoid.Text = "";
            //}
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            this.txtPLoid.Text = ControlStockFlow.GetProductLoid(this.txtCode.Text.Trim());
            GetProductDetail();
        }

    

    }
}