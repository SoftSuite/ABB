using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Data;
using ABB.Flow;

namespace ABBClient.Transaction
{
    public partial class ProductStock : Form
    {
        ProductStockAdjustFlow _flow;
        private int indexNO = 0;
        private int indexLOID = 1;
        private int indexPRODUCTNAME = 2;
        private int indexQTY = 3;
        private int indexUNITNAME = 4;
        private ArrayList _editRows;

        private ProductStockAdjustFlow FlowObj
        {
            get { if (_flow == null) { _flow = new ProductStockAdjustFlow(); } return _flow; }
        }

        private ArrayList EditRows
        {
            get { if (_editRows == null) { _editRows = new ArrayList(); } return _editRows; }
        }

        public ProductStock()
        {
            InitializeComponent();
        }

        private void FormatGridView()
        {
            _editRows = new ArrayList();
            this.btnSave.Enabled = false;
            double zone = 0;
            double warehouse = 0;
            try
            {
                warehouse = Convert.ToDouble(this.cmbWarehouse.SelectedValue);
            }
            catch
            {
                warehouse = Convert.ToDouble(((DataRowView)this.cmbWarehouse.SelectedValue)["LOID"]);
            }
            if (warehouse == 1)
                zone = Constz.Zone.Z01;
            else if (warehouse == 2)
                zone = Constz.Zone.Z04;
            else
                zone = Constz.Zone.Z11;

            this.grvStock.DataSource = FlowObj.GetStockList(warehouse, zone);

            for (int i = 0; i < grvStock.Rows.Count; i++)
            {
                grvStock.Rows[i].Cells[indexNO].ReadOnly = true;
                grvStock.Rows[i].Cells[indexPRODUCTNAME].ReadOnly = true;
                grvStock.Rows[i].Cells[indexUNITNAME].ReadOnly = true;
            }
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormatGridView();
        }

        private void ProductStock_Load(object sender, EventArgs e)
        {
            Appz.FormatDataGridView(this.grvStock, false, false, false);
            Appz.BuildCombo(this.cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ");
            this.cmbWarehouse.SelectedValue = 3;
            this.cmbWarehouse.Enabled = false;
            FormatGridView();
        }

        private void grvStock_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl txtBox = (DataGridViewTextBoxEditingControl)e.Control;
                txtBox.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                if (this.grvStock.CurrentCell.OwningColumn.Index == indexQTY)
                {
                    txtBox.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                }
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        private void grvStock_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ProductStockAdjustData data;
            for (int i = 0; i < EditRows.Count; ++i)
            {
                data = new ProductStockAdjustData();
                data = (ProductStockAdjustData)EditRows[i];
                if (data.LOID == Convert.ToDouble(this.grvStock[indexLOID, e.RowIndex].Value))
                {
                    EditRows.RemoveAt(i);
                    break;
                }
            }
            data = new ProductStockAdjustData();
            data.LOID = Convert.ToDouble(this.grvStock[indexLOID, e.RowIndex].Value);
            data.QTY = Convert.ToDouble(Convert.IsDBNull(this.grvStock[indexQTY, e.RowIndex].Value) ? "0" : this.grvStock[indexQTY, e.RowIndex].Value);
            EditRows.Add(data);
            this.btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FlowObj.UpdateData(Appz.CurrentUserData.UserID, EditRows))
            {
                FormatGridView();
            }
        }

    }
}