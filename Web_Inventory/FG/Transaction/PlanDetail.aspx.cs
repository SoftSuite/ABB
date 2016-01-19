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
using ABB.Flow.Inventory.FG;
using ABB.Data.Inventory.FG;
using ABB.Data;

public partial class FG_Transaction_PlanDetail : System.Web.UI.Page
{
    private PlanDetailPopupFlow _flow;

    private PlanDetailPopupFlow FlowObj
    {
        get { if (_flow == null) _flow = new PlanDetailPopupFlow(); return _flow; }
    }

    private void ResetState(double plan, int month, int day, double product)
    {
        SetData(FlowObj.GetPlanDetailData(plan, month, day, product));
    }

    private void SetData(PlanDetailData data)
    {
        DateTime pdDate = new DateTime(1, 1, 1);
        DateTime poDate = new DateTime(1, 1, 1);
        DateTime receiveDate = new DateTime(data.YEAR, data.MONTH, data.DAY);
        this.lblProduct.Text = data.PRODUCT.ToString();
        this.lblProductName.Text = data.PRODUCTNAME;
        this.lblUnitName.Text = data.UNITNAME;
        this.lblMin.Text = data.MINIMUM.ToString(Constz.IntFormat);
        this.lblMax.Text = data.MAXIMUM.ToString(Constz.IntFormat);
        this.lblDate.Text = receiveDate.ToString(Constz.DateFormat);
        this.lblStatus.Text = data.STATUS;
        this.lblPOLotSize.Text = data.POLOTSIZE.ToString(Constz.IntFormat);
        this.lblPOLeadTime.Text = data.POLEADTIME.ToString(Constz.IntFormat);
        this.txtPOQty.Text = data.POQTY.ToString(Constz.IntFormat);
        poDate = new DateTime(data.YEAR, data.MONTH, data.DAY).AddDays(-data.POLEADTIME);
        this.lblPODate.Text = (data.POLEADTIME == 0 ? "-" : poDate.ToString(Constz.DateFormat));
        this.lblPOLOID.Text = data.POLOID.ToString();
        this.lblPDLotSize.Text = data.PDLOTSIZE.ToString(Constz.IntFormat);
        this.lblPDLeadTime.Text = data.PDLEADTIME.ToString(Constz.IntFormat);
        this.txtPDQty.Text = data.PDQTY.ToString(Constz.IntFormat);
        pdDate = new DateTime(data.YEAR, data.MONTH, data.DAY).AddDays(-data.PDLEADTIME);
        this.lblPDDate.Text = (data.PDLEADTIME == 0 ? "-" : pdDate.ToString(Constz.DateFormat));
        this.lblPDLOID.Text = data.PDLOID.ToString();
        this.lblReceiveLOID.Text = data.RECEIVELOID.ToString();
        // วันที่จะรับเข้าต้องมากกว่าวันที่ปัจจุบัน 1 วัน
        // วันที่สั่งผลิตหรือวันที่สั่งซื้อต้องมากกว่าวันที่ปัจจุบัน
        // สถานะกำลังทำรายการ
        bool canEdit = true;
        canEdit = receiveDate >= DateTime.Today.AddDays(1) && data.STATUS == Constz.Requisition.Status.Waiting.Code && pdDate >= DateTime.Today && poDate >= DateTime.Today;
        this.ctlToolbar.BtnSaveShow = canEdit;

        SetMaterial();
        this.txtPDQty.CssClass = (this.ctlToolbar.BtnSaveShow ? "zTextboxR" : "zTextboxR-View");
        this.txtPDQty.ReadOnly = !this.ctlToolbar.BtnSaveShow;
        this.txtPOQty.CssClass = (this.ctlToolbar.BtnSaveShow ? "zTextboxR" : "zTextboxR-View");
        this.txtPOQty.ReadOnly = !this.ctlToolbar.BtnSaveShow;
    }

    private void SetMaterial()
    {
        this.grvMaterial.DataSource = FlowObj.getMaterialList(Convert.ToDouble(this.lblProduct.Text), Convert.ToDouble(this.txtPOQty.Text) + Convert.ToDouble(this.txtPDQty.Text));
        this.grvMaterial.DataBind();
        if (this.grvMaterial.Rows.Count == 0 || !this.ctlToolbar.BtnSaveShow)
            this.btnCalculate.Enabled = false;
    }

    private PlanPopupData GetData()
    {
        PlanPopupData data = new PlanPopupData();
        data.DAY = Convert.ToInt32(Request["day"] == null ? "0" : Request["day"]);
        data.PDLOID = Convert.ToDouble(this.lblPDLOID.Text);
        data.PDQTY = Convert.ToDouble(this.txtPDQty.Text);
        data.PLAN = Convert.ToDouble(Request["plan"] == null ? "0" : Request["plan"]);
        data.POLOID = Convert.ToDouble(this.lblPOLOID.Text);
        data.POQTY = Convert.ToDouble(this.txtPOQty.Text);
        data.RECEIVELOID = Convert.ToDouble(this.lblReceiveLOID.Text);
        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["plan"] == null ? "0" : Request["plan"]), Convert.ToInt32(Request["month"] == null ? "0" : Request["month"]), Convert.ToInt32(Request["day"] == null ? "0" : Request["day"]), Convert.ToDouble(Request["product"] == null ? "0" : Request["product"]));
            this.ctlToolbar.ClientClickBack = "window.close(); return false;";
            this.ctlToolbar.ClientClickCancel = this.ctlToolbar.ClientClickBack;

            Response.Expires = 0;
            //Response.Expiresabsolute = DateTime.Now.AddDays(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";

            ControlUtil.SetIntTextBox(this.txtPOQty);
            ControlUtil.SetIntTextBox(this.txtPDQty);
        }
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        SetMaterial();
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "winclose", "window.close();", true);
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void grvMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            if (Convert.ToDouble(drow["REMAIN"]) < Convert.ToDouble(Convert.IsDBNull(drow["MINIMUM"]) ? 0 : drow["MINIMUM"]))
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
        }
    }
}
