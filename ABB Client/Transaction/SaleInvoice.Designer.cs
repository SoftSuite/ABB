namespace ABBClient.Transaction
{
    partial class SaleInvoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaleInvoice));
            this.ORDERNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BARCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UNITNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DISCOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NETPRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISVAT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCFax = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCTel = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCAddress = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtCCode = new System.Windows.Forms.TextBox();
            this.txtCreateBy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRefNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtLOID = new System.Windows.Forms.TextBox();
            this.txtGrandTotal = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtVat = new System.Windows.Forms.TextBox();
            this.txtTotalVat = new System.Windows.Forms.TextBox();
            this.txtTotalDiscount = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpReceiveDate = new System.Windows.Forms.DateTimePicker();
            this.txtReceiveBy = new System.Windows.Forms.TextBox();
            this.txtBankBranch = new System.Windows.Forms.TextBox();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpChequeDate = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCheque = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.rdoCheque = new System.Windows.Forms.RadioButton();
            this.rdoCash = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtCredit = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.rdoCredit = new System.Windows.Forms.RadioButton();
            grvSales = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(grvSales)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ORDERNO
            // 
            this.ORDERNO.DataPropertyName = "ORDERNO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ORDERNO.DefaultCellStyle = dataGridViewCellStyle1;
            this.ORDERNO.HeaderText = "ลำดับ";
            this.ORDERNO.Name = "ORDERNO";
            this.ORDERNO.ReadOnly = true;
            this.ORDERNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ORDERNO.Width = 40;
            // 
            // BARCODE
            // 
            this.BARCODE.DataPropertyName = "BARCODE";
            this.BARCODE.HeaderText = "บาร์โค้ด";
            this.BARCODE.Name = "BARCODE";
            this.BARCODE.ReadOnly = true;
            this.BARCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NAME
            // 
            this.NAME.DataPropertyName = "NAME";
            this.NAME.HeaderText = "ชื่อสินค้า";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            this.NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NAME.Width = 250;
            // 
            // QTY
            // 
            this.QTY.DataPropertyName = "QTY";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,##0";
            dataGridViewCellStyle2.NullValue = "0";
            this.QTY.DefaultCellStyle = dataGridViewCellStyle2;
            this.QTY.HeaderText = "จำนวน";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QTY.Width = 50;
            // 
            // UNITNAME
            // 
            this.UNITNAME.DataPropertyName = "UNITNAME";
            this.UNITNAME.HeaderText = "หน่วย";
            this.UNITNAME.Name = "UNITNAME";
            this.UNITNAME.ReadOnly = true;
            this.UNITNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UNITNAME.Width = 60;
            // 
            // PRICE
            // 
            this.PRICE.DataPropertyName = "PRICE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "#,##0.00";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.PRICE.DefaultCellStyle = dataGridViewCellStyle3;
            this.PRICE.HeaderText = "ราคา";
            this.PRICE.Name = "PRICE";
            this.PRICE.ReadOnly = true;
            this.PRICE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PRICE.Width = 60;
            // 
            // DISCOUNT
            // 
            this.DISCOUNT.DataPropertyName = "DISCOUNT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#,##0.00";
            dataGridViewCellStyle4.NullValue = "0.00";
            this.DISCOUNT.DefaultCellStyle = dataGridViewCellStyle4;
            this.DISCOUNT.HeaderText = "ส่วนลด";
            this.DISCOUNT.Name = "DISCOUNT";
            this.DISCOUNT.ReadOnly = true;
            this.DISCOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DISCOUNT.Width = 60;
            // 
            // NETPRICE
            // 
            this.NETPRICE.DataPropertyName = "NETPRICE";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "#,##0.00";
            dataGridViewCellStyle5.NullValue = "0.00";
            this.NETPRICE.DefaultCellStyle = dataGridViewCellStyle5;
            this.NETPRICE.HeaderText = "ราคารวม";
            this.NETPRICE.Name = "NETPRICE";
            this.NETPRICE.ReadOnly = true;
            this.NETPRICE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NETPRICE.Width = 70;
            // 
            // ISVAT
            // 
            this.ISVAT.DataPropertyName = "ISVAT";
            this.ISVAT.HeaderText = "V";
            this.ISVAT.Name = "ISVAT";
            this.ISVAT.ReadOnly = true;
            this.ISVAT.Width = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "ส่วนลด";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "จำนวนเงิน";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(82, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "%";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(grvSales, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(794, 568);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // grvSales
            // 
            grvSales.AllowUserToAddRows = false;
            grvSales.AllowUserToDeleteRows = false;
            grvSales.BackgroundColor = System.Drawing.Color.Silver;
            grvSales.ColumnHeadersHeight = 25;
            grvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grvSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORDERNO,
            this.BARCODE,
            this.NAME,
            this.QTY,
            this.UNITNAME,
            this.PRICE,
            this.DISCOUNT,
            this.NETPRICE,
            this.ISVAT});
            grvSales.Dock = System.Windows.Forms.DockStyle.Fill;
            grvSales.Location = new System.Drawing.Point(5, 157);
            grvSales.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            grvSales.MultiSelect = false;
            grvSales.Name = "grvSales";
            grvSales.ReadOnly = true;
            grvSales.RowHeadersWidth = 25;
            grvSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            grvSales.Size = new System.Drawing.Size(784, 291);
            grvSales.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(794, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnPrint
            // 
            this.btnPrint.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnPrint.Image = global::ABBClient.Properties.Resources.icn_print;
            this.btnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(78, 22);
            this.btnPrint.Text = "พิมพ์ [F10]";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.12846F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 132);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(218)))), ((int)(((byte)(169)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(794, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "รายละเอียดการขาย";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtCFax);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtCTel);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtCAddress);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtCName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtDate);
            this.panel1.Controls.Add(this.txtCCode);
            this.panel1.Controls.Add(this.txtCreateBy);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtRefNo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 97);
            this.panel1.TabIndex = 4;
            // 
            // txtCFax
            // 
            this.txtCFax.ForeColor = System.Drawing.Color.Green;
            this.txtCFax.Location = new System.Drawing.Point(276, 72);
            this.txtCFax.Name = "txtCFax";
            this.txtCFax.Size = new System.Drawing.Size(261, 20);
            this.txtCFax.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(225, 75);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Fax.";
            // 
            // txtCTel
            // 
            this.txtCTel.ForeColor = System.Drawing.Color.Green;
            this.txtCTel.Location = new System.Drawing.Point(74, 72);
            this.txtCTel.Name = "txtCTel";
            this.txtCTel.Size = new System.Drawing.Size(145, 20);
            this.txtCTel.TabIndex = 28;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 75);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "โทรศัพท์";
            // 
            // txtCAddress
            // 
            this.txtCAddress.ForeColor = System.Drawing.Color.Green;
            this.txtCAddress.Location = new System.Drawing.Point(74, 26);
            this.txtCAddress.Multiline = true;
            this.txtCAddress.Name = "txtCAddress";
            this.txtCAddress.Size = new System.Drawing.Size(463, 43);
            this.txtCAddress.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "ที่อยู่";
            // 
            // txtCName
            // 
            this.txtCName.ForeColor = System.Drawing.Color.Green;
            this.txtCName.Location = new System.Drawing.Point(276, 3);
            this.txtCName.Name = "txtCName";
            this.txtCName.Size = new System.Drawing.Size(261, 20);
            this.txtCName.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(225, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "ชื่อลูกค้า";
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.ForeColor = System.Drawing.Color.Green;
            this.txtCode.Location = new System.Drawing.Point(632, 26);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(145, 20);
            this.txtCode.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(559, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "เลขที่ใบเสร็จ";
            // 
            // txtDate
            // 
            this.txtDate.Enabled = false;
            this.txtDate.ForeColor = System.Drawing.Color.Green;
            this.txtDate.Location = new System.Drawing.Point(632, 3);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(93, 20);
            this.txtDate.TabIndex = 20;
            // 
            // txtCCode
            // 
            this.txtCCode.ForeColor = System.Drawing.Color.Green;
            this.txtCCode.Location = new System.Drawing.Point(74, 3);
            this.txtCCode.Name = "txtCCode";
            this.txtCCode.Size = new System.Drawing.Size(145, 20);
            this.txtCCode.TabIndex = 19;
            // 
            // txtCreateBy
            // 
            this.txtCreateBy.Enabled = false;
            this.txtCreateBy.ForeColor = System.Drawing.Color.Green;
            this.txtCreateBy.Location = new System.Drawing.Point(632, 72);
            this.txtCreateBy.Name = "txtCreateBy";
            this.txtCreateBy.ReadOnly = true;
            this.txtCreateBy.Size = new System.Drawing.Size(145, 20);
            this.txtCreateBy.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(560, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "พนักงานขาย";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "รหัสลูกค้า";
            // 
            // txtRefNo
            // 
            this.txtRefNo.Enabled = false;
            this.txtRefNo.ForeColor = System.Drawing.Color.Green;
            this.txtRefNo.Location = new System.Drawing.Point(632, 49);
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.ReadOnly = true;
            this.txtRefNo.Size = new System.Drawing.Size(145, 20);
            this.txtRefNo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(559, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "อ้างอิงเลขที่";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(559, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "วันที่";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 567F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 448);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(794, 120);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtLOID);
            this.panel2.Controls.Add(this.txtGrandTotal);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtVat);
            this.panel2.Controls.Add(this.txtTotalVat);
            this.panel2.Controls.Add(this.txtTotalDiscount);
            this.panel2.Controls.Add(this.txtTotal);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(591, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 114);
            this.panel2.TabIndex = 0;
            // 
            // txtLOID
            // 
            this.txtLOID.Location = new System.Drawing.Point(61, 15);
            this.txtLOID.Name = "txtLOID";
            this.txtLOID.ReadOnly = true;
            this.txtLOID.Size = new System.Drawing.Size(30, 20);
            this.txtLOID.TabIndex = 20;
            this.txtLOID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLOID.Visible = false;
            // 
            // txtGrandTotal
            // 
            this.txtGrandTotal.Location = new System.Drawing.Point(97, 84);
            this.txtGrandTotal.Name = "txtGrandTotal";
            this.txtGrandTotal.ReadOnly = true;
            this.txtGrandTotal.Size = new System.Drawing.Size(100, 20);
            this.txtGrandTotal.TabIndex = 19;
            this.txtGrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "ยอดสุทธิ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "ภาษี";
            // 
            // txtVat
            // 
            this.txtVat.ForeColor = System.Drawing.Color.Green;
            this.txtVat.Location = new System.Drawing.Point(57, 61);
            this.txtVat.MaxLength = 2;
            this.txtVat.Name = "txtVat";
            this.txtVat.ReadOnly = true;
            this.txtVat.Size = new System.Drawing.Size(25, 20);
            this.txtVat.TabIndex = 13;
            this.txtVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalVat
            // 
            this.txtTotalVat.Location = new System.Drawing.Point(97, 61);
            this.txtTotalVat.Name = "txtTotalVat";
            this.txtTotalVat.ReadOnly = true;
            this.txtTotalVat.Size = new System.Drawing.Size(100, 20);
            this.txtTotalVat.TabIndex = 11;
            this.txtTotalVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalDiscount
            // 
            this.txtTotalDiscount.Location = new System.Drawing.Point(97, 38);
            this.txtTotalDiscount.Name = "txtTotalDiscount";
            this.txtTotalDiscount.ReadOnly = true;
            this.txtTotalDiscount.Size = new System.Drawing.Size(100, 20);
            this.txtTotalDiscount.TabIndex = 10;
            this.txtTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(97, 15);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 9;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtCredit);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.rdoCredit);
            this.panel3.Controls.Add(this.dtpReceiveDate);
            this.panel3.Controls.Add(this.txtReceiveBy);
            this.panel3.Controls.Add(this.txtBankBranch);
            this.panel3.Controls.Add(this.txtBankName);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.dtpChequeDate);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.txtCheque);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.rdoCheque);
            this.panel3.Controls.Add(this.rdoCash);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(561, 114);
            this.panel3.TabIndex = 1;
            // 
            // dtpReceiveDate
            // 
            this.dtpReceiveDate.CustomFormat = "dd/MM/yyyy";
            this.dtpReceiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReceiveDate.Location = new System.Drawing.Point(408, 91);
            this.dtpReceiveDate.Name = "dtpReceiveDate";
            this.dtpReceiveDate.Size = new System.Drawing.Size(93, 20);
            this.dtpReceiveDate.TabIndex = 30;
            // 
            // txtReceiveBy
            // 
            this.txtReceiveBy.ForeColor = System.Drawing.Color.Green;
            this.txtReceiveBy.Location = new System.Drawing.Point(72, 91);
            this.txtReceiveBy.Name = "txtReceiveBy";
            this.txtReceiveBy.Size = new System.Drawing.Size(250, 20);
            this.txtReceiveBy.TabIndex = 29;
            // 
            // txtBankBranch
            // 
            this.txtBankBranch.ForeColor = System.Drawing.Color.Green;
            this.txtBankBranch.Location = new System.Drawing.Point(408, 48);
            this.txtBankBranch.Name = "txtBankBranch";
            this.txtBankBranch.Size = new System.Drawing.Size(150, 20);
            this.txtBankBranch.TabIndex = 28;
            // 
            // txtBankName
            // 
            this.txtBankName.ForeColor = System.Drawing.Color.Green;
            this.txtBankName.Location = new System.Drawing.Point(172, 48);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(150, 20);
            this.txtBankName.TabIndex = 27;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(351, 95);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(28, 13);
            this.label22.TabIndex = 26;
            this.label22.Text = "วันที่";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(5, 94);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(45, 13);
            this.label21.TabIndex = 25;
            this.label21.Text = "ผู้รับเงิน";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(350, 51);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 13);
            this.label20.TabIndex = 24;
            this.label20.Text = "สาขา";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(122, 52);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 13);
            this.label19.TabIndex = 23;
            this.label19.Text = "ธนาคาร";
            // 
            // dtpChequeDate
            // 
            this.dtpChequeDate.CustomFormat = "dd/MM/yyyy";
            this.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChequeDate.Location = new System.Drawing.Point(408, 25);
            this.dtpChequeDate.Name = "dtpChequeDate";
            this.dtpChequeDate.Size = new System.Drawing.Size(93, 20);
            this.dtpChequeDate.TabIndex = 22;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(350, 29);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "ลงวันที่";
            // 
            // txtCheque
            // 
            this.txtCheque.ForeColor = System.Drawing.Color.Green;
            this.txtCheque.Location = new System.Drawing.Point(172, 26);
            this.txtCheque.Name = "txtCheque";
            this.txtCheque.Size = new System.Drawing.Size(150, 20);
            this.txtCheque.TabIndex = 20;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(122, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "เลขที่";
            // 
            // rdoCheque
            // 
            this.rdoCheque.AutoSize = true;
            this.rdoCheque.Location = new System.Drawing.Point(72, 27);
            this.rdoCheque.Name = "rdoCheque";
            this.rdoCheque.Size = new System.Drawing.Size(44, 17);
            this.rdoCheque.TabIndex = 16;
            this.rdoCheque.TabStop = true;
            this.rdoCheque.Text = "เช็ค";
            this.rdoCheque.UseVisualStyleBackColor = true;
            this.rdoCheque.Click += new System.EventHandler(this.rdoCheque_Click);
            // 
            // rdoCash
            // 
            this.rdoCash.AutoSize = true;
            this.rdoCash.Location = new System.Drawing.Point(72, 4);
            this.rdoCash.Name = "rdoCash";
            this.rdoCash.Size = new System.Drawing.Size(56, 17);
            this.rdoCash.TabIndex = 15;
            this.rdoCash.TabStop = true;
            this.rdoCash.Text = "เงินสด";
            this.rdoCash.UseVisualStyleBackColor = true;
            this.rdoCash.Click += new System.EventHandler(this.rdoCash_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "ชำระเงินโดย";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::ABBClient.Properties.Resources.view;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.Width = 30;
            // 
            // txtCredit
            // 
            this.txtCredit.ForeColor = System.Drawing.Color.Green;
            this.txtCredit.Location = new System.Drawing.Point(208, 69);
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new System.Drawing.Size(150, 20);
            this.txtCredit.TabIndex = 33;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(158, 72);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(31, 13);
            this.label23.TabIndex = 32;
            this.label23.Text = "เลขที่";
            // 
            // rdoCredit
            // 
            this.rdoCredit.AutoSize = true;
            this.rdoCredit.Location = new System.Drawing.Point(72, 70);
            this.rdoCredit.Name = "rdoCredit";
            this.rdoCredit.Size = new System.Drawing.Size(77, 17);
            this.rdoCredit.TabIndex = 31;
            this.rdoCredit.TabStop = true;
            this.rdoCredit.Text = "บัตรเครดิต";
            this.rdoCredit.UseVisualStyleBackColor = true;
            // 
            // SaleInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(794, 568);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "SaleInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ใบเสร็จรับเงิน";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SaleInvoice_KeyDown);
            this.Load += new System.EventHandler(this.SaleInvoice_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(grvSales)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView grvSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BARCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn UNITNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DISCOUNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NETPRICE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ISVAT;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtCCode;
        private System.Windows.Forms.TextBox txtCreateBy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRefNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtLOID;
        private System.Windows.Forms.TextBox txtGrandTotal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtVat;
        private System.Windows.Forms.TextBox txtTotalVat;
        private System.Windows.Forms.TextBox txtTotalDiscount;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCTel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCAddress;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCFax;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdoCash;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton rdoCheque;
        private System.Windows.Forms.DateTimePicker dtpChequeDate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtCheque;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DateTimePicker dtpReceiveDate;
        private System.Windows.Forms.TextBox txtReceiveBy;
        private System.Windows.Forms.TextBox txtBankBranch;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCredit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.RadioButton rdoCredit;
    }
}