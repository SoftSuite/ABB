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
    public class StockInShopFlow
    {
        private string _error = "";
        private double _LOID = 0;
        private StockInDAL _dal;
        private StockInItemDAL _dalItem;
        private StockInShopDAL _search;

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
        private StockInShopDAL SearchObj
        {
            get { if (_search == null) { _search = new StockInShopDAL(); } return _search; }
        }

        public DataTable GetStockInShopList(StockInShopSearchData data)
        {
            DataTable dt = SearchObj.GetStockInShopList(data);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = i;
                i += 1;
            }
            return dt;
        }

        public bool CommitData(ArrayList arrData, double Approver, string userID)
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
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        if (DALItemObj.GetDataList("WHERE STOCKIN = " + loid.ToString(), obj.zTrans).Rows.Count <= 0) throw new ApplicationException("รายการเลขที่ " + DALObj.CODE + " ไม่ได้ระบุรายการสินค้า");
                        DALObj.APPROVEDATE = DateTime.Today;
                        DALObj.APPROVER = Approver;
                        DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                        ret = DALItemObj.UpdateStatusByStockIn(loid, DALObj.STATUS, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);

                        //ret = DALObj.CutStock(loid, userID, obj.zTrans);
                        ret = DALObj.CutStock(loid, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    }
                }
                if (obj.zTrans != null) obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                if (obj.zTrans != null) obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
        }

        public bool DeleteData(ArrayList arrData)
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
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALItemObj.DeleteDataByStockIn(loid, obj.zTrans);
                        ret = DALObj.DeleteCurrentData(obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    }
                }
                if (obj.zTrans != null) obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                if (obj.zTrans != null) obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
        }

        private void UpdateData(StockInData data, string userID, System.Data.OracleClient.OracleTransaction trans)
        {
            bool ret = true;
            DALObj.CODE = data.CODE;
            DALObj.DOCTYPE = Constz.DocType.RecShop.LOID;
            DALObj.GRANDTOT = data.GRANDTOT;
            DALObj.RECEIVEDATE = data.RECEIVEDATE;
            DALObj.RECEIVER = data.RECEIVER;
            DALObj.REMARK = data.REMARK;
            DALObj.SENDER = data.SENDER;
            DALObj.STATUS = data.STATUS;
            DALObj.REFTABLE = data.REFTABLE;
            DALObj.REFLOID = data.REFLOID;
            DALObj.REFCODE = data.REFCODE;
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
                DALItemObj.UNIT = itemData.UNIT;
                if (!DALItemObj.InsertCurrentData(userID, trans)) throw new ApplicationException(DALItemObj.ErrorMessage);
            }
        }

        private void ValidateData(StockInData data)
        {
            if (data.REFCODE.Trim() == "")
                throw new ApplicationException("กรุณาระบุเลขที่ใบเบิกออกจากคลัง");
            else if (data.STOCKINITEM.Count == 0)
                throw new ApplicationException("กรุณาเลือกรายการสินค้า");
        }

        public bool UpdateData(StockInData data, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateTransaction();
            try
            {
                ValidateData(data);
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                UpdateData(data, userID, obj.zTrans);

                if (obj.zTrans != null) obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                if (obj.zTrans != null) obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
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
                    //ret = DALObj.CutStock(DALObj.LOID, userID, obj.zTrans);
                    ret = DALObj.CutStock(DALObj.LOID, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                }

                if (obj.zTrans != null) obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                if (obj.zTrans != null) obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
        }

        public bool InsertData(string userID, double warehouse)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateTransaction();
            try
            {
                StockInData data = new StockInData();
                data.RECEIVEDATE = DateTime.Today;
                data.RECEIVER = warehouse;
                data.SENDER = warehouse;
                data.STATUS = Constz.Requisition.Status.Waiting.Code;

                DALObj.OnDB = false;
                DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                UpdateData(data, userID, obj.zTrans);

                if (obj.zTrans != null) obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                if (obj.zTrans != null) obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
        }

        public DataTable GetStockOutList(ABB.Data.Search.StockOutProductSearchData data, double currentStockIn)
        {
            DataTable dt = SearchObj.GetStockOutList(data, currentStockIn);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = i;
                i += 1;
            }
            return dt;
        }

        public StockInShopData GetStockInData(double stockIn)
        {
            StockInShopData data = new StockInShopData();
            DALObj.GetDataByLOID(stockIn, null);
            data.CODE = DALObj.CODE;
            data.CREATEBY = DALObj.CREATEBY;
            data.GRANDTOT = DALObj.GRANDTOT;
            data.LOID = DALObj.LOID;
            data.RECEIVEDATE = DALObj.RECEIVEDATE;
            data.RECEIVER = DALObj.RECEIVER;
            data.REFLOID = DALObj.REFLOID;
            data.REMARK = DALObj.REMARK;
            data.SENDER = DALObj.SENDER;
            data.STATUS = DALObj.STATUS;
            data.REQUISITIONCODE = DALObj.REFCODE;
            //StockOutDAL sDAL = new StockOutDAL();
            //sDAL.TableName = "STOCKOUT" + Constz.ABBSERV + "";
            //sDAL.GetDataByLOID(DALObj.REFLOID, null);
            //RequisitionDAL rDAL = new RequisitionDAL();
            //rDAL.TableName = "REQUISITION" + Constz.ABBSERV + "";
            //rDAL.GetDataByLOID(sDAL.REFLOID, null);
            //data.REQUISITIONCODE = rDAL.CODE;
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

        public StockInShopData GetStockOutData(double stockOut)
        {
            StockInShopData data = new StockInShopData();
            StockOutDAL sDAL = new StockOutDAL();
            sDAL.TableName = "STOCKOUT" + Constz.ABBSERV + "";
            sDAL.GetDataByLOID(stockOut, null);
            data.CODE = sDAL.CODE;
            data.RECEIVER = sDAL.RECEIVER;
            data.REFLOID = sDAL.LOID;
            data.SENDER = sDAL.RECEIVER;
            RequisitionDAL rDAL = new RequisitionDAL();
            rDAL.TableName = "REQUISITION" + Constz.ABBSERV + "";
            rDAL.GetDataByLOID(sDAL.REFLOID, null);
            data.REQUISITIONCODE = rDAL.CODE;
            DataTable dt = SearchObj.GetStockOutProductList(stockOut);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = i;
                i += 1;
            }
            data.ITEM = dt;
            return data;
        }

        public StockInShopData GetStockOutData(string requisitionCode, double currentStockIn)
        {
            double stockOut = 0;
            ABB.Data.Search.StockOutProductSearchData searchData = new ABB.Data.Search.StockOutProductSearchData();
            searchData.REQUISITIONCODEFROM = requisitionCode;
            searchData.REQUISITIONCODETO = requisitionCode;
            DataTable dtSearch = GetStockOutList(searchData, currentStockIn);
            if (dtSearch.Rows.Count == 1)
            {
                stockOut = Convert.ToDouble(dtSearch.Rows[0]["STOCKOUT"]);
            }
            StockInShopData data = new StockInShopData();
            StockOutDAL sDAL = new StockOutDAL();
            sDAL.TableName = "STOCKOUT" + Constz.ABBSERV + "";
            sDAL.GetDataByLOID(stockOut, null);
            data.CODE = sDAL.CODE;
            data.RECEIVER = sDAL.RECEIVER;
            data.REFLOID = sDAL.LOID;
            data.SENDER = sDAL.RECEIVER;
            RequisitionDAL rDAL = new RequisitionDAL();
            rDAL.TableName = "REQUISITION" + Constz.ABBSERV + "";
            rDAL.GetDataByLOID(sDAL.REFLOID, null);
            data.REQUISITIONCODE = rDAL.CODE;
            DataTable dt = SearchObj.GetStockOutProductList(stockOut);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = i;
                i += 1;
            }
            data.ITEM = dt;
            return data;
        }

    }
}
