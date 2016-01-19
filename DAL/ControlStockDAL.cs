using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace ABB.DAL
{
    public class ControlStockDAL
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
        /// <param name="zID"></param>
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

        /// <summary>
        /// Get Data List of This Table
        /// </summary>
        /// <param name="whereCause"></param>
        /// <param name="sortField"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public DataTable GetDataList(string whereCause, string sortField, OracleTransaction zTrans)
        {
            return OracleDB.ExecListCmd(sql_select + whereCause + (sortField == "" ? "" : "ORDER BY " + sortField));
        }

        #endregion

        #region Constant

        private string tableName = "PRODUCTMINMAX";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        Int32 _PRODUCT = 0;
        Int32 _WAREHOUSE = 0;
        double _STANDARD = 0;
        double _MINIMUM = 0;
        double _MAXIMUM = 0;
        Int32 _ACTIVE = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
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
        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public Int32 PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public Int32 WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
        public double STANDARD
        {
            get { return _STANDARD; }
            set { _STANDARD = value; }
        }
        public double MINIMUM
        {
            get { return _MINIMUM; }
            set { _MINIMUM = value; }
        }
        public double MAXIMUM
        {
            get { return _MAXIMUM; }
            set { _MAXIMUM = value; }
        }
        public Int32 ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public string  CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }
        public DateTime  CREATEON
        {
            get { return _CREATEON; }
            set { _CREATEON = value; }
        }
        public string UPDATEBY
        {
            get { return _UPDATEBY; }
            set { _UPDATEBY = value; }
        }
        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
            set { _UPDATEON = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID, PRODUCT,WAREHOUSE,STANDARD,MINIMUM,MAXIMUM,ACTIVE,CREATEBY,CREATEON) VALUES (";
                sqlz += "  " + _LOID + ",";
                sqlz += "  " + _PRODUCT+ ",";
                sqlz += "  " + _WAREHOUSE + ",";
                sqlz += "  " + _STANDARD + ",";
                sqlz += "  " + _MINIMUM + ",";
                sqlz += "  " + _MAXIMUM + ",";
                sqlz += "  " + _ACTIVE + ",";
                sqlz += "  '" + _CREATEBY.ToString() + "',";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + " ";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " STANDARD  = " + _STANDARD + ", ";
                sqlz += " MINIMUM  = " + _MINIMUM + ", ";
                sqlz += " MAXIMUM  = " + _MAXIMUM + ", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDate(_UPDATEON) + " ";
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
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToInt32(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["WAREHOUSE"])) _WAREHOUSE = Convert.ToInt32(zRdr["WAREHOUSE"]);
                        if (!Convert.IsDBNull(zRdr["STANDARD"])) _STANDARD = Convert.ToDouble(zRdr["STANDARD"]);
                        if (!Convert.IsDBNull(zRdr["MINIMUM"])) _MINIMUM = Convert.ToDouble(zRdr["MINIMUM"]);
                        if (!Convert.IsDBNull(zRdr["MAXIMUM"])) _MAXIMUM = Convert.ToDouble(zRdr["MAXIMUM"]);
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = Convert.ToInt32(zRdr["ACTIVE"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                    }
                    else
                    {
                        ret = false;
                        _error = OracleDB.Err_NoSelect;
                    }
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
    }
}

