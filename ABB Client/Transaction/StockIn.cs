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
    public partial class StockIn : Form
    {
        private StockInShopFlow _flow;
        private StockInEdit frmStockInEdt;
        private Reports.PreviewReport frmReport;
        private int indexCHECK = 0;
        private int indexLOID = 1;
        //private int indexORDERNO = 2;
        private int indexSTOCKINCODE = 3;
        //private int indexRECEIVEDATE = 4;
        //private int indexREQUISITIONCODE = 5;
        //private int indexGRANDTOT = 7;
        private int indexSTATUSNAME = 7;

        private StockInShopFlow FlowObj
        {
            get { if (_flow == null) { _flow = new StockInShopFlow(); } return _flow; }
        }

        public StockIn()
        {
            InitializeComponent();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvStockIn, false, false, true);
        }

        private StockInShopSearchData GetSearchData()
        {
            StockInShopSearchData data = new StockInShopSearchData();
            data.DATEFROM = this.dtpDateFrom.Value.Date;
            data.DATETO = this.dtpDateTo.Value.Date;
            data.REQUISITIONCODE = this.txtRequisitionCode.Text.Trim();
            data.STOCKINCODE = this.txtStockInCode.Text.Trim();
            return data;
        }

        private ArrayList GetStockInItem()
        {
            ArrayList arrData = new ArrayList();
            foreach(DataGridViewRow gRow in this.grvStockIn.Rows)
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
            this.grvStockIn.DataSource = FlowObj.GetStockInShopList(GetSearchData());
        }

        private void StockIn_Load(object sender, EventArgs e)
        {
            FormatGridView();
            SearchData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!FlowObj.InsertData(Appz.CurrentUserData.UserID, Appz.CurrentUserData.Warehouse))
                Appz.OpenErrorDialog(FlowObj.ErrorMessage);
            else
            {
                frmStockInEdt = new StockInEdit(FlowObj.LOID);
                frmStockInEdt.ShowDialog();
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
                if (Appz.OpenQuestionDialog("ต้องการลบรายการที่เลือกใช่หรือไม่?") == DialogResult.OK)
                {
                    if (FlowObj.DeleteData(arrData))
                    {
                        Appz.OpenInformationDialog("ลบรายการเรียบร้อยแล้ว");
                        SearchData();
                    }
                    else
                        Appz.OpenErrorDialog(FlowObj.ErrorMessage);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //if (frmReport == null || frmReport.IsDisposed) { frmReport = new ABBClient.Reports.PreviewReport(); }
            //frmReport.ShowDialog();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ArrayList arrData = GetStockInItem();
            if (arrData.Count == 0)
                Appz.OpenErrorDialog("ไม่พบรายการที่เลือก");
            else
            {
                if (Appz.OpenQuestionDialog("ต้องการยืนยันรายการที่เลือกใช่หรือไม่?") == DialogResult.OK)
                {
                    if (FlowObj.CommitData(arrData, Appz.CurrentUserData.OfficerID, Appz.CurrentUserData.UserID))
                    {
                        Appz.OpenInformationDialog("ยืนยันรายการเรียบร้อยแล้ว");
                        SearchData();
                    }
                    else
                        Appz.OpenErrorDialog(FlowObj.ErrorMessage);
                }
            }
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
                    if (this.grvStockIn[indexSTATUSNAME, e.RowIndex].Value.ToString() == Constz.Requisition.Status.Waiting.Name)
                    {
                        this.grvStockIn[e.ColumnIndex, e.RowIndex].Value = !Convert.ToBoolean(this.grvStockIn[e.ColumnIndex, e.RowIndex].Value);
                    }
                    else
                        Appz.OpenErrorDialog("ไม่สามารถเลือกราการที่ไม่อยู่ในสถานะทำรายการได้");
                }
                else if (e.ColumnIndex == indexSTOCKINCODE)
                {
                    frmStockInEdt = new StockInEdit(Convert.ToDouble(this.grvStockIn[indexLOID, e.RowIndex].Value));
                    frmStockInEdt.ShowDialog();
                    SearchData();
                }
            }
        }

    }
}