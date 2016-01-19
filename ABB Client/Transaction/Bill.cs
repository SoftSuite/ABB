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
    public partial class Bill : Form
    {
        private BillFlow _flow;
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
        private App_Code.POSPrinter _print;

        private BillFlow FlowObj
        {
            get { if (_flow == null) { _flow = new BillFlow(); } return _flow; }
        }

        public App_Code.POSPrinter PrintObj
        {
            get { if (_print == null) { _print = new ABBClient.App_Code.POSPrinter(); } return _print; }
        }

        public Bill()
        {
            InitializeComponent();
        }

        public Bill(double requisition)
        {
            InitializeComponent();
            this.txtLOID.Text = requisition.ToString();
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvSales, true, false, true);
        }

        private void PrintData()
        {
            try
            {
                PrintObj.Print(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
            }
            catch (Exception ex)
            {
                Appz.OpenErrorDialog(ex.Message);
            }
        }

        private void ResetState()
        {
            double requisition = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
            BillData data = FlowObj.GetData(requisition);
            this.Text += " " + data.CODE;
            this.txtCode.Text = data.CODE;
            this.txtCreateBy.Text = data.CREATEBY;
            this.txtCustomerName.Text = data.CUSTOMERNAME.Trim();
            this.txtDate.Text = data.REQDATE.ToString(Constz.DateFormat);
            this.txtGrandTotal.Text = data.GRANDTOT.ToString(Constz.DblFormat);
            this.txtRefCode.Text = data.REFCODE;
            this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
            this.txtTotalDiscount.Text = data.TOTDIS.ToString(Constz.DblFormat);
            this.txtTotalVat.Text = data.TOTVAT.ToString(Constz.DblFormat);
            this.txtVat.Text = data.VAT.ToString(Constz.IntFormat);
            this.lblNetAmount.Text = data.GRANDTOT.ToString(Constz.IntFormat);

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

        private void Print()
        {
            SaleInvoice frmInvoice = new SaleInvoice(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
            //this.Close();
            frmInvoice.ShowDialog();
        }

        private void Bill_Load(object sender, EventArgs e)
        {
            FormatGridView();
            ResetState();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void Bill_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F9:
                    PrintData();
                    e.Handled = true;
                    break;

                case Keys.F10:
                    Print();
                    e.Handled = true;
                    break;
            }
        }

    }
}