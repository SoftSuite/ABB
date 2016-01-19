using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;
using ABB.Data.Inventory.WH;

namespace ABB.DAL
{
    public class StockOutDAL
    {
        #region Public Method

        /// <summary>
        /// Insert Data From Object to DB
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool InsertCurrentData(string UserID, OracleTransaction zTrans)
        {
            _CREATEBY = UserID;
            _CREATEON = DateTime.Now;
            return doInsert(zTrans);
        }

        /// <summary>
        /// Update Data From Object to DB
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool UpdateCurrentData(string UserID, OracleTransaction zTrans)
        {
            _UPDATEBY = UserID;
            _UPDATEON = DateTime.Now;
            return doUpdate(" LOID = " + _LOID.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zLOID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByLOID(double zLOID, OracleTransaction zTrans)
        {
            return doGetdata(" LOID = " + zLOID.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteCurrentData(OracleTransaction zTrans)
        {
            return doDelete(" LOID = " + _LOID.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Get Data List of This Table
        /// </summary>
        /// <param name="whereCause"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public DataTable GetDataList(string whereCause, OracleTransaction zTrans)
        {
            return OracleDB.ExecListCmd(sql_select + whereCause);
        }

        #endregion

        #region Constant

        private string tableName = "STOCKOUT";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CODE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _APPROVER = 0;
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _ACTIVE = "";
        string _REFTABLE = "";
        double _REFLOID = 0;
        double _RECEIVER = 0;
        double _DOCTYPE = 0;
        string _STATUS = "";
        string _INVCODE = "";
        string _REASON = "";
        string _REMARK = "";
        double _SENDER = 0;
        double _CTITLE = 0;
        string _CNAME = "";
        string _CLASTNAME = "";
        string _CADDRESS = "";
        string _CTEL = "";
        string _CFAX = "";
        DateTime _DELIVERYDATE = new DateTime(1, 1, 1);
        DateTime _REQDATE = new DateTime(1, 1, 1);
        string _PRODUCTREF = "";
        double _PRODUCTLOID = 0;
        double _DIVISION = 0;
        string _SUPPORTREFCODE = "";
        string _SUPPORTCAUSE = "";
        #endregion

        #region Public Property
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        public string ErrorMessage
        {
            get { return _error; }
            set { _error = value; }
        }
        public bool OnDB
        {
            get { return _OnDB; }
            set { _OnDB = value; }
        }
        public double LOID
        {
            get { return _LOID; }
        }
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public string CREATEBY
        {
            get { return _CREATEBY; }
        }
        public DateTime CREATEON
        {
            get { return _CREATEON; }
        }
        public string UPDATEBY
        {
            get { return _UPDATEBY; }
        }
        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
        }
        public double APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }
        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }
        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }
        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }
        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }
        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public double SENDER
        {
            get { return _SENDER; }
            set { _SENDER = value; }
        }
        public double CTITLE
        {
            get { return _CTITLE; }
            set { _CTITLE = value; }
        }
        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }
        public string CLASTNAME
        {
            get { return _CLASTNAME; }
            set { _CLASTNAME = value; }
        }
        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }
        public string CTEL
        {
            get { return _CTEL; }
            set { _CTEL = value; }
        }
        public string CFAX
        {
            get { return _CFAX; }
            set { _CFAX = value; }
        }
        public DateTime DELIVERYDATE
        {
            get { return _DELIVERYDATE; }
            set { _DELIVERYDATE = value; }
        }
        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }
        public string PRODUCTREF
        {
            get { return _PRODUCTREF; }
            set { _PRODUCTREF = value; }
        }
        public double PRODUCTLOID
        {
            get { return _PRODUCTLOID; }
            set { _PRODUCTLOID = value; }
        }
        public double DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }
        public string SUPPORTCAUSE 
        {
            get { return _SUPPORTCAUSE; }
            set { _SUPPORTCAUSE = value; }
        }
        public string SUPPORTREFCODE
        {
            get { return _SUPPORTREFCODE; }
            set { _SUPPORTREFCODE = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CODE,CREATEBY,CREATEON,UPDATEBY,UPDATEON,APPROVER,APPROVEDATE,ACTIVE,REFTABLE,REFLOID,RECEIVER,DOCTYPE,STATUS,INVCODE,REASON,REMARK,SENDER,CTITLE,CNAME,CLASTNAME,CADDRESS,CTEL,CFAX,DELIVERYDATE,REQDATE,PRODUCTREF,PRODUCTLOID,DIVISION,SUPPORTREFCODE, SUPPORTCAUSE)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += "  " + _APPROVER.ToString() + ",";// APPROVER";
                sqlz += " " + OracleDB.QRDateTime(_APPROVEDATE) + ",";// APPROVEDATE";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_REFTABLE) + "',";// REFTABLE";
                sqlz += "  " + _REFLOID.ToString() + ",";// REFLOID";
                sqlz += "  " + _RECEIVER.ToString() + ",";// RECEIVER";
                sqlz += "  " + _DOCTYPE.ToString() + ",";// DOCTYPE";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " '" + OracleDB.QRText(_INVCODE) + "',";// INVCODE";
                sqlz += " '" + OracleDB.QRText(_REASON) + "',";// REASON";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += "  " + _SENDER.ToString() + ",";// SENDER";
                sqlz += "  " + _CTITLE.ToString() + ",";// CTITLE";
                sqlz += " '" + OracleDB.QRText(_CNAME) + "',";// CNAME";
                sqlz += " '" + OracleDB.QRText(_CLASTNAME) + "',";// CLASTNAME";
                sqlz += " '" + OracleDB.QRText(_CADDRESS) + "',";// CADDRESS";
                sqlz += " '" + OracleDB.QRText(_CTEL) + "',";// CTEL";
                sqlz += " '" + OracleDB.QRText(_CFAX) + "',";// CFAX";
                sqlz += " " + OracleDB.QRDateTime(_DELIVERYDATE) + ",";// DELIVERYDATE";
                sqlz += " " + OracleDB.QRDateTime(_REQDATE) + ",";// REQDATE";
                sqlz += " '" + OracleDB.QRText(_PRODUCTREF) + "',";// PRODUCTREF";
                sqlz += "  " + _PRODUCTLOID.ToString() + ",";// PRODUCTLOID";
                sqlz += "  " + _DIVISION.ToString() + ",";// DIVISION";
                sqlz += " '" + OracleDB.QRText(_SUPPORTREFCODE) + "',";// SUPPORTREFCODE";
                sqlz += " '" + OracleDB.QRText(_SUPPORTCAUSE) + "'";// SUPPORTCAUSE";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " APPROVER  = " + _APPROVER.ToString() + ", ";
                sqlz += " APPROVEDATE  = " + OracleDB.QRDateTime(_APPROVEDATE) + ", ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " REFTABLE  = '" + OracleDB.QRText(_REFTABLE) + "', ";
                sqlz += " REFLOID  = " + _REFLOID.ToString() + ", ";
                sqlz += " RECEIVER  = " + _RECEIVER.ToString() + ", ";
                sqlz += " DOCTYPE  = " + _DOCTYPE.ToString() + ", ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " INVCODE  = '" + OracleDB.QRText(_INVCODE) + "', ";
                sqlz += " REASON  = '" + OracleDB.QRText(_REASON) + "', ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " SENDER  = " + _SENDER.ToString() + ", ";
                sqlz += " CTITLE  = " + _CTITLE.ToString() + ", ";
                sqlz += " CNAME  = '" + OracleDB.QRText(_CNAME) + "', ";
                sqlz += " CLASTNAME  = '" + OracleDB.QRText(_CLASTNAME) + "', ";
                sqlz += " CADDRESS  = '" + OracleDB.QRText(_CADDRESS) + "', ";
                sqlz += " CTEL  = '" + OracleDB.QRText(_CTEL) + "', ";
                sqlz += " CFAX  = '" + OracleDB.QRText(_CFAX) + "', ";
                sqlz += " DELIVERYDATE  = " + OracleDB.QRDateTime(_DELIVERYDATE) + ", ";
                sqlz += " REQDATE  = " + OracleDB.QRDateTime(_REQDATE) + ", ";
                sqlz += " PRODUCTREF  = '" + OracleDB.QRText(_PRODUCTREF) + "', ";
                sqlz += " PRODUCTLOID  = " + _PRODUCTLOID.ToString() + ", ";
                sqlz += " DIVISION  = " + _DIVISION.ToString() + ", ";
                sqlz += " SUPPORTCAUSE  = '" + OracleDB.QRText(_SUPPORTCAUSE) + "', ";
                sqlz += " SUPPORTREFCODE  = '" + OracleDB.QRText(_SUPPORTREFCODE) + "' ";
                sqlz += "  ";
                return sqlz;
            }
        }
        private string sql_delete
        {
            get
            {
                string sqlz = " DELETE FROM " + tableName + " ";
                return sqlz;
            }
        }

        private string sql_select
        {
            get
            {
                string sqlz = " SELECT * FROM " + tableName + " ";
                return sqlz;
            }
        }
        #endregion

        #region Internal Method
        private bool doInsert(OracleTransaction zTrans)
        {
            bool ret = true;
            if (!_OnDB)
            {
                try
                {
                    _LOID = OracleDB.GetLOID(tableName, zTrans);
                    _CODE = OracleDB.GetRunningCode(TableName, DOCTYPE.ToString(), zTrans);
                    ret = (OracleDB.ExecNonQueryCmd(sql_insert, zTrans) > 0);
                    if (!ret) _error = OracleDB.Err_NoInsert;
                    else _OnDB = true;
                }
                catch (OracleException ex)
                {
                    ret = false;
                    _error = OracleDB.GetOracleExceptionText(ex);
                }
                catch (Exception ex)
                {
                    ret = false;
                    _error = ex.Message;
                }
            }

            return ret;
        }

        private bool doUpdate(string whText, OracleTransaction zTrans)
        {
            bool ret = true;
            if (_OnDB)
            {
                if (whText.Trim() != "")
                {
                    string tmpWhere = " WHERE " + whText;
                    try
                    {
                        ret = (OracleDB.ExecNonQueryCmd(sql_update + tmpWhere, zTrans) > 0);
                        if (!ret) _error = OracleDB.Err_NoUpdate;
                    }
                    catch (OracleException ex)
                    {
                        ret = false;
                        _error = OracleDB.GetOracleExceptionText(ex);
                    }
                    catch (Exception ex)
                    {
                        ret = false;
                        _error = ex.Message;
                    }
                }
                else
                {
                    ret = false;
                    _error = OracleDB.Err_UpdateNoWhere;
                }
            }
            else
            {
                ret = false;
                _error = OracleDB.Err_NoExistUpdate;
            }
            return ret;

        }

        private bool doDelete(string whText, OracleTransaction zTrans)
        {
            bool ret = true;
            if (whText.Trim() != "")
            {
                string tmpWhere = " WHERE " + whText;
                try
                {
                    ret = (OracleDB.ExecNonQueryCmd(sql_delete + tmpWhere, zTrans) > 0);
                    if (!ret) _error = OracleDB.Err_NoDelete;
                    else _OnDB = false;
                }
                catch (OracleException ex)
                {
                    ret = false;
                    _error = OracleDB.GetOracleExceptionText(ex);
                }
                catch (Exception ex)
                {
                    ret = false;
                    _error = ex.Message;
                }
            }
            else
            {
                ret = false;
                _error = OracleDB.Err_DeleteNoWhere;
            }

            return ret;
        }

        private bool doGetdata(string whText, OracleTransaction zTrans)
        {
            bool ret = true;
            if (whText.Trim() != "")
            {
                string tmpWhere = " WHERE " + whText;
                OracleDataReader zRdr = null;
                try
                {
                    zRdr = OracleDB.ExecQueryCmd(sql_select + tmpWhere, zTrans);
                    if (zRdr.Read())
                    {
                        _OnDB = true;
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["APPROVER"])) _APPROVER = Convert.ToDouble(zRdr["APPROVER"]);
                        if (!Convert.IsDBNull(zRdr["APPROVEDATE"])) _APPROVEDATE = OracleDB.DBDate(zRdr["APPROVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFTABLE"])) _REFTABLE = zRdr["REFTABLE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFLOID"])) _REFLOID = Convert.ToDouble(zRdr["REFLOID"]);
                        if (!Convert.IsDBNull(zRdr["RECEIVER"])) _RECEIVER = Convert.ToDouble(zRdr["RECEIVER"]);
                        if (!Convert.IsDBNull(zRdr["DOCTYPE"])) _DOCTYPE = Convert.ToDouble(zRdr["DOCTYPE"]);
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["INVCODE"])) _INVCODE = zRdr["INVCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REASON"])) _REASON = zRdr["REASON"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDER"])) _SENDER = Convert.ToDouble(zRdr["SENDER"]);
                        if (!Convert.IsDBNull(zRdr["CTITLE"])) _CTITLE = Convert.ToDouble(zRdr["CTITLE"]);
                        if (!Convert.IsDBNull(zRdr["CNAME"])) _CNAME = zRdr["CNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CLASTNAME"])) _CLASTNAME = zRdr["CLASTNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CADDRESS"])) _CADDRESS = zRdr["CADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTEL"])) _CTEL = zRdr["CTEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["CFAX"])) _CFAX = zRdr["CFAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["DELIVERYDATE"])) _DELIVERYDATE = OracleDB.DBDate(zRdr["DELIVERYDATE"]);
                        if (!Convert.IsDBNull(zRdr["REQDATE"])) _REQDATE = OracleDB.DBDate(zRdr["REQDATE"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCTREF"])) _PRODUCTREF = zRdr["PRODUCTREF"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCTLOID"])) _PRODUCTLOID = Convert.ToDouble(zRdr["PRODUCTLOID"]);
                        if (!Convert.IsDBNull(zRdr["DIVISION"])) _DIVISION = Convert.ToDouble(zRdr["DIVISION"]);
                        if (!Convert.IsDBNull(zRdr["SUPPORTCAUSE"])) _SUPPORTCAUSE = zRdr["SUPPORTCAUSE"].ToString();
                        if (!Convert.IsDBNull(zRdr["SUPPORTREFCODE"])) _SUPPORTREFCODE = zRdr["SUPPORTREFCODE"].ToString();
                    }
                    else
                    {
                        ret = false;
                        _error = OracleDB.Err_NoSelect;
                    }
                    zRdr.Close();
                }
                catch (OracleException ex)
                {
                    ret = false;
                    _error = OracleDB.GetOracleExceptionText(ex);
                    if (zRdr != null && !zRdr.IsClosed)
                        zRdr.Close();
                }
                catch (Exception ex)
                {
                    ret = false;
                    _error = ex.Message;
                    if (zRdr != null && !zRdr.IsClosed)
                        zRdr.Close();
                }
            }
            else
            {
                ret = false;
                _error = "No data found.";
            }
            return ret;
        }
        #endregion

        public double TotalReference(string refTable, double refLOID, OracleTransaction zTrans)
        {
            string sql = "SELECT COUNT(*) FROM " + tableName + " WHERE REFTABLE = '" + refTable + "' AND REFLOID = " + refLOID.ToString() + " ";
            return Convert.ToDouble(OracleDB.ExecSingleCmd(sql, zTrans));
        }

        ///// <summary>
        ///// Update requisition.
        ///// </summary>
        ///// <param name="loid">The requisition key.</param>
        ///// <param name="status">The new status.</param>
        ///// <returns></returns>
        //public bool UpdateStockOutStatus(double loid, string status, string userID)
        //{
        //    return UpdateStockOutStatus(loid, status, userID, null);
        //}

        ///// <summary>
        ///// Update requisition.
        ///// </summary>
        ///// <param name="loid">The requisition key.</param>
        ///// <param name="status">The new status.</param>
        ///// <param name="zTrans"></param>
        ///// <returns></returns>
        //public bool UpdateStockOutStatus(double loid, string status, string userID, OracleTransaction zTrans)
        //{
        //    string sql = "UPDATE " + tableName + " SET STATUS = '" + status + "', ";
        //    sql += "UPDATEBY = '" + userID + "', ";
        //    sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
        //    sql += "WHERE LOID = " + loid.ToString() + " ";
        //    //+(status == Constz.Requisition.Status.Approved.Code ? "AND STATUS = '" + Constz.Requisition.Status.Waiting.Code + "' " : (status == Constz.Requisition.Status.Void.Code ? "AND STATUS = '" + Constz.Requisition.Status.Approved.Code + "' " : ""));
        //    bool ret = true;
        //    try
        //    {
        //        ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
        //        if (!ret) _error = OracleDB.Err_NoUpdate;
        //    }
        //    catch (Exception ex)
        //    {
        //        ret = false;
        //        _error = ex.Message;
        //    }
        //    return ret;
        //}

        public double GetStockOutByReference(double refLOID, string refTable, double excludeStockOut, OracleTransaction zTrans)
        {
            string sql = "SELECT COUNT(LOID) FROM STOCKOUT ";
            sql += "WHERE REFTABLE = '" + refTable + "' AND REFLOID = " + refLOID.ToString() + " AND LOID <> " + excludeStockOut.ToString() + " ";
            sql += "AND STATUS <> '" + Constz.Requisition.Status.Void.Code + "' ";
            return Convert.ToDouble(OracleDB.ExecSingleCmd(sql));
        }

        public double SumPrice(double stockOut, OracleTransaction zTrans)
        {
            string sql = "SELECT SUM(PRICE*QTY) FROM STOCKOUTITEM WHERE STOCKOUT = " + stockOut.ToString();
            object ret = OracleDB.ExecSingleCmd(sql, zTrans);
            if (Convert.IsDBNull(ret)) return 0;
            else
                return Convert.ToDouble(ret);
        }

        public double GetDoctype(string requisitiontype)
        {
            string sql = "SELECT LOID FROM DOCTYPE WHERE REQUISITIONTYPE = '" + requisitiontype + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double LOID = 0;
            if (dt.Rows.Count > 0)
            {
                LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
            }

            return LOID;
        }

        public double GetRequisitiontype(double doctype)
        {
            string sql = "SELECT REQUISITIONTYPE FROM DOCTYPE WHERE LOID = '" + doctype + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double REQUISITIONTYPE = 0;
            if (dt.Rows.Count > 0)
            {
                REQUISITIONTYPE = (Convert.ToDouble(dt.Rows[0]["REQUISITIONTYPE"] == DBNull.Value ? "0" : dt.Rows[0]["REQUISITIONTYPE"]));
            }

            return REQUISITIONTYPE;
        }

        public string GetRequisitionCode(double loid)
        {
            string sql = "SELECT CODE FROM REQUISITION WHERE LOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            string CODE = "";
            if (dt.Rows.Count > 0)
            {
                CODE = dt.Rows[0]["CODE"].ToString();
            }

            return CODE;
        }

        public string GetPDOrderCode(double loid)
        {
            string sql = "SELECT CODE FROM PDORDER WHERE LOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            string CODE = "";
            if (dt.Rows.Count > 0)
            {
                CODE = dt.Rows[0]["CODE"].ToString();
            }

            return CODE;
        }

        public double GetRequisitionTotal(double loid)
        {
            string sql = "SELECT TOTAL FROM REQUISITION WHERE LOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double TOTAL = 0;
            if (dt.Rows.Count > 0)
            {
                TOTAL = Convert.ToDouble(dt.Rows[0]["TOTAL"]);
            }

            return TOTAL;
        }


        public StockoutWHData DoGetProduct(double loid)
        {
            string sql = "SELECT PO_LOID AS LOID,POI_LOID, POCODE AS CODE, ORDERDATE AS REQDATE, DUEDATE, PD_LOID AS PRODUCT, PDCODE AS PRODUCTCODE, PDNAME AS PRODUCTNAME, QTY AS STDQTY, UNAME AS UNIT, S_LOID  FROM V_PDORDER_LIST WHERE POI_LOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            StockoutWHData data = new StockoutWHData();
            if (dt.Rows.Count > 0)
            {
                data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.REQDATE = dt.Rows[0]["REQDATE"] == DBNull.Value ? new DateTime(1, 1, 1) : Convert.ToDateTime(dt.Rows[0]["REQDATE"]);
                data.DUEDATE = dt.Rows[0]["DUEDATE"] == DBNull.Value ? new DateTime(1, 1, 1) : Convert.ToDateTime(dt.Rows[0]["DUEDATE"]);
                data.PRODUCT = dt.Rows[0]["PRODUCT"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["PRODUCT"]);
                data.PRODUCTCODE = dt.Rows[0]["PRODUCTCODE"] == DBNull.Value ? "" : dt.Rows[0]["PRODUCTCODE"].ToString();
                data.PRODUCTNAME = dt.Rows[0]["PRODUCTNAME"] == DBNull.Value ? "" : dt.Rows[0]["PRODUCTNAME"].ToString();
                data.QTY = dt.Rows[0]["STDQTY"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["STDQTY"]);
                data.UNIT = dt.Rows[0]["UNIT"] == DBNull.Value ? "" : dt.Rows[0]["UNIT"].ToString();
                data.CUSTOMER = dt.Rows[0]["S_LOID"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["S_LOID"].ToString());
                data.REFPROD = dt.Rows[0]["POI_LOID"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["POI_LOID"]);
            }

            return data;
        }

        public StockoutWHData DoGetReqProduct(double loid)
        {
            string sql = "SELECT RQLOID AS LOID,POLOID, RQCODE AS CODE, REQDATE, DUEDATE, PDCODE AS PRODUCTCODE, PDLOID AS PRODUCT, PDNAME AS PRODUCTNAME, QTY AS STDQTY, UNAME AS UNIT, WAREHOUSE FROM V_REQUISITION_PROD WHERE RQLOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            StockoutWHData data = new StockoutWHData();
            if (dt.Rows.Count > 0)
            {
                data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.REQDATE = dt.Rows[0]["REQDATE"] == DBNull.Value ? new DateTime(1, 1, 1) : Convert.ToDateTime(dt.Rows[0]["REQDATE"]);
                data.DUEDATE = dt.Rows[0]["DUEDATE"] == DBNull.Value ? new DateTime(1, 1, 1) : Convert.ToDateTime(dt.Rows[0]["DUEDATE"]);
                data.PRODUCT = dt.Rows[0]["PRODUCT"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["PRODUCT"]);
                data.PRODUCTCODE = dt.Rows[0]["PRODUCTCODE"] == DBNull.Value ? "" : dt.Rows[0]["PRODUCTCODE"].ToString();
                data.PRODUCTNAME = dt.Rows[0]["PRODUCTNAME"] == DBNull.Value ? "" : dt.Rows[0]["PRODUCTNAME"].ToString();
                data.QTY = dt.Rows[0]["STDQTY"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["STDQTY"]);
                data.UNIT = dt.Rows[0]["UNIT"] == DBNull.Value ? "" : dt.Rows[0]["UNIT"].ToString();
                data.CUSTOMER = dt.Rows[0]["WAREHOUSE"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["WAREHOUSE"].ToString());
                data.REFPROD = dt.Rows[0]["POLOID"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["POLOID"]);
            }

            return data;
        }


        public bool CutStock(double stockOut, string userID, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                //if (Convert.ToDouble(OracleDB.ExecSingleCmd("SELECT COUNT(LOID) FROM STOCKOUTITEM WHERE LOTNO IS NULL AND STOCKOUT = " + stockOut.ToString(), trans)) > 0)
                //{
                //    ret = false;
                //    _error = "ไม่สามารถทำรายการได้ เนื่องจากสินค้าบางรายการไม่ได้ระบุ Lot No.";
                //}
                //else
                OracleDB.ExecNonQueryCmd("CALL SP_CUTSTOCKOUT(" + stockOut.ToString() + ", '" + userID + "')", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool UpdatePDProductStatus(double loid, string status, string userID, OracleTransaction zTrans)
        {
            string sql = "UPDATE PDPRODUCT SET PRODSTATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE LOID = " + loid.ToString() + " ";
            bool ret = true;
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
                if (!ret) _error = OracleDB.Err_NoUpdate;
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool UpdatePDOrderStatus(double loid, string status, string userID, OracleTransaction zTrans)
        {
            string sql = "SELECT PDORDER FROM PDPRODUCT WHERE LOID = " + loid;
            DataTable dt = OracleDB.ExecListCmd(sql);
            double PDORDER = 0;
            if (dt.Rows.Count > 0)
            {
                PDORDER = Convert.ToDouble(dt.Rows[0]["PDORDER"]);
            }

            sql = "UPDATE PDORDER SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE LOID = " + PDORDER.ToString() + " ";
            bool ret = true;
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
                if (!ret) _error = OracleDB.Err_NoUpdate;
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

    }
}
