using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace ABB.DAL
{
    public class PromotionSalesDAL
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
            return doGetdata(" P.LOID = " + zLOID.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteCurrentData(OracleTransaction zTrans)
        {
            return doDelete(" P.LOID = " + _LOID.ToString() + " ", zTrans);
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

        private string tableName = "PROMOTION";

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
        string _NAME = "";
        DateTime _EFDATE = new DateTime(1, 1, 1);
        DateTime _EPDATE = new DateTime(1, 1, 1);
        string _DISCOUNT = "";
        string _WAREHOUSE = "";
        string _ZONE = "";
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
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public DateTime EFDATE
        {
            get { return _EFDATE; }
            set { _EFDATE = value; }
        }
        public DateTime EPDATE
        {
            get { return _EPDATE; }
            set { _EPDATE = value; }
        }
        public string DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }
        public string WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
        public string ZONE
        {
            get { return _ZONE; }
            set { _ZONE = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID, CODE, CREATEBY, CREATEON, NAME, EFDATE, EPDATE, DISCOUNT, WAREHOUSE, ZONE) VALUES (";
                sqlz += "  " + _LOID.ToString() + ",";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";
                sqlz += " '" + OracleDB.QRText(_NAME) + "',";
                sqlz += " " + OracleDB.QRDateTime(_EFDATE) + ",";
                sqlz += " " + OracleDB.QRDateTime(_EPDATE) + ",";
                sqlz += " '" + OracleDB.QRText(_DISCOUNT) + "',";
                sqlz += " '" + OracleDB.QRText(_WAREHOUSE) + "',";
                sqlz += " '" + OracleDB.QRText(_ZONE) + "' ";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " LOID  = " + _LOID.ToString() + ", ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDate(_UPDATEON) + ", ";
                sqlz += " NAME  = '" + OracleDB.QRText(_NAME) + "', ";
                sqlz += " EFDATE  = " + OracleDB.QRDate(_EFDATE) + ", ";
                sqlz += " EPDATE  = " + OracleDB.QRDate(_EPDATE) + ", ";
                sqlz += " DISCOUNT  = '" + OracleDB.QRText(_DISCOUNT) + "', ";
                sqlz += " WAREHOUSE  = '" + OracleDB.QRText(_WAREHOUSE) + "', ";
                sqlz += " ZONE  = '" + OracleDB.QRText(_ZONE) + "' ";
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
                string sqlz = "SELECT P.LOID LOID,P.CODE,P.CREATEBY,P.CREATEON,P.UPDATEBY,P.UPDATEON, ";
                sqlz += "P.NAME,P.EFDATE,P.EPDATE,P.DISCOUNT,P.WAREHOUSE,P.ZONE,WH.NAME,WH.ACTIVE,ZO.NAME,ZO.ACTIVE ";
                sqlz += "FROM PROMOTION P LEFT JOIN WAREHOUSE WH ON P.WAREHOUSE=WH.LOID ";
                sqlz += "INNER JOIN ZONE ZO ON P.ZONE=ZO.LOID ";
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
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["EFDATE"])) _EFDATE = OracleDB.DBDate(zRdr["EFDATE"]);
                        if (!Convert.IsDBNull(zRdr["EPDATE"])) _EPDATE = OracleDB.DBDate(zRdr["EPDATE"]);
                        if (!Convert.IsDBNull(zRdr["DISCOUNT"])) _DISCOUNT = zRdr["DISCOUNT"].ToString();
                        if (!Convert.IsDBNull(zRdr["WAREHOUSE"])) _WAREHOUSE = zRdr["WAREHOUSE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ZONE"])) _ZONE = zRdr["ZONE"].ToString();
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
    }
}
