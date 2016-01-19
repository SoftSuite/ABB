using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;
using ABB.Data.Purchase;

namespace ABB.DAL
{
    public class POEditDAL
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

        private string tableName = "POEDIT";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;

        double _LOID = 0;
        string _TYPE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        DateTime _POEDITDATE = new DateTime(1, 1, 1);
        string _APPROVER = "";
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _REMARK = "";
        string _REASON = "";
        string _STATUS = "";
        string _ACTIVE = "";
        double _POOLD = 0;
        double _PONEW = 0;
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
        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public DateTime POEDITDATE
        {
            get { return _POEDITDATE; }
            set { _POEDITDATE = value; }
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
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public double POOLD
        {
            get { return _POOLD; }
            set { _POOLD = value; }
        }
        public double PONEW
        {
            get { return _PONEW; }
            set { _PONEW = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,CODE,POEDITDATE,APPROVER,APPROVEDATE,REMARK,REASON,STATUS,ACTIVE,POOLD,PONEW)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " " + OracleDB.QRDateTime(_POEDITDATE) + ",";// POEDITDATE";
                sqlz += " '" + OracleDB.QRText(_APPROVER) + "',";// APPROVER";
                sqlz += " " + OracleDB.QRDateTime(_APPROVEDATE) + ",";// APPROVEDATE";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += " '" + OracleDB.QRText(_REASON) + "',";// REASON";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + _POOLD.ToString() + "',";// POOLD";
                sqlz += " '" + _PONEW.ToString() + "'";// PONEW";
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
                sqlz += " POEDITDATE  = " + OracleDB.QRDateTime(_POEDITDATE) + ", ";
                sqlz += " APPROVER  = '" + OracleDB.QRText(_APPROVER) + "', ";
                sqlz += " APPROVEDATE  = " + OracleDB.QRDateTime(_APPROVEDATE) + ", ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " REASON  = '" + OracleDB.QRText(_REASON) + "', ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " POOLD  = '" + _POOLD.ToString() + "', ";
                sqlz += " PONEW  = '" + _PONEW.ToString() + "' ";
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
                    if (_CODE == "") _CODE = OracleDB.GetRunningCode(TableName, TableName, zTrans);
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
                        if (!Convert.IsDBNull(zRdr["POEDITDATE"])) _POEDITDATE = OracleDB.DBDate(zRdr["POEDITDATE"]);
                        if (!Convert.IsDBNull(zRdr["APPROVER"])) _APPROVER = zRdr["APPROVER"].ToString();
                        if (!Convert.IsDBNull(zRdr["APPROVEDATE"])) _APPROVEDATE = OracleDB.DBDate(zRdr["APPROVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["REASON"])) _REASON = zRdr["REASON"].ToString();
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["POOLD"])) _POOLD = Convert.ToDouble(zRdr["POOLD"]);
                        if (!Convert.IsDBNull(zRdr["PONEW"])) _PONEW = Convert.ToDouble(zRdr["PONEW"]);
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

        //update สถานะใบ PO เก่าเป็นยกเลิก//
        public bool UpdateStatusPOOld(double loid, string status, string userID, OracleTransaction zTrans)
        {
            string sql = "UPDATE PDORDER SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE LOID = " + loid + " ";
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
        //update Ative PO เก่าให้เป็น 0 //
        public bool UpdatePOOldActive(double loid, string active, string userID, OracleTransaction zTrans)
        {
            string sql = "UPDATE PDORDER SET ACTIVE = '" + active + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE LOID = " + loid + " ";
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

        /// แก้ไข stockin ให้อ้างอิง PO ใหม่ ///
        public bool UpdateStockIn(double oldloid, double newloid, string userID, OracleTransaction zTrans)
        {
            string sql = "UPDATE STOCKIN SET REFLOID = '" + newloid + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE REFTABLE = 'PDORDER' AND REFLOID = " + oldloid + " ";
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

        public bool UpdateStockInItem(double newloid, string userID, OracleTransaction zTrans)
        {
            string sql_s = "SELECT LOID, REFPOITEM FROM POITEM WHERE PDORDER = '" + newloid + "' ";

            bool ret = true;

            DataTable dt = OracleDB.ExecListCmd(sql_s);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    string sql_u = "UPDATE STOCKINITEM SET REFLOID = '" + dt.Rows[i]["LOID"].ToString() + "', "; //LOID จาก sql_select
                    sql_u += "UPDATEBY = '" + userID + "', ";
                    sql_u += "UPDATEON = " + OracleDB.QRDateTime() + " ";
                    sql_u += "WHERE REFTABLE = 'POITEM' AND REFLOID = " + dt.Rows[i]["REFPOITEM"].ToString() + " "; //REFPOITEM จาก sql_select
                    ret = (OracleDB.ExecNonQueryCmd(sql_u, zTrans) > 0);
                    if (!ret) throw new ApplicationException(OracleDB.Err_NoUpdate);
                }
                catch (Exception ex)
                {
                    ret = false;
                    _error = ex.Message;
                }
            }

            return ret;
        }

        public DataTable GetPOEditList(POEditData data)
        {
            string whereString = "";

            if (data.PECODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(PECODE) = '" + OracleDB.QRText(data.PECODE.Trim()).ToUpper() + "' ";
            if (data.POCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(POCODE) = '" + OracleDB.QRText(data.POCODE.Trim()).ToUpper() + "' ";
            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "POEDITDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "POEDITDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
            if (data.PODATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "ORDERDATE >= " + OracleDB.QRDate(data.PODATEFROM) + " ";
            if (data.PODATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "ORDERDATE <= " + OracleDB.QRDate(data.PODATETO) + " ";
            if (data.SUPPLIER != "0")
                whereString += (whereString == "" ? "" : "AND ") + "SUPPLIER = " + data.SUPPLIER.ToString() + " ";
            if (data.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(data.STATUSFROM.Trim()) + "' ";
            if (data.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(data.STATUSTO.Trim()) + "' ";

            string sql = "SELECT * FROM (SELECT  ROWNUM NO, PE.LOID PELOID, PE.CODE PECODE, PE.POEDITDATE, PE.REASON, PO.LOID POLOID, PO.CODE POCODE, PO.ORDERDATE, ";
            sql += "CASE PE.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE PE.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, PO.SUPPLIER,S.SUPPLIERNAME, PE.PONEW, PE.POOLD ";
            sql += "FROM POEDIT PE INNER JOIN PDORDER PO ON PE.POOLD = PO.LOID ";
            sql += "INNER JOIN SUPPLIER S ON PO.SUPPLIER = S.LOID) ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY PECODE DESC";

            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }


    }
}
