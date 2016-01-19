using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class PDOrderDAL
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

        private string tableName = "PDORDER";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        string _SENDPO = "";
        string _SENDOTHER = "";
        string _REFSUPPCODE = "";
        DateTime _SENDPODATE = new DateTime(1, 1, 1);
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        DateTime _ORDERDATE = new DateTime(1, 1, 1);
        string _ORDERTYPE = "";
        double _SUPPLIER = 0;
        string _CNAME = "";
        string _CADDRESS = "";
        string _CTEL = "";
        string _CFAX = "";
        string _APPROVER = "";
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _REMARK = "";
        string _PAYMENTTYPE = "";
        string _PAYMENTDESC = "";
        double _TOTAL = 0;
        double _TOTVAT = 0;
        double _TOTDIS = 0;
        double _GRANDTOT = 0;
        double _REFLOID = 0;
        string _STATUS = "";
        string _ACTIVE = "";
        string _DELIVERY = "";
        string _OTHER = "";
        DateTime _DUEDATE = new DateTime(1, 1, 1);
        DateTime _ANADATE = new DateTime(1, 1, 1);
        double _VAT = 0;
        string _POTYPE = "";
        string _ANACODE = "";
        string _REFTABLE = "";
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
        public string SENDPO
        {
            get { return _SENDPO; }
            set { _SENDPO = value; }
        }
        public string SENDOTHER
        {
            get { return _SENDOTHER; }
            set { _SENDOTHER = value; }
        }
        public string REFSUPPCODE
        {
            get { return _REFSUPPCODE; }
            set { _REFSUPPCODE = value; }
        }
        public DateTime SENDPODATE
        {
            get { return _SENDPODATE; }
            set { _SENDPODATE = value; }
        }
        public DateTime ANADATE
        {
            get { return _ANADATE; }
            set { _ANADATE = value; }
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
        public string ANACODE
        {
            get { return _ANACODE; }
            set { _ANACODE = value; }
        }
        public DateTime ORDERDATE
        {
            get { return _ORDERDATE; }
            set { _ORDERDATE = value; }
        }
        public string ORDERTYPE
        {
            get { return _ORDERTYPE; }
            set { _ORDERTYPE = value; }
        }
        public double SUPPLIER
        {
            get { return _SUPPLIER; }
            set { _SUPPLIER = value; }
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
        public string PAYMENTTYPE
        {
            get { return _PAYMENTTYPE; }
            set { _PAYMENTTYPE = value; }
        }
        public string PAYMENTDESC
        {
            get { return _PAYMENTDESC; }
            set { _PAYMENTDESC = value; }
        }
        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }
        public double TOTVAT
        {
            get { return _TOTVAT; }
            set { _TOTVAT = value; }
        }
        public double TOTDIS
        {
            get { return _TOTDIS; }
            set { _TOTDIS = value; }
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
        public string DELIVERY
        {
            get { return _DELIVERY; }
            set { _DELIVERY = value; }
        }
        public string OTHER
        {
            get { return _OTHER; }
            set { _OTHER = value; }
        }
        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }
        public double VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }
        public string POTYPE
        {
            get { return _POTYPE; }
            set { _POTYPE = value; }
        }
        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (SENDPO,SENDOTHER,REFSUPPCODE,ANADATE,SENDPODATE,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,ANACODE,CODE,ORDERDATE,ORDERTYPE,SUPPLIER,CNAME,CADDRESS,CTEL,CFAX,APPROVER,APPROVEDATE,REMARK,PAYMENTTYPE,PAYMENTDESC,TOTAL,TOTVAT,TOTDIS,GRANDTOT,REFLOID,STATUS,ACTIVE,DELIVERY,OTHER,DUEDATE,VAT,POTYPE,REFTABLE)VALUES(";
                sqlz += " '" + OracleDB.QRText(_SENDPO) + "',";// SENDPO";
                sqlz += " '" + OracleDB.QRText(_SENDOTHER) + "',";// SENDOTHER";
                sqlz += " '" + OracleDB.QRText(_REFSUPPCODE) + "',";// REFSUPPCODE";
                sqlz += " " + OracleDB.QRDateTime(_SENDPODATE) + ",";// SENDPODATE";
                sqlz += " " + OracleDB.QRDateTime(_ANADATE) + ",";// ANADATE";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_ANACODE) + "',";// ANACODE";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " " + OracleDB.QRDateTime(_ORDERDATE) + ",";// ORDERDATE";
                sqlz += " '" + OracleDB.QRText(_ORDERTYPE) + "',";// ORDERTYPE";
                sqlz += "  " + _SUPPLIER.ToString() + ",";// SUPPLIER";
                sqlz += " '" + OracleDB.QRText(_CNAME) + "',";// CNAME";
                sqlz += " '" + OracleDB.QRText(_CADDRESS) + "',";// CADDRESS";
                sqlz += " '" + OracleDB.QRText(_CTEL) + "',";// CTEL";
                sqlz += " '" + OracleDB.QRText(_CFAX) + "',";// CFAX";
                sqlz += " '" + OracleDB.QRText(_APPROVER) + "',";// APPROVER";
                sqlz += " " + OracleDB.QRDateTime(_APPROVEDATE) + ",";// APPROVEDATE";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += " '" + OracleDB.QRText(_PAYMENTTYPE) + "',";// PAYMENTTYPE";
                sqlz += " '" + OracleDB.QRText(_PAYMENTDESC) + "',";// PAYMENTDESC";
                sqlz += "  " + _TOTAL.ToString() + ",";// TOTAL";
                sqlz += "  " + _TOTVAT.ToString() + ",";// TOTVAT";
                sqlz += "  " + _TOTDIS.ToString() + ",";// TOTDIS";
                sqlz += "  " + _GRANDTOT.ToString() + ",";// GRANDTOT";
                sqlz += "  " + _REFLOID.ToString() + ",";// REFLOID";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_DELIVERY) + "',";// DELIVERY";
                sqlz += " '" + OracleDB.QRText(_OTHER) + "',";// OTHER";
                sqlz += " " + OracleDB.QRDateTime(_DUEDATE) + ",";// DUEDATE";
                sqlz += "  " + _VAT.ToString() + ",";// VAT";
                sqlz += " '" + OracleDB.QRText(_POTYPE) + "',";// POTYPE";
                sqlz += " '" + OracleDB.QRText(_REFTABLE) + "'";// REFTABLE";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " SENDPO  = '" + OracleDB.QRText(_SENDPO) + "', ";
                sqlz += " SENDOTHER  = '" + OracleDB.QRText(_SENDOTHER) + "', ";
                sqlz += " REFSUPPCODE  = '" + OracleDB.QRText(_REFSUPPCODE) + "', ";
                sqlz += " SENDPODATE  = " + OracleDB.QRDateTime(_SENDPODATE) + ", ";
                sqlz += " ANADATE  = " + OracleDB.QRDateTime(_ANADATE) + ", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                sqlz += " ANACODE  = '" + OracleDB.QRText(_ANACODE) + "', ";
                sqlz += " ORDERDATE  = " + OracleDB.QRDateTime(_ORDERDATE) + ", ";
                sqlz += " ORDERTYPE  = '" + OracleDB.QRText(_ORDERTYPE) + "', ";
                sqlz += " SUPPLIER  = " + _SUPPLIER.ToString() + ", ";
                sqlz += " CNAME  = '" + OracleDB.QRText(_CNAME) + "', ";
                sqlz += " CADDRESS  = '" + OracleDB.QRText(_CADDRESS) + "', ";
                sqlz += " CTEL  = '" + OracleDB.QRText(_CTEL) + "', ";
                sqlz += " CFAX  = '" + OracleDB.QRText(_CFAX) + "', ";
                sqlz += " APPROVER  = '" + OracleDB.QRText(_APPROVER) + "', ";
                sqlz += " APPROVEDATE  = " + OracleDB.QRDateTime(_APPROVEDATE) + ", ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " PAYMENTTYPE  = '" + OracleDB.QRText(_PAYMENTTYPE) + "', ";
                sqlz += " PAYMENTDESC  = '" + OracleDB.QRText(_PAYMENTDESC) + "', ";
                sqlz += " TOTAL  = " + _TOTAL.ToString() + ", ";
                sqlz += " TOTVAT  = " + _TOTVAT.ToString() + ", ";
                sqlz += " TOTDIS  = " + _TOTDIS.ToString() + ", ";
                sqlz += " GRANDTOT  = " + _GRANDTOT.ToString() + ", ";
                sqlz += " REFLOID  = " + _REFLOID.ToString() + ", ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " DELIVERY  = '" + OracleDB.QRText(_DELIVERY) + "', ";
                sqlz += " OTHER  = '" + OracleDB.QRText(_OTHER) + "', ";
                sqlz += " DUEDATE  = " + OracleDB.QRDateTime(_DUEDATE) + ", ";
                sqlz += " VAT  = " + _VAT.ToString() + ", ";
                sqlz += " POTYPE  = '" + OracleDB.QRText(_POTYPE) + "', ";
                sqlz += " REFTABLE  = '" + OracleDB.QRText(_REFTABLE) + "' ";
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
                        if (_POTYPE == "B")
                            _CODE = OracleDB.GetRunningCode(_ORDERDATE, "PO", zTrans);
                        else
                            _CODE = OracleDB.GetRunningCode(TableName, _ORDERTYPE, zTrans);
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
                        if (!Convert.IsDBNull(zRdr["SENDPO"])) _SENDPO = zRdr["SENDPO"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDOTHER"])) _SENDOTHER = zRdr["SENDOTHER"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFSUPPCODE"])) _REFSUPPCODE = zRdr["REFSUPPCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDPODATE"])) _SENDPODATE = OracleDB.DBDate(zRdr["SENDPODATE"]);
                        if (!Convert.IsDBNull(zRdr["ANADATE"])) _ANADATE = OracleDB.DBDate(zRdr["ANADATE"]);
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ANACODE"])) _ANACODE = zRdr["ANACODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ORDERDATE"])) _ORDERDATE = OracleDB.DBDate(zRdr["ORDERDATE"]);
                        if (!Convert.IsDBNull(zRdr["ORDERTYPE"])) _ORDERTYPE = zRdr["ORDERTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["SUPPLIER"])) _SUPPLIER = Convert.ToDouble(zRdr["SUPPLIER"]);
                        if (!Convert.IsDBNull(zRdr["CNAME"])) _CNAME = zRdr["CNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CADDRESS"])) _CADDRESS = zRdr["CADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTEL"])) _CTEL = zRdr["CTEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["CFAX"])) _CFAX = zRdr["CFAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["APPROVER"])) _APPROVER = zRdr["APPROVER"].ToString();
                        if (!Convert.IsDBNull(zRdr["APPROVEDATE"])) _APPROVEDATE = OracleDB.DBDate(zRdr["APPROVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["PAYMENTTYPE"])) _PAYMENTTYPE = zRdr["PAYMENTTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["PAYMENTDESC"])) _PAYMENTDESC = zRdr["PAYMENTDESC"].ToString();
                        if (!Convert.IsDBNull(zRdr["TOTAL"])) _TOTAL = Convert.ToDouble(zRdr["TOTAL"]);
                        if (!Convert.IsDBNull(zRdr["TOTVAT"])) _TOTVAT = Convert.ToDouble(zRdr["TOTVAT"]);
                        if (!Convert.IsDBNull(zRdr["TOTDIS"])) _TOTDIS = Convert.ToDouble(zRdr["TOTDIS"]);
                        if (!Convert.IsDBNull(zRdr["GRANDTOT"])) _GRANDTOT = Convert.ToDouble(zRdr["GRANDTOT"]);
                        if (!Convert.IsDBNull(zRdr["REFLOID"])) _REFLOID = Convert.ToDouble(zRdr["REFLOID"]);
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["DELIVERY"])) _DELIVERY = zRdr["DELIVERY"].ToString();
                        if (!Convert.IsDBNull(zRdr["OTHER"])) _OTHER = zRdr["OTHER"].ToString();
                        if (!Convert.IsDBNull(zRdr["DUEDATE"])) _DUEDATE = OracleDB.DBDate(zRdr["DUEDATE"]);
                        if (!Convert.IsDBNull(zRdr["VAT"])) _VAT = Convert.ToDouble(zRdr["VAT"]);
                        if (!Convert.IsDBNull(zRdr["POTYPE"])) _POTYPE = zRdr["POTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFTABLE"])) _REFTABLE = zRdr["REFTABLE"].ToString();
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
        /// บันทึกการผลิตสินค้า หลังเบิกวัตถุดิบแล้ว
        /// </summary>
        /// <param name="pdorder">PDORDER.LOID</param>
        /// <param name="userID"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool CutStockQS(double pdorder, string userID, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CUTSTOCKPDORDER_QS(" + pdorder.ToString() + ", '" + userID + "' )", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// บันทึกการผลิตสินค้า หลังการฉายรังสีแล้ว
        /// </summary>
        /// <param name="pdorder">PDORDER.LOID</param>
        /// <param name="userID"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool CutStockItemQS(double pdorder, string userID, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CUTSTOCKPDPRODUCT_QS(" + pdorder.ToString() + ", '" + userID + "' )", trans);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// บันทึกการผลิตสินค้า หลังกักกันสินค้า หรือ ตรวจสอบสินค้าแล้ว
        /// </summary>
        /// <param name="pdorder">PDORDER.LOID</param>
        /// <param name="userID"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool CutStock(double pdorder, string userID, OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_CUTSTOCKPDORDER(" + pdorder.ToString() + ", '" + userID + "' )", trans);
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