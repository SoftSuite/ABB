using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.DAL;
using ABB.DAL.Sales;
using ABB.Data;
using ABB.Data.Sales;

namespace ABB.Flow.Sales
{
    public class StockInReturnFlow
    {
        private string _error = "";
        private double _LOID = 0;
        private StockInDAL _dal;
        private StockInItemDAL _dalItem;
        private StockInReturnDAL _search;
        private StockInReturnViewDAL _view;

        public string ErrorMessage
        {
            get { return _error; }
        }
        public double LOID
        {
            get { return _LOID; }
        }
        private StockInDAL DALObj
        {
            get { if (_dal == null) { _dal = new StockInDAL(); } return _dal; }
        }
        private StockInItemDAL DALItemObj
        {
            get { if (_dalItem == null) { _dalItem = new StockInItemDAL(); } return _dalItem; }
        }
        private StockInReturnDAL SearchObj
        {
            get { if (_search == null) { _search = new StockInReturnDAL(); } return _search; }
        }
        private StockInReturnViewDAL ViewObj
        {
            get { if (_view == null) { _view = new StockInReturnViewDAL(); } return _view; }
        }

        public DataTable GetStockInList(StockInReturnSearchData data)
        {
            DataTable dt = SearchObj.GetStockInList(data);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = i;
                i += 1;
            }
            return dt;
        }

        public bool CancelData(string userID, ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; ++i)
                {
                    double loid = Convert.ToDouble(arrData[i]);
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(loid, obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Approved.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Void.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = DALItemObj.UpdateStatusByStockIn(loid, DALObj.STATUS, userID, obj.zTrans);

                        ret = DALObj.CutStock(loid, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    }
                    else if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Void.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        DALItemObj.UpdateStatusByStockIn(loid, DALObj.STATUS, userID, obj.zTrans);
                    }
                }
                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
        }

        public bool InsertData(string userID, double warehouse)
        {
            bool ret = true;
            StockInData data = new StockInData();
            data.RECEIVEDATE = DateTime.Today;
            data.RECEIVER = warehouse;
            data.SENDER = warehouse;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateTransaction();
            try
            {
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                UpdateData(data, userID, obj.zTrans);

                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
        }

        private void UpdateData(StockInData data, string userID, System.Data.OracleClient.OracleTransaction trans)
        {
            bool ret = true;
            DALObj.CODE = data.CODE;
            DALObj.DOCTYPE = Constz.DocType.RetShop.LOID;
            DALObj.GRANDTOT = data.GRANDTOT;
            DALObj.RECEIVEDATE = data.RECEIVEDATE;
            DALObj.RECEIVER = data.RECEIVER;
            DALObj.REMARK = data.REMARK;
            DALObj.REASON = data.REASON;
            DALObj.SENDER = data.SENDER;
            DALObj.STATUS = data.STATUS;
            DALObj.REFTABLE = data.REFTABLE;
            DALObj.REFLOID = data.REFLOID;
            DALObj.APPROVEDATE = data.APPROVEDATE;
            DALObj.APPROVER = data.APPROVER;
            if (DALObj.OnDB)
                ret = DALObj.UpdateCurrentData(userID, trans);
            else
                ret = DALObj.InsertCurrentData(userID, trans);
            _LOID = DALObj.LOID;
            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

            DALItemObj.DeleteDataByStockIn(data.LOID, trans);
            for (int i = 0; i < data.STOCKINITEM.Count; ++i)
            {
                StockInItemData itemData = (StockInItemData)data.STOCKINITEM[i];
                DALItemObj.OnDB = false;
                DALItemObj.PRICE = itemData.PRICE;
                DALItemObj.PRODUCT = itemData.PRODUCT;
                DALItemObj.QTY = itemData.QTY;
                DALItemObj.STATUS = DALObj.STATUS;
                DALItemObj.STOCKIN = DALObj.LOID;
                DALItemObj.REFLOID = itemData.REFLOID;
                DALItemObj.REFTABLE = itemData.REFTABLE;
                DALItemObj.UNIT = itemData.UNIT;
                if (!DALItemObj.InsertCurrentData(userID, trans)) throw new ApplicationException(DALItemObj.ErrorMessage);
            }
        }

        private void ValidateData(StockInData data)
        {
            if (data.STOCKINITEM.Count == 0)
                throw new ApplicationException("กรุณาเลือกรายการสินค้า");
        }

        public bool CommitData(StockInData data, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateTransaction();
            try
            {
                ValidateData(data);
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code || data.LOID == 0)
                {
                    data.STATUS = Constz.Requisition.Status.Approved.Code;
                    UpdateData(data, userID, obj.zTrans);
                    ret = DALObj.CutStock(DALObj.LOID, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                }

                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
        }

        public DataTable GetInvoiceList(ABB.Data.Search.InvoiceForReturnSearchData data)
        {
            DataTable dt = SearchObj.GetInvoiceList(data);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = i;
                i += 1;
            }
            return dt;
        }

        public StockInReturnData GetStockInData(double stockIn)
        {
            StockInReturnData data = new StockInReturnData();
            ViewObj.GetDataByLOID(stockIn, null);
            data.CODE = ViewObj.CODE;
            data.CREATEBY = ViewObj.CREATEBY;
            data.TOTAL = ViewObj.GRANDTOT;
            data.LOID = ViewObj.LOID;
            data.RECEIVEDATE = ViewObj.RECEIVEDATE;
            data.RECEIVER = ViewObj.RECEIVER;
            data.REFLOID = ViewObj.REFLOID;
            data.REMARK = ViewObj.REMARK;
            data.SENDER = ViewObj.SENDER;
            data.STATUS = ViewObj.STATUS;
            data.CUSTOMERCODE = ViewObj.CUSTOMERCODE;
            data.CUSTOMERNAME = ViewObj.CUSTOMERNAME;
            data.INVOICECODE = ViewObj.INVOICECODE;
            data.INVOICEDATE = ViewObj.INVOICEDATE;
            data.REASON = ViewObj.REASON;
            DataTable dt = SearchObj.GetStockInProductList(stockIn);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = i;
                i += 1;
            }
            data.ITEM = dt;
            return data;
        }

        public StockInReturnData GetInvoiceData(double requisition)
        {
            StockInReturnData data = new StockInReturnData();
            SearchObj.GetDataByLOID(requisition, null);
            data.RECEIVER = SearchObj.WAREHOUSE;
            data.REFLOID = SearchObj.REQUISITION;
            data.SENDER = SearchObj.WAREHOUSE;
            data.CUSTOMERCODE = SearchObj.CUSTOMERCODE;
            data.CUSTOMERNAME = SearchObj.CUSTOMERNAME;
            data.INVOICECODE = SearchObj.INVOICECODE;
            data.INVOICEDATE = SearchObj.INVOICEDATE;
            DataTable dt = SearchObj.GetInvoiceProductList(SearchObj.REQUISITION);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = i;
                i += 1;
            }
            data.ITEM = dt;
            return data;
        }

        public StockInReturnData GetInvoiceData(string requisitionCode)
        {
            StockInReturnData data = new StockInReturnData();
            SearchObj.GetDataByCODE(requisitionCode, null);
            data.RECEIVER = SearchObj.WAREHOUSE;
            data.REFLOID = SearchObj.REQUISITION;
            data.SENDER = SearchObj.WAREHOUSE;
            data.CUSTOMERCODE = SearchObj.CUSTOMERCODE;
            data.CUSTOMERNAME = SearchObj.CUSTOMERNAME;
            data.INVOICECODE = SearchObj.INVOICECODE;
            data.INVOICEDATE = SearchObj.INVOICEDATE;
            DataTable dt = SearchObj.GetInvoiceProductList(SearchObj.REQUISITION);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = i;
                i += 1;
            }
            data.ITEM = dt;
            return data;
        }

        public double GetDiscount(string customer)
        {
            double discount = 0;
            DataTable dt = SearchObj.GetDiscount(customer);
            if (dt.Rows.Count > 0)
            {
                discount = Int32.Parse(dt.Rows[0]["DISCOUNT"].ToString());
            }
            return discount;
        }

    }
}
