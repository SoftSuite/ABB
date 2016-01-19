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
using ABB.Data.Admin;
using ABB.Data.Purchase;
using ABB.Flow;
using ABB.Flow.Purchase;

public partial class Transaction_PDOrder : System.Web.UI.Page
{
    private PDOrderFlow _flow;

    public PDOrderFlow FlowObj
    {
        get { if (_flow == null) _flow = new PDOrderFlow(); return _flow; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }
    private void ResetState(double LOID)
    {
        if (LOID != 0)
        {
            PurchaseOrderData data = FlowObj.GetData(LOID);
            SetData(data);
        }
        else
        {
            //this.rbtIsType.SelectedIndex = 0;
            //this.chkActive.Checked = true;
        }
    }
    private void SetData(PurchaseOrderData data)
    {
        if (data.LOID == 0)
        {
            //this.rbtIsType.SelectedIndex = 0;
        }
        this.txtLOID.Text = data.LOID.ToString();
        this.lblCode.Text = data.CODE;
        this.DatePickerControl1.DateValue = data.SENDPODATE;
        this.cmbSendPO.SelectedValue = data.SENDPO;
        this.txtSendPO.Text = data.SENDOTHER;
        this.txtRefSuppcode.Text = data.REFSUPPCODE;
    }
    private PurchaseOrderData GetData()
    {
        PurchaseOrderData data = new PurchaseOrderData();
        data.CODE = this.lblCode.Text.Trim();
        data.LOID = this.txtLOID.Text == "" ? 0 : Convert.ToDouble(this.txtLOID.Text);
        data.SENDPODATE = this.DatePickerControl1.DateValue;
        data.SENDPO = this.cmbSendPO.SelectedItem.Value;
        data.SENDOTHER = this.txtSendPO.Text.Trim();
        data.REFSUPPCODE = this.txtRefSuppcode.Text.Trim();
        return data;
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void cmbSendPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbSendPO.SelectedValue == "OT")
        {
            txtSendPO.Enabled = true;
        }
        else
        {
            txtSendPO.Enabled = false;
        }
    }
}
