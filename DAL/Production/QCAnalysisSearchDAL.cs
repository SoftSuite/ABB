using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL.Production
{
    public class QCAnalysisSearchDAL
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

        public bool InsertCurrentDataLoid(string UserID, OracleTransaction zTrans)
        {
            _CREATEBY = UserID;
            _CREATEON = DateTime.Now;
            return doInsertLoid(zTrans);
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
            return doUpdate(" STLOID = " + _STLOID.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zLOID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByLOID(double zLOID, OracleTransaction zTrans)
        {
            return doGetdata(" STLOID = " + zLOID.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteCurrentData(OracleTransaction zTrans)
        {
            return doDelete(" STLOID = " + _STLOID.ToString() + " ", zTrans);
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

        private string tableName = "V_TODOLIST_QC";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _STLOID = 0;
        string _QCCODE = "";
        DateTime _QCDATE = new DateTime(1, 1, 1);
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        double _PDLOID = 0;
        string _PDNAME = "";
        double _QTY = 0;
        string _UNAME = "";
        string _APPROVER = "";
        string _DVNAME = "";
        string _STATUS = "";
        string _STATUSVAL = "";
        string _ORDERTYPE = "";
       
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
        public double STLOID
        {
            get { return _STLOID; }
            set { _STLOID = value; }
        }
        public string QCCODE
        {
            get { return _QCCODE; }
            set { _QCCODE = value; }
        }
        public DateTime QCDATE
        {
            get { return _QCDATE; }
            set { _QCDATE = value; }
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
        public double PDLOID
        {
            get { return _PDLOID; }
            set { _PDLOID = value; }
        }
        public string PDNAME
        {
            get { return _PDNAME; }
            set { _PDNAME = value; }
        }
        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }
        public string UNAME
        {
            get { return _UNAME; }
            set { _UNAME = value; }
        }
        public string APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }
        public string DVNAME
        {
            get { return _DVNAME; }
            set { _DVNAME = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public string STATUSVAL
        {
            get { return _STATUSVAL; }
            set { _STATUSVAL = value; }
        }
        public string ORDERTYPE
        {
            get { return _ORDERTYPE; }
            set { _ORDERTYPE = value; }
        }
       
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (CODE,STLOID,QCCODE,QCDATE,PDNAME,STATUS,STATUSVAL,ORDERTYPE)VALUES(";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += "  " + _STLOID.ToString() + ",";// STLOID";
                sqlz += " '" + OracleDB.QRText(_QCCODE) + "',";// QCCODE";
                sqlz += " " + OracleDB.QRDateTime(_QCDATE) + ",";// QCDATE";
                sqlz += " '" + OracleDB.QRText(_PDNAME) + "',";// CLASTNAME";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// CLASTNAME";
                sqlz += " '" + OracleDB.QRText(_STATUSVAL) + "',";// CLASTNAME";
                sqlz += " '" + OracleDB.QRText(_ORDERTYPE) + "'";// PAYMENTCONDITION";
                sqlz += " ) ";
                return sqlz;
            }
        }


        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                sqlz += " STLOID  = " + _STLOID.ToString() + ", ";
                sqlz += " QCCODE  = '" + OracleDB.QRText(_QCCODE) + "', ";
                sqlz += " QCDATE  = " + OracleDB.QRDateTime(_QCDATE) + ", ";
                sqlz += " PDNAME  = '" + OracleDB.QRText(_PDNAME) + "', ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " STATUSVAL  = '" + OracleDB.QRText(_STATUSVAL) + "', ";
                sqlz += " ORDERTYPE  = '" + OracleDB.QRText(_ORDERTYPE) + "' ";
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
                string sqlz = " SELECT * FROM V_TODOLIST_QC ";
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
                    _STLOID = OracleDB.GetLOID(tableName, zTrans);
                    //if (_CODE == "" && _REQUISITIONTYPE != 0) _CODE = OracleDB.GetRunningCode(TableName, _REQUISITIONTYPE.ToString(), zTrans);
                    //ret = (OracleDB.ExecNonQueryCmd(sql_insert, zTrans) > 0);
                    //if (!ret) _error = OracleDB.Err_NoInsert;
                    //else _OnDB = true;
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

        private bool doInsertLoid(OracleTransaction zTrans)
        {
            bool ret = true;
            if (!_OnDB)
            {
                try
                {
                    _STLOID = OracleDB.GetLOID(tableName, zTrans);
                    //if (_CODE == "" && _REQUISITIONTYPE != 0) _CODE = OracleDB.GetRunningCode(TableName, _REQUISITIONTYPE.ToString(), zTrans);
                    //ret = (OracleDB.ExecNonQueryCmd(sql_insert, zTrans) > 0);
                    //if (!ret) _error = OracleDB.Err_NoInsert;
                    //else _OnDB = true;
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
                        if (!Convert.IsDBNull(zRdr["STLOID"])) _STLOID = Convert.ToDouble(zRdr["STLOID"]);
                        if (!Convert.IsDBNull(zRdr["QCCODE"])) _QCCODE = zRdr["QCCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["QCDATE"])) _QCDATE = OracleDB.DBDate(zRdr["QCDATE"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["PDLOID"])) _PDLOID = Convert.ToDouble(zRdr["PDLOID"]);
                        if (!Convert.IsDBNull(zRdr["PDNAME"])) _PDNAME = zRdr["PDNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["QTY"])) _QTY = Convert.ToDouble(zRdr["QTY"]);
                        if (!Convert.IsDBNull(zRdr["UNAME"])) _UNAME = zRdr["UNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["APPROVER"])) _APPROVER = zRdr["APPROVER"].ToString();
                        if (!Convert.IsDBNull(zRdr["DVNAME"])) _DVNAME = zRdr["DVNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["STATUSVAL"])) _STATUSVAL = zRdr["STATUSVAL"].ToString();
                        if (!Convert.IsDBNull(zRdr["ORDERTYPE"])) _ORDERTYPE = zRdr["ORDERTYPE"].ToString();
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
        /// Update requisition.
        /// </summary>
        /// <param name="loid">The requisition key.</param>
        /// <param name="status">The new status.</param>
        /// <param name="zTrans"></param>
        /// <returns></returns>
        public bool UpdateRequisitionStatus(double loid, string status, string userID, OracleTransaction zTrans)
        {
            string sql = "UPDATE " + tableName + " SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE LOID = " + loid.ToString() + " ";
            //+ (status == Constz.Requisition.Status.Approved.Code ? "AND STATUS = '" + Constz.Requisition.Status.Waiting.Code + "' " : (status == Constz.Requisition.Status.Void.Code ? "AND STATUS = '" + Constz.Requisition.Status.Approved.Code + "' " : ""));
            bool ret = true;
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
                if (!ret) throw new ApplicationException(OracleDB.Err_NoUpdate);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// Update requisition.
        /// </summary>
        /// <param name="loid">The requisition key.</param>
        /// <param name="status">The new status.</param>
        /// <returns></returns>
        public bool UpdateRequisitionStatus(double loid, string status, string userID)
        {
            return UpdateRequisitionStatus(loid, status, userID, null);
        }

        public string GetInvCode(double requisitionType, OracleTransaction trans)
        {
            return OracleDB.GetRunningCode("REQUISITION_INVCODE", requisitionType.ToString(), trans);
        }

        public bool CutStockRequisition(double requisition, string userID, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_UPDATESTOCKRQ(" + requisition.ToString() + ", '" + userID + "' )", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }


        public bool UpdatePDRequestStatus(double p, string status, string userID, OracleTransaction oracleTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}