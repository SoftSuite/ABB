using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Flow.Sales;
using ABB.Flow.Search;
using ABB.Data;
using ABB.Data.Sales;
    
namespace ABBClient.Transaction
{
    public partial class ReturnTesterEdit : Form
    {
        public ReturnTesterEdit()
        {
            InitializeComponent();
            SetDataGridViewImageColumnCellStyle();
        }

        #region Variables

        private bool endEditBarcode = true;
        private ABBClient.Reports.PreviewReport _pvReport;
        private ReturnTesterFlow _flow;
        private int indexORDERNO = 0;
        private int indexBARCODE = 1;
        private int indexSEARCH = 2;
        private int indexNAME = 3;
        private int indexQTY = 4;
        private int indexUNITNAME = 5;
        private int indexPRICE = 6;
        private int indexNETPRICE = 7;
        private int indexPRODUCT = 8;
        private int indexUNIT = 9;
        private double _StockOut = 0;

        #endregion

        #region Properties

        public ReturnTesterFlow FlowObj
        {
            get { if (_flow == null) { _flow = new ReturnTesterFlow(); } return _flow; }
            set { _flow = value; }
        }

        public double StockOut
        {
            set { _StockOut = value; }
        }

        #endregion

        private void SetDataGridViewImageColumnCellStyle()
        {
            Appz.SetDataGridViewImageColumnCellStyle(SEARCH);
        }

        public ReturnTesterData GetData()
        {
            ReturnTesterData data = new ReturnTesterData();
            data.CREATEBY = this.txtCreateBy.Text;
            data.CREATEON = this.dtpCreateOn.Value;
            data.ACTIVE = Constz.ActiveStatus.Active;
            if (this.txtStatus.Text != Constz.Requisition.Status.Waiting.Code)
            {
                data.APPROVEDATE = dtpApproveDate.Value;
                data.APPROVER = Convert.ToDouble(this.txtApprover.Text == "" ? "0" : this.txtApprover.Text);
            }
            data.CODE = this.txtCode.Text;
            data.DOCTYPE = Constz.DocType.RetSample.LOID;
            data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
            data.REASON = this.txtReason.Text.Trim();
            if (this.cmbReceiver.SelectedValue != null) data.RECEIVER = Convert.ToDouble(this.cmbReceiver.SelectedValue);
            data.REMARK = this.txtRemark.Text.Trim();
            data.SENDER = Convert.ToDouble(this.txtSender.Text);
            data.STATUS = this.txtStatus.Text.Trim();
            data.STOCKITEM = new ArrayList();
            foreach (DataGridViewRow gRow in this.grvStockIn.Rows)
            {
                if (!gRow.IsNewRow)
                {
                    StockOutItemData itemData = new StockOutItemData();

                    itemData.ACTIVE = Constz.ActiveStatus.Active;
                    itemData.PRODUCT = Convert.ToDouble(gRow.Cells[indexPRODUCT].Value);
                    itemData.QTY = Convert.ToDouble(gRow.Cells[indexQTY].Value);
                    itemData.STATUS = data.STATUS;
                    itemData.UNIT = Convert.ToDouble(gRow.Cells["UNIT"].Value);
                    data.STOCKITEM.Add(itemData);
                }
            }
            return data;
        }

        public void SetData(ReturnTesterData data)
        {
            this.txtStatus.Text = data.STATUS;
            SetStatusName(data.STATUS);
            this.txtApprover.Text = data.APPROVER.ToString();
            if (data.APPROVEDATE.Year != 1) this.dtpApproveDate.Value = data.APPROVEDATE;
            this.txtCode.Text = data.CODE;
            this.txtLOID.Text = data.LOID.ToString();
            this.txtReason.Text = data.REASON;
            this.cmbReceiver.SelectedValue = data.RECEIVER;
            this.txtRemark.Text = data.REMARK;
            this.txtSender.Text = data.SENDER.ToString();
            this.grvStockIn.Rows.Clear();
            this.txtDate.Text = data.CREATEON.ToString("dd/MM/yyyy");
            this.dtpCreateOn.Value = data.CREATEON;
            this.txtCreateBy.Text = data.CREATEBY;
            for (int i = 0; i < data.STOCKITEM.Count; ++i)
            {
                ReturnTesterItemData itemData = (ReturnTesterItemData)data.STOCKITEM[i];
                DataGridViewRow gRow = (DataGridViewRow)this.grvStockIn.Rows[this.grvStockIn.NewRowIndex].Clone();
                gRow.Cells[indexORDERNO].Value = i + 1;
                gRow.Cells[indexBARCODE].Value = itemData.BARCODE;
                gRow.Cells[indexNAME].Value = itemData.NAME;
                gRow.Cells[indexQTY].Value = itemData.QTY;
                gRow.Cells[indexUNITNAME].Value = itemData.UNITNAME;
                gRow.Cells[indexPRICE].Value = itemData.PRICE;
                gRow.Cells[indexNETPRICE].Value = itemData.PRICE * itemData.QTY;
                gRow.Cells[indexPRODUCT].Value = itemData.PRODUCT;
                gRow.Cells[indexUNIT].Value = itemData.UNIT;
                this.grvStockIn.Rows.Add(gRow);
            }
            if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
            {
                this.grvStockIn.AllowUserToAddRows = false;
                this.grvStockIn.AllowUserToDeleteRows = false;
                this.grvStockIn.ReadOnly = true;
                this.btnSave.Enabled = false;
                this.btnSubmit.Enabled = false;

                this.grvStockIn.Columns[indexSEARCH].Visible = false;
            }
            else
            {
                this.grvStockIn.AllowUserToAddRows = true;
                this.grvStockIn.AllowUserToDeleteRows = true;
                this.grvStockIn.ReadOnly = false;
                this.btnSave.Enabled = true;
                this.btnSubmit.Enabled = true;

                this.grvStockIn.Columns[indexSEARCH].Visible = true;
            }
            CalculateGrandTotal();
        }

        private bool SaveData(ReturnTesterData data)
        {
            bool ret = true;
            ret = FlowObj.UpdateData(Appz.CurrentUserData.UserID, data);
            if (ret)
                SetData(FlowObj.GetData(FlowObj.LOID));
            else
                Appz.OpenErrorDialog(FlowObj.ErrorMessage);
            return ret;
        }

        #region Methods

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvStockIn, true, true, false);
        }

        private void SetStatusName(string statusCode)
        {
            switch (statusCode)
            {
                case Constz.Requisition.Status.Waiting.Code :
                    this.txtStatusName.Text = Constz.Requisition.Status.Waiting.Name;
                    break;

                case Constz.Requisition.Status.Approved.Code:
                    this.txtStatusName.Text = Constz.Requisition.Status.Approved.Name;
                    break;

                case Constz.Requisition.Status.Void.Code:
                    this.txtStatusName.Text = Constz.Requisition.Status.Void.Name;
                    break;
            }
        }

        private void ResetOrder()
        {
            int index = 1;
            foreach (DataGridViewRow gRow in this.grvStockIn.Rows)
            {
                if (!gRow.IsNewRow)
                {
                    gRow.Cells[indexORDERNO].Value = index.ToString();
                    index += 1;
                }
            }
        }

        private void CalculateProductNetPrice()
        {
            foreach (DataGridViewRow gRow in this.grvStockIn.Rows)
            {
                CalculateProductItemNetPrice(gRow);
            }
            CalculateGrandTotal();
        }

        private void SearchProduct(string barcode, int rowIndex)
        {
            Search.ProductBarcodePopup frmProduct = new ABBClient.Search.ProductBarcodePopup(barcode, Appz.CurrentUserData.Warehouse);
            if (frmProduct.ShowDialog() == DialogResult.OK)
            {
                SetProductDetail(rowIndex, frmProduct.ProductID);
                SetProductDuplicate(rowIndex, frmProduct.ProductID, Convert.ToDouble(this.grvStockIn[indexQTY, rowIndex].Value));
            }
        }

        private void SetProductDetail(int rowIndex, double productID)
        {
            ProductTesterData data = FlowObj.GetProductData(productID);
            this.grvStockIn.Rows[rowIndex].Cells[indexPRODUCT].Value = productID.ToString();
            this.grvStockIn.Rows[rowIndex].Cells[indexUNIT].Value = data.UNIT.ToString();
            this.grvStockIn.Rows[rowIndex].Cells[indexBARCODE].Value = data.BARCODE;
            this.grvStockIn.Rows[rowIndex].Cells[indexNAME].Value = data.NAME;
            if (this.grvStockIn.Rows[rowIndex].Cells[indexQTY].Value == null) this.grvStockIn.Rows[rowIndex].Cells[indexQTY].Value = "1";
            if (this.grvStockIn.Rows[rowIndex].Cells[indexQTY].Value.ToString().Trim() == "") this.grvStockIn.Rows[rowIndex].Cells[indexQTY].Value = "1";
            this.grvStockIn.Rows[rowIndex].Cells[indexUNITNAME].Value = data.UNITNAME;
            this.grvStockIn.Rows[rowIndex].Cells[indexPRICE].Value = data.PRICE.ToString();
            this.grvStockIn.Rows[rowIndex].Cells[indexNETPRICE].Value = (Convert.ToDouble(this.grvStockIn.Rows[rowIndex].Cells[indexPRICE].Value) * Convert.ToDouble(this.grvStockIn.Rows[rowIndex].Cells[indexQTY].Value)).ToString();

            DataGridViewRow addRow = this.grvStockIn.Rows[rowIndex];
            if (addRow.IsNewRow)
            {
                SetDataGridViewImageColumnCellStyle();
                DataGridViewRow newRow = (DataGridViewRow)addRow.Clone();
                newRow.Cells[indexORDERNO].Value = this.grvStockIn.Rows.Count.ToString();
                newRow.Cells[indexBARCODE].Value = addRow.Cells[indexBARCODE].Value;
                newRow.Cells[indexNAME].Value = addRow.Cells[indexNAME].Value;
                newRow.Cells[indexNETPRICE].Value = addRow.Cells[indexNETPRICE].Value;
                newRow.Cells[indexPRICE].Value = addRow.Cells[indexPRICE].Value;
                newRow.Cells[indexPRODUCT].Value = addRow.Cells[indexPRODUCT].Value;
                newRow.Cells[indexQTY].Value = addRow.Cells[indexQTY].Value;
                newRow.Cells[indexUNIT].Value = addRow.Cells[indexUNIT].Value;
                newRow.Cells[indexUNITNAME].Value = addRow.Cells[indexUNITNAME].Value;
                this.grvStockIn.Rows.Add(newRow);

                for (int i = 0; i < this.grvStockIn.Columns.Count; ++i)
                {
                    addRow.Cells[i].Value = DBNull.Value;
                }

                CalculateProductItemNetPrice(newRow);
                CalculateGrandTotal();
            }
        }

        private bool SetProductDuplicate(int rowIndex, double product, double qty)
        {
            bool ret = false;
            foreach (DataGridViewRow pRow in this.grvStockIn.Rows)
            {
                if (!pRow.IsNewRow)
                {
                    if (pRow.Index != rowIndex && pRow.Cells[indexPRODUCT].Value.ToString() == product.ToString())
                    {
                        ret = true;
                        pRow.Cells[indexQTY].Value = (Convert.ToDouble(pRow.Cells[indexQTY].Value) + qty).ToString(Constz.IntFormat);
                        CalculateProductItemNetPrice(pRow);
                        break;
                    }
                }
            }

            if (ret)
            {
                if (!this.grvStockIn.Rows[rowIndex].IsNewRow)
                {
                    this.grvStockIn.EndEdit();
                    this.grvStockIn.Rows.Remove(this.grvStockIn.Rows[rowIndex]);
                }

                CalculateGrandTotal();
            }
            return ret;
        }

        private void CalculateProductItemNetPrice(DataGridViewRow gRow)
        {
            double price = 0;
            double quantity = 0;
            if (!gRow.IsNewRow)
            {
                if (!Convert.IsDBNull(gRow.Cells[indexPRICE].Value))
                    price = Convert.ToDouble(gRow.Cells[indexPRICE].Value);
                if (!Convert.IsDBNull(gRow.Cells[indexQTY].Value))
                    quantity = Convert.ToDouble(gRow.Cells[indexQTY].Value);

                gRow.Cells[indexNETPRICE].Value = (price* quantity).ToString();
            }
        }

        private void CalculateGrandTotal()
        {
            double total = 0;
            foreach (DataGridViewRow gRow in this.grvStockIn.Rows)
            {
                if (!Convert.IsDBNull(gRow.Cells[indexNETPRICE].Value)) total += Convert.ToDouble(gRow.Cells[indexNETPRICE].Value);
            }
            this.txtGrandTotal.Text = total.ToString(Constz.DblFormat);
        }

        #endregion

        #region Events

        private void grvStockIn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == indexSEARCH)
            {
                SearchProduct("", e.RowIndex);
            }
        }

        private void grvStockIn_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.grvStockIn.Rows[e.RowIndex].IsNewRow)
            {
                if (e.ColumnIndex == indexBARCODE)
                {
                    string barcode = this.grvStockIn.Rows[e.RowIndex].Cells[indexBARCODE].Value.ToString();
                    if (barcode == "")
                    {
                        this.grvStockIn.Rows[e.RowIndex].Cells[indexPRODUCT].Value = DBNull.Value;
                        this.grvStockIn.Rows[e.RowIndex].Cells[indexUNIT].Value = DBNull.Value;
                        this.grvStockIn.Rows[e.RowIndex].Cells[indexBARCODE].Value = DBNull.Value;
                        this.grvStockIn.Rows[e.RowIndex].Cells[indexNAME].Value = DBNull.Value;
                        this.grvStockIn.Rows[e.RowIndex].Cells[indexQTY].Value = DBNull.Value;
                        this.grvStockIn.Rows[e.RowIndex].Cells[indexPRICE].Value = DBNull.Value;
                        this.grvStockIn.Rows[e.RowIndex].Cells[indexNETPRICE].Value = DBNull.Value;
                    }
                    else
                    {
                        if (barcode.Substring(0, 1) == "*")
                        {
                            this.grvStockIn.Rows[e.RowIndex].Cells[indexPRODUCT].Value = DBNull.Value;
                            this.grvStockIn.Rows[e.RowIndex].Cells[indexUNIT].Value = DBNull.Value;
                            this.grvStockIn.Rows[e.RowIndex].Cells[indexBARCODE].Value = DBNull.Value;
                            this.grvStockIn.Rows[e.RowIndex].Cells[indexNAME].Value = DBNull.Value;
                            this.grvStockIn.Rows[e.RowIndex].Cells[indexPRICE].Value = DBNull.Value;
                            this.grvStockIn.Rows[e.RowIndex].Cells[indexNETPRICE].Value = DBNull.Value;

                            if (barcode.Length > 1)
                            {
                                barcode = barcode.Substring(1);
                                this.grvStockIn.Rows[e.RowIndex].Cells[indexQTY].Value = Convert.ToDouble(barcode);
                            }
                            this.grvStockIn.Rows[e.RowIndex].Cells[indexBARCODE].Value = "";
                            endEditBarcode = false;
                        }
                        else
                        {
                            SearchFlow search = new SearchFlow();
                            ABB.Data.Search.SearchProductData data = new ABB.Data.Search.SearchProductData();
                            data.CODE = barcode.Trim();
                            data.WAREHOUSE = Appz.CurrentUserData.Warehouse;
                            DataTable dt = search.GetProductList(data);
                            if (dt.Rows.Count == 1)
                            {
                                this.grvStockIn.Rows[e.RowIndex].Cells[indexPRODUCT].Value = dt.Rows[0]["LOID"].ToString();
                                SetProductDetail(e.RowIndex, Convert.ToDouble(dt.Rows[0]["LOID"]));

                                if (!SetProductDuplicate(e.RowIndex, Convert.ToDouble(dt.Rows[0]["LOID"]), Convert.ToDouble(this.grvStockIn[indexQTY, e.RowIndex].Value)))
                                {
                                    CalculateProductItemNetPrice(this.grvStockIn.Rows[e.RowIndex]);
                                    CalculateGrandTotal();
                                }

                            }
                            else
                            {
                                Appz.OpenErrorDialog("ไม่พบสินค้าตามบาร์โค้ดที่ระบุ");
                                return;
                            }
                        }
                    }
                }


                else if (e.ColumnIndex == indexQTY)
                {
                    if (this.grvStockIn.Rows[e.RowIndex].Cells[indexQTY].Value.ToString().Trim() == "") this.grvStockIn.Rows[e.RowIndex].Cells[indexQTY].Value = "1";

                    CalculateProductItemNetPrice(this.grvStockIn.Rows[e.RowIndex]);
                    CalculateGrandTotal();
                }
            }
        }

        private void grvStockIn_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ThrowException)
            {
                e.Cancel = true;
                Appz.OpenErrorDialog(e.Exception.Message);
            }
        }

        private void grvStockIn_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!this.grvStockIn.Rows[e.RowIndex].IsNewRow && this.grvStockIn[indexORDERNO, e.RowIndex].Value != null)
            {
                if (endEditBarcode)
                {
                    double product = 0;
                    if (!Convert.IsDBNull(this.grvStockIn.Rows[e.RowIndex].Cells[indexPRODUCT].Value)) product = Convert.ToDouble(this.grvStockIn.Rows[e.RowIndex].Cells[indexPRODUCT].Value);
                    if (product == 0)
                    {
                        Appz.OpenErrorDialog("กรุณาระบุสินค้า");
                        e.Cancel = true;
                    }
                    else if (Convert.ToDouble(this.grvStockIn.Rows[e.RowIndex].Cells[indexQTY].Value.ToString() == "" ? "0" : this.grvStockIn.Rows[e.RowIndex].Cells[indexQTY].Value) <= 0)
                    {
                        Appz.OpenErrorDialog("กรุณาระบุจำนวนสินค้า");
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                    endEditBarcode = true;
                }
            }

        }

        private void grvStockIn_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ResetOrder();
            SetDataGridViewImageColumnCellStyle();
        }

        private void grvStockIn_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            CalculateProductNetPrice();
            ResetOrder();
        }



        private void ReturnTesterEdit_Load(object sender, EventArgs e)
        {
            this.cmbReceiver.DataSource = null;
            Appz.BuildCombo(this.cmbReceiver, "WAREHOUSE", "NAME", "LOID", "NAME", "");
            FormatGridView();
            ReturnTesterData data = FlowObj.GetData(_StockOut);
            if (_StockOut == 0)
            {
                data.CREATEBY = Appz.CurrentUserData.UserID;
                data.STATUS = Constz.Requisition.Status.Waiting.Code;
                data.CREATEON = DateTime.Now;
                data.SENDER = Appz.CurrentUserData.Warehouse;
            }
            SetData(data);
            
            
         
        }

      
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool ret = SaveData(GetData());
            if (ret == true)
                Appz.OpenInformationDialog("บันทึกรายการเรียบร้อย");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Appz.OpenQuestionDialog("ต้องการส่งรายการขอคืนสินค้าตัวอย่างไปยังคลังสินค้าใช่หรือไม่?") == DialogResult.OK)
            {
                ReturnTesterData data = GetData();
                data.STATUS = Constz.Requisition.Status.Approved.Code;
                data.APPROVEDATE = DateTime.Now;
                data.APPROVER = Appz.CurrentUserData.OfficerID;
                bool ret = SaveData(data);
                if (ret == true)
                    Appz.OpenInformationDialog("ส่งคลังสำเร็จรูปเรียบร้อย");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text) > 0)
            {
                ArrayList arr = new ArrayList();
                ReportParameterData data = new ReportParameterData();
                data.PARAMETERNAME = "LOID";
                data.PARAMETERVALUE = this.txtLOID.Text;
                arr.Add(data);
                _pvReport = new ABBClient.Reports.PreviewReport(Constz.Report.ReturnTester, arr);
                _pvReport.ShowDialog();
            }
        }

        private void grvStockIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.grvStockIn.Rows[this.grvStockIn.Rows.Count - 1].Cells[indexSEARCH].Value = ((DataGridViewImageColumn)this.grvStockIn.Columns[indexSEARCH]).Image;
        }

        private void grvStockIn_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl txtBox = (DataGridViewTextBoxEditingControl)e.Control;

                txtBox.KeyPress -= new KeyPressEventHandler(Control_KeyPress);

                if (this.grvStockIn.CurrentCell.OwningColumn.Index == indexQTY)
                {
                    txtBox.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                }
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }



    }
}