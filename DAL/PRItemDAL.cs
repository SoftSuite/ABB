using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class PRItemDAL
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

        private string tableName = "PRITEM";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LASTYEAR = 0;
        string _URGENT = "";
        string _ISMATERIAL = "";
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _PRODUCT = 0;
        double _PDREQUEST = 0;
        double _QTY = 0;
        double _UNIT = 0;
        double _MINSTOCK = 0;
        double _MAXSTOCK = 0;
        double _STOCK = 0;
        double _OLDPRICE = 0;
        double _CURPRICE = 0;
        double _MINPRICE = 0;
        double _LAST3MON = 0;
        DateTime _DUEDATE = new DateTime(1, 1, 1);
        string _STATUS = "";
        string _ACTIVE = "";
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
        public double LASTYEAR
        {
            get { return _LASTYEAR; }
            set { _LASTYEAR = value; }
        }
        public string URGENT
        {
            get { return _URGENT; }
            set { _URGENT = value; }
        }
        public string ISMATERIAL
        {
            get { return _ISMATERIAL; }
            set { _ISMATERIAL = value; }
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
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public double PDREQUEST
        {
            get { return _PDREQUEST; }
            set { _PDREQUEST = value; }
        }
        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public double MINSTOCK
        {
            get { return _MINSTOCK; }
            set { _MINSTOCK = value; }
        }
        public double MAXSTOCK
        {
            get { return _MAXSTOCK; }
            set { _MAXSTOCK = value; }
        }
        public double STOCK
        {
            get { return _STOCK; }
            set { _STOCK = value; }
        }
        public double OLDPRICE
        {
            get { return _OLDPRICE; }
            set { _OLDPRICE = value; }
        }
        public double CURPRICE
        {
            get { return _CURPRICE; }
            set { _CURPRICE = value; }
        }
        public double MINPRICE
        {
            get { return _MINPRICE; }
            set { _MINPRICE = value; }
        }
        public double LAST3MON
        {
            get { return _LAST3MON; }
            set { _LAST3MON = value; }
        }
        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LASTYEAR,URGENT,ISMATERIAL,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,PRODUCT,PDREQUEST,QTY,UNIT,MINSTOCK,MAXSTOCK,STOCK,OLDPRICE,CURPRICE,MINPRICE,LAST3MON,DUEDATE,STATUS,ACTIVE)VALUES(";
                sqlz += "  " + _LASTYEAR.ToString() + ",";// LASTYEAR";
                sqlz += " '" + OracleDB.QRText(_URGENT) + "',";// URGENT";
                sqlz += " '" + OracleDB.QRText(_ISMATERIAL) + "',";// ISMATERIAL";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += "  " + _PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += "  " + _PDREQUEST.ToString() + ",";// PDREQUEST";
                sqlz += "  " + _QTY.ToString() + ",";// QTY";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += "  " + _MINSTOCK.ToString() + ",";// MINSTOCK";
                sqlz += "  " + _MAXSTOCK.ToString() + ",";// MAXSTOCK";
                sqlz += "  " + _STOCK.ToString() + ",";// STOCK";
                sqlz += "  " + _OLDPRICE.ToString() + ",";// OLDPRICE";
                sqlz += "  " + _CURPRICE.ToString() + ",";// CURPRICE";
                sqlz += "  " + _MINPRICE.ToString() + ",";// MINPRICE";
                sqlz += "  " + _LAST3MON.ToString() + ",";// LAST3MON";
                sqlz += " " + OracleDB.QRDateTime(_DUEDATE) + ",";// DUEDATE";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "'";// ACTIVE";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " LASTYEAR  = " + _LASTYEAR.ToString() + ", ";
                sqlz += " URGENT  = '" + OracleDB.QRText(_URGENT) + "', ";
                sqlz += " ISMATERIAL  = '" + OracleDB.QRText(_ISMATERIAL) + "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString() + ", ";
                sqlz += " PDREQUEST  = " + _PDREQUEST.ToString() + ", ";
                sqlz += " QTY  = " + _QTY.ToString() + ", ";
                sqlz += " UNIT  = " + _UNIT.ToString() + ", ";
                sqlz += " MINSTOCK  = " + _MINSTOCK.ToString() + ", ";
                sqlz += " MAXSTOCK  = " + _MAXSTOCK.ToString() + ", ";
                sqlz += " STOCK  = " + _STOCK.ToString() + ", ";
                sqlz += " OLDPRICE  = " + _OLDPRICE.ToString() + ", ";
                sqlz += " CURPRICE  = " + _CURPRICE.ToString() + ", ";
                sqlz += " MINPRICE  = " + _MINPRICE.ToString() + ", ";
                sqlz += " LAST3MON  = " + _LAST3MON.ToString() + ", ";
                sqlz += " DUEDATE  = " + OracleDB.QRDateTime(_DUEDATE) + ", ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "' ";
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
                        if (!Convert.IsDBNull(zRdr["LASTYEAR"])) _LASTYEAR = Convert.ToDouble(zRdr["LASTYEAR"]);
                        if (!Convert.IsDBNull(zRdr["URGENT"])) _URGENT = zRdr["URGENT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISMATERIAL"])) _ISMATERIAL = zRdr["ISMATERIAL"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["PDREQUEST"])) _PDREQUEST = Convert.ToDouble(zRdr["PDREQUEST"]);
                        if (!Convert.IsDBNull(zRdr["QTY"])) _QTY = Convert.ToDouble(zRdr["QTY"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["MINSTOCK"])) _MINSTOCK = Convert.ToDouble(zRdr["MINSTOCK"]);
                        if (!Convert.IsDBNull(zRdr["MAXSTOCK"])) _MAXSTOCK = Convert.ToDouble(zRdr["MAXSTOCK"]);
                        if (!Convert.IsDBNull(zRdr["STOCK"])) _STOCK = Convert.ToDouble(zRdr["STOCK"]);
                        if (!Convert.IsDBNull(zRdr["OLDPRICE"])) _OLDPRICE = Convert.ToDouble(zRdr["OLDPRICE"]);
                        if (!Convert.IsDBNull(zRdr["CURPRICE"])) _CURPRICE = Convert.ToDouble(zRdr["CURPRICE"]);
                        if (!Convert.IsDBNull(zRdr["MINPRICE"])) _MINPRICE = Convert.ToDouble(zRdr["MINPRICE"]);
                        if (!Convert.IsDBNull(zRdr["LAST3MON"])) _LAST3MON = Convert.ToDouble(zRdr["LAST3MON"]);
                        if (!Convert.IsDBNull(zRdr["DUEDATE"])) _DUEDATE = OracleDB.DBDate(zRdr["DUEDATE"]);
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
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

        public bool DeleteDataByPDRequest(double PDRequest, OracleTransaction zTrans)
        {
            return doDelete("PDREQUEST = " + PDRequest.ToString() + " ", zTrans);
        }

        public bool UpdateStatusByPDRequest(double PDRequest, string status, string userID, OracleTransaction zTrans)
        {
            bool ret = true;
            string sql = "UPDATE " + TableName + " SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
            sql += "WHERE PDREQUEST = " + PDRequest.ToString() + " ";
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

    }
}