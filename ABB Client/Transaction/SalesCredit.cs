using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Data;
using ABB.Flow.Sales;
using ABB.Data.Sales;

namespace ABBClient.Transaction
{
    public partial class SalesCredit : Form
    {
        SalePayData _data;

        public SalesCredit(SalePayData data)
        {
            InitializeComponent();
            setCombobox();
            _data = data;

            this.txtCreditCardPay.Text = data.CREDITCARDPAY.ToString(Constz.DblFormat);
            if (this.cmbCreditType.SelectedValue != null)
            {
                this.cmbCreditType.SelectedValue = data.CREDITTYPE;
                if (data.CREDITCARDID.Length > this.txtID1.MaxLength)
                {
                    this.txtID1.Text = data.CREDITCARDID.Substring(0, this.txtID1.MaxLength);
                    data.CREDITCARDID = data.CREDITCARDID.Substring(4);
                }
                else
                    this.txtID1.Text = data.CREDITCARDID;

                if (data.CREDITCARDID.Length > this.txtID2.MaxLength)
                {
                    this.txtID2.Text = data.CREDITCARDID.Substring(0, this.txtID2.MaxLength);
                    data.CREDITCARDID = data.CREDITCARDID.Substring(4);
                }
                else
                    this.txtID2.Text = data.CREDITCARDID;

                if (data.CREDITCARDID.Length > this.txtID3.MaxLength)
                {
                    this.txtID3.Text = data.CREDITCARDID.Substring(0, this.txtID3.MaxLength);
                    data.CREDITCARDID = data.CREDITCARDID.Substring(4);
                }
                else
                    this.txtID3.Text = data.CREDITCARDID;

                if (data.CREDITCARDID.Length > this.txtID4.MaxLength)
                {
                    this.txtID4.Text = data.CREDITCARDID.Substring(0, this.txtID4.MaxLength);
                }
                else
                    this.txtID4.Text = data.CREDITCARDID;

                SetCardDetail();
            }
        }

        public SalesCredit()
        {
            InitializeComponent();
            setCombobox();
        }

        private void setCombobox()
        {
            Appz.BuildCombo(this.cmbCreditType, "CREDITCARD", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ");
        }

        private void SetCardDetail()
        {
            if (this.cmbCreditType.SelectedValue == null)
            {
                this.txtCharge.Text = "0.00";
            }
            else
            {
                PointOfSaleFlow flow = new PointOfSaleFlow();
                try
                {
                    CreditCardData data = flow.GetCreditCardData(Convert.ToDouble(this.cmbCreditType.SelectedValue));
                    this.txtCharge.Text = data.CHARGE.ToString(Constz.DblFormat);
                }
                catch
                {
                    this.txtCharge.Text = "0.00";
                }
            }
            CalculateCharge();
        }

        private void CalculateCharge()
        {
            this.txtTotalCharge.Text = (Convert.ToDouble(this.txtCharge.Text) * Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text) / 100).ToString(Constz.DblFormat);
        }

        public SalePayData GetData()
        {
            SalePayData data = new SalePayData();
            data.CREDITCARDID = this.txtID1.Text.Trim() + this.txtID2.Text.Trim() + this.txtID3.Text.Trim() + this.txtID4.Text.Trim();
            data.CREDITCARDPAY = Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text);
            if (this.cmbCreditType.SelectedValue != null) data.CREDITTYPE = Convert.ToDouble(this.cmbCreditType.SelectedValue);
            return data;
        }

        private void txtID1_TextChanged(object sender, EventArgs e)
        {
            if (this.txtID1.Text.Length == this.txtID1.MaxLength)
            {
                this.txtID2.Focus();
            }
            else
            {
                this.txtID2.Text = "";
                this.txtID3.Text = "";
                this.txtID4.Text = "";
            }
        }

        private void txtID2_TextChanged(object sender, EventArgs e)
        {
            if (this.txtID2.Text.Length == this.txtID2.MaxLength)
            {
                this.txtID3.Focus();
            }
            else
            {
                this.txtID3.Text = "";
                this.txtID4.Text = "";
            }
        }

        private void txtID3_TextChanged(object sender, EventArgs e)
        {
            if (this.txtID3.Text.Length == this.txtID3.MaxLength)
            {
                this.txtID4.Focus();
            }
            else
            {
                this.txtID4.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text) == 0)
                Appz.OpenErrorDialog("กรุณาระบุจำนวนเงินที่ชำระ");
            else if (Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text) + _data.COUPON > _data.GRANDTOTAL)
                Appz.OpenErrorDialog("ยอดเงินชำระบัตรเครดิต ต้องไม่เกิน ผลต่างของยอดที่ต้องชำระกับคูปอง");
            else if ((this.cmbCreditType.SelectedValue != null ) &&(txtID1.Text.Trim() == "" || txtID2.Text.Trim() == "" || txtID3.Text.Trim() == "" || txtID4.Text.Trim() == ""))
                Appz.OpenErrorDialog("กรุณาระบุเลขที่บัตรเครดิต");
            else
                this.DialogResult = DialogResult.OK;
        }

        private void SalesCredit_Load(object sender, EventArgs e)
        {
            
        }

        private void cmbCreditType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCardDetail();
        }

        private void txtCreditCardPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetDBlTextBox(sender, e);
        }

        private void txtID1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        private void txtID2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        private void txtID3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        private void txtID4_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        private void txtID4_TextChanged(object sender, EventArgs e)
        {
            if (this.txtID4.Text.Length == this.txtID4.MaxLength)
            {
                this.txtCreditCardPay.Focus();
            }
        }

        private void txtCreditCardPay_TextChanged(object sender, EventArgs e)
        {
            CalculateCharge();
        }

    }
}