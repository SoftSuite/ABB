using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow;
using ABB.Data;

namespace ABBClient
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Authenticated()
        {
            AppFlow uFlow = new AppFlow();
            if (uFlow.IsPOSAuthenticated(this.txtUserID.Text, this.txtPassword.Text, Convert.ToDouble(this.cmbWareHouse.SelectedValue)))
            {
                Appz.CurrentUserData = uFlow.Data;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                Appz.OpenErrorDialog(uFlow.ErrorMessage);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Authenticated();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Appz.BuildCombo(this.cmbWareHouse, "WAREHOUSE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ");
            this.cmbWareHouse.SelectedValue = 3;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                if (this.txtPassword.Text.Trim() != "")
                    Authenticated();
                else
                    this.txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                if (this.txtUserID.Text.Trim() != "")
                    Authenticated();
                else
                    this.txtUserID.Focus();
            }
        }

    }
}