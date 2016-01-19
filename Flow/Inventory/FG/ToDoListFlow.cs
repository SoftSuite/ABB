using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.Data.Inventory.FG;
using ABB.DAL.Inventory;
using ABB.DAL.Inventory.FG;
using ABB.DAL;

namespace ABB.Flow.Inventory.FG
{
    public class ToDoListFlow
    {
        string _error = "";
        double _LOID = 0;
        private ToDoListDAL _dal;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public ToDoListDAL DALObj
        {
            get { if (_dal == null) { _dal = new ToDoListDAL(); } return _dal; }
        }

        public DataTable GetMinimumStockList(ToDoListMinimumStockData data)
        {
            return DALObj.GetMinimumStockList(data);
        }

        public DataTable GetStockInkList(ToDoListStockInData data)
        {
            return DALObj.GetStockInList(data);
        }

        public DataTable GetStockOutkList(ToDoListStockOutData data)
        {
            return DALObj.GetStockOutList(data);
        }

        public DataTable GetExpireList(ToDoListExpireData data)
        {
            return DALObj.GetExpireList(data);
        }

        public DataTable GetProduct(double loid)
        {
            string sql = "SELECT * ";
            sql += "FROM v_product_pr_list WHERE LOID = '" + loid + "' ";
            DataTable tb = OracleDB.ExecListCmd(sql);
            return tb;
        }

        public bool NewPODocument(string UserID, ToDoListMinimumStockRequestData data)
        {
            bool ret = true;
            ret = NewPDRequest(UserID, data);
            return ret;
        }

        public bool NewPDDocument(string UserID, ToDoListMinimumStockRequestData data)
        {
            bool ret = true;
            ret = NewRequisition(UserID, data);
            return ret;
        }

        private bool NewPDRequest(string UserID, ToDoListMinimumStockRequestData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PDRequestDAL _DAL = new PDRequestDAL();
                _DAL.OnDB = false;
                _DAL.REQUESTDATE = DateTime.Now.Date;
                _DAL.ORDERTYPE = data.ORDERTYPE;
                _DAL.REQUESTBY = data.REQUESTBY;
                _DAL.DIVISION = data.DIVISION;
                _DAL.ACTIVE = data.ACTIVE;
                _DAL.STATUS = data.STATUS;
                //_DAL

                ret = _DAL.InsertCurrentData(UserID, obj.zTrans);
                if (!ret)
                {
                    throw new ApplicationException(_DAL.ErrorMessage);
                }
                PRItemDAL _DALItem = new PRItemDAL();
                for (int i = 0; i < data.ITEM.Count; ++i)
                {
                    _DALItem.OnDB = false;
                    ToDoListMinimumStockRequestItemData itemData = (ToDoListMinimumStockRequestItemData)data.ITEM[i];
                    _DALItem.PDREQUEST = _DAL.LOID;
                    _DALItem.PRODUCT = itemData.PRODUCT;
                    _DALItem.QTY = itemData.QTY;
                    DataTable dt = GetProduct(itemData.PRODUCT);
                    _DALItem.STATUS = data.STATUS;
                    _DALItem.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                    _DALItem.MINPRICE = Convert.ToDouble(dt.Rows[0]["MINPRICE"]);
                    _DALItem.OLDPRICE = Convert.ToDouble(dt.Rows[0]["LASTPRICE"]);
                    _DALItem.MINSTOCK = Convert.ToDouble(dt.Rows[0]["MIN"]);
                    _DALItem.MAXSTOCK = Convert.ToDouble(dt.Rows[0]["MAX"]);
                    _DALItem.STOCK = Convert.ToDouble(dt.Rows[0]["STOCK"]);
                    _DALItem.LAST3MON = Convert.ToDouble(dt.Rows[0]["USED3MONTH"]);
                    _DALItem.LASTYEAR = Convert.ToDouble(dt.Rows[0]["USED12MONTH"]);
                    _DALItem.DUEDATE = Convert.ToDateTime(dt.Rows[0]["DUEDATE"]);
                    ret = _DALItem.InsertCurrentData(UserID, obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(_DALItem.ErrorMessage);
                    }
                }

                _LOID = _DAL.LOID;

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

        private bool NewRequisition(string UserID, ToDoListMinimumStockRequestData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                RequisitionDAL _DAL = new RequisitionDAL();
                _DAL.OnDB = false;
                _DAL.OFFICER = data.REQUESTBY;
                _DAL.REQDATE = DateTime.Now.Date;
                _DAL.ACTIVE = data.ACTIVE;
                _DAL.STATUS = data.STATUS;
                _DAL.WAREHOUSE = data.WAREHOUSE;
                _DAL.REQUISITIONTYPE = data.REQUISITIONTYPE;
                _DAL.RESERVEDATE = DateTime.Now.Date;

                ret = _DAL.InsertCurrentData(UserID, obj.zTrans);
                if (!ret)
                {
                    throw new ApplicationException(_DAL.ErrorMessage);
                }
                RequisitionItemDAL _DALItem = new RequisitionItemDAL();
                for (int i = 0; i < data.ITEM.Count; ++i)
                {
                    _DALItem.OnDB = false;
                    ToDoListMinimumStockRequestItemData itemData = (ToDoListMinimumStockRequestItemData)data.ITEM[i];
                    _DALItem.ACTIVE = data.ACTIVE;
                    _DALItem.PRODUCT = itemData.PRODUCT;
                    _DALItem.QTY = itemData.QTY;
                    _DALItem.REQUISITION = _DAL.LOID;
                    _DALItem.UNIT = itemData.UNIT;
                    _DALItem.DUEDATE = DateTime.Now.Date;
                    ret = _DALItem.InsertCurrentData(UserID, obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(_DALItem.ErrorMessage);
                    }
                }

                _LOID = _DAL.LOID;

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

        public bool NewStockIn(string UserID, ToDoListStockinOrderData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                StockInDAL _DAL = new StockInDAL();
                _DAL.OnDB = false;
                _DAL.ACCCODE = data.ACCCODE;
                _DAL.DOCTYPE = data.DOCTYPE;
                _DAL.RECEIVEDATE = data.RECEIVEDATE;
                _DAL.RECEIVER = data.RECEIVER;
                _DAL.SENDER = data.SENDER;
                _DAL.STATUS = data.STATUS;

                ret = _DAL.InsertCurrentData(UserID, obj.zTrans);
                if (!ret)
                {
                    throw new ApplicationException(_DAL.ErrorMessage);
                }
                StockInItemDAL _DALItem = new StockInItemDAL();
                for (int i = 0; i < data.ITEM.Count; ++i)
                {
                    _DALItem.OnDB = false;
                    ToDoListStockInOrderItemData itemData = (ToDoListStockInOrderItemData)data.ITEM[i];
                    _DALItem.LOTNO = itemData.LOTNO;
                    _DALItem.PRICE = itemData.PRICE;
                    _DALItem.PRODUCT = itemData.PRODUCT;
                    _DALItem.QTY = itemData.QTY;
                    _DALItem.REFLOID = itemData.REFLOID;
                    _DALItem.REFTABLE = itemData.REFTABLE;
                    _DALItem.STATUS = itemData.STATUS;
                    _DALItem.STOCKIN = _DAL.LOID;
                    _DALItem.UNIT = itemData.UNIT;
                    ret = _DALItem.InsertCurrentData(UserID, obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(_DALItem.ErrorMessage);
                    }
                }

                _LOID = _DAL.LOID;

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

        public bool NewStockOut(string userID, double requisition, double warehouse)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                RequisitionDAL reqDAL = new RequisitionDAL();
                reqDAL.GetDataByLOID(requisition, obj.zTrans);

                RequisitionItemDAL reqItemDAL = new RequisitionItemDAL();
                DataTable dt = reqItemDAL.GetDataByRequisition(requisition, obj.zTrans);

                DocTypeDAL docDAL = new DocTypeDAL();
                docDAL.GetDataByRequisitionType(reqDAL.REQUISITIONTYPE, obj.zTrans);

                StockOutDAL _DAL = new StockOutDAL();
                _DAL.OnDB = false;
                _DAL.ACTIVE = Constz.ActiveStatus.Active;
                _DAL.DOCTYPE = docDAL.LOID;
                _DAL.RECEIVER = (reqDAL.CUSTOMER != 0 ? reqDAL.CUSTOMER : reqDAL.WAREHOUSE);
                _DAL.REFLOID = reqDAL.LOID;
                _DAL.REFTABLE = reqDAL.TableName;
                _DAL.REQDATE = reqDAL.REQDATE;
                _DAL.SENDER = warehouse;
                _DAL.STATUS = Constz.Requisition.Status.Waiting.Code;
                _DAL.CTITLE = reqDAL.CTITLE;
                _DAL.CNAME = reqDAL.CNAME;
                _DAL.CLASTNAME = reqDAL.CLASTNAME;
                _DAL.CADDRESS = reqDAL.CADDRESS;
                _DAL.CFAX = reqDAL.CFAX;
                _DAL.CTEL = reqDAL.CTEL;

                ret = _DAL.InsertCurrentData(userID, obj.zTrans);
                if (!ret)
                {
                    throw new ApplicationException(_DAL.ErrorMessage);
                }
                StockOutItemDAL _DALItem = new StockOutItemDAL();
                foreach (DataRow dRow in dt.Rows)
                {
                    StockFGDAL fgDAL = new StockFGDAL();
                    double qty = Convert.ToDouble(dRow["QTY"]);
                    DataTable dtStock = fgDAL.GetProductStock(warehouse, Convert.ToDouble(dRow["PRODUCT"]), obj.zTrans);
                    foreach (DataRow sRow in dtStock.Rows)
                    {
                        _DALItem.OnDB = false;
                        _DALItem.LOTNO = sRow["LOTNO"].ToString();
                        _DALItem.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                        if (qty >= Convert.ToDouble(sRow["QTY"]))
                            _DALItem.QTY = Convert.ToDouble(sRow["QTY"]);
                        else
                            _DALItem.QTY = qty;
                        _DALItem.REFLOID = Convert.ToDouble(dRow["LOID"]);
                        _DALItem.REFTABLE = "REQUISITIONITEM";
                        _DALItem.STATUS = Constz.Requisition.Status.Waiting.Code;
                        _DALItem.STOCKOUT = _DAL.LOID;
                        _DALItem.UNIT = Convert.ToDouble(dRow["UNIT"]);
                        _DALItem.ACTIVE = Constz.ActiveStatus.Active;
                        _DALItem.PRICE = Convert.ToDouble(dRow["PRICE"]);
                        double remain = _DALItem.GetRemainQTYStockFG(_DALItem.LOTNO, _DALItem.PRODUCT, obj.zTrans);
                        _DALItem.REMAIN = remain;
                        qty -= Convert.ToDouble(sRow["QTY"]);

                        ret = _DALItem.InsertCurrentData(userID, obj.zTrans);
                        if (!ret)
                        {
                            throw new ApplicationException(_DALItem.ErrorMessage);
                        }

                        if (qty <= 0) break;
                    }

                }

                _LOID = _DAL.LOID;

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
