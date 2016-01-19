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
    public partial class StockInReturn : Form
    {
        private StockInReturnFlow _flow;
        private StockInReturnEdit frmStockInReturnEdit;
        private Reports.PreviewReport frmReport;
        private int indexCHECK = 0;
        private int indexCODE = 1;
        private int indexLOID = 6;
        private int indexSTATUSNAME = 7;

        private StockInReturnFlow FlowObj
        {
            get { if (_flow == null) { _flow = new StockInReturnFlow(); } return _flow; }
        }

        public StockInReturn()
        {
            InitializeComponent();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvStockIn, false, false, true);
        }

        private StockInReturnSearchData GetSearchData()
        {
            StockInReturnSearchData data = new StockInReturnSearchData();
            data.DATEFROM = this.dtpDateFrom.Value.Date;
            data.DATETO = this.dtpDateTo.Value.Date;
            data.CODE = this.txtStockInCode.Text.Trim();
            return data;
        }

        private ArrayList GetStockInItem()
        {
            ArrayList arrData = new ArrayList();
            foreach (DataGridViewRow gRow in this.grvStockIn.Rows)
            {
                if (gRow.Cells[indexCHECK].Value != null)
                {
                    if ((bool)gRow.Cells[indexCHECK].Value) arrData.Add(Convert.ToDouble(gRow.Cells[indexLOID].Value));
                }
            }
            return arrData;
        }

        private void SearchData()
        {
            this.grvStockIn.DataSource = FlowObj.GetStockInList(GetSearchData());
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!FlowObj.InsertData(Appz.CurrentUserData.UserID, Appz.CurrentUserData.Warehouse))
                Appz.OpenErrorDialog(FlowObj.ErrorMessage);
            else
            {
                frmStockInReturnEdit = new StockInReturnEdit(FlowObj.LOID);
                frmStockInReturnEdit.ShowDialog();
                SearchData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ArrayList arrData = GetStockInItem();
            if (arrData.Count == 0)
                Appz.OpenErrorDialog("ไม่พบรายการที่เลือก");
            else
            {
                if (Appz.OpenQuestionDialog("ต้องการยกเลิกรายการที่เลือกใช่หรือไม่?") == DialogResult.OK)
                {
                    if (FlowObj.CancelData(Appz.CurrentUserData.UserID, arrData))
                    {
                        Appz.OpenInformationDialog("ยกเลิกรายการเรียบร้อยแล้ว");
                        SearchData();
                    }
                    else
                        Appz.OpenErrorDialog(FlowObj.ErrorMessage);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void grvStockIn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == indexCHECK)
                {
                    if (this.grvStockIn[indexSTATUSNAME, e.RowIndex].Value.ToString() == Constz.Requisition.Status.Void.Name)
                        Appz.OpenErrorDialog("ไม่สามารถเลือกรายการสถานะยกเลิกได้");
                    else
                        this.grvStockIn[e.ColumnIndex, e.RowIndex].Value = !Convert.ToBoolean(this.grvStockIn[e.ColumnIndex, e.RowIndex].Value);
                }
                else if (e.ColumnIndex == indexCODE)
                {
                    frmStockInReturnEdit = new StockInReturnEdit(Convert.ToDouble(this.grvStockIn[indexLOID, e.RowIndex].Value));
                    frmStockInReturnEdit.ShowDialog();
                    SearchData();
                }
            }
        }

        private void StockInReturn_Load(object sender, EventArgs e)
        {
            FormatGridView();
            SearchData();
        }

    }
}