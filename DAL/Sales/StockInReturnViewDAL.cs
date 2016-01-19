using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class StockInReturnViewDAL
    {
        #region Public Method

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

        #endregion

        #region Constant

        private string tableName
        {
            get
            {
                string sql = "( SELECT S.LOID, S.CODE, S.CREATEBY, S.CREATEON, S.SENDER, S.RECEIVER, S.RECEIVEDATE, S.APPROVER, S.APPROVEDATE, ";
                sql += "S.STATUS, S.REASON, S.REMARK, S.GRANDTOT, S.REFLOID, R.CODE INVOICECODE, R.REQDATE INVOICEDATE, C.CODE CUSTOMERCODE, ";
                sql += "TITLE.NAME || C.NAME || ' ' || C.LASTNAME CUSTOMERNAME ";
                sql += "FROM STOCKIN S LEFT JOIN REQUISITION R ON R.LOID = S.REFLOID AND S.REFTABLE = 'REQUISITION' ";
                sql += "LEFT JOIN CUSTOMER C ON C.LOID = R.CUSTOMER ";
                sql += "LEFT JOIN TITLE ON TITLE.LOID = C.TITLE) V_STOCKINRETURN ";
                return sql;
            }
        }

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CODE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        double _SENDER = 0;
        double _RECEIVER = 0;
        DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        double _APPROVER = 0;
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _STATUS = "";
        string _REASON = "";
        string _REMARK = "";
        double _GRANDTOT = 0;
        double _REFLOID = 0;
        string _INVOICECODE = "";
        DateTime _INVOICEDATE = new DateTime(1, 1, 1);
        string _CUSTOMERCODE = "";
        string _CUSTOMERNAME = "";
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
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public string CREATEBY
        {
            get { return _CREATEBY; }
        }
        public DateTime CREATEON
        {
            get { return _CREATEON; }
        }
        public double SENDER
        {
            get { return _SENDER; }
            set { _SENDER = value; }
        }
        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }
        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }
        public double APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }
        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
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
        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
        }
        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }
        public string INVOICECODE
        {
            get { return _INVOICECODE; }
            set { _INVOICECODE = value; }
        }
        public DateTime INVOICEDATE
        {
            get { return _INVOICEDATE; }
            set { _INVOICEDATE = value; }
        }
        public string CUSTOMERCODE
        {
            get { return _CUSTOMERCODE; }
            set { _CUSTOMERCODE = value; }
        }
        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }
        #endregion

        #region Query String
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
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["SENDER"])) _SENDER = Convert.ToDouble(zRdr["SENDER"]);
                        if (!Convert.IsDBNull(zRdr["RECEIVER"])) _RECEIVER = Convert.ToDouble(zRdr["RECEIVER"]);
                        if (!Convert.IsDBNull(zRdr["RECEIVEDATE"])) _RECEIVEDATE = OracleDB.DBDate(zRdr["RECEIVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["APPROVER"])) _APPROVER = Convert.ToDouble(zRdr["APPROVER"]);
                        if (!Convert.IsDBNull(zRdr["APPROVEDATE"])) _APPROVEDATE = OracleDB.DBDate(zRdr["APPROVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["REASON"])) _REASON = zRdr["REASON"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["GRANDTOT"])) _GRANDTOT = Convert.ToDouble(zRdr["GRANDTOT"]);
                        if (!Convert.IsDBNull(zRdr["REFLOID"])) _REFLOID = Convert.ToDouble(zRdr["REFLOID"]);
                        if (!Convert.IsDBNull(zRdr["INVOICECODE"])) _INVOICECODE = zRdr["INVOICECODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["INVOICEDATE"])) _INVOICEDATE = OracleDB.DBDate(zRdr["INVOICEDATE"]);
                        if (!Convert.IsDBNull(zRdr["CUSTOMERCODE"])) _CUSTOMERCODE = zRdr["CUSTOMERCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CUSTOMERNAME"])) _CUSTOMERNAME = zRdr["CUSTOMERNAME"].ToString();
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