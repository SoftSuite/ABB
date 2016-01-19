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
using ABB.Data.Sales;
using ABB.Flow.Common;
using ABB.Flow.Sales;
using ABB.Global;

public partial class Master_ProductGroup : System.Web.UI.Page
{
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            return new SessionPageStatePersister(this);
        }
    }
    private ProductGroupFlow _flow;
    private ProductGroupFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ProductGroupFlow(); } return _flow; }
    }

    private void ClearData()
    {
        this.txtLOID.Text = "";
        this.txtCode.Text = "";
        this.cmbProductType.SelectedIndex = 0;
        this.chkActive.Checked = true;
        this.txtName.Text = "";
    }

    private void ResetState(double LOID)
    {
        if (LOID != 0)
        {
            ProductGroupData data = FlowObj.GetData(LOID);
            SetData(data);
        }
        else
        {
            ClearData();
        }
    }

    private ProductGroupData GetData()
    {
        ProductGroupData data = new ProductGroupData();
        data.ACTIVE = (this.chkActive.Checked ? Constz.ActiveStatus.Active : Constz.ActiveStatus.InActive);
        data.CODE = this.txtCode.Text.Trim();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.NAME = this.txtName.Text.Trim();
        data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
        return data;
    }

    private void SetData(ProductGroupData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE.Trim();
        this.cmbProductType.SelectedIndex = this.cmbProductType.Items.IndexOf(this.cmbProductType.Items.FindByValue(data.PRODUCTTYPE.ToString()));
        this.chkActive.Checked = (data.ACTIVE == Constz.ActiveStatus.Active);
        this.txtName.Text = data.NAME.Trim();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/ProductGroupSearch.aspx");
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

}
