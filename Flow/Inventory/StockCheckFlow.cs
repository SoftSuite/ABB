using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.DAL;
using ABB.Flow.Inventory;
using ABB.Data.Inventory;

namespace ABB.Flow.Inventory
{
    public class StockCheckFlow
    {
        string _error = "";
        double _LOID = 0;
        //PDOrderDAL _dal;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        //public PDOrderDAL DALObj
        //{
        //    get { if (_dal == null) { _dal = new PDOrderDAL(); } return _dal; }
        //}

        public DataTable GetStockCheckItemList(StockCheckSearchData data)
        {
            string whereString = "";

            if (data.BATCHNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(BATCHNO) = '" + OracleDB.QRText(data.BATCHNO.Trim()).ToUpper() + "' ";

            if (data.WAREHOUSE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "WAREHOUSE = " + data.WAREHOUSE.ToString() + " ";

            if (data.LOCATION != 0)
                whereString += (whereString == "" ? "" : "AND ") + "L_LOID = " + data.LOCATION.ToString() + " ";

            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CHECKDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CHECKDATE <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.BARCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(BARCODE) LIKE '%" + data.BARCODE.Trim().ToUpper() + "%' ";

            if (data.PRODUCTNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(PD_NAME) LIKE '%" + data.PRODUCTNAME.Trim().ToUpper() + "%' ";

            if (data.LOTNO.Trim() != "ทั้งหมด")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(LOTNO) = '" + OracleDB.QRText(data.LOTNO.Trim()).ToUpper() + "' ";

            if (data.DIFFCHECK)
                whereString += (whereString == "" ? "" : "AND ") + "DIFFQTY <> 0 ";

            string sql = "SELECT ROWNUM NO, BATCHNO, BARCODE, PD_NAME, LOTNO, L_NAME, SYSQTY, COUNTQTY, DIFFQTY, SH_LOID, PD_LOID , PS_LOID ";
            sql += "FROM v_stockcheck_list ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);

            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public string GetStatusByBatchNo(string batchno)
        {
            string sql = "SELECT STATUS FROM STOCKCHECK WHERE BATCHNO='" + batchno + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count == 0)
                return "";
            else
                return dt.Rows[0]["STATUS"].ToString();
        }

        public StockCheckData GetData(double _LOID)
        {
            StockCheckData data = new StockCheckData();
            StockCheckDAL sDAL = new StockCheckDAL();
            sDAL.GetDataByLOID(_LOID, null);
            data.BATCHNO = sDAL.BATCHNO;
            data.CHECKDATE = sDAL.CHECKDATE;
            data.WAREHOUSE = sDAL.WAREHOUSE;
            return data;
        }

        public bool UpdateStockCheckStatus(string batchno, string status, string userID)
        {
            string sql = "UPDATE STOCKCHECK SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE BATCHNO='" + batchno + "'";
            bool ret = true;
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql) > 0);
                if (!ret) _error = OracleDB.Err_NoUpdate;
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool UpdateStockCheckStatus(string batchno, string status, string userID ,ArrayList arr)
        {
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            bool ret = true;

            try
            {
                string sql = "UPDATE STOCKCHECK SET STATUS = '" + status + "', ";
                sql += "UPDATEBY = '" + userID + "', ";
                sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
                sql += "WHERE BATCHNO='" + batchno + "'";

                ret = (OracleDB.ExecNonQueryCmd(sql, obj.zTrans) > 0);
                if (!ret)
                {
                    throw new ApplicationException(OracleDB.Err_NoExistUpdate);
                }

                StockCheckImproveDAL itemDAL = new StockCheckImproveDAL();

                for (Int16 i = 0; i < arr.Count; i++)
                {
                    itemDAL.OnDB = false;
                    StockCheckImproveData item = (StockCheckImproveData)arr[i];
                    double ans = item.SYSQTY + item.IMPROVEQTY;
                    itemDAL.STOCKCHECK = item.STOCKCHECK;
                    itemDAL.PRODUCTSTOCK = item.PRODUCTSTOCK;
                    itemDAL.SYSQTY = item.SYSQTY;
                    itemDAL.IMPROVEQTY = ans;
                    itemDAL.REASON = item.REASON;

                    ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);

                    sql = "UPDATE PRODUCTSTOCK SET QTY = '" + ans.ToString() + "', ";
                    sql += "UPDATEBY = '" + userID + "', ";
                    sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
                    sql += "WHERE LOID='" + item.PRODUCTSTOCK.ToString() + "'";

                    ret = (OracleDB.ExecNonQueryCmd(sql, obj.zTrans) > 0);
                    if (!ret)
                    {
                        throw new ApplicationException(OracleDB.Err_NoExistUpdate);
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

        public bool InsertNewBatchNo(string userID, StockCheckData data)
        {
            bool ret = true;
            StockCheckDAL itemDAL = new StockCheckDAL();
            OracleDBObj obj = new OracleDBObj();
            obj.CreateTransaction();
            try
            {
                itemDAL.BATCHNO = GenNewBatchNo(data.WAREHOUSE, obj.zTrans);
                itemDAL.CHECKDATE = DateTime.Now;
                itemDAL.WAREHOUSE = data.WAREHOUSE;
                itemDAL.STATUS = Constz.Requisition.Status.Waiting.Code;

                ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
                _LOID = itemDAL.LOID;

                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
        }

        private string GenNewBatchNo(double warehouse, System.Data.OracleClient.OracleTransaction trans )
        {
            string last = "00001";
            string sql = "SELECT NVL(MAX(LOID),0) FROM STOCKCHECK ";
            DataTable dt = OracleDB.ExecListCmd(sql, trans);
            if (dt.Rows.Count != 0)
            {
                last = String.Format("{0:00000}", Convert.ToInt32(dt.Rows[0][0]) + 1);
                if (last.Length > 5)
                {
                    last = last.Substring(0, last.Length - 5);
                }
            }

            WarehouseDAL wDAL = new WarehouseDAL();
            wDAL.GetDataByLOID(warehouse, trans);

            return String.Format(wDAL.CODE + "{0:yyMM}", DateTime.Now) + last;
            //return OracleDB.GetRunningCode("STOCKCHECK", "STOCKCHECK");
        }
    }
}
