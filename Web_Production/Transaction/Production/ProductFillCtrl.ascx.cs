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
using ABB.Flow.Production;
using ABB.Global;

public partial class Transaction_Production_ProductFillCtrl : System.Web.UI.UserControl
{
    private string _packing = "";
    private string _package = "";
    private double _pdqty = 0;
    private string _expdate = "";
    private double _yield = 0;
    private double _lost = 0;

    #region Get Property

    //# Property get ค่า
    public string GetPacking
    {
        get { if (_packing == "") { _packing = txtPacking.Text.Trim(); } return _packing; }
    }

    public string GetPackage
    {
        get { if (_package == "") { _package = txtPackAge.Text.Trim(); } return _package; }
    }

    public double GetPdQty
    {
        get { if (_pdqty == 0) { _pdqty = Convert.ToDouble(txtPdQty.Text.Trim()); } return _pdqty; }
    }

    public string  GetExpDate
    {
        get { if (_expdate == "") { _expdate = dpExpDate.DateValue.ToString(); } return _expdate; }
    }

    public double GetYield
    {
        get { if (_yield == 0) { _yield = Convert.ToDouble(txtYield.Text.Trim()); } return _yield; }
    }

    public double GetLost
    {
        get { if (_lost == 0) { _lost = Convert.ToDouble(txtLost.Text.Trim()); } return _lost; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ControlUtil.SetDblTextBox(txtPdQty);
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["PDPLOID"] != null)
                LoadData(Request.QueryString["PDPLOID"].ToString());
        }
    }

    private void LoadData(string PdpLoid)
    {
        DataTable dt = PDProductFlow.GetProductionFillData(PdpLoid);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txtPacking.Text = dt.Rows[0]["PACKING"].ToString();
            txtPackAge.Text = dt.Rows[0]["PACKAGE"].ToString();
            txtPackSize.Text = dt.Rows[0]["PACKSIZE"].ToString();
            lblUname.Text = dt.Rows[0]["UNAME"].ToString();
            txtStdQty.Text = dt.Rows[0]["STDQTY"].ToString();
            txtPdQty.Text = dt.Rows[0]["PDQTY"].ToString();
            txtLost.Text = dt.Rows[0]["LOST"].ToString();
            txtYield.Text = String.Format("{0:N2}", Convert.ToDouble(dt.Rows[0]["YIELD"]));
            dpMfgDate.DateValue  = Convert.ToDateTime(dt.Rows[0]["MFGDATE"].ToString());
            txtPdpLoid.Text = dt.Rows[0]["PDPLOID"].ToString();
            txtPoLoid.Text = dt.Rows[0]["POLOID"].ToString();
            txtPdLoid.Text = dt.Rows[0]["PDLOID"].ToString();
            txtULoid.Text = dt.Rows[0]["ULOID"].ToString();
            txtPRODSTATUS.Text = dt.Rows[0]["PRODSTATUS"].ToString();
            txtPOSTATUS.Text = dt.Rows[0]["POSTATUS"].ToString();
            txtPDUNAME.Text = dt.Rows[0]["PDUNAME"].ToString();

            if (dt.Rows[0]["EXPDATE"].ToString() != "")
                dpExpDate.DateValue = Convert.ToDateTime(dt.Rows[0]["EXPDATE"].ToString()); 
        }

        if (txtPOSTATUS.Text.Trim() == "AP" || txtPRODSTATUS.Text.Trim() == "AP")
        {
            txtPacking.CssClass = "zTextbox-View";
            txtPacking.ReadOnly = true;
            txtPackAge.CssClass = "zTextbox-View";
            txtPackAge.ReadOnly = true;
            dpExpDate.Enabled = false;
            txtPdQty.CssClass = "zTextboxR-View";
        }
    }

    public void ProductFillReLoad(double PdpLoid)
    {
        LoadData(Convert.ToString(PdpLoid));
    }

    protected void txtPdQty_TextChanged(object sender, EventArgs e)
    {
        double pdqty = 0;
        double stdqty = 0;
        double yield = 0;

        if (txtStdQty.Text.Trim() != "")
            stdqty =  Convert.ToDouble(txtStdQty.Text.Trim());

        if (txtPdQty.Text.Trim() != "")
            pdqty = Convert.ToDouble(txtPdQty.Text.Trim());

        yield = (pdqty * 100) / stdqty;

        txtYield.Text = yield.ToString();

    }


}
