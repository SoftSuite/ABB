namespace ABBClient.Transaction
{
    partial class StockIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockIn));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSubmit = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtRequisitionCode = new System.Windows.Forms.TextBox();
            this.txtStockInCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SelColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCKINCODE = new System.Windows.Forms.DataGridViewLinkColumn();
            this.RECEIVEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REQUISITIONCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRANDTOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUSNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            grvStockIn = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(grvStockIn)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(grvStockIn, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 566);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnDelete,
            this.btnPrint,
            this.btnSubmit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnNew.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnNew.Image = global::ABBClient.Properties.Resources.icn_new;
            this.btnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(63, 22);
            this.btnNew.Text = "สร้างใหม่";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnDelete.Image = global::ABBClient.Properties.Resources.icn_delete;
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(39, 22);
            this.btnDelete.Text = "ลบ";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnPrint.Image = global::ABBClient.Properties.Resources.icn_print;
            this.btnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(49, 22);
            this.btnPrint.Text = "พิมพ์";
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnSubmit.Image = global::ABBClient.Properties.Resources.icn_submit;
            this.btnSubmit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSubmit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(53, 22);
            this.btnSubmit.Text = "ยืนยัน";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dtpDateTo);
            this.panel1.Controls.Add(this.dtpDateFrom);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtRequisitionCode);
            this.panel1.Controls.Add(this.txtStockInCode);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 87);
            this.panel1.TabIndex = 2;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(301, 32);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(100, 20);
            this.dtpDateTo.TabIndex = 12;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(170, 32);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(100, 20);
            this.dtpDateFrom.TabIndex = 12;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(442, 53);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(25, 25);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtRequisitionCode
            // 
            this.txtRequisitionCode.Location = new System.Drawing.Point(170, 56);
            this.txtRequisitionCode.Name = "txtRequisitionCode";
            this.txtRequisitionCode.Size = new System.Drawing.Size(231, 20);
            this.txtRequisitionCode.TabIndex = 7;
            // 
            // txtStockInCode
            // 
            this.txtStockInCode.Location = new System.Drawing.Point(170, 8);
            this.txtStockInCode.Name = "txtStockInCode";
            this.txtStockInCode.Size = new System.Drawing.Size(231, 20);
            this.txtStockInCode.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "เลขที่ใบเบิก";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ถึง";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "วันที่รับ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(80, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "เลขที่";
            // 
            // grvStockIn
            // 
            grvStockIn.AllowUserToAddRows = false;
            grvStockIn.AllowUserToDeleteRows = false;
            grvStockIn.AllowUserToOrderColumns = true;
            grvStockIn.BackgroundColor = System.Drawing.Color.Silver;
            grvStockIn.ColumnHeadersHeight = 25;
            grvStockIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grvStockIn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelColumn,
            this.LOID,
            this.ORDERNO,
            this.STOCKINCODE,
            this.RECEIVEDATE,
            this.REQUISITIONCODE,
            this.GRANDTOT,
            this.STATUSNAME});
            grvStockIn.Dock = System.Windows.Forms.DockStyle.Fill;
            grvStockIn.Location = new System.Drawing.Point(5, 122);
            grvStockIn.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            grvStockIn.MultiSelect = false;
            grvStockIn.Name = "grvStockIn";
            grvStockIn.ReadOnly = true;
            grvStockIn.RowHeadersWidth = 25;
            grvStockIn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            grvStockIn.Size = new System.Drawing.Size(782, 439);
            grvStockIn.TabIndex = 3;
            grvStockIn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvStockIn_CellContentClick);
            // 
            // SelColumn
            // 
            this.SelColumn.Frozen = true;
            this.SelColumn.HeaderText = "";
            this.SelColumn.Name = "SelColumn";
            this.SelColumn.ReadOnly = true;
            this.SelColumn.Width = 50;
            // 
            // LOID
            // 
            this.LOID.DataPropertyName = "LOID";
            this.LOID.Frozen = true;
            this.LOID.HeaderText = "LOID";
            this.LOID.Name = "LOID";
            this.LOID.ReadOnly = true;
            this.LOID.Visible = false;
            // 
            // ORDERNO
            // 
            this.ORDERNO.DataPropertyName = "ORDERNO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.ORDERNO.DefaultCellStyle = dataGridViewCellStyle1;
            this.ORDERNO.Frozen = true;
            this.ORDERNO.HeaderText = "ลำดับ";
            this.ORDERNO.Name = "ORDERNO";
            this.ORDERNO.ReadOnly = true;
            this.ORDERNO.Width = 50;
            // 
            // STOCKINCODE
            // 
            this.STOCKINCODE.DataPropertyName = "STOCKINCODE";
            this.STOCKINCODE.Frozen = true;
            this.STOCKINCODE.HeaderText = "เลขที่";
            this.STOCKINCODE.Name = "STOCKINCODE";
            this.STOCKINCODE.ReadOnly = true;
            // 
            // RECEIVEDATE
            // 
            this.RECEIVEDATE.DataPropertyName = "RECEIVEDATE";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            this.RECEIVEDATE.DefaultCellStyle = dataGridViewCellStyle2;
            this.RECEIVEDATE.HeaderText = "วันที่รับ";
            this.RECEIVEDATE.Name = "RECEIVEDATE";
            this.RECEIVEDATE.ReadOnly = true;
            // 
            // REQUISITIONCODE
            // 
            this.REQUISITIONCODE.DataPropertyName = "REQUISITIONCODE";
            this.REQUISITIONCODE.HeaderText = "เลขที่เบิก";
            this.REQUISITIONCODE.Name = "REQUISITIONCODE";
            this.REQUISITIONCODE.ReadOnly = true;
            // 
            // GRANDTOT
            // 
            this.GRANDTOT.DataPropertyName = "GRANDTOT";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "#,##0.00";
            this.GRANDTOT.DefaultCellStyle = dataGridViewCellStyle3;
            this.GRANDTOT.HeaderText = "ยอดรวม";
            this.GRANDTOT.Name = "GRANDTOT";
            this.GRANDTOT.ReadOnly = true;
            // 
            // STATUSNAME
            // 
            this.STATUSNAME.DataPropertyName = "STATUSNAME";
            this.STATUSNAME.HeaderText = "สถานะ";
            this.STATUSNAME.Name = "STATUSNAME";
            this.STATUSNAME.ReadOnly = true;
            this.STATUSNAME.Width = 120;
            // 
            // StockIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "StockIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ใบแสดงยกยอดรับสินค้า";
            this.Load += new System.EventHandler(this.StockIn_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(grvStockIn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grvStockIn;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtRequisitionCode;
        private System.Windows.Forms.TextBox txtStockInCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton btnSubmit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERNO;
        private System.Windows.Forms.DataGridViewLinkColumn STOCKINCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIVEDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn REQUISITIONCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRANDTOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUSNAME;
    }
}