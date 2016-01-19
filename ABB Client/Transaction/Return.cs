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

namespace ABBClient.Transaction
{
    public partial class Return : Form
    {
        
        private double _LOID = 0;
        private string _FLAG = "";
        private DataTable tempTable = null;

        public Return(double SILOID, String FLAG)
        {
            InitializeComponent();
            _LOID = SILOID;
            _FLAG = FLAG;
        }

        private void Return_Load(object sender, EventArgs e)
        {
            FormatGridView();

            // กรณี update
            if (_FLAG != "ADD")
            {
                txtChk.Text = "-";
                txtRqCode.Enabled = false;
                btnSearchSlip.Enabled = false;
                btnSave.Enabled = false;
                LoadData();
            }
            else
            {
                txtRemark.Enabled = true;
                txtReason.Enabled = true; 
                DataTable dtResult = new DataTable();
                dtResult = ReturnSearchFlow.GetStockInData(_LOID.ToString());
                if (dtResult.Rows.Count > 0)
                {
                    txtRqCode.Text = "";
                    dpReqDate.Value  =  DateTime.Today;
                    txtCusCode.Text = "";
                    txtCusName.Text = "";

                    txtSiCode.Text = dtResult.Rows[0]["SICODE"].ToString();
                    txtReceivedate.Text = Convert.ToDateTime(dtResult.Rows[0]["RECEIVEDATE"]).ToString("dd/MM/yyyy");
                    txtCreateby.Text = dtResult.Rows[0]["CREATEBY"].ToString();
                    txtGrandTot.Text = dtResult.Rows[0]["GRANDTOT"].ToString();
                }
            }
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvReturn, false, true, false);
        }

        private void CreateTempTable()
        {
            tempTable = new DataTable();
            DataColumn dcORDERNO = new DataColumn("ORDERNO");
            DataColumn dcPDCODE = new DataColumn("PDCODE");
            DataColumn dcPDNAME = new DataColumn("PDNAME");
            DataColumn dcQTY = new DataColumn("QTY");
            DataColumn dcUNAME = new DataColumn("UNAME");
            DataColumn dcPRICE = new DataColumn("PRICE");
            DataColumn dcTOTAL = new DataColumn("TOTAL");
            DataColumn dcSILOID = new DataColumn("SILOID");
            DataColumn dcSIILOID = new DataColumn("SIILOID");
            DataColumn dcREASON = new DataColumn("REASON");
            DataColumn dcREMARK = new DataColumn("REMARK");
            DataColumn dcSISTATUS = new DataColumn("SISTATUS");
            DataColumn dcSIISTATUS = new DataColumn("SIISTATUS");
            DataColumn dcPDLOID = new DataColumn("PDLOID");
            DataColumn dcRQILOID = new DataColumn("RQILOID");
            DataColumn dcULOID = new DataColumn("ULOID");
            DataColumn dcQTY_OLD = new DataColumn("QTY_OLD");
            tempTable.Columns.Add(dcORDERNO);
            tempTable.Columns.Add(dcPDCODE);
            tempTable.Columns.Add(dcPDNAME);
            tempTable.Columns.Add(dcQTY);
            tempTable.Columns.Add(dcUNAME);
            tempTable.Columns.Add(dcPRICE);
            tempTable.Columns.Add(dcTOTAL);
            tempTable.Columns.Add(dcSILOID);
            tempTable.Columns.Add(dcSIILOID);
            tempTable.Columns.Add(dcREASON);
            tempTable.Columns.Add(dcREMARK);
            tempTable.Columns.Add(dcSISTATUS);
            tempTable.Columns.Add(dcSIISTATUS);
            tempTable.Columns.Add (dcPDLOID);
            tempTable.Columns.Add (dcRQILOID);
            tempTable.Columns.Add(dcULOID);
            tempTable.Columns.Add(dcQTY_OLD);

        }

        private void LoadData()
        {
            CreateTempTable();
            DataTable dt = ReturnSearchFlow.GetProductStockInShopData(Appz.CurrentUserData.Warehouse.ToString(), _LOID.ToString());
            tempTable.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = tempTable.Rows.Add();
                dr["ORDERNO"] = Convert.ToString(i + 1);
                dr["PDCODE"] = dt.Rows[i]["PDCODE"].ToString();
                dr["PDNAME"] = dt.Rows[i]["PDNAME"].ToString();
                dr["QTY"] = Convert.ToDouble(dt.Rows[i]["QTY"]);
                dr["UNAME"] = dt.Rows[i]["UNAME"].ToString();
                dr["PRICE"] = Convert.ToDouble(dt.Rows[i]["PRICE"]);
                dr["TOTAL"] = Convert.ToDouble(dt.Rows[i]["TOTAL"]);
                dr["SILOID"] = Convert.ToDouble(dt.Rows[i]["SILOID"]);
                dr["SIILOID"] = Convert.ToDouble(dt.Rows[i]["SIILOID"]);
                dr["SISTATUS"] = dt.Rows[i]["SISTATUS"].ToString();
                dr["SIISTATUS"] = dt.Rows[i]["SIISTATUS"].ToString();
                dr["QTY_OLD"] = Convert.ToDouble(dt.Rows[i]["QTY"]);
            }

            grvReturn.DataSource = tempTable;
            FormatGridView();
            SetData();
        }

        private void SetData()
        {
            DataTable dtResult = new DataTable();
            dtResult = ReturnSearchFlow.GetRequisition_StockInData(Appz.CurrentUserData.Warehouse.ToString(), _LOID.ToString());
            if (dtResult.Rows.Count > 0)
            {
                txtRqCode.Text = dtResult.Rows[0]["RQCODE"].ToString();
                dpReqDate.Value = Convert.ToDateTime(dtResult.Rows[0]["REQDATE"]);
                txtCusCode.Text = dtResult.Rows[0]["CUSCODE"].ToString();
                txtCusName.Text = dtResult.Rows[0]["CUSNAME"].ToString();
                txtCreateby.Text = dtResult.Rows[0]["CREATEBY"].ToString();
                txtSiCode.Text = dtResult.Rows[0]["SICODE"].ToString();
                txtReceivedate.Text = dtResult.Rows[0]["RECEIVEDATE"].ToString();
                txtGrandTot.Text = dtResult.Rows[0]["GRANDTOT"].ToString();
                txtReason.Text = dtResult.Rows[0]["REASON"].ToString();
                txtRemark.Text = dtResult.Rows[0]["REMARK"].ToString();
            }
        }


        private void GetRequisitionItemData()
        {
            CreateTempTable();
            DataTable dt = ReturnSearchFlow.GetRequisitionItemData(Appz.CurrentUserData.Warehouse.ToString(), txtRqCode.Text.ToString());

            tempTable.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = tempTable.Rows.Add();
                dr["ORDERNO"] = Convert.ToString(i + 1);
                dr["PDCODE"] = dt.Rows[i]["PDCODE"].ToString();
                dr["PDNAME"] = dt.Rows[i]["PDNAME"].ToString();
                dr["QTY"] = Convert.ToDouble(dt.Rows[i]["RQ_QTY"]);
                dr["UNAME"] = dt.Rows[i]["UNITNAME"].ToString();
                dr["PRICE"] = dt.Rows[i]["PRICE"].ToString();
                dr["TOTAL"] = dt.Rows[i]["TOTAL"].ToString();
                dr["SILOID"] = _LOID;
                dr["PDLOID"] = dt.Rows[i]["PDLOID"].ToString();
                dr["RQILOID"] = dt.Rows[i]["RQILOID"].ToString();
                dr["ULOID"] = Convert.ToDouble(dt.Rows[i]["ULOID"]);
                dr["QTY_OLD"] = Convert.ToDouble(dt.Rows[i]["RQ_QTY"]);
            }

            grvReturn.DataSource = tempTable;
            FormatGridView();
            SetData();
            SetData_Requisition();
            for (int i =0; i < grvReturn.Rows.Count; i++)
            {
                DataGridViewTextBoxCell QTY = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["QTY"];
                DataGridViewTextBoxCell PRICE = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["PRICE"];
                DataGridViewTextBoxCell TOTAL = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["TOTAL"];

                if (QTY.Value.ToString() != "")
                {
                    TOTAL.Value = Convert.ToString(Convert.ToDouble(QTY.Value.ToString()) * Convert.ToDouble(PRICE.Value));
                }
            }

            SumTotal();
        }
        private void SetData_Requisition()
        {
            DataTable dtResult = new DataTable();
            dtResult = ReturnSearchFlow.GetRequisitionItem_CopyData(Appz.CurrentUserData.Warehouse.ToString(), txtRqCode.Text.Trim());//_LOID.ToString()
            if (dtResult.Rows.Count > 0)
            {
                txtRqCode.Text = dtResult.Rows[0]["RQCODE"].ToString();
                dpReqDate.Value = Convert.ToDateTime(dtResult.Rows[0]["REQDATE"]);
                txtCusCode.Text = dtResult.Rows[0]["CUSCODE"].ToString();
                txtCusName.Text = dtResult.Rows[0]["CUSNAME"].ToString();

            }
        }

        private void txtRqCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChk.Text = "-";
                GetRequisitionItemData();
            }
        }

        private void grvReturn_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridView grvReturn = (DataGridView)sender;
                DataGridViewTextBoxEditingControl txtBox = (DataGridViewTextBoxEditingControl)e.Control;

                txtBox.KeyPress -= new KeyPressEventHandler(grvReturn_KeyPress);
                txtBox.TextChanged -= new EventHandler(grvReturn_TextChanged);
                txtBox.Leave -= new EventHandler(grvReturn_Leave);

                if (grvReturn.CurrentCell.OwningColumn.Name == "QTY")
                {
                    txtBox.KeyPress += new KeyPressEventHandler(grvReturn_KeyPress);
                    txtBox.TextChanged += new EventHandler(grvReturn_TextChanged);
                    txtBox.Leave += new EventHandler(grvReturn_Leave);                    
                }
            }
        }

        private void grvReturn_Leave(object sender, EventArgs e)
        {
            DataGridViewTextBoxEditingControl QTY = ((DataGridViewTextBoxEditingControl)sender);
            if (QTY.Text.Trim() == "")
                QTY.Text = "0";
        }

        private void grvReturn_TextChanged(object sender, EventArgs e)
        {
            int rowindex = grvReturn.CurrentRow.Index;
            DataGridViewTextBoxEditingControl QTY = ((DataGridViewTextBoxEditingControl)sender);
            DataGridViewTextBoxCell PRICE = (DataGridViewTextBoxCell)grvReturn.Rows[rowindex].Cells["PRICE"];
            DataGridViewTextBoxCell TOTAL = (DataGridViewTextBoxCell)grvReturn.Rows[rowindex].Cells["TOTAL"];
            DataGridViewTextBoxCell QTY_OLD = (DataGridViewTextBoxCell)grvReturn.Rows[rowindex].Cells["QTY_OLD"];

            if (QTY.Text.Trim() != "")
            {
                if (Convert.ToDouble(QTY.Text.Trim()) > Convert.ToDouble(QTY_OLD.Value))
                {
                    Appz.OpenErrorDialog("จำนวนสินค้ารับคืนมากกว่าจำนวนสินค้าในใบเสร็จที่อ้างอิง");
                    QTY.Text = QTY_OLD.Value.ToString();
                }
                else 
                    TOTAL.Value = Convert.ToString(Convert.ToDouble(QTY.Text.Trim()) * Convert.ToDouble(PRICE.Value));
            }

            SumTotal();
        }

        private void SumTotal()
        {
            double sum = 0;
            for (int i = 0; i < grvReturn.Rows.Count; i++)
            {
                DataGridViewTextBoxCell TOTAL = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["TOTAL"];
                sum += Convert.ToDouble(TOTAL.Value);
            }
            txtGrandTot.Text = sum.ToString();
        }

        private void grvReturn_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetDBlTextBox(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((txtRqCode.Text.Trim() == "") || (txtChk.Text.Trim() != "-"))
            {
                Appz.OpenWarningDialog("กรุณาระบุเลขที่ใบเสร็จรับเงิน");
            }
            else
            {
                int fg = -1;
                if (Appz.OpenQuestionDialog("ต้องการบันทึกข้อมูลใช่หรือไม่?") == DialogResult.OK)
                    fg = InsertStockInItem();
                if (fg == 1)
                    Appz.OpenInformationDialog("บันทึกรายการเรียบร้อย");
                LoadData();
                txtReason.Enabled = false;
                txtRemark.Enabled = false;
                btnSearchSlip.Enabled = false;
                btnSave.Enabled = false;
                txtRqCode.Enabled = false;
         
            }
        }

        private int InsertStockInItem()
        {
            grvReturn.EndEdit();
            ArrayList arr = new ArrayList();
            ArrayList SiArr = new ArrayList();
            bool ret = true;
            bool rr = true;
            double cnt = 0;
            cnt = grvReturn.Rows.Count;
            StockInData sd = new StockInData();
            sd.GRANDTOT = Convert.ToDouble(txtGrandTot.Text.Trim());
            sd.LOID = Convert.ToDouble(_LOID.ToString());
            sd.REASON = txtReason.Text.Trim();
            sd.REMARK = txtRemark.Text.Trim();
            SiArr.Add(sd);
            if (_FLAG == "ADD")
            {
                for (int i = 0; i < cnt; i++)
                {

                    DataGridViewTextBoxCell QTY = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["QTY"];
                    DataGridViewTextBoxCell STOCKIN = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["SILOID"];
                    DataGridViewTextBoxCell PRODUCT = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["PDLOID"];
                    DataGridViewTextBoxCell REFLOID = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["RQILOID"];
                    DataGridViewTextBoxCell PRICE = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["PRICE"];
                    DataGridViewTextBoxCell ULOID = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["ULOID"];

                    StockInItemData dr = new StockInItemData();
                    dr.QTY = Convert.ToDouble(QTY.Value.ToString());
                    dr.STOCKIN = Convert.ToDouble(STOCKIN.Value.ToString());
                    dr.PRODUCT = Convert.ToDouble(PRODUCT.Value.ToString());
                    dr.REFLOID = Convert.ToDouble(REFLOID.Value.ToString());
                    dr.PRICE = Convert.ToDouble(PRICE.Value.ToString());
                    dr.UNIT = Convert.ToDouble(ULOID.Value.ToString());
                    arr.Add(dr);
                }

                ReturnSearchFlow csFlow = new ReturnSearchFlow();
                ret = csFlow.InsertStockInitem(Appz.CurrentUserData.UserID, arr);
                if (ret == true)
                {
                    rr = csFlow.UpdateStockIn_GrandTot(Appz.CurrentUserData.UserID, SiArr);
                    setStatusRequisition("0", "VO");
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
                    //กรณี update จำนวนรับคืน
                //    for (int i = 0; i < cnt; i++)
                //    {
                //        DataGridViewTextBoxCell SIILOID = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["SIILOID"];
                //        DataGridViewTextBoxCell QTY = (DataGridViewTextBoxCell)grvReturn.Rows[i].Cells["QTY"];
                //        StockInItemData dr = new StockInItemData();
                //        dr.LOID = Convert.ToDouble(SIILOID.Value.ToString());
                //        dr.QTY = Convert.ToDouble(QTY.Value.ToString());
                //        arr.Add(dr);
                //    }

                //    ReturnSearchFlow csFlow = new ReturnSearchFlow();


                //    ret = csFlow.UpdateTemptable(Appz.CurrentUserData.UserID, arr);
                //    if (ret == true)
                //    {
                //        rr = csFlow.UpdateStockIn_GrandTot(Appz.CurrentUserData.UserID, SiArr);
                //        return 2;
                //    }
                //    else
                //    {
                //        Appz.OpenWarningDialog(csFlow.ErrorMessage);
                        return 0;
                    }
                }
            }

        private void grvReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.txtSiLoid.Text = grvReturn.Rows[e.RowIndex].Cells["SILOID"].Value.ToString();
                this.txtSiStatus.Text = grvReturn.Rows[e.RowIndex].Cells["SISTATUS"].Value.ToString();
                this.txtSiiStatus.Text = grvReturn.Rows[e.RowIndex].Cells["SIISTATUS"].Value.ToString();
                if (_FLAG != "ADD")
                {
                    grvReturn.Rows[e.RowIndex].Cells["QTY"].ReadOnly = true;
                }  
            }   
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            int cnt = -1;
            bool ret = true;
            if (Appz.OpenQuestionDialog("ต้องการกลับหน้าจอหลักใช่หรือไม่?") == DialogResult.OK)
            {
                cnt = ReturnSearchFlow.CheckStockInitemData(_LOID.ToString());

                if (cnt == 0)
                {
                    ReturnSearchFlow RtFlow = new ReturnSearchFlow();
                    
                    ret = RtFlow.DeleteStockInData(_LOID.ToString());
                    if (txtSiiStatus.Text.Trim() != "")
                        setStatusRequisition("0", "VO");
                    else
                        setStatusRequisition("1", "AP");

                }
                this.Close();
            }
        }

        private void btnSearchSlip_Click(object sender, EventArgs e)
        {
            txtChk.Text = "-";
            if (txtRqCode.Text.Trim() != "")
                setStatusRequisition("1", "AP");
            Search.ReturnPopup frmReturnpopup = new Search.ReturnPopup();
            frmReturnpopup.ShowDialog(this);
            txtRqCode.Text = frmReturnpopup.RequisitionCode.ToString();
            GetRequisitionItemData();
        }

        private void setStatusRequisition(string active,string status)
        {
            bool rt = true;
            ArrayList arr = new ArrayList();
            ReturnSearchFlow csFlow = new ReturnSearchFlow();
            RequisitionData dr = new RequisitionData();
            dr.CODE = txtRqCode.Text.Trim();
            dr.ACTIVE = active.ToString();
            dr.STATUS = status.ToString();
            arr.Add(dr);
            rt = csFlow.UpdateRequisition_Choose(Appz.CurrentUserData.UserID.ToString(), arr);
        }

    }



}