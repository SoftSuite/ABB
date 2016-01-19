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
using ABB.Flow.Inventory;
using ABB.Data.Inventory;
using ABB.Global;
using ABB.Data;

public partial class Transaction_StockCheck : System.Web.UI.Page
{
    private StockCheckFlow _flow;
    public StockCheckFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockCheckFlow(); return _flow; }
    }

    private void ClearControls()
    {
        this.cmbWarehouseName.SelectedIndex = 0;
        this.txtHour.Text = DateTime.Now.Hour.ToString();
        this.txtMinute.Text = DateTime.Now.Minute.ToString();
        this.txtSecond.Text = DateTime.Now.Second.ToString();
        this.txtCheckDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt16(this.txtHour.Text), Convert.ToInt16(this.txtMinute.Text), Convert.ToInt16(this.txtSecond.Text)).ToString();
        this.ctlCheckDate.DateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt16(this.txtHour.Text), Convert.ToInt16(this.txtMinute.Text), Convert.ToInt16(this.txtSecond.Text));
        //this.txtBatchNo.Text = FlowObj.GenNewBatchNo();

    }

    private StockCheckData GetData()
    {
        StockCheckData data = new StockCheckData();
        data.BATCHNO = this.txtBatchNo.Text;
        data.CHECKDATE = this.ctlCheckDate.DateValue;
        data.WAREHOUSE = Convert.ToDouble(this.cmbWarehouseName.SelectedItem.Value);
        return data;
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect("StockCheckSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (this.cmbWarehouseName.SelectedIndex == 0)
        {
            Appz.ClientAlert(this, "กรุณาเลือกคลัง");
            return;
        }
        if (FlowObj.InsertNewBatchNo(Authz.CurrentUserInfo.UserID, GetData()))
        {
            //ClearControls();
            StockCheckData data = FlowObj.GetData(FlowObj.LOID);
            this.txtBatchNo.Text = data.BATCHNO;
            this.txtCheckDate.Text = data.CHECKDATE.ToString(Constz.DateFormat);
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSaveShow = false;

            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");

            //Response.Redirect(Constz.HomeFolder + "Transaction/StockCheckSearch.aspx");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Page.User.Identity.Name != "")
        {
            ComboSource.BuildCombo(this.cmbWarehouseName, "WAREHOUSE", "NAME", "LOID", "NAME", "LOID IN (1,2)", "เลือก", "0");
            ClearControls();
        }
    }
}
