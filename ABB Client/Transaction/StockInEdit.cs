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
using ABB.Data.Sales;
using ABB.Data;

namespace ABBClient.Transaction
{
    public partial class StockInEdit : Form
    {
        #region Variables

        private bool endEditBarcode = true;
        private SaleFlow _sFlow;
        private StockInShopFlow _flow;
        private ABBClient.Reports.PreviewReport _pvReport;
        private Search.ProductStockInShopPopup frmSearch;
        private int indexPRODUCT = 0;
        private int indexUNIT = 1;
        private int indexORDERNO = 2;
        private int indexBARCODE = 3;
        private int indexSEARCH = 4;
        private int indexNAME = 5;
        private int indexQTY = 6;
        private int indexUNITNAME = 7;
        private int indexPRICE = 8;
        private int indexNETPRICE = 9;

        private StockInShopFlow FlowObj
        {
            get { if (_flow == null) { _flow = new StockInShopFlow(); } return _flow; }
        }

        public SaleFlow SaleObj
        {
            get { if (_sFlow == null) { _sFlow = new SaleFlow(); } return _sFlow; }
        }

        #endregion

        #region Methods

        public StockInEdit(double stockIn)
        {
            InitializeComponent();
            SetDataGridViewImageColumnCellStyle();
            this.txtLOID.Text = stockIn.ToString();
        }

        private void SetDataGridViewImageColumnCellStyle()
        {
            Appz.SetDataGridViewImageColumnCellStyle(SEARCH);
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvStockin, true, true, false);
        }

        private void SetNewBarcodeFocus()
        {
            try
            {
                this.grvStockin.Focus();
                this.grvStockin.CurrentCell = this.grvStockin[indexBARCODE, this.grvStockin.NewRowIndex];
                this.grvStockin.CurrentCell.Selected = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void Calculate()
        {
            double total = 0;
            foreach (DataGridViewRow gRow in this.grvStockin.Rows)
            {
                gRow.Cells[indexNETPRICE].Value = Convert.ToDouble(Convert.IsDBNull(gRow.Cells[indexPRICE].Value) ? "0" : gRow.Cells[indexPRICE].Value) * Convert.ToDouble(Convert.IsDBNull(gRow.Cells[indexQTY].Value) ? "0" : gRow.Cells[indexQTY].Value);
                total += Convert.ToDouble(gRow.Cells[indexNETPRICE].Value);
            }
            this.txtGrandTot.Text = total.ToString(Constz.DblFormat);
        }

        private bool Validate(DataGridViewRow gRow)
        {
            bool ret = true;
            double product = 0;
            double minQty = 0;
            double qty = 0;
            if (!Convert.IsDBNull(gRow.Cells[indexQTY].Value))
                if (gRow.Cells[indexQTY].Value != null) qty = Convert.ToDouble(gRow.Cells[indexQTY].Value.ToString() == "" ? "0" : gRow.Cells[indexQTY].Value);
            if (!Convert.IsDBNull(gRow.Cells[indexPRODUCT].Value)) product = Convert.ToDouble(gRow.Cells[indexPRODUCT].Value);
            if (product == 0)
            {
                Appz.OpenErrorDialog("กรุณาระบุสินค้า");
                ret = false;
            }
            else if (qty <= 0)
            {
                Appz.OpenErrorDialog("กรุณาระบุจำนวนสินค้า");
                ret = false;
            }
            return ret;
        }

        private void ResetOrder()
        {
            int index = 1;
            foreach (DataGridViewRow gRow in this.grvStockin.Rows)
            {
                if (!gRow.IsNewRow)
                {
                    gRow.Cells[indexORDERNO].Value = index.ToString();
                    index += 1;
                }
            }
        }

        #region Search Product

        private void SearchProduct(string barcode, DataGridViewRow gRow)
        {
            Search.ProductShopPopup frmProduct = new ABBClient.Search.ProductShopPopup(barcode);
            if (frmProduct.ShowDialog() == DialogResult.OK)
            {
                SetProductDetail(gRow, frmProduct.ProductID);
            }
        }

        private void SetProductDetail(DataGridViewRow gRow, double productID)
        {
            bool isDuplicate = false;
            double quantity = 0;
            if (gRow.Cells[indexQTY].Value == null) gRow.Cells[indexQTY].Value = 1;
            if (gRow.Cells[indexQTY].Value.ToString().Trim() == "") gRow.Cells[indexQTY].Value = 1;
            quantity = Convert.ToDouble(gRow.Cells[indexQTY].Value);

            foreach (DataGridViewRow cRow in this.grvStockin.Rows)
            {
                if (!cRow.IsNewRow)
                {
                    if (cRow.Index != gRow.Index && cRow.Cells[indexPRODUCT].Value.ToString() == productID.ToString())
                    {
                        cRow.Cells[indexQTY].Value = (Convert.ToDouble(cRow.Cells[indexQTY].Value) + quantity).ToString(Constz.IntFormat);
                        isDuplicate = true;
                        break;
                    }
                }
            }

            if (isDuplicate)
            {
                if (gRow.IsNewRow)
                {
                    for (int i = 0; i < this.grvStockin.Columns.Count; ++i)
                    {
                        gRow.Cells[i].Value = DBNull.Value;
                    }
                }
                else
                {
                    this.grvStockin.EndEdit();
                    this.grvStockin.Rows.Remove(gRow);
                }

            }
            else
            {
                ProductSaleData data = SaleObj.GetProductPromotion(productID, 3);
                gRow.Cells[indexPRODUCT].Value = data.PRODUCT;
                gRow.Cells[indexUNIT].Value = data.UNIT.ToString();
                gRow.Cells[indexBARCODE].Value = data.BARCODE;
                gRow.Cells[indexNAME].Value = data.PRODUCTNAME;
                gRow.Cells[indexUNITNAME].Value = data.UNITNAME;
                gRow.Cells[indexPRICE].Value = data.UNITPRICE;
                gRow.Cells[indexNETPRICE].Value = data.UNITPRICE * Convert.ToDouble(gRow.Cells[indexQTY].Value);

                if (gRow.IsNewRow)
                {
                    SetDataGridViewImageColumnCellStyle();

                    DataGridViewRow newRow = (DataGridViewRow)gRow.Clone();
                    newRow.Cells[indexORDERNO].Value = this.grvStockin.Rows.Count.ToString();
                    newRow.Cells[indexBARCODE].Value = gRow.Cells[indexBARCODE].Value;
                    newRow.Cells[indexNAME].Value = gRow.Cells[indexNAME].Value;
                    newRow.Cells[indexNETPRICE].Value = gRow.Cells[indexNETPRICE].Value;
                    newRow.Cells[indexPRICE].Value = gRow.Cells[indexPRICE].Value;
                    newRow.Cells[indexPRODUCT].Value = gRow.Cells[indexPRODUCT].Value;
                    newRow.Cells[indexQTY].Value = gRow.Cells[indexQTY].Value;
                    newRow.Cells[indexUNIT].Value = gRow.Cells[indexUNIT].Value;
                    newRow.Cells[indexUNITNAME].Value = gRow.Cells[indexUNITNAME].Value;

                    for (int i = 0; i < this.grvStockin.Columns.Count; ++i)
                    {
                        gRow.Cells[i].Value = DBNull.Value;
                    }

                    if (!Validate(newRow)) return;

                    this.grvStockin.Rows.Add(newRow);//

                }
            }

            Calculate();
        }

        #endregion

        #region Data

        private void ResetState()
        {
            SetData(FlowObj.GetStockInData(Convert.ToDouble(this.txtLOID.Text)));
        }

        private void SetData(StockInShopData data)
        {
            this.txtRequisitionCode.Text = data.REQUISITIONCODE;
            this.txtLOID.Text = data.LOID.ToString();
            this.txtReceiver.Text = "3";
            this.txtSender.Text = "1";
            this.txtStatus.Text = data.STATUS;
            this.txtCode.Text = data.CODE;
            this.txtReceiveDate.Text = data.RECEIVEDATE.ToString(Constz.DateFormat);
            this.dtpReceiveDate.Value = data.RECEIVEDATE;
            this.txtCreateBy.Text = data.CREATEBY;
            this.txtGrandTot.Text = data.GRANDTOT.ToString(Constz.DblFormat);
            this.txtRemark.Text = data.REMARK;

            this.grvStockin.Rows.Clear();
            for (int i = 0; i < data.ITEM.Rows.Count; ++i)
            {
                DataRow itemRow = data.ITEM.Rows[i];
                DataGridViewRow gRow = (DataGridViewRow)this.grvStockin.Rows[this.grvStockin.NewRowIndex].Clone();
                gRow.Cells[indexPRODUCT].Value = Convert.ToDouble(itemRow["PRODUCT"]);
                gRow.Cells[indexUNIT].Value = Convert.ToDouble(itemRow["UNIT"]);
                gRow.Cells[indexORDERNO].Value = i + 1;
                gRow.Cells[indexBARCODE].Value = itemRow["BARCODE"].ToString();
                gRow.Cells[indexNAME].Value = itemRow["NAME"].ToString();
                gRow.Cells[indexQTY].Value = Convert.ToDouble(itemRow["QTY"]);
                gRow.Cells[indexUNITNAME].Value = itemRow["UNITNAME"].ToString();
                gRow.Cells[indexPRICE].Value = Convert.ToDouble(itemRow["PRICE"]);
                gRow.Cells[indexNETPRICE].Value = Convert.ToDouble(itemRow["PRICE"]) * Convert.ToDouble(itemRow["QTY"]);

                gRow.Cells[indexORDERNO].ReadOnly = true;
                gRow.Cells[indexNAME].ReadOnly = true;
                gRow.Cells[indexUNITNAME].ReadOnly = true;
                gRow.Cells[indexPRICE].ReadOnly = true;
                gRow.Cells[indexNETPRICE].ReadOnly = true;
                this.grvStockin.Rows.Add(gRow);
            }
            if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
            {
                this.btnSave.Visible = false;
                this.btnSubmit.Visible = false;
                this.txtRequisitionCode.ReadOnly = true;
                this.txtRequisitionCode.Enabled = false;
                Appz.FormatDataGridView(this.grvStockin, false, false, true);
                this.grvStockin.Columns[indexSEARCH].Visible = false;
            }
            else
            {
                Appz.FormatDataGridView(this.grvStockin, true, true, false);
            }

        }

        private StockInData GetData()
        {
            StockInData data = new StockInData();
            data.LOID = Convert.ToDouble(this.txtLOID.Text);
            data.CODE = this.txtCode.Text.Trim();
            data.GRANDTOT = Convert.ToDouble(this.txtGrandTot.Text);
            data.RECEIVEDATE = this.dtpReceiveDate.Value.Date;
            data.RECEIVER = Convert.ToDouble(this.txtReceiver.Text);
            data.REMARK = this.txtRemark.Text.Trim();
            data.SENDER = Convert.ToDouble(this.txtSender.Text);
            data.STATUS = this.txtStatus.Text;
            data.REFCODE = this.txtRequisitionCode.Text.Trim();
            data.GRANDTOT = Convert.ToDouble(this.txtGrandTot.Text == "" ? "0" : this.txtGrandTot.Text);

            foreach (DataGridViewRow gRow in this.grvStockin.Rows)
            {
                StockInItemData itemData = new StockInItemData();
                if (!gRow.IsNewRow) 
                {
                    itemData.PRICE = Convert.ToDouble(gRow.Cells[indexPRICE].Value);
                    itemData.PRODUCT = Convert.ToDouble(gRow.Cells[indexPRODUCT].Value);
                    itemData.QTY = Convert.ToDouble(gRow.Cells[indexQTY].Value);
                    itemData.UNIT = Convert.ToDouble(gRow.Cells[indexUNIT].Value);
                    data.STOCKINITEM.Add(itemData);
                }
            }

            return data;
        }

        #endregion

        #endregion

        #region Events

        private void StockInEdit_Load(object sender, EventArgs e)
        {
            ResetState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FlowObj.UpdateData(GetData(), Appz.CurrentUserData.UserID))
            {
                this.txtLOID.Text = FlowObj.LOID.ToString();
                ResetState();
                Appz.OpenInformationDialog("บันทึกข้อมูลเรียบร้อยแล้ว");
            }
            else
                Appz.OpenErrorDialog(FlowObj.ErrorMessage);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Appz.OpenQuestionDialog("ต้องการยืนยันรายการใช่หรือไม่?") == DialogResult.OK)
            {
                if (FlowObj.CommitData(GetData(), Appz.CurrentUserData.UserID))
                {
                    this.txtLOID.Text = FlowObj.LOID.ToString();
                    ResetState();
                    Appz.OpenInformationDialog("ยืนยันรายการเรียบร้อยแล้ว");
                }
                else
                    Appz.OpenErrorDialog(FlowObj.ErrorMessage);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ArrayList arr = new ArrayList();
            ReportParameterData data = new ReportParameterData();
            data.PARAMETERNAME = "LOID";
            data.PARAMETERVALUE = this.txtLOID.Text;
            arr.Add(data);
            _pvReport = new ABBClient.Reports.PreviewReport(Constz.Report.ProductStockInShop, arr);
            _pvReport.ShowDialog();
        }

        #region GridView

        private void grvStockin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == indexSEARCH && e.RowIndex >= 0)
            {
                SearchProduct("", this.grvStockin.Rows[e.RowIndex]);
                SetNewBarcodeFocus();
            }
        }

        private void grvStockin_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.grvStockin.Rows[e.RowIndex].IsNewRow)
            {
                if (e.ColumnIndex == indexBARCODE)
                {
                    string barcode = (this.grvStockin.Rows[e.RowIndex].Cells[indexBARCODE].Value == null ? "" : this.grvStockin.Rows[e.RowIndex].Cells[indexBARCODE].Value.ToString().Trim());

                    if (barcode == "")
                    {
                        this.grvStockin.Rows[e.RowIndex].Cells[indexPRODUCT].Value = DBNull.Value;
                        this.grvStockin.Rows[e.RowIndex].Cells[indexUNIT].Value = DBNull.Value;
                        this.grvStockin.Rows[e.RowIndex].Cells[indexBARCODE].Value = DBNull.Value;
                        this.grvStockin.Rows[e.RowIndex].Cells[indexNAME].Value = DBNull.Value;
                        this.grvStockin.Rows[e.RowIndex].Cells[indexQTY].Value = DBNull.Value;
                        this.grvStockin.Rows[e.RowIndex].Cells[indexPRICE].Value = DBNull.Value;
                        this.grvStockin.Rows[e.RowIndex].Cells[indexNETPRICE].Value = DBNull.Value;
                    }
                    else
                    {
                        if (barcode.Substring(0, 1) == "*")
                        {
                            this.grvStockin.Rows[e.RowIndex].Cells[indexPRODUCT].Value = DBNull.Value;
                            this.grvStockin.Rows[e.RowIndex].Cells[indexUNIT].Value = DBNull.Value;
                            this.grvStockin.Rows[e.RowIndex].Cells[indexBARCODE].Value = DBNull.Value;
                            this.grvStockin.Rows[e.RowIndex].Cells[indexNAME].Value = DBNull.Value;
                            this.grvStockin.Rows[e.RowIndex].Cells[indexQTY].Value = DBNull.Value;
                            this.grvStockin.Rows[e.RowIndex].Cells[indexPRICE].Value = DBNull.Value;
                            this.grvStockin.Rows[e.RowIndex].Cells[indexNETPRICE].Value = DBNull.Value;

                            if (barcode.Length > 1)
                            {
                                barcode = barcode.Substring(1);
                                this.grvStockin.Rows[e.RowIndex].Cells[indexQTY].Value = Convert.ToDouble(barcode);
                            }
                            this.grvStockin.Rows[e.RowIndex].Cells[indexBARCODE].Value = "";
                            endEditBarcode = false;
                        }
                        else
                        {
                            SearchFlow search = new SearchFlow();
                            ABB.Data.Search.SearchProductData data = new ABB.Data.Search.SearchProductData();
                            data.CODE = barcode.Trim();
                            DataTable dt = search.GetProductShopList(data);
                            if (dt.Rows.Count == 1)
                            {
                                this.grvStockin.Rows[e.RowIndex].Cells[indexPRODUCT].Value = dt.Rows[0]["LOID"].ToString();
                                SetProductDetail(this.grvStockin.Rows[e.RowIndex], Convert.ToDouble(dt.Rows[0]["LOID"]));
                                SetNewBarcodeFocus();
                            }
                            else
                            {
                                Appz.OpenErrorDialog("ไม่พบสินค้าตามบาร์โค้ดที่ระบุ");
                                endEditBarcode = false;
                                return;
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == indexQTY)
                {
                    if (this.grvStockin.Rows[e.RowIndex].Cells[indexQTY].Value.ToString().Trim() == "") this.grvStockin.Rows[e.RowIndex].Cells[indexQTY].Value = "1";

                    Calculate();
                    SetNewBarcodeFocus();
                }
            }
        }

        private void grvStockin_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl txtBox = (DataGridViewTextBoxEditingControl)e.Control;
                txtBox.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                if (this.grvStockin.CurrentCell.OwningColumn.Index == indexQTY)
                {
                    txtBox.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                }
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        private void grvStockin_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!this.grvStockin.Rows[e.RowIndex].IsNewRow && this.grvStockin[indexORDERNO, e.RowIndex].Value != null)
            {
                if (endEditBarcode)
                {
                    e.Cancel = !Validate(this.grvStockin.Rows[e.RowIndex]);
                }
                else
                    e.Cancel = true;
                endEditBarcode = true;
            }
        }

        private void grvStockin_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ResetOrder();
            SetDataGridViewImageColumnCellStyle();
        }

        private void grvStockin_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Calculate();
            ResetOrder();
        }

        #endregion

        private void txtRequisitionCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13)) SetNewBarcodeFocus();
        }

        #endregion

    }
}