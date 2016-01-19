namespace ABBClient.Reports
{
    partial class PreviewReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewReport));
            this.ctlReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ctlReportViewer
            // 
            this.ctlReportViewer.ActiveViewIndex = -1;
            this.ctlReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlReportViewer.DisplayGroupTree = false;
            this.ctlReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlReportViewer.Location = new System.Drawing.Point(0, 0);
            this.ctlReportViewer.Name = "ctlReportViewer";
            this.ctlReportViewer.ShowGroupTreeButton = false;
            this.ctlReportViewer.Size = new System.Drawing.Size(742, 466);
            this.ctlReportViewer.TabIndex = 0;
            // 
            // PreviewReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 466);
            this.Controls.Add(this.ctlReportViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreviewReport";
            this.Text = "รายงาน";
            this.Load += new System.EventHandler(this.PreviewReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ctlReportViewer;
    }
}