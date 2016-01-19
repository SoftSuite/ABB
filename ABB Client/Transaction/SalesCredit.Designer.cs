namespace ABBClient.Transaction
{
    partial class SalesCredit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesCredit));
            this.cmbCreditType = new System.Windows.Forms.ComboBox();
            this.txtCharge = new System.Windows.Forms.TextBox();
            this.txtID1 = new System.Windows.Forms.TextBox();
            this.txtID2 = new System.Windows.Forms.TextBox();
            this.txtID3 = new System.Windows.Forms.TextBox();
            this.txtID4 = new System.Windows.Forms.TextBox();
            this.txtCreditCardPay = new System.Windows.Forms.TextBox();
            this.txtTotalCharge = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbCreditType
            // 
            this.cmbCreditType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreditType.FormattingEnabled = true;
            this.cmbCreditType.Location = new System.Drawing.Point(151, 12);
            this.cmbCreditType.Name = "cmbCreditType";
            this.cmbCreditType.Size = new System.Drawing.Size(169, 21);
            this.cmbCreditType.TabIndex = 0;
            this.cmbCreditType.SelectedIndexChanged += new System.EventHandler(this.cmbCreditType_SelectedIndexChanged);
            // 
            // txtCharge
            // 
            this.txtCharge.Location = new System.Drawing.Point(151, 36);
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.ReadOnly = true;
            this.txtCharge.Size = new System.Drawing.Size(169, 20);
            this.txtCharge.TabIndex = 1;
            this.txtCharge.Text = "0.00";
            this.txtCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtID1
            // 
            this.txtID1.Location = new System.Drawing.Point(151, 59);
            this.txtID1.MaxLength = 4;
            this.txtID1.Name = "txtID1";
            this.txtID1.Size = new System.Drawing.Size(40, 20);
            this.txtID1.TabIndex = 2;
            this.txtID1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtID1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID1_KeyPress);
            this.txtID1.TextChanged += new System.EventHandler(this.txtID1_TextChanged);
            // 
            // txtID2
            // 
            this.txtID2.Location = new System.Drawing.Point(194, 59);
            this.txtID2.MaxLength = 4;
            this.txtID2.Name = "txtID2";
            this.txtID2.Size = new System.Drawing.Size(40, 20);
            this.txtID2.TabIndex = 3;
            this.txtID2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtID2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID2_KeyPress);
            this.txtID2.TextChanged += new System.EventHandler(this.txtID2_TextChanged);
            // 
            // txtID3
            // 
            this.txtID3.Location = new System.Drawing.Point(237, 59);
            this.txtID3.MaxLength = 4;
            this.txtID3.Name = "txtID3";
            this.txtID3.Size = new System.Drawing.Size(40, 20);
            this.txtID3.TabIndex = 4;
            this.txtID3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtID3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID3_KeyPress);
            this.txtID3.TextChanged += new System.EventHandler(this.txtID3_TextChanged);
            // 
            // txtID4
            // 
            this.txtID4.Location = new System.Drawing.Point(280, 59);
            this.txtID4.MaxLength = 4;
            this.txtID4.Name = "txtID4";
            this.txtID4.Size = new System.Drawing.Size(40, 20);
            this.txtID4.TabIndex = 5;
            this.txtID4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtID4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID4_KeyPress);
            this.txtID4.TextChanged += new System.EventHandler(this.txtID4_TextChanged);
            // 
            // txtCreditCardPay
            // 
            this.txtCreditCardPay.Location = new System.Drawing.Point(151, 82);
            this.txtCreditCardPay.Name = "txtCreditCardPay";
            this.txtCreditCardPay.Size = new System.Drawing.Size(169, 20);
            this.txtCreditCardPay.TabIndex = 6;
            this.txtCreditCardPay.Text = "0.00";
            this.txtCreditCardPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCreditCardPay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreditCardPay_KeyPress);
            this.txtCreditCardPay.TextChanged += new System.EventHandler(this.txtCreditCardPay_TextChanged);
            // 
            // txtTotalCharge
            // 
            this.txtTotalCharge.Location = new System.Drawing.Point(151, 105);
            this.txtTotalCharge.Name = "txtTotalCharge";
            this.txtTotalCharge.ReadOnly = true;
            this.txtTotalCharge.Size = new System.Drawing.Size(169, 20);
            this.txtTotalCharge.TabIndex = 7;
            this.txtTotalCharge.Text = "0.00";
            this.txtTotalCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "ชนิดบัตรเครดิต";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "อัตราค่าธรรมเนียม (%)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "เลขที่บัตรเครดิต";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "ยอดชำระ (บาท)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "ค่าธรรมเนียม (บาท)";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(151, 142);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "ตกลง";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SalesCredit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(344, 178);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalCharge);
            this.Controls.Add(this.txtCreditCardPay);
            this.Controls.Add(this.txtID4);
            this.Controls.Add(this.txtID3);
            this.Controls.Add(this.txtID2);
            this.Controls.Add(this.txtID1);
            this.Controls.Add(this.txtCharge);
            this.Controls.Add(this.cmbCreditType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SalesCredit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "รายละอียดบัตรเครดิต";
            this.Load += new System.EventHandler(this.SalesCredit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCreditType;
        private System.Windows.Forms.TextBox txtCharge;
        private System.Windows.Forms.TextBox txtID1;
        private System.Windows.Forms.TextBox txtID2;
        private System.Windows.Forms.TextBox txtID3;
        private System.Windows.Forms.TextBox txtID4;
        private System.Windows.Forms.TextBox txtCreditCardPay;
        private System.Windows.Forms.TextBox txtTotalCharge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
    }
}