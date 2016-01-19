using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class BasketDAL
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

        private string tableName = "BASKET";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        string _TYPE = "";
        double _UNIT = 0;
        double _WAREHOUSE = 0;
        string _LOTNO = "";
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        DateTime _CHECKDATE = new DateTime(1, 1, 1);
        double _PRODUCT = 0;
        double _SUPPLIER = 0;
        double _QTY = 0;
        string _STATUS = "";
        DateTime _STOCKINDATE = new DateTime(1, 1, 1);
        string _REMARK = "";
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
        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
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
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public DateTime CHECKDATE
        {
            get { return _CHECKDATE; }
            set { _CHECKDATE = value; }
        }
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public double SUPPLIER
        {
            get { return _SUPPLIER; }
            set { _SUPPLIER = value; }
        }
        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public DateTime STOCKINDATE
        {
            get { return _STOCKINDATE; }
            set { _STOCKINDATE = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (TYPE,UNIT,WAREHOUSE,LOTNO,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,CODE,CHECKDATE,PRODUCT,SUPPLIER,QTY,STATUS,STOCKINDATE,REMARK)VALUES(";
                sqlz += " '" + OracleDB.QRText(_TYPE) + "',";// TYPE";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += "  " + _WAREHOUSE.ToString() + ",";// WAREHOUSE";
                sqlz += " '" + OracleDB.QRText(_LOTNO) + "',";// LOTNO";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " " + OracleDB.QRDateTime(_CHECKDATE) + ",";// CHECKDATE";
                sqlz += "  " + _PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += "  " + _SUPPLIER.ToString() + ",";// SUPPLIER";
                sqlz += "  " + _QTY.ToString() + ",";// QTY";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " " + OracleDB.QRDateTime(_STOCKINDATE) + ",";// STOCKINDATE";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "'";// REMARK";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " TYPE  = '" + OracleDB.QRText(_TYPE) + "', ";
                sqlz += " UNIT  = " + _UNIT.ToString() + ", ";
                sqlz += " WAREHOUSE  = " + _WAREHOUSE.ToString() + ", ";
                sqlz += " LOTNO  = '" + OracleDB.QRText(_LOTNO) + "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                sqlz += " CHECKDATE  = " + OracleDB.QRDateTime(_CHECKDATE) + ", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString() + ", ";
                sqlz += " SUPPLIER  = " + _SUPPLIER.ToString() + ", ";
                sqlz += " QTY  = " + _QTY.ToString() + ", ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " STOCKINDATE  = " + OracleDB.QRDateTime(_STOCKINDATE) + ", ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "' ";
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
                    if (_CODE == "") _CODE = OracleDB.GetRunningCode(tableName, _TYPE, zTrans);
                    if (_LOTNO == "") _LOTNO = OracleDB.GetRunningCode(tableName, "LOTNO", zTrans);
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
                        if (!Convert.IsDBNull(zRdr["TYPE"])) _TYPE = zRdr["TYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["WAREHOUSE"])) _WAREHOUSE = Convert.ToDouble(zRdr["WAREHOUSE"]);
                        if (!Convert.IsDBNull(zRdr["LOTNO"])) _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CHECKDATE"])) _CHECKDATE = OracleDB.DBDate(zRdr["CHECKDATE"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["SUPPLIER"])) _SUPPLIER = Convert.ToDouble(zRdr["SUPPLIER"]);
                        if (!Convert.IsDBNull(zRdr["QTY"])) _QTY = Convert.ToDouble(zRdr["QTY"]);
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["STOCKINDATE"])) _STOCKINDATE = OracleDB.DBDate(zRdr["STOCKINDATE"]);
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
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

        public bool CutStockBasket(double basket, string userID, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CUTSTOCKBASKET(" + basket.ToString() + ", '" + userID + "' )", trans);
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