using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class PDRequestDAL
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

        private string tableName = "PDREQUEST";

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
        DateTime _REQUESTDATE = new DateTime(1, 1, 1);
        string _ORDERTYPE = "";
        double _PURCHASETYPE = 0;
        double _REQUESTBY = 0;
        double _DIVISION = 0;
        string _APPROVER = "";
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _APPROVE = "";
        string _ACTIVE = "";
        string _STATUS = "";
        string _REQUIREMENT = "";
        string _REASON = "";
        string _REMARK = "";
        string _FROMCOMPANY = "";
        double _WAREHOUSE = 0;
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
        public DateTime REQUESTDATE
        {
            get { return _REQUESTDATE; }
            set { _REQUESTDATE = value; }
        }
        public string ORDERTYPE
        {
            get { return _ORDERTYPE; }
            set { _ORDERTYPE = value; }
        }
        public double PURCHASETYPE
        {
            get { return _PURCHASETYPE; }
            set { _PURCHASETYPE = value; }
        }
        public double REQUESTBY
        {
            get { return _REQUESTBY; }
            set { _REQUESTBY = value; }
        }
        public double DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }
        public string APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }
        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }
        public string APPROVE
        {
            get { return _APPROVE; }
            set { _APPROVE = value; }
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
        public string REQUIREMENT
        {
            get { return _REQUIREMENT; }
            set { _REQUIREMENT = value; }
        }
        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public string FROMCOMPANY
        {
            get { return _FROMCOMPANY; }
            set { _FROMCOMPANY = value; }
        }
        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,CODE,REQUESTDATE,ORDERTYPE,PURCHASETYPE,REQUESTBY,DIVISION,APPROVER,APPROVEDATE,APPROVE,ACTIVE,STATUS,REQUIREMENT,REASON,REMARK,FROMCOMPANY,WAREHOUSE)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " " + OracleDB.QRDateTime(_REQUESTDATE) + ",";// REQUESTDATE";
                sqlz += " '" + OracleDB.QRText(_ORDERTYPE) + "',";// ORDERTYPE";
                sqlz += "  " + _PURCHASETYPE.ToString() + ",";// PURCHASETYPE";
                sqlz += "  " + _REQUESTBY.ToString() + ",";// REQUESTBY";
                sqlz += "  " + _DIVISION.ToString() + ",";// DIVISION";
                sqlz += " '" + OracleDB.QRText(_APPROVER) + "',";// APPROVER";
                sqlz += " " + OracleDB.QRDateTime(_APPROVEDATE) + ",";// APPROVEDATE";
                sqlz += " '" + OracleDB.QRText(_APPROVE) + "',";// APPROVE";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " '" + OracleDB.QRText(_REQUIREMENT) + "',";// REQUIREMENT";
                sqlz += " '" + OracleDB.QRText(_REASON) + "',";// REASON";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "', ";// REMARK";
                sqlz += " '" + OracleDB.QRText(_FROMCOMPANY) + "', ";// FROMCOMPANY";
                sqlz += " " + _WAREHOUSE.ToString() + " "; // WAREHOUSE;
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
                sqlz += " REQUESTDATE  = " + OracleDB.QRDateTime(_REQUESTDATE) + ", ";
                sqlz += " ORDERTYPE  = '" + OracleDB.QRText(_ORDERTYPE) + "', ";
                sqlz += " PURCHASETYPE  = " + _PURCHASETYPE.ToString() + ", ";
                sqlz += " REQUESTBY  = " + _REQUESTBY.ToString() + ", ";
                sqlz += " DIVISION  = " + _DIVISION.ToString() + ", ";
                sqlz += " APPROVER  = '" + OracleDB.QRText(_APPROVER) + "', ";
                sqlz += " APPROVEDATE  = " + OracleDB.QRDateTime(_APPROVEDATE) + ", ";
                sqlz += " APPROVE  = '" + OracleDB.QRText(_APPROVE) + "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " REQUIREMENT  = '" + OracleDB.QRText(_REQUIREMENT) + "', ";
                sqlz += " REASON  = '" + OracleDB.QRText(_REASON) + "', ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " FROMCOMPANY  = '" + OracleDB.QRText(_FROMCOMPANY) + "', ";
                sqlz += " WAREHOUSE = " + _WAREHOUSE.ToString() + " ";
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
                    if (_CODE == "")
                    {
                        if (_PURCHASETYPE == Constz.PurchaseType.TYPE06)
                            _CODE = OracleDB.GetRunningCode(_REQUESTDATE, "PR", zTrans);
                        else
                            _CODE = OracleDB.GetRunningCode(TableName, _DIVISION.ToString(), zTrans);
                    }
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
                        if (!Convert.IsDBNull(zRdr["REQUESTDATE"])) _REQUESTDATE = OracleDB.DBDate(zRdr["REQUESTDATE"]);
                        if (!Convert.IsDBNull(zRdr["ORDERTYPE"])) _ORDERTYPE = zRdr["ORDERTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["PURCHASETYPE"])) _PURCHASETYPE = Convert.ToDouble(zRdr["PURCHASETYPE"]);
                        if (!Convert.IsDBNull(zRdr["REQUESTBY"])) _REQUESTBY = Convert.ToDouble(zRdr["REQUESTBY"]);
                        if (!Convert.IsDBNull(zRdr["DIVISION"])) _DIVISION = Convert.ToDouble(zRdr["DIVISION"]);
                        if (!Convert.IsDBNull(zRdr["APPROVER"])) _APPROVER = zRdr["APPROVER"].ToString();
                        if (!Convert.IsDBNull(zRdr["APPROVEDATE"])) _APPROVEDATE = OracleDB.DBDate(zRdr["APPROVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["APPROVE"])) _APPROVE = zRdr["APPROVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["REQUIREMENT"])) _REQUIREMENT = zRdr["REQUIREMENT"].ToString();
                        if (!Convert.IsDBNull(zRdr["REASON"])) _REASON = zRdr["REASON"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["FROMCOMPANY"])) _FROMCOMPANY = zRdr["FROMCOMPANY"].ToString();
                        if (!Convert.IsDBNull(zRdr["WAREHOUSE"])) _WAREHOUSE = Convert.ToDouble(zRdr["WAREHOUSE"]);
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

    }
}