using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class RequisitionTypeDAL
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

        private string tableName = "REQUISITIONTYPE";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _FROMZONE = 0;
        double _TOZONE = 0;
        string _ISFGSTOCKOUT = "";
        string _ISINVOICE = "";
        string _ISRESERVE = "";
        string _ISFGRETURN = "";
        string _REPORTNAME = "";
        string _TYPE = "";
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _NAME = "";
        string _DESCRIPTION = "";
        string _CODE = "";
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
        public double FROMZONE
        {
            get { return _FROMZONE; }
            set { _FROMZONE = value; }
        }
        public double TOZONE
        {
            get { return _TOZONE; }
            set { _TOZONE = value; }
        }
        public string ISFGSTOCKOUT
        {
            get { return _ISFGSTOCKOUT; }
            set { _ISFGSTOCKOUT = value; }
        }
        public string ISINVOICE
        {
            get { return _ISINVOICE; }
            set { _ISINVOICE = value; }
        }
        public string ISRESERVE
        {
            get { return _ISRESERVE; }
            set { _ISRESERVE = value; }
        }
        public string ISFGRETURN
        {
            get { return _ISFGRETURN; }
            set { _ISFGRETURN = value; }
        }
        public string REPORTNAME
        {
            get { return _REPORTNAME; }
            set { _REPORTNAME = value; }
        }
        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
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
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (FROMZONE,TOZONE,ISFGSTOCKOUT,ISINVOICE,ISRESERVE,ISFGRETURN,REPORTNAME,TYPE,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,NAME,DESCRIPTION,CODE)VALUES(";
                sqlz += "  " + _FROMZONE.ToString() + ",";// FROMZONE";
                sqlz += "  " + _TOZONE.ToString() + ",";// TOZONE";
                sqlz += " '" + OracleDB.QRText(_ISFGSTOCKOUT) + "',";// ISFGSTOCKOUT";
                sqlz += " '" + OracleDB.QRText(_ISINVOICE) + "',";// ISINVOICE";
                sqlz += " '" + OracleDB.QRText(_ISRESERVE) + "',";// ISRESERVE";
                sqlz += " '" + OracleDB.QRText(_ISFGRETURN) + "',";// ISFGRETURN";
                sqlz += " '" + OracleDB.QRText(_REPORTNAME) + "',";// REPORTNAME";
                sqlz += " '" + OracleDB.QRText(_TYPE) + "',";// TYPE";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_NAME) + "',";// NAME";
                sqlz += " '" + OracleDB.QRText(_DESCRIPTION) + "',";// DESCRIPTION";
                sqlz += " '" + OracleDB.QRText(_CODE) + "'";// CODE";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " FROMZONE  = " + _FROMZONE.ToString() + ", ";
                sqlz += " TOZONE  = " + _TOZONE.ToString() + ", ";
                sqlz += " ISFGSTOCKOUT  = '" + OracleDB.QRText(_ISFGSTOCKOUT) + "', ";
                sqlz += " ISINVOICE  = '" + OracleDB.QRText(_ISINVOICE) + "', ";
                sqlz += " ISRESERVE  = '" + OracleDB.QRText(_ISRESERVE) + "', ";
                sqlz += " ISFGRETURN  = '" + OracleDB.QRText(_ISFGRETURN) + "', ";
                sqlz += " REPORTNAME  = '" + OracleDB.QRText(_REPORTNAME) + "', ";
                sqlz += " TYPE  = '" + OracleDB.QRText(_TYPE) + "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " NAME  = '" + OracleDB.QRText(_NAME) + "', ";
                sqlz += " DESCRIPTION  = '" + OracleDB.QRText(_DESCRIPTION) + "', ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "' ";
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
                        if (!Convert.IsDBNull(zRdr["FROMZONE"])) _FROMZONE = Convert.ToDouble(zRdr["FROMZONE"]);
                        if (!Convert.IsDBNull(zRdr["TOZONE"])) _TOZONE = Convert.ToDouble(zRdr["TOZONE"]);
                        if (!Convert.IsDBNull(zRdr["ISFGSTOCKOUT"])) _ISFGSTOCKOUT = zRdr["ISFGSTOCKOUT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISINVOICE"])) _ISINVOICE = zRdr["ISINVOICE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISRESERVE"])) _ISRESERVE = zRdr["ISRESERVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISFGRETURN"])) _ISFGRETURN = zRdr["ISFGRETURN"].ToString();
                        if (!Convert.IsDBNull(zRdr["REPORTNAME"])) _REPORTNAME = zRdr["REPORTNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["TYPE"])) _TYPE = zRdr["TYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["DESCRIPTION"])) _DESCRIPTION = zRdr["DESCRIPTION"].ToString();
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
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