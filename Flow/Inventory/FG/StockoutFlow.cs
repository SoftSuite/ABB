using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Sales;
using ABB.Data.Inventory.FG;
using ABB.DAL;
using ABB.DAL.Inventory;
using ABB.Flow.Admin;
using ABB.Flow.Sales;

namespace ABB.Flow.Inventory.FG
{
    public class StockoutFlow
    {
        private string _error = "";
        private double _LOID = 0;
        private RequisitionDAL _reqDAL;
        private StockOutDAL _DAL;
        private StockOutItemDAL _itemDAL;
        private StockFGDAL _sDAL;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public RequisitionDAL ReqDAL
        {
            get { if (_reqDAL == null) { _reqDAL = new RequisitionDAL(); } return _reqDAL; }
        }

        public StockOutDAL DALObj
        {
            get { if (_DAL == null) { _DAL = new StockOutDAL(); } return _DAL; }
        }

        public StockOutItemDAL DALItemObj
        {
            get { if (_itemDAL == null) { _itemDAL = new StockOutItemDAL(); } return _itemDAL; }
        }

        public StockFGDAL StockSearchDAL
        {
            get { if (_sDAL == null) { _sDAL = new StockFGDAL(); } return _sDAL; }
        }

        public DataTable GetStockOutItemList(double stockOut)
        {
            return DALItemObj.GetDataList("STOCKOUT = " + stockOut.ToString(), null);
        }

        public DataTable GetStockOutList(StockOutFGSearchData data)
        {
            DataTable dt = StockSearchDAL.GetStockOutList(data);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["NO"] = i;
                i += 1;
            }
            return dt;
        }

        public DataTable GetReturnList(StockoutSearchData data)
        {
            return StockSearchDAL.GetReturnList(data);
        }

        public DataTable GetReturnWHList(StockoutSearchData data)
        {
            return StockSearchDAL.GetReturnWHList(data);
        }

        public StockOutFGReqData GetRequisitionData(double requisition)
        {
            StockOutFGReqData data = new StockOutFGReqData();
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ReqDAL.GetDataByLOID(requisition, obj.zTrans);
                data.CADDRESS = ReqDAL.CADDRESS;
                data.CFAX = ReqDAL.CFAX;
                data.CLASTNAME = ReqDAL.CLASTNAME;
                data.CNAME = ReqDAL.CNAME;
                data.CTEL = ReqDAL.CTEL;
                data.CTITLE = ReqDAL.CTITLE;
                data.CUSTOMER = ReqDAL.CUSTOMER;
                CustomerDAL cDAL = new CustomerDAL();
                cDAL.GetDataByLOID(data.CUSTOMER, obj.zTrans);
                data.CUSTOMERCODE = cDAL.CODE;
                data.CUSTOMERNAME = (cDAL.NAME + " " + cDAL.LASTNAME).Trim();
                data.REQUISITION = ReqDAL.LOID;
                data.REQUISITIONCODE = ReqDAL.CODE;
                data.REQUISITIONTYPE = ReqDAL.REQUISITIONTYPE;
                data.RESERVEDATE = ReqDAL.RESERVEDATE;
                data.REQDATE = ReqDAL.REQDATE;
                DocTypeDAL dDAL = new DocTypeDAL();
                dDAL.GetDataByRequisitionType(data.REQUISITIONTYPE, obj.zTrans);
                data.DOCTYPE = dDAL.LOID;

                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                _error = ex.Message;
            }
            return data;
        }

        public StockoutFGData GetData(double loid)
        {
            StockoutFGData data = new StockoutFGData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.ACTIVE = DALObj.ACTIVE;
                //data.REQCODE = GetRequisitionCode(DALObj2.REFLOID);
                data.REFLOID = DALObj.REFLOID;
                data.CADDRESS = DALObj.CADDRESS;
                data.CFAX = DALObj.CFAX;
                data.CLASTNAME = DALObj.CLASTNAME;
                data.CNAME = DALObj.CNAME;
                data.CODE = DALObj.CODE;
                data.CREATEON = DALObj.CREATEON;
                data.CREATEBY = DALObj.CREATEBY;
                data.CTEL = DALObj.CTEL;
                data.CTITLE = DALObj.CTITLE;
                data.RECEIVER = DALObj.RECEIVER;
                data.DELIVERYDATE = DALObj.DELIVERYDATE;
                data.INVCODE = DALObj.INVCODE;
                data.REMARK = DALObj.REMARK;
                data.REQDATE = DALObj.REQDATE;
                data.DOCTYPE = DALObj.DOCTYPE;
                data.STATUS = DALObj.STATUS;
                data.REASON = DALObj.REASON;
                data.SENDER = DALObj.SENDER;
                data.TOTAL = DALObj.SumPrice(DALObj.LOID, null);
                StockOutFGReqData reqData = GetRequisitionData(data.REFLOID);
                data.RESERVEDATE = reqData.RESERVEDATE;
                data.REQUISITIONCODE = reqData.REQUISITIONCODE;
            }
            return data;
        }

        public DataTable GetStockOutItem(string stockout)
        {
            return DALItemObj.GetStockOutList(stockout);
        }

        public DataTable GetStockOutItemBlank()
        {
            return DALItemObj.GetStockOutItemListBlank();
        }

        public DataTable GetStockOutReturnItem(string stockout)
        {
            return DALItemObj.GetStockOutReturnList(stockout);
        }

        public DataTable GetStockOutReturnItemBlank()
        {
            return DALItemObj.GetStockOutReturnItemListBlank();
        }

        private void UpdateData(string userID, StockoutFGData data, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;
            DALObj.GetDataByLOID(data.LOID, zTrans);

            DALObj.ACTIVE = data.ACTIVE;
            DALObj.CADDRESS = data.CADDRESS;
            DALObj.CFAX = data.CFAX;
            DALObj.CLASTNAME = data.CLASTNAME;
            DALObj.CNAME = data.CNAME;
            DALObj.CTEL = data.CTEL;
            DALObj.CTITLE = data.CTITLE;
            DALObj.RECEIVER = data.RECEIVER;
            DALObj.DELIVERYDATE = data.DELIVERYDATE;
            DALObj.REMARK = data.REMARK;
            DALObj.REASON = data.REASON;
            DALObj.REQDATE = data.REQDATE;
            DALObj.DOCTYPE = data.DOCTYPE;
            DALObj.STATUS = data.STATUS;
            DALObj.REFTABLE = data.REFTABLE;
            DALObj.REFLOID = data.REFLOID;
            //DALObj.REFPROD = data.REFPROD;
            DALObj.PRODUCTREF = data.PRODUCTREF;
            DALObj.INVCODE = data.INVCODE;
            //DALObj.REASON = data.REASON;
            DALObj.SENDER = data.SENDER;

            if (DALObj.OnDB)
                ret = DALObj.UpdateCurrentData(userID, zTrans);
            else
                ret = DALObj.InsertCurrentData(userID, zTrans);

            _LOID = DALObj.LOID;
            if (!ret)
            {
                throw new ApplicationException(DALObj.ErrorMessage);
            }

            DALItemObj.DeleteDataByStockOut(_LOID, zTrans);
            for (int i = 0; i < data.ITEM.Count; i++)
            {
                StockOutItemData itemData = (StockOutItemData)data.ITEM[i];
                _itemDAL = new StockOutItemDAL();
                DALItemObj.ACTIVE = itemData.ACTIVE;
                DALItemObj.LOTNO = itemData.LOTNO;
                DALItemObj.PRICE = itemData.PRICE;
                DALItemObj.PRODUCT = itemData.PRODUCT;
                DALItemObj.QTY = itemData.QTY;
                DALItemObj.REFLOID = itemData.REFLOID;
                DALItemObj.REFTABLE = itemData.REFTABLE;
                DALItemObj.STATUS = data.STATUS;
                DALItemObj.STOCKOUT = _LOID;
                DALItemObj.UNIT = itemData.UNIT;
                DALItemObj.INVNO = itemData.INVNO;
                ret = DALItemObj.InsertCurrentData(userID, zTrans);
                if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);
            }
        }

        public bool CommitData(string userID, StockoutFGData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                UpdateData(userID, data, obj.zTrans);
                UpdateStockOutStatus(_LOID, data.STATUS, userID, obj.zTrans);

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

        public bool UpdateData(string userID, StockoutFGData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                UpdateData(userID, data, obj.zTrans);

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

        public bool DeleteData(ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                StockOutItemDAL itemDAL = new StockOutItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        _itemDAL = new StockOutItemDAL();
                        DALItemObj.DeleteDataByStockOut(Convert.ToDouble(arrData[i]), obj.zTrans);
                        ret = DALObj.DeleteCurrentData(obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    }
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

        private void UpdateStockOutStatus(double loid, string status, string userID, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;
            DALObj.OnDB = false;
            DALObj.GetDataByLOID(loid, zTrans);
            DALObj.APPROVEDATE = DateTime.Today;
            if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
            {
                DALObj.STATUS = status;
                ret = DALObj.UpdateCurrentData(userID, zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                ret = DALObj.CutStock(loid, userID, zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
            }
        }

        public bool UpdateStockOutStatus(ArrayList arrData, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    UpdateStockOutStatus(Convert.ToDouble(arrData[i]), status, userID, obj.zTrans);
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

        public bool UpdateStockOutStatus(double Loid, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (status == Constz.Requisition.Status.Approved.Code)
                {
                    UpdateStockOutStatus(Loid, status, userID, obj.zTrans);
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

        public ProductSearchData GetProductData(double loid)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(loid);
        }

        public ProductSearchData GetProductBarcode(double loid)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(loid);
        }

        public ProductSearchData GetProductData(string barcode)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(barcode);
        }

        //public UnitSearchData GetUnitData(double loid)
        //{
        //    UnitFlow uFlow = new UnitFlow();
        //    return uFlow.GetData(loid);
        //}

        public SupplierData GetSenderData(double loid)
        {
            StockFGDAL pFlow = new StockFGDAL();
            return pFlow.DoGetSenderData(loid);
        }

        public bool UpdateDataReturn(string userID, StockoutFGData data)
        {
            bool ret = true;
            if (!VerifyData(data)) throw new ApplicationException(_error);
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.GetDataByLOID(data.LOID, obj.zTrans);

                DALObj.ACTIVE = data.ACTIVE;
                DALObj.CADDRESS = data.CADDRESS;
                DALObj.CFAX = data.CFAX;
                DALObj.CLASTNAME = data.CLASTNAME;
                DALObj.CNAME = data.CNAME;
                DALObj.CTEL = data.CTEL;
                DALObj.CTITLE = data.CTITLE;
                DALObj.RECEIVER = data.RECEIVER;
                DALObj.DELIVERYDATE = data.DELIVERYDATE;
                DALObj.REMARK = data.REMARK;
                DALObj.REASON = data.REASON;
                DALObj.REQDATE = data.REQDATE;
                DALObj.DOCTYPE = data.DOCTYPE;
                DALObj.STATUS = data.STATUS;
                DALObj.REFLOID = data.REFLOID;
                DALObj.REFTABLE = data.REFTABLE;
                DALObj.PRODUCTREF = data.PRODUCTREF;
                DALObj.SENDER = data.SENDER;
                DALObj.INVCODE = data.INVCODE;
              //  DALObj.REASON = data.REASON;

                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                else
                    ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                _LOID = DALObj.LOID;
                if (!ret)
                {
                    throw new ApplicationException(DALObj.ErrorMessage);
                }

                StockOutItemDAL itemDAL = new StockOutItemDAL();
                itemDAL.DeleteDataByStockOut(data.LOID, obj.zTrans);
                for (Int16 i = 0; i < data.ITEM.Count; ++i)
                {
                    RequisitionItemData item = (RequisitionItemData)data.ITEM[i];
                    itemDAL.ACTIVE = item.ACTIVE;
                    itemDAL.PRODUCT = item.PRODUCT;
                    itemDAL.QTY = item.QTY;
                    itemDAL.PRICE = item.PRICE;
                    itemDAL.INVNO = item.INVNO;
                    itemDAL.STOCKOUT = DALObj.LOID;
                    itemDAL.UNIT = item.UNIT;
                    itemDAL.LOTNO = item.LOTNO;
                    itemDAL.STATUS = data.STATUS;

                    itemDAL.OnDB = false;
                    ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);



                }//for

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

        private bool VerifyData(StockoutFGData data)
        {
            bool ret = true;

            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุการสินค้า";
            }
            else if (data.RECEIVER == 0)
            {
                ret = false;
                _error = "กรุณาระบุลูกค้า";
            }
            else if (data.REASON == "")
            {
                ret = false;
                _error = "กรุณาระบุสาเหตุการส่งคืน";
            }

            return ret;
        }

        public double GetQTYStock(string lotno, double product, double warehouse)
        {
            return DALItemObj.GetQTYStockReturn(lotno, product, warehouse);
        }
    }
}

