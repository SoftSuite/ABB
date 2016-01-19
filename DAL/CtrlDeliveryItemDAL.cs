using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class CtrlDeliveryItemDAL
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

        private string tableName = "CTRLDELIVERYITEM";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        double _CTRLDELIVERY = 0;
        double _REQUISITION = 0;
        string _CODE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CONTACTNAME = "";
        string _CNAME = "";
        string _CADDRESS = "";
        string _CTEL = "";
        double _BOXQTY = 0;
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

        public double CTRLDELIVERY
        {
            get { return _CTRLDELIVERY; }
            set { _CTRLDELIVERY = value; }
        }

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public DateTime CREATEON
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


        public string CONTACTNAME
        {
            get { return _CONTACTNAME; }
            set { _CONTACTNAME = value; }
        }

        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }

        public string CTEL
        {
            get { return _CTEL; }
            set { _CTEL = value; }
        }

        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }

        public double BOXQTY
        {
            get { return _BOXQTY; }
            set { _BOXQTY = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CTRLDELIVERY,REQUISITION,CONTACTNAME,CNAME,CADDRESS,CTEL,BOXQTY,CREATEBY,CREATEON,UPDATEBY,UPDATEON)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += "  " + _CTRLDELIVERY.ToString() + ",";// CTRLDELIVERY";
                sqlz += "  " + _REQUISITION.ToString() + ",";// REQUISITION";
                sqlz += " '" + OracleDB.QRText(_CONTACTNAME) + "',";// CONTACTNAME";
                sqlz += " '" + OracleDB.QRText(_CNAME) + "',";// CNAME";
                sqlz += " '" + OracleDB.QRText(_CADDRESS) + "',";// CADDRESS";
                sqlz += " '" + OracleDB.QRText(_CTEL) + "',";// CTEL";
                sqlz += "  " + _BOXQTY.ToString() + ",";// BOXQTY";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + "";// UPDATEON";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " CTRLDELIVERY  = '" + CTRLDELIVERY.ToString() + "', ";
                sqlz += " REQUISITION  = '" + REQUISITION.ToString() + "', ";
                sqlz += " CONTACTNAME  = '" + OracleDB.QRText(_CONTACTNAME) + "', ";
                sqlz += " CNAME  = '" + OracleDB.QRText(_CNAME) + "', ";
                sqlz += " CADDRESS  = '" + OracleDB.QRText(_CADDRESS) + "', ";
                sqlz += " CTEL  = '" + OracleDB.QRText(_CTEL) + "', ";
                sqlz += " BOXQTY  = '" + BOXQTY.ToString() + "', ";
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
                        if (!Convert.IsDBNull(zRdr["CTRLDELIVERY"])) _CTRLDELIVERY = Convert.ToDouble(zRdr["CTRLDELIVERY"]);
                        if (!Convert.IsDBNull(zRdr["REQUISITION"])) _REQUISITION = Convert.ToDouble(zRdr["REQUISITION"]);
                        if (!Convert.IsDBNull(zRdr["CONTACTNAME"])) _CONTACTNAME = zRdr["CONTACTNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CNAME"])) _CNAME = zRdr["CNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CADDRESS"])) _CADDRESS = zRdr["CADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTEL"])) _CTEL = zRdr["CTEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["BOXQTY"])) _LOID = Convert.ToDouble(zRdr["BOXQTY"]);
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
        #endregion

        public bool DeleteDataByCtrlDelivery(double ctrldelivery, OracleTransaction zTrans)
        {
            return doDelete("CTRLDELIVERY = " + ctrldelivery.ToString() + " ", zTrans);
        }

    }
}
