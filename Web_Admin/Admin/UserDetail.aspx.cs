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
using ABB.Flow;
using ABB.Global;


public partial class Admin_UserDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["id"] == "" || Request["id"] == null)
                Response.Redirect("UserList.aspx");
            SetStartUpData();
        }
    }

    private void SetStartUpData()
    {
        UserFlow uFlow = new UserFlow();
        DataTable zDt = uFlow.GetUserData(Request["id"]);
        if (zDt.Rows.Count > 0)
        {
            lblUID.Text = zDt.Rows[0]["USERID"].ToString();
            lblUName.Text = zDt.Rows[0]["TNAME"].ToString() + " " + zDt.Rows[0]["LASTNAME"].ToString();
        }
        txtRoleID.Text = uFlow.GetRoleID(Request["id"]);
        if (txtRoleID.Text == "")
        {
            pnlExtSystem.Visible = false;
            pnlRole.Visible = false;
            lblRoleError.Visible = true;
        }
        else
        {

            zDt = uFlow.GetRoleData(txtRoleID.Text);
            if (zDt.Rows.Count > 0)
            {
                pnlRole.Visible = true;
                lblRoleError.Visible = false ;
                pnlExtSystem.Visible = true;
                cmbLevel.SelectedIndex = cmbLevel.Items.IndexOf(cmbLevel.Items.FindByValue(zDt.Rows[0]["ZLEVEL"].ToString()));
                chkHHT.Checked = (zDt.Rows[0]["HHT"].ToString() == "Y");
                chkPOS.Checked = (zDt.Rows[0]["POS"].ToString() == "Y");

                if (cmbLevel.SelectedItem.Value == "A")
                    pnlRole.Visible = false;
                else
                {
                    SetEditGroupMode();
                }

                // SET Group Data
                z2Group.SetSource(uFlow.GetGroupRoleNotIn(txtRoleID.Text));
                z2Group.SetDest(uFlow.GetGroupRoleIn(txtRoleID.Text));

                // SET Menu Data
                z2Menu.SetSource(uFlow.GetMenuRoleNotAssign(txtRoleID.Text));
                z2Menu.SetDest(uFlow.GetMenuRoleAssign(txtRoleID.Text));
            }
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect("UserList.aspx");
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        UserFlow uFlow = new UserFlow();
        if (txtRoleID.Text == "")
        {
            if (uFlow.CreateRoleForUser(Authz.CurrentUserInfo.UserID, Request["id"], cmbLevel.SelectedItem.Value))
            {
                Response.Redirect("UserDetail.aspx?id=" + Request["id"]);
            }
            else
                Appz.ClientAlert(this, uFlow.ErrorMessage);
        }
        else
        {
            // Update Role Level
            if (!uFlow.UpdateRoleLevel(Authz.CurrentUserInfo.UserID, txtRoleID.Text, cmbLevel.SelectedItem.Value, chkHHT.Checked, chkPOS.Checked))
                Appz.ClientAlert(this, uFlow.ErrorMessage);
            else
            {

                // Save Group
                if (!uFlow.SaveUserGroup(Authz.CurrentUserInfo.UserID, txtRoleID.Text, (cmbLevel.SelectedItem.Value == "A" ? new ArrayList() : z2Group.SelectedData)))
                    Appz.ClientAlert(this, uFlow.ErrorMessage);
                else
                {

                    // Save Role
                    if (!uFlow.SaveRoleMenu(Authz.CurrentUserInfo.UserID, txtRoleID.Text, (cmbLevel.SelectedItem.Value == "A" ? new ArrayList() : z2Menu.SelectedData)))
                        Appz.ClientAlert(this, uFlow.ErrorMessage);
                    else
                        Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อย");
                }
            }
        }
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        Response.Redirect("UserDetail.aspx?id=" + Request["id"]);
    }


    protected void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbLevel.SelectedItem.Value == "A")
            pnlRole.Visible = false;
        else
        {
            pnlRole.Visible = true;
            SetEditGroupMode();
        }
    }
    protected void lnbGroup_Click(object sender, EventArgs e)
    {
        SetEditGroupMode();
    }
    protected void lnbMenu_Click(object sender, EventArgs e)
    {
        SetEditMenuMode();
    }

    private void SetEditGroupMode()
    {
        lnbGroup.Font.Bold = true;
        lnbMenu.Font.Bold = false;
        pnlGroup.Visible = true;
        pnlMenu.Visible = false;
    }

    private void SetEditMenuMode()
    {
        lnbGroup.Font.Bold = false;
        lnbMenu.Font.Bold = true;
        pnlGroup.Visible = false;
        pnlMenu.Visible = true;
    }
}
