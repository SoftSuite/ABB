namespace ABBClient.Transaction
{
    partial class Support
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Support));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnSubmit = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grvSupportSearch = new System.Windows.Forms.DataGridView();
            this.CHKAPPROVE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RQCODE = new System.Windows.Forms.DataGridViewLinkColumn();
            this.RESERVEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LASTNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRANDTOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RQSTATUSNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RQLOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RQSTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtRqLoid = new System.Windows.Forms.TextBox();
            this.btnSearchSupport = new System.Windows.Forms.Button();
            this.cmbRqStatusFrom = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dpReserveDateTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dpReserveDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRqCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvSupportSearch)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
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
            this.btnSubmit.Size = new System.Drawing.Size(75, 22);
            this.btnSubmit.Text = "ส่งฝ่ายขาย";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grvSupportSearch, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 566);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // grvSupportSearch
            // 
            this.grvSupportSearch.AllowUserToAddRows = false;
            this.grvSupportSearch.AllowUserToDeleteRows = false;
            this.grvSupportSearch.AllowUserToOrderColumns = true;
            this.grvSupportSearch.BackgroundColor = System.Drawing.Color.Silver;
            this.grvSupportSearch.ColumnHeadersHeight = 25;
            this.grvSupportSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grvSupportSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CHKAPPROVE,
            this.RQCODE,
            this.RESERVEDATE,
            this.CUSNAME,
            this.LASTNAME,
            this.GRANDTOT,
            this.RQSTATUSNAME,
            this.RQLOID,
            this.RQSTATUS});
            this.grvSupportSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvSupportSearch.Location = new System.Drawing.Point(5, 118);
            this.grvSupportSearch.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.grvSupportSearch.MultiSelect = false;
            this.grvSupportSearch.Name = "grvSupportSearch";
            this.grvSupportSearch.RowHeadersWidth = 25;
            this.grvSupportSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvSupportSearch.Size = new System.Drawing.Size(782, 443);
            this.grvSupportSearch.TabIndex = 5;
            this.grvSupportSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvSupportSearch_CellClick);
            this.grvSupportSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvSupportSearch_CellContentClick);
            // 
            // CHKAPPROVE
            // 
            this.CHKAPPROVE.HeaderText = "เลือก";
            this.CHKAPPROVE.Name = "CHKAPPROVE";
            this.CHKAPPROVE.Width = 60;
            // 
            // RQCODE
            // 
            this.RQCODE.DataPropertyName = "RQCODE";
            this.RQCODE.HeaderText = "เลขที่";
            this.RQCODE.Name = "RQCODE";
            this.RQCODE.ReadOnly = true;
            this.RQCODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.RQCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.RQCODE.Width = 110;
            // 
            // RESERVEDATE
            // 
            this.RESERVEDATE.DataPropertyName = "RESERVEDATE";
            dataGridViewCellStyle7.Format = "dd/MM/yyyy";
            this.RESERVEDATE.DefaultCellStyle = dataGridViewCellStyle7;
            this.RESERVEDATE.HeaderText = "วันที่";
            this.RESERVEDATE.Name = "RESERVEDATE";
            this.RESERVEDATE.ReadOnly = true;
            this.RESERVEDATE.Width = 80;
            // 
            // CUSNAME
            // 
            this.CUSNAME.DataPropertyName = "CUSNAME";
            this.CUSNAME.HeaderText = "ชื่อลูกค้า";
            this.CUSNAME.Name = "CUSNAME";
            this.CUSNAME.ReadOnly = true;
            // 
            // LASTNAME
            // 
            this.LASTNAME.DataPropertyName = "LASTNAME";
            this.LASTNAME.HeaderText = "นามสกุล";
            this.LASTNAME.Name = "LASTNAME";
            this.LASTNAME.ReadOnly = true;
            this.LASTNAME.Width = 150;
            // 
            // GRANDTOT
            // 
            this.GRANDTOT.DataPropertyName = "GRANDTOT";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle8.Format = "#,##0.00";
            this.GRANDTOT.DefaultCellStyle = dataGridViewCellStyle8;
            this.GRANDTOT.HeaderText = "ยอดสุทธิ";
            this.GRANDTOT.Name = "GRANDTOT";
            this.GRANDTOT.ReadOnly = true;
            // 
            // RQSTATUSNAME
            // 
            this.RQSTATUSNAME.DataPropertyName = "RQSTATUSNAME";
            this.RQSTATUSNAME.HeaderText = "สถานะ";
            this.RQSTATUSNAME.Name = "RQSTATUSNAME";
            this.RQSTATUSNAME.ReadOnly = true;
            // 
            // RQLOID
            // 
            this.RQLOID.DataPropertyName = "RQLOID";
            this.RQLOID.HeaderText = "RQLOID";
            this.RQLOID.Name = "RQLOID";
            this.RQLOID.Visible = false;
            // 
            // RQSTATUS
            // 
            this.RQSTATUS.DataPropertyName = "RQSTATUS";
            this.RQSTATUS.HeaderText = "RQSTATUS";
            this.RQSTATUS.Name = "RQSTATUS";
            this.RQSTATUS.Visible = false;
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtRqLoid);
            this.panel1.Controls.Add(this.btnSearchSupport);
            this.panel1.Controls.Add(this.cmbRqStatusFrom);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbCustomer);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dpReserveDateTo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dpReserveDateFrom);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtRqCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 50);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 63);
            this.panel1.TabIndex = 3;
            // 
            // txtRqLoid
            // 
            this.txtRqLoid.Location = new System.Drawing.Point(684, 2);
            this.txtRqLoid.Name = "txtRqLoid";
            this.txtRqLoid.Size = new System.Drawing.Size(30, 20);
            this.txtRqLoid.TabIndex = 14;
            this.txtRqLoid.Visible = false;
            // 
            // btnSearchSupport
            // 
            this.btnSearchSupport.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchSupport.Image")));
            this.btnSearchSupport.Location = new System.Drawing.Point(653, 27);
            this.btnSearchSupport.Name = "btnSearchSupport";
            this.btnSearchSupport.Size = new System.Drawing.Size(25, 25);
            this.btnSearchSupport.TabIndex = 13;
            this.btnSearchSupport.UseVisualStyleBackColor = true;
            this.btnSearchSupport.Click += new System.EventHandler(this.btnSearchSupport_Click);
            // 
            // cmbRqStatusFrom
            // 
            this.cmbRqStatusFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRqStatusFrom.FormattingEnabled = true;
            this.cmbRqStatusFrom.Location = new System.Drawing.Point(377, 31);
            this.cmbRqStatusFrom.Name = "cmbRqStatusFrom";
            this.cmbRqStatusFrom.Size = new System.Drawing.Size(231, 21);
            this.cmbRqStatusFrom.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(333, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "สถานะ";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(63, 31);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(231, 21);
            this.cmbCustomer.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "ลูกค้า";
            // 
            // dpReserveDateTo
            // 
            this.dpReserveDateTo.CustomFormat = "dd/MM/yyyy";
            this.dpReserveDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpReserveDateTo.Location = new System.Drawing.Point(513, 7);
            this.dpReserveDateTo.Name = "dpReserveDateTo";
            this.dpReserveDateTo.Size = new System.Drawing.Size(95, 20);
            this.dpReserveDateTo.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(488, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "ถึง";
            // 
            // dpReserveDateFrom
            // 
            this.dpReserveDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dpReserveDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpReserveDateFrom.Location = new System.Drawing.Point(377, 7);
            this.dpReserveDateFrom.Name = "dpReserveDateFrom";
            this.dpReserveDateFrom.Size = new System.Drawing.Size(95, 20);
            this.dpReserveDateFrom.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "วันที่";
            // 
            // txtRqCode
            // 
            this.txtRqCode.Location = new System.Drawing.Point(63, 7);
            this.txtRqCode.Name = "txtRqCode";
            this.txtRqCode.Size = new System.Drawing.Size(100, 20);
            this.txtRqCode.TabIndex = 1;
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
            // Support
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Support";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "บันทึกขอสนับสนุนสินค้า";
            this.Load += new System.EventHandler(this.Support_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvSupportSearch)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnSubmit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbRqStatusFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dpReserveDateTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dpReserveDateFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRqCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchSupport;
        private System.Windows.Forms.DataGridView grvSupportSearch;
        private System.Windows.Forms.TextBox txtRqLoid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CHKAPPROVE;
        private System.Windows.Forms.DataGridViewLinkColumn RQCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RESERVEDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn LASTNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRANDTOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn RQSTATUSNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn RQLOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RQSTATUS;
    }
}