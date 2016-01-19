using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Data;
using ABB.Data.Sales;
using ABB.Flow.Sales;

namespace ABBClient.Transaction
{
    public partial class ReturnTester : Form
    {
        public ReturnTester()
        {
            InitializeComponent();
        }

        private double _LOID = 0;
        private int indexCHECKBOX = 0;
        private int indexLOID = 1;
        private int indexCODE = 2;
        //private int indexDATE = 3;
        //private int indexWAREHOUSE = 4;
        //private int indexQTY = 5;
        //private int indexSTATUS = 6;
        private int indexRANK = 7;
        private ReturnTesterFlow _flow;
        private Transaction.ReturnTesterEdit frmReturnTesterEdit;

        public ReturnTesterFlow FlowObj
        {
            get { if (_flow == null) _flow = new ReturnTesterFlow(); return _flow; }
        }

        public double StockoutID
        {
            get { return _LOID; }
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvReturn, false, false, true);
        }

        private ReturnTesterSearchData GetData()
        {
            ReturnTesterSearchData data = new ReturnTesterSearchData();
            data.SENDER = Appz.CurrentUserData.Warehouse;
            data.CODE = this.txtCode.Text.Trim();
            data.DATEFROM = this.dtpDateFrom.Value;
            data.DATETO = this.dtpDateTo.Value;
            if (this.cmbStatusFrom.SelectedValue != null)
                data.STATUSFROM = this.cmbStatusFrom.SelectedValue.ToString();
            if (this.cmbStatusTo.SelectedValue != null)
                data.STATUSTO = this.cmbStatusTo.SelectedValue.ToString();
            return data;
        }

        private void SearchData()
        {
            this.grvReturn.DataSource = FlowObj.GetReturnTesterList(GetData());
            this.chkAll.Enabled = (this.grvReturn.Rows.Count > 0);
            this.chkAll.Checked = false;

            foreach (DataGridViewRow gRow in this.grvReturn.Rows)
            {
                if (gRow.Cells[indexRANK].Value.ToString() != Constz.Requisition.Status.Waiting.Rank)
                {
                    gRow.Cells[indexCHECKBOX].ReadOnly = true;
                }
                else
                {
                    gRow.Cells[indexCHECKBOX].ReadOnly = false;
                }
            }
        }

        private ArrayList GetCheckList()
        {
            ArrayList arr = new ArrayList();
            foreach (DataGridViewRow gRow in this.grvReturn.Rows)
            {
                if (gRow.Cells[indexCHECKBOX].Value.ToString() == "1")
                {
                    arr.Add(Convert.ToDouble(gRow.Cells[indexLOID].Value));
                }
            }
            return arr;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void ReturnTester_Load(object sender, EventArgs e)
        {
            Appz.BuildStatusCombo(this.cmbStatusFrom);
            Appz.BuildStatusCombo(this.cmbStatusTo);
            FormatGridView();
            SearchData();
        }

        private void chkAll_Click(object sender, EventArgs e)
        {
            bool ch = this.chkAll.Checked;
            foreach (DataGridViewRow gRow in this.grvReturn.Rows)
            {
                if (gRow.Cells[indexRANK].Value.ToString() == Constz.Requisition.Status.Waiting.Rank) gRow.Cells[indexCHECKBOX].Value = (ch ? "1" : "0");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (frmReturnTesterEdit == null || frmReturnTesterEdit.IsDisposed) { frmReturnTesterEdit = new ReturnTesterEdit(); }
            frmReturnTesterEdit.StartPosition = FormStartPosition.CenterScreen;
            frmReturnTesterEdit.StockOut = 0;
            frmReturnTesterEdit.ShowDialog();
            SearchData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ArrayList arr = GetCheckList();
            if (arr.Count > 0)
            {
                if (Appz.OpenQuestionDialog("ต้องการลบรายการขอคืนสินค้าตัวอย่างใช่หรือไม่?") == DialogResult.OK)
                {
                    if (FlowObj.DeleteData(arr))
                        SearchData();
                    else
                        Appz.OpenErrorDialog(FlowObj.ErrorMessage);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ArrayList arr = GetCheckList();
            if (arr.Count > 0)
            {
                if (Appz.OpenQuestionDialog("ต้องการส่งรายการขอคืนสินค้าตัวอย่างไปยังคลังสินค้าใช่หรือไม่?") == DialogResult.OK)
                {
                    if (FlowObj.ApproveData(Appz.CurrentUserData.UserID, Appz.CurrentUserData.OfficerID, arr))
                    {
                        Appz.OpenInformationDialog("ส่งคลังสำเร็จรูปเรียบร้อย");
                        SearchData();
                    }
                        
                    else
                        Appz.OpenErrorDialog(FlowObj.ErrorMessage);
                }
            }
        }

        private void grvReturn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == indexCODE)
            {
                if (frmReturnTesterEdit == null || frmReturnTesterEdit.IsDisposed) { frmReturnTesterEdit = new ReturnTesterEdit(); }
                frmReturnTesterEdit.StartPosition = FormStartPosition.CenterScreen;
                frmReturnTesterEdit.StockOut = Convert.ToDouble(this.grvReturn[indexLOID,e.RowIndex].Value);
                frmReturnTesterEdit.ShowDialog();
                SearchData();
            }
            else if (e.ColumnIndex == indexCHECKBOX)
            {
                if (this.grvReturn[indexRANK, e.RowIndex].Value.ToString() == Constz.Requisition.Status.Waiting.Rank)
                    this.grvReturn[e.ColumnIndex,e.RowIndex].Value = (this.grvReturn[e.ColumnIndex,e.RowIndex].Value.ToString() == "0" ? "1" : "0");
                else
                    Appz.OpenErrorDialog("ไม่สามารถเลือกราการที่ไม่อยู่ในสถานะทำรายการได้");
            }
        }

    }
}