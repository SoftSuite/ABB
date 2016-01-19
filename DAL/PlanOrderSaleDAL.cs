using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class PlanOrderSaleDAL
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

        private string tableName = "PLANORDERSALE";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        string _STATUS = "";
        double _SALEMAN = 0;
        double _M1 = 0;
        double _M2 = 0;
        double _M3 = 0;
        double _M4 = 0;
        double _M5 = 0;
        double _M6 = 0;
        double _M7 = 0;
        double _M8 = 0;
        double _M9 = 0;
        double _M10 = 0;
        double _M11 = 0;
        double _M12 = 0;
        double _PRODUCTMASTER = 0;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _PLAN = 0;
        double _PRODUCT = 0;
        double _UNIT = 0;
        double _MONTH = 0;
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
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public double SALEMAN
        {
            get { return _SALEMAN; }
            set { _SALEMAN = value; }
        }
        public double M1
        {
            get { return _M1; }
            set { _M1 = value; }
        }
        public double M2
        {
            get { return _M2; }
            set { _M2 = value; }
        }
        public double M3
        {
            get { return _M3; }
            set { _M3 = value; }
        }
        public double M4
        {
            get { return _M4; }
            set { _M4 = value; }
        }
        public double M5
        {
            get { return _M5; }
            set { _M5 = value; }
        }
        public double M6
        {
            get { return _M6; }
            set { _M6 = value; }
        }
        public double M7
        {
            get { return _M7; }
            set { _M7 = value; }
        }
        public double M8
        {
            get { return _M8; }
            set { _M8 = value; }
        }
        public double M9
        {
            get { return _M9; }
            set { _M9 = value; }
        }
        public double M10
        {
            get { return _M10; }
            set { _M10 = value; }
        }
        public double M11
        {
            get { return _M11; }
            set { _M11 = value; }
        }
        public double M12
        {
            get { return _M12; }
            set { _M12 = value; }
        }
        public double PRODUCTMASTER
        {
            get { return _PRODUCTMASTER; }
            set { _PRODUCTMASTER = value; }
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
        public double MONTH
        {
            get { return _MONTH; }
            set { _MONTH = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (STATUS,SALEMAN,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12,PRODUCTMASTER,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,PLAN,PRODUCT,UNIT,MONTH)VALUES(";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += "  " + _SALEMAN.ToString() + ",";// SALEMAN";
                sqlz += "  " + _M1.ToString() + ",";// M1";
                sqlz += "  " + _M2.ToString() + ",";// M2";
                sqlz += "  " + _M3.ToString() + ",";// M3";
                sqlz += "  " + _M4.ToString() + ",";// M4";
                sqlz += "  " + _M5.ToString() + ",";// M5";
                sqlz += "  " + _M6.ToString() + ",";// M6";
                sqlz += "  " + _M7.ToString() + ",";// M7";
                sqlz += "  " + _M8.ToString() + ",";// M8";
                sqlz += "  " + _M9.ToString() + ",";// M9";
                sqlz += "  " + _M10.ToString() + ",";// M10";
                sqlz += "  " + _M11.ToString() + ",";// M11";
                sqlz += "  " + _M12.ToString() + ",";// M12";
                sqlz += "  " + _PRODUCTMASTER.ToString() + ",";// PRODUCTMASTER";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += "  " + _PLAN.ToString() + ",";// PLAN";
                sqlz += "  " + _PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += "  " + _MONTH.ToString() + "";// MONTH";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " SALEMAN  = " + _SALEMAN.ToString() + ", ";
                sqlz += " M1  = " + _M1.ToString() + ", ";
                sqlz += " M2  = " + _M2.ToString() + ", ";
                sqlz += " M3  = " + _M3.ToString() + ", ";
                sqlz += " M4  = " + _M4.ToString() + ", ";
                sqlz += " M5  = " + _M5.ToString() + ", ";
                sqlz += " M6  = " + _M6.ToString() + ", ";
                sqlz += " M7  = " + _M7.ToString() + ", ";
                sqlz += " M8  = " + _M8.ToString() + ", ";
                sqlz += " M9  = " + _M9.ToString() + ", ";
                sqlz += " M10  = " + _M10.ToString() + ", ";
                sqlz += " M11  = " + _M11.ToString() + ", ";
                sqlz += " M12  = " + _M12.ToString() + ", ";
                sqlz += " PRODUCTMASTER  = " + _PRODUCTMASTER.ToString() + ", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " PLAN  = " + _PLAN.ToString() + ", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString() + ", ";
                sqlz += " UNIT  = " + _UNIT.ToString() + ", ";
                sqlz += " MONTH  = " + _MONTH.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["SALEMAN"])) _SALEMAN = Convert.ToDouble(zRdr["SALEMAN"]);
                        if (!Convert.IsDBNull(zRdr["M1"])) _M1 = Convert.ToDouble(zRdr["M1"]);
                        if (!Convert.IsDBNull(zRdr["M2"])) _M2 = Convert.ToDouble(zRdr["M2"]);
                        if (!Convert.IsDBNull(zRdr["M3"])) _M3 = Convert.ToDouble(zRdr["M3"]);
                        if (!Convert.IsDBNull(zRdr["M4"])) _M4 = Convert.ToDouble(zRdr["M4"]);
                        if (!Convert.IsDBNull(zRdr["M5"])) _M5 = Convert.ToDouble(zRdr["M5"]);
                        if (!Convert.IsDBNull(zRdr["M6"])) _M6 = Convert.ToDouble(zRdr["M6"]);
                        if (!Convert.IsDBNull(zRdr["M7"])) _M7 = Convert.ToDouble(zRdr["M7"]);
                        if (!Convert.IsDBNull(zRdr["M8"])) _M8 = Convert.ToDouble(zRdr["M8"]);
                        if (!Convert.IsDBNull(zRdr["M9"])) _M9 = Convert.ToDouble(zRdr["M9"]);
                        if (!Convert.IsDBNull(zRdr["M10"])) _M10 = Convert.ToDouble(zRdr["M10"]);
                        if (!Convert.IsDBNull(zRdr["M11"])) _M11 = Convert.ToDouble(zRdr["M11"]);
                        if (!Convert.IsDBNull(zRdr["M12"])) _M12 = Convert.ToDouble(zRdr["M12"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCTMASTER"])) _PRODUCTMASTER = Convert.ToDouble(zRdr["PRODUCTMASTER"]);
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["PLAN"])) _PLAN = Convert.ToDouble(zRdr["PLAN"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["MONTH"])) _MONTH = Convert.ToDouble(zRdr["MONTH"]);
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

        public DataTable GetDataByPlan(double plan, OracleTransaction zTrans)
        {
            string tmpWhere = " WHERE PLAN = " + plan.ToString();
            return OracleDB.ExecListCmd(sql_select + tmpWhere, zTrans);
        }

        public bool GetDataByPlanAndProductAndSale(double plan, double product, double saleman, OracleTransaction zTrans)
        {
            string tmpWhere = " PLAN = " + plan.ToString() + " AND PRODUCT = " + product.ToString() + " AND SALEMAN = " + saleman.ToString() + " ";
            return doGetdata(tmpWhere, zTrans);
        }

        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteDataByPlan(double plan, OracleTransaction zTrans)
        {
            return doDelete(" PLAN = " + plan.ToString() + " ", zTrans);
        }

        public bool DeleteDataByPlanAndProduct(double plan, double product, OracleTransaction zTrans)
        {
            return doDelete(" PLAN = " + plan.ToString() + " AND PRODUCT = " + product.ToString() + " ", zTrans);
        }

        public bool DeleteDataByPlanAndProductExceptLOID(double plan, double product, string exceptLOID, OracleTransaction zTrans)
        {
            return doDelete(" PLAN = " + plan.ToString() + " AND PRODUCT = " + product.ToString() + " AND LOID NOT IN (" + exceptLOID + ") ", zTrans);
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

        public bool ResetQuantityByPlanAndProductAndMonth(double plan, double product, int month, string userID, OracleTransaction zTrans)
        {
            bool ret = true;
            string sql = "UPDATE " + TableName + " SET M" + month.ToString() + " = 0, ";
            sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
            sql += "WHERE PLAN = " + plan.ToString() + " AND PRODUCT = " + product.ToString() + " ";
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

        //public bool ResetQuantityByPlanAndProduct(double plan, double product, string userID, OracleTransaction zTrans)
        //{
        //    bool ret = true;
        //    string sql = "UPDATE " + TableName + " SET M1 = 0, M2 = 0, M3 = 0, M4 = 0, M5 = 0, M6 = 0, M7 = 0, M8 = 0, M9 = 0, M10 = 0, M11 = 0, M12 = 0,";
        //    sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
        //    sql += "WHERE PLAN = " + plan.ToString() + " AND PRODUCT = " + product.ToString() + " ";
        //    try
        //    {
        //        ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
        //        if (!ret) _error = OracleDB.Err_NoUpdate;
        //    }
        //    catch (OracleException ex)
        //    {
        //        ret = false;
        //        _error = OracleDB.GetOracleExceptionText(ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        ret = false;
        //        _error = ex.Message;
        //    }
        //    return ret;
        //}

    }
}