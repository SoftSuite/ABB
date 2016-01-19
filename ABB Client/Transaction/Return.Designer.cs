namespace ABBClient.Transaction
{
    partial class Return
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Return));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReceivedate = new System.Windows.Forms.TextBox();
            this.txtSiCode = new System.Windows.Forms.TextBox();
            this.txtGrandTot = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSiStatus = new System.Windows.Forms.TextBox();
            this.txtSiiStatus = new System.Windows.Forms.TextBox();
            this.txtSiLoid = new System.Windows.Forms.TextBox();
            this.txtChk = new System.Windows.Forms.TextBox();
            this.txtCreateby = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearchSlip = new System.Windows.Forms.Button();
            this.txtRqCode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dpReqDate = new System.Windows.Forms.DateTimePicker();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.txtCusCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.grvReturn = new System.Windows.Forms.DataGridView();
            this.ORDERNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SILOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIILOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SISTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIISTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDLOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RQILOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ULOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_OLD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.toolStrip2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvReturn)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnPrint,
            this.btnBack});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(931, 25);
            this.toolStrip2.TabIndex = 25;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 375);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "สาเหตุการคืน";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtReceivedate);
            this.groupBox2.Controls.Add(this.txtSiCode);
            this.groupBox2.Location = new System.Drawing.Point(553, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 75);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "วันที่";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "เลขที่";
            // 
            // txtReceivedate
            // 
            this.txtReceivedate.Enabled = false;
            this.txtReceivedate.Location = new System.Drawing.Point(43, 40);
            this.txtReceivedate.Name = "txtReceivedate";
            this.txtReceivedate.Size = new System.Drawing.Size(120, 20);
            this.txtReceivedate.TabIndex = 1;
            // 
            // txtSiCode
            // 
            this.txtSiCode.Enabled = false;
            this.txtSiCode.Location = new System.Drawing.Point(43, 14);
            this.txtSiCode.Name = "txtSiCode";
            this.txtSiCode.Size = new System.Drawing.Size(120, 20);
            this.txtSiCode.TabIndex = 0;
            // 
            // txtGrandTot
            // 
            this.txtGrandTot.Enabled = false;
            this.txtGrandTot.Location = new System.Drawing.Point(71, 320);
            this.txtGrandTot.Name = "txtGrandTot";
            this.txtGrandTot.Size = new System.Drawing.Size(85, 20);
            this.txtGrandTot.TabIndex = 20;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSiStatus);
            this.groupBox3.Controls.Add(this.txtSiiStatus);
            this.groupBox3.Controls.Add(this.txtSiLoid);
            this.groupBox3.Controls.Add(this.txtChk);
            this.groupBox3.Controls.Add(this.txtGrandTot);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtCreateby);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(755, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 448);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            // 
            // txtSiStatus
            // 
            this.txtSiStatus.Enabled = false;
            this.txtSiStatus.Location = new System.Drawing.Point(9, 107);
            this.txtSiStatus.Name = "txtSiStatus";
            this.txtSiStatus.Size = new System.Drawing.Size(88, 20);
            this.txtSiStatus.TabIndex = 23;
            this.txtSiStatus.Visible = false;
            // 
            // txtSiiStatus
            // 
            this.txtSiiStatus.Enabled = false;
            this.txtSiiStatus.Location = new System.Drawing.Point(9, 133);
            this.txtSiiStatus.Name = "txtSiiStatus";
            this.txtSiiStatus.Size = new System.Drawing.Size(88, 20);
            this.txtSiiStatus.TabIndex = 22;
            this.txtSiiStatus.Visible = false;
            // 
            // txtSiLoid
            // 
            this.txtSiLoid.Enabled = false;
            this.txtSiLoid.Location = new System.Drawing.Point(9, 81);
            this.txtSiLoid.Name = "txtSiLoid";
            this.txtSiLoid.Size = new System.Drawing.Size(88, 20);
            this.txtSiLoid.TabIndex = 21;
            this.txtSiLoid.Visible = false;
            // 
            // txtChk
            // 
            this.txtChk.Enabled = false;
            this.txtChk.Location = new System.Drawing.Point(9, 55);
            this.txtChk.Name = "txtChk";
            this.txtChk.Size = new System.Drawing.Size(88, 20);
            this.txtChk.TabIndex = 4;
            this.txtChk.Visible = false;
            // 
            // txtCreateby
            // 
            this.txtCreateby.Enabled = false;
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
            // btnSearchSlip
            // 
            this.btnSearchSlip.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchSlip.Image")));
            this.btnSearchSlip.Location = new System.Drawing.Point(290, 8);
            this.btnSearchSlip.Name = "btnSearchSlip";
            this.btnSearchSlip.Size = new System.Drawing.Size(25, 25);
            this.btnSearchSlip.TabIndex = 12;
            this.btnSearchSlip.UseVisualStyleBackColor = true;
            this.btnSearchSlip.Click += new System.EventHandler(this.btnSearchSlip_Click);
            // 
            // txtRqCode
            // 
            this.txtRqCode.Location = new System.Drawing.Point(110, 14);
            this.txtRqCode.Name = "txtRqCode";
            this.txtRqCode.Size = new System.Drawing.Size(174, 20);
            this.txtRqCode.TabIndex = 1;
            this.txtRqCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRqCode_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dpReqDate);
            this.groupBox1.Controls.Add(this.txtCusName);
            this.groupBox1.Controls.Add(this.txtCusCode);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnSearchSlip);
            this.groupBox1.Controls.Add(this.txtRqCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 75);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // dpReqDate
            // 
            this.dpReqDate.Enabled = false;
            this.dpReqDate.Location = new System.Drawing.Point(364, 12);
            this.dpReqDate.Name = "dpReqDate";
            this.dpReqDate.Size = new System.Drawing.Size(165, 20);
            this.dpReqDate.TabIndex = 29;
            this.dpReqDate.Value = new System.DateTime(2007, 12, 21, 0, 0, 0, 0);
            // 
            // txtCusName
            // 
            this.txtCusName.Enabled = false;
            this.txtCusName.Location = new System.Drawing.Point(211, 42);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(318, 20);
            this.txtCusName.TabIndex = 15;
            // 
            // txtCusCode
            // 
            this.txtCusCode.Enabled = false;
            this.txtCusCode.Location = new System.Drawing.Point(110, 42);
            this.txtCusCode.Name = "txtCusCode";
            this.txtCusCode.Size = new System.Drawing.Size(95, 20);
            this.txtCusCode.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "รหัสลูกค้า";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(322, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "ลงวันที่";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "เลขที่ใบเสร็จรับเงิน";
            // 
            // txtReason
            // 
            this.txtReason.Enabled = false;
            this.txtReason.Location = new System.Drawing.Point(12, 391);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReason.Size = new System.Drawing.Size(375, 91);
            this.txtReason.TabIndex = 23;
            // 
            // grvReturn
            // 
            this.grvReturn.AllowUserToAddRows = false;
            this.grvReturn.AllowUserToOrderColumns = true;
            this.grvReturn.BackgroundColor = System.Drawing.Color.Silver;
            this.grvReturn.ColumnHeadersHeight = 25;
            this.grvReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grvReturn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORDERNO,
            this.PDCODE,
            this.PDNAME,
            this.QTY,
            this.UNAME,
            this.PRICE,
            this.TOTAL,
            this.SILOID,
            this.SIILOID,
            this.SISTATUS,
            this.SIISTATUS,
            this.PDLOID,
            this.RQILOID,
            this.ULOID,
            this.QTY_OLD});
            this.grvReturn.Location = new System.Drawing.Point(12, 122);
            this.grvReturn.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.grvReturn.MultiSelect = false;
            this.grvReturn.Name = "grvReturn";
            this.grvReturn.RowHeadersWidth = 25;
            this.grvReturn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvReturn.Size = new System.Drawing.Size(736, 248);
            this.grvReturn.TabIndex = 20;
            this.grvReturn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvReturn_CellClick);
            this.grvReturn.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grvReturn_EditingControlShowing);
            // 
            // ORDERNO
            // 
            this.ORDERNO.DataPropertyName = "ORDERNO";
            this.ORDERNO.HeaderText = "ลำดับ";
            this.ORDERNO.Name = "ORDERNO";
            this.ORDERNO.ReadOnly = true;
            this.ORDERNO.Width = 40;
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
            this.PDNAME.Width = 200;
            // 
            // QTY
            // 
            this.QTY.DataPropertyName = "QTY";
            this.QTY.HeaderText = "จำนวน";
            this.QTY.Name = "QTY";
            this.QTY.Width = 80;
            // 
            // UNAME
            // 
            this.UNAME.DataPropertyName = "UNAME";
            this.UNAME.HeaderText = "หน่วย";
            this.UNAME.Name = "UNAME";
            this.UNAME.ReadOnly = true;
            this.UNAME.Width = 50;
            // 
            // PRICE
            // 
            this.PRICE.DataPropertyName = "PRICE";
            this.PRICE.HeaderText = "ราคา";
            this.PRICE.Name = "PRICE";
            this.PRICE.ReadOnly = true;
            this.PRICE.Width = 60;
            // 
            // TOTAL
            // 
            this.TOTAL.DataPropertyName = "TOTAL";
            this.TOTAL.HeaderText = "รวมเงิน";
            this.TOTAL.Name = "TOTAL";
            this.TOTAL.ReadOnly = true;
            // 
            // SILOID
            // 
            this.SILOID.DataPropertyName = "SILOID";
            this.SILOID.HeaderText = "SILOID";
            this.SILOID.Name = "SILOID";
            this.SILOID.Visible = false;
            // 
            // SIILOID
            // 
            this.SIILOID.DataPropertyName = "SIILOID";
            this.SIILOID.HeaderText = "SIILOID";
            this.SIILOID.Name = "SIILOID";
            this.SIILOID.Visible = false;
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
            // ULOID
            // 
            this.ULOID.DataPropertyName = "ULOID";
            this.ULOID.HeaderText = "ULOID";
            this.ULOID.Name = "ULOID";
            this.ULOID.Visible = false;
            // 
            // QTY_OLD
            // 
            this.QTY_OLD.DataPropertyName = "QTY";
            this.QTY_OLD.HeaderText = "QTY_OLD";
            this.QTY_OLD.Name = "QTY_OLD";
            this.QTY_OLD.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(393, 375);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "หมายเหตุ";
            // 
            // txtRemark
            // 
            this.txtRemark.Enabled = false;
            this.txtRemark.Location = new System.Drawing.Point(393, 391);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(355, 91);
            this.txtRemark.TabIndex = 27;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::ABBClient.Properties.Resources.icn_edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Return
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(931, 488);
            this.ControlBox = false;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.grvReturn);
            this.Name = "Return";
            this.Text = "ใบรับคืนสินค้า";
            this.Load += new System.EventHandler(this.Return_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvReturn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReceivedate;
        private System.Windows.Forms.TextBox txtSiCode;
        private System.Windows.Forms.TextBox txtGrandTot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCreateby;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Button btnSearchSlip;
        private System.Windows.Forms.TextBox txtRqCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.DataGridView grvReturn;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.TextBox txtCusCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtChk;
        private System.Windows.Forms.DateTimePicker dpReqDate;
        private System.Windows.Forms.TextBox txtSiLoid;
        private System.Windows.Forms.TextBox txtSiStatus;
        private System.Windows.Forms.TextBox txtSiiStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn UNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn SILOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIILOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SISTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIISTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDLOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RQILOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ULOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_OLD;

    }
}