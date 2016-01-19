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
using ABB.Data.Admin;
using ABB.Data.Inventory.FG;
using ABB.Data.Production;
using ABB.Flow;
using ABB.Flow.Production;
using ABB.Global;


public partial class Transaction_QCAnalysis_PO : System.Web.UI.Page
{
    #region Variables & Properties

    private StockinQCFlow _flow;

    public StockinQCFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockinQCFlow(); return _flow; }
    }

    #endregion

    #region Others


    private void ResetState(double loid)
    {
        SetData(FlowObj.GetData(loid));
    }

    #endregion

    #region Data

    private void SetData(StockinQCData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtQCCode.Text = data.QCCODE;
        this.txtCreateby.Text = data.CREATEBY;
        this.txtInvNo.Text = data.INVNO;
        this.ctlQCDate.DateValue = data.QCDATE;
        this.txtAnaCode.Text = data.ANACODE;
        if (data.ANADATE.Year == 1) data.ANADATE = DateTime.Now.Date;
        this.ctlAnaDate.DateValue = data.ANADATE;
        SetSender(data.SENDER);
        this.txtDivision.Text = FlowObj.GetDivision(data.CREATEBY);
        this.grvItem.DataSource = FlowObj.GetStockInItem(data.LOID);
        this.grvItem.DataBind();
        data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;

        if (data.STATUS == Constz.Requisition.Status.Approved.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
        if (data.WAREHOUSE == 2)
        {
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductQC, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        }
        else
        {
            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.F_FG_16_R00, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        }
    }

    private void SetSender(double sender)
    {
        SupplierData data = FlowObj.GetSenderData(sender);
        this.txtSPCode.Text = data.CODE;
        this.txtSPName.Text = data.NAME;

    }

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันทำรายการใช่หรือไม่?');";

        }
    }

    private bool checkGrid()
    {
        bool bRet = true;
        foreach (GridViewRow row in grvItem.Rows)
        {
            RadioButton rbtPass = (RadioButton)row.Cells[0].FindControl("rbtPass");
            RadioButton rbtNotPass = (RadioButton)row.Cells[0].FindControl("rbtNotPass");
            if (!rbtPass.Checked && !rbtNotPass.Checked)
                bRet = false;
        }

        return bRet;
    }
    #endregion

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "QC/Transaction/QCAnalysisSearch.aspx");
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        bool isSave = true;
        if (txtAnaCode.Text != "")
        {
            foreach (GridViewRow row in grvItem.Rows)
            {
                double loid = Convert.ToDouble(row.Cells[10].Text);
                string qcresult = "";
                RadioButton rbtPass = (RadioButton)row.Cells[0].FindControl("rbtPass");
                RadioButton rbtNotPass = (RadioButton)row.Cells[1].FindControl("rbtNotPass");
                if (rbtNotPass.Checked)
                    qcresult = Constz.QCResult.Fail.Code;
                else if (rbtPass.Checked)
                    qcresult = Constz.QCResult.Pass.Code;
                TextBox txtQCQtyView = (TextBox)row.Cells[8].FindControl("txtQCQtyView");
                TextBox txtQCRemarkView = (TextBox)row.Cells[8].FindControl("txtQCRemarkView");
                Controls_DatePickerControl txtDate = (Controls_DatePickerControl)row.Cells[9].FindControl("txtDate");

                isSave = FlowObj.UpdateQCResult(loid, txtDate.DateValue, qcresult, txtQCRemarkView.Text,txtQCQtyView.Text.Trim(), Authz.CurrentUserInfo.UserID);
            }

            if (isSave)
            {
                if (FlowObj.UpdateQCStockin(Convert.ToDouble(txtLOID.Text), txtAnaCode.Text, ctlAnaDate.DateValue, Authz.CurrentUserInfo.UserID, Constz.Requisition.Status.QC.Code))
                {
                    ResetState(Convert.ToDouble(txtLOID.Text));
                    Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }
        }
        else
            Appz.ClientAlert(this, "กรุณาใส่เลขที่วิเคราะห์");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (txtAnaCode.Text != "")
        {
            if (!checkGrid())
                Appz.ClientAlert(this, "ระบุผลการตรวจสอบให้ครบทุกรายการ");
            else
            {
                bool isSave = true;
                foreach (GridViewRow row in grvItem.Rows)
                {
                    double loid = Convert.ToDouble(row.Cells[10].Text);
                    string qcresult = "Y";
                    RadioButton rbtPass = (RadioButton)row.Cells[0].FindControl("rbtPass");
                    if (!rbtPass.Checked)
                        qcresult = "N";
                    TextBox txtQCQtyView = (TextBox)row.Cells[8].FindControl("txtQCQtyView");
                    TextBox txtQCRemarkView = (TextBox)row.Cells[0].FindControl("txtQCRemarkView");
                    Controls_DatePickerControl txtDate = (Controls_DatePickerControl)row.Cells[9].FindControl("txtDate");
                    isSave = FlowObj.UpdateQCResult(loid, txtDate.DateValue, qcresult, txtQCRemarkView.Text, txtQCQtyView.Text.Trim(), Authz.CurrentUserInfo.UserID);
                }

                if (isSave)
                {
                    if (FlowObj.UpdateQCStockin(Convert.ToDouble(txtLOID.Text), txtAnaCode.Text, ctlAnaDate.DateValue, Authz.CurrentUserInfo.UserID, Constz.Requisition.Status.Approved.Code))
                    {
                        ResetState(Convert.ToDouble(txtLOID.Text));
                        Appz.ClientAlert(this, "ยืนยันข้อมูลเรียบร้อยแล้ว");
                    }
                    else
                        Appz.ClientAlert(this, FlowObj.ErrorMessage);
                }
            }
        }
        else
            Appz.ClientAlert(this, "กรุณาใส่เลขที่วิเคราะห์");
    }
    #endregion
}
