using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.DAL;
using ABB.DAL.Handheld;
using ABB.Data;
using ABB.Data.Handheld.Common;

namespace ABB.Flow.Handheld
{
    public class StockCheckBatchFlow
    {
        #region Variables and Properties

        private StockCheckBatchDAL _sdal;
        private StockCheckDAL _dal;
        private StockCheckItemDAL _dalItem;
        private string _error = "";
        private ProductSearchData _data;

        private StockCheckBatchDAL SDALObj
        {
            get { if (_sdal == null) { _sdal = new StockCheckBatchDAL(); } return _sdal; }
        }

        private StockCheckDAL DALObj
        {
            get { if (_dal == null) { _dal = new StockCheckDAL(); } return _dal; }
        }

        private StockCheckItemDAL DALItemObj
        {
            get { if (_dalItem == null) { _dalItem = new StockCheckItemDAL(); } return _dalItem; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public ProductSearchData ProductData
        {
            get { if (_data == null) { _data = new ProductSearchData(); } return _data; }
        }

        #endregion

        public string GetLocationName(double location)
        {
            LocationDAL loDAL = new LocationDAL();
            loDAL.GetDataByLOID(location, null);
            return loDAL.NAME;
        }

        public DataTable GetStockCheckist(double warehouse)
        {
            return SDALObj.GetStockCheckList(warehouse);
        }

        public DataTable GetStockCheckItemList(double stockCheck, double zone, string createBy)
        {
            return SDALObj.GetStockCheckItemList(stockCheck, zone, createBy);
        }

        public bool GetProductData(string barcode, double warehouse, double location)
        {
            bool ret = true;
            DataTable dt = SDALObj.GetProductStock(barcode, warehouse, location);
            if (dt.Rows.Count == 1)
            {
                ProductData.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                ProductData.LOID = Convert.ToDouble(dt.Rows[0]["PRODUCT"]);
                ProductData.NAME = dt.Rows[0]["NAME"].ToString();
                ProductData.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                ProductData.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
            }
            else
            {
                ret = false;
                _error = "ไม่พบสินค้าในสต็อก";
            }

            return ret;
        }

        public StockCheckBatchData GetStockCheckData(double stockCheck)
        {
            StockCheckBatchData data = new StockCheckBatchData();
            DataTable dt = SDALObj.GetStockCheckData(stockCheck);
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                data.BATCHNO = dRow["BATCHNO"].ToString();
                data.CHECKDATE = Convert.ToDateTime(dRow["CHECKDATE"]);
                data.STOCKCHECK = Convert.ToDouble(dRow["LOID"]);
                data.WAREHOUSENAME = dRow["WAREHOUSENAME"].ToString();
            }
            return data;
        }

        public StockCheckBatchItemData GetStockCheckItemData(double stockCheckItem)
        {
            StockCheckBatchItemData data = new StockCheckBatchItemData();
            DataTable dt = SDALObj.GetStockCheckItemData(stockCheckItem);
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                data.BATCHNO = dRow["BATCHNO"].ToString();
                data.CHECKDATE = Convert.ToDateTime(dRow["CHECKDATE"]);
                data.STOCKCHECK = Convert.ToDouble(dRow["STOCKCHECK"]);
                data.WAREHOUSENAME = dRow["WAREHOUSENAME"].ToString();
                data.LOCATION = Convert.ToDouble(dRow["LOCATION"]);
                data.LOCATIONNAME = dRow["LOCATIONNAME"].ToString();
                data.PRODUCTNAME = dRow["PRODUCTNAME"].ToString();
                data.LOTNO = dRow["LOTNO"].ToString();
                data.UNITNAME = dRow["UNITNAME"].ToString();
                data.COUNTQTY = Convert.ToDouble(dRow["COUNTQTY"]);
            }
            return data;
        }

        public bool DeleteStockCheckItem(double loid)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALItemObj.GetDataByLOID(loid, obj.zTrans);
                ret = DALItemObj.DeleteCurrentData(obj.zTrans);
                if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);

                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        private bool VerifyData(StockCheckItemData data)
        {
            bool ret = true;
            if (data.PRODUCT == 0)
            {
                ret = false;
                _error = "กรุณาระบุสินค้า";
            }
            else if (data.LOTNO == "")
            {
                ret = false;
                _error = "กรุณาเลือก Lot No";
            }
            else if (data.COUNTQTY == 0)
            {
                ret = false;
                _error = "กรุณาระบุจำนวนที่นับได้";
            }
            return ret;
        }

        public bool insertStockCheckItem(string userID, StockCheckItemData data)
        {
            bool ret = true;
            if (VerifyData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALItemObj.OnDB = false;
                    DALItemObj.COUNTQTY = data.COUNTQTY;
                    DALItemObj.LOTNO = data.LOTNO;
                    DALItemObj.PRODUCT = data.PRODUCT;
                    DALItemObj.STOCKCHECK = data.STOCKCHECK;
                    DALItemObj.LOCATION = data.LOCATION;

                    ret = DALItemObj.InsertCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);

                    obj.zTrans.Commit();
                    obj.CloseConnection();
                }
                catch (Exception ex)
                {
                    obj.zTrans.Rollback();
                    obj.CloseConnection();
                    ret = false;
                    _error = ex.Message;
                }
            }
            else
            {
                ret = false;
            }
            return ret;
        }

        public bool UpdateStockCheckStatus(string userID, double stockCheck, string status)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(stockCheck, obj.zTrans);
                DALObj.STATUS = status;

                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

    }
}
