using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class PlanUseDAL
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

        private string tableName = "PLANUSE";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
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
        string _STATUS = "";
        double _DAY1 = 0;
        double _DAY2 = 0;
        double _DAY3 = 0;
        double _DAY4 = 0;
        double _DAY5 = 0;
        double _DAY6 = 0;
        double _DAY7 = 0;
        double _DAY8 = 0;
        double _DAY9 = 0;
        double _DAY10 = 0;
        double _DAY11 = 0;
        double _DAY12 = 0;
        double _DAY13 = 0;
        double _DAY14 = 0;
        double _DAY15 = 0;
        double _DAY16 = 0;
        double _DAY17 = 0;
        double _DAY18 = 0;
        double _DAY19 = 0;
        double _DAY20 = 0;
        double _DAY21 = 0;
        double _DAY22 = 0;
        double _DAY23 = 0;
        double _DAY24 = 0;
        double _DAY25 = 0;
        double _DAY26 = 0;
        double _DAY27 = 0;
        double _DAY28 = 0;
        double _DAY29 = 0;
        double _DAY30 = 0;
        double _DAY31 = 0;
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
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public double DAY1
        {
            get { return _DAY1; }
            set { _DAY1 = value; }
        }
        public double DAY2
        {
            get { return _DAY2; }
            set { _DAY2 = value; }
        }
        public double DAY3
        {
            get { return _DAY3; }
            set { _DAY3 = value; }
        }
        public double DAY4
        {
            get { return _DAY4; }
            set { _DAY4 = value; }
        }
        public double DAY5
        {
            get { return _DAY5; }
            set { _DAY5 = value; }
        }
        public double DAY6
        {
            get { return _DAY6; }
            set { _DAY6 = value; }
        }
        public double DAY7
        {
            get { return _DAY7; }
            set { _DAY7 = value; }
        }
        public double DAY8
        {
            get { return _DAY8; }
            set { _DAY8 = value; }
        }
        public double DAY9
        {
            get { return _DAY9; }
            set { _DAY9 = value; }
        }
        public double DAY10
        {
            get { return _DAY10; }
            set { _DAY10 = value; }
        }
        public double DAY11
        {
            get { return _DAY11; }
            set { _DAY11 = value; }
        }
        public double DAY12
        {
            get { return _DAY12; }
            set { _DAY12 = value; }
        }
        public double DAY13
        {
            get { return _DAY13; }
            set { _DAY13 = value; }
        }
        public double DAY14
        {
            get { return _DAY14; }
            set { _DAY14 = value; }
        }
        public double DAY15
        {
            get { return _DAY15; }
            set { _DAY15 = value; }
        }
        public double DAY16
        {
            get { return _DAY16; }
            set { _DAY16 = value; }
        }
        public double DAY17
        {
            get { return _DAY17; }
            set { _DAY17 = value; }
        }
        public double DAY18
        {
            get { return _DAY18; }
            set { _DAY18 = value; }
        }
        public double DAY19
        {
            get { return _DAY19; }
            set { _DAY19 = value; }
        }
        public double DAY20
        {
            get { return _DAY20; }
            set { _DAY20 = value; }
        }
        public double DAY21
        {
            get { return _DAY21; }
            set { _DAY21 = value; }
        }
        public double DAY22
        {
            get { return _DAY22; }
            set { _DAY22 = value; }
        }
        public double DAY23
        {
            get { return _DAY23; }
            set { _DAY23 = value; }
        }
        public double DAY24
        {
            get { return _DAY24; }
            set { _DAY24 = value; }
        }
        public double DAY25
        {
            get { return _DAY25; }
            set { _DAY25 = value; }
        }
        public double DAY26
        {
            get { return _DAY26; }
            set { _DAY26 = value; }
        }
        public double DAY27
        {
            get { return _DAY27; }
            set { _DAY27 = value; }
        }
        public double DAY28
        {
            get { return _DAY28; }
            set { _DAY28 = value; }
        }
        public double DAY29
        {
            get { return _DAY29; }
            set { _DAY29 = value; }
        }
        public double DAY30
        {
            get { return _DAY30; }
            set { _DAY30 = value; }
        }
        public double DAY31
        {
            get { return _DAY31; }
            set { _DAY31 = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (PRODUCTMASTER,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,PLAN,PRODUCT,UNIT,MONTH,STATUS,DAY1,DAY2,DAY3,DAY4,DAY5,DAY6,DAY7,DAY8,DAY9,DAY10,DAY11,DAY12,DAY13,DAY14,DAY15,DAY16,DAY17,DAY18,DAY19,DAY20,DAY21,DAY22,DAY23,DAY24,DAY25,DAY26,DAY27,DAY28,DAY29,DAY30,DAY31)VALUES(";
                sqlz += "  " + _PRODUCTMASTER.ToString() + ",";// PRODUCTMASTER";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += "  " + _PLAN.ToString() + ",";// PLAN";
                sqlz += "  " + _PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += "  " + _MONTH.ToString() + ",";// MONTH";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += "  " + _DAY1.ToString() + ",";// DAY1";
                sqlz += "  " + _DAY2.ToString() + ",";// DAY2";
                sqlz += "  " + _DAY3.ToString() + ",";// DAY3";
                sqlz += "  " + _DAY4.ToString() + ",";// DAY4";
                sqlz += "  " + _DAY5.ToString() + ",";// DAY5";
                sqlz += "  " + _DAY6.ToString() + ",";// DAY6";
                sqlz += "  " + _DAY7.ToString() + ",";// DAY7";
                sqlz += "  " + _DAY8.ToString() + ",";// DAY8";
                sqlz += "  " + _DAY9.ToString() + ",";// DAY9";
                sqlz += "  " + _DAY10.ToString() + ",";// DAY10";
                sqlz += "  " + _DAY11.ToString() + ",";// DAY11";
                sqlz += "  " + _DAY12.ToString() + ",";// DAY12";
                sqlz += "  " + _DAY13.ToString() + ",";// DAY13";
                sqlz += "  " + _DAY14.ToString() + ",";// DAY14";
                sqlz += "  " + _DAY15.ToString() + ",";// DAY15";
                sqlz += "  " + _DAY16.ToString() + ",";// DAY16";
                sqlz += "  " + _DAY17.ToString() + ",";// DAY17";
                sqlz += "  " + _DAY18.ToString() + ",";// DAY18";
                sqlz += "  " + _DAY19.ToString() + ",";// DAY19";
                sqlz += "  " + _DAY20.ToString() + ",";// DAY20";
                sqlz += "  " + _DAY21.ToString() + ",";// DAY21";
                sqlz += "  " + _DAY22.ToString() + ",";// DAY22";
                sqlz += "  " + _DAY23.ToString() + ",";// DAY23";
                sqlz += "  " + _DAY24.ToString() + ",";// DAY24";
                sqlz += "  " + _DAY25.ToString() + ",";// DAY25";
                sqlz += "  " + _DAY26.ToString() + ",";// DAY26";
                sqlz += "  " + _DAY27.ToString() + ",";// DAY27";
                sqlz += "  " + _DAY28.ToString() + ",";// DAY28";
                sqlz += "  " + _DAY29.ToString() + ",";// DAY29";
                sqlz += "  " + _DAY30.ToString() + ",";// DAY30";
                sqlz += "  " + _DAY31.ToString() + "";// DAY31";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " PRODUCTMASTER  = " + _PRODUCTMASTER.ToString() + ", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " PLAN  = " + _PLAN.ToString() + ", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString() + ", ";
                sqlz += " UNIT  = " + _UNIT.ToString() + ", ";
                sqlz += " MONTH  = " + _MONTH.ToString() + ", ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " DAY1  = " + _DAY1.ToString() + ", ";
                sqlz += " DAY2  = " + _DAY2.ToString() + ", ";
                sqlz += " DAY3  = " + _DAY3.ToString() + ", ";
                sqlz += " DAY4  = " + _DAY4.ToString() + ", ";
                sqlz += " DAY5  = " + _DAY5.ToString() + ", ";
                sqlz += " DAY6  = " + _DAY6.ToString() + ", ";
                sqlz += " DAY7  = " + _DAY7.ToString() + ", ";
                sqlz += " DAY8  = " + _DAY8.ToString() + ", ";
                sqlz += " DAY9  = " + _DAY9.ToString() + ", ";
                sqlz += " DAY10  = " + _DAY10.ToString() + ", ";
                sqlz += " DAY11  = " + _DAY11.ToString() + ", ";
                sqlz += " DAY12  = " + _DAY12.ToString() + ", ";
                sqlz += " DAY13  = " + _DAY13.ToString() + ", ";
                sqlz += " DAY14  = " + _DAY14.ToString() + ", ";
                sqlz += " DAY15  = " + _DAY15.ToString() + ", ";
                sqlz += " DAY16  = " + _DAY16.ToString() + ", ";
                sqlz += " DAY17  = " + _DAY17.ToString() + ", ";
                sqlz += " DAY18  = " + _DAY18.ToString() + ", ";
                sqlz += " DAY19  = " + _DAY19.ToString() + ", ";
                sqlz += " DAY20  = " + _DAY20.ToString() + ", ";
                sqlz += " DAY21  = " + _DAY21.ToString() + ", ";
                sqlz += " DAY22  = " + _DAY22.ToString() + ", ";
                sqlz += " DAY23  = " + _DAY23.ToString() + ", ";
                sqlz += " DAY24  = " + _DAY24.ToString() + ", ";
                sqlz += " DAY25  = " + _DAY25.ToString() + ", ";
                sqlz += " DAY26  = " + _DAY26.ToString() + ", ";
                sqlz += " DAY27  = " + _DAY27.ToString() + ", ";
                sqlz += " DAY28  = " + _DAY28.ToString() + ", ";
                sqlz += " DAY29  = " + _DAY29.ToString() + ", ";
                sqlz += " DAY30  = " + _DAY30.ToString() + ", ";
                sqlz += " DAY31  = " + _DAY31.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["DAY1"])) _DAY1 = Convert.ToDouble(zRdr["DAY1"]);
                        if (!Convert.IsDBNull(zRdr["DAY2"])) _DAY2 = Convert.ToDouble(zRdr["DAY2"]);
                        if (!Convert.IsDBNull(zRdr["DAY3"])) _DAY3 = Convert.ToDouble(zRdr["DAY3"]);
                        if (!Convert.IsDBNull(zRdr["DAY4"])) _DAY4 = Convert.ToDouble(zRdr["DAY4"]);
                        if (!Convert.IsDBNull(zRdr["DAY5"])) _DAY5 = Convert.ToDouble(zRdr["DAY5"]);
                        if (!Convert.IsDBNull(zRdr["DAY6"])) _DAY6 = Convert.ToDouble(zRdr["DAY6"]);
                        if (!Convert.IsDBNull(zRdr["DAY7"])) _DAY7 = Convert.ToDouble(zRdr["DAY7"]);
                        if (!Convert.IsDBNull(zRdr["DAY8"])) _DAY8 = Convert.ToDouble(zRdr["DAY8"]);
                        if (!Convert.IsDBNull(zRdr["DAY9"])) _DAY9 = Convert.ToDouble(zRdr["DAY9"]);
                        if (!Convert.IsDBNull(zRdr["DAY10"])) _DAY10 = Convert.ToDouble(zRdr["DAY10"]);
                        if (!Convert.IsDBNull(zRdr["DAY11"])) _DAY11 = Convert.ToDouble(zRdr["DAY11"]);
                        if (!Convert.IsDBNull(zRdr["DAY12"])) _DAY12 = Convert.ToDouble(zRdr["DAY12"]);
                        if (!Convert.IsDBNull(zRdr["DAY13"])) _DAY13 = Convert.ToDouble(zRdr["DAY13"]);
                        if (!Convert.IsDBNull(zRdr["DAY14"])) _DAY14 = Convert.ToDouble(zRdr["DAY14"]);
                        if (!Convert.IsDBNull(zRdr["DAY15"])) _DAY15 = Convert.ToDouble(zRdr["DAY15"]);
                        if (!Convert.IsDBNull(zRdr["DAY16"])) _DAY16 = Convert.ToDouble(zRdr["DAY16"]);
                        if (!Convert.IsDBNull(zRdr["DAY17"])) _DAY17 = Convert.ToDouble(zRdr["DAY17"]);
                        if (!Convert.IsDBNull(zRdr["DAY18"])) _DAY18 = Convert.ToDouble(zRdr["DAY18"]);
                        if (!Convert.IsDBNull(zRdr["DAY19"])) _DAY19 = Convert.ToDouble(zRdr["DAY19"]);
                        if (!Convert.IsDBNull(zRdr["DAY20"])) _DAY20 = Convert.ToDouble(zRdr["DAY20"]);
                        if (!Convert.IsDBNull(zRdr["DAY21"])) _DAY21 = Convert.ToDouble(zRdr["DAY21"]);
                        if (!Convert.IsDBNull(zRdr["DAY22"])) _DAY22 = Convert.ToDouble(zRdr["DAY22"]);
                        if (!Convert.IsDBNull(zRdr["DAY23"])) _DAY23 = Convert.ToDouble(zRdr["DAY23"]);
                        if (!Convert.IsDBNull(zRdr["DAY24"])) _DAY24 = Convert.ToDouble(zRdr["DAY24"]);
                        if (!Convert.IsDBNull(zRdr["DAY25"])) _DAY25 = Convert.ToDouble(zRdr["DAY25"]);
                        if (!Convert.IsDBNull(zRdr["DAY26"])) _DAY26 = Convert.ToDouble(zRdr["DAY26"]);
                        if (!Convert.IsDBNull(zRdr["DAY27"])) _DAY27 = Convert.ToDouble(zRdr["DAY27"]);
                        if (!Convert.IsDBNull(zRdr["DAY28"])) _DAY28 = Convert.ToDouble(zRdr["DAY28"]);
                        if (!Convert.IsDBNull(zRdr["DAY29"])) _DAY29 = Convert.ToDouble(zRdr["DAY29"]);
                        if (!Convert.IsDBNull(zRdr["DAY30"])) _DAY30 = Convert.ToDouble(zRdr["DAY30"]);
                        if (!Convert.IsDBNull(zRdr["DAY31"])) _DAY31 = Convert.ToDouble(zRdr["DAY31"]);
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

        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
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