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
    public partial class ProductStockInShop : Form
    {
        private double _LOID = 0;
        private string _FLAG = "";
        private DataTable tempTable = null;
        public ProductStockInShop(double SILOID,String FLAG)
        {
            InitializeComponent();
            _LOID = SILOID;
            _FLAG = FLAG;
        }
        

        private void ProductStockInShop_Load(object sender, EventArgs e)
        {
            FormatGridView();
            this.grvProductStockInShop.EnableHeadersVisualStyles = false;
            this.grvProductStockInShop.AlternatingRowsDefaultCellStyle = Appz.AlternateRowStyle;
            this.grvProductStockInShop.ColumnHeadersDefaultCellStyle = Appz.HeaderStyle;

            // กรณี update
            if (_FLAG != "ADD")
            {
                txtChk.Text = "-";
                LoadData();
            }
            else
            {
                DataTable dtResult = new DataTable();
                dtResult = ProductStockInShopFlow.GetStockInData(_LOID.ToString());
                if (dtResult.Rows.Count > 0)
                {
                    txtRqCode.Text = "";
                    txtSiCode.Text = dtResult.Rows[0]["CODE"].ToString();
                    txtReceivedate.Text = dtResult.Rows[0]["RECEIVEDATE"].ToString();
                    txtCreateby.Text = dtResult.Rows[0]["CREATEBY"].ToString();
                    txtTotal.Text = "0";
                }
            }
        }

        private void SetStockIn()
        {
            DataTable dtResult = new DataTable();
            dtResult = ProductStockInShopFlow.GetRequisition_StockInData(Appz.CurrentUserData.Warehouse.ToString(), _LOID.ToString());
            if (dtResult.Rows.Count > 0)
            {
                txtRqCode.Text = dtResult.Rows[0]["RQCODE"].ToString();
                txtSiCode.Text = dtResult.Rows[0]["SICODE"].ToString();
                txtReceivedate.Text = dtResult.Rows[0]["RECEIVEDATE"].ToString();
                txtCreateby.Text = dtResult.Rows[0]["CREATEBY"].ToString();
                txtTotal.Text = dtResult.Rows[0]["TOTAL"].ToString();
            }
        }
        private void FormatGridView()
        {
            this.grvProductStockInShop.AllowUserToAddRows = false;
            this.grvProductStockInShop.AllowUserToDeleteRows = false;
            this.grvProductStockInShop.AutoGenerateColumns = false;
            this.grvProductStockInShop.AllowUserToOrderColumns = true;
            this.grvProductStockInShop.AllowUserToResizeColumns = true;
            this.grvProductStockInShop.AllowUserToResizeRows = true;
            this.grvProductStockInShop.BackgroundColor = System.Drawing.Color.Silver;
            this.grvProductStockInShop.ColumnHeadersDefaultCellStyle = Appz.HeaderStyle;
            this.grvProductStockInShop.AlternatingRowsDefaultCellStyle = Appz.AlternateRowStyle;
            this.grvProductStockInShop.EnableHeadersVisualStyles = false;
            this.grvProductStockInShop.MultiSelect = false;
            this.grvProductStockInShop.RowHeadersWidth = 21;
            this.grvProductStockInShop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void LoadData()
        {
            CreateTempTable();
            DataTable dt = ProductStockInShopFlow.GetProductStockInShopData(Appz.CurrentUserData.Warehouse.ToString(), _LOID.ToString());
            tempTable.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = tempTable.Rows.Add();
                dr["ORDERNO"] = Convert.ToString(i + 1);
                dr["PDCODE"] = dt.Rows[i]["PDCODE"].ToString();
                dr["PDNAME"] = dt.Rows[i]["PDNAME"].ToString();
                dr["RQ_QTY"] = Convert.ToDouble(dt.Rows[i]["RQ_QTY"]);
                dr["RECEIVE_QTY"] = Convert.ToDouble(dt.Rows[i]["RECEIVE_QTY"]);
                dr["UNITNAME"] = dt.Rows[i]["UNITNAME"].ToString();
                dr["PRICE"] = dt.Rows[i]["PRICE"].ToString();
                dr["TOTAL"] = dt.Rows[i]["TOTAL"].ToString();
                dr["SILOID"] = Convert.ToDouble(dt.Rows[i]["SILOID"]);
                dr["SIILOID"] = Convert.ToDouble(dt.Rows[i]["SIILOID"]);
                dr["PDLOID"] = Convert.ToDouble(dt.Rows[i]["PDLOID"]);
                dr["RQILOID"] = Convert.ToDouble(dt.Rows[i]["RQILOID"]);
                dr["SISTATUS"] = dt.Rows[i]["SISTATUS"].ToString();
                dr["SIISTATUS"] = dt.Rows[i]["SIISTATUS"].ToString();
            }
            grvProductStockInShop.DataSource = tempTable;
            FormatGridView();
            SetData();
        }

        private void SetData()
        {
            DataTable dtResult = new DataTable();
            dtResult = ProductStockInShopFlow.GetRequisition_StockInData(Appz.CurrentUserData.Warehouse.ToString(), _LOID.ToString());
            if (dtResult.Rows.Count > 0)
            {
                txtRqCode.Text = dtResult.Rows[0]["RQCODE"].ToString();
                txtSiCode.Text = dtResult.Rows[0]["SICODE"].ToString();
                txtReceivedate.Text = dtResult.Rows[0]["RECEIVEDATE"].ToString();
                txtCreateby.Text = dtResult.Rows[0]["CREATEBY"].ToString();
                txtTotal.Text =  dtResult.Rows[0]["TOTAL"].ToString();
            }
        }
        private void grvProductStockInShop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.txtSiiStatus.Text = grvProductStockInShop.Rows[e.RowIndex].Cells["SIISTATUS"].Value.ToString();
            }
        }

        private void CreateTempTable()
        {
            tempTable = new DataTable();
            DataColumn dcORDERNO = new DataColumn("ORDERNO");
            DataColumn dcPDCODE = new DataColumn("PDCODE");
            DataColumn dcPDNAME = new DataColumn("PDNAME");
            DataColumn dcRQ_QTY = new DataColumn("RQ_QTY");
            DataColumn dcRECEIVE_QTY = new DataColumn("RECEIVE_QTY");
            DataColumn dcUNITNAME = new DataColumn("UNITNAME");
            DataColumn dcPRICE = new DataColumn("PRICE");
            DataColumn dcTOTAL = new DataColumn("TOTAL");
            DataColumn dcSILOID = new DataColumn("SILOID");
            DataColumn dcSIILOID = new DataColumn("SIILOID");
            DataColumn dcPDLOID = new DataColumn("PDLOID");
            DataColumn dcRQILOID = new DataColumn("RQILOID");
            DataColumn dcSISTATUS = new DataColumn("SISTATUS");
            DataColumn dcSIISTATUS = new DataColumn("SIISTATUS");

            tempTable.Columns.Add(dcORDERNO);
            tempTable.Columns.Add(dcPDCODE);
            tempTable.Columns.Add(dcPDNAME);
            tempTable.Columns.Add(dcRQ_QTY);
            tempTable.Columns.Add(dcRECEIVE_QTY);
            tempTable.Columns.Add(dcUNITNAME);
            tempTable.Columns.Add(dcPRICE);
            tempTable.Columns.Add(dcTOTAL);
            tempTable.Columns.Add(dcSILOID);
            tempTable.Columns.Add(dcSIILOID);
            tempTable.Columns.Add(dcPDLOID);
            tempTable.Columns.Add(dcRQILOID);
            tempTable.Columns.Add(dcSISTATUS);
            tempTable.Columns.Add(dcSIISTATUS);


        }

        private void grvProductStockInShop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.DataGridViewTextboxCell")
                MessageBox.Show("ff");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ( (txtRqCode.Text.Trim() == "") || (txtChk.Text.Trim() != "-" ))
            {
                Appz.OpenWarningDialog("กรุณาระบุเลขที่ใบเบิกออกจากคลัง");
            }
            else
            {
            int fg = -1;
            if (Appz.OpenQuestionDialog("ต้องการบันทึกข้อมูลใช่หรือไม่?") == DialogResult.OK)
                fg = InsertStockInItem();
            if (fg == 1)
                Appz.OpenInformationDialog("บันทึกรายการเรียบร้อย");
            if (fg == 2)
                Appz.OpenInformationDialog("แก้ไขรายการเรียบร้อย");
            LoadData();
            }
            
        }

        private int InsertStockInItem()
        {
            grvProductStockInShop.EndEdit();
            ArrayList arr = new ArrayList();
            bool ret = true;
            double cnt = 0;
            cnt = grvProductStockInShop.Rows.Count;
            if (_FLAG == "ADD")
            {
                for (int i = 0; i < cnt; i++)
                {
                    
                    DataGridViewTextBoxCell QTY = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["RECEIVE_QTY"];
                    DataGridViewTextBoxCell STOCKIN = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["SILOID"];
                    DataGridViewTextBoxCell PRODUCT = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["PDLOID"];
                    DataGridViewTextBoxCell REFLOID = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["RQILOID"];
                    StockInItemData dr = new StockInItemData();
                    dr.QTY = Convert.ToDouble(QTY.Value.ToString());
                    dr.STOCKIN = Convert.ToDouble(STOCKIN.Value.ToString());
                    dr.PRODUCT = Convert.ToDouble(PRODUCT.Value.ToString());
                    dr.REFLOID = Convert.ToDouble(REFLOID.Value.ToString());
                    
                    arr.Add(dr);
                }
                ProductStockInShopFlow csFlow = new ProductStockInShopFlow();
                ret = csFlow.InsertStockInitem(Appz.CurrentUserData.UserID, arr);
                if (ret == true)
                {
                    //Appz.OpenInformationDialog("บันทึกรายการเรียบร้อย");
                    return 1;
                }
                else
                {
                    Appz.OpenWarningDialog(csFlow.ErrorMessage);
                    return 0;  
                } 
            }
            else
            {
                if (txtSiiStatus.Text.Trim() == "AP")
                {
                    Appz.OpenWarningDialog("รายการนี้ถูกยืนยันแล้ว");
                    return 0;
                }
                    
                else
                {
                    for (int i = 0; i < cnt; i++)
                    {
                        DataGridViewTextBoxCell SIILOID = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["SIILOID"];
                        DataGridViewTextBoxCell RECEIVE_QTY = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["RECEIVE_QTY"];
                        StockInItemData dr = new StockInItemData();
                        dr.LOID = Convert.ToDouble(SIILOID.Value.ToString());
                        dr.QTY = Convert.ToDouble(RECEIVE_QTY.Value.ToString());
                        arr.Add(dr);
                    }
                    ProductStockInShopFlow csFlow = new ProductStockInShopFlow();

                    ret = csFlow.UpdateTemptable(Appz.CurrentUserData.UserID, arr);
                    if (ret == true)
                        return 2;
                    else
                        Appz.OpenWarningDialog(csFlow.ErrorMessage);
                        return 0;                        
                }
            }
            
        }
        private void btnSearchPDStockInShop_Click(object sender, EventArgs e)
        {
            txtChk.Text = "-";
            Search.ProductStockInShopPopup frmPdStockInShoppopup = new Search.ProductStockInShopPopup();
            frmPdStockInShoppopup.ShowDialog(this);
            txtRqCode.Text = frmPdStockInShoppopup.RequisitionCode.ToString();
            GetRequisitionItemData();
        }

        private void txtRqCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChk.Text = "-";
                GetRequisitionItemData();
            }
        }

        private void GetRequisitionItemData()
        {
            CreateTempTable();
            DataTable dt = ProductStockInShopFlow.GetRequisitionItemData(Appz.CurrentUserData.Warehouse.ToString(), txtRqCode.Text.ToString());
            tempTable.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = tempTable.Rows.Add();
                dr["ORDERNO"] = Convert.ToString(i + 1);
                dr["PDCODE"] = dt.Rows[i]["PDCODE"].ToString();
                dr["PDNAME"] = dt.Rows[i]["PDNAME"].ToString();
                dr["RQ_QTY"] = Convert.ToDouble(dt.Rows[i]["RQ_QTY"]);
                dr["RECEIVE_QTY"] = Convert.ToDouble(dt.Rows[i]["RECEIVE_QTY"]);
                dr["UNITNAME"] = dt.Rows[i]["UNITNAME"].ToString();
                dr["PRICE"] = dt.Rows[i]["PRICE"].ToString();
                dr["TOTAL"] = dt.Rows[i]["TOTAL"].ToString();
                dr["SILOID"] = _LOID;
                dr["PDLOID"] = dt.Rows[i]["PDLOID"].ToString();
                dr["RQILOID"] = dt.Rows[i]["RQILOID"].ToString();

            }

            grvProductStockInShop.DataSource = tempTable;
            FormatGridView();
            SetData();

        }
        private void btnApprove_Click(object sender, EventArgs e)
        {
            if ((txtRqCode.Text.Trim() == "") || (txtChk.Text.Trim() != "-"))
            {
                Appz.OpenWarningDialog("กรุณาระบุเลขที่ใบเบิกออกจากคลัง");
            }
            else
            {
                int fg = -1;
                bool ret = true;
                if (Appz.OpenQuestionDialog("ต้องการบันทึกข้อมูลและยืนยันรับเข้าคลังใช่หรือไม่?") == DialogResult.OK)
                {
                    //if (txtSiiLoid.Text == "")
                    fg = InsertStockInItem();

                    if (fg != 0)
                        ret = SetApprove();
                    Appz.OpenInformationDialog("บันทึกข้อมูลและยืนยันรับเข้าคลังเรียบร้อย");
                }
            }

        }

        private bool SetApprove()
        {
            bool ret = true;
            ArrayList arrLOID = new ArrayList();
            for (int i = 0; i < grvProductStockInShop.Rows.Count; i++)
            {
                arrLOID.Add(grvProductStockInShop.Rows[i].Cells["SILOID"].Value.ToString());
            }


            ProductStockInShopFlow csFlow = new ProductStockInShopFlow();
            ret = csFlow.UpdateStockInData(Appz.CurrentUserData.UserID, arrLOID);

            if (ret == false)
                Appz.OpenWarningDialog(csFlow.ErrorMessage);
            return ret;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            int cnt = -1;
            bool ret = true ;
            if (Appz.OpenQuestionDialog("ต้องการกลับหน้าจอหลักใช่หรือไม่?") == DialogResult.OK )
            {
                cnt = ProductStockInShopFlow.CheckStockInitemData(_LOID.ToString());
                if (cnt == 0)
                {
                    ProductStockInShopFlow PdFlow = new ProductStockInShopFlow();
                    ret = PdFlow.DeleteStockInData(_LOID.ToString());
                }
                this.Close();
            }

        }


    }
}