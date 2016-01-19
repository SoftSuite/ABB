using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class RequisitionDAL
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

        private string tableName = "REQUISITION";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        string _DELIVERYTYPE = "";
        string _OTHER = "";
        DateTime _CREDITDATE = new DateTime(1, 1, 1);
        string _BANKBRANCH = "";
        string _PAYMENTCONDITION = "";
        double _REFTYPELOID = 0;
        string _REFTYPETABLE = "";
        double _REFWAREHOUSE = 0;
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        string _REFNO = "";
        DateTime _REQDATE = new DateTime(1, 1, 1);
        string _INVCODE = "";
        DateTime _RESERVEDATE = new DateTime(1, 1, 1);
        double _CUSTOMER = 0;
        double _OFFICER = 0;
        double _WAREHOUSE = 0;
        double _TOTAL = 0;
        double _TOTDIS = 0;
        double _GRANDTOT = 0;
        double _REQUISITIONTYPE = 0;
        double _REFLOID = 0;
        string _REFTABLE = "";
        string _STATUS = "";
        string _ACTIVE = "";
        string _LOTNO = "";
        string _PAYMENT = "";
        double _CASH = 0;
        double _CREDITPAY = 0;
        double _CREDITCARDPAY = 0;
        double _COUPON = 0;
        string _CREDITCARDID = "";
        double _BANK = 0;
        double _CREDITTYPE = 0;
        string _CHEQUE = "";
        DateTime _CHEQUEDATE = new DateTime(1, 1, 1);
        string _REASON = "";
        DateTime _DUEDATE = new DateTime(1, 1, 1);
        string _REMARK = "";
        double _CTITLE = 0;
        string _CNAME = "";
        string _CLASTNAME = "";
        string _CADDRESS = "";
        string _CTEL = "";
        string _CFAX = "";
        string _CEMAIL = "";
        double _VAT = 0;
        double _TOTVAT = 0;
        double _LOID = 0;
        string _CREATEBY = "";
        string _USEMEMBERDISCOUNT = "";
        string _CCODE = "";
        string _BANKNAME = "";
        string _RECEIVEBY = "";
        DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        #endregion

        #region Public Property
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
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
        public string DELIVERYTYPE
        {
            get { return _DELIVERYTYPE; }
            set { _DELIVERYTYPE = value; }
        }
        public string OTHER
        {
            get { return _OTHER; }
            set { _OTHER = value; }
        }
        public DateTime CREDITDATE
        {
            get { return _CREDITDATE; }
            set { _CREDITDATE = value; }
        }
        public string BANKBRANCH
        {
            get { return _BANKBRANCH; }
            set { _BANKBRANCH = value; }
        }
        public string PAYMENTCONDITION
        {
            get { return _PAYMENTCONDITION; }
            set { _PAYMENTCONDITION = value; }
        }
        public double REFTYPELOID
        {
            get { return _REFTYPELOID; }
            set { _REFTYPELOID = value; }
        }
        public string REFTYPETABLE
        {
            get { return _REFTYPETABLE; }
            set { _REFTYPETABLE = value; }
        }
        public double REFWAREHOUSE
        {
            get { return _REFWAREHOUSE; }
            set { _REFWAREHOUSE = value; }
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
        public string REFNO
        {
            get { return _REFNO; }
            set { _REFNO = value; }
        }
        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }
        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }
        public DateTime RESERVEDATE
        {
            get { return _RESERVEDATE; }
            set { _RESERVEDATE = value; }
        }
        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }
        public double OFFICER
        {
            get { return _OFFICER; }
            set { _OFFICER = value; }
        }
        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
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
        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
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
        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }
        public string PAYMENT
        {
            get { return _PAYMENT; }
            set { _PAYMENT = value; }
        }
        public double CASH
        {
            get { return _CASH; }
            set { _CASH = value; }
        }
        public double CREDITPAY
        {
            get { return _CREDITPAY; }
            set { _CREDITPAY = value; }
        }
        public double CREDITCARDPAY
        {
            get { return _CREDITCARDPAY; }
            set { _CREDITCARDPAY = value; }
        }
        public double COUPON
        {
            get { return _COUPON; }
            set { _COUPON = value; }
        }
        public string CREDITCARDID
        {
            get { return _CREDITCARDID; }
            set { _CREDITCARDID = value; }
        }
        public double BANK
        {
            get { return _BANK; }
            set { _BANK = value; }
        }
        public double CREDITTYPE
        {
            get { return _CREDITTYPE; }
            set { _CREDITTYPE = value; }
        }
        public string CHEQUE
        {
            get { return _CHEQUE; }
            set { _CHEQUE = value; }
        }
        public DateTime CHEQUEDATE
        {
            get { return _CHEQUEDATE; }
            set { _CHEQUEDATE = value; }
        }
        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }
        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
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
        public string CEMAIL
        {
            get { return _CEMAIL; }
            set { _CEMAIL = value; }
        }
        public double VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }
        public double TOTVAT
        {
            get { return _TOTVAT; }
            set { _TOTVAT = value; }
        }
        public double LOID
        {
            get { return _LOID; }
        }
        public string CREATEBY
        {
            get { return _CREATEBY; }
        }
        public string USEMEMBERDISCOUNT
        {
            get { return _USEMEMBERDISCOUNT; }
            set { _USEMEMBERDISCOUNT = value; }
        }
        public string CCODE
        {
            get { return _CCODE; }
            set { _CCODE = value; }
        }
        public string BANKNAME
        {
            get { return _BANKNAME; }
            set { _BANKNAME = value; }
        }
        public string RECEIVEBY
        {
            get { return _RECEIVEBY; }
            set { _RECEIVEBY = value; }
        }
        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (DELIVERYTYPE,OTHER,CREDITDATE,BANKBRANCH,PAYMENTCONDITION,REFTYPELOID,REFTYPETABLE,REFWAREHOUSE,CREATEON,UPDATEBY,UPDATEON,CODE,REFNO,REQDATE,INVCODE,RESERVEDATE,CUSTOMER,OFFICER,WAREHOUSE,TOTAL,TOTDIS,GRANDTOT,REQUISITIONTYPE,REFLOID,REFTABLE,STATUS,ACTIVE,LOTNO,PAYMENT,CASH,CREDITPAY,CREDITCARDPAY,COUPON,CREDITCARDID,BANK,CREDITTYPE,CHEQUE,CHEQUEDATE,REASON,DUEDATE,REMARK,CTITLE,CNAME,CLASTNAME,CADDRESS,CTEL,CFAX,CEMAIL,VAT,TOTVAT,LOID,CREATEBY, USEMEMBERDISCOUNT, CCODE, BANKNAME, RECEIVEBY, RECEIVEDATE)VALUES(";
                sqlz += " '" + OracleDB.QRText(_DELIVERYTYPE) + "',";// DELIVERYTYPE";
                sqlz += " '" + OracleDB.QRText(_OTHER) + "',";// OTHER";
                sqlz += " " + OracleDB.QRDateTime(_CREDITDATE) + ",";// CREDITDATE";
                sqlz += " '" + OracleDB.QRText(_BANKBRANCH) + "',";// BANKBRANCH";
                sqlz += " '" + OracleDB.QRText(_PAYMENTCONDITION) + "',";// PAYMENTCONDITION";
                sqlz += "  " + _REFTYPELOID.ToString() + ",";// REFTYPELOID";
                sqlz += " '" + OracleDB.QRText(_REFTYPETABLE) + "',";// REFTYPETABLE";
                sqlz += "  " + _REFWAREHOUSE.ToString() + ",";// REFWAREHOUSE";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " '" + OracleDB.QRText(_REFNO) + "',";// REFNO";
                sqlz += " " + OracleDB.QRDateTime(_REQDATE) + ",";// REQDATE";
                sqlz += " '" + OracleDB.QRText(_INVCODE) + "',";// INVCODE";
                sqlz += " " + OracleDB.QRDateTime(_RESERVEDATE) + ",";// RESERVEDATE";
                sqlz += "  " + _CUSTOMER.ToString() + ",";// CUSTOMER";
                sqlz += "  " + _OFFICER.ToString() + ",";// OFFICER";
                sqlz += "  " + _WAREHOUSE.ToString() + ",";// WAREHOUSE";
                sqlz += "  " + _TOTAL.ToString() + ",";// TOTAL";
                sqlz += "  " + _TOTDIS.ToString() + ",";// TOTDIS";
                sqlz += "  " + _GRANDTOT.ToString() + ",";// GRANDTOT";
                sqlz += "  " + _REQUISITIONTYPE.ToString() + ",";// REQUISITIONTYPE";
                sqlz += "  " + _REFLOID.ToString() + ",";// REFLOID";
                sqlz += " '" + OracleDB.QRText(_REFTABLE) + "',";// REFTABLE";
                sqlz += " '" + OracleDB.QRText(_STATUS) + "',";// STATUS";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_LOTNO) + "',";// LOTNO";
                sqlz += " '" + OracleDB.QRText(_PAYMENT) + "',";// PAYMENT";
                sqlz += "  " + _CASH.ToString() + ",";// CASH";
                sqlz += "  " + _CREDITPAY.ToString() + ",";// CREDITPAY";
                sqlz += "  " + _CREDITCARDPAY.ToString() + ",";// CREDITCARDPAY";
                sqlz += "  " + _COUPON.ToString() + ",";// COUPON";
                sqlz += " '" + OracleDB.QRText(_CREDITCARDID) + "',";// CREDITCARDID";
                sqlz += "  " + _BANK.ToString() + ",";// BANK";
                sqlz += "  " + _CREDITTYPE.ToString() + ",";// CREDITTYPE";
                sqlz += " '" + OracleDB.QRText(_CHEQUE) + "',";// CHEQUE";
                sqlz += " " + OracleDB.QRDateTime(_CHEQUEDATE) + ",";// CHEQUEDATE";
                sqlz += " '" + OracleDB.QRText(_REASON) + "',";// REASON";
                sqlz += " " + OracleDB.QRDateTime(_DUEDATE) + ",";// DUEDATE";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += "  " + _CTITLE.ToString() + ",";// CTITLE";
                sqlz += " '" + OracleDB.QRText(_CNAME) + "',";// CNAME";
                sqlz += " '" + OracleDB.QRText(_CLASTNAME) + "',";// CLASTNAME";
                sqlz += " '" + OracleDB.QRText(_CADDRESS) + "',";// CADDRESS";
                sqlz += " '" + OracleDB.QRText(_CTEL) + "',";// CTEL";
                sqlz += " '" + OracleDB.QRText(_CFAX) + "',";// CFAX";
                sqlz += " '" + OracleDB.QRText(_CEMAIL) + "',";// CEMAIL";
                sqlz += "  " + _VAT.ToString() + ",";// VAT";
                sqlz += "  " + _TOTVAT.ToString() + ",";// TOTVAT";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "', ";// CREATEBY";
                sqlz += " '" + OracleDB.QRText(_USEMEMBERDISCOUNT) + "', ";// USEMEMBERDISCOUNT";
                sqlz += " '" + OracleDB.QRText(_CCODE) + "', ";// CCODE";
                sqlz += " '" + OracleDB.QRText(_BANKNAME) + "', ";// BANKNAME";
                sqlz += " '" + OracleDB.QRText(_RECEIVEBY) + "', ";// RECEIVEBY";
                sqlz += " " + OracleDB.QRDateTime(_RECEIVEDATE) + " ";// RECEIVEDATE";
                sqlz += " ) ";
                return sqlz;
            }
        }

        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " DELIVERYTYPE  = '" + OracleDB.QRText(_DELIVERYTYPE) + "', ";
                sqlz += " OTHER  = '" + OracleDB.QRText(_OTHER) + "', ";
                sqlz += " CREDITDATE  = " + OracleDB.QRDateTime(_CREDITDATE) + ", ";
                sqlz += " BANKBRANCH  = '" + OracleDB.QRText(_BANKBRANCH) + "', ";
                sqlz += " PAYMENTCONDITION  = '" + OracleDB.QRText(_PAYMENTCONDITION) + "', ";
                sqlz += " REFTYPELOID  = " + _REFTYPELOID.ToString() + ", ";
                sqlz += " REFTYPETABLE  = '" + OracleDB.QRText(_REFTYPETABLE) + "', ";
                sqlz += " REFWAREHOUSE  = " + _REFWAREHOUSE.ToString() + ", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                sqlz += " REFNO  = '" + OracleDB.QRText(_REFNO) + "', ";
                sqlz += " REQDATE  = " + OracleDB.QRDateTime(_REQDATE) + ", ";
                sqlz += " INVCODE  = '" + OracleDB.QRText(_INVCODE) + "', ";
                sqlz += " RESERVEDATE  = " + OracleDB.QRDateTime(_RESERVEDATE) + ", ";
                sqlz += " CUSTOMER  = " + _CUSTOMER.ToString() + ", ";
                sqlz += " OFFICER  = " + _OFFICER.ToString() + ", ";
                sqlz += " WAREHOUSE  = " + _WAREHOUSE.ToString() + ", ";
                sqlz += " TOTAL  = " + _TOTAL.ToString() + ", ";
                sqlz += " TOTDIS  = " + _TOTDIS.ToString() + ", ";
                sqlz += " GRANDTOT  = " + _GRANDTOT.ToString() + ", ";
                sqlz += " REQUISITIONTYPE  = " + _REQUISITIONTYPE.ToString() + ", ";
                sqlz += " REFLOID  = " + _REFLOID.ToString() + ", ";
                sqlz += " REFTABLE  = '" + OracleDB.QRText(_REFTABLE) + "', ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS) + "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " LOTNO  = '" + OracleDB.QRText(_LOTNO) + "', ";
                sqlz += " PAYMENT  = '" + OracleDB.QRText(_PAYMENT) + "', ";
                sqlz += " CASH  = " + _CASH.ToString() + ", ";
                sqlz += " CREDITPAY  = " + _CREDITPAY.ToString() + ", ";
                sqlz += " CREDITCARDPAY  = " + _CREDITCARDPAY.ToString() + ", ";
                sqlz += " COUPON  = " + _COUPON.ToString() + ", ";
                sqlz += " CREDITCARDID  = '" + OracleDB.QRText(_CREDITCARDID) + "', ";
                sqlz += " BANK  = " + _BANK.ToString() + ", ";
                sqlz += " CREDITTYPE  = " + _CREDITTYPE.ToString() + ", ";
                sqlz += " CHEQUE  = '" + OracleDB.QRText(_CHEQUE) + "', ";
                sqlz += " CHEQUEDATE  = " + OracleDB.QRDateTime(_CHEQUEDATE) + ", ";
                sqlz += " REASON  = '" + OracleDB.QRText(_REASON) + "', ";
                sqlz += " DUEDATE  = " + OracleDB.QRDateTime(_DUEDATE) + ", ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " CTITLE  = " + _CTITLE.ToString() + ", ";
                sqlz += " CNAME  = '" + OracleDB.QRText(_CNAME) + "', ";
                sqlz += " CLASTNAME  = '" + OracleDB.QRText(_CLASTNAME) + "', ";
                sqlz += " CADDRESS  = '" + OracleDB.QRText(_CADDRESS) + "', ";
                sqlz += " CTEL  = '" + OracleDB.QRText(_CTEL) + "', ";
                sqlz += " CFAX  = '" + OracleDB.QRText(_CFAX) + "', ";
                sqlz += " CEMAIL  = '" + OracleDB.QRText(_CEMAIL) + "', ";
                sqlz += " VAT  = " + _VAT.ToString() + ", ";
                sqlz += " TOTVAT  = " + _TOTVAT.ToString() + ", ";
                sqlz += " USEMEMBERDISCOUNT  = '" + OracleDB.QRText(_USEMEMBERDISCOUNT) + "', ";
                sqlz += " CCODE  = '" + OracleDB.QRText(_CCODE) + "', ";
                sqlz += " BANKNAME  = '" + OracleDB.QRText(_BANKNAME) + "', ";
                sqlz += " RECEIVEBY  = '" + OracleDB.QRText(_RECEIVEBY) + "', ";
                sqlz += " RECEIVEDATE  = " + OracleDB.QRDateTime(_RECEIVEDATE) + " ";
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
                    if (_CODE == "" && _REQUISITIONTYPE != 0) _CODE = OracleDB.GetRunningCode(TableName, _REQUISITIONTYPE.ToString(), zTrans);
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
                        if (!Convert.IsDBNull(zRdr["DELIVERYTYPE"])) _DELIVERYTYPE = zRdr["DELIVERYTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["OTHER"])) _OTHER = zRdr["OTHER"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREDITDATE"])) _CREDITDATE = OracleDB.DBDate(zRdr["CREDITDATE"]);
                        if (!Convert.IsDBNull(zRdr["BANKBRANCH"])) _BANKBRANCH = zRdr["BANKBRANCH"].ToString();
                        if (!Convert.IsDBNull(zRdr["PAYMENTCONDITION"])) _PAYMENTCONDITION = zRdr["PAYMENTCONDITION"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFTYPELOID"])) _REFTYPELOID = Convert.ToDouble(zRdr["REFTYPELOID"]);
                        if (!Convert.IsDBNull(zRdr["REFTYPETABLE"])) _REFTYPETABLE = zRdr["REFTYPETABLE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFWAREHOUSE"])) _REFWAREHOUSE = Convert.ToDouble(zRdr["REFWAREHOUSE"]);
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFNO"])) _REFNO = zRdr["REFNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["REQDATE"])) _REQDATE = OracleDB.DBDate(zRdr["REQDATE"]);
                        if (!Convert.IsDBNull(zRdr["INVCODE"])) _INVCODE = zRdr["INVCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["RESERVEDATE"])) _RESERVEDATE = OracleDB.DBDate(zRdr["RESERVEDATE"]);
                        if (!Convert.IsDBNull(zRdr["CUSTOMER"])) _CUSTOMER = Convert.ToDouble(zRdr["CUSTOMER"]);
                        if (!Convert.IsDBNull(zRdr["OFFICER"])) _OFFICER = Convert.ToDouble(zRdr["OFFICER"]);
                        if (!Convert.IsDBNull(zRdr["WAREHOUSE"])) _WAREHOUSE = Convert.ToDouble(zRdr["WAREHOUSE"]);
                        if (!Convert.IsDBNull(zRdr["TOTAL"])) _TOTAL = Convert.ToDouble(zRdr["TOTAL"]);
                        if (!Convert.IsDBNull(zRdr["TOTDIS"])) _TOTDIS = Convert.ToDouble(zRdr["TOTDIS"]);
                        if (!Convert.IsDBNull(zRdr["GRANDTOT"])) _GRANDTOT = Convert.ToDouble(zRdr["GRANDTOT"]);
                        if (!Convert.IsDBNull(zRdr["REQUISITIONTYPE"])) _REQUISITIONTYPE = Convert.ToDouble(zRdr["REQUISITIONTYPE"]);
                        if (!Convert.IsDBNull(zRdr["REFLOID"])) _REFLOID = Convert.ToDouble(zRdr["REFLOID"]);
                        if (!Convert.IsDBNull(zRdr["REFTABLE"])) _REFTABLE = zRdr["REFTABLE"].ToString();
                        if (!Convert.IsDBNull(zRdr["STATUS"])) _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOTNO"])) _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["PAYMENT"])) _PAYMENT = zRdr["PAYMENT"].ToString();
                        if (!Convert.IsDBNull(zRdr["CASH"])) _CASH = Convert.ToDouble(zRdr["CASH"]);
                        if (!Convert.IsDBNull(zRdr["CREDITPAY"])) _CREDITPAY = Convert.ToDouble(zRdr["CREDITPAY"]);
                        if (!Convert.IsDBNull(zRdr["CREDITCARDPAY"])) _CREDITCARDPAY = Convert.ToDouble(zRdr["CREDITCARDPAY"]);
                        if (!Convert.IsDBNull(zRdr["COUPON"])) _COUPON = Convert.ToDouble(zRdr["COUPON"]);
                        if (!Convert.IsDBNull(zRdr["CREDITCARDID"])) _CREDITCARDID = zRdr["CREDITCARDID"].ToString();
                        if (!Convert.IsDBNull(zRdr["BANK"])) _BANK = Convert.ToDouble(zRdr["BANK"]);
                        if (!Convert.IsDBNull(zRdr["CREDITTYPE"])) _CREDITTYPE = Convert.ToDouble(zRdr["CREDITTYPE"]);
                        if (!Convert.IsDBNull(zRdr["CHEQUE"])) _CHEQUE = zRdr["CHEQUE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CHEQUEDATE"])) _CHEQUEDATE = OracleDB.DBDate(zRdr["CHEQUEDATE"]);
                        if (!Convert.IsDBNull(zRdr["REASON"])) _REASON = zRdr["REASON"].ToString();
                        if (!Convert.IsDBNull(zRdr["DUEDATE"])) _DUEDATE = OracleDB.DBDate(zRdr["DUEDATE"]);
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTITLE"])) _CTITLE = Convert.ToDouble(zRdr["CTITLE"]);
                        if (!Convert.IsDBNull(zRdr["CNAME"])) _CNAME = zRdr["CNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CLASTNAME"])) _CLASTNAME = zRdr["CLASTNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CADDRESS"])) _CADDRESS = zRdr["CADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTEL"])) _CTEL = zRdr["CTEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["CFAX"])) _CFAX = zRdr["CFAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["CEMAIL"])) _CEMAIL = zRdr["CEMAIL"].ToString();
                        if (!Convert.IsDBNull(zRdr["VAT"])) _VAT = Convert.ToDouble(zRdr["VAT"]);
                        if (!Convert.IsDBNull(zRdr["TOTVAT"])) _TOTVAT = Convert.ToDouble(zRdr["TOTVAT"]);
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["USEMEMBERDISCOUNT"])) _USEMEMBERDISCOUNT = zRdr["USEMEMBERDISCOUNT"].ToString();
                        if (!Convert.IsDBNull(zRdr["CCODE"])) _CCODE = zRdr["CCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["BANKNAME"])) _BANKNAME = zRdr["BANKNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["RECEIVEBY"])) _RECEIVEBY = zRdr["RECEIVEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["RECEIVEDATE"])) _RECEIVEDATE = OracleDB.DBDate(zRdr["RECEIVEDATE"]);
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

        public bool GetDataByINV(string code, OracleTransaction zTrans)
        {
            return doGetdata(" INVCODE = '" + code.ToString() + "' ", zTrans);
        }

        public bool GetDataByLOTNO(string code, OracleTransaction zTrans)
        {
            return doGetdata(" RQ.LOTNO = '" + code.ToString() + "'", zTrans);
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
                OracleDB.ExecNonQueryCmd("CALL SP_CUTSTOCKRQ(" + requisition.ToString() + ", '" + userID + "' )", trans);
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