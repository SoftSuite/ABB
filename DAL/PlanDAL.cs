using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class PlanDAL
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

        private string tableName = "PLAN";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        string _PLANTYPE = "";
        string _DESCRIPTION = "";
        DateTime _CONFIRMDATE = new DateTime(1, 1, 1);
        string _ACTIVE = "";
        string _STATUS = "";
        string _YEAR = "";
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
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public string PLANTYPE
        {
            get { return _PLANTYPE; }
            set { _PLANTYPE = value; }
        }
        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        public DateTime CONFIRMDATE
        {
            get { return _CONFIRMDATE; }
            set { _CONFIRMDATE = value; }
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
        public string YEAR
        {
            get { return _YEAR; }
            set { _YEAR = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,CODE,PLANTYPE,DESCRIPTION,CONFIRMDATE,ACTIVE,STATUS,YEAR)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " '" + OracleDB.QRText(_PLANTYPE) + "',";// PLANTYPE";
                sqlz += " '" + OracleDB.QRText(_DESCRIPTION) + "',";// DESCRIPTION";
                sqlz += " " + OracleDB.QRDateTime(_CONFIRMDATE) + ",";// CONFIRMDATE";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " '" + OracleDB.QRText(_YEAR) + "'";// YEAR";
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
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                sqlz += " PLANTYPE  = '" + OracleDB.QRText(_PLANTYPE) + "', ";
                sqlz += " DESCRIPTION  = '" + OracleDB.QRText(_DESCRIPTION) + "', ";
                sqlz += " CONFIRMDATE  = " + OracleDB.QRDateTime(_CONFIRMDATE) + ", ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " YEAR  = '" + OracleDB.QRText(_YEAR) + "' ";
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
                    if (_CODE == "") _CODE = OracleDB.GetRunningCode(tableName, _PLANTYPE, zTrans);
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
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["PLANTYPE"])) _PLANTYPE = zRdr["PLANTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["DESCRIPTION"])) _DESCRIPTION = zRdr["DESCRIPTION"].ToString();
                        if (!Convert.IsDBNull(zRdr["CONFIRMDATE"])) _CONFIRMDATE = OracleDB.DBDate(zRdr["CONFIRMDATE"]);
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["YEAR"])) _YEAR = zRdr["YEAR"].ToString();
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
        /// ตรวจสอบว่ามีการสร้างแผนซ้ำที่ไม่อยู่ในสถานะยกเลิกในประเภทและปีเดียวกันหรือไม่
        /// </summary>
        public bool IsDuplicateData(string year, string planType, OracleTransaction zTrans)
        {
            _OnDB = false;
            doGetdata(" YEAR = '" + year + "' AND PLANTYPE =  '" + planType + "' AND STATUS <> '" + Constz.Requisition.Status.Void.Code + "' ", zTrans);
            if (_OnDB) { _error = "ไม่สามารถสร้างแผนได้ เนื่องจากมีแผนประจำปี " + year + " อยู่แล้ว"; }
            return _OnDB;
        }
        
        /// <summary>
        /// คัดลอกรายละเอียดแผนการจำหน่ายสินค้า
        /// </summary>
        public bool CopyPlan(string userID, double planSource, double planDestination, OracleTransaction zTrans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_COPYPLAN('" + userID + "', " + planSource.ToString() + ", " + planDestination.ToString() + " )", zTrans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// ยกเลิกแผนการจำหน่ายสินค้า
        /// </summary>
        public bool CancelPlanSale(string userID, double plan, OracleTransaction zTrans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CANCELPLANSALE('" + userID + "', " + plan.ToString() + " )", zTrans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// ตรวจสอบว่ามีการสร้างแผนการจำหน่ายสินค้าหรือไม่
        /// </summary>
        public bool HasPlanOrder(string year, OracleTransaction zTrans)
        {
            _OnDB = false;
            doGetdata(" YEAR = '" + year + "' AND PLANTYPE =  '" + Constz.PlanType.SA + "' AND STATUS = '" + Constz.Requisition.Status.Approved.Code + "' ", zTrans);
            if (!_OnDB) { _error = "ไม่สามารถสร้างแผนได้ ต้องสร้างแผนการจำหน่ายสินค้าก่อน"; }
            return (_OnDB);
        }

        /// <summary>
        /// เพิ่มแผนการสั่งสินค้า
        /// </summary>
        public bool InsertFGPlan(string userID, double plan, string year, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_INSERTFGPLAN('" + userID + "', " + plan.ToString() + ", " + year.ToString() + " )", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool CalculateFGPlanUseAndRemain(string userID, double plan, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CALFGPLANUSEANDREMAIN('" + userID + "', " + plan.ToString() + " )", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool CalculateFGPlanRemainByProduct(string userID, double plan, double product, double productMaster, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CALFGPLANREMAINPRODUCT('" + userID + "', " + plan.ToString() + ", " + product.ToString() + ", " + productMaster.ToString() + ")", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// เพิ่มแผนการสั่งวัตถุดิบ
        /// </summary>
        public bool InsertWHPlan(string userID, double plan, string year, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_INSERTWHPLAN('" + userID + "', " + plan.ToString() + ", " + year.ToString() + " )", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool CalculateWHPlanUseAndRemain(string userID, double plan, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CALWHPLANUSEANDREMAIN('" + userID + "', " + plan.ToString() + " )", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool CalculateWHPlanRemainByProduct(string userID, double plan, double product, double productMaster, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CALWHPLANREMAINPRODUCT('" + userID + "', " + plan.ToString() + ", " + product.ToString() + ", " + productMaster.ToString() + ")", trans);
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