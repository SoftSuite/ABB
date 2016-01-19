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
    public partial class ReturnSearch : Form
    {
        public ReturnSearch()
        {
            InitializeComponent();
        }
        string str_DateFrom;
        string str_DateTo;

        private void ReturnSearch_Load(object sender, EventArgs e)
        {
            DateFromPk.Value = DateTime.Today;
            DateToPk.Value = DateTime.Today;
            LoadData();
        }

        private void LoadData()
        {
            FormatGridView();
            SetDate();
            this.grvReturnSearch.DataSource = ReturnSearchFlow.GetReturnSearch(Appz.CurrentUserData.Warehouse.ToString(), txtSICode.Text.Trim().ToString(), str_DateFrom.ToString(), str_DateTo.ToString());

        }
        private void SetDate()
        {
            str_DateFrom = getDateFrom();
            str_DateTo = getDateTo();
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

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvReturnSearch, false, false, true);
        }

        private void btnSearchReturn_Click(object sender, EventArgs e)
        {
            SetDate();
            ArrayList arr = ReturnSearchFlow.GetReturnSearch(Appz.CurrentUserData.Warehouse.ToString(), txtSICode.Text.ToString(), str_DateFrom, str_DateTo);
            this.grvReturnSearch.DataSource = arr;

            if (arr.Count > 0)
            {
                this.grvReturnSearch.Columns["CHKAPPROVE"].HeaderText = "เลือก";
                this.grvReturnSearch.Columns["SICODE"].HeaderText = "เลขที่";
                this.grvReturnSearch.Columns["APPROVEDATE"].HeaderText = "วันที่";
                this.grvReturnSearch.Columns["RQCODE"].HeaderText = "เลขที่ใบเสร็จ";
                this.grvReturnSearch.Columns["CUSNAME"].HeaderText = "ลูกค้า";
                this.grvReturnSearch.Columns["GRANDTOT"].HeaderText = "ยอดสุทธิ";
                this.grvReturnSearch.Columns["CHKAPPROVE"].DisplayIndex = 0;
                this.grvReturnSearch.Columns["SICODE"].DisplayIndex = 1;
                this.grvReturnSearch.Columns["APPROVEDATE"].DisplayIndex = 2;
                this.grvReturnSearch.Columns["RQCODE"].DisplayIndex = 3;
                this.grvReturnSearch.Columns["CUSNAME"].DisplayIndex = 4;
                this.grvReturnSearch.Columns["GRANDTOT"].DisplayIndex = 5;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool ret = true;
            bool rt = true;
            if (Appz.OpenQuestionDialog("ต้องการลบรายการใช่หรือไม่?") == DialogResult.OK)
            {
                ArrayList arrLOID = new ArrayList();
                ArrayList arr = new ArrayList();
                ReturnSearchFlow csFlow = new ReturnSearchFlow();
                
                for (int i = 0; i < grvReturnSearch.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell maCell = (DataGridViewCheckBoxCell)this.grvReturnSearch.Rows[i].Cells["CHKAPPROVE"];

                    if (maCell.FormattedValue.Equals(true))
                    {
                        RequisitionData dr = new RequisitionData();
                        arrLOID.Add(grvReturnSearch.Rows[i].Cells["SILOID"].Value.ToString());
                       // arr.Add(grvReturnSearch.Rows[i].Cells["RQCODE"].Value.ToString()); 
                        dr.CODE = grvReturnSearch.Rows[i].Cells["RQCODE"].Value.ToString();
                        dr.ACTIVE = "1";
                        dr.STATUS = "AP";
                        arr.Add(dr);
                    }
                }
                ret = csFlow.DeleteStockIn_StockInitemData(Appz.CurrentUserData.UserID, arrLOID);
                if (ret == true)
                {
                    rt = csFlow.UpdateRequisition_Choose(Appz.CurrentUserData.UserID.ToString(), arr);
                    Appz.OpenInformationDialog("ลบรายการเรียบร้อย");
                }
                    
                else
                {
                    Appz.OpenWarningDialog(csFlow.ErrorMessage);
                }
                    
                LoadData();
            }
        }

        private void grvReturnSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (grvReturnSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().ToString() == "System.Windows.Forms.DataGridViewCheckBoxCell")
                {
                    DataGridViewCheckBoxCell maCell = new DataGridViewCheckBoxCell();
                    maCell = (DataGridViewCheckBoxCell)grvReturnSearch.Rows[e.RowIndex].Cells[e.ColumnIndex];

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
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ReturnSearchFlow csFlow = new ReturnSearchFlow();
            StockInData csData = new StockInData();
            csData.SENDER = Appz.CurrentUserData.Warehouse;
            csData.RECEIVER = Appz.CurrentUserData.Warehouse;
            double loid;

            //insert STockin
            loid = csFlow.InsertStockIn(Appz.CurrentUserData.UserID.ToString(), csData);
            if (loid == 0)
                MessageBox.Show(csFlow.ErrorMessage);
            else
                Openfrm(loid, "ADD");

            LoadData();

        }

        private void Openfrm(double loid, string str)
        {
            Transaction.Return frmReturn = new Transaction.Return(loid, str);
            frmReturn.ShowDialog(this);
        }

        private void grvReturnSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.txtSiLoid.Text = grvReturnSearch.Rows[e.RowIndex].Cells["SILOID"].Value.ToString();
                if (grvReturnSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().ToString() == "System.Windows.Forms.DataGridViewLinkCell")
                {
                    double SILOID = Convert.ToDouble(grvReturnSearch.Rows[e.RowIndex].Cells[6].Value);
                    Openfrm(SILOID, "EDIT");
                    StockInData data = new StockInData();
                }
            }
        }
    }
}