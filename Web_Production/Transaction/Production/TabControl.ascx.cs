using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ABB.Data;

public partial class Transaction_Production_TabControl : System.Web.UI.UserControl
{
    private string normalL = Constz.ImageFolder + "tBNL.PNG";
    private string normalR = Constz.ImageFolder + "tBNR.PNG";
    private string selectL = Constz.ImageFolder + "tSL.PNG";
    private string selectR = Constz.ImageFolder + "tSR.PNG";

    public delegate void TabChangeEvent(object sender, EventArgs e);
    public event TabChangeEvent SelectedChange;

    public string SelectedTab
    {
        get { return (this.lblTab.Text == "" ? "0" : this.lblTab.Text); }
    }

    private void SetImage()
    {
        this.pnlFirst.Visible = (this.lblRow.Text == "2");
        this.pnlSecond.Visible = (this.lblRow.Text != "2");

        this.btnRawMaterialUsing.Visible = (SelectedTab != Constz.ProductionTab.RawMaterialUsing.Index);
        this.lblRawMaterialUsing.Visible = (SelectedTab == Constz.ProductionTab.RawMaterialUsing.Index); ;

        this.btnPack.Visible = (SelectedTab != Constz.ProductionTab.Pack.Index);
        this.lblPack.Visible = (SelectedTab == Constz.ProductionTab.Pack.Index);

        this.btnX_RaySending.Visible = (SelectedTab != Constz.ProductionTab.X_RaySending.Index);
        this.lblX_RaySending.Visible = (SelectedTab == Constz.ProductionTab.X_RaySending.Index);

        this.btnX_RayReceiving.Visible = (SelectedTab != Constz.ProductionTab.X_RayReceiving.Index);
        this.lblX_RayReceiving.Visible = (SelectedTab == Constz.ProductionTab.X_RayReceiving.Index);

        this.btnQC.Visible = (SelectedTab != Constz.ProductionTab.QC.Index);
        this.lblQC.Visible = (SelectedTab == Constz.ProductionTab.QC.Index);

        this.btnImport.Visible = (SelectedTab != Constz.ProductionTab.Import.Index);
        this.lblImport.Visible = (SelectedTab == Constz.ProductionTab.Import.Index); ;

        this.btnExport.Visible = (SelectedTab != Constz.ProductionTab.Export.Index);
        this.lblExport.Visible = (SelectedTab == Constz.ProductionTab.Export.Index);

        this.btnRawMaterialLoss.Visible = (SelectedTab != Constz.ProductionTab.RawMaterialLoss.Index);
        this.lblRawMaterialLoss.Visible = (SelectedTab == Constz.ProductionTab.RawMaterialLoss.Index);

        this.btnPackLoss.Visible = (SelectedTab != Constz.ProductionTab.PackLoss.Index);
        this.lblPackLoss.Visible = (SelectedTab == Constz.ProductionTab.PackLoss.Index);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lblTab.Text = Constz.ProductionTab.RawMaterialUsing.Index;
            this.lblRow.Text = Constz.ProductionTab.RawMaterialUsing.Row;

            this.lblRawMaterialUsing.Text = Constz.ProductionTab.RawMaterialUsing.Name;
            this.btnRawMaterialUsing.Text = this.lblRawMaterialUsing.Text;
            this.btnRawMaterialUsingV.Text = this.lblRawMaterialUsing.Text;

            this.lblPack.Text = Constz.ProductionTab.Pack.Name;
            this.btnPack.Text = this.lblPack.Text;
            this.btnPackV.Text = this.lblPack.Text;

            this.lblX_RaySending.Text = Constz.ProductionTab.X_RaySending.Name;
            this.btnX_RaySending.Text = this.lblX_RaySending.Text;
            this.btnX_RaySendingV.Text = this.lblX_RaySending.Text;

            this.lblX_RayReceiving.Text = Constz.ProductionTab.X_RayReceiving.Name;
            this.btnX_RayReceiving.Text = this.lblX_RayReceiving.Text;
            this.btnX_RayReceivingV.Text = this.lblX_RayReceiving.Text;

            this.lblQC.Text = Constz.ProductionTab.QC.Name;
            this.btnQC.Text = this.lblQC.Text;
            this.btnQCV.Text = this.lblQC.Text;

            this.lblImport.Text = Constz.ProductionTab.Import.Name;
            this.btnImport.Text = this.lblImport.Text;
            this.btnImportV.Text = this.lblImport.Text;

            this.lblExport.Text = Constz.ProductionTab.Export.Name;
            this.btnExport.Text = this.lblExport.Text;
            this.btnExportV.Text = this.lblExport.Text;

            this.lblRawMaterialLoss.Text = Constz.ProductionTab.RawMaterialLoss.Name;
            this.btnRawMaterialLoss.Text = this.lblRawMaterialLoss.Text;
            this.btnRawMaterialLossV.Text = this.lblRawMaterialLoss.Text;

            this.lblPackLoss.Text = Constz.ProductionTab.PackLoss.Name;
            this.btnPackLoss.Text = this.lblPackLoss.Text;
            this.btnPackLossV.Text = this.lblPackLoss.Text;

            SetImage();
        }
    }

    protected void btnRawMaterialUsing_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.RawMaterialUsing.Index;
        this.lblRow.Text = Constz.ProductionTab.RawMaterialUsing.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
    protected void btnRawMaterialUsingV_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.RawMaterialUsing.Index;
        this.lblRow.Text = Constz.ProductionTab.RawMaterialUsing.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnPack_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.Pack.Index;
        this.lblRow.Text = Constz.ProductionTab.Pack.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
    protected void btnPackV_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.Pack.Index;
        this.lblRow.Text = Constz.ProductionTab.Pack.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnX_RaySending_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.X_RaySending.Index;
        this.lblRow.Text = Constz.ProductionTab.X_RaySending.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
    protected void btnX_RaySendingV_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.X_RaySending.Index;
        this.lblRow.Text = Constz.ProductionTab.X_RaySending.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnX_RayReceiving_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.X_RayReceiving.Index;
        this.lblRow.Text = Constz.ProductionTab.X_RayReceiving.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
    protected void btnX_RayReceivingV_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.X_RayReceiving.Index;
        this.lblRow.Text = Constz.ProductionTab.X_RayReceiving.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.Import.Index;
        this.lblRow.Text = Constz.ProductionTab.Import.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
    protected void btnImportV_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.Import.Index;
        this.lblRow.Text = Constz.ProductionTab.Import.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnQC_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.QC.Index;
        this.lblRow.Text = Constz.ProductionTab.QC.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
    protected void btnQCV_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.QC.Index;
        this.lblRow.Text = Constz.ProductionTab.QC.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnRawMaterialLoss_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.RawMaterialLoss.Index;
        this.lblRow.Text = Constz.ProductionTab.RawMaterialLoss.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
    protected void btnRawMaterialLossV_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.RawMaterialLoss.Index;
        this.lblRow.Text = Constz.ProductionTab.RawMaterialLoss.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnPackLoss_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.PackLoss.Index;
        this.lblRow.Text = Constz.ProductionTab.PackLoss.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
    protected void btnPackLossV_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.PackLoss.Index;
        this.lblRow.Text = Constz.ProductionTab.PackLoss.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.Export.Index;
        this.lblRow.Text = Constz.ProductionTab.Export.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
    protected void btnExportV_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ProductionTab.Export.Index;
        this.lblRow.Text = Constz.ProductionTab.Export.Row;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

}
