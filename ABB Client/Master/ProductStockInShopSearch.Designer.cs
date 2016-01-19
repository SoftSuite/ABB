namespace ABBClient.Master
{
    partial class ProductStockInShopSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductStockInShopSearch));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnApprove = new System.Windows.Forms.ToolStripButton();
            this.txtSICode = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSiLoid = new System.Windows.Forms.TextBox();
            this.DateToPk = new System.Windows.Forms.DateTimePicker();
            this.DateFromPk = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRQCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchPDStockInShop = new System.Windows.Forms.Button();
            this.grvPDSearchInShop = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ChkApprove = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SICODE = new System.Windows.Forms.DataGridViewLinkColumn();
            this.RECEIVEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RQCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REQDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAREHOUSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STILOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SISTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPDSearchInShop)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnDelete,
            this.btnPrint,
            this.btnApprove});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(734, 25);
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
            // btnApprove
            // 
            this.btnApprove.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnApprove.Image = global::ABBClient.Properties.Resources.icn_submit;
            this.btnApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(53, 22);
            this.btnApprove.Text = "ยืนยัน";
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // txtSICode
            // 
            this.txtSICode.Location = new System.Drawing.Point(170, 8);
            this.txtSICode.Name = "txtSICode";
            this.txtSICode.Size = new System.Drawing.Size(172, 20);
            this.txtSICode.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtSiLoid);
            this.panel1.Controls.Add(this.DateToPk);
            this.panel1.Controls.Add(this.DateFromPk);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtRQCode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSearchPDStockInShop);
            this.panel1.Controls.Add(this.txtSICode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(724, 145);
            this.panel1.TabIndex = 2;
            // 
            // txtSiLoid
            // 
            this.txtSiLoid.Location = new System.Drawing.Point(467, 108);
            this.txtSiLoid.Name = "txtSiLoid";
            this.txtSiLoid.Size = new System.Drawing.Size(37, 20);
            this.txtSiLoid.TabIndex = 18;
            this.txtSiLoid.Visible = false;
            // 
            // DateToPk
            // 
            this.DateToPk.Location = new System.Drawing.Point(379, 32);
            this.DateToPk.Name = "DateToPk";
            this.DateToPk.Size = new System.Drawing.Size(172, 20);
            this.DateToPk.TabIndex = 17;
            this.DateToPk.Value = new System.DateTime(2007, 12, 21, 0, 0, 0, 0);
            // 
            // DateFromPk
            // 
            this.DateFromPk.Location = new System.Drawing.Point(170, 32);
            this.DateFromPk.Name = "DateFromPk";
            this.DateFromPk.Size = new System.Drawing.Size(172, 20);
            this.DateFromPk.TabIndex = 16;
            this.DateFromPk.Value = new System.DateTime(2007, 12, 21, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(354, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "ถึง";
            // 
            // txtRQCode
            // 
            this.txtRQCode.Location = new System.Drawing.Point(170, 58);
            this.txtRQCode.Name = "txtRQCode";
            this.txtRQCode.Size = new System.Drawing.Size(172, 20);
            this.txtRQCode.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "เลขที่ใบเบิก";
            // 
            // btnSearchPDStockInShop
            // 
            this.btnSearchPDStockInShop.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchPDStockInShop.Image")));
            this.btnSearchPDStockInShop.Location = new System.Drawing.Point(357, 58);
            this.btnSearchPDStockInShop.Name = "btnSearchPDStockInShop";
            this.btnSearchPDStockInShop.Size = new System.Drawing.Size(25, 25);
            this.btnSearchPDStockInShop.TabIndex = 11;
            this.btnSearchPDStockInShop.UseVisualStyleBackColor = true;
            this.btnSearchPDStockInShop.Click += new System.EventHandler(this.btnSearchPDStockInShop_Click);
            // 
            // grvPDSearchInShop
            // 
            this.grvPDSearchInShop.AllowUserToAddRows = false;
            this.grvPDSearchInShop.AllowUserToDeleteRows = false;
            this.grvPDSearchInShop.AllowUserToOrderColumns = true;
            this.grvPDSearchInShop.BackgroundColor = System.Drawing.Color.Silver;
            this.grvPDSearchInShop.ColumnHeadersHeight = 25;
            this.grvPDSearchInShop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grvPDSearchInShop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChkApprove,
            this.LOID,
            this.ORDERNO,
            this.SICODE,
            this.RECEIVEDATE,
            this.RQCODE,
            this.REQDATE,
            this.TOTAL,
            this.WAREHOUSE,
            this.STILOID,
            this.SISTATUS});
            this.grvPDSearchInShop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvPDSearchInShop.Location = new System.Drawing.Point(5, 180);
            this.grvPDSearchInShop.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.grvPDSearchInShop.MultiSelect = false;
            this.grvPDSearchInShop.Name = "grvPDSearchInShop";
            this.grvPDSearchInShop.RowHeadersWidth = 25;
            this.grvPDSearchInShop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvPDSearchInShop.Size = new System.Drawing.Size(724, 233);
            this.grvPDSearchInShop.TabIndex = 3;
            this.grvPDSearchInShop.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvPDSearchInShop_CellClick);
            this.grvPDSearchInShop.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvPDSearchInShop_CellContentClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grvPDSearchInShop, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(734, 418);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // ChkApprove
            // 
            this.ChkApprove.DataPropertyName = "CHKAPPROVE";
            this.ChkApprove.HeaderText = "เลือก";
            this.ChkApprove.Name = "ChkApprove";
            // 
            // LOID
            // 
            this.LOID.DataPropertyName = "LOID";
            this.LOID.HeaderText = "LOID";
            this.LOID.Name = "LOID";
            this.LOID.Visible = false;
            // 
            // ORDERNO
            // 
            this.ORDERNO.DataPropertyName = "ORDERNO";
            this.ORDERNO.HeaderText = "ลำดับ";
            this.ORDERNO.Name = "ORDERNO";
            this.ORDERNO.Width = 50;
            // 
            // SICODE
            // 
            this.SICODE.DataPropertyName = "SICODE";
            this.SICODE.HeaderText = "เลขที่";
            this.SICODE.Name = "SICODE";
            this.SICODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SICODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // RECEIVEDATE
            // 
            this.RECEIVEDATE.DataPropertyName = "RECEIVEDATE";
            this.RECEIVEDATE.HeaderText = "วันที่";
            this.RECEIVEDATE.Name = "RECEIVEDATE";
            // 
            // RQCODE
            // 
            this.RQCODE.DataPropertyName = "RQCODE";
            this.RQCODE.HeaderText = "เลขที่รับคำสั่งซื้อ";
            this.RQCODE.Name = "RQCODE";
            // 
            // REQDATE
            // 
            this.REQDATE.DataPropertyName = "REQDATE";
            this.REQDATE.HeaderText = "วันที่รับคำสั่งซื้อ";
            this.REQDATE.Name = "REQDATE";
            // 
            // TOTAL
            // 
            this.TOTAL.DataPropertyName = "TOTAL";
            this.TOTAL.HeaderText = "ยอดสุทธิ";
            this.TOTAL.Name = "TOTAL";
            // 
            // WAREHOUSE
            // 
            this.WAREHOUSE.DataPropertyName = "WAREHOUSE";
            this.WAREHOUSE.HeaderText = "WAREHOUSE";
            this.WAREHOUSE.Name = "WAREHOUSE";
            this.WAREHOUSE.Visible = false;
            // 
            // STILOID
            // 
            this.STILOID.DataPropertyName = "STILOID";
            this.STILOID.HeaderText = "STILOID";
            this.STILOID.Name = "STILOID";
            this.STILOID.Visible = false;
            // 
            // SISTATUS
            // 
            this.SISTATUS.DataPropertyName = "SISTATUS";
            this.SISTATUS.HeaderText = "SISTATUS";
            this.SISTATUS.Name = "SISTATUS";
            this.SISTATUS.Visible = false;
            // 
            // ProductStockInShopSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 418);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProductStockInShopSearch";
            this.Text = "ใบแสดงยกยอดรับ";
            this.Load += new System.EventHandler(this.ProductStockInShopSearch_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPDSearchInShop)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.TextBox txtSICode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearchPDStockInShop;
        private System.Windows.Forms.DataGridView grvPDSearchInShop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRQCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DateFromPk;
        private System.Windows.Forms.DateTimePicker DateToPk;
        private System.Windows.Forms.ToolStripButton btnApprove;
        private System.Windows.Forms.TextBox txtSiLoid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkApprove;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERNO;
        private System.Windows.Forms.DataGridViewLinkColumn SICODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIVEDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RQCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn REQDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAREHOUSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STILOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SISTATUS;
    }
}