namespace ABBClient.Search
{
    partial class ReturnPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReturnPopup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtRequisition = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCodeTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.txtCodeFrom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.grvRequisition = new System.Windows.Forms.DataGridView();
            this.ORDERNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INVOICECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INVOICEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTOMERCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTOMERNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAREHOUSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REQUISITION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvRequisition)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(218)))), ((int)(((byte)(169)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(554, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ค้นหา";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dtpDateTo);
            this.panel1.Controls.Add(this.dtpDateFrom);
            this.panel1.Controls.Add(this.txtRequisition);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtCodeTo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cmbProduct);
            this.panel1.Controls.Add(this.txtCustomerCode);
            this.panel1.Controls.Add(this.txtCodeFrom);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(5, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 113);
            this.panel1.TabIndex = 1;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(301, 33);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(120, 20);
            this.dtpDateTo.TabIndex = 20;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(150, 33);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(120, 20);
            this.dtpDateFrom.TabIndex = 19;
            // 
            // txtRequisition
            // 
            this.txtRequisition.Location = new System.Drawing.Point(510, 13);
            this.txtRequisition.Name = "txtRequisition";
            this.txtRequisition.Size = new System.Drawing.Size(36, 20);
            this.txtRequisition.TabIndex = 20;
            this.txtRequisition.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(276, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "ถึง";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(59, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "วันที่ออกใบเสร็จ";
            // 
            // txtCodeTo
            // 
            this.txtCodeTo.Location = new System.Drawing.Point(301, 9);
            this.txtCodeTo.Name = "txtCodeTo";
            this.txtCodeTo.Size = new System.Drawing.Size(120, 20);
            this.txtCodeTo.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(276, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "ถึง";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(445, 79);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(25, 25);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(150, 82);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(271, 21);
            this.cmbProduct.TabIndex = 10;
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(150, 58);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(271, 20);
            this.txtCustomerCode.TabIndex = 9;
            // 
            // txtCodeFrom
            // 
            this.txtCodeFrom.Location = new System.Drawing.Point(150, 9);
            this.txtCodeFrom.Name = "txtCodeFrom";
            this.txtCodeFrom.Size = new System.Drawing.Size(120, 20);
            this.txtCodeFrom.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "เลขที่สมาชิก";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "สินค้า";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "เลขที่ใบเสร็จ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grvRequisition, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 388);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnSelect);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 358);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(564, 30);
            this.panel2.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(304, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "ปิดหน้าต่าง";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(223, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "เลือก";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // grvRequisition
            // 
            this.grvRequisition.AllowUserToAddRows = false;
            this.grvRequisition.AllowUserToDeleteRows = false;
            this.grvRequisition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grvRequisition.BackgroundColor = System.Drawing.Color.Silver;
            this.grvRequisition.ColumnHeadersHeight = 25;
            this.grvRequisition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grvRequisition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORDERNO,
            this.INVOICECODE,
            this.INVOICEDATE,
            this.CUSTOMERCODE,
            this.CUSTOMERNAME,
            this.WAREHOUSE,
            this.REQUISITION});
            this.grvRequisition.Location = new System.Drawing.Point(5, 148);
            this.grvRequisition.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.grvRequisition.MultiSelect = false;
            this.grvRequisition.Name = "grvRequisition";
            this.grvRequisition.ReadOnly = true;
            this.grvRequisition.RowHeadersWidth = 25;
            this.grvRequisition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvRequisition.Size = new System.Drawing.Size(552, 205);
            this.grvRequisition.TabIndex = 4;
            this.grvRequisition.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvRequisition_CellDoubleClick);
            this.grvRequisition.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvRequisition_CellClick);
            // 
            // ORDERNO
            // 
            this.ORDERNO.DataPropertyName = "ORDERNO";
            this.ORDERNO.HeaderText = "ลำดับ";
            this.ORDERNO.Name = "ORDERNO";
            this.ORDERNO.ReadOnly = true;
            this.ORDERNO.Width = 50;
            // 
            // INVOICECODE
            // 
            this.INVOICECODE.DataPropertyName = "INVOICECODE";
            this.INVOICECODE.HeaderText = "เลขที่ใบเสร็จ";
            this.INVOICECODE.Name = "INVOICECODE";
            this.INVOICECODE.ReadOnly = true;
            // 
            // INVOICEDATE
            // 
            this.INVOICEDATE.DataPropertyName = "INVOICEDATE";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.INVOICEDATE.DefaultCellStyle = dataGridViewCellStyle1;
            this.INVOICEDATE.HeaderText = "วันที่ออกใบเสร็จ";
            this.INVOICEDATE.Name = "INVOICEDATE";
            this.INVOICEDATE.ReadOnly = true;
            this.INVOICEDATE.Width = 120;
            // 
            // CUSTOMERCODE
            // 
            this.CUSTOMERCODE.DataPropertyName = "CUSTOMERCODE";
            this.CUSTOMERCODE.HeaderText = "รหัสลูกค้า";
            this.CUSTOMERCODE.Name = "CUSTOMERCODE";
            this.CUSTOMERCODE.ReadOnly = true;
            this.CUSTOMERCODE.Visible = false;
            // 
            // CUSTOMERNAME
            // 
            this.CUSTOMERNAME.DataPropertyName = "CUSTOMERNAME";
            this.CUSTOMERNAME.HeaderText = "ชื่อลูกค้า";
            this.CUSTOMERNAME.Name = "CUSTOMERNAME";
            this.CUSTOMERNAME.ReadOnly = true;
            this.CUSTOMERNAME.Width = 230;
            // 
            // WAREHOUSE
            // 
            this.WAREHOUSE.DataPropertyName = "WAREHOUSE";
            this.WAREHOUSE.HeaderText = "WAREHOUSE";
            this.WAREHOUSE.Name = "WAREHOUSE";
            this.WAREHOUSE.ReadOnly = true;
            this.WAREHOUSE.Visible = false;
            // 
            // REQUISITION
            // 
            this.REQUISITION.DataPropertyName = "REQUISITION";
            this.REQUISITION.HeaderText = "REQUISITION";
            this.REQUISITION.Name = "REQUISITION";
            this.REQUISITION.ReadOnly = true;
            this.REQUISITION.Visible = false;
            // 
            // ReturnPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 388);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReturnPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "รายการใบเสร็จรับเงิน";
            this.Load += new System.EventHandler(this.ReturnPopup_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvRequisition)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.TextBox txtCodeFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtCodeTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRequisition;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.DataGridView grvRequisition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn INVOICECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn INVOICEDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOMERCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOMERNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAREHOUSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn REQUISITION;
    }
}