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

public partial class Transaction_Production_ProductionControl : System.Web.UI.UserControl
{

    #region Var for Ctl
    //### ประกาศตัวแปรใช้ใน ProductFill
    private string _packing = "";
    private string _package = "";
    private double _pdqty = 0;
    private string _expdate = "";
    private double _yield = 0;
    private double _lost = 0;

    //### ประกาศตัวแปรใช้ใน Raidate
    private string _radiatedate = "";
    private double _radiateqty = 0;
    private double _radiateunit = 0;
    private string _radiateremark = "";

    //### ประกาศตัวแปรใช้ใน RadiateReturn
    private string _radiateretdate = "";
    private double _radiateretqty = 0;
    private double _radiateretunit = 0;
    private string _radiateretremark = "";

    //### ประกาศตัวแปรใช้ใน StockInDetail
    private string _quarantinedate = "";
    private double _quarantineqty = 0;
    private double _quarantineunit = 0;
    private string _quarantineremark = "";

    //### ประกาศตัวแปรใช้ใน SendQC
    private string _sendqcdate = "";

    //### ประกาศตัวแปรใช้ใน StockOutDetail
    private string _sendfgdate = "";
    private double _sendfgqty = 0;
    private string _sendfgremark = "";
    //###################################
    #endregion

    #region Get In ProductFill
    //### Get ค่าใน ProductFill

    public string GetPacking
    {
        get { if (_packing == "") { _packing = ctlPack.GetPacking; } return _packing; }
    }

    public string GetPackage
    {
        get { if (_package == "") { _package = ctlPack.GetPackage; } return _package; }
    }

    public double GetPdQty
    {
        get { if (_pdqty == 0) { _pdqty = ctlPack.GetPdQty; } return _pdqty; }
    }

    public string GetExpDate
    {
        get { if (_expdate == "") { _expdate = ctlPack.GetExpDate; } return _expdate; }
    }
    public double GetYield
    {
        get { if (_yield == 0) { _yield = ctlPack.GetYield; } return _yield; }
    }
    public double GetLost
    {
        get { if (_lost == 0) { _lost = ctlPack.GetLost; } return _lost; }
    }
    #endregion

    #region Get In Radiate
    //### Get ค่าใน Radiate

    public string GetRadiateDate
    {
        get { if (_radiatedate == "") { _radiatedate = ctlXRaySending.GetRadiateDate; } return _radiatedate; }
    }

    public double GetRadiateQty
    {
        get { if (_radiateqty == 0) { _radiateqty = ctlXRaySending.GetRadiateQty; } return _radiateqty; }
    }

    public double GetRadiateUnit
    {
        get { if (_radiateunit == 0) { _radiateunit = ctlXRaySending.GetRadiateUnit; } return _radiateunit; }
    }

    public string GetRadiateRemark
    {
        get { if (_radiateremark == "") { _radiateremark = ctlXRaySending.GetRadiateRemark; } return _radiateremark; }
    }

    #endregion

    #region Get In StockinDetail
    //### Get ค่าใน StockinDetail

    public string GetQuarantineDate
    {
        get { if (_quarantinedate == "") { _quarantinedate = ctlImport.GetQuarantineDate; } return _quarantinedate; }
    }

    public double GetQuarantineQty
    {
        get { if (_quarantineqty == 0) { _quarantineqty = ctlImport.GetQuarantineQty; } return _quarantineqty; }
    }

    public double GetQuarantineUnit
    {
        get { if (_quarantineunit == 0) { _quarantineunit = ctlImport.GetQuarantineUnit; } return _quarantineunit; }
    }

    public string GetQuarantineRemark
    {
        get { if (_quarantineremark == "") { _quarantineremark = ctlImport.GetQuarantineRemark; } return _quarantineremark; }
    }
    #endregion

    #region Get In SendQC

    //### Get ค่าใน SendQC

    public string GetSendQCDate
    {
        get { if (_sendqcdate == "") { _sendqcdate = ctlQC.GetSendQCDate; } return _sendqcdate; }
    }
    #endregion

    #region Get In StockOutDetail
    //### Get ค่าใน StockOutDetail
    public string GetSendFGDate
    {
        get { if (_sendfgdate == "") { _sendfgdate = ctlExport.GetSendFGDate; } return _sendfgdate; }
    }

    public double GetSendFGQty
    {
        get { if (_sendfgqty == 0) { _sendfgqty = ctlExport.GetSendFGQty; } return _sendfgqty; }
    }

    public string GetSendFGRemark
    {
        get { if (_sendfgremark == "") { _sendfgremark = ctlExport.GetSendFGRemark; } return _sendfgremark; }
    }
    #endregion

    #region Get In RadiateReturn
    //### Get ค่าใน RadiateReturn
    public string GetRadiateRetDate
    {
        get { if (_radiateretdate == "") { _radiateretdate = ctlXrayReceiving.GetRadiateRetDate; } return _radiateretdate; }
    }

    public double GetRadiateRetQty
    {
        get { if (_radiateretqty == 0) { _radiateretqty = ctlXrayReceiving.GetRadiateRetQty; } return _radiateretqty; }
    }

    public double GetRadiateRetUnit
    {
        get { if (_radiateretunit == 0) { _radiateretunit = ctlXrayReceiving.GetRadiateRetUnit; } return _radiateretunit; }
    }

    public string GetRadiateRetRemark
    {
        get { if (_radiateretremark == "") { _radiateretremark = ctlXrayReceiving.GetRadiateRetRemark; } return _radiateretremark; }
    }
    #endregion

    public void SetPackageLotPrintScript(double PDPLOID)
    {
        this.ctlPackageLoss.SetPrintScript(PDPLOID);
    }

    public void SetMaterialLotPrintScript(double PDPLOID)
    {
        this.ctlMaterialLoss.SetPrintScript(PDPLOID);
    }

    private void SetControl()
    {
        this.ctlMaterialUsing.Visible = (this.ctlTab.SelectedTab == Constz.ProductionTab.RawMaterialUsing.Index);
        this.ctlPack.Visible = (this.ctlTab.SelectedTab == Constz.ProductionTab.Pack.Index);
        this.ctlXRaySending.Visible = (this.ctlTab.SelectedTab == Constz.ProductionTab.X_RaySending.Index);
        this.ctlXrayReceiving.Visible = (this.ctlTab.SelectedTab == Constz.ProductionTab.X_RayReceiving.Index);
        this.ctlImport.Visible = (this.ctlTab.SelectedTab == Constz.ProductionTab.Import.Index);
        this.ctlQC.Visible = (this.ctlTab.SelectedTab == Constz.ProductionTab.QC.Index);
        this.ctlMaterialLoss.Visible = (this.ctlTab.SelectedTab == Constz.ProductionTab.RawMaterialLoss.Index);
        this.ctlPackageLoss.Visible = (this.ctlTab.SelectedTab == Constz.ProductionTab.PackLoss.Index);
        this.ctlExport.Visible = (this.ctlTab.SelectedTab == Constz.ProductionTab.Export.Index);

        if (txtPdLoid.Text.Trim() != "0" && txtPdpLoid.Text.Trim() != "0")
            TabReLoad(Convert.ToDouble(txtPdLoid.Text.Trim()), Convert.ToDouble(txtPdpLoid.Text.Trim()));
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();
        }
    }

    protected void ctlTab_SelectedChange(object sender, EventArgs e)
    {
        SetControl();
    }

    public void TabReLoad(double PDLOID, double PDPLOID)
    {
        if (PDLOID != 0 && PDPLOID != 0)
        {
            txtPdpLoid.Text = PDPLOID.ToString();
            txtPdLoid.Text = PDLOID.ToString();
            ctlMaterialUsing.MaterialCtrlReLoadData(PDLOID, PDPLOID);
            ctlMaterialLoss.MaterialLostReLoadData(PDLOID, PDPLOID);
            ctlPackageLoss.PackageLostReLoadData(PDLOID, PDPLOID);
            ctlPack.ProductFillReLoad(PDPLOID);
            ctlXRaySending.RadiationReLoad(PDPLOID);
            ctlXrayReceiving.RadiationReturnReLoad(PDPLOID);
            ctlQC.SendQCReLoad(PDPLOID);
            ctlImport.StockInDetialReLoad(PDPLOID);
            ctlExport.StockOutDetailReLoad(PDPLOID);
        }
    }

}
