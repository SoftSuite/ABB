using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.DAL.Search;
using ABB.Data.Search;
using ABB.Data.Sales;
using ABB.Data.Inventory.FG;

namespace ABB.Flow.Search
{
    public class SearchFlow
    {
        public DataTable GetCustomerList(SearchCustomerData data)
        {
            return SearchDAL.GetCustomerList(data);
        }

        public DataTable GetCustRetProductList(SearchCustomerData data)
        {
            return SearchDAL.GetCustRetProductList(data);
        }

        public DataTable GetSupplierList(SearchCustomerData data)
        {
            return SearchDAL.GetSupplierList(data);
        }

        public DataTable GetProductList(SearchProductData data)
        {
            return SearchDAL.GetProductList(data);
        }

        public DataTable GetProductMasterList(SearchProductData data)
        {
            return SearchDAL.GetProductMasterList(data);
        }

        public DataTable GetSaleList(SearchSaleData data)
        {
            return SearchDAL.GetSaleList(data);
        }
        public DataTable GetSaleList2(SearchSaleData data)
        {
            return SearchDAL.GetSaleList2(data);
        }
        public DataTable GetReserveList(ProductReserveSearchData data)
        {
            return SearchDAL.GetReserveList(data);
        }
        public DataTable GetInvList(SearchSaleData data)
        {
            return SearchDAL.GetInvList(data);
        }

        public DataTable GetRequisitionList(StockoutSearchData data)
        {
            return SearchDAL.GetRequisitionList(data);
        }

        public DataTable GetProductionList(StockoutSearchData data)
        {
            return SearchDAL.GetProductionList(data);
        }

        public DataTable GetReqProductionList(StockoutSearchData data)
        {
            return SearchDAL.GetReqProductionList(data);
        }

        public DataTable GetOrderProductList(StockInFGData data)
        {
            return SearchDAL.GetOrderProductList(data);
        }

        public DataTable GetProductPRList (PopupProductPRSearchData data)
        {
            return SearchDAL.GetProductPRList(data);
        }

        public DataTable GetPOList(PopupPOSearchData data)
        {
            return SearchDAL.GetPOList(data);
        }

        public DataTable GetProductReturnList(PopupStockoutSearchData data)
        {
            return SearchDAL.GetProductReturnList(data);
        }

        public DataTable GetProductWHList(StockInFGData data)
        {
            return SearchDAL.GetProductWHList(data);
        }

        public DataTable GetProductOTList(StockInFGData data)
        {
            return SearchDAL.GetProductOTList(data);
        }

        public DataTable GetBasketList(PopupStockoutBasketData data)
        {
            return SearchDAL.GetBasketList(data);
        }

        public DataTable GetStockinReturnList(PopupStockinReturnData data)
        {
            return SearchDAL.GetStockinReturnList(data);
        }
        public DataTable GetStockinReturnWHList(PopupStockinReturnData data)
        {
            return SearchDAL.GetStockinReturnWHList(data);
        }
        public DataTable GetStockinReturnSearchList(PopupStockinReturnSearchData data)
        {
            return SearchDAL.GetStockinReturnSearchList(data);
        }

        public DataTable GetProductPlanList(SearchProductPlanData data)
        {
            return SearchDAL.GetProductPlanList(data);
        }

        public DataTable GetInvoiceList(SearchInvoiceData data)
        {
            return SearchDAL.GetInvoiceList(data);
        }

        public DataTable GetInvoiceRequestList(InvoiceRequestSearchData data, double currentInvoice)
        {
            return SearchDAL.GetInvoiceRequest(data, currentInvoice);
        }

        public DataTable GetProductReserveList(SearchProductData data)
        {
            return SearchDAL.GetProductReserveList(data);
        }

        public DataTable GetProductShopList(SearchProductData data)
        {
            return SearchDAL.GetProductShopList(data);
        }

        public DataTable GetProductPRList(SearchProductData data)
        {
            return SearchDAL.GetProductPRList(data);
        }

    }
}
