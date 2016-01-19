using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using ABB.Data;
using ABB.Flow.Common;

namespace ABB.Global
{
    public class ComboSource
    {
        #region BuildCombo

        public static void BuildCombo(DropDownList zCombo, string TableName, string TextFiledName, string ValueFieldName, string OrderFieldName, string WhereString)
        {
            BuildCombo(zCombo, TableName, TextFiledName, ValueFieldName, OrderFieldName, WhereString, null, null);
        }

        public static void BuildCombo(DropDownList zCombo, string TableName, string TextFiledName, string ValueFieldName, string OrderFieldName, string WhereString, string BlankText, string BlankValue)
        {
            ComboSourceFlow cmbFlow = new ComboSourceFlow();
            DataTable dt = cmbFlow.GetSource(TableName, TextFiledName, ValueFieldName, OrderFieldName, WhereString);
            if (dt != null)
            {
                zCombo.Items.Clear();
                zCombo.DataSource = dt;
                zCombo.DataTextField = TextFiledName;
                zCombo.DataValueField = ValueFieldName;
                zCombo.DataBind();

                if (BlankText != null || BlankValue != null)
                {
                    ListItem itm = new ListItem(BlankText, BlankValue);
                    zCombo.Items.Insert(0, itm);
                }

            }
        }

        #endregion

        #region BuildComboDistinct

        public static void BuildComboDistinct(DropDownList zCombo, string TableName, string TextFiledName, string ValueFieldName, string OrderFieldName, string WhereString)
        {
            BuildComboDistinct(zCombo, TableName, TextFiledName, ValueFieldName, OrderFieldName, WhereString, null, null);
        }

        public static void BuildComboDistinct(DropDownList zCombo, string TableName, string TextFiledName, string ValueFieldName, string OrderFieldName, string WhereString, string BlankText, string BlankValue)
        {
            ComboSourceFlow cmbFlow = new ComboSourceFlow();
            DataTable dt = cmbFlow.GetSourceDistinct(TableName, TextFiledName, ValueFieldName, OrderFieldName, WhereString);
            if (dt != null)
            {
                zCombo.Items.Clear();
                zCombo.DataSource = dt;
                zCombo.DataTextField = TextFiledName;
                zCombo.DataValueField = ValueFieldName;
                zCombo.DataBind();

                if (BlankText != null || BlankValue != null)
                {
                    ListItem itm = new ListItem(BlankText, BlankValue);
                    zCombo.Items.Insert(0, itm);
                }

            }
        }

        #endregion

        #region BuldStatus [ Requisition , StockIn , Invoice]

        #region BuildStockInStatusRankCombo

        public static void BuildStockInStatusRankCombo(DropDownList zCombo)
        {
            BuildStockInStatusRankCombo(zCombo, null, null);
        }

        public static void BuildStockInStatusRankCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.QC.Name, Constz.Requisition.Status.QC.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Finish.Name, Constz.Requisition.Status.Finish.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildStockInStatusCombo

        public static void BuildStockInStatusCombo(DropDownList zCombo)
        {
            BuildStockInStatusCombo(zCombo, null, null);
        }

        public static void BuildStockInStatusCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.QC.Name, Constz.Requisition.Status.QC.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Finish.Name, Constz.Requisition.Status.Finish.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Code);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildStockinReturnStatusCombo

        public static void BuildStockinReturnStatusCombo(DropDownList zCombo)
        {
            BuildStockinReturnStatusCombo(zCombo, null, null);
        }

        public static void BuildStockinReturnStatusCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.StockinReturn.Status.Waiting.Name, Constz.StockinReturn.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.StockinReturn.Status.Approved.Name, Constz.StockinReturn.Status.Approved.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.StockinReturn.Status.Void.Name, Constz.StockinReturn.Status.Void.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildStockinReturnWHStatusCombo

        public static void BuildStockinReturnWHStatusCombo(DropDownList zCombo)
        {
            BuildStockinReturnWHStatusCombo(zCombo, null, null);
        }

        public static void BuildStockinReturnWHStatusCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Finish.Name, Constz.Requisition.Status.Finish.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildInvoiceStatusRankCombo

        public static void BuildInvoiceStatusRankCombo(DropDownList zCombo)
        {
            BuildInvoiceStatusRankCombo(zCombo, null, null);
        }

        public static void BuildInvoiceStatusRankCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();

            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Reserve.Name, Constz.Requisition.Status.Reserve.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildInvoiceStatusCombo

        public static void BuildInvoiceStatusCombo(DropDownList zCombo)
        {
            BuildInvoiceStatusCombo(zCombo, null, null);
        }

        public static void BuildInvoiceStatusCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();

            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Reserve.Name, Constz.Requisition.Status.Reserve.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Code);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildStatusRankCombo

        public static void BuildStatusRankCombo(DropDownList zCombo)
        {
            BuildStatusRankCombo(zCombo, null, null);
        }
        public static void BuildSTStatusRankCombo(DropDownList zCombo)
        {
            BuildSTStatusRankCombo(zCombo, null, null);
        }
        public static void BuildStatusRankComboReturn(DropDownList zCombo)
        {
            BuildStatusRankComboReturn(zCombo, null, null);
        }
        public static void BuildStatusRankComboQC(DropDownList zCombo)
        {
            BuildStatusRankComboQC(zCombo, null, null);
        }
        public static void BuildStatusRankComboPDReserver(DropDownList zCombo)
        {
            BuildStatusRankComboPDReserve(zCombo, null, null);
        }
        public static void BuildStatusRankBasketCombo(DropDownList zCombo)
        {
            BuildStatusRankBasketCombo(zCombo, null, null);
        }

        public static void BuildStatusPRRankCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem();
            item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.SP.Name, Constz.Requisition.Status.SP.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }
        public static void BuildStatusRankCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }
        public static void BuildSTStatusRankCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.QC.Name, Constz.Requisition.Status.QC.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Finish.Name, Constz.Requisition.Status.Finish.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }
        public static void BuildStatusRankComboReturn(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.ApproveWH.Name, Constz.Requisition.Status.ApproveWH.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }
        public static void BuildStatusRankComboQC(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.SendQC.Name, Constz.Requisition.Status.SendQC.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.ReturnQC.Name, Constz.Requisition.Status.ReturnQC.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }
        public static void BuildStatusRankComboPDReserve(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.DoWaiting.Name, Constz.Requisition.Status.DoWaiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.SendWareHouse.Name, Constz.Requisition.Status.SendWareHouse.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }
        public static void BuildStatusRankBasketCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Basket.Status.Waiting.Name, Constz.Basket.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Basket.Status.Approved.Name, Constz.Basket.Status.Approved.Rank);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildStatusCombo

        public static void BuildStatusCombo(DropDownList zCombo)
        {
            BuildStatusCombo(zCombo, null, null);
        }

        public static void BuildStatusCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Code);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildStatusReturnRankCombo

        public static void BuildStatusReturnRankCombo(DropDownList zCombo)
        {
            BuildStatusReturnRankCombo(zCombo, null, null);
        }

        public static void BuildStatusReturnRankCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Rank);
            zCombo.Items.Add(item);


            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildStatusReturnCombo

        public static void BuildStatusReturnCombo(DropDownList zCombo)
        {
            BuildStatusReturnCombo(zCombo, null, null);
        }

        public static void BuildStatusReturnCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Code);
            zCombo.Items.Add(item);


            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #endregion

        #region BuildCustomerTypeCombo

        public static void BuildCustomerTypeCombo(DropDownList zComb)
        {
            BuildCustomerTypeCombo(zComb, null, null);
        }

        public static void BuildCustomerTypeCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.CustomerType.Personal.Name, Constz.CustomerType.Personal.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.CustomerType.Company.Name, Constz.CustomerType.Company.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.CustomerType.Government.Name, Constz.CustomerType.Government.Code);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildPaymentTypeCombo

        public static void BuildPaymentTypeCombo(DropDownList zComb)
        {
            BuildPaymentTypeCombo(zComb, null, null);
        }

        public static void BuildPaymentTypeCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.Payment.Cash.Name, Constz.Payment.Cash.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Payment.CreditCard.Name, Constz.Payment.CreditCard.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.Payment.Credit.Name, Constz.Payment.Credit.Code);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildDeliveryTypeCombo

        public static void BuildDeliveryTypeCombo(DropDownList zComb)
        {
            BuildDeliveryTypeCombo(zComb, null, null);
        }

        public static void BuildDeliveryTypeCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.DeliveryType.Undefined.Name, Constz.DeliveryType.Undefined.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.DeliveryType.Customer.Name, Constz.DeliveryType.Customer.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.DeliveryType.Car.Name, Constz.DeliveryType.Car.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.DeliveryType.TransferCompany.Name, Constz.DeliveryType.TransferCompany.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.DeliveryType.Mail.Name, Constz.DeliveryType.Mail.Code);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

        #region BuildOrderTypeCombo

        public static void BuildOrderTypeCombo(DropDownList zComb)
        {
            BuildOrderTypeCombo(zComb, null, null);
        }

        public static void BuildOrderTypeCombo(DropDownList zCombo, string BlankText, string BlankValue)
        {
            zCombo.Items.Clear();
            ListItem item = new ListItem(Constz.OrderType.PO.Name, Constz.OrderType.PO.Code);
            zCombo.Items.Add(item);
            item = new ListItem(Constz.OrderType.PD.Name, Constz.OrderType.PD.Code);
            zCombo.Items.Add(item);

            if (BlankText != null || BlankValue != null)
            {
                item = new ListItem(BlankText, BlankValue);
                zCombo.Items.Insert(0, item);
            }
        }

        #endregion

    }
}
