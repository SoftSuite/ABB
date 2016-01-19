using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Data;
using ABB.Flow.Sales;

namespace ABBClient
{
    public partial class Main : Form
    {
        private string ApplicationName = "POS v0.00.00 (beta)";
        private Master.ControlStock frmControlStock;
        private Transaction.StockInReturn frmStockInReturn;
        private Transaction.Sales frmSales;
        private Transaction.BillSearch frmBillSearch;
        private Transaction.ReturnTester frmReturnTester;
        private Transaction.Support frmSupport;
        private Transaction.StockInEdit frmStockInEdit;
        private Transaction.SupportEdit frmsupportEdit;
        private PreReport.RepSendMoney frmRepSendMoney;
        private PreReport.RepStockRemain frmRepStockRemain;
        private PreReport.RepStockoutDoctype frmRepStockoutDoctype;
        private PreReport.RepSaleSummaryBill frmRepSaleSummaryBill;
        private PreReport.RepProductSaleSummary frmRepProductSaleSummary;
        private PreReport.RepProductReturnSummary frmRepProductReturnSummary;
        private Transaction.StockIn frmStockIn;
        private Transaction.ProductStock frmProductStock;
        private Transaction.UploadData frmUploadData;
        private Login frmLogin;

        public Main()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            foreach (string argument in arguments)
            {
                if (argument.Split('=')[0].ToLower() == "/u")
                {
                    string guid = argument.Split('=')[1];
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.System);
                    ProcessStartInfo si = new ProcessStartInfo(path + "\\msiexec.exe", "/i" + guid);
                    Process.Start(si);
                    Close();
                    Application.Exit();
                }
            }

            InitializeComponent();
        }

        private void CloseAllWindows()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f != null)
                {
                    f.Close();
                    f.Dispose();
                }
            }
        }

        private void SetLogin()
        {
            this.Text = ApplicationName;
            CloseAllWindows();
            this.menuLogin.Visible = false;
            this.menuLogOff.Visible = true;
            this.menuMaster.Enabled = true;
            this.menuTransaction.Enabled = true;
            this.menuReport.Enabled = true;
            this.menuWindow.Visible = true;
            this.menuUploadData.Enabled = true;

            this.ABBToolbar.Visible = true;
            Appz.SetSysConfig();
        }

        private void SetLogOff()
        {
            this.Text = ApplicationName;
            CloseAllWindows();
            this.menuLogin.Visible = true;
            this.menuLogOff.Visible = false;
            this.menuMaster.Enabled = false;
            this.menuTransaction.Enabled = false;
            this.menuReport.Enabled = false;
            this.menuWindow.Visible = false;
            this.menuUploadData.Enabled = false;

            this.ABBToolbar.Visible = false;
        }

        private void DoLogin()
        {
            if (frmLogin == null || frmLogin.IsDisposed) { frmLogin = new Login(); }
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                SetLogin();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = ApplicationName;
            SetLogOff();
            DoLogin();
        }

        #region Menu

        #region File

        private void menuLogin_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void menuLogOff_Click(object sender, EventArgs e)
        {
            SetLogOff();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Master

        private void menuControlStock_Click(object sender, EventArgs e)
        {
            if (frmControlStock == null || frmControlStock.IsDisposed) { frmControlStock = new ABBClient.Master.ControlStock(); }
            frmControlStock.MdiParent = this;
            frmControlStock.Show();
            frmControlStock.Focus();
        }

        #endregion

        #region Transaction

        private void menuSales_Click(object sender, EventArgs e)
        {
            if (frmSales == null || frmSales.IsDisposed) { frmSales = new Transaction.Sales(); }
            frmSales.MdiParent = this;
            frmSales.Show();
            frmSales.Focus();
        }

        private void menuBillSearch_Click(object sender, EventArgs e)
        {
            if (frmBillSearch == null || frmBillSearch.IsDisposed) { frmBillSearch = new ABBClient.Transaction.BillSearch(); }
            frmBillSearch.MdiParent = this;
            frmBillSearch.Show();
            frmBillSearch.Focus();
        }

        private void menuSupport_Click(object sender, EventArgs e)
        {
            if (frmSupport == null || frmSupport.IsDisposed) { frmSupport = new ABBClient.Transaction.Support(); }
            frmSupport.MdiParent = this;
            frmSupport.Show();
            frmSupport.Focus();
        }

        private void menuStockIn_Click(object sender, EventArgs e)
        {
            //if (frmPDStockInShopSearch == null || frmPDStockInShopSearch.IsDisposed)
            //{ frmPDStockInShopSearch = new ABBClient.Transaction.ProductStockInShopSearch(); }
            //frmPDStockInShopSearch.MdiParent = this;
            //frmPDStockInShopSearch.Show();
            //frmPDStockInShopSearch.Focus();
            if (frmStockIn == null || frmStockIn.IsDisposed) { frmStockIn = new ABBClient.Transaction.StockIn(); }
            frmStockIn.MdiParent = this;
            frmStockIn.Show();
            frmStockIn.Focus();
        }

        private void MenuStockInReturn_Click(object sender, EventArgs e)
        {
            if (frmStockInReturn == null || frmStockInReturn.IsDisposed)
            { frmStockInReturn = new ABBClient.Transaction.StockInReturn(); }
            frmStockInReturn.MdiParent = this;
            frmStockInReturn.Show();
            frmStockInReturn.Focus();
        }

        private void menuReturnTester_Click(object sender, EventArgs e)
        {
            if (frmReturnTester == null || frmReturnTester.IsDisposed) { frmReturnTester = new ABBClient.Transaction.ReturnTester(); }
            frmReturnTester.MdiParent = this;
            frmReturnTester.Show();
            frmReturnTester.Focus();
        }

        #endregion

        #region Report

        private void menuReportStock_Click(object sender, EventArgs e)
        {
            if (frmRepStockRemain == null || frmRepStockRemain.IsDisposed) { frmRepStockRemain = new PreReport.RepStockRemain(); }
            frmRepStockRemain.MdiParent = this;
            frmRepStockRemain.Show();
            frmRepStockRemain.Focus();
        }

        private void menuReportMoney_Click(object sender, EventArgs e)
        {
            if (frmRepSendMoney == null || frmRepSendMoney.IsDisposed) { frmRepSendMoney = new PreReport.RepSendMoney(); }
            frmRepSendMoney.MdiParent = this;
            frmRepSendMoney.Show();
            frmRepSendMoney.Focus();
        }

        private void menuReportBillSaleSummary_Click(object sender, EventArgs e)
        {
            if (frmRepSaleSummaryBill == null || frmRepSaleSummaryBill.IsDisposed) { frmRepSaleSummaryBill = new PreReport.RepSaleSummaryBill(); }
            frmRepSaleSummaryBill.MdiParent = this;
            frmRepSaleSummaryBill.Show();
            frmRepSaleSummaryBill.Focus();
        }

        private void menuReportProductSales_Click(object sender, EventArgs e)
        {
            if (frmRepProductSaleSummary == null || frmRepProductSaleSummary.IsDisposed) { frmRepProductSaleSummary = new PreReport.RepProductSaleSummary(); }
            frmRepProductSaleSummary.MdiParent = this;
            frmRepProductSaleSummary.Show();
            frmRepProductSaleSummary.Focus();
        }

        private void menuReportProductReturn_Click(object sender, EventArgs e)
        {
            if (frmRepProductReturnSummary == null || frmRepProductReturnSummary.IsDisposed) { frmRepProductReturnSummary = new PreReport.RepProductReturnSummary(); }
            frmRepProductReturnSummary.MdiParent = this;
            frmRepProductReturnSummary.Show();
            frmRepProductReturnSummary.Focus();
        }

        private void menuReportStockOutDoc_Click(object sender, EventArgs e)
        {
            if (frmRepStockoutDoctype == null || frmRepStockoutDoctype.IsDisposed) { frmRepStockoutDoctype = new PreReport.RepStockoutDoctype(); }
            frmRepStockoutDoctype.MdiParent = this;
            frmRepStockoutDoctype.Show();
            frmRepStockoutDoctype.Focus();
        }

        private void MenuReportSupportSummary_Click(object sender, EventArgs e)
        {

        }

        private void menuReportReturnTester_Click(object sender, EventArgs e)
        {

        }

        private void menuReportStockCheck_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Window

        private void menuCloseAll_Click(object sender, EventArgs e)
        {
            CloseAllWindows();
        }

        #endregion

        #endregion

        #region Toolbar

        private void btnSales_Click(object sender, EventArgs e)
        {
            if (frmSales == null || frmSales.IsDisposed) { frmSales = new Transaction.Sales(); }
            frmSales.MdiParent = this;
            frmSales.Show();
            frmSales.Focus();
        }

        private void btnReportMoney_Click(object sender, EventArgs e)
        {
            if (frmRepSendMoney == null || frmRepSendMoney.IsDisposed) { frmRepSendMoney = new PreReport.RepSendMoney(); }
            frmRepSendMoney.MdiParent = this;
            frmRepSendMoney.Show();
            frmRepSendMoney.Focus();
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            StockInShopFlow FlowObj = new StockInShopFlow();
            if (!FlowObj.InsertData(Appz.CurrentUserData.UserID, Appz.CurrentUserData.Warehouse))
                Appz.OpenErrorDialog(FlowObj.ErrorMessage);
            else
            {
                frmStockInEdit = new ABBClient.Transaction.StockInEdit(FlowObj.LOID);
                frmStockInEdit.MdiParent = this;
                frmStockInEdit.Show();
                frmStockInEdit.Focus();
            }
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            SupportSearchFlow csFlow = new SupportSearchFlow();
            RequisitionData csData = new RequisitionData();
            string str_today = getDateToday();
            csData.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ04;
            csData.STATUS = Constz.Requisition.Status.Waiting.Code;
            csData.REQDATE = Convert.ToDateTime(str_today);
            csData.WAREHOUSE = Appz.CurrentUserData.Warehouse;
            csData.RESERVEDATE = Convert.ToDateTime(str_today);
            csData.ACTIVE = Constz.ActiveStatus.Active;
            csData.VAT = Convert.ToDouble(ABB.Flow.SysConfigFlow.GetValue(Constz.ConfigName.VAT));

            double loid;

            //insert Requisition
            loid = csFlow.InsertRequisition(Appz.CurrentUserData.UserID.ToString(), csData);

            frmsupportEdit = new Transaction.SupportEdit(loid);
            frmsupportEdit.MdiParent = this;
            frmsupportEdit.Show();
            frmsupportEdit.Focus();
        }

        private string getDateToday()
        {
            string str;
            str = "";
            str = Convert.ToString(DateTime.Now.Day) + '/';
            str += Convert.ToString(DateTime.Now.Month) + '/';
            str += Convert.ToString(DateTime.Now.Year + 543);
            return str;
        }

        private void btnReportStockOutDoc_Click(object sender, EventArgs e)
        {
            if (frmRepStockoutDoctype == null || frmRepStockoutDoctype.IsDisposed) { frmRepStockoutDoctype = new PreReport.RepStockoutDoctype(); }
            frmRepStockoutDoctype.MdiParent = this;
            frmRepStockoutDoctype.Show();
            frmRepStockoutDoctype.Focus();
        }

        private void btnReportStock_Click(object sender, EventArgs e)
        {
            if (frmRepStockRemain == null || frmRepStockRemain.IsDisposed) { frmRepStockRemain = new PreReport.RepStockRemain(); }
            frmRepStockRemain.MdiParent = this;
            frmRepStockRemain.Show();
            frmRepStockRemain.Focus();
        }

        private void btnLogOff_Click(object sender, EventArgs e)
        {
            SetLogOff();
        }

        #endregion

        private void ProdutStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmProductStock == null || frmProductStock.IsDisposed) { frmProductStock = new Transaction.ProductStock(); }
            frmProductStock.MdiParent = this;
            frmProductStock.Show();
            frmProductStock.Focus();
        }

        private void menuUploadData_Click(object sender, EventArgs e)
        {
            if (frmUploadData == null || frmUploadData.IsDisposed) { frmUploadData = new Transaction.UploadData(); }
            //frmProductStock.MdiParent = this;
            frmUploadData.ShowDialog();
            frmUploadData.Focus();
        }


    }
}