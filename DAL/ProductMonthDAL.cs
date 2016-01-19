using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;


namespace ABB.DAL
{
    public class ProductMonthDAL
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
            return doUpdate(" PRODUCT = " + _PRODUCT.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByPRODUCT(double zPRODUCT, OracleTransaction zTrans)
        {
            return doGetdata(" PRODUCT = " + zPRODUCT.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteCurrentData(OracleTransaction zTrans)
        {
            return doDelete(" PRODUCT = " + _PRODUCT.ToString() + " ", zTrans);
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

        private string tableName = "PRODUCTMONTH";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        double _PRODUCT = 0;
        string _M1 = "";
        string _M2 = "";
        string _M3 = "";
        string _M4 = "";
        string _M5 = "";
        string _M6 = "";
        string _M7 = "";
        string _M8 = "";
        string _M9 = "";
        string _M10 = "";
        string _M11 = "";
        string _M12 = "";
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
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public string M1
        {
            get { return _M1; }
            set { _M1 = value; }
        }
        public string M2
        {
            get { return _M2; }
            set { _M2 = value; }
        }
        public string M3
        {
            get { return _M3; }
            set { _M3 = value; }
        }
        public string M4
        {
            get { return _M4; }
            set { _M4 = value; }
        }
        public string M5
        {
            get { return _M5; }
            set { _M5 = value; }
        }
        public string M6
        {
            get { return _M6; }
            set { _M6 = value; }
        }
        public string M7
        {
            get { return _M7; }
            set { _M7 = value; }
        }
        public string M8
        {
            get { return _M8; }
            set { _M8 = value; }
        }
        public string M9
        {
            get { return _M9; }
            set { _M9 = value; }
        }
        public string M10
        {
            get { return _M10; }
            set { _M10 = value; }
        }
        public string M11
        {
            get { return _M11; }
            set { _M11 = value; }
        }
        public string M12
        {
            get { return _M12; }
            set { _M12 = value; }
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
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,PRODUCT, M1, M2, M3, M4, M5, M6, M7, M8, M9, M10 ";
                sqlz += ", M11, M12, CREATEBY, CREATEON) VALUES (";
                sqlz += "  " + _LOID.ToString() + ",";
                sqlz += " '" + OracleDB.QRText(_PRODUCT.ToString()) + "',";
                sqlz += " '" + OracleDB.QRText(_M1) + "',";
                sqlz += " '" + OracleDB.QRText(_M2) + "',";
                sqlz += " '" + OracleDB.QRText(_M3) + "',";
                sqlz += " '" + OracleDB.QRText(_M4) + "',";
                sqlz += " '" + OracleDB.QRText(_M5) + "',";
                sqlz += " '" + OracleDB.QRText(_M6) + "',";
                sqlz += " '" + OracleDB.QRText(_M7) + "',";
                sqlz += " '" + OracleDB.QRText(_M8) + "',";
                sqlz += " '" + OracleDB.QRText(_M9) + "',";
                sqlz += " '" + OracleDB.QRText(_M10) + "',";
                sqlz += " '" + OracleDB.QRText(_M11) + "',";
                sqlz += " '" + OracleDB.QRText(_M12) + "',";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";
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
                sqlz += " M1  = '" + OracleDB.QRText(_M1) + "', ";
                sqlz += " M2  = '" + OracleDB.QRText(_M2) + "', ";
                sqlz += " M3  = '" + OracleDB.QRText(_M3) + "', ";
                sqlz += " M4  = '" + OracleDB.QRText(_M4) + "', ";
                sqlz += " M5  = '" + OracleDB.QRText(_M5) + "', ";
                sqlz += " M6  = '" + OracleDB.QRText(_M6) + "', ";
                sqlz += " M7  = '" + OracleDB.QRText(_M7) + "', ";
                sqlz += " M8  = '" + OracleDB.QRText(_M8) + "', ";
                sqlz += " M9  = '" + OracleDB.QRText(_M9) + "', ";
                sqlz += " M10  = '" + OracleDB.QRText(_M10) + "', ";
                sqlz += " M11  = '" + OracleDB.QRText(_M11) + "', ";
                sqlz += " M12  = '" + OracleDB.QRText(_M12) + "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + " ";
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

        private string sql_select2
        {
            get
            {
                string sqlz = " SELECT * FROM PRODUCT";
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
                  //  if (!ret) _error = OracleDB.Err_NoDelete;
                   // else 
                    _OnDB = false;
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
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _LOID = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["M1"])) _M1 = zRdr["M1"].ToString();
                        if (!Convert.IsDBNull(zRdr["M2"])) _M2 = zRdr["M2"].ToString();
                        if (!Convert.IsDBNull(zRdr["M3"])) _M3 = zRdr["M3"].ToString();
                        if (!Convert.IsDBNull(zRdr["M4"])) _M4 = zRdr["M4"].ToString();
                        if (!Convert.IsDBNull(zRdr["M5"])) _M5 = zRdr["M5"].ToString();
                        if (!Convert.IsDBNull(zRdr["M6"])) _M6 = zRdr["M6"].ToString();
                        if (!Convert.IsDBNull(zRdr["M7"])) _M7 = zRdr["M7"].ToString();
                        if (!Convert.IsDBNull(zRdr["M8"])) _M8 = zRdr["M8"].ToString();
                        if (!Convert.IsDBNull(zRdr["M9"])) _M9 = zRdr["M9"].ToString();
                        if (!Convert.IsDBNull(zRdr["M10"])) _M10 = zRdr["M10"].ToString();
                        if (!Convert.IsDBNull(zRdr["M11"])) _M11 = zRdr["M11"].ToString();
                        if (!Convert.IsDBNull(zRdr["M12"])) _M12 = zRdr["M12"].ToString();
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


        public double doGetProduct(string Code)
        {
            double sLOID = 0;
            string tmpWhere = " WHERE CODE='" + Code + "'";
            OracleDataReader zRdr = null;
            try
            {
                zRdr = OracleDB.ExecQueryCmd(sql_select2 + tmpWhere);
                if (zRdr.Read())
                {
          //          _OnDB = true;
                    if (!Convert.IsDBNull(zRdr["LOID"])) sLOID = Convert.ToDouble(zRdr["LOID"]);
                }
                else
                {
                    _error = OracleDB.Err_NoSelect;
                }
                zRdr.Close();
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                if (zRdr != null && !zRdr.IsClosed)
                    zRdr.Close();
            }

            return sLOID;
        }

        #endregion
    }
}
