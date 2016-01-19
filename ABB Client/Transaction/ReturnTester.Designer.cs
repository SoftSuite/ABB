namespace ABBClient.Transaction
{
    partial class ReturnTester
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReturnTester));
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.grvReturn = new System.Windows.Forms.DataGridView();
            this.BOX = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODE = new System.Windows.Forms.DataGridViewLinkColumn();
            this.CREATEON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAREHOUSENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RANK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnSubmit = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbStatusTo = new System.Windows.Forms.ComboBox();
            this.cmbStatusFrom = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grvReturn)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(194, 31);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(100, 20);
            this.dtpDateTo.TabIndex = 5;
            // 
            // grvReturn
            // 
            this.grvReturn.AllowUserToAddRows = false;
            this.grvReturn.AllowUserToDeleteRows = false;
            this.grvReturn.AllowUserToOrderColumns = true;
            this.grvReturn.BackgroundColor = System.Drawing.Color.Silver;
            this.grvReturn.ColumnHeadersHeight = 25;
            this.grvReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grvReturn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BOX,
            this.LOID,
            this.CODE,
            this.CREATEON,
            this.WAREHOUSENAME,
            this.TOTAL,
            this.STATUS,
            this.RANK});
            this.grvReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvReturn.Location = new System.Drawing.Point(5, 116);
            this.grvReturn.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.grvReturn.MultiSelect = false;
            this.grvReturn.Name = "grvReturn";
            this.grvReturn.ReadOnly = true;
            this.grvReturn.RowHeadersWidth = 25;
            this.grvReturn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvReturn.Size = new System.Drawing.Size(782, 445);
            this.grvReturn.TabIndex = 5;
            this.grvReturn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvReturn_CellContentClick);
            // 
            // BOX
            // 
            this.BOX.DataPropertyName = "BOX";
            this.BOX.FalseValue = "0";
            this.BOX.Frozen = true;
            this.BOX.HeaderText = "";
            this.BOX.Name = "BOX";
            this.BOX.ReadOnly = true;
            this.BOX.TrueValue = "1";
            this.BOX.Width = 30;
            // 
            // LOID
            // 
            this.LOID.DataPropertyName = "LOID";
            this.LOID.HeaderText = "LOID";
            this.LOID.Name = "LOID";
            this.LOID.ReadOnly = true;
            this.LOID.Visible = false;
            // 
            // CODE
            // 
            this.CODE.DataPropertyName = "CODE";
            this.CODE.HeaderText = "เลขที่";
            this.CODE.Name = "CODE";
            this.CODE.ReadOnly = true;
            this.CODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CREATEON
            // 
            this.CREATEON.DataPropertyName = "CREATEON";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            dataGridViewCellStyle1.NullValue = null;
            this.CREATEON.DefaultCellStyle = dataGridViewCellStyle1;
            this.CREATEON.HeaderText = "วันที่";
            this.CREATEON.Name = "CREATEON";
            this.CREATEON.ReadOnly = true;
            this.CREATEON.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WAREHOUSENAME
            // 
            this.WAREHOUSENAME.DataPropertyName = "WAREHOUSENAME";
            this.WAREHOUSENAME.HeaderText = "ส่งคลังสินค้า";
            this.WAREHOUSENAME.Name = "WAREHOUSENAME";
            this.WAREHOUSENAME.ReadOnly = true;
            this.WAREHOUSENAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WAREHOUSENAME.Width = 230;
            // 
            // TOTAL
            // 
            this.TOTAL.DataPropertyName = "TOTAL";
            dataGridViewCellStyle2.Format = "#,##0";
            dataGridViewCellStyle2.NullValue = "0";
            this.TOTAL.DefaultCellStyle = dataGridViewCellStyle2;
            this.TOTAL.HeaderText = "จำนวนรายการ";
            this.TOTAL.Name = "TOTAL";
            this.TOTAL.ReadOnly = true;
            this.TOTAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "สถานะ";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RANK
            // 
            this.RANK.DataPropertyName = "RANK";
            this.RANK.HeaderText = "RANK";
            this.RANK.Name = "RANK";
            this.RANK.ReadOnly = true;
            this.RANK.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(169, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "ถึง";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(63, 31);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(100, 20);
            this.dtpDateFrom.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "วันที่";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(63, 7);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "เลขที่";
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
            // btnSubmit
            // 
            this.btnSubmit.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnSubmit.Image = global::ABBClient.Properties.Resources.icn_submit;
            this.btnSubmit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSubmit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(97, 22);
            this.btnSubmit.Text = "ส่งคลังสำเร็จรูป";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnDelete,
            this.btnSubmit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(658, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(25, 25);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmbStatusTo);
            this.panel1.Controls.Add(this.cmbStatusFrom);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.dtpDateTo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpDateFrom);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 50);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 61);
            this.panel1.TabIndex = 3;
            // 
            // cmbStatusTo
            // 
            this.cmbStatusTo.FormattingEnabled = true;
            this.cmbStatusTo.Location = new System.Drawing.Point(519, 30);
            this.cmbStatusTo.Name = "cmbStatusTo";
            this.cmbStatusTo.Size = new System.Drawing.Size(100, 21);
            this.cmbStatusTo.TabIndex = 17;
            // 
            // cmbStatusFrom
            // 
            this.cmbStatusFrom.FormattingEnabled = true;
            this.cmbStatusFrom.Location = new System.Drawing.Point(390, 30);
            this.cmbStatusFrom.Name = "cmbStatusFrom";
            this.cmbStatusFrom.Size = new System.Drawing.Size(100, 21);
            this.cmbStatusFrom.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(494, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "ถึง";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(346, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "สถานะ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grvReturn, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 566);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(218)))), ((int)(((byte)(169)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(782, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "ค้นหา";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(38, 123);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 19;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.Click += new System.EventHandler(this.chkAll_Click);
            // 
            // ReturnTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ReturnTester";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ใบแจ้งส่งคืนสินค้าตัวอย่าง";
            this.Load += new System.EventHandler(this.ReturnTester_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvReturn)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DataGridView grvReturn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnSubmit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStatusTo;
        private System.Windows.Forms.ComboBox cmbStatusFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BOX;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOID;
        private System.Windows.Forms.DataGridViewLinkColumn CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATEON;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAREHOUSENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn RANK;
    }
}