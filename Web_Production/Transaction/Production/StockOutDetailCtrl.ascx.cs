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
using ABB.DAL.Production;
using ABB.Data.Production;

public partial class Transaction_Production_StockOutDetailCtrl : System.Web.UI.UserControl
{
    private string _sendfgdate = "";
    private double _sendfgqty = 0;
    private string _sendfgremark = "";

    #region Get Property
    //# Property get ค่า
    public string GetSendFGDate
    {
        get { if (_sendfgdate == "") { _sendfgdate = PkSendFgDate.DateValue.ToString(); } return _sendfgdate; }
    }

    public double GetSendFGQty
    {
        get { if (_sendfgqty == 0) { _sendfgqty = Convert.ToDouble(txtSendFgQty.Text.Trim()); } return _sendfgqty; }
    }

    public string GetSendFGRemark
    {
        get { if (_sendfgremark == "") { _sendfgremark = txtSendFgRemark.Text.Trim(); } return _sendfgremark; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false) 
        {
            ControlUtil.SetDblTextBox(txtSendFgQty);
            ComboSource.BuildCombo(cmbUnitLost, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = '1'");
            ComboSource.BuildCombo(cmbUnitSendFg, "UNIT", "NAME", "LOID", "NAME", "ACTIVE ='1'");
            if (Request.QueryString["PDPLOID"] != null)
                LoadData(Request.QueryString["PDPLOID"].ToString());
        }
    }

    private void LoadData(string PdpLoid)
    {
        btnStockOut.Visible = false;
        DataTable dt = PDProductFlow.GetStockOutDetailData(PdpLoid);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txtLost.Text = dt.Rows[0]["LOST"].ToString();
            txtSendFgQty.Text = dt.Rows[0]["SENDFGQTY"].ToString();
            cmbUnitSendFg.SelectedValue = dt.Rows[0]["ULOID"].ToString();
            cmbUnitLost.SelectedValue = dt.Rows[0]["ULOID"].ToString();
            txtSendFgRemark.Text = dt.Rows[0]["SENDFGREMARK"].ToString();
            txtPdpLoid.Text = dt.Rows[0]["PDPLOID"].ToString();
            txtPoLoid.Text = dt.Rows[0]["POLOID"].ToString();
            txtPdLoid.Text = dt.Rows[0]["PDLOID"].ToString();
            txtULoid.Text = dt.Rows[0]["ULOID"].ToString();
            txtPRODSTATUS.Text = dt.Rows[0]["PRODSTATUS"].ToString();
            txtPOSTATUS.Text = dt.Rows[0]["POSTATUS"].ToString();
            txtQcResult.Text = dt.Rows[0]["QCRESULT"].ToString();
            if (dt.Rows[0]["SENDFGDATE"].ToString() != "")
                PkSendFgDate.DateValue = Convert.ToDateTime(dt.Rows[0]["SENDFGDATE"]);
        }

        if (txtPRODSTATUS.Text.Trim() == "AP" || txtPOSTATUS.Text.Trim() == "AP")
        {
            PkSendFgDate.Enabled = false;
            txtSendFgQty.ReadOnly = true;
            txtSendFgQty.CssClass = "zTextBoxR-View";
            txtSendFgRemark.CssClass = "zTextBox-View"; 
        }

        if (txtPRODSTATUS.Text.Trim() == "QB" && txtPOSTATUS.Text.Trim() == "QB")
        {
            btnStockOut.Visible = true;
        }
    }

    public void StockOutDetailReLoad(double PdpLoid)
    {
        LoadData(Convert.ToString(PdpLoid));
    }
    protected void btnStockOut_Click(object sender, EventArgs e)
    {
        double sqty = PDProductFlow.CheckSendFGQty(txtPdpLoid.Text.Trim());
        if (Convert.ToDouble(txtSendFgQty.Text.Trim()) > sqty)
        {
            Appz.ClientAlert(Page, "จำนวนที่ส่งออกต้องไม่เกินจำนวนที่รับเข้าคลังกักกัน");
            return;
        }
        else
        {
            bool ret = true;
            PDProductData pdpData = new PDProductData();
            PDOrderData poData = new PDOrderData();
            poData.STATUS = "AP";
            pdpData.PRODSTATUS = "AP";
            pdpData.SENDFGDATE = (PkSendFgDate.DateValue.Year.ToString() == "1" ? DateTime.Today : PkSendFgDate.DateValue);
            pdpData.SENDFGREMARK = txtSendFgRemark.Text.Trim();
            pdpData.SENDFGQTY = Convert.ToDouble((txtSendFgQty.Text.Trim() == "" ? "0" : txtSendFgQty.Text.Trim()));

            ret = PDProductFlow.Update_StatusRD(Authz.CurrentUserInfo.UserID.ToString(), pdpData, poData, txtPdpLoid.Text.Trim(), txtPoLoid.Text.Trim());
            if (ret == true)
            {
                Appz.ClientAlert(Page, "จ่ายออกจากคลังกักกันเรียบร้อย");
                LoadData(txtPdpLoid.Text.Trim());
                btnStockOut.Visible = false;
            }
        }
    }
}
