using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow.Sales;
using System.Collections;
using ABB.Data.Sales;

namespace ABBClient.Master
{
    public partial class ProductStockInShopSearch : Form
    {
        public ProductStockInShopSearch()
        {
            InitializeComponent();
        }
        string str_DateFrom;
        string str_DateTo;

        
        private void ProductStockInShopSearch_Load(object sender, EventArgs e)
        {
            
            this.grvPDSearchInShop.EnableHeadersVisualStyles = false;
            this.grvPDSearchInShop.AlternatingRowsDefaultCellStyle = Appz.AlternateRowStyle;
            this.grvPDSearchInShop.ColumnHeadersDefaultCellStyle = Appz.HeaderStyle;
            DateFromPk.Value = DateTime.Today;
            DateToPk.Value = DateTime.Today;
            LoadData();
        }

        private void LoadData()
        {
            FormatGridView();
            SetDate();
            this.grvPDSearchInShop.DataSource = ProductStockInShopFlow.GetSearchProductStockInShop(Appz.CurrentUserData.Warehouse.ToString(), txtSICode.Text.ToString(), str_DateFrom, str_DateTo, txtRQCode.Text.ToString());
           
        }
        private void SetDate()
        {
            str_DateFrom = getDateFrom();
            str_DateTo = getDateTo();
        }


        private void btnSearchPDStockInShop_Click(object sender, EventArgs e)
        {
            SetDate();
            ArrayList arr = ProductStockInShopFlow.GetSearchProductStockInShop(Appz.CurrentUserData.Warehouse.ToString(), txtSICode.Text.ToString(), str_DateFrom, str_DateTo, txtRQCode.Text.ToString());
            this.grvPDSearchInShop.DataSource = arr;

            if (arr.Count > 0)
            {
                this.grvPDSearchInShop.Columns["CHKAPPROVE"].HeaderText = "เลือก";
                this.grvPDSearchInShop.Columns["ORDERNO"].HeaderText = "ลำดับ";
                this.grvPDSearchInShop.Columns["SICODE"].HeaderText = "เลขที่";
                this.grvPDSearchInShop.Columns["RECEIVEDATE"].HeaderText = "วันที่";
                this.grvPDSearchInShop.Columns["RQCODE"].HeaderText = "เลขที่รับคำสั่งซื้อ";
                this.grvPDSearchInShop.Columns["REQDATE"].HeaderText = "วันที่รับคำสั่งซื้อ";
                this.grvPDSearchInShop.Columns["TOTAL"].HeaderText = "ยอดสุทธิ";
                this.grvPDSearchInShop.Columns["CHKAPPROVE"].DisplayIndex = 0;
                this.grvPDSearchInShop.Columns["ORDERNO"].DisplayIndex = 1;
                this.grvPDSearchInShop.Columns["SICODE"].DisplayIndex = 2;
                this.grvPDSearchInShop.Columns["RECEIVEDATE"].DisplayIndex = 3;
                this.grvPDSearchInShop.Columns["RQCODE"].DisplayIndex = 4;
                this.grvPDSearchInShop.Columns["REQDATE"].DisplayIndex = 5;
                this.grvPDSearchInShop.Columns["TOTAL"].DisplayIndex = 6;

            }
        }

        private string getDateFrom()
        {
            string str;
            str = "";
            str = Convert.ToString(DateFromPk.Value.Day) + '/';
            str += Convert.ToString(DateFromPk.Value.Month) + '/';
            str += Convert.ToString(DateFromPk.Value.Year);
            return str;
        }

        private string getDateTo()
        {
            string str;
            str = "";
            str = Convert.ToString(DateToPk.Value.Day) + '/';
            str += Convert.ToString(DateToPk.Value.Month) + '/';
            str += Convert.ToString(DateToPk.Value.Year);
            return str;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            bool ret = true;

            for (int i = 0; i < grvPDSearchInShop.Rows.Count  ; i++)
            {

                DataGridViewTextBoxCell SISTATUS = (DataGridViewTextBoxCell)grvPDSearchInShop.Rows[i].Cells["SISTATUS"];
                if (SISTATUS.Value.ToString() == "AP")
                {
                    Appz.OpenWarningDialog("รายการนี้ถูกยืนยันแล้ว");
                    return;
                }                
            }
            if (Appz.OpenQuestionDialog("ต้องการยืนยันรับเข้าคลังหรือไม่?") == DialogResult.OK)
            {
                ArrayList arrLOID = new ArrayList();
                for (int i = 0; i < grvPDSearchInShop.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell maCell = (DataGridViewCheckBoxCell)this.grvPDSearchInShop.Rows[i].Cells["CHKAPPROVE"];

                    if (maCell.FormattedValue.Equals(true))
                    {
                        arrLOID.Add(grvPDSearchInShop.Rows[i].Cells["LOID"].Value.ToString());                            
                    }
                }
                ProductStockInShopFlow csFlow = new ProductStockInShopFlow();
                ret = csFlow.UpdateStockInData(Appz.CurrentUserData.UserID, arrLOID);

                if (ret == true)
                    Appz.OpenInformationDialog("บันทึกรายการเรียบร้อย");
                else
                    Appz.OpenWarningDialog(csFlow.ErrorMessage);
                LoadData();
            }
        }

        private void grvPDSearchInShop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvPDSearchInShop.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().ToString() == "System.Windows.Forms.DataGridViewCheckBoxCell")
            {
                DataGridViewCheckBoxCell maCell = new DataGridViewCheckBoxCell();
                maCell = (DataGridViewCheckBoxCell)grvPDSearchInShop.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (maCell.FormattedValue.Equals(true))
                {
                    maCell.Value = false;
                }
                else
                {
                    maCell.Value = true;
                }
            }          
        }

        private void FormatGridView()
        {
            this.grvPDSearchInShop.AllowUserToAddRows = false;
            this.grvPDSearchInShop.AllowUserToDeleteRows = false;
            this.grvPDSearchInShop.AutoGenerateColumns = false;
            this.grvPDSearchInShop.AllowUserToOrderColumns = true;
            this.grvPDSearchInShop.AllowUserToResizeColumns = true;
            this.grvPDSearchInShop.AllowUserToResizeRows = true;
            this.grvPDSearchInShop.BackgroundColor = System.Drawing.Color.Silver;
            this.grvPDSearchInShop.ColumnHeadersDefaultCellStyle = Appz.HeaderStyle;
            this.grvPDSearchInShop.AlternatingRowsDefaultCellStyle = Appz.AlternateRowStyle;
            this.grvPDSearchInShop.EnableHeadersVisualStyles = false;
            this.grvPDSearchInShop.MultiSelect = false;
            this.grvPDSearchInShop.RowHeadersWidth = 21;
            this.grvPDSearchInShop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void grvPDSearchInShop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.txtSiLoid.Text = grvPDSearchInShop.Rows[e.RowIndex].Cells["LOID"].Value.ToString();
                if (grvPDSearchInShop.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().ToString() == "System.Windows.Forms.DataGridViewLinkCell")
                {
                    double SILOID = Convert.ToDouble(grvPDSearchInShop.Rows[e.RowIndex].Cells[1].Value);
                    Openfrm(SILOID, "EDIT");
                    StockInData data = new StockInData();
                }
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (Appz.OpenQuestionDialog("ต้องการสร้างรายการใหม่ใช่หรือไม่") == DialogResult.OK )
            {
                ProductStockInShopFlow csFlow = new ProductStockInShopFlow();
                StockInData csData = new StockInData();
                csData.SENDER = Appz.CurrentUserData.Warehouse;
                double   loid ;

                //insert STockin
                loid = csFlow.InsertStockIn(Appz.CurrentUserData.UserID.ToString(),csData );
                if (loid == 0)
                    MessageBox.Show(csFlow.ErrorMessage);
                else
                    Openfrm(loid,"ADD");

                    LoadData();
            }
        }
        private void Openfrm(double loid,string str)
        {
            ProductStockInShop frmProductStockInShop = new ProductStockInShop(loid,str);
            frmProductStockInShop.ShowDialog(this);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool ret = true;
            if (Appz.OpenQuestionDialog("ต้องการลบรายการใช่หรือไม่?") == DialogResult.OK)
            {
                ArrayList arrLOID = new ArrayList();
                for (int i = 0; i < grvPDSearchInShop.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell maCell = (DataGridViewCheckBoxCell)this.grvPDSearchInShop.Rows[i].Cells["CHKAPPROVE"];

                    if (maCell.FormattedValue.Equals(true))
                    {
                        arrLOID.Add(grvPDSearchInShop.Rows[i].Cells["LOID"].Value.ToString());
                    }
                }
                ProductStockInShopFlow csFlow = new ProductStockInShopFlow();
                ret = csFlow.DeleteStockIn_StockInitemData(Appz.CurrentUserData.UserID, arrLOID);

                if (ret == true)
                    Appz.OpenInformationDialog("ลบรายการเรียบร้อย");
                else
                    Appz.OpenWarningDialog(csFlow.ErrorMessage);
                LoadData();
            }
        }
    }
}