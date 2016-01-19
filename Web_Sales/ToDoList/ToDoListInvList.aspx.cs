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
using ABB.Flow.Sales;
using ABB.Global;

public partial class ToDoList_ToDoListInvList : System.Web.UI.Page
{
    private ToDoListFlow _flow;

    private ToDoListFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ToDoListFlow(); } return _flow; }
    }

    private ToDoListData GetData()
    {
        ToDoListData data = new ToDoListData();
        data.CODE = this.txtCode.Text.Trim();
        data.CUSTOMER = Convert.ToDouble(this.cmbCustomer.SelectedItem.Value);
        data.RESERVEDATEFROM = this.ctlDateFrom.DateValue;
        data.RESERVEDATETO = this.ctlDateTo.DateValue;
        data.STATUS = this.cmbStatus.SelectedItem.Value;
        return data;
    }

    private void SearchData()
    {
        this.grvRequisition.DataSource = FlowObj.GetRequisitionList(GetData());
        this.grvRequisition.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmbStatus.Items.Clear();
            this.cmbStatus.Items.Add(new ListItem("ทั้งหมด", ""));
            this.cmbStatus.Items.Add(new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Code));

            ComboSource.BuildCombo(this.cmbCustomer, "V_CUSTOMER", "CUSTOMERNAME", "LOID", "CUSTOMERNAME", "", "ทั้งหมด", "0");
            SearchData();
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductInvoice.aspx");
    }

}
