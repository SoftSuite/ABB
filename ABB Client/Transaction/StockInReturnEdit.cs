using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow.Sales;
using ABB.Data.Sales;
using ABB.Data;

namespace ABBClient.Transaction
{
    public partial class StockInReturnEdit : Form
    {
        private ABBClient.Reports.PreviewReport _pvReport;
        private StockInReturnFlow _flow;
        private Search.ReturnPopup frmSearch;
        private int indexORDERNO = 0;
        private int indexBARCODE = 1;
        private int indexPRODUCTNAME = 2;
        private int indexINVQTY = 3;
        private int indexQTY = 4;
        private int indexUNITNAME = 5;
        private int indexPRICE = 6;
        private int indexTOTAL = 7;
        private int indexREFLOID = 8;
        private int indexPRODUCT = 9;
        private int indexUNIT = 10;
        private int indexTOTALDIS = 11;

        private StockInReturnFlow FlowObj
        {
            get { if (_flow == null) { _flow = new StockInReturnFlow(); } return _flow; }
        }

        public StockInReturnEdit(double stockIn)
        {
            InitializeComponent();
            this.txtLOID.Text = stockIn.ToString();
        }

        public StockInReturnEdit()
        {
            InitializeComponent();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvStockIn, false, true, false);
        }

        private void ResetState()
        {
            SetData(FlowObj.GetStockInData(Convert.ToDouble(this.txtLOID.Text)));
        }

        private void Calculate(string customer)
        {
            double total = 0;
           // double DISCOUNT = 0;
            foreach (DataGridViewRow gRow in this.grvStockIn.Rows)
            {
                total += Convert.ToDouble(gRow.Cells[indexTOTAL].Value);
            }
           // DISCOUNT = FlowObj.GetDiscount(customer);
           // this.txtTotal.Text = ((total * (100 - DISCOUNT))/100).ToString(Constz.DblFormat);
            this.txtTotal.Text = total.ToString();
        }

        private void SetDatagrid()
        {
            this.ORDERNO.ReadOnly = true;
            this.BARCODE.ReadOnly = true;
            this.PRODUCTNAME.ReadOnly = true;
            this.INVQTY.ReadOnly = true;
            this.UNITNAME.ReadOnly = true;
            this.PRICE.ReadOnly = true;
            this.TOTAL.ReadOnly = true;
        }

        private void SetData(StockInReturnData data)
        {
            if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
            {
                this.btnSearch.Visible = false;
                this.btnSubmit.Visible = false;
                this.txtInvoiceCode.ReadOnly = true;
                this.txtInvoiceCode.Enabled = false;
                Appz.FormatDataGridView(this.grvStockIn, false, false, true);
            }
            else
                Appz.FormatDataGridView(this.grvStockIn, false, true, false);

            this.txtInvoiceCode.Text = data.INVOICECODE;
            this.txtOldInvoiceCode.Text = data.INVOICECODE;
            this.txtLOID.Text = data.LOID.ToString();
            this.txtRefLOID.Text = data.REFLOID.ToString();
            this.txtReceiver.Text = data.RECEIVER.ToString();
            this.txtSender.Text = data.SENDER.ToString();
            this.txtStatus.Text = data.STATUS;
            this.txtCode.Text = data.CODE;
            this.txtReceiveDate.Text = data.RECEIVEDATE.ToString(Constz.DateFormat);
            this.dtpReceiveDate.Value = data.RECEIVEDATE;
            this.txtCreateBy.Text = data.CREATEBY;
            this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
            this.txtRemark.Text = data.REMARK;
            this.txtReason.Text = data.REASON;
            this.txtCustomerCode.Text = data.CUSTOMERCODE;
            this.txtCustomerName.Text = data.CUSTOMERNAME;
            if (data.INVOICEDATE.Year == 1)
                this.txtInvoiceDate.Text = "";
            else
                this.txtInvoiceDate.Text = data.INVOICEDATE.ToString(Constz.DateFormat);
            this.grvStockIn.DataSource = data.ITEM;
            SetDatagrid();
        }

        private StockInData GetData()
        {
            StockInData data = new StockInData();
            data.LOID = Convert.ToDouble(this.txtLOID.Text);
            data.CODE = this.txtCode.Text.Trim();
            data.GRANDTOT = Convert.ToDouble(this.txtTotal.Text);
            data.RECEIVEDATE = this.dtpReceiveDate.Value.Date;
            data.RECEIVER = Convert.ToDouble(this.txtReceiver.Text);
            data.REMARK = this.txtRemark.Text.Trim();
            data.SENDER = Convert.ToDouble(this.txtSender.Text);
            data.STATUS = this.txtStatus.Text;
            data.REFTABLE = "REQUISITION";
            data.REFLOID = Convert.ToDouble(this.txtRefLOID.Text);
            data.REASON = this.txtReason.Text.Trim();
            foreach (DataGridViewRow gRow in this.grvStockIn.Rows)
            {
                StockInItemData itemData = new StockInItemData();
                itemData.PRICE = Convert.ToDouble(gRow.Cells[indexPRICE].Value);
                itemData.PRODUCT = Convert.ToDouble(gRow.Cells[indexPRODUCT].Value);
                itemData.QTY = Convert.ToDouble(gRow.Cells[indexQTY].Value);
                itemData.UNIT = Convert.ToDouble(gRow.Cells[indexUNIT].Value);
                itemData.REFLOID = Convert.ToDouble(gRow.Cells[indexREFLOID].Value);
                itemData.REFTABLE = "REQUISITIONITEM";
                data.STOCKINITEM.Add(itemData);
            }
            return data;
        }

        private void SetStockOutData(StockInReturnData data)
        {
            this.txtInvoiceCode.Text = data.INVOICECODE;
            this.txtOldInvoiceCode.Text = data.INVOICECODE;
            this.txtRefLOID.Text = data.REFLOID.ToString();
            this.txtReceiver.Text = data.RECEIVER.ToString();
            this.txtSender.Text = data.SENDER.ToString();
            this.txtCustomerCode.Text = data.CUSTOMERCODE;
            this.txtCustomerName.Text = data.CUSTOMERNAME;
            if (data.INVOICEDATE.Year == 1)
                this.txtInvoiceDate.Text = "";
            else
                this.txtInvoiceDate.Text = data.INVOICEDATE.ToString(Constz.DateFormat);
            this.grvStockIn.DataSource = data.ITEM;
            SetDatagrid();
            Calculate(data.CUSTOMERCODE);
        }

        private void StockInReturnEdit_Load(object sender, EventArgs e)
        {
            ResetState();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Appz.OpenQuestionDialog("ต้องการยืนยันรายการใช่หรือไม่?") == DialogResult.OK)
            {
                if (FlowObj.CommitData(GetData(), Appz.CurrentUserData.UserID))
                {
                    this.txtLOID.Text = FlowObj.LOID.ToString();
                    ResetState();
                    Appz.OpenInformationDialog("ยืนยันรายการเรียบร้อยแล้ว");
                }
                else
                    Appz.OpenErrorDialog(FlowObj.ErrorMessage);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ArrayList arr = new ArrayList();
            ReportParameterData data = new ReportParameterData();
            data.PARAMETERNAME = "LOID";
            data.PARAMETERVALUE = this.txtLOID.Text;
            arr.Add(data);
            _pvReport = new ABBClient.Reports.PreviewReport(Constz.Report.StockInReturn, arr);
            _pvReport.ShowDialog();
        }

        private void txtInvoiceCode_Leave(object sender, EventArgs e)
        {
            if (this.txtOldInvoiceCode.Text != this.txtInvoiceCode.Text)
            {
                if (txtInvoiceCode.Text.Trim() == "")
                    SetStockOutData(new StockInReturnData());
                else
                    SetStockOutData(FlowObj.GetInvoiceData(this.txtInvoiceCode.Text.Trim()));
            }
        }

        private void txtInvoiceCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13)) this.btnSearch.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (frmSearch == null || frmSearch.IsDisposed) { frmSearch = new ABBClient.Search.ReturnPopup(); }
            if (frmSearch.ShowDialog() == DialogResult.OK)
            {
                SetStockOutData(FlowObj.GetInvoiceData(frmSearch.RequisitionID));
            }
        }

        private void grvStockIn_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == indexQTY)
            {
                this.grvStockIn[indexTOTAL, e.RowIndex].Value = Convert.ToDouble(this.grvStockIn[indexQTY, e.RowIndex].Value) * (Convert.ToDouble(this.grvStockIn[indexPRICE, e.RowIndex].Value) - Convert.ToDouble(this.grvStockIn[indexTOTALDIS, e.RowIndex].Value));
                Calculate(this.txtCustomerCode.Text.Trim());
            }
        }

        private void grvStockIn_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl txtBox = (DataGridViewTextBoxEditingControl)e.Control;

                txtBox.KeyPress -= new KeyPressEventHandler(Control_KeyPress);

                if (this.grvStockIn.CurrentCell.OwningColumn.Index == indexQTY)
                {
                    txtBox.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                }
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        private void grvStockIn_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (Convert.ToDouble(this.grvStockIn[indexQTY, e.RowIndex].Value) > Convert.ToDouble(this.grvStockIn[indexINVQTY, e.RowIndex].Value))
                {
                    Appz.OpenErrorDialog("จำนวนที่รับคืนต้องไม่เกินจำนวนที่ซื้อไป");
                    this.grvStockIn[indexQTY, e.RowIndex].Selected = true;
                    e.Cancel = true;
                }
                else if (Convert.ToDouble(this.grvStockIn[indexQTY, e.RowIndex].Value) == 0)
                {
                    Appz.OpenErrorDialog("กรุณาระบุจำนวนที่รับคืน");
                    this.grvStockIn[indexQTY, e.RowIndex].Selected = true;
                    e.Cancel = true;
                }
            }
        }

        private void grvStockIn_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Calculate(this.txtCustomerCode.Text.Trim());
        }

    }
}