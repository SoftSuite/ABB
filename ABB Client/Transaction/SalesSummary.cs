using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using ABB.Data;
using ABB.Data.Sales;

namespace ABBClient.Transaction
{
    public partial class SalesSummary : Form
    {
        private SalePayData _data;

        public SalePayData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public SalesSummary()
        {
            InitializeComponent();
        }

        public SalesSummary(SalePayData data)
        {
            InitializeComponent();
            Data = data;
        }

        public SalesSummary(double GrandTotal)
        {
            InitializeComponent();
            this.lblGrandTotal.Text = GrandTotal.ToString(Constz.DblFormat);
            this.txtCash.Text = this.lblGrandTotal.Text;
            this.txtCash.Focus();
        }

        private void CalculateChange()
        {
            double grandTotal = Convert.ToDouble(this.lblGrandTotal.Text == "" ? "0" : this.lblGrandTotal.Text);
            double cash = Convert.ToDouble(this.txtCash.Text == "" ? "0" : this.txtCash.Text);
            double creditPay = Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text);
            double coupon = Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text);
            if (coupon + creditPay > grandTotal)
            {
                this.txtCash.Text = "0.00";
                this.lblChange.Text = "0.00";
            }
            else
            {
                this.lblChange.Text = (Convert.ToDouble(this.txtCash.Text == "" ? "0" : this.txtCash.Text) + Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text) +
                    Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text) - Convert.ToDouble(this.lblGrandTotal.Text == "" ? "0" : this.lblGrandTotal.Text)).ToString(Constz.DblFormat);
            }
        }

        private void PrintData()
        {
            double grandTotal = Convert.ToDouble(this.lblGrandTotal.Text == "" ? "0" : this.lblGrandTotal.Text);
            double cash = Convert.ToDouble(this.txtCash.Text == "" ? "0" : this.txtCash.Text);
            double creditPay = Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text);
            double coupon = Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text);
            if (cash + creditPay + coupon < grandTotal)
                Appz.OpenErrorDialog("จำนวนเงินที่รับน้อยกว่ายอดเงินรวม");
            else if (creditPay + coupon > grandTotal)
                Appz.OpenErrorDialog("ยอดเงินชำระบัตรเครดิต ต้องไม่เกิน ผลต่างของยอดที่ต้องชำระกับคูปอง");
            else
            {
                Data.CASH = cash;
                Data.COUPON = coupon;
                Data.CREDITCARDPAY = creditPay;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void SetCreditCard()
        {
            if (Data.GRANDTOTAL - Convert.ToDouble(this.txtCash.Text == "" ? "0" : this.txtCash.Text) - Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text) < 0)
                Data.CREDITCARDPAY = 0;
            else
                Data.CREDITCARDPAY = Data.GRANDTOTAL - Convert.ToDouble(this.txtCash.Text == "" ? "0" : this.txtCash.Text) - Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text);
            Data.COUPON = Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text);
            Transaction.SalesCredit frmCredit = new SalesCredit(Data);
            if (frmCredit.ShowDialog() == DialogResult.OK)
            {
                SalePayData frmData = frmCredit.GetData();
                Data.CREDITCARDID = frmData.CREDITCARDID;
                Data.CREDITCARDPAY = frmData.CREDITCARDPAY;
                Data.CREDITTYPE = frmData.CREDITTYPE;
                Data.CASH = Data.GRANDTOTAL - frmData.CREDITCARDPAY - Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text);
                if (Data.CASH < 0) Data.CASH = 0;

                this.txtCreditCardPay.Text = frmData.CREDITCARDPAY.ToString(Constz.DblFormat);
                this.txtCash.Text = Data.CASH.ToString(Constz.DblFormat);
                CalculateChange();
            }
        }

        private void OpenDrawer()
        {
            try
            {
                SerialPort sp = new SerialPort(ConfigurationManager.AppSettings["PORT"].ToString(), 9600);
                sp.Open();
                sp.ReadTimeout = 500;
                sp.WriteLine("NOP");
                sp.Close();
            }
            catch
            {
            }
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetDBlTextBox(sender, e);
        }

        private void txtCoupon_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetDBlTextBox(sender, e);
        }

        private void SalesSummary_Load(object sender, EventArgs e)
        {
            this.lblGrandTotal.Text = Data.GRANDTOTAL.ToString(Constz.DblFormat);
            this.txtCash.Text = Data.CASH.ToString(Constz.DblFormat);
            this.txtCreditCardPay.Text = Data.CREDITCARDPAY.ToString(Constz.DblFormat);
            this.txtCoupon.Text = Data.COUPON.ToString(Constz.DblFormat);
            CalculateChange();
        }

        private void btnCreditDetail_Click(object sender, EventArgs e)
        {
            SetCreditCard();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            double grandTotal = Convert.ToDouble(this.lblGrandTotal.Text == "" ? "0" : this.lblGrandTotal.Text);
            double cash = Convert.ToDouble(this.txtCash.Text == "" ? "0" : this.txtCash.Text);
            double creditPay = Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text);
            double coupon = Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text);
            if (cash + creditPay + coupon < grandTotal)
                Appz.OpenErrorDialog("จำนวนเงินที่รับน้อยกว่ายอดเงินรวม");
            else if (creditPay +coupon > grandTotal)
                Appz.OpenErrorDialog("ยอดเงินชำระบัตรเครดิต ต้องไม่เกิน ผลต่างของยอดที่ต้องชำระกับคูปอง");
            else
            {
                Data.CASH = cash;
                Data.COUPON = coupon;
                Data.CREDITCARDPAY = creditPay;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void txtCoupon_TextChanged(object sender, EventArgs e)
        {
            double grandTotal = Convert.ToDouble(this.lblGrandTotal.Text == "" ? "0" : this.lblGrandTotal.Text);
            double cash = Convert.ToDouble(this.txtCash.Text == "" ? "0" : this.txtCash.Text);
            double creditPay = Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text);
            double coupon = Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text);
            if (coupon > grandTotal)
            {
                this.txtCash.Text = "0.00";
                this.txtCreditCardPay.Text = "0.00";

                Data.CASH = 0;
                Data.CREDITCARDID = "";
                Data.CREDITCARDPAY = 0;
                Data.CREDITTYPE = 0;
                this.lblChange.Text = "0.00";
            }
            else
            {
                this.txtCash.Text = (grandTotal - creditPay - coupon).ToString(Constz.DblFormat);
                 CalculateChange();
            }
        }

        private void SalesSummary_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    SetCreditCard();
                    e.Handled = true;
                    break;

                case Keys.F9:
                    OpenDrawer();
                    e.Handled = true;
                    break;

                case Keys.F10 :
                    PrintData();
                    e.Handled = true;
                    break;

            }
        }
    }
}