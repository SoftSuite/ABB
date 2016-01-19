using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow.Sales ;
using System.Collections;

namespace ABBClient.Master
{
    public partial class PackageSearch : Form
    {
        public PackageSearch()
        {
            InitializeComponent(); 
        }

        private void LoadData()
        {
            this.grvPackageStock.DataSource = PackageFlow.GetSearchPackage(Appz.CurrentUserData.Warehouse.ToString(),"", "");
        }

        private void PackageSearch_Load(object sender, EventArgs e)
        {
            Appz.FormatDataGridView(this.grvPackageStock, false, false, true);
            LoadData();
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            //this.grvPackageStock.DataSource = PackageFlow.GetSearchPackage(Authz.CurrentUserInfo.Warehouse.ToString(),txtCode.Text.ToString(),txtPName.Text.ToString());
            ArrayList arr = PackageFlow.GetSearchPackage(Appz.CurrentUserData.Warehouse.ToString(), txtCode.Text.ToString(), txtPName.Text.ToString());
            this.grvPackageStock.DataSource = arr;

            if (arr.Count > 0)
            {
                
                this.grvPackageStock.Columns["ORDERNO"].HeaderText = "�ӴѺ";
                this.grvPackageStock.Columns["BARCODE"].HeaderText = "���ʡ�����";
                this.grvPackageStock.Columns["PNAME"].HeaderText = "���͡�����";
                this.grvPackageStock.Columns["Cost"].HeaderText = "�Ҥҷع";
                this.grvPackageStock.Columns["STDPRICE"].HeaderText = "�ҤҢ��";
                this.grvPackageStock.Columns["ORDERNO"].DisplayIndex = 0;
                this.grvPackageStock.Columns["BARCODE"].DisplayIndex = 1;
                this.grvPackageStock.Columns["PNAME"].DisplayIndex = 2;
                this.grvPackageStock.Columns["Cost"].DisplayIndex = 3;
                this.grvPackageStock.Columns["STDPRICE"].DisplayIndex = 4;

            }
            //else
            //{
            //    this.grvPackageStock.Columns["ORDERNO"].HeaderText = "�ӴѺ";
            //    this.grvPackageStock.Columns["ProductCode"].HeaderText = "���ʡ�����";
            //    this.grvPackageStock.Columns["ProductName"].HeaderText = "���͡�����";
            //    this.grvPackageStock.Columns["Cost"].HeaderText = "�Ҥҷع";
            //    this.grvPackageStock.Columns["STDPRICE"].HeaderText = "�ҤҢ��";
            //}
        }
    }
}