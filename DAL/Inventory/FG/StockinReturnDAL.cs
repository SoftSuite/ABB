using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL.Inventory.FG
{
    public class StockinReturnDAL
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

        private string tableName = "STOCKIN";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        string _REMARK = "";
        string _ANACODE = "";
        double _GRANDTOT = 0;
        string _CADDRESS = "";
        DateTime _ANADATE = new DateTime(1, 1, 1);
        double _REFLOID = 0;
        string _REFTABLE = "";
        string _QCRESULT = "";
        double _CTITLE = 0;
        string _CNAME = "";
        string _CLASTNAME = "";
        string _CTEL = "";
        string _CFAX = "";
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        string _QCCODE = "";
        string _ACCCODE = "";
        double _DOCTYPE = 0;
        double _SENDER = 0;
        double _RECEIVER = 0;
        DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        double _APPROVER = 0;
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _INVNO = "";
        string _STATUS = "";
        DateTime _QCDATE = new DateTime(1, 1, 1);
        string _REASON = "";
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
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public string ANACODE
        {
            get { return _ANACODE; }
            set { _ANACODE = value; }
        }
        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
        }
        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }
        public DateTime ANADATE
        {
            get { return _ANADATE; }
            set { _ANADATE = value; }
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
        public string QCRESULT
        {
            get { return _QCRESULT; }
            set { _QCRESULT = value; }
        }
        public double CTITLE
        {
            get { return _CTITLE; }
            set { _CTITLE = value; }
        }
        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }
        public string CLASTNAME
        {
            get { return _CLASTNAME; }
            set { _CLASTNAME = value; }
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
        public string QCCODE
        {
            get { return _QCCODE; }
            set { _QCCODE = value; }
        }
        public string ACCCODE
        {
            get { return _ACCCODE; }
            set { _ACCCODE = value; }
        }
        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
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
        public string INVNO
        {
            get { return _INVNO; }
            set { _INVNO = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public DateTime QCDATE
        {
            get { return _QCDATE; }
            set { _QCDATE = value; }
        }
        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (REMARK,ANACODE,GRANDTOT,CADDRESS,ANADATE,REFLOID,REFTABLE,QCRESULT,CTITLE,CNAME,CLASTNAME,CTEL,CFAX,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,CODE,QCCODE,ACCCODE,DOCTYPE,SENDER,RECEIVER,RECEIVEDATE,APPROVER,APPROVEDATE,INVNO,STATUS,QCDATE,REASON)VALUES(";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += " '" + OracleDB.QRText(_ANACODE) + "',";// ANACODE";
                sqlz += "  " + _GRANDTOT.ToString() + ",";// GRANDTOT";
                sqlz += " '" + OracleDB.QRText(_CADDRESS) + "',";// CADDRESS";
                sqlz += " " + OracleDB.QRDateTime(_ANADATE) + ",";// ANADATE";
                sqlz += "  " + _REFLOID.ToString() + ",";// REFLOID";
                sqlz += " '" + OracleDB.QRText(_REFTABLE) + "',";// REFTABLE";
                sqlz += " '" + OracleDB.QRText(_QCRESULT) + "',";// QCRESULT";
                sqlz += "  " + _CTITLE.ToString() + ",";// CTITLE";
                sqlz += " '" + OracleDB.QRText(_CNAME) + "',";// CNAME";
                sqlz += " '" + OracleDB.QRText(_CLASTNAME) + "',";// CLASTNAME";
                sqlz += " '" + OracleDB.QRText(_CTEL) + "',";// CTEL";
                sqlz += " '" + OracleDB.QRText(_CFAX) + "',";// CFAX";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " '" + OracleDB.QRText(_QCCODE) + "',";// QCCODE";
                sqlz += " '" + OracleDB.QRText(_ACCCODE) + "',";// ACCCODE";
                sqlz += "  " + _DOCTYPE.ToString() + ",";// DOCTYPE";
                sqlz += "  " + _SENDER.ToString() + ",";// SENDER";
                sqlz += "  " + _RECEIVER.ToString() + ",";// RECEIVER";
                sqlz += " " + OracleDB.QRDateTime(_RECEIVEDATE) + ",";// RECEIVEDATE";
                sqlz += "  " + _APPROVER.ToString() + ",";// APPROVER";
                sqlz += " " + OracleDB.QRDateTime(_APPROVEDATE) + ",";// APPROVEDATE";
                sqlz += " '" + OracleDB.QRText(_INVNO) + "',";// INVNO";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " " + OracleDB.QRDateTime(_QCDATE) + ",";// QCDATE";
                sqlz += " '" + OracleDB.QRText(_REASON) + "'";// REASON";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " ANACODE  = '" + OracleDB.QRText(_ANACODE) + "', ";
                sqlz += " GRANDTOT  = " + _GRANDTOT.ToString() + ", ";
                sqlz += " CADDRESS  = '" + OracleDB.QRText(_CADDRESS) + "', ";
                sqlz += " ANADATE  = " + OracleDB.QRDateTime(_ANADATE) + ", ";
                sqlz += " REFLOID  = " + _REFLOID.ToString() + ", ";
                sqlz += " REFTABLE  = '" + OracleDB.QRText(_REFTABLE) + "', ";
                sqlz += " QCRESULT  = '" + OracleDB.QRText(_QCRESULT) + "', ";
                sqlz += " CTITLE  = " + _CTITLE.ToString() + ", ";
                sqlz += " CNAME  = '" + OracleDB.QRText(_CNAME) + "', ";
                sqlz += " CLASTNAME  = '" + OracleDB.QRText(_CLASTNAME) + "', ";
                sqlz += " CTEL  = '" + OracleDB.QRText(_CTEL) + "', ";
                sqlz += " CFAX  = '" + OracleDB.QRText(_CFAX) + "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                sqlz += " QCCODE  = '" + OracleDB.QRText(_QCCODE) + "', ";
                sqlz += " ACCCODE  = '" + OracleDB.QRText(_ACCCODE) + "', ";
                sqlz += " DOCTYPE  = " + _DOCTYPE.ToString() + ", ";
                sqlz += " SENDER  = " + _SENDER.ToString() + ", ";
                sqlz += " RECEIVER  = " + _RECEIVER.ToString() + ", ";
                sqlz += " RECEIVEDATE  = " + OracleDB.QRDateTime(_RECEIVEDATE) + ", ";
                sqlz += " APPROVER  = " + _APPROVER.ToString() + ", ";
                sqlz += " APPROVEDATE  = " + OracleDB.QRDateTime(_APPROVEDATE) + ", ";
                sqlz += " INVNO  = '" + OracleDB.QRText(_INVNO) + "', ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " QCDATE  = " + OracleDB.QRDateTime(_QCDATE) + ", ";
                sqlz += " REASON  = '" + OracleDB.QRText(_REASON) + "' ";
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
                    if (_CODE == "") _CODE = OracleDB.GetRunningCode(tableName, _DOCTYPE.ToString(), zTrans);
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
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["ANACODE"])) _ANACODE = zRdr["ANACODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["GRANDTOT"])) _GRANDTOT = Convert.ToDouble(zRdr["GRANDTOT"]);
                        if (!Convert.IsDBNull(zRdr["CADDRESS"])) _CADDRESS = zRdr["CADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["ANADATE"])) _ANADATE = OracleDB.DBDate(zRdr["ANADATE"]);
                        if (!Convert.IsDBNull(zRdr["REFLOID"])) _REFLOID = Convert.ToDouble(zRdr["REFLOID"]);
                        if (!Convert.IsDBNull(zRdr["REFTABLE"])) _REFTABLE = zRdr["REFTABLE"].ToString();
                        if (!Convert.IsDBNull(zRdr["QCRESULT"])) _QCRESULT = zRdr["QCRESULT"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTITLE"])) _CTITLE = Convert.ToDouble(zRdr["CTITLE"]);
                        if (!Convert.IsDBNull(zRdr["CNAME"])) _CNAME = zRdr["CNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CLASTNAME"])) _CLASTNAME = zRdr["CLASTNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTEL"])) _CTEL = zRdr["CTEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["CFAX"])) _CFAX = zRdr["CFAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["QCCODE"])) _QCCODE = zRdr["QCCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACCCODE"])) _ACCCODE = zRdr["ACCCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["DOCTYPE"])) _DOCTYPE = Convert.ToDouble(zRdr["DOCTYPE"]);
                        if (!Convert.IsDBNull(zRdr["SENDER"])) _SENDER = Convert.ToDouble(zRdr["SENDER"]);
                        if (!Convert.IsDBNull(zRdr["RECEIVER"])) _RECEIVER = Convert.ToDouble(zRdr["RECEIVER"]);
                        if (!Convert.IsDBNull(zRdr["RECEIVEDATE"])) _RECEIVEDATE = OracleDB.DBDate(zRdr["RECEIVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["APPROVER"])) _APPROVER = Convert.ToDouble(zRdr["APPROVER"]);
                        if (!Convert.IsDBNull(zRdr["APPROVEDATE"])) _APPROVEDATE = OracleDB.DBDate(zRdr["APPROVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["INVNO"])) _INVNO = zRdr["INVNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["QCDATE"])) _QCDATE = OracleDB.DBDate(zRdr["QCDATE"]);
                        if (!Convert.IsDBNull(zRdr["REASON"])) _REASON = zRdr["REASON"].ToString();
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