using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow.Sales;
using System.Collections;
using ABB.Data;
using ABB.Data.Sales;
using ABBClient;


namespace ABBClient.Transaction
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
                    txtReceivedate.Text = Convert.ToDateTime(dtResult.Rows[0]["RECEIVEDATE"]).ToString("dd/MM/yyyy");
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
            Appz.FormatDataGridView(this.grvProductStockInShop, false, true, false);
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
                dr["TOTAL"] = dt.Rows[i]["SSUM"].ToString();
                dr["SILOID"] = Convert.ToDouble(dt.Rows[i]["SILOID"]);
                dr["SIILOID"] = Convert.ToDouble(dt.Rows[i]["SIILOID"]);
                dr["PDLOID"] = Convert.ToDouble(dt.Rows[i]["PDLOID"]);
                dr["RQILOID"] = Convert.ToDouble(dt.Rows[i]["RQILOID"]);
                dr["SISTATUS"] = dt.Rows[i]["SISTATUS"].ToString();
                dr["SIISTATUS"] = dt.Rows[i]["SIISTATUS"].ToString();
                dr["ULOID"] = Convert.ToDouble(dt.Rows[i]["ULOID"]);
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
                txtSiLoid.Text = dtResult.Rows[0]["SILIOID"].ToString();
                txtRqCode.Text = dtResult.Rows[0]["RQCODE"].ToString();
                txtSiCode.Text = dtResult.Rows[0]["SICODE"].ToString();
                txtReceivedate.Text = Convert.ToDateTime(dtResult.Rows[0]["RECEIVEDATE"]).ToString("dd/MM/yyyy");
                txtCreateby.Text = dtResult.Rows[0]["CREATEBY"].ToString();
                txtTotal.Text =  dtResult.Rows[0]["TOTAL"].ToString();
                txtChkRqCode.Text = dtResult.Rows[0]["RQCODE"].ToString();
                if (dtResult.Rows[0]["SISTATUS"].ToString() == "AP")
                    btnSearchPDStockInShop.Enabled = false;
            }
            SumTotal();
        }
        private void grvProductStockInShop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.txtSiiStatus.Text = grvProductStockInShop.Rows[e.RowIndex].Cells["SIISTATUS"].Value.ToString();
                this.txtSiLoid.Text = grvProductStockInShop.Rows[e.RowIndex].Cells["SILOID"].Value.ToString();
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
            DataColumn dcULOID = new DataColumn("ULOID");
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
            tempTable.Columns.Add(dcULOID);
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
            {
                Appz.OpenInformationDialog("บันทึกรายการเรียบร้อย");
                _FLAG = "EDIT";
            }
                
            if (fg == 2)
                Appz.OpenInformationDialog("แก้ไขรายการเรียบร้อย");
            LoadData();
            }           
        }

        private int InsertStockInItem()
        {
            grvProductStockInShop.EndEdit();
            ArrayList arr = new ArrayList();
            ArrayList SiArr = new ArrayList();
            bool ret = true;
            bool rr = true;
            double cnt = 0;
            cnt = grvProductStockInShop.Rows.Count;
            StockInData sd = new StockInData();
            sd.GRANDTOT = Convert.ToDouble(txtTotal.Text.Trim());
            sd.LOID = Convert.ToDouble((txtSiLoid.Text.Trim()==""?"0":txtSiLoid.Text.Trim()));
            SiArr.Add(sd);
            if (_FLAG == "ADD")
            {
                for (int i = 0; i < cnt; i++)
                {
                    
                    DataGridViewTextBoxCell QTY = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["RECEIVE_QTY"];
                    DataGridViewTextBoxCell STOCKIN = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["SILOID"];
                    DataGridViewTextBoxCell PRODUCT = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["PDLOID"];
                    DataGridViewTextBoxCell REFLOID = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["RQILOID"];
                    DataGridViewTextBoxCell PRICE = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["PRICE"];
                    DataGridViewTextBoxCell ULOID = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["ULOID"];

                    StockInItemData dr = new StockInItemData();
                    dr.QTY = Convert.ToDouble(QTY.Value.ToString());
                    dr.STOCKIN = Convert.ToDouble(STOCKIN.Value.ToString());
                    dr.PRODUCT = Convert.ToDouble(PRODUCT.Value.ToString());
                    dr.REFLOID = Convert.ToDouble(REFLOID.Value.ToString());
                    dr.PRICE = Convert.ToDouble(PRICE.Value.ToString());
                    dr.UNIT = Convert.ToDouble(ULOID.Value.ToString());
                    arr.Add(dr);
                }
                ProductStockInShopFlow csFlow = new ProductStockInShopFlow();
                ret = csFlow.InsertStockInitem(Appz.CurrentUserData.UserID, arr);
                if (ret == true)
                {
                    rr = csFlow.UpdateStockIn_GrandTot(Appz.CurrentUserData.UserID, SiArr);
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
                    {
                        rr = csFlow.UpdateStockIn_GrandTot(Appz.CurrentUserData.UserID, SiArr);
                        return 2;
                    }  
                    else
                    {
                        Appz.OpenWarningDialog(csFlow.ErrorMessage);
                        return 0;
                    }
                }
            }            
        }
        private void btnSearchPDStockInShop_Click(object sender, EventArgs e)
        {
            txtChk.Text = "-";
            Search.ProductStockInShopPopup frmPdStockInShoppopup = new Search.ProductStockInShopPopup();
            frmPdStockInShoppopup.ShowDialog(this);
            txtRqCode.Text = frmPdStockInShoppopup.RequisitionCode.ToString();
            if (txtRqCode.Text.Trim() == "")
                txtRqCode.Text = txtChkRqCode.Text ;
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
            DataTable dt = ProductStockInShopFlow.GetRequisitionItemData(Appz.CurrentUserData.Warehouse.ToString(), txtRqCode.Text.Trim());
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
                dr["ULOID"] = Convert.ToDouble(dt.Rows[i]["ULOID"]);
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
                    fg = InsertStockInItem();

                    if (fg != 0)
                        ret = SetApprove();
                    Appz.OpenInformationDialog("บันทึกข้อมูลและยืนยันรับเข้าคลังเรียบร้อย");
                    btnSearchPDStockInShop.Enabled = false;
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
            if (txtSiiStatus.Text.Trim () != "")
                ret = csFlow.UpdateStockInData(Appz.CurrentUserData.UserID, arrLOID);
            else
                ret = csFlow.UpdateStockInApproveData(Appz.CurrentUserData.UserID, arrLOID);

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

        private void grvProductStockInShop_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridView grvProductStockInShop = (DataGridView)sender;
                DataGridViewTextBoxEditingControl txtBox = (DataGridViewTextBoxEditingControl)e.Control;

                txtBox.KeyPress -= new KeyPressEventHandler(grvProductStockInShop_KeyPress);
                txtBox.TextChanged -= new EventHandler(grvProductStockInShop_TextChanged);
                txtBox.Leave -= new EventHandler(grvProductStockInShop_Leave);

                if (grvProductStockInShop.CurrentCell.OwningColumn.Name == "RECEIVE_QTY")
                {
                    txtBox.KeyPress += new KeyPressEventHandler(grvProductStockInShop_KeyPress);
                    txtBox.TextChanged += new EventHandler(grvProductStockInShop_TextChanged);
                    txtBox.Leave += new EventHandler(grvProductStockInShop_Leave);    
                }
            }
        }

        private void grvProductStockInShop_Leave(object sender, EventArgs e)
        {
            DataGridViewTextBoxEditingControl RECEIVE_QTY = ((DataGridViewTextBoxEditingControl)sender);
            if (RECEIVE_QTY.Text.Trim() == "")
                RECEIVE_QTY.Text = "0";
        }

        private void grvProductStockInShop_TextChanged(object sender, EventArgs e)
        {
            int rowindex = grvProductStockInShop.CurrentRow.Index;
            DataGridViewTextBoxEditingControl RECEIVE_QTY = ((DataGridViewTextBoxEditingControl)sender);
            DataGridViewTextBoxCell PRICE = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[rowindex].Cells["PRICE"];
            DataGridViewTextBoxCell TOTAL = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[rowindex].Cells["TOTAL"];

            if (RECEIVE_QTY.Text.Trim() != "")
            {
                TOTAL.Value = Convert.ToString(Convert.ToDouble(RECEIVE_QTY.Text.Trim()) * Convert.ToDouble(PRICE.Value));
            }

            SumTotal();
        }

        private void SumTotal()
        {
            double sum = 0;
            for (int i = 0; i < grvProductStockInShop.Rows.Count; i++)
            {
                DataGridViewTextBoxCell TOTAL = (DataGridViewTextBoxCell)grvProductStockInShop.Rows[i].Cells["TOTAL"];
                sum += Convert.ToDouble(TOTAL.Value);
            }
            txtTotal.Text = sum.ToString();
        }

        private void grvProductStockInShop_KeyPress(object sender, KeyPressEventArgs e)
        {           
            ControlUtil.SetDBlTextBox(sender, e);
        }

        private void grvProductStockInShop_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            SumTotal();
        }
    }
}