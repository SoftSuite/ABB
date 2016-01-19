namespace ABBClient
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.ABBMenu = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogOff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuUploadData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.menuControlStock = new System.Windows.Forms.ToolStripMenuItem();
            this.ProdutStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSales = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBillSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSupport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStockIn = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStockInReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReturnTester = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportStock = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportMoney = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportBillSaleSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportProductSales = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportStockOutDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportProductReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ABBStatus = new System.Windows.Forms.StatusStrip();
            this.ABBToolbar = new System.Windows.Forms.ToolStrip();
            this.btnSales = new System.Windows.Forms.ToolStripButton();
            this.btnReportMoney = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStockIn = new System.Windows.Forms.ToolStripButton();
            this.btnSupport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReportStockOutDoc = new System.Windows.Forms.ToolStripButton();
            this.btnReportStock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLogOff = new System.Windows.Forms.ToolStripButton();
            this.ABBMenu.SuspendLayout();
            this.ABBToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // ABBMenu
            // 
            this.ABBMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuMaster,
            this.menuTransaction,
            this.menuReport,
            this.menuWindow});
            this.ABBMenu.Location = new System.Drawing.Point(0, 0);
            this.ABBMenu.MdiWindowListItem = this.menuWindow;
            this.ABBMenu.Name = "ABBMenu";
            this.ABBMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ABBMenu.ShowItemToolTips = true;
            this.ABBMenu.Size = new System.Drawing.Size(792, 24);
            this.ABBMenu.TabIndex = 0;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLogin,
            this.menuLogOff,
            this.toolStripSeparator5,
            this.menuUploadData,
            this.toolStripSeparator1,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.menuFile.Size = new System.Drawing.Size(31, 20);
            this.menuFile.Text = "ไฟล์";
            // 
            // menuLogin
            // 
            this.menuLogin.Image = global::ABBClient.Properties.Resources.LogOff;
            this.menuLogin.Name = "menuLogin";
            this.menuLogin.Size = new System.Drawing.Size(163, 22);
            this.menuLogin.Text = "ลงชื่อเข้าระบบ";
            this.menuLogin.Click += new System.EventHandler(this.menuLogin_Click);
            // 
            // menuLogOff
            // 
            this.menuLogOff.Image = global::ABBClient.Properties.Resources.LogOff;
            this.menuLogOff.Name = "menuLogOff";
            this.menuLogOff.Size = new System.Drawing.Size(163, 22);
            this.menuLogOff.Text = "ลงชื่อออกจากระบบ";
            this.menuLogOff.Click += new System.EventHandler(this.menuLogOff_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(160, 6);
            // 
            // menuUploadData
            // 
            this.menuUploadData.Name = "menuUploadData";
            this.menuUploadData.Size = new System.Drawing.Size(163, 22);
            this.menuUploadData.Text = "Upload Data";
            this.menuUploadData.Click += new System.EventHandler(this.menuUploadData_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(163, 22);
            this.menuExit.Text = "ปิดโปรแกรม";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuMaster
            // 
            this.menuMaster.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuControlStock,
            this.ProdutStockToolStripMenuItem});
            this.menuMaster.Name = "menuMaster";
            this.menuMaster.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.menuMaster.Size = new System.Drawing.Size(60, 20);
            this.menuMaster.Text = "ข้อมูลหลัก";
            // 
            // menuControlStock
            // 
            this.menuControlStock.Name = "menuControlStock";
            this.menuControlStock.Size = new System.Drawing.Size(166, 22);
            this.menuControlStock.Text = "ควบคุมปริมาณสินค้า";
            this.menuControlStock.Click += new System.EventHandler(this.menuControlStock_Click);
            // 
            // ProdutStockToolStripMenuItem
            // 
            this.ProdutStockToolStripMenuItem.Name = "ProdutStockToolStripMenuItem";
            this.ProdutStockToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ProdutStockToolStripMenuItem.Text = "สินค้าคงคลัง";
            this.ProdutStockToolStripMenuItem.Click += new System.EventHandler(this.ProdutStockToolStripMenuItem_Click);
            // 
            // menuTransaction
            // 
            this.menuTransaction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSales,
            this.menuBillSearch,
            this.menuSupport,
            this.menuStockIn,
            this.MenuStockInReturn,
            this.menuReturnTester});
            this.menuTransaction.Name = "menuTransaction";
            this.menuTransaction.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.menuTransaction.Size = new System.Drawing.Size(70, 20);
            this.menuTransaction.Text = "รายการทั่วไป";
            // 
            // menuSales
            // 
            this.menuSales.Name = "menuSales";
            this.menuSales.Size = new System.Drawing.Size(191, 22);
            this.menuSales.Text = "ขายสินค้า";
            this.menuSales.Click += new System.EventHandler(this.menuSales_Click);
            // 
            // menuBillSearch
            // 
            this.menuBillSearch.Name = "menuBillSearch";
            this.menuBillSearch.Size = new System.Drawing.Size(191, 22);
            this.menuBillSearch.Text = "ค้นหาใบเสร็จรับเงิน";
            this.menuBillSearch.Click += new System.EventHandler(this.menuBillSearch_Click);
            // 
            // menuSupport
            // 
            this.menuSupport.Name = "menuSupport";
            this.menuSupport.Size = new System.Drawing.Size(191, 22);
            this.menuSupport.Text = "บันทึกขอสนับสนุนสินค้า";
            this.menuSupport.Click += new System.EventHandler(this.menuSupport_Click);
            // 
            // menuStockIn
            // 
            this.menuStockIn.Name = "menuStockIn";
            this.menuStockIn.Size = new System.Drawing.Size(191, 22);
            this.menuStockIn.Text = "ใบแสดงยกยอดรับสินค้า";
            this.menuStockIn.Click += new System.EventHandler(this.menuStockIn_Click);
            // 
            // MenuStockInReturn
            // 
            this.MenuStockInReturn.Name = "MenuStockInReturn";
            this.MenuStockInReturn.Size = new System.Drawing.Size(191, 22);
            this.MenuStockInReturn.Text = "ใบรับคืนสินค้า";
            this.MenuStockInReturn.Click += new System.EventHandler(this.MenuStockInReturn_Click);
            // 
            // menuReturnTester
            // 
            this.menuReturnTester.Name = "menuReturnTester";
            this.menuReturnTester.Size = new System.Drawing.Size(191, 22);
            this.menuReturnTester.Text = "ใบแจ้งส่งคืนสินค้าตัวอย่าง";
            this.menuReturnTester.Click += new System.EventHandler(this.menuReturnTester_Click);
            // 
            // menuReport
            // 
            this.menuReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReportStock,
            this.menuReportMoney,
            this.menuReportBillSaleSummary,
            this.menuReportProductSales,
            this.menuReportStockOutDoc,
            this.menuReportProductReturn});
            this.menuReport.Name = "menuReport";
            this.menuReport.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.menuReport.Size = new System.Drawing.Size(44, 20);
            this.menuReport.Text = "&รายงาน";
            // 
            // menuReportStock
            // 
            this.menuReportStock.Name = "menuReportStock";
            this.menuReportStock.Size = new System.Drawing.Size(222, 22);
            this.menuReportStock.Text = "รายงานสินค้าคงคลัง";
            this.menuReportStock.Click += new System.EventHandler(this.menuReportStock_Click);
            // 
            // menuReportMoney
            // 
            this.menuReportMoney.Name = "menuReportMoney";
            this.menuReportMoney.Size = new System.Drawing.Size(222, 22);
            this.menuReportMoney.Text = "บันทึกการนำส่งเงินค่ายาสมุนไพร";
            this.menuReportMoney.Click += new System.EventHandler(this.menuReportMoney_Click);
            // 
            // menuReportBillSaleSummary
            // 
            this.menuReportBillSaleSummary.Name = "menuReportBillSaleSummary";
            this.menuReportBillSaleSummary.Size = new System.Drawing.Size(222, 22);
            this.menuReportBillSaleSummary.Text = "รายงานสรุปยอดขายตามบิล";
            this.menuReportBillSaleSummary.Click += new System.EventHandler(this.menuReportBillSaleSummary_Click);
            // 
            // menuReportProductSales
            // 
            this.menuReportProductSales.Name = "menuReportProductSales";
            this.menuReportProductSales.Size = new System.Drawing.Size(222, 22);
            this.menuReportProductSales.Text = "รายงานสรุปรายการสินค้าที่ขาย";
            this.menuReportProductSales.Click += new System.EventHandler(this.menuReportProductSales_Click);
            // 
            // menuReportStockOutDoc
            // 
            this.menuReportStockOutDoc.Name = "menuReportStockOutDoc";
            this.menuReportStockOutDoc.Size = new System.Drawing.Size(222, 22);
            this.menuReportStockOutDoc.Text = "รายงานการออกเอกสารการจ่าย";
            this.menuReportStockOutDoc.Click += new System.EventHandler(this.menuReportStockOutDoc_Click);
            // 
            // menuReportProductReturn
            // 
            this.menuReportProductReturn.Name = "menuReportProductReturn";
            this.menuReportProductReturn.Size = new System.Drawing.Size(222, 22);
            this.menuReportProductReturn.Text = "รายงานสรุปรายการสินค้าที่รับคืน";
            this.menuReportProductReturn.Click += new System.EventHandler(this.menuReportProductReturn_Click);
            // 
            // menuWindow
            // 
            this.menuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCloseAll});
            this.menuWindow.Name = "menuWindow";
            this.menuWindow.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.menuWindow.Size = new System.Drawing.Size(47, 20);
            this.menuWindow.Text = "หน้าต่าง";
            // 
            // menuCloseAll
            // 
            this.menuCloseAll.Name = "menuCloseAll";
            this.menuCloseAll.Size = new System.Drawing.Size(158, 22);
            this.menuCloseAll.Text = "ปิดหน้าต่างทั้งหมด";
            this.menuCloseAll.Click += new System.EventHandler(this.menuCloseAll_Click);
            // 
            // ABBStatus
            // 
            this.ABBStatus.Location = new System.Drawing.Point(0, 544);
            this.ABBStatus.Name = "ABBStatus";
            this.ABBStatus.Size = new System.Drawing.Size(792, 22);
            this.ABBStatus.TabIndex = 1;
            // 
            // ABBToolbar
            // 
            this.ABBToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ABBToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSales,
            this.btnReportMoney,
            this.toolStripSeparator2,
            this.btnStockIn,
            this.btnSupport,
            this.toolStripSeparator3,
            this.btnReportStockOutDoc,
            this.btnReportStock,
            this.toolStripSeparator4,
            this.btnLogOff});
            this.ABBToolbar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ABBToolbar.Location = new System.Drawing.Point(0, 24);
            this.ABBToolbar.Name = "ABBToolbar";
            this.ABBToolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ABBToolbar.Size = new System.Drawing.Size(792, 25);
            this.ABBToolbar.TabIndex = 3;
            // 
            // btnSales
            // 
            this.btnSales.Image = global::ABBClient.Properties.Resources.Sales;
            this.btnSales.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSales.Name = "btnSales";
            this.btnSales.Size = new System.Drawing.Size(45, 22);
            this.btnSales.Text = "ขาย";
            this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
            // 
            // btnReportMoney
            // 
            this.btnReportMoney.Image = global::ABBClient.Properties.Resources.Send;
            this.btnReportMoney.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReportMoney.Name = "btnReportMoney";
            this.btnReportMoney.Size = new System.Drawing.Size(55, 22);
            this.btnReportMoney.Text = "ส่งเงิน";
            this.btnReportMoney.Click += new System.EventHandler(this.btnReportMoney_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnStockIn
            // 
            this.btnStockIn.Image = global::ABBClient.Properties.Resources.Total;
            this.btnStockIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStockIn.Name = "btnStockIn";
            this.btnStockIn.Size = new System.Drawing.Size(59, 22);
            this.btnStockIn.Text = "ยอดรับ";
            this.btnStockIn.Click += new System.EventHandler(this.btnStockIn_Click);
            // 
            // btnSupport
            // 
            this.btnSupport.Image = global::ABBClient.Properties.Resources.Support;
            this.btnSupport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.Size = new System.Drawing.Size(69, 22);
            this.btnSupport.Text = "สนับสนุน";
            this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnReportStockOutDoc
            // 
            this.btnReportStockOutDoc.Image = global::ABBClient.Properties.Resources.Document;
            this.btnReportStockOutDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReportStockOutDoc.Name = "btnReportStockOutDoc";
            this.btnReportStockOutDoc.Size = new System.Drawing.Size(79, 22);
            this.btnReportStockOutDoc.Text = "เอกสารจ่าย";
            this.btnReportStockOutDoc.Click += new System.EventHandler(this.btnReportStockOutDoc_Click);
            // 
            // btnReportStock
            // 
            this.btnReportStock.Image = global::ABBClient.Properties.Resources.Stock;
            this.btnReportStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReportStock.Name = "btnReportStock";
            this.btnReportStock.Size = new System.Drawing.Size(84, 22);
            this.btnReportStock.Text = "สินค้าคงคลัง";
            this.btnReportStock.Click += new System.EventHandler(this.btnReportStock_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLogOff
            // 
            this.btnLogOff.Image = global::ABBClient.Properties.Resources.LogOff;
            this.btnLogOff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogOff.Name = "btnLogOff";
            this.btnLogOff.Size = new System.Drawing.Size(116, 22);
            this.btnLogOff.Text = "ลงชื่อออกจากระบบ";
            this.btnLogOff.Click += new System.EventHandler(this.btnLogOff_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.ABBToolbar);
            this.Controls.Add(this.ABBStatus);
            this.Controls.Add(this.ABBMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.ABBMenu;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABB";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.ABBMenu.ResumeLayout(false);
            this.ABBMenu.PerformLayout();
            this.ABBToolbar.ResumeLayout(false);
            this.ABBToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ABBMenu;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.StatusStrip ABBStatus;
        private System.Windows.Forms.ToolStripMenuItem menuLogin;
        private System.Windows.Forms.ToolStripMenuItem menuLogOff;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuMaster;
        private System.Windows.Forms.ToolStripMenuItem menuTransaction;
        private System.Windows.Forms.ToolStripMenuItem menuReport;
        private System.Windows.Forms.ToolStripMenuItem menuWindow;
        private System.Windows.Forms.ToolStripMenuItem menuCloseAll;
        private System.Windows.Forms.ToolStripMenuItem menuControlStock;
        private System.Windows.Forms.ToolStripMenuItem menuSales;
        private System.Windows.Forms.ToolStripMenuItem menuBillSearch;
        private System.Windows.Forms.ToolStripMenuItem menuSupport;
        private System.Windows.Forms.ToolStripMenuItem menuStockIn;
        private System.Windows.Forms.ToolStripMenuItem MenuStockInReturn;
        private System.Windows.Forms.ToolStripMenuItem menuReturnTester;
        private System.Windows.Forms.ToolStripMenuItem menuReportStock;
        private System.Windows.Forms.ToolStripMenuItem menuReportMoney;
        private System.Windows.Forms.ToolStripMenuItem menuReportBillSaleSummary;
        private System.Windows.Forms.ToolStripMenuItem menuReportProductSales;
        private System.Windows.Forms.ToolStripMenuItem menuReportStockOutDoc;
        private System.Windows.Forms.ToolStrip ABBToolbar;
        private System.Windows.Forms.ToolStripButton btnSales;
        private System.Windows.Forms.ToolStripButton btnReportMoney;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnStockIn;
        private System.Windows.Forms.ToolStripButton btnSupport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnReportStockOutDoc;
        private System.Windows.Forms.ToolStripButton btnReportStock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnLogOff;
        private System.Windows.Forms.ToolStripMenuItem ProdutStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuUploadData;
        private System.Windows.Forms.ToolStripMenuItem menuReportProductReturn;
    }
}