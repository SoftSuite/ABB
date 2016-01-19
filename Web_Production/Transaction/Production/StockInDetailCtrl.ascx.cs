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
using ABB.Data.Production;
using ABB.Flow.Production;

public partial class Transaction_Production_StockInDetailCtrl : System.Web.UI.UserControl
{
    private string _quarantinedate = "";
    private double _quarantineqty = 0;
    private double _quarantineunit = 0;
    private string _quarantineremark = "";

    #region Get Property
    //# Property get ค่า
    public string GetQuarantineDate
    {
        get { if (_quarantinedate == "") { _quarantinedate = PkQuarantineDate.DateValue.ToString(); } return _quarantinedate; }
    }

    public double GetQuarantineQty
    {
        get { if (_quarantineqty == 0) { _quarantineqty =Convert.ToDouble(txtQuarantineQty.Text.Trim()); } return _quarantineqty; }
    }

    public double GetQuarantineUnit
    {
        get { if (_quarantineunit == 0) { _quarantineunit = Convert.ToDouble(cmbQuarantineUnit.SelectedValue); } return _quarantineunit; }
    }

    public string GetQuarantineRemark
    {
        get { if (_quarantineremark == "") { _quarantineremark = txtQuarantineRemark.Text.Trim(); } return _quarantineremark; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            ControlUtil.SetDblTextBox(txtQuarantineQty);
            ComboSource.BuildCombo(this.cmbQuarantineUnit, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = '1'");
            if (Request.QueryString["PDPLOID"] != null)
                LoadData(Request.QueryString["PDPLOID"].ToString());
        }
    }

    private void LoadData(string PdpLoid)
    {
        btnStockIn.Visible = false;
        DataTable dt = PDProductFlow.GetStockInDetailData(PdpLoid);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txtQuarantineQty.Text = dt.Rows[0]["QUARANTINEQTY"].ToString();
            cmbQuarantineUnit.SelectedValue = dt.Rows[0]["QUARANTINEUNIT"].ToString();
            txtQuarantineRemark.Text = dt.Rows[0]["QUARANTINEREMARK"].ToString();
            txtPdpLoid.Text = dt.Rows[0]["PDPLOID"].ToString();
            txtPoLoid.Text = dt.Rows[0]["POLOID"].ToString();
            txtPdLoid.Text = dt.Rows[0]["PDLOID"].ToString();
            txtULoid.Text = dt.Rows[0]["ULOID"].ToString();
            txtPRODSTATUS.Text = dt.Rows[0]["PRODSTATUS"].ToString();
            txtPOSTATUS.Text = dt.Rows[0]["POSTATUS"].ToString();
            txtRadiateRetDate.Text = dt.Rows[0]["RADIATERETDATE"].ToString();
            if (dt.Rows[0]["QUARANTINEDATE"].ToString() != "")
                PkQuarantineDate.DateValue = Convert.ToDateTime(dt.Rows[0]["QUARANTINEDATE"]);
        }

        if (txtPRODSTATUS.Text.Trim() == "AP" || txtPOSTATUS.Text.Trim() == "AP" || txtPRODSTATUS.Text.Trim() == "QS" || txtPOSTATUS.Text.Trim() == "QS")
        {
            PkQuarantineDate.Enabled = false;
            txtQuarantineQty.CssClass = "zTextBoxR-View";
            txtQuarantineQty.ReadOnly = true;
            txtQuarantineRemark.CssClass = "zTextBox-View";
            txtQuarantineRemark.Enabled = false;
        }
        else if (txtPRODSTATUS.Text.Trim() == "RW" || txtPOSTATUS.Text.Trim() == "RW")
            btnStockIn.Visible = true;

        else if ((txtPRODSTATUS.Text.Trim() == "RR" || txtPOSTATUS.Text.Trim() == "RR") && txtRadiateRetDate.Text.Trim() != "")
            btnStockIn.Visible = true;
    }

    public void StockInDetialReLoad(double PdpLoid)
    {
        LoadData(Convert.ToString(PdpLoid));
    }

    protected void btnStockIn_Click(object sender, EventArgs e)
    {
        double qqty = PDProductFlow.CheckQuarantineQty(txtPdpLoid.Text.Trim());

        if (Convert.ToDouble(txtQuarantineQty.Text.Trim()) > qqty)
        {
            Appz.ClientAlert(Page, "จำนวนที่รับต้องไม่เกินจำนวนที่ผลิตได้จริง");
            return;
        }
        else
        {
            if (txtPRODSTATUS.Text.Trim() == "RW" || txtPOSTATUS.Text.Trim() == "RW")
                UpdateStatus();
            else if (txtRadiateRetDate.Text.Trim() == "")
            {
                Appz.ClientAlert(Page, "ไม่สามารถทำรายการได้ ต้องมีวันที่รับคืนจากการฉายรังสี");
                return;
            }
            else if (txtPRODSTATUS.Text.Trim() == "RR" || txtPOSTATUS.Text.Trim() == "RR")
                UpdateStatus();


            LoadData(txtPdpLoid.Text.Trim());
        }
    }

    private void UpdateStatus()
    {
        bool ret = true;
        PDProductData pdpData = new PDProductData();
        PDOrderData poData = new PDOrderData();
        poData.STATUS = "QS";
        pdpData.PRODSTATUS = "QS";
        pdpData.QUARANTINEDATE = (PkQuarantineDate.DateValue.Year.ToString() == "1" ? DateTime.Today : PkQuarantineDate.DateValue);
        pdpData.QUARANTINEQTY = (txtQuarantineQty.Text.Trim() == "" ? 0 : Convert.ToDouble(txtQuarantineQty.Text.Trim()));
        pdpData.QUARANTINEREMARK = txtQuarantineRemark.Text.Trim();

        ret = PDProductFlow.Update_StatusRD(Authz.CurrentUserInfo.UserID.ToString(), pdpData, poData, txtPdpLoid.Text.Trim(), txtPoLoid.Text.Trim());
        if (ret == true)
        {
            Appz.ClientAlert(Page, "ส่งเข้าคลังกักกันเรียบร้อย");
            LoadData(txtPdpLoid.Text.Trim());
            btnStockIn.Visible = false;
        }
    }
}
