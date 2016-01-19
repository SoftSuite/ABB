using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.Data.Handheld.Common.StockIn;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Handheld.WH;

namespace ABB.Flow.Handheld.WH
{
    public class StockInPDFlow : ABB.Flow.Handheld.FG.StockInPDFlow
    {
        private StockInPDDAL _pdDAL;

        private StockInPDDAL PDDALObj
        {
            get { if (_pdDAL == null) { _pdDAL = new StockInPDDAL(); } return _pdDAL; }
        }

        public override DataTable GetStockInPDList(double docType)
        {
            return PDDALObj.GetStockInPDList(docType);
        }

        public StockInProductData GetWHPrductData(string barCode)
        {
            StockInProductData data = new StockInProductData();
            DataTable dt = PDDALObj.GetProductDataByBarcode(barCode);
            if (dt.Rows.Count == 1)
            {
                data.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
                data.NAME = dt.Rows[0]["NAME"].ToString();
                data.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
                data.PRICE = Convert.ToDouble(dt.Rows[0]["PRICE"]);
            }
            return data;
        }

        public StockInProductDetailData GetWHProductDetail(double stockInItem)
        {
            StockInProductDetailData data = new StockInProductDetailData();
            DataTable dt = PDDALObj.GetProductDetail(stockInItem);
            if (dt.Rows.Count == 1)
            {
                data.STOCKIN = Convert.ToDouble(dt.Rows[0]["STOCKIN"]);
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.LOTNO = dt.Rows[0]["LOTNO"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["APPROVEDATE"])) data.APPROVEDATE = Convert.ToDateTime(dt.Rows[0]["APPROVEDATE"]);
                data.PRODUCTNAME = dt.Rows[0]["NAME"].ToString();
                data.QTY = Convert.ToDouble(dt.Rows[0]["QTY"]);
                data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
            }
            return data;
        }
    }
}
