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
using ABB.Flow.Production;
using ABB.Data.Production;
using ABB.DAL.Production;
using ABB.Flow.Production;

/// <summary>
/// Create by: Nang
/// Create Date: 20 Feb 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>
/// 


public partial class Transaction_Production_SendQCCtrl : System.Web.UI.UserControl
{
    private string _sendqcdate = "";

    #region Get Property
    //# Property get ค่า
    public string GetSendQCDate
    {
        get { if (_sendqcdate == "") { _sendqcdate = PkSendQcDate.DateValue.ToString(); } return _sendqcdate; }
    }
    #endregion
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            
            ComboSource.BuildCombo(cmbUnit1, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = '1'");
            ComboSource.BuildCombo(cmbUnit2, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = '1'");
            ComboSource.BuildCombo(cmbUnit3, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = '1'");
            if (Request.QueryString["PDPLOID"] != null)
                LoadData(Request.QueryString["PDPLOID"].ToString());
        }
    }

    private void LoadData(string PdpLoid)
    {
        btnSendQc.Visible = false;
        DataTable dt = PDProductFlow.GetSendQCData(PdpLoid);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txtQcQty1.Text = dt.Rows[0]["QCQTY1"].ToString();
            cmbUnit1.SelectedValue = dt.Rows[0]["UNIT1"].ToString();
            cmbUnit2.SelectedValue = dt.Rows[0]["UNIT1"].ToString();
            cmbUnit3.SelectedValue = dt.Rows[0]["UNIT1"].ToString();
            txtQcQty2.Text = dt.Rows[0]["QCQTY2"].ToString();
            txtQcQty3.Text = dt.Rows[0]["QCQTY3"].ToString();
            txtPdpLoid.Text = dt.Rows[0]["PDPLOID"].ToString();
            txtPoLoid.Text = dt.Rows[0]["POLOID"].ToString();
            txtRadiateRemark.Text = dt.Rows[0]["QCREMARK"].ToString();
            txtPRODSTATUS.Text = dt.Rows[0]["PRODSTATUS"].ToString();
            txtPOSTATUS.Text = dt.Rows[0]["POSTATUS"].ToString();

            if (dt.Rows[0]["QCRESULT"].ToString() == "Y")
                ResultY.Checked = true;
            else
                ResultN.Checked = true;
            
            if (dt.Rows[0]["SENDQCDATE"].ToString() != "")
                PkSendQcDate.DateValue = Convert.ToDateTime(dt.Rows[0]["SENDQCDATE"]);
        }

        if (txtPRODSTATUS.Text.Trim() == "AP" || txtPOSTATUS.Text.Trim() == "AP" || txtPRODSTATUS.Text.Trim() == "QC" || txtPOSTATUS.Text.Trim() == "QC")
        {
            PkSendQcDate.Enabled = false;
        }

        if (txtPRODSTATUS.Text.Trim() == "QS" || txtPOSTATUS.Text.Trim() == "QS")
            btnSendQc.Visible = true;
    }

    public void SendQCReLoad(double PdpLoid)
    {
        LoadData(Convert.ToString(PdpLoid));
    }

    protected void btnSendQc_Click(object sender, EventArgs e)
    {
        bool ret = true;
        PDOrderData PoData = new PDOrderData();
        PDProductData pData = new PDProductData();
        PoData.STATUS = "QC";
        pData.PRODSTATUS = "QC";
        if (PkSendQcDate.DateValue.Year.ToString() == "1")
        {
            Appz.ClientAlert(Page, "กรุณาระบุวันที่ส่งวิเคราะห์");
            return;
        }
        else
            pData.SENDQCDATE = PkSendQcDate.DateValue;
        ret = PDProductFlow.Update_StatusRD(Authz.CurrentUserInfo.UserID.ToString(), pData, PoData, txtPdpLoid.Text.Trim(), txtPoLoid.Text.Trim());
        if (ret == true)
        {
            Appz.ClientAlert(Page, "ส่ง QC เรียบร้อย");
            btnSendQc.Visible = false;
            LoadData(txtPdpLoid.Text.Trim());
        }
    }
}


