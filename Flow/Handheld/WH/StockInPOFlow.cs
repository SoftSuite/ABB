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
    public class StockInPOFlow
    {
        private StockInPODAL _pdDAL;
        private StockInDAL _dal;
        private StockInItemDAL _DAL;
        private string _error = "";
        private double _LOID = 0;

        private StockInPODAL PODALObj
        {
            get { if (_pdDAL == null) { _pdDAL = new StockInPODAL(); } return _pdDAL; }
        }

        private StockInDAL DALObj
        {
            get { if (_dal == null) { _dal = new StockInDAL(); } return _dal; }
        }

        private StockInItemDAL DALitemObj
        {
            get { if (_DAL == null) { _DAL = new StockInItemDAL(); } return _DAL; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        #region Before QC

        public DataTable GetStockInPOList(double docType, string status)
        {
            return PODALObj.GetStockInPOList(docType, status);
        }

        private bool ValidateData(StockInData data)
        {
            bool ret = true;
            if (data.INVNO.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุเลขที่ใบส่งของ";
            }
            else if (data.SENDER == 0)
            {
                ret = false;
                _error = "กรุณาเลือกผู้จำหน่าย";
            }
            return ret;
        }

        public bool InsertStockIn(string userID, StockInData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                DALObj.ACCCODE = data.ACCCODE;
                DALObj.APPROVEDATE = data.APPROVEDATE;
                DALObj.APPROVER = data.APPROVER;
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
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    _LOID = DALObj.LOID;
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

        public StockInPOData GetStockInPOData(double loid)
        {
            StockInPOData data = new StockInPOData();
            DataTable dt = PODALObj.GetStockInPOData(loid);
            if (dt.Rows.Count == 1)
            {
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.INVNO = dt.Rows[0]["INVNO"].ToString();
                data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
                data.SENDER = Convert.ToDouble(dt.Rows[0]["SENDER"]);
                data.STATUS = dt.Rows[0]["STATUS"].ToString();
                data.SUPPLIERNAME = dt.Rows[0]["SUPPLIERNAME"].ToString();
            }
            return data;
        }

        public StockInPOData GetStockInPOData(double loid, double PDOrder)
        {
            StockInPOData data = new StockInPOData();
            data.PDORDER = PDOrder;
            DataTable dt = PODALObj.GetStockInPOData(loid);
            if (dt.Rows.Count == 1)
            {
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.INVNO = dt.Rows[0]["INVNO"].ToString();
                data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
                data.SENDER = Convert.ToDouble(dt.Rows[0]["SENDER"]);
                data.STATUS = dt.Rows[0]["STATUS"].ToString();
                data.SUPPLIERNAME = dt.Rows[0]["SUPPLIERNAME"].ToString();
            }
            PDOrderDAL _orderDAL = new PDOrderDAL();
            if (_orderDAL.GetDataByLOID(PDOrder, null))
            {
                data.ORDERCODE = _orderDAL.CODE;
            }
            return data;
        }

        public StockInProductData GetProductData(string barCode, double PDOrder)
        {
            StockInProductData data = new StockInProductData();
            DataTable dt = PODALObj.GetProductDataByBarcode(barCode, PDOrder);
            if (dt.Rows.Count == 1)
            {
                data.REFLOID = Convert.ToDouble(dt.Rows[0]["REFLOID"]);
                data.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
                data.NAME = dt.Rows[0]["NAME"].ToString();
                data.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
                data.PRICE = Convert.ToDouble(dt.Rows[0]["PRICE"]);
            }
            return data;
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
                _error = "กรุณาระบุจำนวนรับ";
            }
            else if (data.QCQTY == 0)
            {
                ret = false;
                _error = "กรุณาระบุจำนวนส่ง QC";
            }
            else if (data.QCQTY > data.QTY)
            {
                _error = "จำนวนส่ง QC ต้องไม่เกินจำนวนรับ";
            }
            return ret;
        }

        public bool InsertStockInItem(string userID, StockInItemData data)
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
                        DALitemObj.QCQTY += data.QCQTY;
                        DALitemObj.QTY += data.QTY;
                        ret = DALitemObj.UpdateCurrentData(userID, obj.zTrans);
                    }
                    else
                    {
                        DALitemObj.QCQTY = data.QCQTY;
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

        public DataTable GetProductList(double stockIn, double PDOrder)
        {
            return PODALObj.GetProductList(stockIn, PDOrder);
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

        public bool ReceiveStockIn(string userID, double loid)
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
                        DALObj.STATUS = Constz.Requisition.Status.QC.Code;
                        DALObj.QCCODE = OracleDB.GetRunningCode("STOCKIN_QC", DALObj.DOCTYPE.ToString(), obj.zTrans);
                        DALObj.QCDATE = DateTime.Today;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = DALitemObj.UpdateStatusByStockIn(loid, DALObj.STATUS, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALitemObj.ErrorMessage);

                        ret = DALObj.CutStock(loid, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = PODALObj.UpdatePDOrderReceiveQty(DALObj.LOID, obj.zTrans);
                        if (!ret) throw new ApplicationException(PODALObj.ErrorMessage);
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

        public bool QCStockIn(string userID, double loid)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (DALObj.GetDataByLOID(loid, obj.zTrans))
                {
                    if (DALObj.STATUS == Constz.Requisition.Status.Approved.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Finish.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = DALitemObj.UpdateStatusByStockIn(loid, DALObj.STATUS, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALitemObj.ErrorMessage);

                        ret = DALObj.CutStock(loid, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

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

        public StockInPOProductDetailData GetProductPODetail(double stockInItem)
        {
            StockInPOProductDetailData data = new StockInPOProductDetailData();
            DataTable dt = PODALObj.GetProductDetail(stockInItem);
            if (dt.Rows.Count == 1)
            {
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.INVNO = dt.Rows[0]["INVNO"].ToString();
                data.LOID = Convert.ToDouble(dt.Rows[0]["STOCKIN"]);
                data.NAME = dt.Rows[0]["NAME"].ToString();
                data.ORDERCODE = dt.Rows[0]["ORDERCODE"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["ORDERDATE"])) data.ORDERDATE = Convert.ToDateTime(dt.Rows[0]["ORDERDATE"]);
                data.ORDERQTY = Convert.ToDouble(dt.Rows[0]["ORDERQTY"]);
                data.QTY = Convert.ToDouble(dt.Rows[0]["QTY"]);
                data.QCQTY = Convert.ToDouble(dt.Rows[0]["QCQTY"]);
                data.SUPPLIERNAME = dt.Rows[0]["SUPPLIERNAME"].ToString();
                data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
            }
            return data;
        }

        #endregion

        #region After QC

        public DataTable GetQCProductList(double stockIn)
        {
            return PODALObj.GetQCProductList(stockIn);
        }

        public StockInQCData GetStockInQCData(double loid)
        {
            StockInQCData data = new StockInQCData();
            DataTable dt = PODALObj.GetStockInQCData(loid);
            if (dt.Rows.Count == 1)
            {
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
                data.STATUS = dt.Rows[0]["STATUS"].ToString();
                data.QCCODE = dt.Rows[0]["QCCODE"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["RECEIVEDATE"])) data.RECEIVEDATE = Convert.ToDateTime(dt.Rows[0]["RECEIVEDATE"]);
            }
            return data;
        }

        public StockInQCProductDetailData GetProductQCData(double stockInItem)
        {
            StockInQCProductDetailData data = new StockInQCProductDetailData();
            DataTable dt = PODALObj.GetQCProductDetail(stockInItem);
            if (dt.Rows.Count == 1)
            {
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.LOID = Convert.ToDouble(dt.Rows[0]["STOCKIN"]);
                data.NAME = dt.Rows[0]["NAME"].ToString();
                data.QCCODE = dt.Rows[0]["QCCODE"].ToString();
                data.QCQTY = Convert.ToDouble(dt.Rows[0]["QCQTY"]);
                if (!Convert.IsDBNull(dt.Rows[0]["QTY"])) data.QTY = Convert.ToDouble(dt.Rows[0]["QTY"]);
                if (!Convert.IsDBNull(dt.Rows[0]["RECEIVEDATE"])) data.RECEIVEDATE = Convert.ToDateTime(dt.Rows[0]["RECEIVEDATE"]);
                data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
            }
            return data;
        }

        public bool CancelStockInItemQC(string userID, double stockInItem)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALitemObj.GetDataByLOID(stockInItem, obj.zTrans);
                DALitemObj.QTY = 0;
                ret = DALitemObj.UpdateCurrentData(userID, obj.zTrans);
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

        public bool UpdateStockInItemQty(string userID, double stockInItem, double qty)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALitemObj.GetDataByLOID(stockInItem, obj.zTrans);
                DALitemObj.QTY += qty;
                ret = DALitemObj.UpdateCurrentData(userID, obj.zTrans);
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

        public StockInProductData GetProductQCData(string barCode, double stocIn)
        {
            StockInProductData data = new StockInProductData();
            DataTable dt = PODALObj.GetQCProductByBarcode(barCode, stocIn);
            if (dt.Rows.Count == 1)
            {
                data.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
                data.NAME = dt.Rows[0]["NAME"].ToString();
                data.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
                data.QTY = Convert.ToDouble(dt.Rows[0]["QTY"]);
            }
            return data;
        }

        #endregion

    }
}
