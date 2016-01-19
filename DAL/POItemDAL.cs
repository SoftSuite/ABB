using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class POItemDAL
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

        private string tableName = "POITEM";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        string _ACTIVE = "";
        string _STATUS = "";
        double _REFPOITEM = 0;
        string _FLAGDEL = "";
        string _ISVAT = "";
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _PRODUCT = 0;
        double _PRITEM = 0;
        double _PDORDER = 0;
        double _QTY = 0;
        double _RECEIVEQTY = 0;
        double _UNIT = 0;
        double _PRICE = 0;
        double _DISCOUNT = 0;
        DateTime _DUEDATE = new DateTime(1, 1, 1);
        string _DESCRIPTION = "";
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
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public double REFPOITEM
        {
            get { return _REFPOITEM; }
            set { _REFPOITEM = value; }
        }
        public string FLAGDEL
        {
            get { return _FLAGDEL; }
            set { _FLAGDEL = value; }
        }
        public string ISVAT
        {
            get { return _ISVAT; }
            set { _ISVAT = value; }
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
        public double PRITEM
        {
            get { return _PRITEM; }
            set { _PRITEM = value; }
        }
        public double PDORDER
        {
            get { return _PDORDER; }
            set { _PDORDER = value; }
        }
        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }
        public double RECEIVEQTY
        {
            get { return _RECEIVEQTY; }
            set { _RECEIVEQTY = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }
        public double DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }
        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }
        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (ACTIVE,STATUS,REFPOITEM,FLAGDEL,ISVAT,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,PRODUCT,PRITEM,PDORDER,QTY,RECEIVEQTY,UNIT,PRICE,DISCOUNT,DUEDATE,DESCRIPTION)VALUES(";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += "  " + _REFPOITEM.ToString() + ",";// REFPOITEM";
                sqlz += " '" + OracleDB.QRText(_FLAGDEL) + "',";// FLAGDEL";
                sqlz += " '" + OracleDB.QRText(_ISVAT) + "',";// ISVAT";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += "  " + _PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += "  " + _PRITEM.ToString() + ",";// PRITEM";
                sqlz += "  " + _PDORDER.ToString() + ",";// PDORDER";
                sqlz += "  " + _QTY.ToString() + ",";// QTY";
                sqlz += "  " + _RECEIVEQTY.ToString() + ",";// RECEIVEQTY";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += "  " + _PRICE.ToString() + ",";// PRICE";
                sqlz += "  " + _DISCOUNT.ToString() + ",";// DISCOUNT";
                sqlz += " " + OracleDB.QRDateTime(_DUEDATE) + ",";// DUEDATE";
                sqlz += " '" + OracleDB.QRText(_DESCRIPTION) + "'";// DESCRIPTION";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " REFPOITEM  = " + _REFPOITEM.ToString() + ", ";
                sqlz += " FLAGDEL  = '" + OracleDB.QRText(_FLAGDEL) + "', ";
                sqlz += " ISVAT  = '" + OracleDB.QRText(_ISVAT) + "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString() + ", ";
                sqlz += " PRITEM  = " + _PRITEM.ToString() + ", ";
                sqlz += " PDORDER  = " + _PDORDER.ToString() + ", ";
                sqlz += " QTY  = " + _QTY.ToString() + ", ";
                sqlz += " RECEIVEQTY  = " + _RECEIVEQTY.ToString() + ", ";
                sqlz += " UNIT  = " + _UNIT.ToString() + ", ";
                sqlz += " PRICE  = " + _PRICE.ToString() + ", ";
                sqlz += " DISCOUNT  = " + _DISCOUNT.ToString() + ", ";
                sqlz += " DUEDATE  = " + OracleDB.QRDateTime(_DUEDATE) + ", ";
                sqlz += " DESCRIPTION  = '" + OracleDB.QRText(_DESCRIPTION) + "' ";
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
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFPOITEM"])) _REFPOITEM = Convert.ToDouble(zRdr["REFPOITEM"]);
                        if (!Convert.IsDBNull(zRdr["FLAGDEL"])) _FLAGDEL = zRdr["FLAGDEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISVAT"])) _ISVAT = zRdr["ISVAT"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["PRITEM"])) _PRITEM = Convert.ToDouble(zRdr["PRITEM"]);
                        if (!Convert.IsDBNull(zRdr["PDORDER"])) _PDORDER = Convert.ToDouble(zRdr["PDORDER"]);
                        if (!Convert.IsDBNull(zRdr["QTY"])) _QTY = Convert.ToDouble(zRdr["QTY"]);
                        if (!Convert.IsDBNull(zRdr["RECEIVEQTY"])) _RECEIVEQTY = Convert.ToDouble(zRdr["RECEIVEQTY"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["PRICE"])) _PRICE = Convert.ToDouble(zRdr["PRICE"]);
                        if (!Convert.IsDBNull(zRdr["DISCOUNT"])) _DISCOUNT = Convert.ToDouble(zRdr["DISCOUNT"]);
                        if (!Convert.IsDBNull(zRdr["DUEDATE"])) _DUEDATE = OracleDB.DBDate(zRdr["DUEDATE"]);
                        if (!Convert.IsDBNull(zRdr["DESCRIPTION"])) _DESCRIPTION = zRdr["DESCRIPTION"].ToString();
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

        public bool DeleteDataByPDOrder(double PDOrder, OracleTransaction zTrans)
        {
            return doDelete("PDORDER = " + PDOrder.ToString() + " ", zTrans);
        }

        public bool UpdateStatusByPDOrder(double PDOrder, string status, string userID, OracleTransaction zTrans)
        {
            bool ret = true;
            string sql = "UPDATE " + TableName + " SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
            sql += "WHERE PDORDER = " + PDOrder.ToString() + " ";
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