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
    public partial class SupportEdit : Form
    {
        public SupportEdit()
        {
            InitializeComponent();
            SetDataGridViewImageColumnCellStyle();
        }

        public SupportEdit(double requisition)
        {
            InitializeComponent();
            this.txtLOID.Text = requisition.ToString();
            SetDataGridViewImageColumnCellStyle();
        }

        #region Variables

        private bool endEditBarcode = true;
        private SupportFlow _flow;
        private SaleFlow _sFlow;
        private int indexPRODUCT = 0;
        private int indexUNIT = 1;
        private int indexORDERNO = 2;
        private int indexBARCODE = 3;
        private int indexSEARCH = 4;
        private int indexNAME = 5;
        private int indexQTY = 6;
        private int indexUNITNAME = 7;
        private int indexPRICE = 8;
        private int indexDISCOUNT = 9;
        private int indexNETPRICE = 10;
        private int indexISVAT = 11;
        private int indexNORMALDISCOUNT = 12;

        #endregion

        #region Properties

        public SupportFlow FlowObj
        {
            get { if (_flow == null) { _flow = new SupportFlow(); } return _flow; }
        }

        public SaleFlow SaleObj
        {
            get { if (_sFlow == null) { _sFlow = new SaleFlow(); } return _sFlow; }
        }

        #endregion

        #region Methods

        #region Data

        private void SetData(RequisitionData data)
        {
            this.txtCreateBy.Text = data.CREATEBY;
            this.txtStatus.Text = data.STATUS;
            this.txtStatusName.Text = Appz.GetStatusName(data.STATUS);
            this.txtVat.Text = ABB.Flow.SysConfigFlow.GetValue(Constz.ConfigName.VAT);
            this.txtCode.Text = data.CODE;
            this.dtpReserveDate.Value = data.RESERVEDATE;
            this.txtReserveDate.Text = data.RESERVEDATE.ToString(Constz.DateFormat);
            this.txtCustomer.Text = data.CUSTOMER.ToString();
            SetCustomerDetail(data.CUSTOMER);
            this.txtTotal.Text = data.TOTAL.ToString(Constz.DblFormat);
            this.txtTotalDiscount.Text = data.TOTDIS.ToString(Constz.DblFormat);
            this.txtTotalVat.Text = data.TOTVAT.ToString(Constz.DblFormat);
            this.txtGrandTotal.Text = data.GRANDTOT.ToString(Constz.DblFormat);
            this.txtReason.Text = data.REASON;
            this.txtRemark.Text = data.REMARK;
            this.grvSupport.Rows.Clear();
            for (int i = 0; i < data.REQUISITIONITEM.Count; ++i)
            {
                RequisitionItemData itemData = (RequisitionItemData)data.REQUISITIONITEM[i];
                DataGridViewRow gRow = (DataGridViewRow)this.grvSupport.Rows[this.grvSupport.NewRowIndex].Clone();
                gRow.Cells[indexPRODUCT].Value = itemData.PRODUCT;
                gRow.Cells[indexUNIT].Value = itemData.UNIT;
                gRow.Cells[indexORDERNO].Value = i + 1;
                gRow.Cells[indexBARCODE].Value = itemData.BarCode;
                gRow.Cells[indexNAME].Value = itemData.ProductName;
                gRow.Cells[indexQTY].Value = itemData.QTY;
                gRow.Cells[indexUNITNAME].Value = itemData.UnitName;
                gRow.Cells[indexPRICE].Value = itemData.PRICE;
                gRow.Cells[indexDISCOUNT].Value = itemData.DISCOUNT;
                gRow.Cells[indexNETPRICE].Value = (itemData.PRICE - itemData.DISCOUNT) * itemData.QTY;
                gRow.Cells[indexISVAT].Value = (itemData.ISVAT == Constz.VAT.Included.Code);
                gRow.Cells[indexNORMALDISCOUNT].Value = itemData.DISCOUNT;
                this.grvSupport.Rows.Add(gRow);
            }
            if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
            {
                this.btnSave.Visible = false;
                this.btnSubmit.Visible = false;
                this.btnSearchCustomer.Enabled = false;
                this.grvSupport.Columns[indexSEARCH].Visible = false;
                Appz.FormatDataGridView(this.grvSupport, false, false, true);
            }
        }

        private RequisitionData GetData()
        {
            RequisitionData data = new RequisitionData();
            data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.CODE = this.txtCode.Text.Trim();
            data.CUSTOMER = Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text);
            data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
            data.STATUS = this.txtStatus.Text.Trim();
            data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
            data.TOTDIS = Convert.ToDouble(this.txtTotalDiscount.Text == "" ? "0" : this.txtTotalDiscount.Text);
            data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
            data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
            data.WAREHOUSE = Appz.CurrentUserData.Warehouse;
            data.RESERVEDATE = dtpReserveDate.Value.Date;
            data.REASON = this.txtReason.Text.Trim();
            data.REMARK = this.txtRemark.Text.Trim();

            foreach (DataGridViewRow gRow in this.grvSupport.Rows)
            {
                RequisitionItemData itemData = new RequisitionItemData();
                if (!gRow.IsNewRow)
                {
                    itemData.ACTIVE = Constz.ActiveStatus.Active;
                    itemData.DISCOUNT = Convert.ToDouble(gRow.Cells[indexDISCOUNT].Value);
                    itemData.ISVAT = (Convert.ToBoolean(gRow.Cells[indexISVAT].Value) ? Constz.VAT.Included.Code : Constz.VAT.NotIncluded.Code);
                    itemData.NETPRICE = Convert.ToDouble(gRow.Cells[indexNETPRICE].Value);
                    itemData.PRICE = Convert.ToDouble(gRow.Cells[indexPRICE].Value);
                    itemData.PRODUCT = Convert.ToDouble(gRow.Cells[indexPRODUCT].Value);
                    itemData.QTY = Convert.ToDouble(gRow.Cells[indexQTY].Value);
                    itemData.UNIT = Convert.ToDouble(gRow.Cells[indexUNIT].Value);
                    data.REQUISITIONITEM.Add(itemData);
                }
            }

            return data;
        }

        #endregion

        #region Others

        private void SetNewBarcodeFocus()
        {
            try
            {
                this.grvSupport.CurrentCell = this.grvSupport[indexBARCODE, this.grvSupport.NewRowIndex];
            }
            catch (Exception ex)
            {

            }
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvSupport, true, true, false);
        }

        private void SetDataGridViewImageColumnCellStyle()
        {
            Appz.SetDataGridViewImageColumnCellStyle(SEARCH);
        }

        private void ResetState(double requisition)
        {
            this.txtLOID.Text = requisition.ToString();
            SetData(FlowObj.GetData(requisition));
        }

        private bool UpdateData()
        {
            bool ret = true;
            ret = FlowObj.UpdateData(Appz.CurrentUserData.UserID, GetData());
            if (ret)
            {
                ResetState(FlowObj.LOID);
                Appz.OpenInformationDialog("บันทึกข้อมูลเรียบร้อยแล้ว");
            }
            else
                Appz.OpenErrorDialog(FlowObj.ErrorMessage);
            return ret;
        }

        private bool CommitData()
        {
            bool ret = true;
            ret = FlowObj.CommitData(Appz.CurrentUserData.UserID, GetData());
                if (ret)
                {
                    ResetState(FlowObj.LOID);
                    Appz.OpenInformationDialog("ส่งฝ่ายขายเรียบร้อยแล้ว");
                }
                else
                    Appz.OpenErrorDialog(FlowObj.ErrorMessage);
                return ret;
        }

        private void PrintData()
        {
            ArrayList arr = new ArrayList();
            ReportParameterData data = new ReportParameterData();
            data.PARAMETERNAME = "LOID";
            data.PARAMETERVALUE = this.txtLOID.Text;
            arr.Add(data);
            ABBClient.Reports.PreviewReport _pvReport = new ABBClient.Reports.PreviewReport(Constz.Report.Support, arr);
            _pvReport.ShowDialog();
        }

        private void ResetOrder()
        {
            int index = 1;
            foreach (DataGridViewRow gRow in this.grvSupport.Rows)
            {
                if (!gRow.IsNewRow)
                {
                    gRow.Cells[indexORDERNO].Value = index.ToString();
                    index += 1;
                }
            }
        }

        #endregion

        #region Calculate

        private void CalculateGrandTotal()
        {
            if (this.txtStatus.Text != Constz.Requisition.Status.Waiting.Code) return;
            double vatPercent = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
            double totalVat = 0;
            double discount = 0;
            double total = 0;
            double grandTotal = 0;
            double qty = 0;
            double discountPercent = 0;

            foreach (DataGridViewRow gRow in this.grvSupport.Rows)
            {
                if (!gRow.IsNewRow)
                {
                    if (!Convert.IsDBNull(gRow.Cells[indexQTY].Value)) qty = Convert.ToDouble(gRow.Cells[indexQTY].Value);
                    gRow.Cells[indexDISCOUNT].Value = gRow.Cells[indexNORMALDISCOUNT].Value;
                    if (!Convert.IsDBNull(gRow.Cells[indexDISCOUNT].Value)) discount += Convert.ToDouble(gRow.Cells[indexDISCOUNT].Value) * qty;
                    gRow.Cells[indexNETPRICE].Value = SaleObj.CalcucateProductTotalItem(Convert.ToDouble(gRow.Cells[indexPRICE].Value), qty, Convert.ToDouble(gRow.Cells[indexDISCOUNT].Value));
                    if (!Convert.IsDBNull(gRow.Cells[indexNETPRICE].Value)) grandTotal += Convert.ToDouble(gRow.Cells[indexNETPRICE].Value);
                }
            }

            totalVat = SaleObj.CalculateTotalVat(grandTotal + discount, vatPercent);
            total = grandTotal + discount - totalVat;
            discountPercent = 0;
            if (SaleObj.GetCustomerDiscount(Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text), total + totalVat))
                discountPercent = SaleObj.DISCOUNT;

            if (discountPercent > 0)
            {
                if (Math.Round(discount, 2) < SaleObj.CalcucateDiscount(total + totalVat, discountPercent))
                {
                    totalVat = 0;
                    discount = 0;
                    total = 0;
                    grandTotal = 0;
                    foreach (DataGridViewRow gRow in this.grvSupport.Rows)
                    {
                        if (!gRow.IsNewRow)
                        {
                            if (!Convert.IsDBNull(gRow.Cells[indexQTY].Value)) qty = Convert.ToDouble(gRow.Cells[indexQTY].Value);
                            gRow.Cells[indexDISCOUNT].Value = SaleObj.CalcucateDiscount(Convert.ToDouble(gRow.Cells[indexPRICE].Value), discountPercent);
                            if (!Convert.IsDBNull(gRow.Cells[indexDISCOUNT].Value)) discount += Convert.ToDouble(gRow.Cells[indexDISCOUNT].Value) * qty;
                            gRow.Cells[indexNETPRICE].Value = SaleObj.CalcucateProductTotalItem(Convert.ToDouble(gRow.Cells[indexPRICE].Value), qty, Convert.ToDouble(gRow.Cells[indexDISCOUNT].Value));
                            if (!Convert.IsDBNull(gRow.Cells[indexNETPRICE].Value)) grandTotal += Convert.ToDouble(gRow.Cells[indexNETPRICE].Value);
                        }
                    }

                    totalVat = SaleObj.CalculateTotalVat(grandTotal + discount, vatPercent);
                    total = grandTotal + discount - totalVat;
                }
            }

            this.txtTotal.Text = total.ToString(Constz.DblFormat);
            this.txtTotalDiscount.Text = discount.ToString(Constz.DblFormat);
            this.txtTotalVat.Text = totalVat.ToString(Constz.DblFormat);
            this.txtGrandTotal.Text = grandTotal.ToString(Constz.DblFormat);
        }

        #endregion

        #region Search Product

        private void SearchProduct(string barcode, DataGridViewRow gRow)
        {
            Search.ProductBarcodePopup frmProduct = new ABBClient.Search.ProductBarcodePopup(barcode, Appz.CurrentUserData.Warehouse, Constz.Zone.Z11);
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

            foreach (DataGridViewRow cRow in this.grvSupport.Rows)
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
                    for (int i = 0; i < this.grvSupport.Columns.Count; ++i)
                    {
                        gRow.Cells[i].Value = DBNull.Value;
                    }
                }
                else
                {
                    this.grvSupport.EndEdit();
                    this.grvSupport.Rows.Remove(gRow);
                }

            }
            else
            {
                ProductSaleData data = SaleObj.GetProductPromotion(productID, Appz.CurrentUserData.Warehouse);
                gRow.Cells[indexPRODUCT].Value = data.PRODUCT;
                gRow.Cells[indexUNIT].Value = data.UNIT.ToString();
                gRow.Cells[indexBARCODE].Value = data.BARCODE;
                gRow.Cells[indexNAME].Value = data.PRODUCTNAME;
                gRow.Cells[indexUNITNAME].Value = data.UNITNAME;
                gRow.Cells[indexPRICE].Value = SaleObj.CalculateUnitPrice(data.UNITPRICE, Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text), data.ISVAT);
                gRow.Cells[indexNORMALDISCOUNT].Value = data.DISCOUNT.ToString();
                gRow.Cells[indexNETPRICE].Value = SaleObj.CalcucateProductTotalItem(Convert.ToDouble(gRow.Cells[indexPRICE].Value), Convert.ToDouble(gRow.Cells[indexQTY].Value), data.DISCOUNT);
                gRow.Cells[indexISVAT].Value = (data.ISVAT == Constz.VAT.Included.Code);

                if (gRow.IsNewRow)
                {
                    SetDataGridViewImageColumnCellStyle();

                    DataGridViewRow newRow = (DataGridViewRow)gRow.Clone();
                    newRow.Cells[indexORDERNO].Value = this.grvSupport.Rows.Count.ToString();
                    newRow.Cells[indexBARCODE].Value = gRow.Cells[indexBARCODE].Value;
                    newRow.Cells[indexNORMALDISCOUNT].Value = gRow.Cells[indexNORMALDISCOUNT].Value;
                    newRow.Cells[indexISVAT].Value = gRow.Cells[indexISVAT].Value;
                    newRow.Cells[indexNAME].Value = gRow.Cells[indexNAME].Value;
                    newRow.Cells[indexNETPRICE].Value = gRow.Cells[indexNETPRICE].Value;
                    newRow.Cells[indexPRICE].Value = gRow.Cells[indexPRICE].Value;
                    newRow.Cells[indexPRODUCT].Value = gRow.Cells[indexPRODUCT].Value;
                    newRow.Cells[indexQTY].Value = gRow.Cells[indexQTY].Value;
                    newRow.Cells[indexUNIT].Value = gRow.Cells[indexUNIT].Value;
                    newRow.Cells[indexUNITNAME].Value = gRow.Cells[indexUNITNAME].Value;

                    for (int i = 0; i < this.grvSupport.Columns.Count; ++i)
                    {
                        gRow.Cells[i].Value = DBNull.Value;
                    }

                    if (!Validate(newRow)) return;

                    this.grvSupport.Rows.Add(newRow);//

                }
            }

            CalculateGrandTotal();
        }

        #endregion

        #region Search Customer

        private void SearchCustomer(string customerCode)
        {
            Search.CustomerPopup frmCustomer = new ABBClient.Search.CustomerPopup(customerCode);
            frmCustomer.ShowDialog();
            this.txtCustomer.Text = frmCustomer.CustomerID.ToString();
            SetCustomerDetail(frmCustomer.CustomerID);

            CalculateGrandTotal();
        }

        private void SetCustomerDetail(double customer)
        {
            CustomerSaleData data = SaleObj.GetCustomerData(customer);
            this.txtCustomerCode.Text = data.CODE;
            this.txtCustomerName.Text = data.CUSTOMERNAME;

        }

        #endregion

        #endregion

        #region Events

        #region Others

        private void SupportEdit_Load(object sender, EventArgs e)
        {
            FormatGridView();
            this.grvSupport.Columns[indexSEARCH].DefaultCellStyle.NullValue = null;
            ResetState(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            SearchCustomer("");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Appz.OpenQuestionDialog("ต้องการส่งฝ่ายขายใช่หรือไม่?") == DialogResult.OK)
            {
                CommitData();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintData();
        }

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            if (this.txtCustomerCode.Text.Trim() == "")
            {
                this.txtCustomerName.Text = "";
                this.txtCustomer.Text = "";
                CalculateGrandTotal();
            }
            else
            {
                SearchFlow search = new SearchFlow();
                ABB.Data.Search.SearchCustomerData data = new ABB.Data.Search.SearchCustomerData();
                data.CODE = this.txtCustomerCode.Text.Trim();
                data.FULLNAME = this.txtCustomerName.Text.Trim();
                DataTable dt = search.GetCustomerList(data);
                if (dt.Rows.Count == 1)
                {
                    SetCustomerDetail(Convert.ToDouble(dt.Rows[0]["LOID"]));
                }
                else
                {
                    foreach (DataRow dRow in dt.Rows)
                    {
                        if (dRow["CODE"].ToString() == this.txtCustomerCode.Text.Trim())
                        {
                            if (this.txtCustomer.Text != dRow["LOID"].ToString())
                                SetCustomerDetail(Convert.ToDouble(dRow["LOID"]));
                            return;
                        }
                    }
                    SearchCustomer(this.txtCustomerCode.Text.Trim());
                    if (Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text) <= 0)
                        return;//e.Cancel = true; ##by nang
                }
            }
        }

        private void txtCustomerCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
                this.btnSearchCustomer.Focus();
        }

        #endregion

        #region DataGridView

        private void grvSupport_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.grvSupport.Rows[e.RowIndex].IsNewRow && this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
            {
                if (e.ColumnIndex == indexBARCODE)
                {
                    string barcode = (this.grvSupport.Rows[e.RowIndex].Cells[indexBARCODE].Value == null ? "" : this.grvSupport.Rows[e.RowIndex].Cells[indexBARCODE].Value.ToString().Trim());

                    if (barcode == "")
                    {
                        this.grvSupport.Rows[e.RowIndex].Cells[indexPRODUCT].Value = DBNull.Value;
                        this.grvSupport.Rows[e.RowIndex].Cells[indexUNIT].Value = DBNull.Value;
                        this.grvSupport.Rows[e.RowIndex].Cells[indexBARCODE].Value = DBNull.Value;
                        this.grvSupport.Rows[e.RowIndex].Cells[indexNAME].Value = DBNull.Value;
                        this.grvSupport.Rows[e.RowIndex].Cells[indexQTY].Value = DBNull.Value;
                        this.grvSupport.Rows[e.RowIndex].Cells[indexPRICE].Value = DBNull.Value;
                        this.grvSupport.Rows[e.RowIndex].Cells[indexDISCOUNT].Value = DBNull.Value;
                        this.grvSupport.Rows[e.RowIndex].Cells[indexNETPRICE].Value = DBNull.Value;
                        this.grvSupport.Rows[e.RowIndex].Cells[indexISVAT].Value = DBNull.Value;
                        this.grvSupport.Rows[e.RowIndex].Cells[indexNORMALDISCOUNT].Value = DBNull.Value;
                    }
                    else
                    {
                        if (barcode.Substring(0, 1) == "*")
                        {
                            this.grvSupport.Rows[e.RowIndex].Cells[indexPRODUCT].Value = DBNull.Value;
                            this.grvSupport.Rows[e.RowIndex].Cells[indexUNIT].Value = DBNull.Value;
                            this.grvSupport.Rows[e.RowIndex].Cells[indexBARCODE].Value = DBNull.Value;
                            this.grvSupport.Rows[e.RowIndex].Cells[indexNAME].Value = DBNull.Value;
                            this.grvSupport.Rows[e.RowIndex].Cells[indexQTY].Value = DBNull.Value;
                            this.grvSupport.Rows[e.RowIndex].Cells[indexPRICE].Value = DBNull.Value;
                            this.grvSupport.Rows[e.RowIndex].Cells[indexDISCOUNT].Value = DBNull.Value;
                            this.grvSupport.Rows[e.RowIndex].Cells[indexNETPRICE].Value = DBNull.Value;
                            this.grvSupport.Rows[e.RowIndex].Cells[indexISVAT].Value = DBNull.Value;
                            this.grvSupport.Rows[e.RowIndex].Cells[indexNORMALDISCOUNT].Value = DBNull.Value;

                            if (barcode.Length > 1)
                            {
                                barcode = barcode.Substring(1);
                                this.grvSupport.Rows[e.RowIndex].Cells[indexQTY].Value = Convert.ToDouble(barcode);
                            }
                            this.grvSupport.Rows[e.RowIndex].Cells[indexBARCODE].Value = "";
                            endEditBarcode = false;
                        }
                        else
                        {
                            SearchFlow search = new SearchFlow();
                            ABB.Data.Search.SearchProductData data = new ABB.Data.Search.SearchProductData();
                            data.CODE = barcode.Trim();
                            data.WAREHOUSE = Appz.CurrentUserData.Warehouse;
                            data.TYPE = Constz.ProductType.Type.FG.Code;
                            data.ZONE = Constz.Zone.Z11;
                            DataTable dt = search.GetProductList(data);
                            if (dt.Rows.Count == 1)
                            {
                                this.grvSupport.Rows[e.RowIndex].Cells[indexPRODUCT].Value = dt.Rows[0]["LOID"].ToString();
                                SetProductDetail(this.grvSupport.Rows[e.RowIndex], Convert.ToDouble(dt.Rows[0]["LOID"]));
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
                    if (this.grvSupport.Rows[e.RowIndex].Cells[indexQTY].Value.ToString().Trim() == "") this.grvSupport.Rows[e.RowIndex].Cells[indexQTY].Value = "1";

                    CalculateGrandTotal();
                    SetNewBarcodeFocus();
                }
            }
        }

        private void grvSupport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == indexSEARCH && e.RowIndex >= 0 && this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
            {
                SearchProduct("", this.grvSupport.Rows[e.RowIndex]);
                SetNewBarcodeFocus();
            }
        }

        private bool Validate(DataGridViewRow gRow)
        {
            bool ret = true;
            double product = 0;
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
            else
            {
                if (!FlowObj.CheckProductStock(Appz.CurrentUserData.Warehouse, product, qty))
                {
                    //if (Appz.OpenWarningDialog("จำนวนสินค้าที่ระบุ เกินจำนวนที่มีอยู่ในสต็อก ต้องการทำรายการต่อหรือไม่") == DialogResult.Cancel)
                    Appz.OpenErrorDialog("จำนวนสินค้าที่ระบุ เกินจำนวนที่มีอยู่ในสต็อก");
                    ret = false;
                }
            }
            return ret;
        }

        private void grvSupport_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!this.grvSupport.Rows[e.RowIndex].IsNewRow && this.grvSupport[indexORDERNO, e.RowIndex].Value != null)
            {
                if (endEditBarcode)
                {
                    e.Cancel = !Validate(this.grvSupport.Rows[e.RowIndex]);
                }
                else
                    e.Cancel = true;
                endEditBarcode = true;
            }
        }

        private void grvSupport_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ResetOrder();
            SetDataGridViewImageColumnCellStyle();
        }

        private void grvSupport_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            CalculateGrandTotal();
            ResetOrder();
        }

        private void grvSupport_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl txtBox = (DataGridViewTextBoxEditingControl)e.Control;
                txtBox.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                if (this.grvSupport.CurrentCell.OwningColumn.Index == indexQTY)
                {
                    txtBox.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                }
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        #endregion

        #endregion

    }
}