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

public partial class Master_ProductType : System.Web.UI.Page
{
    private ProductTypeFlow _flow;
    private ProductTypeFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ProductTypeFlow(); } return _flow; }
    }

    private void ClearData()
    {
        this.txtLOID.Text = "";
        this.txtCode.Text = "";
        this.chkActive.Checked = true;
        this.txtName.Text = "";
        this.rbtIsType.SelectedIndex = 0;
    }

    private void ResetState(double LOID)
    {
        if (LOID != 0)
        {
            ProductTypeSearchData data = FlowObj.GetData(LOID);
            SetData(data);
        }
        else
        {
            ClearData();
        }
    }

    private ProductTypeSearchData GetData()
    {
        ProductTypeSearchData data = new ProductTypeSearchData();
        data.ACTIVE = (this.chkActive.Checked ? Constz.ActiveStatus.Active : Constz.ActiveStatus.InActive);
        data.CODE = this.txtCode.Text.Trim();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.NAME = this.txtName.Text.Trim();
        data.TYPE = this.rbtIsType.SelectedItem.Value.Trim();
        return data;
    }

    private void SetData(ProductTypeSearchData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE.Trim();
        this.chkActive.Checked = (data.ACTIVE == Constz.ActiveStatus.Active);
        this.txtName.Text = data.NAME.Trim();
        this.rbtIsType.SelectedValue = data.TYPE;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.rbtIsType.Items.Clear();
            this.rbtIsType.Items.Add(new ListItem(Constz.ProductType.Type.FG.Name, Constz.ProductType.Type.FG.Code));
            this.rbtIsType.Items.Add(new ListItem(Constz.ProductType.Type.WH.Name, Constz.ProductType.Type.WH.Code));
            this.rbtIsType.Items.Add(new ListItem(Constz.ProductType.Type.Others.Name, Constz.ProductType.Type.Others.Code));
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/ProductTypeSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
            ResetState(FlowObj.LOID);
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

}
