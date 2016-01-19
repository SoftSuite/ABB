using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow.Sales;
using ABB.Data;
using ABB.Data.Sales;

namespace ABBClient.Transaction
{
    public partial class SaleInvoice : Form
    {
        private SaleInvoiceFlow _flow;
        private int indexORDERNO = 0;
        private int indexBARCODE = 1;
        private int indexNAME = 2;
        private int indexQTY = 3;
        private int indexUNITNAME = 4;
        private int indexPRICE = 5;
        private int indexDISCOUNT = 6;
        private int indexNETPRICE = 7;
        private int indexISVAT = 8;
        private ABBClient.Reports.PreviewReport _pvReport;

        private SaleInvoiceFlow FlowObj
        {
            get { if (_flow == null) { _flow = new SaleInvoiceFlow(); } return _flow; }
        }

        public SaleInvoice(double requisition)
        {
            InitializeComponent();
            this.txtLOID.Text = requisition.ToString();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvSales, true, false, true);
        }

        private void ResetState()
        {
            double requisition = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
            SetData(FlowObj.GetData(requisition));
        }

        private void SetData(BillData data)
        {
            this.Text += " " + data.CODE;
            this.txtCode.Text = data.CODE;
            this.txtCreateBy.Text = data.CREATEBY;
            this.txtCName.Text = data.CUSTOMERNAME.Trim();
            this.txtDate.Text = data.REQDATE.ToString(Constz.DateFormat);
            this.txtGrandTotal.Text = data.GRANDTOT.ToString(Constz.DblFormat);
            this.txtRefNo.Text = data.REFCODE;
            this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
            this.txtTotalDiscount.Text = data.TOTDIS.ToString(Constz.DblFormat);
            this.txtTotalVat.Text = data.TOTVAT.ToString(Constz.DblFormat);
            this.txtVat.Text = data.VAT.ToString(Constz.IntFormat);
            this.txtCCode.Text = data.CCODE;
            this.txtCName.Text = data.CNAME;
            this.txtCAddress.Text = data.CADDRESS;
            this.txtCTel.Text = data.CTEL;
            this.txtCFax.Text = data.CFAX;
            this.txtCheque.Text = data.CHEQUE;
            if (data.CHEQUEDATE.Year == 1)
                this.dtpChequeDate.Value = DateTime.Today;
            else
                this.dtpChequeDate.Value = data.CHEQUEDATE;
            this.txtBankName.Text = data.BANKNAME;
            this.txtBankBranch.Text = data.BANKBRANCH;
            this.txtCredit.Text = data.CREDITCARDID;
            this.txtReceiveBy.Text = data.RECEIVEBY;
            if (data.RECEIVEDATE.Year == 1)
                this.dtpReceiveDate.Value = DateTime.Today;
            else
                this.dtpReceiveDate.Value = data.RECEIVEDATE;

            this.rdoCash.Checked = (data.PAYMENT == Constz.Payment.Cash.Code);
            this.rdoCheque.Checked = (data.PAYMENT == Constz.Payment.Cheque.Code);
            this.rdoCredit.Checked = (data.PAYMENT == Constz.Payment.CreditCard.Code);

            this.grvSales.Rows.Clear();
            for (int i = 0; i < data.ITEM.Count; ++i)
            {
                RequisitionItemData itemData = (RequisitionItemData)data.ITEM[i];
                DataGridViewRow gRow = (DataGridViewRow)this.grvSales.Rows[this.grvSales.NewRowIndex].Clone();
                gRow.Cells[indexORDERNO].Value = i + 1;
                gRow.Cells[indexBARCODE].Value = itemData.BarCode;
                gRow.Cells[indexNAME].Value = itemData.ProductName;
                gRow.Cells[indexQTY].Value = itemData.QTY;
                gRow.Cells[indexUNITNAME].Value = itemData.UnitName;
                gRow.Cells[indexPRICE].Value = itemData.PRICE;
                gRow.Cells[indexDISCOUNT].Value = itemData.DISCOUNT;
                gRow.Cells[indexNETPRICE].Value = itemData.NETPRICE;
                gRow.Cells[indexISVAT].Value = (itemData.ISVAT == Constz.VAT.Included.Code);
                this.grvSales.Rows.Add(gRow);
            }
            this.grvSales.AllowUserToAddRows = false;
        }

        private BillData GetData()
        {
            BillData data = new BillData();
            data.REQUISITION = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
            data.REFCODE = this.txtRefNo.Text.Trim();
            data.CCODE = this.txtCCode.Text.Trim();
            data.CNAME = this.txtCName.Text.Trim();
            data.CADDRESS = this.txtCAddress.Text.Trim();
            data.CTEL = this.txtCTel.Text.Trim();
            data.CFAX = this.txtCFax.Text.Trim();
            if (rdoCash.Checked)
                data.PAYMENT = Constz.Payment.Cash.Code;
            else if (rdoCheque.Checked)
                data.PAYMENT = Constz.Payment.Cheque.Code;
            else
                data.PAYMENT = Constz.Payment.CreditCard.Code;
            data.CHEQUE = this.txtCheque.Text.Trim();
            data.CREDITCARDID = this.txtCredit.Text.Trim();
            data.CHEQUEDATE = this.dtpChequeDate.Value;
            data.BANKNAME = this.txtBankName.Text.Trim();
            data.BANKBRANCH = this.txtBankBranch.Text.Trim();
            data.RECEIVEBY = this.txtReceiveBy.Text;
            data.RECEIVEDATE = this.dtpReceiveDate.Value;
            return data;
        }

        private void Print()
        {
            if (FlowObj.UpdateData(Appz.CurrentUserData.UserID, GetData()))
            {
                ArrayList arr = new ArrayList();
                ReportParameterData data = new ReportParameterData();
                data.PARAMETERNAME = "LOID";
                data.PARAMETERVALUE = this.txtLOID.Text;
                arr.Add(data);
                _pvReport = new ABBClient.Reports.PreviewReport(Constz.Report.Invoice, arr);
                this.Close();
                _pvReport.ShowDialog();
            }
            else
            {
                Appz.OpenErrorDialog(FlowObj.ErrorMessage);
            }
        }

        private void SetPaymentType()
        {
            this.txtCheque.Enabled = rdoCheque.Checked;
            this.dtpChequeDate.Enabled = rdoCheque.Checked;
            this.txtBankName.Enabled = rdoCheque.Checked;
            this.txtBankBranch.Enabled = rdoCheque.Checked;
            this.txtCredit.Enabled = rdoCredit.Checked;
        }

        private void SaleInvoice_Load(object sender, EventArgs e)
        {
            FormatGridView();
            ResetState();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void rdoCash_Click(object sender, EventArgs e)
        {
            if (!rdoCash.Checked)
            {
                this.rdoCash.Checked = true;
                this.rdoCheque.Checked = false;
                rdoCredit.Checked = false;
                SetPaymentType();
            }
        }

        private void rdoCheque_Click(object sender, EventArgs e)
        {
            if (!rdoCheque.Checked)
            {
                this.rdoCheque.Checked = true;
                this.rdoCash.Checked = false;
                rdoCredit.Checked = false;
                SetPaymentType();
            }
        }
        private void rdoCredit_Click(object sender, EventArgs e)
        {
            if (!rdoCredit.Checked)
            {
                this.rdoCheque.Checked = false;
                this.rdoCash.Checked = false;
                rdoCredit.Checked = true;
                SetPaymentType();
            }
        }
        private void SaleInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F10:
                    Print();
                    e.Handled = true;
                    break;
            }
        }

    }
}