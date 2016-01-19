namespace ABBClient.Master
{
    partial class ProductStockInShop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductStockInShop));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearchPDStockInShop = new System.Windows.Forms.Button();
            this.txtRqCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.grvProductStockInShop = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCreateby = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnApprove = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSiiStatus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReceivedate = new System.Windows.Forms.TextBox();
            this.txtSiCode = new System.Windows.Forms.TextBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SISTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIISTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDLOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RQILOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIILOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SILOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RQ_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECEIVE_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UNITNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtChk = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductStockInShop)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearchPDStockInShop);
            this.groupBox1.Controls.Add(this.txtRqCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 75);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // btnSearchPDStockInShop
            // 
            this.btnSearchPDStockInShop.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchPDStockInShop.Image")));
            this.btnSearchPDStockInShop.Location = new System.Drawing.Point(310, 13);
            this.btnSearchPDStockInShop.Name = "btnSearchPDStockInShop";
            this.btnSearchPDStockInShop.Size = new System.Drawing.Size(25, 25);
            this.btnSearchPDStockInShop.TabIndex = 12;
            this.btnSearchPDStockInShop.UseVisualStyleBackColor = true;
            this.btnSearchPDStockInShop.Click += new System.EventHandler(this.btnSearchPDStockInShop_Click);
            // 
            // txtRqCode
            // 
            this.txtRqCode.Location = new System.Drawing.Point(130, 13);
            this.txtRqCode.Name = "txtRqCode";
            this.txtRqCode.Size = new System.Drawing.Size(174, 20);
            this.txtRqCode.TabIndex = 1;
            this.txtRqCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRqCode_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "เลขที่ใบเบิกออกจากคลัง";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(12, 385);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(736, 91);
            this.txtRemark.TabIndex = 7;
            // 
            // grvProductStockInShop
            // 
            this.grvProductStockInShop.AllowUserToAddRows = false;
            this.grvProductStockInShop.AllowUserToOrderColumns = true;
            this.grvProductStockInShop.BackgroundColor = System.Drawing.Color.Silver;
            this.grvProductStockInShop.ColumnHeadersHeight = 25;
            this.grvProductStockInShop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grvProductStockInShop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnEdit,
            this.SISTATUS,
            this.SIISTATUS,
            this.PDLOID,
            this.RQILOID,
            this.SIILOID,
            this.ORDERNO,
            this.SILOID,
            this.PDCODE,
            this.PDNAME,
            this.RQ_QTY,
            this.RECEIVE_QTY,
            this.UNITNAME,
            this.PRICE,
            this.TOTAL});
            this.grvProductStockInShop.Location = new System.Drawing.Point(12, 116);
            this.grvProductStockInShop.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.grvProductStockInShop.MultiSelect = false;
            this.grvProductStockInShop.Name = "grvProductStockInShop";
            this.grvProductStockInShop.RowHeadersWidth = 25;
            this.grvProductStockInShop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvProductStockInShop.Size = new System.Drawing.Size(736, 248);
            this.grvProductStockInShop.TabIndex = 3;
            this.grvProductStockInShop.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvProductStockInShop_CellClick);
            this.grvProductStockInShop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grvProductStockInShop_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTotal);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtCreateby);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(755, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 448);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(71, 320);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(85, 20);
            this.txtTotal.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 323);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "ยอดสุทธิ";
            // 
            // txtCreateby
            // 
            this.txtCreateby.Location = new System.Drawing.Point(71, 17);
            this.txtCreateby.Name = "txtCreateby";
            this.txtCreateby.Size = new System.Drawing.Size(85, 20);
            this.txtCreateby.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "พนักงานขาย";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 369);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "หมายเหตุ";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnApprove,
            this.btnPrint,
            this.btnBack});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(931, 25);
            this.toolStrip2.TabIndex = 18;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnSave.Image = global::ABBClient.Properties.Resources.icn_save;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(54, 22);
            this.btnSave.Text = "บันทึก";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // btnBack
            // 
            this.btnBack.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBack.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(86, 22);
            this.btnBack.Text = "กลับหน้าหลัก";
            this.btnBack.ToolTipText = "กลับหน้าหลัก";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtChk);
            this.groupBox2.Controls.Add(this.txtSiiStatus);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtReceivedate);
            this.groupBox2.Controls.Add(this.txtSiCode);
            this.groupBox2.Location = new System.Drawing.Point(367, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 75);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // txtSiiStatus
            // 
            this.txtSiiStatus.Location = new System.Drawing.Point(327, 14);
            this.txtSiiStatus.Name = "txtSiiStatus";
            this.txtSiiStatus.Size = new System.Drawing.Size(40, 20);
            this.txtSiiStatus.TabIndex = 21;
            this.txtSiiStatus.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "วันที่ยกยอดรับ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "เลขที่ยกยอดรับ";
            // 
            // txtReceivedate
            // 
            this.txtReceivedate.Location = new System.Drawing.Point(101, 40);
            this.txtReceivedate.Name = "txtReceivedate";
            this.txtReceivedate.Size = new System.Drawing.Size(181, 20);
            this.txtReceivedate.TabIndex = 1;
            // 
            // txtSiCode
            // 
            this.txtSiCode.Location = new System.Drawing.Point(101, 14);
            this.txtSiCode.Name = "txtSiCode";
            this.txtSiCode.Size = new System.Drawing.Size(181, 20);
            this.txtSiCode.TabIndex = 0;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::ABBClient.Properties.Resources.icn_edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // btnEdit
            // 
            this.btnEdit.HeaderText = "";
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseColumnTextForButtonValue = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Width = 50;
            // 
            // SISTATUS
            // 
            this.SISTATUS.DataPropertyName = "SISTATUS";
            this.SISTATUS.HeaderText = "SISTATUS";
            this.SISTATUS.Name = "SISTATUS";
            this.SISTATUS.Visible = false;
            // 
            // SIISTATUS
            // 
            this.SIISTATUS.DataPropertyName = "SIISTATUS";
            this.SIISTATUS.HeaderText = "SIISTATUS";
            this.SIISTATUS.Name = "SIISTATUS";
            this.SIISTATUS.Visible = false;
            // 
            // PDLOID
            // 
            this.PDLOID.DataPropertyName = "PDLOID";
            this.PDLOID.HeaderText = "PDLOID";
            this.PDLOID.Name = "PDLOID";
            this.PDLOID.Visible = false;
            // 
            // RQILOID
            // 
            this.RQILOID.DataPropertyName = "RQILOID";
            this.RQILOID.HeaderText = "RQILOID";
            this.RQILOID.Name = "RQILOID";
            this.RQILOID.Visible = false;
            // 
            // SIILOID
            // 
            this.SIILOID.DataPropertyName = "SIILOID";
            this.SIILOID.HeaderText = "SIILOID";
            this.SIILOID.Name = "SIILOID";
            this.SIILOID.Visible = false;
            // 
            // ORDERNO
            // 
            this.ORDERNO.DataPropertyName = "ORDERNO";
            this.ORDERNO.HeaderText = "ลำดับ";
            this.ORDERNO.Name = "ORDERNO";
            this.ORDERNO.ReadOnly = true;
            this.ORDERNO.Width = 50;
            // 
            // SILOID
            // 
            this.SILOID.DataPropertyName = "SILOID";
            this.SILOID.HeaderText = "SILOID";
            this.SILOID.Name = "SILOID";
            this.SILOID.ReadOnly = true;
            this.SILOID.Visible = false;
            // 
            // PDCODE
            // 
            this.PDCODE.DataPropertyName = "PDCODE";
            this.PDCODE.HeaderText = "บาร์โค้ด";
            this.PDCODE.Name = "PDCODE";
            this.PDCODE.ReadOnly = true;
            // 
            // PDNAME
            // 
            this.PDNAME.DataPropertyName = "PDNAME";
            this.PDNAME.HeaderText = "สินค้า";
            this.PDNAME.Name = "PDNAME";
            this.PDNAME.ReadOnly = true;
            // 
            // RQ_QTY
            // 
            this.RQ_QTY.DataPropertyName = "RQ_QTY";
            this.RQ_QTY.HeaderText = "จำนวนเบิก";
            this.RQ_QTY.Name = "RQ_QTY";
            this.RQ_QTY.ReadOnly = true;
            // 
            // RECEIVE_QTY
            // 
            this.RECEIVE_QTY.DataPropertyName = "RECEIVE_QTY";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.RECEIVE_QTY.DefaultCellStyle = dataGridViewCellStyle2;
            this.RECEIVE_QTY.HeaderText = "จำนวนรับ";
            this.RECEIVE_QTY.Name = "RECEIVE_QTY";
            // 
            // UNITNAME
            // 
            this.UNITNAME.DataPropertyName = "UNITNAME";
            this.UNITNAME.HeaderText = "หน่วย";
            this.UNITNAME.Name = "UNITNAME";
            this.UNITNAME.ReadOnly = true;
            // 
            // PRICE
            // 
            this.PRICE.DataPropertyName = "PRICE";
            this.PRICE.HeaderText = "ราคา";
            this.PRICE.Name = "PRICE";
            this.PRICE.ReadOnly = true;
            // 
            // TOTAL
            // 
            this.TOTAL.DataPropertyName = "TOTAL";
            this.TOTAL.HeaderText = "รวมเงิน";
            this.TOTAL.Name = "TOTAL";
            this.TOTAL.ReadOnly = true;
            // 
            // txtChk
            // 
            this.txtChk.Location = new System.Drawing.Point(327, 40);
            this.txtChk.Name = "txtChk";
            this.txtChk.Size = new System.Drawing.Size(40, 20);
            this.txtChk.TabIndex = 22;
            this.txtChk.Visible = false;
            // 
            // ProductStockInShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 488);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.grvProductStockInShop);
            this.Name = "ProductStockInShop";
            this.Text = "ใบแสดงยกยอดรับ";
            this.Load += new System.EventHandler(this.ProductStockInShop_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductStockInShop)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearchPDStockInShop;
        private System.Windows.Forms.TextBox txtRqCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.DataGridView grvProductStockInShop;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCreateby;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtReceivedate;
        private System.Windows.Forms.TextBox txtSiCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSiiStatus;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.ToolStripButton btnApprove;
        private System.Windows.Forms.DataGridViewButtonColumn btnEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SISTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIISTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDLOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RQILOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIILOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SILOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn RQ_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIVE_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn UNITNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL;
        private System.Windows.Forms.TextBox txtChk;

    }
}