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


public partial class Transaction_QCAnalysis_PD : System.Web.UI.Page
{
    #region Variables & Properties

    private PDProductQCFlow _flow;

    public PDProductQCFlow FlowObj
    {
        get { if (_flow == null) _flow = new PDProductQCFlow(); return _flow; }
    }

    #endregion

    #region Others

    private void ResetState(double loid)
    {
        SetData(FlowObj.GetData(loid));
    }

    #endregion

    #region Data

    private void SetData(PDProductData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtQCCode.Text = data.LOTNO;
        //this.txtCreateby.Text = data.CREATEBY;
        //this.txtInvNo.Text = data.INVNO;
        this.ctlQCDate.DateValue = data.SENDQCDATE;
        this.ctlAnaDate.DateValue = data.ANADATE;
        this.txtNo.Text = data.ANACODE;
        data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;

        SetPDOreder(data.PDORDER);
       // this.txtDivision.Text = FlowObj.GetDivision(data.CREATEBY);
        this.grvItem.DataSource = FlowObj.GetPDProductItem(data.LOID);

        this.grvItem.DataBind();

        if (data.PRODSTATUS == Constz.Requisition.Status.Approved.Code)
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

    private void SetPDOreder(double pdorder)
    {
        StockinQCData data = FlowObj.GetPDOrderData(pdorder);
        this.txtInvNo.Text = data.INVNO;
        //this.txtNo.Text = data.ANACODE;
        //this.ctlAnaDate.DateValue = data.ANADATE;
        this.txtCreateby.Text = data.CREATEBY;
        this.txtDivision.Text = FlowObj.GetDivision(data.CREATEBY);
        //this.txtSPCode.Text = data.SENDERCODE;
        //this.txtSPName.Text = data.SENDERNAME;

    }

    private StockinQCData GetPDOrederData()
    {
        StockinQCData data = new StockinQCData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.ANACODE = this.txtNo.Text.Trim();
        data.ANADATE = this.ctlAnaDate.DateValue;
        return data;
    }

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันส่งคลังใช่หรือไม่?');";
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
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetPDOrederData()))
        {
            foreach (GridViewRow row in grvItem.Rows)
            {
                double loid = Convert.ToDouble(row.Cells[12].Text);
                string qcresult = "";
                RadioButton rbtPass = (RadioButton)row.Cells[0].FindControl("rbtPass");
                RadioButton rbtNotPass = (RadioButton)row.Cells[1].FindControl("rbtNotPass");
                if (rbtNotPass.Checked)
                    qcresult = Constz.QCResult.Fail.Code;
                else if (rbtPass.Checked)
                    qcresult = Constz.QCResult.Pass.Code;

                TextBox txtQCRemarkView = (TextBox)row.Cells[10].FindControl("txtQCRemarkView");
                TextBox txtS1 = (TextBox)row.Cells[6].FindControl("txtS1");
                TextBox txtS2 = (TextBox)row.Cells[7].FindControl("txtS2");
                TextBox txtS3 = (TextBox)row.Cells[8].FindControl("txtS3");
                Controls_DatePickerControl txtDate = (Controls_DatePickerControl)row.Cells[11].FindControl("txtDate");

                if (FlowObj.UpdateQCResult(loid, txtDate.DateValue, Convert.ToDouble(txtS1.Text), Convert.ToDouble(txtS2.Text), Convert.ToDouble(txtS3.Text), qcresult, txtQCRemarkView.Text, Authz.CurrentUserInfo.UserID, Constz.Requisition.Status.QC.Code))
                {
                    ResetState(Convert.ToDouble(txtLOID.Text));
                    Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetPDOrederData()))
        {
        if (!checkGrid())
            Appz.ClientAlert(this, "ระบุผลการตรวจสอบให้ครบทุกรายการ");
        else
        {
            foreach (GridViewRow row in grvItem.Rows)
            {
                double loid = Convert.ToDouble(row.Cells[12].Text);
                string qcresult = Constz.QCResult.Pass.Code;
                RadioButton rbtPass = (RadioButton)row.Cells[0].FindControl("rbtPass");
                if (!rbtPass.Checked)
                    qcresult = Constz.QCResult.Fail.Code;
                TextBox txtQCRemarkView = (TextBox)row.Cells[10].FindControl("txtQCRemarkView");
                TextBox txtS1 = (TextBox)row.Cells[6].FindControl("txtS1");
                TextBox txtS2 = (TextBox)row.Cells[7].FindControl("txtS2");
                TextBox txtS3 = (TextBox)row.Cells[8].FindControl("txtS3");
                Controls_DatePickerControl txtDate = (Controls_DatePickerControl)row.Cells[11].FindControl("txtDate");

                if (FlowObj.UpdateQCResult(loid, txtDate.DateValue, Convert.ToDouble(txtS1.Text), Convert.ToDouble(txtS2.Text), Convert.ToDouble(txtS3.Text), qcresult, txtQCRemarkView.Text, Authz.CurrentUserInfo.UserID, Constz.ProductionStatus.Status.QB.Code))
                {
                    ResetState(Convert.ToDouble(txtLOID.Text));
                    Appz.ClientAlert(this, "ยืนยันข้อมูลเรียบร้อยแล้ว");
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }

        }
        }
   
    }
    #endregion
}
