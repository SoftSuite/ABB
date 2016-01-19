using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.Data.Handheld.Common.StockIn;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Handheld.FG;

namespace ABB.Flow.Handheld.FG
{
    public class StockInPDFlow
    {
        private string _error = "";
        private StockInPDDAL _pdDAL;
        private StockInDAL _dal;
        private StockInItemDAL _dalItem;
        private double _LOID = 0;

        public string ErrorMessage
        {
            get { return _error; }
        }

        private StockInPDDAL PDDALObj
        {
            get { if ( _pdDAL == null) { _pdDAL = new StockInPDDAL(); } return _pdDAL; }
        }

        private StockInDAL DALObj
        {
            get { if (_dal == null) { _dal = new StockInDAL(); } return _dal; }
        }

        private StockInItemDAL DALitemObj
        {
            get { if ( _dalItem == null) { _dalItem = new StockInItemDAL(); } return _dalItem; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public virtual DataTable GetStockInPDList(double docType)
        {
            return PDDALObj.GetStockInPDList(docType);
        }

        public DataTable GetProductList(double stockIn)
        {
            return PDDALObj.GetProductList(stockIn);
        }

        public StockInProductData GetPrductData(string barCode, string produceType)
        {
            StockInProductData data = new StockInProductData();
            DataTable dt = PDDALObj.GetProductDataByBarcode(barCode, produceType);
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
        public double GetPDProduct(double product, string lotno)
        {
            double refloid = 0;
            DataTable dt = PDDALObj.GetPDProduct(product, lotno);
            if (dt.Rows.Count == 0)
            {
                refloid = 0;
            }
            else
            {
                refloid = Convert.ToDouble(dt.Rows[0]["LOID"]);

            }
           
            return refloid;
        }
        public StockInProductDetailData GetProductDetail(double stockInItem)
        {
            StockInProductDetailData data = new StockInProductDetailData();
            DataTable dt = PDDALObj.GetProductDetail(stockInItem);
            if (dt.Rows.Count == 1)
            {
                data.STOCKIN = Convert.ToDouble(dt.Rows[0]["STOCKIN"]);
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.LOTNO = dt.Rows[0]["LOTNO"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["MFGDATE"])) data.MFGDATE = Convert.ToDateTime(dt.Rows[0]["MFGDATE"]);
                data.PRODUCTNAME = dt.Rows[0]["NAME"].ToString();
                data.QTY = Convert.ToDouble(dt.Rows[0]["QTY"]);
                data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
            }
            return data;
        }

        public StockInData GetData(double loid)
        {
            StockInData data = new StockInData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.ACCCODE = DALObj.ACCCODE;
                data.APPROVEDATE = DALObj.APPROVEDATE;
                data.APPROVER = DALObj.APPROVER;
                data.CODE = DALObj.CODE;
                data.DOCTYPE = DALObj.DOCTYPE;
                data.GRANDTOT = DALObj.GRANDTOT;
                data.INVNO = DALObj.INVNO;
                data.QCCODE = DALObj.QCCODE;
                data.QCDATE = DALObj.QCDATE;
                data.REASON = DALObj.REASON;
                data.RECEIVEDATE = DALObj.RECEIVEDATE;
                data.RECEIVER = DALObj.RECEIVER;
                data.REMARK = DALObj.REMARK;
                data.SENDER = DALObj.SENDER;
                data.STATUS = DALObj.STATUS;
            }
            return data;
        }

        public bool DeleteStockIn(double loid)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ret = DALitemObj.DeleteDataByStockIn(loid, obj.zTrans);
                if (!ret) throw new ApplicationException(DALitemObj.ErrorMessage);
                DALObj.GetDataByLOID(loid, obj.zTrans);
                ret = DALObj.DeleteCurrentData(obj.zTrans);
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

        public bool DeleteStockInItem(double loid)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALitemObj.GetDataByLOID(loid, obj.zTrans);
                ret = DALitemObj.DeleteCurrentData(obj.zTrans);
                if (!ret) throw new ApplicationException(DALitemObj.ErrorMessage);

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

        public bool SubmitStockIn(string userID, double loid)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (DALObj.GetDataByLOID(loid, obj.zTrans))
                {
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Approved.Code;

                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = DALitemObj.UpdateStatusByStockIn(loid, DALObj.STATUS, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALitemObj.ErrorMessage);

                        ret = DALObj.CutStock(loid, userID, obj.zTrans);
                    }
                    obj.zTrans.Commit();
                    obj.CloseConnection();
                }
                else
                {
                    throw new ApplicationException(DALObj.ErrorMessage);
                }
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

        public bool InsertStockIn(string userID, StockInData data)
        {
            bool ret = true;
            DALObj.ACCCODE = data.ACCCODE;
            DALObj.APPROVEDATE = data.APPROVEDATE;
            DALObj.APPROVER = data.APPROVER;
            DALObj.CODE = data.CODE;
            DALObj.DOCTYPE = data.DOCTYPE;
            DALObj.GRANDTOT = data.GRANDTOT;
            DALObj.INVNO = data.INVNO;
            DALObj.QCCODE = data.QCCODE;
            DALObj.QCDATE = data.QCDATE;
            DALObj.REASON = data.REASON;
            DALObj.RECEIVEDATE = data.RECEIVEDATE;
            DALObj.RECEIVER = data.RECEIVER;
            DALObj.REMARK = data.REMARK;
            DALObj.SENDER = data.SENDER;
            DALObj.STATUS = data.STATUS;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ret = DALObj.InsertCurrentData(userID, obj.zTrans);
                _LOID = DALObj.LOID;

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

        private bool VerifyData(StockInItemData data)
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

        public bool InsertStockInItem(string userID,StockInItemData data)
        {
            bool ret = true;
            if (VerifyData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALitemObj.GetData(data.STOCKIN, data.PRODUCT, data.REFLOID, data.REFTABLE, obj.zTrans);
                    DALitemObj.LOTNO = data.LOTNO;
                    DALitemObj.PRICE = data.PRICE;
                    DALitemObj.PRODUCT = data.PRODUCT;
                    DALitemObj.REFLOID = data.REFLOID;
                    DALitemObj.REFTABLE = data.REFTABLE;
                    DALitemObj.STATUS = data.STATUS;
                    DALitemObj.STOCKIN = data.STOCKIN;
                    DALitemObj.UNIT = data.UNIT;
                    if (DALitemObj.OnDB)
                    {
                        DALitemObj.QCQTY += data.QTY;
                        ret = DALitemObj.UpdateCurrentData(userID, obj.zTrans);
                    }
                    else
                    {
                        DALitemObj.QTY = data.QTY;
                        ret = DALitemObj.InsertCurrentData(userID, obj.zTrans);
                    }
                    if (!ret) throw new ApplicationException(DALitemObj.ErrorMessage);

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
                ret = false;
            return ret;
        }

    }
}
