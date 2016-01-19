using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class PlanSaleItemDAL
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

        private string tableName = "PLANSALEITEM";

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
        double _PLANQTY1 = 0;
        double _PLANQTY2 = 0;
        double _PLANQTY3 = 0;
        double _PLANQTY4 = 0;
        double _PLANQTY5 = 0;
        double _PLANQTY6 = 0;
        double _PLANQTY7 = 0;
        double _PLANQTY8 = 0;
        double _PLANQTY9 = 0;
        double _PLANQTY10 = 0;
        double _PLANQTY11 = 0;
        double _PLANQTY12 = 0;
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
        public double PLANQTY1
        {
            get { return _PLANQTY1; }
            set { _PLANQTY1 = value; }
        }
        public double PLANQTY2
        {
            get { return _PLANQTY2; }
            set { _PLANQTY2 = value; }
        }
        public double PLANQTY3
        {
            get { return _PLANQTY3; }
            set { _PLANQTY3 = value; }
        }
        public double PLANQTY4
        {
            get { return _PLANQTY4; }
            set { _PLANQTY4 = value; }
        }
        public double PLANQTY5
        {
            get { return _PLANQTY5; }
            set { _PLANQTY5 = value; }
        }
        public double PLANQTY6
        {
            get { return _PLANQTY6; }
            set { _PLANQTY6 = value; }
        }
        public double PLANQTY7
        {
            get { return _PLANQTY7; }
            set { _PLANQTY7 = value; }
        }
        public double PLANQTY8
        {
            get { return _PLANQTY8; }
            set { _PLANQTY8 = value; }
        }
        public double PLANQTY9
        {
            get { return _PLANQTY9; }
            set { _PLANQTY9 = value; }
        }
        public double PLANQTY10
        {
            get { return _PLANQTY10; }
            set { _PLANQTY10 = value; }
        }
        public double PLANQTY11
        {
            get { return _PLANQTY11; }
            set { _PLANQTY11 = value; }
        }
        public double PLANQTY12
        {
            get { return _PLANQTY12; }
            set { _PLANQTY12 = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,PLAN,PRODUCT,UNIT,PLANQTY1,PLANQTY2,PLANQTY3,PLANQTY4,PLANQTY5,PLANQTY6,PLANQTY7,PLANQTY8,PLANQTY9,PLANQTY10,PLANQTY11,PLANQTY12)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += "  " + _PLAN.ToString() + ",";// PLAN";
                sqlz += "  " + _PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += "  " + _PLANQTY1.ToString() + ",";// PLANQTY1";
                sqlz += "  " + _PLANQTY2.ToString() + ",";// PLANQTY2";
                sqlz += "  " + _PLANQTY3.ToString() + ",";// PLANQTY3";
                sqlz += "  " + _PLANQTY4.ToString() + ",";// PLANQTY4";
                sqlz += "  " + _PLANQTY5.ToString() + ",";// PLANQTY5";
                sqlz += "  " + _PLANQTY6.ToString() + ",";// PLANQTY6";
                sqlz += "  " + _PLANQTY7.ToString() + ",";// PLANQTY7";
                sqlz += "  " + _PLANQTY8.ToString() + ",";// PLANQTY8";
                sqlz += "  " + _PLANQTY9.ToString() + ",";// PLANQTY9";
                sqlz += "  " + _PLANQTY10.ToString() + ",";// PLANQTY10";
                sqlz += "  " + _PLANQTY11.ToString() + ",";// PLANQTY11";
                sqlz += "  " + _PLANQTY12.ToString() + "";// PLANQTY12";
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
                sqlz += " PLANQTY1  = " + _PLANQTY1.ToString() + ", ";
                sqlz += " PLANQTY2  = " + _PLANQTY2.ToString() + ", ";
                sqlz += " PLANQTY3  = " + _PLANQTY3.ToString() + ", ";
                sqlz += " PLANQTY4  = " + _PLANQTY4.ToString() + ", ";
                sqlz += " PLANQTY5  = " + _PLANQTY5.ToString() + ", ";
                sqlz += " PLANQTY6  = " + _PLANQTY6.ToString() + ", ";
                sqlz += " PLANQTY7  = " + _PLANQTY7.ToString() + ", ";
                sqlz += " PLANQTY8  = " + _PLANQTY8.ToString() + ", ";
                sqlz += " PLANQTY9  = " + _PLANQTY9.ToString() + ", ";
                sqlz += " PLANQTY10  = " + _PLANQTY10.ToString() + ", ";
                sqlz += " PLANQTY11  = " + _PLANQTY11.ToString() + ", ";
                sqlz += " PLANQTY12  = " + _PLANQTY12.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["PLANQTY1"])) _PLANQTY1 = Convert.ToDouble(zRdr["PLANQTY1"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY2"])) _PLANQTY2 = Convert.ToDouble(zRdr["PLANQTY2"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY3"])) _PLANQTY3 = Convert.ToDouble(zRdr["PLANQTY3"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY4"])) _PLANQTY4 = Convert.ToDouble(zRdr["PLANQTY4"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY5"])) _PLANQTY5 = Convert.ToDouble(zRdr["PLANQTY5"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY6"])) _PLANQTY6 = Convert.ToDouble(zRdr["PLANQTY6"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY7"])) _PLANQTY7 = Convert.ToDouble(zRdr["PLANQTY7"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY8"])) _PLANQTY8 = Convert.ToDouble(zRdr["PLANQTY8"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY9"])) _PLANQTY9 = Convert.ToDouble(zRdr["PLANQTY9"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY10"])) _PLANQTY10 = Convert.ToDouble(zRdr["PLANQTY10"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY11"])) _PLANQTY11 = Convert.ToDouble(zRdr["PLANQTY11"]);
                        if (!Convert.IsDBNull(zRdr["PLANQTY12"])) _PLANQTY12 = Convert.ToDouble(zRdr["PLANQTY12"]);
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

    }
}