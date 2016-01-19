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
using ABB.Data.Purchase;
using ABB.Flow;
using ABB.Flow.Purchase;
using ABB.Global;

public partial class ToDoList_Controls_ProductReceiveList : System.Web.UI.UserControl
{
    private PurchaseToDoListFlow _flow;
    public PurchaseToDoListFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PurchaseToDoListFlow(); } return _flow; }
    }

    private ProductReceiveListSearchData GetSearchData()
    {
        ProductReceiveListSearchData data = new ProductReceiveListSearchData();
        data.CODE = this.txtCode.Text.Trim();
        data.DATEFROM = this.dtpDateFrom.DateValue;
        data.DATETO = this.dtpDateTo.DateValue;
        data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedItem.Value);
        data.SUPPLIER = Convert.ToDouble(this.cmbSupplier.SelectedItem.Value);
        return data;
    }

    private void SearchData()
    {
        this.grvProductReceive.DataSource = FlowObj.GetProductReceiveList(GetSearchData());
        this.grvProductReceive.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
            ComboSource.BuildCombo(this.cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvProductReceive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}
