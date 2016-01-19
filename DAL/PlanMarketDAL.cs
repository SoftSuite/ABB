using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;
using ABB.Data.Sales;

namespace ABB.DAL
{
    public class PlanMarketDAL
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

        private string tableName = "PLANMARKET";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _M11 = 0;
        double _M12 = 0;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _PLAN = 0;
        double _CUSTOMER = 0;
        double _PERCENT = 0;
        double _RANK = 0;
        string _STATUS = "";
        double _M1 = 0;
        double _M2 = 0;
        double _M3 = 0;
        double _M4 = 0;
        double _M5 = 0;
        double _M6 = 0;
        double _M7 = 0;
        double _M8 = 0;
        double _M9 = 0;
        double _M10 = 0;
        #endregion

        #region Public Property
        public string TableName
        {
            get { return tableName; }
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
        public double M11
        {
            get { return _M11; }
            set { _M11 = value; }
        }
        public double M12
        {
            get { return _M12; }
            set { _M12 = value; }
        }

        public double LOID
        {
            get { return _LOID; }
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
        public double PLAN
        {
            get { return _PLAN; }
            set { _PLAN = value; }
        }
        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }
        public double PERCENT
        {
            get { return _PERCENT; }
            set { _PERCENT = value; }
        }
        public double RANK
        {
            get { return _RANK; }
            set { _RANK = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public double M1
        {
            get { return _M1; }
            set { _M1 = value; }
        }
        public double M2
        {
            get { return _M2; }
            set { _M2 = value; }
        }
        public double M3
        {
            get { return _M3; }
            set { _M3 = value; }
        }
        public double M4
        {
            get { return _M4; }
            set { _M4 = value; }
        }
        public double M5
        {
            get { return _M5; }
            set { _M5 = value; }
        }
        public double M6
        {
            get { return _M6; }
            set { _M6 = value; }
        }
        public double M7
        {
            get { return _M7; }
            set { _M7 = value; }
        }
        public double M8
        {
            get { return _M8; }
            set { _M8 = value; }
        }
        public double M9
        {
            get { return _M9; }
            set { _M9 = value; }
        }
        public double M10
        {
            get { return _M10; }
            set { _M10 = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (M11,M12,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,PLAN,CUSTOMER,PERCENT,RANK,STATUS,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10)VALUES(";
                sqlz += "  " + _M11.ToString() + ",";// M11";
                sqlz += "  " + _M12.ToString() + ",";// M12";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += "  " + _PLAN.ToString() + ",";// PLAN";
                sqlz += "  " + _CUSTOMER.ToString() + ",";// CUSTOMER";
                sqlz += "  " + _PERCENT.ToString() + ",";// PERCENT";
                sqlz += "  " + _RANK.ToString() + ",";// RANK";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += "  " + _M1.ToString() + ",";// M1";
                sqlz += "  " + _M2.ToString() + ",";// M2";
                sqlz += "  " + _M3.ToString() + ",";// M3";
                sqlz += "  " + _M4.ToString() + ",";// M4";
                sqlz += "  " + _M5.ToString() + ",";// M5";
                sqlz += "  " + _M6.ToString() + ",";// M6";
                sqlz += "  " + _M7.ToString() + ",";// M7";
                sqlz += "  " + _M8.ToString() + ",";// M8";
                sqlz += "  " + _M9.ToString() + ",";// M9";
                sqlz += "  " + _M10.ToString() + "";// M10";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " M11  = " + _M11.ToString() + ", ";
                sqlz += " M12  = " + _M12.ToString() + ", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " PLAN  = " + _PLAN.ToString() + ", ";
                sqlz += " CUSTOMER  = " + _CUSTOMER.ToString() + ", ";
                sqlz += " PERCENT  = " + _PERCENT.ToString() + ", ";
                sqlz += " RANK  = " + _RANK.ToString() + ", ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " M1  = " + _M1.ToString() + ", ";
                sqlz += " M2  = " + _M2.ToString() + ", ";
                sqlz += " M3  = " + _M3.ToString() + ", ";
                sqlz += " M4  = " + _M4.ToString() + ", ";
                sqlz += " M5  = " + _M5.ToString() + ", ";
                sqlz += " M6  = " + _M6.ToString() + ", ";
                sqlz += " M7  = " + _M7.ToString() + ", ";
                sqlz += " M8  = " + _M8.ToString() + ", ";
                sqlz += " M9  = " + _M9.ToString() + ", ";
                sqlz += " M10  = " + _M10.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["M11"])) _M11 = Convert.ToDouble(zRdr["M11"]);
                        if (!Convert.IsDBNull(zRdr["M12"])) _M12 = Convert.ToDouble(zRdr["M12"]);
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["PLAN"])) _PLAN = Convert.ToDouble(zRdr["PLAN"]);
                        if (!Convert.IsDBNull(zRdr["CUSTOMER"])) _CUSTOMER = Convert.ToDouble(zRdr["CUSTOMER"]);
                        if (!Convert.IsDBNull(zRdr["PERCENT"])) _PERCENT = Convert.ToDouble(zRdr["PERCENT"]);
                        if (!Convert.IsDBNull(zRdr["RANK"])) _RANK = Convert.ToDouble(zRdr["RANK"]);
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["M1"])) _M1 = Convert.ToDouble(zRdr["M1"]);
                        if (!Convert.IsDBNull(zRdr["M2"])) _M2 = Convert.ToDouble(zRdr["M2"]);
                        if (!Convert.IsDBNull(zRdr["M3"])) _M3 = Convert.ToDouble(zRdr["M3"]);
                        if (!Convert.IsDBNull(zRdr["M4"])) _M4 = Convert.ToDouble(zRdr["M4"]);
                        if (!Convert.IsDBNull(zRdr["M5"])) _M5 = Convert.ToDouble(zRdr["M5"]);
                        if (!Convert.IsDBNull(zRdr["M6"])) _M6 = Convert.ToDouble(zRdr["M6"]);
                        if (!Convert.IsDBNull(zRdr["M7"])) _M7 = Convert.ToDouble(zRdr["M7"]);
                        if (!Convert.IsDBNull(zRdr["M8"])) _M8 = Convert.ToDouble(zRdr["M8"]);
                        if (!Convert.IsDBNull(zRdr["M9"])) _M9 = Convert.ToDouble(zRdr["M9"]);
                        if (!Convert.IsDBNull(zRdr["M10"])) _M10 = Convert.ToDouble(zRdr["M10"]);
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

        public DataTable GetDataByPlan(double plan, OracleTransaction zTrans)
        {
            string tmpWhere = " WHERE PLAN = " + plan.ToString();
            return OracleDB.ExecListCmd(sql_select + tmpWhere, zTrans);
        }

        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteDataByPlan(double plan, OracleTransaction zTrans)
        {
            return doDelete(" PLAN = " + plan.ToString() + " ", zTrans);
        }

        public bool UpdateStatusByPlan(double plan, string status, string userID, OracleTransaction zTrans)
        {
            bool ret = true;
            string sql = "UPDATE " + TableName + " SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
            sql += "WHERE PLAN = " + plan.ToString() + " ";
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
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
            return ret;
        }

        public PlanMarketingData DoGetValueFront(int year)
        {
            string sql = "SELECT ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '01' THEN GRANDTOT ELSE 0 END) M1, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '02' THEN GRANDTOT ELSE 0 END) M2, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '03' THEN GRANDTOT ELSE 0 END) M3, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '04' THEN GRANDTOT ELSE 0 END) M4, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '05' THEN GRANDTOT ELSE 0 END) M5, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '06' THEN GRANDTOT ELSE 0 END) M6, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '07' THEN GRANDTOT ELSE 0 END) M7, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '08' THEN GRANDTOT ELSE 0 END) M8, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '09' THEN GRANDTOT ELSE 0 END) M9, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '10' THEN GRANDTOT ELSE 0 END) M10, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '11' THEN GRANDTOT ELSE 0 END) M11, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '12' THEN GRANDTOT ELSE 0 END) M12 ";
            sql += "FROM REQUISITION WHERE REQUISITIONTYPE = 13 AND TO_CHAR(REQDATE,'YYYY') = '" + year + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            PlanMarketingData data = new PlanMarketingData();
            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["M1"])) data.M1 = Convert.ToDouble(dt.Rows[0]["M1"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M2"])) data.M2 = Convert.ToDouble(dt.Rows[0]["M2"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M3"])) data.M3 = Convert.ToDouble(dt.Rows[0]["M3"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M4"])) data.M4 = Convert.ToDouble(dt.Rows[0]["M4"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M5"])) data.M5 = Convert.ToDouble(dt.Rows[0]["M5"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M6"])) data.M6 = Convert.ToDouble(dt.Rows[0]["M6"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M7"])) data.M7 = Convert.ToDouble(dt.Rows[0]["M7"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M8"])) data.M8 = Convert.ToDouble(dt.Rows[0]["M8"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M9"])) data.M9 = Convert.ToDouble(dt.Rows[0]["M9"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M10"])) data.M10 = Convert.ToDouble(dt.Rows[0]["M10"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M11"])) data.M11 = Convert.ToDouble(dt.Rows[0]["M11"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M12"])) data.M12 = Convert.ToDouble(dt.Rows[0]["M12"]);

            }

            return data;
        }

        public PlanMarketingData DoGetValueOther(int year)
        {
            string sql = "SELECT ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '01' THEN GRANDTOT ELSE 0 END) M1, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '02' THEN GRANDTOT ELSE 0 END) M2, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '03' THEN GRANDTOT ELSE 0 END) M3, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '04' THEN GRANDTOT ELSE 0 END) M4, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '05' THEN GRANDTOT ELSE 0 END) M5, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '06' THEN GRANDTOT ELSE 0 END) M6, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '07' THEN GRANDTOT ELSE 0 END) M7, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '08' THEN GRANDTOT ELSE 0 END) M8, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '09' THEN GRANDTOT ELSE 0 END) M9, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '10' THEN GRANDTOT ELSE 0 END) M10, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '11' THEN GRANDTOT ELSE 0 END) M11, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '12' THEN GRANDTOT ELSE 0 END) M12 ";
            sql += "FROM REQUISITION ";
            sql += "WHERE REQUISITIONTYPE = 11 AND CUSTOMER IN (SELECT LOID FROM CUSTOMER WHERE MEMBERTYPE <> 71 AND LOID <> 1) AND TO_CHAR(REQDATE,'YYYY') = '" + year + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            PlanMarketingData data = new PlanMarketingData();
            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["M1"])) data.M1 = Convert.ToDouble(dt.Rows[0]["M1"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M2"])) data.M2 = Convert.ToDouble(dt.Rows[0]["M2"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M3"])) data.M3 = Convert.ToDouble(dt.Rows[0]["M3"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M4"])) data.M4 = Convert.ToDouble(dt.Rows[0]["M4"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M5"])) data.M5 = Convert.ToDouble(dt.Rows[0]["M5"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M6"])) data.M6 = Convert.ToDouble(dt.Rows[0]["M6"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M7"])) data.M7 = Convert.ToDouble(dt.Rows[0]["M7"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M8"])) data.M8 = Convert.ToDouble(dt.Rows[0]["M8"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M9"])) data.M9 = Convert.ToDouble(dt.Rows[0]["M9"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M10"])) data.M10 = Convert.ToDouble(dt.Rows[0]["M10"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M11"])) data.M11 = Convert.ToDouble(dt.Rows[0]["M11"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M12"])) data.M12 = Convert.ToDouble(dt.Rows[0]["M12"]);

            }

            return data;
        }

        public PlanMarketingData DoGetValue(int year,double customer)
        {
            string sql = "SELECT ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '01' THEN GRANDTOT ELSE 0 END) M1, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '02' THEN GRANDTOT ELSE 0 END) M2, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '03' THEN GRANDTOT ELSE 0 END) M3, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '04' THEN GRANDTOT ELSE 0 END) M4, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '05' THEN GRANDTOT ELSE 0 END) M5, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '06' THEN GRANDTOT ELSE 0 END) M6, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '07' THEN GRANDTOT ELSE 0 END) M7, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '08' THEN GRANDTOT ELSE 0 END) M8, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '09' THEN GRANDTOT ELSE 0 END) M9, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '10' THEN GRANDTOT ELSE 0 END) M10, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '11' THEN GRANDTOT ELSE 0 END) M11, ";
            sql += "SUM(CASE TO_CHAR(REQDATE,'MM') WHEN '12' THEN GRANDTOT ELSE 0 END) M12 ";
            sql += "FROM REQUISITION ";
            sql += "WHERE REQUISITIONTYPE = 11 AND CUSTOMER = " + customer.ToString()+ " AND TO_CHAR(REQDATE,'YYYY') = '" + year + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            PlanMarketingData data = new PlanMarketingData();
            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["M1"])) data.M1 = Convert.ToDouble(dt.Rows[0]["M1"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M2"])) data.M2 = Convert.ToDouble(dt.Rows[0]["M2"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M3"])) data.M3 = Convert.ToDouble(dt.Rows[0]["M3"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M4"])) data.M4 = Convert.ToDouble(dt.Rows[0]["M4"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M5"])) data.M5 = Convert.ToDouble(dt.Rows[0]["M5"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M6"])) data.M6 = Convert.ToDouble(dt.Rows[0]["M6"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M7"])) data.M7 = Convert.ToDouble(dt.Rows[0]["M7"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M8"])) data.M8 = Convert.ToDouble(dt.Rows[0]["M8"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M9"])) data.M9 = Convert.ToDouble(dt.Rows[0]["M9"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M10"])) data.M10 = Convert.ToDouble(dt.Rows[0]["M10"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M11"])) data.M11 = Convert.ToDouble(dt.Rows[0]["M11"]);
                if (!Convert.IsDBNull(dt.Rows[0]["M12"])) data.M12 = Convert.ToDouble(dt.Rows[0]["M12"]);

            }

            return data;
        }

    }
}