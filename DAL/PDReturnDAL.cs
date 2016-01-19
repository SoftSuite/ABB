using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;
using ABB.Data.Purchase;

namespace ABB.DAL
{
    public class PDReturnDAL
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

        private string tableName = "PDRETURN";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CREATEBY = "";
        string _TYPE = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        DateTime _PDRETURNDATE = new DateTime(1, 1, 1);
        string _APPROVER = "";
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _REASON = "";
        string _REMARK = "";
        string _STATUS = "";
        string _ACTIVE = "";
        double _REFLOID = 0;
        string _REFTABLE = "";
        string _CNAME = "";
        string _CADDRESS = "";
        string _CTEL = "";
        string _CFAX = "";
        double _SUPPLIER = 0;
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
        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
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
        public DateTime PDRETURNDATE
        {
            get { return _PDRETURNDATE; }
            set { _PDRETURNDATE = value; }
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
        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }
        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }
        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }
        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }
        public string CTEL
        {
            get { return _CTEL; }
            set { _CTEL = value; }
        }
        public string CFAX
        {
            get { return _CFAX; }
            set { _CFAX = value; }
        }
        
        public double SUPPLIER
        {
            get { return _SUPPLIER; }
            set { _SUPPLIER = value; }
        }

        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,CODE,PDRETURNDATE,APPROVER,APPROVEDATE,REASON,REMARK,STATUS,ACTIVE,REFLOID,REFTABLE,CNAME,CADDRESS,CTEL,CFAX,SUPPLIER)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " " + OracleDB.QRDateTime(_PDRETURNDATE) + ",";// PDRETURNDATE";
                sqlz += " '" + OracleDB.QRText(_APPROVER) + "',";// APPROVER";
                sqlz += " " + OracleDB.QRDateTime(_APPROVEDATE) + ",";// APPROVEDATE";
                sqlz += " '" + OracleDB.QRText(_REASON) + "',";// REASON";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += "  " + _REFLOID.ToString() + ",";// REFLOID";
                sqlz += " '" + OracleDB.QRText(_REFTABLE) + "',";// REFTABLE";
                sqlz += " '" + OracleDB.QRText(_CNAME) + "',";// CNAME";
                sqlz += " '" + OracleDB.QRText(_CADDRESS) + "',";// CADDRESS";
                sqlz += " '" + OracleDB.QRText(_CTEL) + "',";// CTEL";
                sqlz += " '" + OracleDB.QRText(_CFAX) + "',";// CFAX";
                sqlz += " '" + _SUPPLIER.ToString() + "'";// SUPPLIER";
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
                sqlz += " PDRETURNDATE  = " + OracleDB.QRDateTime(_PDRETURNDATE) + ", ";
                sqlz += " APPROVER  = '" + OracleDB.QRText(_APPROVER) + "', ";
                sqlz += " APPROVEDATE  = " + OracleDB.QRDateTime(_APPROVEDATE) + ", ";
                sqlz += " REASON  = '" + OracleDB.QRText(_REASON) + "', ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " REFLOID  = " + _REFLOID.ToString() + ", ";
                sqlz += " REFTABLE  = '" + OracleDB.QRText(_REFTABLE) + "', ";
                sqlz += " CNAME  = '" + OracleDB.QRText(_CNAME) + "', ";
                sqlz += " CADDRESS  = '" + OracleDB.QRText(_CADDRESS) + "', ";
                sqlz += " CTEL  = '" + OracleDB.QRText(_CTEL) + "', ";
                sqlz += " CFAX  = '" + OracleDB.QRText(_CFAX) + "', ";
                sqlz += " SUPPLIER  = '" + _SUPPLIER.ToString() + "' ";
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
                        if (!Convert.IsDBNull(zRdr["PDRETURNDATE"])) _PDRETURNDATE = OracleDB.DBDate(zRdr["PDRETURNDATE"]);
                        if (!Convert.IsDBNull(zRdr["APPROVER"])) _APPROVER = zRdr["APPROVER"].ToString();
                        if (!Convert.IsDBNull(zRdr["APPROVEDATE"])) _APPROVEDATE = OracleDB.DBDate(zRdr["APPROVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["REASON"])) _REASON = zRdr["REASON"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFLOID"])) _REFLOID = Convert.ToDouble(zRdr["REFLOID"]);
                        if (!Convert.IsDBNull(zRdr["REFTABLE"])) _REFTABLE = zRdr["REFTABLE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CNAME"])) _CNAME = zRdr["CNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CADDRESS"])) _CADDRESS = zRdr["CADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTEL"])) _CTEL = zRdr["CTEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["CFAX"])) _CFAX = zRdr["CFAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["SUPPLIER"])) _SUPPLIER = Convert.ToDouble(zRdr["SUPPLIER"]);
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
        /// ส่งคืนสินค้า / ส่งคืนวัตถุดิบ
        /// </summary>
        /// <param name="PDReturn">PDReturn.loid</param>
        /// <param name="userID">Login user</param>
        /// <param name="trans">transaction</param>
        /// <returns></returns>
        public bool CutStockPDReturn(double PDReturn, string userID, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CUTSTOCKPDRETURN(" + PDReturn.ToString() + ", '" + userID + "')", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public DataTable GetPDReturnList(ProductReturnData whereData)
        {
            string whereString = "";
            if (whereData.CODEFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CODE >= '" + OracleDB.QRText(whereData.CODEFROM.Trim()) + "' ";
            if (whereData.CODETO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CODE <= '" + OracleDB.QRText(whereData.CODETO.Trim()) + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "PDRETURNDATE >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "PDRETURNDATE <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            if (whereData.PRODUCT != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCT = " + whereData.PRODUCT.ToString() + " ";
            if (whereData.SUPPLIER != 0)
                whereString += (whereString == "" ? "" : "AND ") + "SUPPLIER = " + whereData.SUPPLIER.ToString() + " ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(whereData.STATUSFROM.Trim()) + "' ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(whereData.STATUSTO.Trim()) + "' ";

            string sql = "SELECT ROWNUM NO, B.* FROM (SELECT PT.LOID,PT.CODE,PT.PDRETURNDATE,PTI.LOID PTILOID,PTI.PRODUCT,PD.NAME PRODUCTNAME,PTI.QTY,PTI.UNIT, ";
            sql += "CASE PT.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE PT.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK,U.NAME UNITNAME,PT.SUPPLIER,S.SUPPLIERNAME,ST.CODE STCODE FROM PDRETURN PT ";
            sql += "INNER JOIN (SELECT MIN(LOID) LOID,PDRETURN FROM PDRETURNITEM GROUP BY PDRETURN)A ON PT.LOID = A.PDRETURN ";
            sql += "INNER JOIN PDRETURNITEM PTI ON A.LOID = PTI.LOID ";
            sql += "INNER JOIN STOCKOUT ST ON PT.REFTABLE = 'STOCKOUT' AND PT.REFLOID = ST.LOID INNER JOIN SUPPLIER S ON PT.SUPPLIER = S.LOID ";
            sql += "INNER JOIN PRODUCT PD ON PTI.PRODUCT = PD.LOID INNER JOIN UNIT U ON PTI.UNIT = U.LOID)B ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPDReturnItemList(double pdreturn)
        {
            string sql = "SELECT PTI.LOID,ROWNUM RANK,PTI.PRODUCT,PD.BARCODE,PD.NAME PRODUCTNAME,PTI.LOTNO,PTI.QTY,PTI.UNIT,U.NAME UNITNAME,PTI.PRICE,PTI.PRICE*PTI.QTY NETPRICE ";
            sql += "FROM PDRETURNITEM PTI INNER JOIN PRODUCT PD ON PTI.PRODUCT = PD.LOID ";
            sql += "INNER JOIN UNIT U ON PTI.UNIT = U.LOID ";
            sql += "WHERE PTI.PDRETURN = " + pdreturn;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPDReturnItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 PRODUCT, '' BARCOCE, '' PRODUCTNAME,'' LOTNO, 0 QTY, 0 UNIT, '' UNITNAME ,0 PRICE ,0 NETPRICE";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockoutItemList(double stockout)
        {
            string sql = "SELECT ROWNUM RANK,A.* FROM (SELECT ST.LOID, ST.LOTNO, ST.QTY, PD.BARCODE,PD.LOID PRODUCT, PD.NAME PRODUCTNAME, PD.PRICE, ST.QTY*PD.PRICE AS NETPRICE, U.LOID UNIT, U.NAME UNITNAME, 0 AS DISCOUNT,ST.ACTIVE,PD.ISVAT FROM STOCKOUTITEM ST ";
            sql += " INNER JOIN PRODUCT PD ON ST.PRODUCT = PD.LOID INNER JOIN UNIT U ON PD.UNIT = U.LOID ";
            sql += " WHERE ST.STOCKOUT = " + stockout + " ORDER BY ST.LOID) A";
            return OracleDB.ExecListCmd(sql);
        }

        public string GetSupplierData(double loid)
        {
            string sql = "SELECT SUPPLIERNAME FROM SUPPLIER WHERE LOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            string CNAME = "";
            if (dt.Rows.Count > 0)
            {
                CNAME = dt.Rows[0]["SUPPLIERNAME"].ToString();
            }

            return CNAME;
        }

        public DateTime GetSTDate(double loid)
        {
            string sql = "SELECT CREATEON FROM STOCKOUT WHERE LOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            DateTime DATE = new DateTime(1, 1, 1);
            if (dt.Rows.Count > 0)
            {
                DATE = Convert.ToDateTime(dt.Rows[0]["CREATEON"]);
            }

            return DATE;
        }

        public string GetSTCode(double loid)
        {
            string sql = "SELECT CODE FROM STOCKOUT WHERE LOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            string CODE = "";
            if (dt.Rows.Count > 0)
            {
                CODE = (dt.Rows[0]["CODE"]).ToString();
            }

            return CODE;
        }

    }
}