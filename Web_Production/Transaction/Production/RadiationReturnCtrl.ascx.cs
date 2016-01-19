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
using ABB.Global;
using ABB.Data;
using ABB.Flow.Production;
using ABB.Data.Production;

public partial class Transaction_Production_RadiationReturnCtrl : System.Web.UI.UserControl
{
    private string _radiateretdate = "";
    private double _radiateretqty = 0;
    private double _radiateretunit = 0;
    private string _radiateretremark = "";

    #region Get Property
    //# Property get ค่า
    public string GetRadiateRetDate
    {
        get { if (_radiateretdate == "") { _radiateretdate = PkRadiateRetDate.DateValue.ToString(); } return _radiateretdate; }
    }

    public double GetRadiateRetQty
    {
        get { if (_radiateretqty == 0) { _radiateretqty = Convert.ToDouble(txtRadiateRetQty.Text.Trim()); } return _radiateretqty; }
    }

    public double GetRadiateRetUnit
    {
        get { if (_radiateretunit == 0) { _radiateretunit = Convert.ToDouble(cmbRadiateRetUnit.SelectedValue); } return _radiateretunit; }
    }

    public string GetRadiateRetRemark
    {
        get { if (_radiateretremark == "") { _radiateretremark = txtRadiateRetRemark.Text.Trim(); } return _radiateretremark; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            ControlUtil.SetDblTextBox(txtRadiateRetQty);
            ComboSource.BuildCombo(cmbRadiateRetUnit, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = '1'");
            if (Request.QueryString["PDPLOID"] != null)
                LoadData(Request.QueryString["PDPLOID"].ToString());
        }
    }

    private void LoadData(string PdpLoid)
    {
        DataTable dt = PDProductFlow.GetRadiationReturnData(PdpLoid);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            cmbRadiateRetUnit.SelectedValue = dt.Rows[0]["RADIATERETUNIT"].ToString();
            txtRadiateRetQty.Text = dt.Rows[0]["RADIATERETQTY"].ToString();
            txtRadiateRetRemark.Text = dt.Rows[0]["RADIATERETREMARK"].ToString();
            txtPdpLoid.Text = dt.Rows[0]["PDPLOID"].ToString();
            txtPoLoid.Text = dt.Rows[0]["POLOID"].ToString();
            txtPdLoid.Text = dt.Rows[0]["PDLOID"].ToString();
            txtULoid.Text = dt.Rows[0]["ULOID"].ToString();
            txtPRODSTATUS.Text = dt.Rows[0]["PRODSTATUS"].ToString();
            txtPOSTATUS.Text = dt.Rows[0]["POSTATUS"].ToString();
            if (dt.Rows[0]["RADIATERETDATE"].ToString() != "")
                PkRadiateRetDate.DateValue = Convert.ToDateTime(dt.Rows[0]["RADIATERETDATE"]);
        }

        if (txtPRODSTATUS.Text.Trim() == "AP" || txtPOSTATUS.Text.Trim() == "AP" || txtPRODSTATUS.Text.Trim() == "RR" || txtPOSTATUS.Text.Trim() == "RR")
        {
            PkRadiateRetDate.Enabled = false;
            txtRadiateRetQty.CssClass = "zTextboxR-View";
            txtRadiateRetQty.ReadOnly = true;
            cmbRadiateRetUnit.Enabled = false;
            txtRadiateRetRemark.CssClass = "zTextbox-View";
            btnSend.Visible = false;
        }
    }

    public void RadiationReturnReLoad(double PdpLoid)
    {
        LoadData(Convert.ToString(PdpLoid));
    }


    protected void btnSend_Click(object sender, EventArgs e)
    {
        double rqty = PDProductFlow.CheckRadiateReturnQty(txtPdpLoid.Text.Trim());

        if (PkRadiateRetDate.DateValue.Year.ToString() == "1")
        {
            Appz.ClientAlert(Page, "กรุณาระบุวันที่รับคืนจากการฉายรังสี");
            return;
        }

        else if (txtRadiateRetQty.Text.Trim() == "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุจำนวนที่รับคืนจากการฉายรังสี");
            return;
        }

        else if (cmbRadiateRetUnit.SelectedValue == "" || Convert.IsDBNull(cmbRadiateRetUnit.SelectedValue) == true)
        {
            Appz.ClientAlert(Page, "กรุณาระบุหน่วยที่รับคืนจากการฉายรังสี");
            return;
        }

        else if (Convert.ToDouble(txtRadiateRetQty.Text.Trim()) > rqty)
        {
            Appz.ClientAlert(Page, "จำนวนที่ส่งคืนต้องไม่เกินจำนวนที่ส่งฉายรังสี");
            return;
        }
        else
        {
            bool ret = true;
            PDOrderData PoData = new PDOrderData();
            PDProductData pData = new PDProductData();
            PoData.STATUS = "RR";
            pData.PRODSTATUS = "RR";
            pData.RADIATERETDATE = (PkRadiateRetDate.DateValue.Year.ToString() == "1" ? DateTime.Today : PkRadiateRetDate.DateValue);
            pData.RADIATERETQTY = (txtRadiateRetQty.Text.Trim() == "" ? 0 : Convert.ToDouble(txtRadiateRetQty.Text.Trim()));
            pData.RADIATERETREMARK = txtRadiateRetRemark.Text.Trim();
            pData.RADIATERETUNIT = Convert.ToDouble(cmbRadiateRetUnit.SelectedValue);

            ret = PDProductFlow.Update_StatusRR(Authz.CurrentUserInfo.UserID.ToString(), pData, PoData, txtPdpLoid.Text.Trim(), txtPoLoid.Text.Trim());
            if (ret == true)
            {
                Appz.ClientAlert(Page, "รับคืนจากการฉายรังสีเรียบร้อยแล้ว");
                btnSend.Visible = false;
                LoadData(txtPdpLoid.Text.Trim());
            }
        }
    }
}
