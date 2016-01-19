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
    public partial class Sales : Form
    {

        public Sales()
        {
            InitializeComponent();
            SetDataGridViewImageColumnCellStyle();
        }

        #region Variables

        private bool endEditBarcode = true;
        private PointOfSaleFlow _flow;
        private SaleFlow _sFlow;
        private App_Code.POSPrinter _print;
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
        private int indexREFREQUISITIONITEM = 13;
        private int indexMINQTY = 14;
        private int indexISDISCOUNT = 15;
        private int indexISEDIT = 16;

        #endregion

        #region Properties

        public PointOfSaleFlow FlowObj
        {
            get { if (_flow == null) { _flow = new PointOfSaleFlow(); } return _flow; }
        }

        public SaleFlow SaleObj
        {
            get { if (_sFlow == null) { _sFlow = new SaleFlow(); } return _sFlow; }
        }

        public App_Code.POSPrinter PrintObj
        {
            get { if (_print == null) { _print = new ABBClient.App_Code.POSPrinter(); } return _print; }
        }

        #endregion

        #region Methods

        #region Others

        private void SetNewBarcodeFocus()
        {
            try
            {
                this.grvSales.Focus();
                this.grvSales.CurrentCell = this.grvSales[indexBARCODE, this.grvSales.NewRowIndex];
                this.grvSales.CurrentCell.Selected = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void FormatGridView()
        {
            Appz.FormatDataGridView(this.grvSales, true, true, false);
            //this.grvSales.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        private void SetDataGridViewImageColumnCellStyle()
        {
            Appz.SetDataGridViewImageColumnCellStyle(SEARCH);
        }

        private void ResetState(double requisition)
        {
            this.chkUseMemberDiscount.Checked = false;
            this.txtCreateBy.Text = Appz.CurrentUserData.UserID;
            this.txtVat.Text = ABB.Flow.SysConfigFlow.GetValue(Constz.ConfigName.VAT);
            this.grvSales.Rows.Clear();

            this.txtCustomerCode.ReadOnly = false;
            this.btnSearchCustomer.Enabled = true;

            if (requisition == 0)
            {
                btnNew.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnPrint.Enabled = true;
                btnPay.Enabled = true;
            }
            else
            {
                btnNew.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnPrint.Enabled = true;
                btnPay.Enabled = false;
            }

            this.lblCode.Text = "";
            this.txtRefCode.Text = "";
            this.txtCustomerCode.Text = "";
            this.txtCustomerName.Text = "";
            this.lblNetAmount.Text = "0";
            this.txtRefLOID.Text = "";
            this.txtLOID.Text = requisition.ToString();
            this.txtCustomer.Text = "4261";
            SetCustomerDetail(Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text));
            this.txtDiscountPercent.Text = "0";
            this.txtTotal.Text = "0";
            this.txtTotalDiscount.Text = "0";
            this.txtTotalVat.Text = "0";
            this.txtGrandTotal.Text = "0";

            this.txtCash.Text = "0";
            this.txtCoupon.Text = "0";
            this.txtCreditCardID.Text = "";
            this.txtCreditCardPay.Text = "0";
            this.txtCreditCardType.Text = "0";
            this.txtCustomerCode.Focus();
        }

        private PointOfSaleData GetData()
        {
            PointOfSaleData data = new PointOfSaleData();
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.CASH = Convert.ToDouble(this.txtCash.Text == "" ? "0" : this.txtCash.Text);
            data.CODE = this.lblCode.Text.Trim();
            data.COUPON = Convert.ToDouble(this.txtCoupon.Text == "" ? "0" : this.txtCoupon.Text);
            data.CREDITCARDID = this.txtCreditCardID.Text.Trim();
            data.CREDITCARDPAY = Convert.ToDouble(this.txtCreditCardPay.Text == "" ? "0" : this.txtCreditCardPay.Text);
            data.CREDITTYPE = Convert.ToDouble(this.txtCreditCardType.Text == "" ? "0" : this.txtCreditCardType.Text);
            data.CUSTOMER = Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text);
            data.GRANDTOT = Convert.ToDouble(this.txtGrandTotal.Text == "" ? "0" : this.txtGrandTotal.Text);
            data.INVCODE = data.CODE;
            data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
            data.REFLOID = Convert.ToDouble(this.txtRefLOID.Text == "" ? "0" : this.txtRefLOID.Text);
            data.REFNO = this.txtRefCode.Text.Trim();
            data.REFTABLE = "REQUISITION";
            data.STATUS = Constz.Requisition.Status.Approved.Code;
            data.TOTAL = Convert.ToDouble(this.txtTotal.Text == "" ? "0" : this.txtTotal.Text);
            data.TOTDIS = Convert.ToDouble(this.txtTotalDiscount.Text == "" ? "0" : this.txtTotalDiscount.Text);
            data.TOTVAT = Convert.ToDouble(this.txtTotalVat.Text == "" ? "0" : this.txtTotalVat.Text);
            data.VAT = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
            data.WAREHOUSE = Appz.CurrentUserData.Warehouse;
            data.REQDATE = DateTime.Now;
            data.USEMEMBERDISCOUNT = (chkUseMemberDiscount.Checked ? Constz.UseMemberDiscount.Yes : Constz.UseMemberDiscount.No);

            foreach (DataGridViewRow gRow in this.grvSales.Rows)
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

        private bool SaveData(bool SetInvoice)
        {
            bool ret = true;
            ret = FlowObj.UpdateData(Appz.CurrentUserData.UserID, GetData(), SetInvoice);
            if (ret)
                this.txtLOID.Text = FlowObj.LOID.ToString();
            else
                Appz.OpenErrorDialog(FlowObj.ErrorMessage);
            return ret;
        }

        private void PrintData()
        {
            try
            {
                PrintObj.Print(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
            }
            catch (Exception ex)
            {
                Appz.OpenErrorDialog(ex.Message);
            }
        }

        private void ResetOrder()
        {
            int index = 1;
            foreach (DataGridViewRow gRow in this.grvSales.Rows)
            {
                if (!gRow.IsNewRow)
                {
                    gRow.Cells[indexORDERNO].Value = index.ToString();
                    index += 1;
                }
            }
        }

        private void Print()
        {
            if (Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text) == 0)
                Appz.OpenErrorDialog("กรุณาระบุลูกค้า");
            else if (Convert.ToDouble(this.lblNetAmount.Text == "" ? "0" : this.lblNetAmount.Text) > 0)
            {
                SalePayData data = new SalePayData();
                data.CASH = Convert.ToDouble(this.lblNetAmount.Text);
                data.GRANDTOTAL = data.CASH;

                Transaction.SalesSummary frmSummary = new Transaction.SalesSummary(data);
                if (frmSummary.ShowDialog() == DialogResult.OK)
                {
                    this.txtCash.Text = frmSummary.Data.CASH.ToString(Constz.DblFormat);
                    this.txtCoupon.Text = frmSummary.Data.COUPON.ToString(Constz.DblFormat);
                    this.txtCreditCardID.Text = frmSummary.Data.CREDITCARDID;
                    this.txtCreditCardPay.Text = frmSummary.Data.CREDITCARDPAY.ToString(Constz.DblFormat);
                    this.txtCreditCardType.Text = frmSummary.Data.CREDITTYPE.ToString();

                    if (SaveData(true))
                    {
                        SaleInvoice frmInvoice = new SaleInvoice(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
                        //ReportParameterData rdata = new ReportParameterData();
                        //rdata.PARAMETERNAME = "LOID";
                        //rdata.PARAMETERVALUE = this.txtLOID.Text;
                        //ArrayList arr = new ArrayList();
                        //arr.Add(rdata);
                        //Reports.PreviewReport pvReport = new ABBClient.Reports.PreviewReport(Constz.Report.Invoice, arr);
                        ResetState(0);
                        SetNewBarcodeFocus();
                        frmInvoice.Show();
                    }
                }
            }
        }

        private void Pay()
        {
            if (Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text) == 0)
                Appz.OpenErrorDialog("กรุณาระบุลูกค้า");
            else if (Convert.ToDouble(this.lblNetAmount.Text == "" ? "0" : this.lblNetAmount.Text) > 0)
            {
                SalePayData data = new SalePayData();
                data.CASH = Convert.ToDouble(this.lblNetAmount.Text);
                data.GRANDTOTAL = data.CASH;

                Transaction.SalesSummary frmSummary = new Transaction.SalesSummary(data);
                if (frmSummary.ShowDialog() == DialogResult.OK)
                {
                    this.txtCash.Text = frmSummary.Data.CASH.ToString(Constz.DblFormat);
                    this.txtCoupon.Text = frmSummary.Data.COUPON.ToString(Constz.DblFormat);
                    this.txtCreditCardID.Text = frmSummary.Data.CREDITCARDID;
                    this.txtCreditCardPay.Text = frmSummary.Data.CREDITCARDPAY.ToString(Constz.DblFormat);
                    this.txtCreditCardType.Text = frmSummary.Data.CREDITTYPE.ToString();

                    if (SaveData(false))
                    {
                        PrintData();
                        ResetState(0);
                        SetNewBarcodeFocus();
                    }
                }
            }
        }

        #endregion

        #region Calculate

        private void CalculateGrandTotal()
        {
            double vatPercent = Convert.ToDouble(this.txtVat.Text == "" ? "0" : this.txtVat.Text);
            double totalVat = 0;
            double discount = 0;
            double total = 0;
            double grandTotal = 0;
            double qty = 0;
            bool calculateDiscount = false;

            foreach (DataGridViewRow gRow in this.grvSales.Rows)
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

            this.txtDiscountPercent.Text = "0";
            if (SaleObj.GetCustomerDiscount(Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text), total + totalVat))
                this.txtDiscountPercent.Text = SaleObj.DISCOUNT.ToString();

            if (!chkUseMemberDiscount.Checked)
            {
                if (SaleObj.GetPromotionDiscount(Appz.CurrentUserData.Warehouse, total + totalVat))
                {
                    if (Convert.ToDouble(this.txtDiscountPercent.Text == "" ? "0" : this.txtDiscountPercent.Text) < SaleObj.DISCOUNT)
                        this.txtDiscountPercent.Text = SaleObj.DISCOUNT.ToString();
                    calculateDiscount = true;
                }
            }
            else
            {
                calculateDiscount = true;
                discount = 0;
            }

            //if (SaleObj.GetDiscount(Appz.CurrentUserData.Warehouse,  Convert.ToDouble(this.txtCustomer.Text == "" ? "0" : this.txtCustomer.Text), total + totalVat, (chkUseMemberDiscount.Checked ? Constz.UseMemberDiscount.Yes : Constz.UseMemberDiscount.No)))
            //    this.txtDiscountPercent.Text = SaleObj.DISCOUNT.ToString();

            if (calculateDiscount)
            {
                if (Math.Round(discount, 2) < SaleObj.CalcucateDiscount(total + totalVat, Convert.ToDouble(this.txtDiscountPercent.Text)) || chkUseMemberDiscount.Checked)
                {
                    totalVat = 0;
                    discount = 0;
                    total = 0;
                    grandTotal = 0;
                    foreach (DataGridViewRow gRow in this.grvSales.Rows)
                    {
                        if (!gRow.IsNewRow)
                        {
                            if (!Convert.IsDBNull(gRow.Cells[indexQTY].Value)) qty = Convert.ToDouble(gRow.Cells[indexQTY].Value);
                            if (gRow.Cells[indexISDISCOUNT].Value.ToString() == Constz.Discount.Calculated.Code)
                                gRow.Cells[indexDISCOUNT].Value = SaleObj.CalcucateDiscount(Convert.ToDouble(gRow.Cells[indexPRICE].Value), Convert.ToDouble(this.txtDiscountPercent.Text == "" ? "0" : this.txtDiscountPercent.Text));
                            else
                                gRow.Cells[indexDISCOUNT].Value = 0;
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
            this.lblNetAmount.Text = Convert.ToDouble(this.txtGrandTotal.Text).ToString(Constz.IntFormat);
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

            foreach (DataGridViewRow cRow in this.grvSales.Rows)
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
                    for (int i = 0; i < this.grvSales.Columns.Count; ++i)
                    {
                        gRow.Cells[i].Value = DBNull.Value;
                    }
                }
                else
                {
                    this.grvSales.EndEdit();
                    this.grvSales.Rows.Remove(gRow);
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
                gRow.Cells[indexISDISCOUNT].Value = data.ISDISCOUNT;
                gRow.Cells[indexISEDIT].Value = data.ISEDIT;

                if (gRow.IsNewRow)
                {
                    SetDataGridViewImageColumnCellStyle();

                    DataGridViewRow newRow = (DataGridViewRow)gRow.Clone();
                    newRow.Cells[indexORDERNO].Value = this.grvSales.Rows.Count.ToString();
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
                    newRow.Cells[indexISDISCOUNT].Value = gRow.Cells[indexISDISCOUNT].Value;
                    newRow.Cells[indexISEDIT].Value = gRow.Cells[indexISEDIT].Value;

                    for (int i = 0; i < this.grvSales.Columns.Count; ++i)
                    {
                        gRow.Cells[i].Value = DBNull.Value;
                    }

                    if (!Validate(newRow)) return;
                    
                    this.grvSales.Rows.Add(newRow);//

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
        }

        private void SetCustomerDetail(double customer)
        {
            CustomerSaleData data = SaleObj.GetCustomerData(customer);
            this.txtCustomer.Text = customer.ToString();
            this.txtCustomerCode.Text = data.CODE;
            this.txtCustomerName.Text = data.CUSTOMERNAME;

            CalculateGrandTotal();
            SetNewBarcodeFocus();
        }

        #endregion

        #region Search Reference Requisition

        private void SearchRefRequisition(string refCode)
        {
            Search.SalePopup frmSale = new ABBClient.Search.SalePopup();
            if (frmSale.ShowDialog() == DialogResult.OK)
            {
                this.txtRefLOID.Text = frmSale.RequisitionID.ToString();
                SetRefRequisition(frmSale.RequisitionID);
            }
        }

        private void SetRefRequisition(double refRequisition)
        {
            this.grvSales.Rows.Clear();
            PointOfSaleRefData data = FlowObj.GetRefRequisitionItem(refRequisition);
            for (int i = 0; i < data.REFITEM.Count; ++i)
            {
                PointOfSaleRefItemData itemData = (PointOfSaleRefItemData)data.REFITEM[i];
                DataGridViewRow gRow = (DataGridViewRow)this.grvSales.Rows[this.grvSales.NewRowIndex].Clone();
                gRow.Cells[indexPRODUCT].Value = itemData.PRODUCT;
                gRow.Cells[indexUNIT].Value = itemData.UNIT;
                gRow.Cells[indexORDERNO].Value = i+1;
                gRow.Cells[indexBARCODE].Value = itemData.BARCODE;
                gRow.Cells[indexNAME].Value = itemData.NAME;
                gRow.Cells[indexQTY].Value = itemData.QTY;
                gRow.Cells[indexUNITNAME].Value = itemData.UNITNAME;
                gRow.Cells[indexPRICE].Value = itemData.PRICE;
                gRow.Cells[indexDISCOUNT].Value = itemData.DISCOUNT;
                gRow.Cells[indexNETPRICE].Value = (itemData.PRICE - itemData.DISCOUNT) * itemData.QTY;
                gRow.Cells[indexISVAT].Value = (itemData.ISVAT == Constz.VAT.Included.Code);
                gRow.Cells[indexNORMALDISCOUNT].Value = itemData.DISCOUNT;
                gRow.Cells[indexREFREQUISITIONITEM].Value = itemData.REFREQUISITIONITEM;
                gRow.Cells[indexMINQTY].Value = itemData.QTY;
                gRow.Cells[indexISDISCOUNT].Value = itemData.ISDISCOUNT;
                gRow.Cells[indexISEDIT].Value = itemData.ISEDIT;
                this.grvSales.Rows.Add(gRow);
            }
            this.txtCustomerCode.ReadOnly = true;
            this.btnSearchCustomer.Enabled = false;
            this.txtRefCode.Text = data.REQUISITIONCODE;
            this.txtCustomer.Text = data.CUSTOMER.ToString();
            SetCustomerDetail(data.CUSTOMER);
        }

        #endregion

        #endregion

        #region Events

        private void Sales_Load(object sender, EventArgs e)
        {
            FormatGridView();
            this.grvSales.Columns[indexSEARCH].DefaultCellStyle.NullValue = null;
            this.dtpReqDate.Value = DateTime.Now.Date;
            ResetState(0);
            SetNewBarcodeFocus();
        }

        #region Others

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            SearchCustomer("");
        }

        private void txtVat_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ResetState(0);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetState(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(false);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            Pay();
        }

        private void btnSearchRef_Click(object sender, EventArgs e)
        {
            SearchRefRequisition("");
        }

        private void txtRefCode_Leave(object sender, EventArgs e)
        {
            if (this.txtRefCode.Text.Trim() != "")
            {
                SearchFlow search = new SearchFlow();
                ABB.Data.Search.SearchSaleData data = new ABB.Data.Search.SearchSaleData();
                data.CODEFROM = this.txtRefCode.Text.Trim();
                data.CODETO = data.CODETO;
                DataTable dt = search.GetSaleList2(data);
                if (dt.Rows.Count == 1)
                {
                    this.txtRefLOID.Text = dt.Rows[0]["LOID"].ToString();
                    SetRefRequisition(Convert.ToDouble(this.txtRefLOID.Text));
                }
                else
                {
                    Appz.OpenErrorDialog("ไม่พบเลขที่ใบเสร็จที่อ้างอิง");
                }
                SetNewBarcodeFocus();
            }
            else
            {
                if (Convert.ToDouble(this.txtRefLOID.Text == "" ? "0" : this.txtRefLOID.Text) >0)
                    ResetState(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
            }
        }

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            if (this.txtCustomerCode.Text.Trim() == "")
            {
                this.txtCustomerName.Text = "";
                this.txtCustomer.Text = "";
                this.txtDiscountPercent.Text = "0";
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
                    {
                        this.btnSearchCustomer.Focus();
                        return;//e.Cancel = true; ##by nang
                    }
                    else
                        SetNewBarcodeFocus();
                }
            }
        }

        private void txtCustomerCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
                SetNewBarcodeFocus();
                //this.btnSearchCustomer.Focus();
        }

        private void Sales_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.txtLOID.Text == "")
            {
                if (Appz.OpenQuestionDialog("ต้องการยกเลิกการทำรายการนี้ใช่หรือไม่") != DialogResult.OK)
                    e.Cancel = true;
            }
        }

        #endregion

        #region DataGridView

        private void grvSales_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.grvSales.Rows[e.RowIndex].IsNewRow)
            {
                if (e.ColumnIndex == indexBARCODE)
                {
                    string barcode = (this.grvSales.Rows[e.RowIndex].Cells[indexBARCODE].Value == null ? "" : this.grvSales.Rows[e.RowIndex].Cells[indexBARCODE].Value.ToString().Trim());

                    if (barcode == "")
                    {
                        this.grvSales.Rows[e.RowIndex].Cells[indexPRODUCT].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexUNIT].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexBARCODE].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexNAME].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexQTY].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexPRICE].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexDISCOUNT].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexNETPRICE].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexISVAT].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexNORMALDISCOUNT].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexISDISCOUNT].Value = DBNull.Value;
                        this.grvSales.Rows[e.RowIndex].Cells[indexISEDIT].Value = DBNull.Value;
                    }
                    else
                    {
                        if (barcode.Substring(0, 1) == "*")
                        {
                            this.grvSales.Rows[e.RowIndex].Cells[indexPRODUCT].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexUNIT].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexBARCODE].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexNAME].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexQTY].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexPRICE].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexDISCOUNT].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexNETPRICE].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexISVAT].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexNORMALDISCOUNT].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexISDISCOUNT].Value = DBNull.Value;
                            this.grvSales.Rows[e.RowIndex].Cells[indexISEDIT].Value = DBNull.Value;

                            if (barcode.Length > 1)
                            {
                                barcode = barcode.Substring(1);
                                this.grvSales.Rows[e.RowIndex].Cells[indexQTY].Value = Convert.ToDouble(barcode);
                            }
                            this.grvSales.Rows[e.RowIndex].Cells[indexBARCODE].Value = "";
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
                                this.grvSales.Rows[e.RowIndex].Cells[indexPRODUCT].Value = dt.Rows[0]["LOID"].ToString();
                                SetProductDetail(this.grvSales.Rows[e.RowIndex], Convert.ToDouble(dt.Rows[0]["LOID"]));
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
                    if (this.grvSales.Rows[e.RowIndex].Cells[indexQTY].Value.ToString().Trim() == "") this.grvSales.Rows[e.RowIndex].Cells[indexQTY].Value = "1";

                    CalculateGrandTotal();
                    SetNewBarcodeFocus();
                }
                else if (e.ColumnIndex == indexPRICE)
                {
                    CalculateGrandTotal();
                    SetNewBarcodeFocus();
                }
            }
        }

        private void grvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == indexSEARCH && e.RowIndex >=0)
            {
                double refRequisitionitem = 0;
                if (!this.grvSales.Rows[e.RowIndex].IsNewRow)
                {
                    if (!Convert.IsDBNull(this.grvSales[indexREFREQUISITIONITEM, e.RowIndex].Value))
                    {
                        if (this.grvSales[indexREFREQUISITIONITEM, e.RowIndex].Value != null)  refRequisitionitem = Convert.ToDouble(this.grvSales[indexREFREQUISITIONITEM, e.RowIndex].Value.ToString() == "" ? "0" : this.grvSales[indexREFREQUISITIONITEM, e.RowIndex].Value);
                    }
                }
                if (refRequisitionitem > 0)
                {
                    Appz.OpenErrorDialog("ไม่สามารถเปลี่ยนสินค้าได้ กรุณาเพิ่มรายการสินค้าใหม่");
                }
                else
                {
                    SearchProduct("", this.grvSales.Rows[e.RowIndex]);
                    SetNewBarcodeFocus();
                }
            }
        }

        private bool Validate(DataGridViewRow gRow)
        {
            bool ret = true;
            double product = 0;
            double minQty = 0;
            double qty = 0;
            if (!Convert.IsDBNull(gRow.Cells[indexMINQTY].Value))
                if (gRow.Cells[indexMINQTY].Value != null) minQty = Convert.ToDouble(gRow.Cells[indexMINQTY].Value.ToString() == "" ? "0" : gRow.Cells[indexMINQTY].Value);
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
            else if (minQty > 0 && minQty > qty)
            {
                Appz.OpenErrorDialog("จำนวนสินค้า ต้องไม่น้อยกว่า " + minQty.ToString() + " หน่วย");
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

        private void grvSales_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!this.grvSales.Rows[e.RowIndex].IsNewRow && this.grvSales[indexORDERNO,e.RowIndex].Value != null)
            {
                if (endEditBarcode)
                {
                    e.Cancel = !Validate(this.grvSales.Rows[e.RowIndex]);
                }
                else
                    e.Cancel = true;
                endEditBarcode = true;
            }
        }

        private void grvSales_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ResetOrder();
            SetDataGridViewImageColumnCellStyle();
        }

        private void grvSales_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            CalculateGrandTotal();
            ResetOrder();
        }

        private void grvSales_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == indexBARCODE & !this.grvSales.Rows[e.RowIndex].IsNewRow)
            {
                double refRequisitionitem = 0;
                if (!Convert.IsDBNull(this.grvSales[indexREFREQUISITIONITEM, e.RowIndex].Value))
                {
                    if (this.grvSales[indexREFREQUISITIONITEM, e.RowIndex].Value != null) refRequisitionitem = Convert.ToDouble(this.grvSales[indexREFREQUISITIONITEM, e.RowIndex].Value.ToString() == "" ? "0" : this.grvSales[indexREFREQUISITIONITEM, e.RowIndex].Value);
                }
                if (refRequisitionitem > 0)
                {
                    e.Cancel = true;
                    Appz.OpenErrorDialog("ไม่สามารถเปลี่ยนสินค้าได้ กรุณาเพิ่มรายการสินค้าใหม่");
                }
            }
            else if (e.ColumnIndex == indexPRICE && (Convert.IsDBNull(this.grvSales.Rows[e.RowIndex].Cells[indexISEDIT].Value) ? "N" : this.grvSales.Rows[e.RowIndex].Cells[indexISEDIT].Value.ToString()) == "N")
            {
                e.Cancel = true;
                Appz.OpenErrorDialog("ไม่สามารถแก้ไขราคาได้");
            }
        }

        private void grvSales_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl txtBox = (DataGridViewTextBoxEditingControl)e.Control;
                txtBox.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                txtBox.KeyPress -= new KeyPressEventHandler(ControlPrice_KeyPress);
                if (this.grvSales.CurrentCell.OwningColumn.Index == indexQTY)
                {
                    txtBox.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                }
                else if (this.grvSales.CurrentCell.OwningColumn.Index == indexPRICE)
                {
                    txtBox.KeyPress += new KeyPressEventHandler(ControlPrice_KeyPress);
                }
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetIntTextBox(sender, e);
        }

        private void ControlPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            ControlUtil.SetDBlTextBox(sender, e);
        }

        #endregion

        private void chkUseMemberDiscount_CheckedChanged(object sender, EventArgs e)
        {
            CalculateGrandTotal();
        }

        #endregion

        private void Sales_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F9 :
                    Pay();
                    e.Handled = true;
                    break;

                case Keys.F10:
                    Print();
                    e.Handled = true;
                    break;
            }
        }

    }
}