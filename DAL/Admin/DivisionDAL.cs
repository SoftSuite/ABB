using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data; 
namespace ABB.DAL.Admin
{
    public class DivisionDAL
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

        private string tableName = "DIVISION";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _TNAME = "";
        DateTime _EFDATE = new DateTime(1, 1, 1);
        DateTime _EPDATE = new DateTime(1, 1, 1);
        string _CODE = "";
        double _DEPARTMENT = 0;
        string _ABBNAME = "";
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

        public string TNAME
        {
            get { return _TNAME; }
            set { _TNAME = value; }
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

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public double DEPARTMENT
        {
            get { return _DEPARTMENT; }
            set { _DEPARTMENT = value; }
        }
        public string ABBNAME
        {
            get { return _ABBNAME; }
            set { _ABBNAME = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,TNAME,EFDATE,EPDATE,DEPARTMENT,ABBNAME,CODE)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_TNAME) + "',";// TNAME";
                sqlz += " " + OracleDB.QRDateTime(_EFDATE) + ",";// REGISNO";
                sqlz += " " + OracleDB.QRDateTime(_EPDATE) + ",";// ISEDIT";
                sqlz += "  " + _DEPARTMENT.ToString() + ",";// DEPARTMENT";
                sqlz += " '" + OracleDB.QRText(_ABBNAME) + "',";// ABBNAME";
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
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " TNAME  = '" + OracleDB.QRText(_TNAME) + "', ";
                sqlz += " EFDATE  = " + OracleDB.QRDateTime(_EFDATE) + ", ";
                sqlz += " EPDATE  = " + OracleDB.QRDateTime(_EPDATE) + ", ";
                sqlz += " DEPARTMENT  = " + _DEPARTMENT + ", ";
                sqlz += " ABBNAME  = '" + OracleDB.QRText(_ABBNAME) + "', ";
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

                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["TNAME"])) _TNAME = zRdr["TNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["EFDATE"])) _EFDATE = OracleDB.DBDate(zRdr["EFDATE"]);
                        if (!Convert.IsDBNull(zRdr["EPDATE"])) _EPDATE = OracleDB.DBDate(zRdr["EPDATE"]);
                        if (!Convert.IsDBNull(zRdr["DEPARTMENT"])) _DEPARTMENT = Convert.ToDouble(zRdr["DEPARTMENT"]);
                        if (!Convert.IsDBNull(zRdr["ABBNAME"])) _ABBNAME = zRdr["ABBNAME"].ToString();
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

        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        /// 
        public DataTable GetDataList(string whereCause, string orderByField, OracleTransaction zTrans)
        {
            return OracleDB.ExecListCmd(sql_select + whereCause + (orderByField == "" ? "" : " ORDER BY " + orderByField + " "));
        }

        public DataTable GetDivisionList()
        {
            string sqlz = "SELECT * FROM (SELECT DV.LOID, DV.CODE, DV.TNAME, DV.ABBNAME, DV.DEPARTMENT, DP.TNAME DEPARTMENTNAME FROM DIVISION DV ";
            sqlz += "INNER JOIN DEPARTMENT DP ON DV.DEPARTMENT = DP.LOID)A ";

            return OracleDB.ExecListCmd(sqlz);
        }

        public bool CheckCode(double loid, string code)
        {
            string sql = "SELECT * FROM " + tableName + " WHERE CODE = '" + code + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckName(double loid, string name)
        {
            string sql = "SELECT * FROM " + tableName + " WHERE TNAME = '" + name + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckAbbName(double loid, string abbname)
        {
            string sql = "SELECT * FROM " + tableName + " WHERE ABBNAME = '" + abbname + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }
    }
}



