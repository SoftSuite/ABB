using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Data;
using ABB.Data.Sales;
using ABB.Data.Search;
using ABB.Flow.Search;
using ABB.Flow.Sales;

namespace ABBClient.Search
{
    public partial class CustomerPopup : Form
    {
        private double _LOID = 0;
        private SearchFlow _flow;
        private CustomerFlow _cFlow;

        public SearchFlow FlowObj
        {
            get { if (_flow == null) _flow = new SearchFlow(); return _flow; }
        }

        private CustomerFlow CustomerObj
        {
            get { if (_cFlow == null) {_cFlow = new CustomerFlow(); } return _cFlow; }
        }

        public double CustomerID
        {
            get { return _LOID; }
        }

        public CustomerPopup()
        {
            InitializeComponent();
        }

        public CustomerPopup(string customerCode)
        {
            InitializeComponent();
            this.txtCode.Text = customerCode;
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvCustomer,false,false,true);
        }

        public void SetData(SearchCustomerData data)
        {
            this.txtCode.Text = data.CODE;
            this.txtName.Text = data.NAME;
            this.txtLName.Text = data.LASTNAME;
            this.cmbCustomerType.SelectedValue = data.CUSTOMERTYPE;
            this.cmbMemberType.SelectedValue = data.MEMBERTYPE.ToString();
        }

        private SearchCustomerData GetData()
        {
            SearchCustomerData data = new SearchCustomerData();
            data.CODE = this.txtCode.Text.Trim();
            //data.FULLNAME = this.txtName.Text.Trim();
            data.NAME = this.txtName.Text.Trim();
            data.LASTNAME = this.txtLName.Text.Trim();
            data.CUSTOMERTYPE = this.cmbCustomerType.SelectedValue.ToString();
            data.MEMBERTYPE = Convert.ToDouble(this.cmbMemberType.SelectedValue);
            return data;
        }

        private void SearchData()
        {
            this.grvCustomer.DataSource = FlowObj.GetCustomerList(GetData());
            this.btnSelect.Enabled = (this.grvCustomer.Rows.Count > 0);
        }

        private void SelectCustomer()
        {
            if (this.grvCustomer.Rows.Count > 0)
            {
                _LOID = Convert.ToDouble(this.grvCustomer.CurrentRow.Cells["LOID"].Value);
                this.DialogResult = DialogResult.OK;
                CustomerData data = CustomerObj.GetData(_LOID);
                if (data.EFDATE.Date > DateTime.Today)
                    Appz.OpenInformationDialog("รหัสสมาชิกนี้จะมีผลตั้งแต่วันที่ " + data.EFDATE.ToString("dd/MM/yyyy"));
                else
                {
                    if (data.EPDATE < DateTime.Today)
                    {
                        Appz.OpenInformationDialog("รหัสสมาชิกนี้หมดอายุตั้งแต่วันที่ " + data.EPDATE.ToString("dd/MM/yyyy"));
                    }
                }
                this.Close();
            }
        }

        private void CustomerPopup_Load(object sender, EventArgs e)
        {
            Appz.BuildCombo(this.cmbMemberType, "MEMBERTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
            DataTable dt = new DataTable("CUDTOMERTYP");
            dt.Columns.Add("LOID");
            dt.Columns.Add("NAME");
            DataRow dRow = dt.NewRow();
            dRow["LOID"] = "";
            dRow["NAME"] = "เลือก";
            dt.Rows.Add(dRow);

            dRow = dt.NewRow();
            dRow["LOID"] = Constz.CustomerType.Company.Code;
            dRow["NAME"] = Constz.CustomerType.Company.Name;
            dt.Rows.Add(dRow);

            dRow = dt.NewRow();
            dRow["LOID"] = Constz.CustomerType.Government.Code;
            dRow["NAME"] = Constz.CustomerType.Government.Name;
            dt.Rows.Add(dRow);

            dRow = dt.NewRow();
            dRow["LOID"] = Constz.CustomerType.Personal.Code;
            dRow["NAME"] = Constz.CustomerType.Personal.Name;
            dt.Rows.Add(dRow);

            this.cmbCustomerType.DataSource = dt;
            this.cmbCustomerType.DisplayMember = "NAME";
            this.cmbCustomerType.ValueMember = "LOID";

            FormatGridView();
            SearchData();
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCustomer();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectCustomer();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
                SearchData();
        }

    }
}