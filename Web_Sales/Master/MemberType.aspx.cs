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
using ABB.Global;
using ABB.Flow.Common;
using ABB.Flow.Sales;

public partial class Master_MemberType : System.Web.UI.Page
{
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            return new SessionPageStatePersister(this);
        }
    }
    private MemberTypeFlow _flow;
    private DiscountStep _item;

    private MemberTypeFlow FlowObj
    {
        get { if (_flow == null) { _flow = new MemberTypeFlow(); } return _flow; }
    }

    private DiscountStep ItemObj
    {
        get { if (_item == null) { _item = new DiscountStep(); } return _item; }
    }

    private void ResetState(double LOID)
    {
        ItemObj.ClearSession();
        MemberTypeData data = FlowObj.GetData(LOID);
        SetData(data);
    }

    private MemberTypeData GetData()
    {
        MemberTypeData data = new MemberTypeData();
        data.ACTIVE = (this.chkActive.Checked ? Constz.ActiveStatus.Active : Constz.ActiveStatus.InActive);
        data.CODE = this.txtCode.Text.Trim();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.NAME = this.txtName.Text.Trim();
        data.ITEM = ItemObj.GetItemList();
        return data;
    }

    private void SetData(MemberTypeData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE.Trim();
        this.chkActive.Checked = (data.ACTIVE == Constz.ActiveStatus.Active);
        this.txtName.Text = data.NAME.Trim();

        SetGrvItem();
    }

    private void InsertData(GridViewRow gRow)
    {
        TextBox txtLowerPrice = (TextBox)gRow.Cells[1].FindControl("txtLowerPriceNew");
        TextBox txtDiscount = (TextBox)gRow.Cells[1].FindControl("txtDiscountNew");

        DiscountStepData data = new DiscountStepData();
        data.DISCOUNT = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
        data.LOWERPRICE = Convert.ToDouble(txtLowerPrice.Text == "" ? "0" : txtLowerPrice.Text);

        if (ItemObj.InsertDiscountStep(data))
        {
            SetGrvItem();
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void SetGrvItem()
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        this.grvItem.Visible = (grvItem.Rows.Count > 0);
        this.grvItemNew.Visible = !(grvItem.Rows.Count > 0);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/MemberTypeSearch.aspx");
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

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                TextBox txtLowerPrice = (TextBox)e.Row.Cells[1].FindControl("txtLowerPrice");
                TextBox txtDiscount = (TextBox)e.Row.Cells[1].FindControl("txtDiscount");
                ControlUtil.SetIntTextBox(txtLowerPrice);
                ControlUtil.SetIntTextBox(txtDiscount);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox txtLowerPrice = (TextBox)e.Row.Cells[1].FindControl("txtLowerPriceNew");
            TextBox txtDiscount = (TextBox)e.Row.Cells[1].FindControl("txtDiscountNew");
            ControlUtil.SetIntTextBox(txtLowerPrice);
            ControlUtil.SetIntTextBox(txtDiscount);
        }
    }

    protected void grvItem_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.ExceptionHandled)
        {
            Appz.ClientAlert(this, e.Exception.Message);
        }
        else
        {
            SetGrvItem();
        }
    }

    protected void grvItem_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        e.ExceptionHandled = (e.Exception != null);
        if (e.ExceptionHandled)
        {
            e.KeepInEditMode = true;
            Appz.ClientAlert(this, e.Exception.InnerException.Message);
        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtLowerPrice = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[1].FindControl("txtLowerPrice");
        TextBox txtDiscount = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[1].FindControl("txtDiscount");

        e.NewValues["LOWERPRICE"] = Convert.ToDouble(txtLowerPrice.Text == "" ? "0" : txtLowerPrice.Text).ToString();
        e.NewValues["DISCOUNT"] = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text).ToString();
    }

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvItem.FooterRow);
        }
    }

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvItemNew.Rows[0]);
        }
    }

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtLowerPrice = (TextBox)e.Row.Cells[1].FindControl("txtLowerPriceNew");
            TextBox txtDiscount = (TextBox)e.Row.Cells[1].FindControl("txtDiscountNew");
            ControlUtil.SetIntTextBox(txtLowerPrice);
            ControlUtil.SetIntTextBox(txtDiscount);
        }
    }
}
