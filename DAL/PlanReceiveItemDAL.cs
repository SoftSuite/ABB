using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class PlanReceiveItemDAL
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

        private string tableName = "PLANRECEIVEITEM";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _PLAN = 0;
        double _PRODUCT = 0;
        double _UNIT = 0;
        DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        DateTime _PDDATE = new DateTime(1, 1, 1);
        DateTime _PODATE = new DateTime(1, 1, 1);
        string _STATUS = "";
        double _PRODUCTMASTER = 0;
        double _PDLOID = 0;
        double _POLOID = 0;
        double _PDQTY = 0;
        double _POQTY = 0;
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
        public double PLAN
        {
            get { return _PLAN; }
            set { _PLAN = value; }
        }
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }
        public DateTime PDDATE
        {
            get { return _PDDATE; }
            set { _PDDATE = value; }
        }
        public DateTime PODATE
        {
            get { return _PODATE; }
            set { _PODATE = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public double PRODUCTMASTER
        {
            get { return _PRODUCTMASTER; }
            set { _PRODUCTMASTER = value; }
        }
        public double PDLOID
        {
            get { return _PDLOID; }
            set { _PDLOID = value; }
        }
        public double POLOID
        {
            get { return _POLOID; }
            set { _POLOID = value; }
        }
        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }
        public double POQTY
        {
            get { return _POQTY; }
            set { _POQTY = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,PLAN,PRODUCT,UNIT,RECEIVEDATE,PDDATE,PODATE,STATUS,PRODUCTMASTER,PDLOID,POLOID,PDQTY,POQTY)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += "  " + _PLAN.ToString() + ",";// PLAN";
                sqlz += "  " + _PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += " " + OracleDB.QRDateTime(_RECEIVEDATE) + ",";// RECEIVEDATE";
                sqlz += " " + OracleDB.QRDateTime(_PDDATE) + ",";// PDDATE";
                sqlz += " " + OracleDB.QRDateTime(_PODATE) + ",";// PODATE";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += "  " + _PRODUCTMASTER.ToString() + ",";// PRODUCTMASTER";
                sqlz += "  " + _PDLOID.ToString() + ",";// PDLOID";
                sqlz += "  " + _POLOID.ToString() + ",";// POLOID";
                sqlz += "  " + _PDQTY.ToString() + ",";// PDQTY";
                sqlz += "  " + _POQTY.ToString() + "";// POQTY";
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
                sqlz += " PLAN  = " + _PLAN.ToString() + ", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString() + ", ";
                sqlz += " UNIT  = " + _UNIT.ToString() + ", ";
                sqlz += " RECEIVEDATE  = " + OracleDB.QRDateTime(_RECEIVEDATE) + ", ";
                sqlz += " PDDATE  = " + OracleDB.QRDateTime(_PDDATE) + ", ";
                sqlz += " PODATE  = " + OracleDB.QRDateTime(_PODATE) + ", ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " PRODUCTMASTER  = " + _PRODUCTMASTER.ToString() + ", ";
                sqlz += " PDLOID  = " + _PDLOID.ToString() + ", ";
                sqlz += " POLOID  = " + _POLOID.ToString() + ", ";
                sqlz += " PDQTY  = " + _PDQTY.ToString() + ", ";
                sqlz += " POQTY  = " + _POQTY.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["PLAN"])) _PLAN = Convert.ToDouble(zRdr["PLAN"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["RECEIVEDATE"])) _RECEIVEDATE = OracleDB.DBDate(zRdr["RECEIVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["PDDATE"])) _PDDATE = OracleDB.DBDate(zRdr["PDDATE"]);
                        if (!Convert.IsDBNull(zRdr["PODATE"])) _PODATE = OracleDB.DBDate(zRdr["PODATE"]);
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCTMASTER"])) _PRODUCTMASTER = Convert.ToDouble(zRdr["PRODUCTMASTER"]);
                        if (!Convert.IsDBNull(zRdr["PDLOID"])) _PDLOID = Convert.ToDouble(zRdr["PDLOID"]);
                        if (!Convert.IsDBNull(zRdr["POLOID"])) _POLOID = Convert.ToDouble(zRdr["POLOID"]);
                        if (!Convert.IsDBNull(zRdr["PDQTY"])) _PDQTY = Convert.ToDouble(zRdr["PDQTY"]);
                        if (!Convert.IsDBNull(zRdr["POQTY"])) _POQTY = Convert.ToDouble(zRdr["POQTY"]);
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

        public bool GetData(double plan, double product, DateTime receiveDate, OracleTransaction zTrans)
        {
            return doGetdata(" STATUS = '" + Constz.Requisition.Status.Waiting.Code + "' AND PLAN = " + plan.ToString() + " AND PRODUCT = " + product.ToString() + " AND TO_CHAR(RECEIVEDATE, 'YYYYMMDD') = '" + receiveDate.Year.ToString() + receiveDate.ToString("MMdd") + "' ", zTrans);
        }

        public DataTable GetDataByPlan(double plan, OracleTransaction zTrans)
        {
            string tmpWhere = " WHERE PLAN = " + plan.ToString();
            return OracleDB.ExecListCmd(sql_select + tmpWhere, zTrans);
        }

        public bool DeleteDataByPlan(double plan, OracleTransaction zTrans)
        {
            return doDelete(" PLAN = " + plan.ToString() + " ", zTrans);
        }

        public bool UpdateStatusByPlan(double plan, string status, string userID, OracleTransaction zTrans)
        {
            bool ret = true;
            string sql = "UPDATE " + TableName + " SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
            sql += "WHERE PLAN = " + plan.ToString() + " ";
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