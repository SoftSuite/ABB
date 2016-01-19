using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.DAL;
using ABB.DAL.Handheld.WH;
using ABB.Data;
using ABB.Data.Handheld.WH;
using ABB.Data.Handheld.Common;

namespace ABB.Flow.Handheld.WH
{
    public class StockOutWHFlow
    {
        #region Variables and Properties

        private StockOutWHDAL _sdal;
        private StockOutDAL _dal;
        private StockOutItemDAL _dalItem;
        private string _error = "";
        private ProductSearchData _data;
        private double _LOID = 0;

        private StockOutWHDAL SODALObj
        {
            get { if (_sdal == null) { _sdal = new StockOutWHDAL(); } return _sdal; }
        }

        private StockOutDAL DALObj
        {
            get { if (_dal == null) { _dal = new StockOutDAL(); } return _dal; }
        }

        private StockOutItemDAL DALItemObj
        {
            get { if (_dalItem == null) { _dalItem = new StockOutItemDAL(); } return _dalItem; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public ProductSearchData ProductData
        {
            get { if (_data == null) { _data = new ProductSearchData(); } return _data; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        #endregion

        public DataTable GetStockOutList()
        {
            return SODALObj.GetStockOutList();
        }

        public DataTable GetStockOutItemList(double stockOut)
        {
            return SODALObj.GetStockOutItemList(stockOut);
        }

        public DataTable GetProductStock(double product, double stockOut)
        {
            return SODALObj.GetProductStock(product, stockOut);
        }

        public bool GetProductData(double stockout, string barcode)
        {
            bool ret = true;
            DataTable dt = SODALObj.GetProductDetail(stockout, barcode);
            if (dt.Rows.Count == 1)
            {
                if (GetProductStock(Convert.ToDouble(dt.Rows[0]["PRODUCT"]), stockout).Rows.Count == 0)
                {
                    ret = false;
                    _error = "ไม่พบสินค้าในสต็อก";
                }
                else
                {
                    ProductData.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                    ProductData.LOID = Convert.ToDouble(dt.Rows[0]["PRODUCT"]);
                    ProductData.NAME = dt.Rows[0]["NAME"].ToString();
                    ProductData.REFLOID = Convert.ToDouble(dt.Rows[0]["REFLOID"]);
                    ProductData.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                    ProductData.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
                    ProductData.PRICE = Convert.ToDouble(dt.Rows[0]["PRICE"]);
                }
            }
            else
            {
                ret = false;
                _error = "ไม่พบสินค้าในใบขอเบิก";
            }

            return ret;
        }

        public StockOutWHData GetStockOutData(double stockOut)
        {
            StockOutWHData data = new StockOutWHData();
            DataTable dt = SODALObj.GetStockOutData(stockOut);
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                data.CODE = dRow["CODE"].ToString();
                data.DOCNAME = dRow["DOCNAME"].ToString();
                data.REQCODE = dRow["REQCODE"].ToString();
                data.STOCKOUT = Convert.ToDouble(dRow["LOID"]);
                data.ORDERLOTNO = dRow["ORDERLOTNO"].ToString();
            }
            return data;
        }

        public StockOutItemWHData GetStockOutItemData(double stockOutItem)
        {
            StockOutItemWHData data = new StockOutItemWHData();
            DataTable dt = SODALObj.GetStockOutItemData(stockOutItem);
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                data.CODE = dRow["CODE"].ToString();
                data.DOCNAME = dRow["DOCNAME"].ToString();
                data.REQCODE = dRow["REQCODE"].ToString();
                data.STOCKOUT = Convert.ToDouble(dRow["STOCKOUT"]);
                data.ORDERLOTNO = dRow["ORDERLOTNO"].ToString();
                data.LOTNO = dRow["LOTNO"].ToString();
                data.PRODUCTNAME = dRow["NAME"].ToString();
                data.QTY = Convert.ToDouble(dRow["QTY"]);
                data.UNITNAME = dRow["UNITNAME"].ToString();
            }
            return data;
        }

        public bool DeleteStockOutItem(double loid)
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

        public bool NewStockOut(string userID, double requisition, double sender, double receiver)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                RequisitionDAL rDAL = new RequisitionDAL();
                rDAL.GetDataByLOID(requisition, obj.zTrans);
                if (rDAL.OnDB)
                {
                    DocTypeDAL dDAL = new DocTypeDAL();
                    dDAL.GetDataByRequisitionType(rDAL.REQUISITIONTYPE, obj.zTrans);

                    DALObj.OnDB = false;
                    DALObj.ACTIVE = Constz.ActiveStatus.Active;
                    DALObj.DOCTYPE = dDAL.LOID;
                    DALObj.RECEIVER = receiver;
                    DALObj.REFLOID = rDAL.LOID;
                    DALObj.REFTABLE = "REQUISITION";
                    DALObj.REQDATE = DateTime.Today;
                    DALObj.SENDER = sender;
                    DALObj.STATUS = Constz.Requisition.Status.Waiting.Code;

                    ret = DALObj.InsertCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    _LOID = DALObj.LOID;
                }
                else
                {
                    throw new ApplicationException("ไม่พบข้อมูลคำขอ");
                }

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

        private bool VerifyData(StockOutItemData data)
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
            else if (data.QTY == 0)
            {
                ret = false;
                _error = "กรุณาระบุจำนวน";
            }
            return ret;
        }

        public bool UpdateStockOutItem(string userID, StockOutItemData data)
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
                    DALItemObj.GetData(data.STOCKOUT, data.PRODUCT, data.REFLOID, data.REFTABLE, data.LOTNO, obj.zTrans);
                    DALItemObj.ACTIVE = data.ACTIVE;
                    DALItemObj.LOTNO = data.LOTNO;
                    DALItemObj.PRICE = data.PRICE;
                    DALItemObj.PRODUCT = data.PRODUCT;
                    DALItemObj.REFLOID = data.REFLOID;
                    DALItemObj.REFTABLE = data.REFTABLE;
                    DALItemObj.STATUS = data.STATUS;
                    DALItemObj.STOCKOUT = data.STOCKOUT;
                    DALItemObj.UNIT = data.UNIT;
                    DALItemObj.REMAIN = DALItemObj.GetRemainQTYStock(data.LOTNO, data.PRODUCT, obj.zTrans);

                    if (DALItemObj.OnDB)
                    {
                        DALItemObj.QTY += data.QTY;
                        ret = DALItemObj.UpdateCurrentData(userID, obj.zTrans);
                    }
                    else
                    {
                        DALItemObj.QTY = data.QTY;
                        ret = DALItemObj.InsertCurrentData(userID, obj.zTrans);
                    }

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
            else ret = false;
            return ret;
        }

        public bool UpdateStockOutStatus(string userID, double stockOut, string status)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(stockOut, obj.zTrans);
                DALObj.STATUS = status;

                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                RequisitionDAL rDAL = new RequisitionDAL();
                rDAL.GetDataByLOID(DALObj.REFLOID, obj.zTrans);
                rDAL.ACTIVE = Constz.ActiveStatus.InActive;
                if (rDAL.OnDB)
                    ret = rDAL.UpdateCurrentData(userID, obj.zTrans);
                if (!ret) throw new ApplicationException(rDAL.ErrorMessage);

                ret = DALItemObj.UpdateStatusByStockOut(stockOut, status, userID, obj.zTrans);
                if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);

                PDProductDAL pdDAL = new PDProductDAL();
                pdDAL.GetDataByLOID(rDAL.REFLOID, obj.zTrans);

                PDOrderDAL poDAL = new PDOrderDAL();
                poDAL.GetDataByLOID(pdDAL.PDORDER, obj.zTrans);
                poDAL.STATUS = Constz.Requisition.Status.RW.Code;
                ret = poDAL.UpdateCurrentData(userID, obj.zTrans);
                if (!ret) throw new ApplicationException(poDAL.ErrorMessage);

                ret = DALObj.CutStock(stockOut, userID, obj.zTrans);
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
