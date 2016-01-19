namespace ABBClient.Transaction
{
    partial class ReturnSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReturnSearch));
            this.txtSiLoid = new System.Windows.Forms.TextBox();
            this.DateToPk = new System.Windows.Forms.DateTimePicker();
            this.DateFromPk = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearchReturn = new System.Windows.Forms.Button();
            this.txtSICode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grvReturnSearch = new System.Windows.Forms.DataGridView();
            this.ChkApprove = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SICODE = new System.Windows.Forms.DataGridViewLinkColumn();
            this.APPROVEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RQCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRANDTOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SILOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAREHOUSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvReturnSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSiLoid
            // 
            this.txtSiLoid.Location = new System.Drawing.Point(602, 32);
            this.txtSiLoid.Name = "txtSiLoid";
            this.txtSiLoid.Size = new System.Drawing.Size(37, 20);
            this.txtSiLoid.TabIndex = 18;
            this.txtSiLoid.Visible = false;
            // 
            // DateToPk
            // 
            this.DateToPk.CustomFormat = "dd/MM/yyyy";
            this.DateToPk.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateToPk.Location = new System.Drawing.Point(296, 31);
            this.DateToPk.Name = "DateToPk";
            this.DateToPk.Size = new System.Drawing.Size(95, 20);
            this.DateToPk.TabIndex = 17;
            this.DateToPk.Value = new System.DateTime(2007, 12, 21, 0, 0, 0, 0);
            // 
            // DateFromPk
            // 
            this.DateFromPk.CustomFormat = "dd/MM/yyyy";
            this.DateFromPk.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateFromPk.Location = new System.Drawing.Point(170, 32);
            this.DateFromPk.Name = "DateFromPk";
            this.DateFromPk.Size = new System.Drawing.Size(95, 20);
            this.DateFromPk.TabIndex = 16;
            this.DateFromPk.Value = new System.DateTime(2007, 12, 21, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "ถึง";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grvReturnSearch, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 566);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnDelete,
            this.btnPrint});
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
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtSiLoid);
            this.panel1.Controls.Add(this.DateToPk);
            this.panel1.Controls.Add(this.DateFromPk);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnSearchReturn);
            this.panel1.Controls.Add(this.txtSICode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 63);
            this.panel1.TabIndex = 2;
            // 
            // btnSearchReturn
            // 
            this.btnSearchReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchReturn.Image")));
            this.btnSearchReturn.Location = new System.Drawing.Point(412, 29);
            this.btnSearchReturn.Name = "btnSearchReturn";
            this.btnSearchReturn.Size = new System.Drawing.Size(25, 25);
            this.btnSearchReturn.TabIndex = 11;
            this.btnSearchReturn.UseVisualStyleBackColor = true;
            this.btnSearchReturn.Click += new System.EventHandler(this.btnSearchReturn_Click);
            // 
            // txtSICode
            // 
            this.txtSICode.Location = new System.Drawing.Point(170, 8);
            this.txtSICode.Name = "txtSICode";
            this.txtSICode.Size = new System.Drawing.Size(221, 20);
            this.txtSICode.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "วันที่";
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
            // grvReturnSearch
            // 
            this.grvReturnSearch.AllowUserToAddRows = false;
            this.grvReturnSearch.AllowUserToDeleteRows = false;
            this.grvReturnSearch.AllowUserToOrderColumns = true;
            this.grvReturnSearch.BackgroundColor = System.Drawing.Color.Silver;
            this.grvReturnSearch.ColumnHeadersHeight = 25;
            this.grvReturnSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grvReturnSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChkApprove,
            this.SICODE,
            this.APPROVEDATE,
            this.RQCODE,
            this.CUSNAME,
            this.GRANDTOT,
            this.SILOID,
            this.WAREHOUSE});
            this.grvReturnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvReturnSearch.Location = new System.Drawing.Point(5, 98);
            this.grvReturnSearch.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.grvReturnSearch.MultiSelect = false;
            this.grvReturnSearch.Name = "grvReturnSearch";
            this.grvReturnSearch.RowHeadersWidth = 25;
            this.grvReturnSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvReturnSearch.Size = new System.Drawing.Size(782, 463);
            this.grvReturnSearch.TabIndex = 3;
            this.grvReturnSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvReturnSearch_CellClick);
            this.grvReturnSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvReturnSearch_CellContentClick);
            // 
            // ChkApprove
            // 
            this.ChkApprove.DataPropertyName = "CHKAPPROVE";
            this.ChkApprove.HeaderText = "เลือก";
            this.ChkApprove.Name = "ChkApprove";
            this.ChkApprove.Width = 40;
            // 
            // SICODE
            // 
            this.SICODE.DataPropertyName = "SICODE";
            this.SICODE.HeaderText = "เลขที่";
            this.SICODE.Name = "SICODE";
            this.SICODE.ReadOnly = true;
            this.SICODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SICODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // APPROVEDATE
            // 
            this.APPROVEDATE.DataPropertyName = "APPROVEDATE";
            this.APPROVEDATE.HeaderText = "วันที่";
            this.APPROVEDATE.Name = "APPROVEDATE";
            this.APPROVEDATE.ReadOnly = true;
            // 
            // RQCODE
            // 
            this.RQCODE.DataPropertyName = "RQCODE";
            this.RQCODE.HeaderText = "เลขที่ใบเสร็จ";
            this.RQCODE.Name = "RQCODE";
            this.RQCODE.ReadOnly = true;
            // 
            // CUSNAME
            // 
            this.CUSNAME.DataPropertyName = "CUSNAME";
            this.CUSNAME.HeaderText = "ลูกค้า";
            this.CUSNAME.Name = "CUSNAME";
            this.CUSNAME.ReadOnly = true;
            this.CUSNAME.Width = 250;
            // 
            // GRANDTOT
            // 
            this.GRANDTOT.DataPropertyName = "GRANDTOT";
            this.GRANDTOT.HeaderText = "ยอดสุทธิ";
            this.GRANDTOT.Name = "GRANDTOT";
            this.GRANDTOT.ReadOnly = true;
            // 
            // SILOID
            // 
            this.SILOID.DataPropertyName = "SILOID";
            this.SILOID.HeaderText = "SILOID";
            this.SILOID.Name = "SILOID";
            this.SILOID.ReadOnly = true;
            this.SILOID.Visible = false;
            // 
            // WAREHOUSE
            // 
            this.WAREHOUSE.DataPropertyName = "WAREHOUSE";
            this.WAREHOUSE.HeaderText = "WAREHOUSE";
            this.WAREHOUSE.Name = "WAREHOUSE";
            this.WAREHOUSE.ReadOnly = true;
            this.WAREHOUSE.Visible = false;
            // 
            // ReturnSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ReturnSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ใบรับคืนสินค้า";
            this.Load += new System.EventHandler(this.ReturnSearch_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvReturnSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSiLoid;
        private System.Windows.Forms.DateTimePicker DateToPk;
        private System.Windows.Forms.DateTimePicker DateFromPk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearchReturn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSICode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView grvReturnSearch;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkApprove;
        private System.Windows.Forms.DataGridViewLinkColumn SICODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn APPROVEDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RQCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRANDTOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SILOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAREHOUSE;
    }
}