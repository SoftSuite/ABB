using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Sales;

namespace ABB.Flow.Sales
{
    public class ReturnTesterFlow
    {
        string _error = "";
        double _LOID = 0;
        StockOutDAL _dal;
        ReturnTesterDAL search;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public StockOutDAL DALObj
        {
            get { if (_dal == null) { _dal = new StockOutDAL(); } return _dal; }
        }

        public ReturnTesterDAL SearchDAL
        {
            get { if (search == null) search = new ReturnTesterDAL(); return search; }
        }

        public DataTable GetReturnTesterList(ReturnTesterSearchData data)
        {
            return SearchDAL.GetReturnTesterList(data);
        }

        public ProductTesterData GetProductData(double loid)
        {
            DataTable dt = ABB.DAL.Sales.ReturnTesterDAL.GetProduct(loid, "");
            ProductTesterData data = new ProductTesterData();
            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["BARCODE"])) data.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["NAME"])) data.NAME = dt.Rows[0]["NAME"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["PRICE"])) data.PRICE = Convert.ToDouble(dt.Rows[0]["PRICE"]);
                if (!Convert.IsDBNull(dt.Rows[0]["LOID"])) data.PRODUCT = Convert.ToDouble(dt.Rows[0]["LOID"]);
                if (!Convert.IsDBNull(dt.Rows[0]["UNIT"])) data.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                if (!Convert.IsDBNull(dt.Rows[0]["UNITNAME"])) data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
            }
            return data;
        }

        public ReturnTesterData GetData(double loid)
        {
            ReturnTesterData data = new ReturnTesterData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.APPROVEDATE = DALObj.APPROVEDATE;
                data.APPROVER = DALObj.APPROVER;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.CREATEON = DALObj.CREATEON;
                data.DOCTYPE = DALObj.DOCTYPE;
                data.LOID = DALObj.LOID;
                data.REASON = DALObj.REASON;
                data.RECEIVER = DALObj.RECEIVER;
                data.REMARK = DALObj.REMARK;
                data.SENDER = DALObj.SENDER;
                data.STATUS = DALObj.STATUS;
                data.STOCKITEM = new ArrayList();
                DataTable dt = SearchDAL.GetStockOutItem(loid);
                int i = 1;
                foreach (DataRow dRow in dt.Rows)
                {
                    ReturnTesterItemData itemData = new ReturnTesterItemData();
                    itemData.ORDERNO = i;
                    itemData.BARCODE = dRow["BARCODE"].ToString();
                    itemData.NAME = dRow["NAME"].ToString();
                    itemData.QTY = Convert.ToDouble(dRow["QTY"]);
                    itemData.PRICE = Convert.ToDouble(dRow["PRICE"]);
                    itemData.UNITNAME = dRow["UNITNAME"].ToString();
                    itemData.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                    itemData.UNIT = Convert.ToDouble(dRow["UNIT"]);
                    data.STOCKITEM.Add(itemData);
                }
            }
            return data;
        }

        public bool ValidateData(ReturnTesterData data)
        {
            bool ret = true;
            if (data.RECEIVER == 0)
            {
                ret = false;
                _error = "กรุณาเลือกคลังสินค้า";
            }
            else if (data.REASON.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุเหตุผลในการคืนสินค้าตัวอย่าง";
            }
            else if (data.STOCKITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            return ret;
        }

        public bool UpdateData(string userID, ReturnTesterData data)
        {
            bool ret = true;
            bool cutstock = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    cutstock = (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code && data.STATUS == Constz.Requisition.Status.Approved.Code);
                    DALObj.ACTIVE = data.ACTIVE;
                    DALObj.APPROVEDATE = data.APPROVEDATE;
                    DALObj.APPROVER = data.APPROVER;
                    DALObj.DOCTYPE = data.DOCTYPE;
                    DALObj.REASON = data.REASON;
                    DALObj.RECEIVER = data.RECEIVER;
                    DALObj.REMARK = data.REMARK;
                    DALObj.SENDER = data.SENDER;
                    DALObj.STATUS = data.STATUS;

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                    {
                        DALObj.CODE = OracleDB.GetRunningCode("STOCKOUT", data.DOCTYPE.ToString(), obj.zTrans);
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);
                        cutstock = false;
                    }

                    _LOID = DALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }

                    StockOutItemDAL itemDAL = new StockOutItemDAL();
                    itemDAL.DeleteDataByStockOut(DALObj.LOID, obj.zTrans);
                    for (Int16 i = 0; i < data.STOCKITEM.Count; ++i)
                    {
                        StockOutItemData item = (StockOutItemData)data.STOCKITEM[i];
                        itemDAL.ACTIVE = item.ACTIVE;
                        itemDAL.LOTNO = item.LOTNO;
                        itemDAL.PRODUCT = item.PRODUCT;
                        itemDAL.QTY = item.QTY;
                        itemDAL.STATUS = item.STATUS;
                        itemDAL.STOCKOUT = DALObj.LOID;
                        itemDAL.UNIT = item.UNIT;

                        itemDAL.OnDB = false;
                        ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
                    }
                    if (cutstock)
                    {
                        ret = DALObj.CutStock(DALObj.LOID, userID, obj.zTrans);
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
                StockOutItemDAL itemDAL = new StockOutItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByStockOut(Convert.ToDouble(arrData[i]), obj.zTrans);

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

        public bool ApproveData(string UserID, double Approver, ArrayList arrData)
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
                        DALObj.APPROVEDATE = DateTime.Now.Date;
                        DALObj.APPROVER = Approver;
                        DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                        ret = DALObj.UpdateCurrentData(UserID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = itemDAL.UpdateStatusByStockOut(Convert.ToDouble(arrData[i]), DALObj.STATUS, UserID, obj.zTrans);
                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);

                        ret = DALObj.CutStock(DALObj.LOID, UserID, obj.zTrans);
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

    }
}
