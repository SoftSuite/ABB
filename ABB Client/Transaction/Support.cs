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
using ABB.Data;

namespace ABBClient.Transaction
{
    public partial class Support : Form
    {
        public Support()
        {
            InitializeComponent();
        }

        string str_DateFrom;
        string str_DateTo;

        private void Support_Load(object sender, EventArgs e)
        {
            dpReserveDateFrom.Value = DateTime.Today;
            dpReserveDateTo.Value = DateTime.Today;
            Appz.BuildRequisitionStatusCombo(this.cmbRqStatusFrom);
            //Appz.BuildRequisitionStatusCombo(this.cmbRqStatusTo);
            Appz.BuildCombo(this.cmbCustomer, "V_CUSTOMER", "CUSTOMERNAME", "LOID", "CUSTOMERNAME", "", "ทั้งหมด", "0");
            LoadData();   
        
        }

        private void LoadData()
        {
            FormatGridView();
            SetDate();
            this.grvSupportSearch.DataSource = SupportSearchFlow.GetSupportSearch(Appz.CurrentUserData.Warehouse.ToString(), txtRqCode.Text.Trim(), str_DateFrom, str_DateTo, cmbCustomer.SelectedValue.ToString(), cmbRqStatusFrom.SelectedValue.ToString());            
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
            str = Convert.ToString(dpReserveDateFrom.Value.Day) + '/';
            str += Convert.ToString(dpReserveDateFrom.Value.Month) + '/';
            str += Convert.ToString(dpReserveDateFrom.Value.Year);
            return str;
        }

        private string getDateTo()
        {
            string str;
            str = "";
            str = Convert.ToString(dpReserveDateTo.Value.Day) + '/';
            str += Convert.ToString(dpReserveDateTo.Value.Month) + '/';
            str += Convert.ToString(dpReserveDateTo.Value.Year);
            return str;
        }

        private string getDateToday()
        {
            string str;
            str = "";
            str = Convert.ToString(DateTime.Now.Day) + '/';
            str += Convert.ToString(DateTime.Now.Month) + '/';
            str += Convert.ToString(DateTime.Now.Year+ 543);
            return str;
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvSupportSearch, false, false,true);
        }

        private void btnSearchSupport_Click(object sender, EventArgs e)
        {
            SetDate();
            if (cmbRqStatusFrom.Text.ToString() == "")
            {              
                DataTable dt = SupportSearchFlow.GetSupportSearch(Appz.CurrentUserData.Warehouse.ToString(), txtRqCode.Text.Trim(), str_DateFrom, str_DateTo, cmbCustomer.SelectedValue.ToString(), "");
                this.grvSupportSearch.DataSource = dt;
            }
            else
            {
                DataTable dt = SupportSearchFlow.GetSupportSearch(Appz.CurrentUserData.Warehouse.ToString(), txtRqCode.Text.Trim(), str_DateFrom, str_DateTo, cmbCustomer.SelectedValue.ToString(), cmbRqStatusFrom.SelectedValue.ToString());
                this.grvSupportSearch.DataSource = dt;
            }   
        }

        private void grvSupportSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.txtRqLoid.Text = grvSupportSearch.Rows[e.RowIndex].Cells["RQLOID"].Value.ToString();
                if (grvSupportSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().ToString() == "System.Windows.Forms.DataGridViewLinkCell")
                {
                    double RQLOID = Convert.ToDouble(grvSupportSearch.Rows[e.RowIndex].Cells[7].Value);
                    Openfrm(RQLOID);
                }
            }
        }

        private void Openfrm(double loid)
        {
            Transaction.SupportEdit frmSupportEdit = new Transaction.SupportEdit(loid);
            frmSupportEdit.ShowDialog(this);
            LoadData();
        }

        //private void Openfrm(double loid, string str)
        //{
        //    Transaction.SupportEdit frmSupportEdit = new Transaction.SupportEdit(loid, str);
        //    frmSupportEdit.ShowDialog(this);
        //    LoadData();
        //}

        private void grvSupportSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (grvSupportSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().ToString() == "System.Windows.Forms.DataGridViewCheckBoxCell")
                {
                    if (grvSupportSearch.Rows[e.RowIndex].Cells["RQSTATUS"].Value.ToString() != Constz.Requisition.Status.Waiting.Code)
                    {
                        Appz.OpenErrorDialog("ไม่สามารถเลือกรายการที่ไม่อยู่ในสถานะทำรายการได้");
                    }
                    else
                    {
                        DataGridViewCheckBoxCell maCell = new DataGridViewCheckBoxCell();
                        maCell = (DataGridViewCheckBoxCell)grvSupportSearch.Rows[e.RowIndex].Cells[e.ColumnIndex];

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

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (Appz.OpenQuestionDialog("ต้องการสร้างรายการใหม่ใช่หรือไม่") == DialogResult.OK)
            {
                SupportSearchFlow csFlow = new SupportSearchFlow();
                RequisitionData csData = new RequisitionData();
                string str_today = getDateToday();
                csData.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ04;
                csData.STATUS = Constz.Requisition.Status.Waiting.Code;
                csData.REQDATE = Convert.ToDateTime(str_today);
                csData.WAREHOUSE = Appz.CurrentUserData.Warehouse;
                csData.RESERVEDATE = Convert.ToDateTime(str_today);
                csData.ACTIVE = Constz.ActiveStatus.Active;
                csData.VAT = Convert.ToDouble(ABB.Flow.SysConfigFlow.GetValue(Constz.ConfigName.VAT));
                
                double loid;

                //insert Requisition
                loid = csFlow.InsertRequisition(Appz.CurrentUserData.UserID.ToString(), csData);
                if (loid == 0)
                    MessageBox.Show(csFlow.ErrorMessage);
                else
                    Openfrm(loid);

                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool ret = true;
            if (Appz.OpenQuestionDialog("ต้องการลบรายการใช่หรือไม่?") == DialogResult.OK)
            {
                ArrayList arrLOID = new ArrayList();
                ArrayList arr = new ArrayList();
                SupportSearchFlow csFlow = new SupportSearchFlow();

                for (int i = 0; i < grvSupportSearch.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell maCell = (DataGridViewCheckBoxCell)this.grvSupportSearch.Rows[i].Cells["CHKAPPROVE"];

                    if (maCell.FormattedValue.Equals(true))
                    {
                        RequisitionData dr = new RequisitionData();
                        arrLOID.Add(grvSupportSearch.Rows[i].Cells["RQLOID"].Value.ToString());
                        arr.Add(dr);
                    }
                }
                ret = csFlow.DeleteReq_ReqItemData(arrLOID);
                if (ret == true)
                {
                    Appz.OpenInformationDialog("ลบรายการเรียบร้อย");
                }

                else
                {
                    Appz.OpenWarningDialog(csFlow.ErrorMessage);
                }

                LoadData();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            bool ret = true;
            if (Appz.OpenQuestionDialog("ต้องการส่งฝ่ายขายใช่หรือไม่?") == DialogResult.OK)
            {
                ArrayList arrLOID = new ArrayList();
                ArrayList arr = new ArrayList();
                SupportSearchFlow csFlow = new SupportSearchFlow();

                for (int i = 0; i < grvSupportSearch.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell maCell = (DataGridViewCheckBoxCell)this.grvSupportSearch.Rows[i].Cells["CHKAPPROVE"];
                    if (maCell.FormattedValue.Equals(true))
                    {
                        RequisitionData dr = new RequisitionData();
                        arrLOID.Add(grvSupportSearch.Rows[i].Cells["RQLOID"].Value.ToString());
                        dr.ACTIVE = "1";
                        dr.STATUS = "AP";
                        arr.Add(dr);
                    }
                }
                ret = csFlow.RequisitionApprove(Appz.CurrentUserData.UserID, arrLOID);
                if (ret == true)
                {
                    Appz.OpenInformationDialog("ส่งฝ่ายขายเรียบร้อย");
                }
                else
                {
                    Appz.OpenWarningDialog(csFlow.ErrorMessage);
                }
                LoadData();
            }
        }  
    }
}