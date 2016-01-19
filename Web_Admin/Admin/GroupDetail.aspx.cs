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
using ABB.Data;
using ABB.Global;

public partial class Admin_GroupDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            SetData();
    }

    private void SetData()
    {
        if (Request["id"] != "new" && Request["id"] != "")
        {
            pnlRole.Visible = true;

            UserFlow uFlow = new UserFlow();
            txtGroupID.Text = Request["id"];

            DataTable zDt = uFlow.GetRoleData(txtGroupID.Text);

            if (zDt.Rows.Count > 0)
            {
                if (zDt.Rows[0]["ZLEVEL"].ToString() != "G")
                {
                    pnlRole.Visible = false;
                    lblRoleError.Visible = true;
                    lblRoleError.Text = "Group ID ไม่ถูกต้อง";
                    txtGroupName.Enabled = false;
                }
                else
                {
                    txtGroupName.Text = zDt.Rows[0]["DESCRIPTION"].ToString();
                }
            }
            else
            {
                pnlRole.Visible = false;
                lblRoleError.Visible = true;
                lblRoleError.Text = "Group ID ไม่ถูกต้อง";
                txtGroupName.Enabled = false;
            }


            // SET Menu Data
            z2Menu.SetSource(uFlow.GetMenuRoleNotAssign(txtGroupID.Text));
            z2Menu.SetDest(uFlow.GetMenuRoleAssign(txtGroupID.Text));


        }
        else
        {
            lblRoleError.Visible = true;
            pnlRole.Visible = false;
        }
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        UserFlow uFlow = new UserFlow();
        if (txtGroupID.Text == "")
        {
            if (uFlow.InsertGroup(Authz.CurrentUserInfo.UserID, txtGroupName.Text))
            {
                Response.Redirect("GroupDetail.aspx?id=" + uFlow.GetLastRoleID());
            }
            else
                Appz.ClientAlert(this, uFlow.ErrorMessage);
        }
        else
        {
            if (!uFlow.UpdateGroup(Authz.CurrentUserInfo.UserID, txtGroupID.Text, txtGroupName.Text))
                Appz.ClientAlert(this, uFlow.ErrorMessage);
            else
            {
                // Save Role
                if (!uFlow.SaveRoleMenu(Authz.CurrentUserInfo.UserID, txtGroupID.Text, z2Menu.SelectedData))
                    Appz.ClientAlert(this, uFlow.ErrorMessage);
                else
                    Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อย");
            }
        }
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        Response.Redirect("GroupDetail.aspx?id=" + txtGroupID.Text);
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect("GroupList.aspx");
    }

}
