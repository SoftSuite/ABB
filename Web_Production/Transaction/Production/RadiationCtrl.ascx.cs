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
using ABB.Data;
using ABB.Data.Production;

public partial class Transaction_Production_RadiationCtrl : System.Web.UI.UserControl
{
    private string _radiatedate = "";
    private double _radiateqty = 0;
    private double _radiateunit = 0;
    private string _radiateremark = "";

    #region Get Property
    //# Property get ค่า
    public string GetRadiateDate
    {
        get { if (_radiatedate == "") { _radiatedate = PkRadiateDate.DateValue.ToString(); } return _radiatedate; }
    }

    public double GetRadiateQty
    {
        get { if (_radiateqty == 0) { _radiateqty = Convert.ToDouble(txtRadiateQty.Text.Trim()); } return _radiateqty; }
    }

    public double GetRadiateUnit
    {
        get { if (_radiateunit == 0) { _radiateunit = Convert.ToDouble(cmbRadiateUnit.SelectedValue); } return _radiateunit; }
    }

    public string GetRadiateRemark
    {
        get { if (_radiateremark == "") { _radiateremark = txtRadiateRemark.Text.Trim(); } return _radiateremark; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {
            ControlUtil.SetDblTextBox(txtRadiateQty);
            ComboSource.BuildCombo(cmbRadiateUnit, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = '1'");
            if (Request.QueryString["PDPLOID"] != null)
                LoadData(Request.QueryString["PDPLOID"].ToString());
        }
    }

    private void LoadData(string PdpLoid)
    {
        btnSend.Visible = false;
        DataTable dt = PDProductFlow.GetRadiationData(PdpLoid);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            cmbRadiateUnit.SelectedValue = dt.Rows[0]["RADIATEUNIT"].ToString();
            txtRadiateQty.Text = dt.Rows[0]["RADIATEQTY"].ToString();
            txtRadiateRemark.Text = dt.Rows[0]["RADIATEREMARK"].ToString();
            txtPdpLoid.Text = dt.Rows[0]["PDPLOID"].ToString();
            txtPoLoid.Text = dt.Rows[0]["POLOID"].ToString();
            txtPdLoid.Text = dt.Rows[0]["PDLOID"].ToString();
            txtULoid.Text = dt.Rows[0]["ULOID"].ToString();
            txtPRODSTATUS.Text = dt.Rows[0]["PRODSTATUS"].ToString();
            txtPOSTATUS.Text = dt.Rows[0]["POSTATUS"].ToString();
            if (dt.Rows[0]["RADIATEDATE"].ToString() != "")
                PkRadiateDate.DateValue = Convert.ToDateTime(dt.Rows[0]["RADIATEDATE"]);
        }

        if (txtPRODSTATUS.Text.Trim() == "AP" || txtPOSTATUS.Text.Trim() == "AP" || txtPRODSTATUS.Text.Trim() == "RR" || txtPOSTATUS.Text.Trim() == "RR" || txtPRODSTATUS.Text.Trim() == "RD" || txtPOSTATUS.Text.Trim() == "RD")
        {
            PkRadiateDate.Enabled = false;
            txtRadiateQty.CssClass = "zTextboxR-View";
            txtRadiateQty.ReadOnly = true;
            cmbRadiateUnit.Enabled = false;
            txtRadiateRemark.CssClass = "zTextbox-View";

        }
        else if (txtPRODSTATUS.Text.Trim() == "RW" || txtPOSTATUS.Text.Trim() == "RW")
            btnSend.Visible = true;
    }

    public void RadiationReLoad(double PdpLoid)
    {
        LoadData(Convert.ToString(PdpLoid));
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        double pdqty = PDProductFlow.CheckRadiateQty(txtPdpLoid.Text.Trim());
        
        if (PkRadiateDate.DateValue.Year.ToString() == "1")
        {
            Appz.ClientAlert(Page, "กรุณาระบุวันที่ส่งฉายรังสี");
            return;
        }

        else if (txtRadiateQty.Text == "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุจำนวนที่ส่ง");
            return;
        }

        else if (Convert.ToDouble(txtRadiateQty.Text.Trim()) > pdqty)
        {
            Appz.ClientAlert(Page, "จำนวนที่ส่งต้องไม่เกินจำนวนที่บรรจุได้จริง");
            return;
        }

        else if (Convert.IsDBNull(cmbRadiateUnit.SelectedValue) == true || cmbRadiateUnit.SelectedValue == "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุหน่วยที่ส่งฉายรังสี");
            return;
        }
        else
        {
            bool ret = true;
            PDOrderData PoData = new PDOrderData();
            PDProductData pData = new PDProductData();
            PoData.STATUS = "RD";
            pData.PRODSTATUS = "RD";
            pData.RADIATEDATE = PkRadiateDate.DateValue;
            pData.RADIATEQTY = (txtRadiateQty.Text.Trim() == "" ? 0 : Convert.ToDouble(txtRadiateQty.Text.Trim()));
            pData.RADIATEREMARK = txtRadiateRemark.Text.Trim();
            pData.RADIATEUNIT = Convert.ToDouble(cmbRadiateUnit.SelectedValue);

            ret = PDProductFlow.Update_StatusRD(Authz.CurrentUserInfo.UserID.ToString(), pData, PoData, txtPdpLoid.Text.Trim(), txtPoLoid.Text.Trim());
            if (ret == true)
            {
                Appz.ClientAlert(Page, "ส่งฉายรังสีเรียบร้อย");
                btnSend.Visible = false;
                LoadData(txtPdpLoid.Text.Trim());
            }
        }
    }
}
