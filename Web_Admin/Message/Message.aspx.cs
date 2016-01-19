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
using ABB.Flow.Admin;

public partial class Message_Message : System.Web.UI.Page
{
    private MessageFlow _flow;

    public MessageFlow FlowObj
    {
        get { if (_flow == null) _flow = new MessageFlow(); return _flow; }
    }

    private MessageData GetData()
    {
        MessageData data = new MessageData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text.Trim());
        data.MESSAGE = this.txtMessage.Text.Trim();
        data.EFDATE = this.dtpDateFrom.DateValue;
        data.EPDATE = this.dtpDateTo.DateValue;

        return data;
    }

    private void Search()
    {
        this.grvMessage.DataSource = FlowObj.GetMessageList();
        this.grvMessage.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnSave.Text = "<img src='" + Constz.ImageFolder + "icn_save.gif' border='0' align='AbsMiddle'> บันทึก";
            this.btnCancel.Text = "<img src='" + Constz.ImageFolder + "icn_cancel.gif' border='0' align='AbsMiddle'> ยกเลิก";
            Search();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            ClearAllCtrl();
            Search();
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    private void ClearAllCtrl()
    {
        this.txtLOID.Text = "0";
        this.txtMessage.Text = "";
        this.dtpDateFrom.DateValue = new DateTime(1, 1, 1);
        this.dtpDateTo.DateValue = new DateTime(1, 1, 1);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAllCtrl();
    }

    protected void grvMessage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imbEdit = (ImageButton)e.Row.FindControl("imbEdit");
            ImageButton imbDelete = (ImageButton)e.Row.FindControl("imbDelete");
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;

            if (Authz.CurrentUserInfo.UserID == drow["CREATEBY"].ToString())
            {
                imbEdit.Visible = true;
                imbDelete.Visible = true;
            }
            else
            {
                imbEdit.Visible = false;
                imbDelete.Visible = false;
            }
        }
    }

    protected void grvMessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ImageButton img = (ImageButton)e.CommandSource;
        Int16 rowIndex = (Int16)((GridViewRow)img.Parent.Parent).RowIndex;
        GridViewRow commandRow;
        commandRow = this.grvMessage.Rows[rowIndex];
        TextBox txtLOID = (TextBox)commandRow.Cells[0].FindControl("txtLOID");

        if (e.CommandName == "EditMessage")
        {
            //TextBox txtMessage = (TextBox)commandRow.Cells[0].FindControl("txtMessage");
            //Controls_DatePickerControl dtpEFDate = (Controls_DatePickerControl)commandRow.Cells[0].FindControl("dtpEFDate");
            //Controls_DatePickerControl dtpEPDate = (Controls_DatePickerControl)commandRow.Cells[0].FindControl("dtpEPDate");
            
            this.txtLOID.Text = txtLOID.Text;
            MessageData data = FlowObj.GetMessageData(Convert.ToDouble(this.txtLOID.Text));
            this.txtMessage.Text = data.MESSAGE;
            this.dtpDateFrom.DateValue = data.EFDATE;
            this.dtpDateTo.DateValue = data.EPDATE;
            //this.txtMessage.Text = txtMessage.Text;
            //this.dtpDateFrom.DateValue = dtpEFDate.DateValue;
            //this.dtpDateTo.DateValue = dtpEPDate.DateValue;
        }
        else if (e.CommandName == "DeleteMessage")
        {
            if (FlowObj.DeleteData(Convert.ToDouble(txtLOID.Text)))
                Search();
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }
}
