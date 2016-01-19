using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Sales;
using ABB.Data.Admin;
using ABB.Data.Inventory.FG;
using ABB.DAL;
using ABB.DAL.Inventory;
using ABB.Flow.Admin;
using ABB.Flow.Sales;

namespace ABB.Flow.Inventory.FG
{
    public class StockInFlow
    {
        string _error = "";
        double _LOID = 0;
        StockInDAL _dal;
        StockInItemDAL _itemDal;
        StockFGDAL search;

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

        public StockInItemDAL DALItemObj
        {
            get { if (_itemDal == null) { _itemDal = new StockInItemDAL(); } return _itemDal; }
        }

        public StockFGDAL SearchDAL
        {
            get { if (search == null) search = new StockFGDAL(); return search; }
        }

        public DataTable GetStockInList(StockInFGData data)
        {
            DataTable dt = SearchDAL.GetReceiveList(data);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            return dt;
        }

        public DataTable GetStockInWHList(StockInFGData data)
        {
            DataTable dt = SearchDAL.GetReceiveWHList(data);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            return dt;
        }

        public DataTable GetStockInOTList(StockInFGData data)
        {
            DataTable dt = SearchDAL.GetReceiveOTList(data);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            return dt;
        }

        public StockInFGData GetData(double loid)
        {
            StockInFGData data = new StockInFGData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.SENDER = DALObj.SENDER;
                data.QCCODE = DALObj.QCCODE;
                data.REMARK = DALObj.REMARK;
                data.RECEIVEDATE = DALObj.RECEIVEDATE;
                data.STATUS = DALObj.STATUS;
                data.INVNO = DALObj.INVNO;
                data.RECEIVER = DALObj.RECEIVER;
            }
            return data;
        }

        public DataTable GetStockInItem(double requisition)
        {
            return SearchDAL.GetReceiveItemList(requisition);
        }


        public DataTable GetStockInItemBlank()
        {
            StockInItemDAL itemDAL = new StockInItemDAL();
            return SearchDAL.GetReceiveItemListBlank();
        }

        public bool ValidateData(StockInFGData data)
        {
            bool ret = true;
            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            else if (data.INVNO == "")
            {
                ret = false;
                _error = "กรุณาระบุเลขที่ใบส่งของ";
            }
            else
            {
                for (Int16 i = 0; i < data.ITEM.Count; ++i)
                {
                    StockInItemData item = (StockInItemData)data.ITEM[i];
                    if (item.LOTNO == "")
                    {
                        ret = false;
                        _error = "กรุณาระบุ Lot No";
                    }

                }
            }
            return ret;
        }

        private void UpdateData(string userID, StockInFGData data, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;

            DALObj.OnDB = false;
            DALObj.GetDataByLOID(data.LOID, zTrans);

            DALObj.SENDER = data.SENDER;
            DALObj.RECEIVEDATE = data.RECEIVEDATE;
            DALObj.REMARK = data.REMARK;
            DALObj.STATUS = data.STATUS;
            DALObj.INVNO = data.INVNO;
            DALObj.QCCODE = data.QCCODE;
            DALObj.RECEIVER = data.RECEIVER;
            DALObj.DOCTYPE = data.DOCTYPE;
            DALObj.QCDATE = data.QCDATE;


            if (DALObj.OnDB)
                ret = DALObj.UpdateCurrentData(userID, zTrans);
            else
                ret = DALObj.InsertCurrentData(userID, zTrans);

            _LOID = DALObj.LOID;
            if (!ret)
            {
                throw new ApplicationException(DALObj.ErrorMessage);
            }

            StockInItemDAL itemDAL = new StockInItemDAL();
            itemDAL.DeleteDataByStockIn(data.LOID, zTrans);
            for (Int16 i = 0; i < data.ITEM.Count; ++i)
            {
                StockInItemData item = (StockInItemData)data.ITEM[i];
                itemDAL.PRODUCT = item.PRODUCT;
                itemDAL.QTY = item.QTY;
                itemDAL.PRICE = item.PRICE;
                itemDAL.STOCKIN = DALObj.LOID;
                itemDAL.REFLOID = item.REFLOID;
                itemDAL.REFTABLE = "POITEM";
                itemDAL.LOTNO = item.LOTNO;
                itemDAL.QCQTY = item.QCQTY;
                if (item.QCRESULT == Constz.QCResult.Pass.Name)
                    itemDAL.QCRESULT = Constz.QCResult.Pass.Code;
                else if (item.QCRESULT == Constz.QCResult.Fail.Name)
                    itemDAL.QCRESULT = Constz.QCResult.Fail.Code;
                else
                    itemDAL.QCRESULT = "";

                itemDAL.QCREMARK = item.QCREMARK;
                itemDAL.ACTIVE = Constz.ActiveStatus.Active;
                itemDAL.STATUS = data.STATUS;
                itemDAL.UNIT = item.UNIT;
                itemDAL.PRICE = item.PRICE;
                itemDAL.REMARK = item.REMARK;


                itemDAL.OnDB = false;
                ret = itemDAL.InsertCurrentData(userID, zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
            }
        }



        public bool CommitQCData(string userID, StockInFGData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    data.QCCODE = OracleDB.GetRunningCode("STOCKIN_QC", data.DOCTYPE.ToString());
                    data.QCDATE = DateTime.Now.Date;
                    UpdateData(userID, data, obj.zTrans);

                    ret = DALObj.CutStock(_LOID, userID, obj.zTrans);
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
            }
            else
                ret = false;
            return ret;
        }

        public bool UpdateData(string userID, StockInFGData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
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
            }
            else
                ret = false;
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
                StockInItemDAL itemDAL = new StockInItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByStockIn(Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = DALObj.DeleteCurrentData(obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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

        //public bool NewRequisition(string userID, PDStockInData data)
        //{
        //    bool ret = true;
        //    OracleDBObj obj = new OracleDBObj();
        //    obj.CreateConnection();
        //    obj.CreateTransaction();
        //    try
        //    {
        //        DALObj.ACTIVE = data.ACTIVE;
        //        DALObj.CODE = data.CODE;
        //        DALObj.REMARK = data.REMARK;
        //        DALObj.RECEIVEDATE = data.RECEIVEDATE;
        //        DALObj.STATUS = data.STATUS;
        //        DALObj.INVNO = data.INVNO;
        //        DALObj.QCCODE = data.QCCODE;
        //        DALObj.RECEIVER = data.RECEIVER;
        //        DALObj.DOCTYPE = data.TYPE;


        //        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

        //        _LOID = DALObj.LOID;
        //        if (!ret)
        //        {
        //            throw new ApplicationException(DALObj.ErrorMessage);
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

        private void UpdateQty(double loid, double poloid, double pdloid, double qty, string userID, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;
            ret = DALObj.UpdateStockInQty(loid, qty, userID, zTrans);
            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
            ret = DALObj.UpdatePOQty(poloid, pdloid, qty, userID, zTrans);
            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
        }

        public bool UpdateStockInStatus(double loid, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(loid, obj.zTrans);
                DALObj.STATUS = status;
                ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                ret = DALItemObj.UpdateStatusByStockIn(DALObj.LOID, DALObj.STATUS, userID, obj.zTrans);
                if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);

                ret = DALObj.CutStock(loid, userID, obj.zTrans);
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

        public bool UpdateQty(double loid, double poloid, double pdloid, double qty, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                UpdateQty(loid, poloid, pdloid, qty, userID, obj.zTrans);

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

        public bool UpdateStockInStatus(ArrayList arrData, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    if (status == Constz.Requisition.Status.QC.Code)
                    {
                        if (GetStockInItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                    }
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    ret = DALItemObj.UpdateStatusByStockIn(DALObj.LOID, DALObj.STATUS, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);

                    ret = DALObj.CutStock(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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

        public bool UpdateStockInQCStatus(ArrayList arrData, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    if (status == Constz.Requisition.Status.QC.Code)
                    {
                        if (GetStockInItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                    }
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    DALObj.STATUS = Constz.Requisition.Status.QC.Code;
                    DALObj.QCCODE = OracleDB.GetRunningCode("STOCKIN_QC", DALObj.DOCTYPE.ToString());
                    DALObj.QCDATE = DateTime.Now.Date;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    ret = DALObj.CutStock(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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

        public bool UpdateStockInFNStatus(ArrayList arrData, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    if (status == Constz.Requisition.Status.Finish.Code)
                    {
                        if (GetStockInItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                    }
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    DALObj.STATUS = Constz.Requisition.Status.Finish.Code;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    ret = DALObj.CutStock(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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

        public POItemData GetPOItemData(double loid)
        {
            StockFGDAL pFlow = new StockFGDAL();
            return pFlow.DoGetPOItem(loid);
        }


        public UnitSearchData GetUnitData(double loid)
        {
            UnitFlow uFlow = new UnitFlow();
            return uFlow.GetData(loid);
        }

        public SupplierData GetSenderData(double loid)
        {
            StockFGDAL pFlow = new StockFGDAL();
            return pFlow.DoGetSenderData(loid);
        }


    }
}
