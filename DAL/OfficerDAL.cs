using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class OfficerDAL
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

        private string tableName = "OFFICER";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        string _NICKNAME = "";
        DateTime _BIRTHDATE = new DateTime(1, 1, 1);
        string _TEL = "";
        string _EMAIL = "";
        string _ADDRESS = "";
        string _ROAD = "";
        double _PROVINCE = 0;
        double _AMPHUR = 0;
        double _TAMBOL = 0;
        string _ZIPCODE = "";
        string _REMARK = "";
        double _TITLE = 0;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _TNAME = "";
        string _LASTNAME = "";
        double _DIVISION = 0;
        string _USERID = "";
        string _PASSWORD = "";
        DateTime _EFDATE = new DateTime(1, 1, 1);
        DateTime _EPDATE = new DateTime(1, 1, 1);

        string _HHT = "";
        string _POS = "";
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
        public string NICKNAME
        {
            get { return _NICKNAME; }
            set { _NICKNAME = value; }
        }
        public DateTime BIRTHDATE
        {
            get { return _BIRTHDATE; }
            set { _BIRTHDATE = value; }
        }
        public string TEL
        {
            get { return _TEL; }
            set { _TEL = value; }
        }
        public string EMAIL
        {
            get { return _EMAIL; }
            set { _EMAIL = value; }
        }
        public string ADDRESS
        {
            get { return _ADDRESS; }
            set { _ADDRESS = value; }
        }
        public string ROAD
        {
            get { return _ROAD; }
            set { _ROAD = value; }
        }
        public double PROVINCE
        {
            get { return _PROVINCE; }
            set { _PROVINCE = value; }
        }
        public double AMPHUR
        {
            get { return _AMPHUR; }
            set { _AMPHUR = value; }
        }
        public double TAMBOL
        {
            get { return _TAMBOL; }
            set { _TAMBOL = value; }
        }
        public string ZIPCODE
        {
            get { return _ZIPCODE; }
            set { _ZIPCODE = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public double TITLE
        {
            get { return _TITLE; }
            set { _TITLE = value; }
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
        public string TNAME
        {
            get { return _TNAME; }
            set { _TNAME = value; }
        }
        public string LASTNAME
        {
            get { return _LASTNAME; }
            set { _LASTNAME = value; }
        }
        public double DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }
        public string USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }
        public string PASSWORD
        {
            get { return _PASSWORD; }
            set { _PASSWORD = value; }
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
        public string HHT
        {
            get { return _HHT; }
            set { _HHT = value; }
        }
        public string POS
        {
            get { return _POS; }
            set { _POS = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (NICKNAME,BIRTHDATE,TEL,EMAIL,ADDRESS,ROAD,PROVINCE,AMPHUR,TAMBOL,ZIPCODE,REMARK,TITLE,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,TNAME,LASTNAME,DIVISION,USERID,PASSWORD,EFDATE,EPDATE)VALUES(";
                sqlz += " '" + OracleDB.QRText(_NICKNAME) + "',";// NICKNAME";
                sqlz += " " + OracleDB.QRDateTime(_BIRTHDATE) + ",";// BIRTHDATE";
                sqlz += " '" + OracleDB.QRText(_TEL) + "',";// TEL";
                sqlz += " '" + OracleDB.QRText(_EMAIL) + "',";// EMAIL";
                sqlz += " '" + OracleDB.QRText(_ADDRESS) + "',";// ADDRESS";
                sqlz += " '" + OracleDB.QRText(_ROAD) + "',";// ROAD";
                sqlz += "  " + (_PROVINCE == 0 ? "NULL" : _PROVINCE.ToString()) + ",";// PROVINCE";
                sqlz += "  " + (_AMPHUR == 0 ? "NULL" : _AMPHUR.ToString()) + ",";// AMPHUR";
                sqlz += "  " + (_TAMBOL == 0 ? "NULL" : _TAMBOL.ToString()) + ",";// TAMBOL";
                sqlz += " '" + OracleDB.QRText(_ZIPCODE) + "',";// ZIPCODE";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += "  " + _TITLE.ToString() + ",";// TITLE";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_TNAME) + "',";// TNAME";
                sqlz += " '" + OracleDB.QRText(_LASTNAME) + "',";// LASTNAME";
                sqlz += "  " + _DIVISION.ToString() + ",";// DIVISION";
                sqlz += " '" + OracleDB.QRText(_USERID) + "',";// USERID";
                sqlz += " '" + OracleDB.QRText(_PASSWORD) + "',";// PASSWORD";
                sqlz += " " + OracleDB.QRDateTime(_EFDATE) + ",";// EFDATE";
                sqlz += " " + OracleDB.QRDateTime(_EPDATE) + "";// EPDATE";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " NICKNAME  = '" + OracleDB.QRText(_NICKNAME) + "', ";
                sqlz += " BIRTHDATE  = " + OracleDB.QRDateTime(_BIRTHDATE) + ", ";
                sqlz += " TEL  = '" + OracleDB.QRText(_TEL) + "', ";
                sqlz += " EMAIL  = '" + OracleDB.QRText(_EMAIL) + "', ";
                sqlz += " ADDRESS  = '" + OracleDB.QRText(_ADDRESS) + "', ";
                sqlz += " ROAD  = '" + OracleDB.QRText(_ROAD) + "', ";
                sqlz += " PROVINCE  = " + (_PROVINCE == 0 ? "NULL" : _PROVINCE.ToString()) + ", ";
                sqlz += " AMPHUR  = " + (_AMPHUR == 0 ? "NULL" : _AMPHUR.ToString()) + ", ";
                sqlz += " TAMBOL  = " + (_TAMBOL == 0 ? "NULL" : _TAMBOL.ToString()) + ", ";
                sqlz += " ZIPCODE  = '" + OracleDB.QRText(_ZIPCODE) + "', ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " TITLE  = " + _TITLE.ToString() + ", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " TNAME  = '" + OracleDB.QRText(_TNAME) + "', ";
                sqlz += " LASTNAME  = '" + OracleDB.QRText(_LASTNAME) + "', ";
                sqlz += " DIVISION  = " + _DIVISION.ToString() + ", ";
                sqlz += " USERID  = '" + OracleDB.QRText(_USERID) + "', ";
                sqlz += " PASSWORD  = '" + OracleDB.QRText(_PASSWORD) + "', ";
                sqlz += " EFDATE  = " + OracleDB.QRDateTime(_EFDATE) + ", ";
                sqlz += " EPDATE  = " + OracleDB.QRDateTime(_EPDATE) + " ";
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
                        if (!Convert.IsDBNull(zRdr["NICKNAME"])) _NICKNAME = zRdr["NICKNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["BIRTHDATE"])) _BIRTHDATE = OracleDB.DBDate(zRdr["BIRTHDATE"]);
                        if (!Convert.IsDBNull(zRdr["TEL"])) _TEL = zRdr["TEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["EMAIL"])) _EMAIL = zRdr["EMAIL"].ToString();
                        if (!Convert.IsDBNull(zRdr["ADDRESS"])) _ADDRESS = zRdr["ADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["ROAD"])) _ROAD = zRdr["ROAD"].ToString();
                        if (!Convert.IsDBNull(zRdr["PROVINCE"])) _PROVINCE = Convert.ToDouble(zRdr["PROVINCE"]);
                        if (!Convert.IsDBNull(zRdr["AMPHUR"])) _AMPHUR = Convert.ToDouble(zRdr["AMPHUR"]);
                        if (!Convert.IsDBNull(zRdr["TAMBOL"])) _TAMBOL = Convert.ToDouble(zRdr["TAMBOL"]);
                        if (!Convert.IsDBNull(zRdr["ZIPCODE"])) _ZIPCODE = zRdr["ZIPCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["TITLE"])) _TITLE = Convert.ToDouble(zRdr["TITLE"]);
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["TNAME"])) _TNAME = zRdr["TNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["LASTNAME"])) _LASTNAME = zRdr["LASTNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["DIVISION"])) _DIVISION = Convert.ToDouble(zRdr["DIVISION"]);
                        if (!Convert.IsDBNull(zRdr["USERID"])) _USERID = zRdr["USERID"].ToString();
                        if (!Convert.IsDBNull(zRdr["PASSWORD"])) _PASSWORD = zRdr["PASSWORD"].ToString();
                        if (!Convert.IsDBNull(zRdr["EFDATE"])) _EFDATE = OracleDB.DBDate(zRdr["EFDATE"]);
                        if (!Convert.IsDBNull(zRdr["EPDATE"])) _EPDATE = OracleDB.DBDate(zRdr["EPDATE"]);
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
        /// <param name="zLOID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByUserID(string userID, OracleTransaction zTrans)
        {
            return doGetdata(" UPPER(USERID) = '" + userID.ToUpper() + "' ", zTrans);
        }

        public bool CheckUserID(double loid, string userID)
        {
            string sql = "SELECT * FROM " + tableName + " WHERE USERID = '" + userID + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckName(double loid, string name, string lastname)
        {
            string sql = "SELECT * FROM " + tableName + " WHERE TNAME = '" + name + "' AND LASTNAME = '" + lastname + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool GetDataByHHTUserID(string userID)
        {
            string sql = "SELECT O.*, NVL(R.HHT, 'N') HHT, NVL(R.POS, 'N') POS FROM OFFICER O LEFT JOIN ZROLE R ON R.OFFICER = O.LOID AND HHT = 'Y' ";
            sql += "WHERE UPPER(USERID) = '" + userID.ToUpper() + "' ";
            return doGetdata(sql);
        }

        public bool GetDataByPOSUserID(string userID)
        {
            string sql = "SELECT O.*, NVL(R.HHT, 'N') HHT, NVL(R.POS, 'N') POS FROM OFFICER O LEFT JOIN ZROLE R ON R.OFFICER = O.LOID AND POS = 'Y' ";
            sql += "WHERE UPPER(USERID) = '" + userID.ToUpper() + "' ";
            return doGetdata(sql);
        }

        private bool doGetdata(string sqlSelect)
        {
            bool ret = true;
            OracleDataReader zRdr = null;
            try
            {
                OracleTransaction trans = null;
                zRdr = OracleDB.ExecQueryCmd(sqlSelect, trans);
                if (zRdr.Read())
                {
                    _OnDB = true;
                    if (!Convert.IsDBNull(zRdr["NICKNAME"])) _NICKNAME = zRdr["NICKNAME"].ToString();
                    if (!Convert.IsDBNull(zRdr["BIRTHDATE"])) _BIRTHDATE = OracleDB.DBDate(zRdr["BIRTHDATE"]);
                    if (!Convert.IsDBNull(zRdr["TEL"])) _TEL = zRdr["TEL"].ToString();
                    if (!Convert.IsDBNull(zRdr["EMAIL"])) _EMAIL = zRdr["EMAIL"].ToString();
                    if (!Convert.IsDBNull(zRdr["ADDRESS"])) _ADDRESS = zRdr["ADDRESS"].ToString();
                    if (!Convert.IsDBNull(zRdr["ROAD"])) _ROAD = zRdr["ROAD"].ToString();
                    if (!Convert.IsDBNull(zRdr["PROVINCE"])) _PROVINCE = Convert.ToDouble(zRdr["PROVINCE"]);
                    if (!Convert.IsDBNull(zRdr["AMPHUR"])) _AMPHUR = Convert.ToDouble(zRdr["AMPHUR"]);
                    if (!Convert.IsDBNull(zRdr["TAMBOL"])) _TAMBOL = Convert.ToDouble(zRdr["TAMBOL"]);
                    if (!Convert.IsDBNull(zRdr["ZIPCODE"])) _ZIPCODE = zRdr["ZIPCODE"].ToString();
                    if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                    if (!Convert.IsDBNull(zRdr["TITLE"])) _TITLE = Convert.ToDouble(zRdr["TITLE"]);
                    if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                    if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                    if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                    if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                    if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                    if (!Convert.IsDBNull(zRdr["TNAME"])) _TNAME = zRdr["TNAME"].ToString();
                    if (!Convert.IsDBNull(zRdr["LASTNAME"])) _LASTNAME = zRdr["LASTNAME"].ToString();
                    if (!Convert.IsDBNull(zRdr["DIVISION"])) _DIVISION = Convert.ToDouble(zRdr["DIVISION"]);
                    if (!Convert.IsDBNull(zRdr["USERID"])) _USERID = zRdr["USERID"].ToString();
                    if (!Convert.IsDBNull(zRdr["PASSWORD"])) _PASSWORD = zRdr["PASSWORD"].ToString();
                    if (!Convert.IsDBNull(zRdr["EFDATE"])) _EFDATE = OracleDB.DBDate(zRdr["EFDATE"]);
                    if (!Convert.IsDBNull(zRdr["EPDATE"])) _EPDATE = OracleDB.DBDate(zRdr["EPDATE"]);
                    if (!Convert.IsDBNull(zRdr["HHT"])) _HHT = zRdr["HHT"].ToString();
                    if (!Convert.IsDBNull(zRdr["POS"])) _POS = zRdr["POS"].ToString();
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
            return ret;
        }

    }
}