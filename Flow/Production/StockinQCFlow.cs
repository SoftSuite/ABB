using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Inventory.FG;
using ABB.Data.Production;
using ABB.DAL;
using ABB.DAL.Production;
using ABB.DAL.Inventory;
using ABB.Flow.Admin;
using ABB.Flow.Sales;


namespace ABB.Flow.Production
{
    public class StockinQCFlow
    {
        string _error = "";
        double _LOID = 0;
        StockInDAL _dal;
        QCAnalysisDAL search;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public StockInDAL DALObj
        {
            get { if (_dal == null) { _dal = new StockInDAL(); } return _dal; }
        }

        public QCAnalysisDAL SearchDAL
        {
            get { if (search == null) search = new QCAnalysisDAL(); return search; }
        }

        //public DataTable GetStockInList(StockInFGData data)
        //{
        //    DataTable dt = SearchDAL.GetReceiveList(data);
        //    for (int i = 0; i < dt.Rows.Count; ++i)
        //    {
        //        dt.Rows[i]["NO"] = i + 1;
        //    }
        //    return dt;
        //}

        public StockinQCData GetData(double loid)
        {
            StockinQCData data = new StockinQCData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.STATUS = DALObj.STATUS;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.SENDER = DALObj.SENDER;
                data.QCCODE = DALObj.QCCODE;
                data.QCDATE = DALObj.QCDATE;
                data.INVNO = DALObj.INVNO;
                data.REMARK = DALObj.REMARK;
                data.RECEIVEDATE = DALObj.RECEIVEDATE;
                data.STATUS = DALObj.STATUS;
                data.INVNO = DALObj.INVNO;
                data.RECEIVER = DALObj.RECEIVER;
                data.ANACODE = DALObj.ANACODE;
                data.ANADATE = DALObj.ANADATE;
            }
            return data;
        }

        //public DataTable GetStockInItem(double stockin)
        //{
        //    return SearchDAL.GetStockInItem(stockin);
        //}

        public DataTable GetStockInItem(double stockin)
        {
            DataTable dt = SearchDAL.GetStockInItem(stockin);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["NO"] = i;
                i += 1;
            }
            return dt;
        }




        //public DataTable GetStockInItemBlank()
        //{
        //    StockInItemDAL itemDAL = new StockInItemDAL();
        //    return SearchDAL.GetReceiveItemListBlank();
        //}

        //public bool ValidateData(StockInFGData data)
        //{
        //    bool ret = true;
        //    if (data.ITEM.Count == 0)
        //    {
        //        ret = false;
        //        _error = "กรุณาระบุรายการสินค้า";
        //    }
        //    return ret;
        //}

        //private void UpdateData(string userID, StockInFGData data, System.Data.OracleClient.OracleTransaction zTrans)
        //{
        //    bool ret = true;
        //    DALObj.OnDB = false;
        //    DALObj.GetDataByLOID(data.LOID, zTrans);

        //    DALObj.SENDER = data.SENDER;
        //    DALObj.RECEIVEDATE = data.RECEIVEDATE;
        //    DALObj.REMARK = data.REMARK;
        //    DALObj.STATUS = data.STATUS;
        //    DALObj.INVNO = data.INVNO;
        //    DALObj.QCCODE = data.QCCODE;
        //    DALObj.RECEIVER = data.RECEIVER;
        //    DALObj.DOCTYPE = data.DOCTYPE;


        //    if (DALObj.OnDB)
        //        ret = DALObj.UpdateCurrentData(userID, zTrans);
        //    else
        //        ret = DALObj.InsertCurrentData(userID, zTrans);

        //    _LOID = DALObj.LOID;
        //    if (!ret)
        //    {
        //        throw new ApplicationException(DALObj.ErrorMessage);
        //    }

        //    StockInItemDAL itemDAL = new StockInItemDAL();
        //    itemDAL.DeleteDataByStockIn(data.LOID, zTrans);
        //    for (Int16 i = 0; i < data.ITEM.Count; ++i)
        //    {
        //        StockInItemData item = (StockInItemData)data.ITEM[i];
        //        itemDAL.PRODUCT = item.PRODUCT;
        //        itemDAL.QTY = item.QTY;
        //        itemDAL.PRICE = item.PRICE;
        //        itemDAL.STOCKIN = DALObj.LOID;
        //        itemDAL.REFLOID = item.REFLOID;
        //        itemDAL.REFTABLE = "PDORDER";
        //        itemDAL.LOTNO = item.LOTNO;
        //        itemDAL.QCQTY = item.QCQTY;
        //        itemDAL.QCRESULT = item.QCRESULT;
        //        itemDAL.QCREMARK = item.QCREMARK;
        //        itemDAL.ACTIVE = Constz.ActiveStatus.Active;
        //        itemDAL.STATUS = Constz.Requisition.Status.Waiting.Code;
        //        itemDAL.UNIT = item.UNIT;
        //        itemDAL.PRICE = item.PRICE;


        //        itemDAL.OnDB = false;
        //        ret = itemDAL.InsertCurrentData(userID, zTrans);
        //        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
        //    }
        //}



        //public bool CommitQCData(string userID, StockInFGData data)
        //{
        //    bool ret = true;
        //    if (ValidateData(data))
        //    {
        //        OracleDBObj obj = new OracleDBObj();
        //        obj.CreateConnection();
        //        obj.CreateTransaction();
        //        try
        //        {
        //            data.QCCODE = OracleDB.GetRunningCode("STOCKIN_QC", data.DOCTYPE.ToString());
        //            UpdateData(userID, data, obj.zTrans);
        //            UpdateStockInStatus(_LOID, data.STATUS, userID, obj.zTrans);

        //            obj.zTrans.Commit();
        //            obj.CloseConnection();
        //        }
        //        catch (Exception ex)
        //        {
        //            obj.zTrans.Rollback();
        //            obj.CloseConnection();
        //            ret = false;
        //            _error = ex.Message;
        //        }
        //    }
        //    else
        //        ret = false;
        //    return ret;
        //}

        //public bool UpdateData(string userID, StockInFGData data)
        //{
        //    bool ret = true;
        //    if (ValidateData(data))
        //    {
        //        OracleDBObj obj = new OracleDBObj();
        //        obj.CreateConnection();
        //        obj.CreateTransaction();
        //        try
        //        {
        //            UpdateData(userID, data, obj.zTrans);

        //            obj.zTrans.Commit();
        //            obj.CloseConnection();
        //        }
        //        catch (Exception ex)
        //        {
        //            obj.zTrans.Rollback();
        //            obj.CloseConnection();
        //            ret = false;
        //            _error = ex.Message;
        //        }
        //    }
        //    else
        //        ret = false;
        //    return ret;
        //}

        //public bool DeleteData(ArrayList arrData)
        //{
        //    bool ret = true;
        //    OracleDBObj obj = new OracleDBObj();
        //    obj.CreateConnection();
        //    obj.CreateTransaction();
        //    try
        //    {
        //        RequisitionItemDAL itemDAL = new RequisitionItemDAL();
        //        for (int i = 0; i < arrData.Count; i++)
        //        {
        //            DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
        //            itemDAL.DeleteDataByRequisition(Convert.ToDouble(arrData[i]), obj.zTrans);
        //            ret = DALObj.DeleteCurrentData(obj.zTrans);
        //            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
        //        }
        //        obj.zTrans.Commit();
        //        obj.CloseConnection();
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.zTrans.Rollback();
        //        obj.CloseConnection();
        //        ret = false;
        //        _error = ex.Message;
        //    }
        //    return ret;
        //}

        ////public bool NewRequisition(string userID, PDStockInData data)
        ////{
        ////    bool ret = true;
        ////    OracleDBObj obj = new OracleDBObj();
        ////    obj.CreateConnection();
        ////    obj.CreateTransaction();
        ////    try
        ////    {
        ////        DALObj.ACTIVE = data.ACTIVE;
        ////        DALObj.CODE = data.CODE;
        ////        DALObj.REMARK = data.REMARK;
        ////        DALObj.RECEIVEDATE = data.RECEIVEDATE;
        ////        DALObj.STATUS = data.STATUS;
        ////        DALObj.INVNO = data.INVNO;
        ////        DALObj.QCCODE = data.QCCODE;
        ////        DALObj.RECEIVER = data.RECEIVER;
        ////        DALObj.DOCTYPE = data.TYPE;


        ////        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

        ////        _LOID = DALObj.LOID;
        ////        if (!ret)
        ////        {
        ////            throw new ApplicationException(DALObj.ErrorMessage);
        ////        }

        ////        obj.zTrans.Commit();
        ////        obj.CloseConnection();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        obj.zTrans.Rollback();
        ////        obj.CloseConnection();
        ////        ret = false;
        ////        _error = ex.Message;
        ////    }
        ////    return ret;
        ////}

        public bool UpdateQCResult(double loid, DateTime duedate, string qcresult, string qcremark, string qcqty, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                ret = SearchDAL.UpdateQCResult(loid, duedate, qcresult, qcremark, qcqty, userID, obj.zTrans);
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

        public bool UpdateQCStockin(double loid, string anacode, DateTime anadate, string userID, string status)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                ret = SearchDAL.UpdateQCStockin(loid, anacode, anadate, userID, status, obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                else
                    ret = SearchDAL.UpdateQCStockinItem(loid, userID, status, obj.zTrans);
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
        //public POItemData GetPOItemData(double loid, double product)
        //{
        //    StockFGDAL pFlow = new StockFGDAL();
        //    return pFlow.DoGetPOItem(loid, product);
        //}


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

        public string GetDivision(string createBy)
        {
            return SearchDAL.GetDivision(createBy);
        }

        //public SupplierData GetDivision(string createby)
        //{
        //      return DALObj.DoGetSenderData(loid);
        //}


    }
}

