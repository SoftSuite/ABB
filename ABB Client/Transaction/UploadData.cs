using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ABB.Flow.Sales;

namespace ABBClient.Transaction
{
    public partial class UploadData : Form
    {
        Thread th;

        public UploadData()
        {
            InitializeComponent();
        }

        private bool Upload()
        {
            bool ret = true;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc = System.Diagnostics.Process.Start(Application.StartupPath + "\\backup.bat");
            proc.WaitForExit();
            UploadDataFlow _flow = new UploadDataFlow();
            ret = _flow.UploadData(this.dtpDateFrom.Value);
            if (!ret) Appz.OpenInformationDialog(_flow.ErrorMessage);
            return ret;
        }

        private void UploadShopData()
        {
            if (Upload())
                Appz.OpenInformationDialog("Upload data เรียบร้อยล้ว");
            else
                this.btnUpload.Enabled = true;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            this.dtpDateFrom.Enabled = false;
            this.btnUpload.Enabled = false;

            th = new Thread(new ThreadStart(this.UploadShopData));
            th.IsBackground = true;
            th.Start();
        }

        private void UploadData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (th != null)
            {
                if (th.IsAlive)
                {
                    th.Abort();
                }
            }
        }

        private void UploadData_Load(object sender, EventArgs e)
        {
            this.btnUpload.Enabled = true;
            this.dtpDateFrom.Enabled = true;
        }
    }
}