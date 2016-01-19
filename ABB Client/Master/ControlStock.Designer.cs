namespace ABBClient.Master
{
    partial class ControlStock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlStock));
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtWareHouse = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grvControlStock = new System.Windows.Forms.DataGridView();
            this.ORDERNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BARCODEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PMLOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WHLOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAREHOUSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Minimum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Standard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Maximum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtUnitMaster = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtUnitMax = new System.Windows.Forms.TextBox();
            this.txtUnitStd = new System.Windows.Forms.TextBox();
            this.txtUnitMin = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPLoid = new System.Windows.Forms.TextBox();
            this.txtWHLoid = new System.Windows.Forms.TextBox();
            this.txtPMLoid = new System.Windows.Forms.TextBox();
            this.btnSearchProduct = new System.Windows.Forms.Button();
            this.txtProductDetail = new System.Windows.Forms.TextBox();
            this.txtMaximum = new System.Windows.Forms.TextBox();
            this.txtMinimum = new System.Windows.Forms.TextBox();
            this.txtStandard = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grvControlStock)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(169, 30);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 6;
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtWareHouse
            // 
            this.txtWareHouse.Location = new System.Drawing.Point(169, 6);
            this.txtWareHouse.Name = "txtWareHouse";
            this.txtWareHouse.Size = new System.Drawing.Size(270, 20);
            this.txtWareHouse.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "ปริมาณสูงสุด";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "ปริมาณต่ำสุด";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "ปริมาณคงที่";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "สินค้า";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(80, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "คลัง";
            // 
            // grvControlStock
            // 
            this.grvControlStock.AllowUserToAddRows = false;
            this.grvControlStock.AllowUserToDeleteRows = false;
            this.grvControlStock.AllowUserToOrderColumns = true;
            this.grvControlStock.BackgroundColor = System.Drawing.Color.Silver;
            this.grvControlStock.ColumnHeadersHeight = 25;
            this.grvControlStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grvControlStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORDERNO,
            this.BARCODEE,
            this.PMLOID,
            this.WHLOID,
            this.WAREHOUSE,
            this.NAME,
            this.Minimum,
            this.Standard,
            this.Maximum,
            this.PRODUCT});
            this.grvControlStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvControlStock.Location = new System.Drawing.Point(5, 215);
            this.grvControlStock.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.grvControlStock.MultiSelect = false;
            this.grvControlStock.Name = "grvControlStock";
            this.grvControlStock.ReadOnly = true;
            this.grvControlStock.RowHeadersWidth = 25;
            this.grvControlStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvControlStock.Size = new System.Drawing.Size(782, 346);
            this.grvControlStock.TabIndex = 3;
            this.grvControlStock.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvControlStock_CellClick);
            // 
            // ORDERNO
            // 
            this.ORDERNO.DataPropertyName = "ORDERNO";
            this.ORDERNO.HeaderText = "ลำดับ";
            this.ORDERNO.Name = "ORDERNO";
            this.ORDERNO.ReadOnly = true;
            this.ORDERNO.Width = 50;
            // 
            // BARCODEE
            // 
            this.BARCODEE.DataPropertyName = "BARCODE";
            this.BARCODEE.HeaderText = "บาร์โค้ด";
            this.BARCODEE.Name = "BARCODEE";
            this.BARCODEE.ReadOnly = true;
            // 
            // PMLOID
            // 
            this.PMLOID.DataPropertyName = "PMLOID";
            this.PMLOID.HeaderText = "PMLOID";
            this.PMLOID.Name = "PMLOID";
            this.PMLOID.ReadOnly = true;
            this.PMLOID.Visible = false;
            // 
            // WHLOID
            // 
            this.WHLOID.DataPropertyName = "WHLOID";
            this.WHLOID.HeaderText = "WHLOID";
            this.WHLOID.Name = "WHLOID";
            this.WHLOID.ReadOnly = true;
            this.WHLOID.Visible = false;
            // 
            // WAREHOUSE
            // 
            this.WAREHOUSE.DataPropertyName = "WAREHOUSE";
            this.WAREHOUSE.HeaderText = "WAREHOUSE";
            this.WAREHOUSE.Name = "WAREHOUSE";
            this.WAREHOUSE.ReadOnly = true;
            this.WAREHOUSE.Visible = false;
            // 
            // NAME
            // 
            this.NAME.DataPropertyName = "NAME";
            this.NAME.HeaderText = "ชื่อสินค้า";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            this.NAME.Width = 280;
            // 
            // Minimum
            // 
            this.Minimum.DataPropertyName = "MINIMUM";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "#,##0";
            dataGridViewCellStyle4.NullValue = null;
            this.Minimum.DefaultCellStyle = dataGridViewCellStyle4;
            this.Minimum.HeaderText = "ปริมาณต่ำสุด";
            this.Minimum.Name = "Minimum";
            this.Minimum.ReadOnly = true;
            // 
            // Standard
            // 
            this.Standard.DataPropertyName = "STANDARD";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "#,##0";
            dataGridViewCellStyle5.NullValue = null;
            this.Standard.DefaultCellStyle = dataGridViewCellStyle5;
            this.Standard.HeaderText = "ปริมาณคงที่";
            this.Standard.Name = "Standard";
            this.Standard.ReadOnly = true;
            // 
            // Maximum
            // 
            this.Maximum.DataPropertyName = "MAXIMUM";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "#,##0";
            dataGridViewCellStyle6.NullValue = null;
            this.Maximum.DefaultCellStyle = dataGridViewCellStyle6;
            this.Maximum.HeaderText = "ปริมาณสูงสุด";
            this.Maximum.Name = "Maximum";
            this.Maximum.ReadOnly = true;
            // 
            // PRODUCT
            // 
            this.PRODUCT.DataPropertyName = "PRODUCT";
            this.PRODUCT.HeaderText = "PRODUCT";
            this.PRODUCT.Name = "PRODUCT";
            this.PRODUCT.ReadOnly = true;
            this.PRODUCT.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnSave,
            this.btnCancel,
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
            // btnCancel
            // 
            this.btnCancel.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnCancel.Image = global::ABBClient.Properties.Resources.icn_cancel;
            this.btnCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 1, 1, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(54, 22);
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1);
            this.tableLayoutPanel1.Controls.Add(this.panel1);
            this.tableLayoutPanel1.Controls.Add(this.grvControlStock);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 566);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtUnitMaster);
            this.panel1.Controls.Add(this.txtUnit);
            this.panel1.Controls.Add(this.txtUnitMax);
            this.panel1.Controls.Add(this.txtUnitStd);
            this.panel1.Controls.Add(this.txtUnitMin);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtPLoid);
            this.panel1.Controls.Add(this.txtWHLoid);
            this.panel1.Controls.Add(this.txtPMLoid);
            this.panel1.Controls.Add(this.btnSearchProduct);
            this.panel1.Controls.Add(this.txtProductDetail);
            this.panel1.Controls.Add(this.txtMaximum);
            this.panel1.Controls.Add(this.txtMinimum);
            this.panel1.Controls.Add(this.txtStandard);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.txtWareHouse);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 180);
            this.panel1.TabIndex = 2;
            // 
            // txtUnitMaster
            // 
            this.txtUnitMaster.BackColor = System.Drawing.SystemColors.Control;
            this.txtUnitMaster.Location = new System.Drawing.Point(631, 54);
            this.txtUnitMaster.Name = "txtUnitMaster";
            this.txtUnitMaster.ReadOnly = true;
            this.txtUnitMaster.Size = new System.Drawing.Size(52, 20);
            this.txtUnitMaster.TabIndex = 22;
            this.txtUnitMaster.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUnitMaster.Visible = false;
            // 
            // txtUnit
            // 
            this.txtUnit.BackColor = System.Drawing.SystemColors.Control;
            this.txtUnit.Location = new System.Drawing.Point(525, 54);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(100, 20);
            this.txtUnit.TabIndex = 21;
            this.txtUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUnitMax
            // 
            this.txtUnitMax.Location = new System.Drawing.Point(286, 127);
            this.txtUnitMax.Name = "txtUnitMax";
            this.txtUnitMax.ReadOnly = true;
            this.txtUnitMax.Size = new System.Drawing.Size(100, 20);
            this.txtUnitMax.TabIndex = 20;
            this.txtUnitMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUnitStd
            // 
            this.txtUnitStd.Location = new System.Drawing.Point(286, 103);
            this.txtUnitStd.Name = "txtUnitStd";
            this.txtUnitStd.ReadOnly = true;
            this.txtUnitStd.Size = new System.Drawing.Size(100, 20);
            this.txtUnitStd.TabIndex = 19;
            this.txtUnitStd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUnitMin
            // 
            this.txtUnitMin.Location = new System.Drawing.Point(286, 79);
            this.txtUnitMin.Name = "txtUnitMin";
            this.txtUnitMin.ReadOnly = true;
            this.txtUnitMin.Size = new System.Drawing.Size(100, 20);
            this.txtUnitMin.TabIndex = 18;
            this.txtUnitMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(270, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(270, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(270, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "*";
            // 
            // txtPLoid
            // 
            this.txtPLoid.Location = new System.Drawing.Point(528, 85);
            this.txtPLoid.Name = "txtPLoid";
            this.txtPLoid.Size = new System.Drawing.Size(27, 20);
            this.txtPLoid.TabIndex = 14;
            // 
            // txtWHLoid
            // 
            this.txtWHLoid.Location = new System.Drawing.Point(561, 85);
            this.txtWHLoid.Name = "txtWHLoid";
            this.txtWHLoid.Size = new System.Drawing.Size(26, 20);
            this.txtWHLoid.TabIndex = 13;
            // 
            // txtPMLoid
            // 
            this.txtPMLoid.Location = new System.Drawing.Point(593, 85);
            this.txtPMLoid.Name = "txtPMLoid";
            this.txtPMLoid.Size = new System.Drawing.Size(19, 20);
            this.txtPMLoid.TabIndex = 12;
            // 
            // btnSearchProduct
            // 
            this.btnSearchProduct.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchProduct.Image")));
            this.btnSearchProduct.Location = new System.Drawing.Point(271, 26);
            this.btnSearchProduct.Name = "btnSearchProduct";
            this.btnSearchProduct.Size = new System.Drawing.Size(25, 25);
            this.btnSearchProduct.TabIndex = 11;
            this.btnSearchProduct.UseVisualStyleBackColor = true;
            this.btnSearchProduct.Click += new System.EventHandler(this.btnSearchProduct_Click);
            // 
            // txtProductDetail
            // 
            this.txtProductDetail.Location = new System.Drawing.Point(169, 54);
            this.txtProductDetail.Name = "txtProductDetail";
            this.txtProductDetail.ReadOnly = true;
            this.txtProductDetail.Size = new System.Drawing.Size(351, 20);
            this.txtProductDetail.TabIndex = 10;
            // 
            // txtMaximum
            // 
            this.txtMaximum.Location = new System.Drawing.Point(169, 127);
            this.txtMaximum.Name = "txtMaximum";
            this.txtMaximum.Size = new System.Drawing.Size(100, 20);
            this.txtMaximum.TabIndex = 9;
            this.txtMaximum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaximum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaximum_KeyPress);
            // 
            // txtMinimum
            // 
            this.txtMinimum.Location = new System.Drawing.Point(169, 79);
            this.txtMinimum.Name = "txtMinimum";
            this.txtMinimum.Size = new System.Drawing.Size(100, 20);
            this.txtMinimum.TabIndex = 8;
            this.txtMinimum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMinimum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMinimum_KeyPress);
            // 
            // txtStandard
            // 
            this.txtStandard.Location = new System.Drawing.Point(169, 103);
            this.txtStandard.Name = "txtStandard";
            this.txtStandard.Size = new System.Drawing.Size(100, 20);
            this.txtStandard.TabIndex = 7;
            this.txtStandard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStandard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStandard_KeyPress);
            // 
            // ControlStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ControlStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ควบคุมปริมาณสินค้า";
            this.Load += new System.EventHandler(this.ControlStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvControlStock)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtWareHouse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView grvControlStock;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.Button btnSearchProduct;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtProductDetail;
        private System.Windows.Forms.TextBox txtMaximum;
        private System.Windows.Forms.TextBox txtMinimum;
        private System.Windows.Forms.TextBox txtStandard;
        private System.Windows.Forms.TextBox txtPMLoid;
        private System.Windows.Forms.TextBox txtWHLoid;
        private System.Windows.Forms.TextBox txtPLoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtUnitMax;
        private System.Windows.Forms.TextBox txtUnitStd;
        private System.Windows.Forms.TextBox txtUnitMin;
        private System.Windows.Forms.TextBox txtUnitMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BARCODEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PMLOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WHLOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAREHOUSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn Minimum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Standard;
        private System.Windows.Forms.DataGridViewTextBoxColumn Maximum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCT;

    }
}