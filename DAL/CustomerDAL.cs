using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class CustomerDAL
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

        private string tableName = "CUSTOMER";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _SENDTEL = "";
        double _LOID = 0;
        string _CODE = "";
        double _TITLE = 0;
        string _NAME = "";
        string _LASTNAME = "";
        double _MEMBERTYPE = 0;
        string _CUSTOMERTYPE = "";
        string _IDENTITY = "";
        string _PAYMENT = "";
        double _CREDITDAY = 0;
        double _CREDITAMOUNT = 0;
        string _BILLADDRESS = "";
        string _BILLROAD = "";
        double _BILLTAMBOL = 0;
        double _BILLAMPHUR = 0;
        double _BILLPROVINCE = 0;
        string _BILLZIPCODE = "";
        string _BILLTEL = "";
        string _BILLFAX = "";
        string _BILLEMAIL = "";
        double _CTITLE = 0;
        string _CNAME = "";
        string _CLASTNAME = "";
        string _CADDRESS = "";
        string _CROAD = "";
        double _CTAMBOL = 0;
        double _CAMPHUR = 0;
        double _CPROVINCE = 0;
        string _CZIPCODE = "";
        string _CTEL = "";
        string _CFAX = "";
        string _CEMAIL = "";
        string _CMOBILE = "";
        string _SENDPLACE = "";
        string _SENDADDRESS = "";
        string _SENDROAD = "";
        double _SENDTAMBOL = 0;
        double _SENDAMPHUR = 0;
        double _SENDPROVINCE = 0;
        string _SENDZIPCODE = "";
        string _SENDFAX = "";
        string _SENDEMAIL = "";
        string _DELIVERTYPE = "";
        string _ACTIVE = "";
        DateTime _EFDATE = new DateTime(1, 1, 1);
        DateTime _EPDATE = new DateTime(1, 1, 1);
        string _REMARK = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
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
        public string UPDATEBY
        {
            get { return _UPDATEBY; }
        }
        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
        }
        public string SENDTEL
        {
            get { return _SENDTEL; }
            set { _SENDTEL = value; }
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
        public double TITLE
        {
            get { return _TITLE; }
            set { _TITLE = value; }
        }
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public string LASTNAME
        {
            get { return _LASTNAME; }
            set { _LASTNAME = value; }
        }
        public double MEMBERTYPE
        {
            get { return _MEMBERTYPE; }
            set { _MEMBERTYPE = value; }
        }
        public string CUSTOMERTYPE
        {
            get { return _CUSTOMERTYPE; }
            set { _CUSTOMERTYPE = value; }
        }
        public string IDENTITY
        {
            get { return _IDENTITY; }
            set { _IDENTITY = value; }
        }
        public string PAYMENT
        {
            get { return _PAYMENT; }
            set { _PAYMENT = value; }
        }
        public double CREDITDAY
        {
            get { return _CREDITDAY; }
            set { _CREDITDAY = value; }
        }
        public double CREDITAMOUNT
        {
            get { return _CREDITAMOUNT; }
            set { _CREDITAMOUNT = value; }
        }
        public string BILLADDRESS
        {
            get { return _BILLADDRESS; }
            set { _BILLADDRESS = value; }
        }
        public string BILLROAD
        {
            get { return _BILLROAD; }
            set { _BILLROAD = value; }
        }
        public double BILLTAMBOL
        {
            get { return _BILLTAMBOL; }
            set { _BILLTAMBOL = value; }
        }
        public double BILLAMPHUR
        {
            get { return _BILLAMPHUR; }
            set { _BILLAMPHUR = value; }
        }
        public double BILLPROVINCE
        {
            get { return _BILLPROVINCE; }
            set { _BILLPROVINCE = value; }
        }
        public string BILLZIPCODE
        {
            get { return _BILLZIPCODE; }
            set { _BILLZIPCODE = value; }
        }
        public string BILLTEL
        {
            get { return _BILLTEL; }
            set { _BILLTEL = value; }
        }
        public string BILLFAX
        {
            get { return _BILLFAX; }
            set { _BILLFAX = value; }
        }
        public string BILLEMAIL
        {
            get { return _BILLEMAIL; }
            set { _BILLEMAIL = value; }
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
        public string CROAD
        {
            get { return _CROAD; }
            set { _CROAD = value; }
        }
        public double CTAMBOL
        {
            get { return _CTAMBOL; }
            set { _CTAMBOL = value; }
        }
        public double CAMPHUR
        {
            get { return _CAMPHUR; }
            set { _CAMPHUR = value; }
        }
        public double CPROVINCE
        {
            get { return _CPROVINCE; }
            set { _CPROVINCE = value; }
        }
        public string CZIPCODE
        {
            get { return _CZIPCODE; }
            set { _CZIPCODE = value; }
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
        public string CMOBILE
        {
            get { return _CMOBILE; }
            set { _CMOBILE = value; }
        }
        public string SENDPLACE
        {
            get { return _SENDPLACE; }
            set { _SENDPLACE = value; }
        }
        public string SENDADDRESS
        {
            get { return _SENDADDRESS; }
            set { _SENDADDRESS = value; }
        }
        public string SENDROAD
        {
            get { return _SENDROAD; }
            set { _SENDROAD = value; }
        }
        public double SENDTAMBOL
        {
            get { return _SENDTAMBOL; }
            set { _SENDTAMBOL = value; }
        }
        public double SENDAMPHUR
        {
            get { return _SENDAMPHUR; }
            set { _SENDAMPHUR = value; }
        }
        public double SENDPROVINCE
        {
            get { return _SENDPROVINCE; }
            set { _SENDPROVINCE = value; }
        }
        public string SENDZIPCODE
        {
            get { return _SENDZIPCODE; }
            set { _SENDZIPCODE = value; }
        }
        public string SENDFAX
        {
            get { return _SENDFAX; }
            set { _SENDFAX = value; }
        }
        public string SENDEMAIL
        {
            get { return _SENDEMAIL; }
            set { _SENDEMAIL = value; }
        }
        public string DELIVERTYPE
        {
            get { return _DELIVERTYPE; }
            set { _DELIVERTYPE = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public DateTime EFDATE
        {
            get { return _EFDATE; }
            set { _EFDATE = value; }
        }
        public DateTime EPDATE
        {
            get { return _EPDATE; }
            set { _EPDATE = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public string CREATEBY
        {
            get { return _CREATEBY; }
        }
        public DateTime CREATEON
        {
            get { return _CREATEON; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (UPDATEBY,UPDATEON,SENDTEL,LOID,CODE,TITLE,NAME,LASTNAME,MEMBERTYPE,CUSTOMERTYPE,IDENTITY,PAYMENT,CREDITDAY,CREDITAMOUNT,BILLADDRESS,BILLROAD,BILLTAMBOL,BILLAMPHUR,BILLPROVINCE,BILLZIPCODE,BILLTEL,BILLFAX,BILLEMAIL,CTITLE,CNAME,CLASTNAME,CADDRESS,CROAD,CTAMBOL,CAMPHUR,CPROVINCE,CZIPCODE,CTEL,CFAX,CEMAIL,CMOBILE,SENDPLACE,SENDADDRESS,SENDROAD,SENDTAMBOL,SENDAMPHUR,SENDPROVINCE,SENDZIPCODE,SENDFAX,SENDEMAIL,DELIVERTYPE,ACTIVE,EFDATE,EPDATE,REMARK,CREATEBY,CREATEON)VALUES(";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_SENDTEL) + "',";// SENDTEL";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";

                if(_TITLE == 0)
                    sqlz += "  NULL,";// TITLE";
                else
                    sqlz += "  " + _TITLE.ToString() + ",";// TITLE";
                
                sqlz += " '" + OracleDB.QRText(_NAME) + "',";// NAME";
                sqlz += " '" + OracleDB.QRText(_LASTNAME) + "',";// LASTNAME";
                sqlz += "  " + _MEMBERTYPE.ToString() + ",";// MEMBERTYPE";
                sqlz += " '" + OracleDB.QRText(_CUSTOMERTYPE) + "',";// CUSTOMERTYPE";
                sqlz += " '" + OracleDB.QRText(_IDENTITY) + "',";// IDENTITY";
                sqlz += " '" + OracleDB.QRText(_PAYMENT) + "',";// PAYMENT";
                sqlz += "  " + _CREDITDAY.ToString() + ",";// CREDITDAY";
                sqlz += "  " + _CREDITAMOUNT.ToString() + ",";// CREDITAMOUNT";
                sqlz += " '" + OracleDB.QRText(_BILLADDRESS) + "',";// BILLADDRESS";
                sqlz += " '" + OracleDB.QRText(_BILLROAD) + "',";// BILLROAD";

                if (_BILLTAMBOL == 0)
                    sqlz += "  NULL,";// BILLTAMBOL";
                else
                    sqlz += "  " + _BILLTAMBOL.ToString() + ",";// BILLTAMBOL";

                if (_BILLAMPHUR == 0)
                    sqlz += "  NULL,";// BILLAMPHUR";
                else
                    sqlz += "  " + _BILLAMPHUR.ToString() + ",";// BILLAMPHUR";

                if (_BILLPROVINCE == 0)
                    sqlz += "  NULL,";// BILLPROVINCE";
                else
                    sqlz += "  " + _BILLPROVINCE.ToString() + ",";// BILLPROVINCE";
               
                sqlz += " '" + OracleDB.QRText(_BILLZIPCODE) + "',";// BILLZIPCODE";
                sqlz += " '" + OracleDB.QRText(_BILLTEL) + "',";// BILLTEL";
                sqlz += " '" + OracleDB.QRText(_BILLFAX) + "',";// BILLFAX";
                sqlz += " '" + OracleDB.QRText(_BILLEMAIL) + "',";// BILLEMAIL";

                if (_CTITLE == 0)
                    sqlz += "  NULL,";// CTITLE";
                else
                    sqlz += "  " + _CTITLE.ToString() + ",";// CTITLE";
                
                sqlz += " '" + OracleDB.QRText(_CNAME) + "',";// CNAME";
                sqlz += " '" + OracleDB.QRText(_CLASTNAME) + "',";// CLASTNAME";
                sqlz += " '" + OracleDB.QRText(_CADDRESS) + "',";// CADDRESS";
                sqlz += " '" + OracleDB.QRText(_CROAD) + "',";// CROAD";

                if (_CTAMBOL == 0)
                    sqlz += "  NULL,";// CTAMBOL";
                else
                    sqlz += "  " + _CTAMBOL.ToString() + ",";// CTAMBOL";

                if (_CAMPHUR == 0)
                    sqlz += "  NULL,";// CAMPHUR";
                else
                    sqlz += "  " + _CAMPHUR.ToString() + ",";// CAMPHUR";

                if (_CPROVINCE == 0)
                    sqlz += "  NULL,";// CPROVINCE";
                else
                    sqlz += "  " + _CPROVINCE.ToString() + ",";// CPROVINCE";
                
                
                sqlz += " '" + OracleDB.QRText(_CZIPCODE) + "',";// CZIPCODE";
                sqlz += " '" + OracleDB.QRText(_CTEL) + "',";// CTEL";
                sqlz += " '" + OracleDB.QRText(_CFAX) + "',";// CFAX";
                sqlz += " '" + OracleDB.QRText(_CEMAIL) + "',";// CEMAIL";
                sqlz += " '" + OracleDB.QRText(_CMOBILE) + "',";// CMOBILE";
                sqlz += " '" + OracleDB.QRText(_SENDPLACE) + "',";// SENDPLACE";
                sqlz += " '" + OracleDB.QRText(_SENDADDRESS) + "',";// SENDADDRESS";
                sqlz += " '" + OracleDB.QRText(_SENDROAD) + "',";// SENDROAD";

                if (_SENDTAMBOL == 0)
                    sqlz += "  NULL,";// SENDTAMBOL";
                else
                    sqlz += "  " + _SENDTAMBOL.ToString() + ",";// SENDTAMBOL";

                if (_SENDAMPHUR == 0)
                    sqlz += "  NULL,";// SENDAMPHUR";
                else
                    sqlz += "  " + _SENDAMPHUR.ToString() + ",";// SENDAMPHUR";

                if (_SENDPROVINCE == 0)
                    sqlz += "  NULL,";// SENDPROVINCE";
                else
                    sqlz += "  " + _SENDPROVINCE.ToString() + ",";// SENDPROVINCE";
               
                sqlz += " '" + OracleDB.QRText(_SENDZIPCODE) + "',";// SENDZIPCODE";
                sqlz += " '" + OracleDB.QRText(_SENDFAX) + "',";// SENDFAX";
                sqlz += " '" + OracleDB.QRText(_SENDEMAIL) + "',";// SENDEMAIL";
                sqlz += " '" + OracleDB.QRText(_DELIVERTYPE) + "',";// DELIVERTYPE";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " " + OracleDB.QRDateTime(_EFDATE) + ",";// EFDATE";
                sqlz += " " + OracleDB.QRDateTime(_EPDATE) + ",";// EPDATE";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + "";// CREATEON";
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
                sqlz += " SENDTEL  = '" + OracleDB.QRText(_SENDTEL) + "', ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";

                if (_TITLE == 0)
                    sqlz += " TITLE  = NULL, ";
                else
                    sqlz += " TITLE  = " + _TITLE.ToString() + ", ";
                
                sqlz += " NAME  = '" + OracleDB.QRText(_NAME) + "', ";
                sqlz += " LASTNAME  = '" + OracleDB.QRText(_LASTNAME) + "', ";
                sqlz += " MEMBERTYPE  = " + _MEMBERTYPE.ToString() + ", ";
                sqlz += " CUSTOMERTYPE  = '" + OracleDB.QRText(_CUSTOMERTYPE) + "', ";
                sqlz += " IDENTITY  = '" + OracleDB.QRText(_IDENTITY) + "', ";
                sqlz += " PAYMENT  = '" + OracleDB.QRText(_PAYMENT) + "', ";
                sqlz += " CREDITDAY  = " + _CREDITDAY.ToString() + ", ";
                sqlz += " CREDITAMOUNT  = " + _CREDITAMOUNT.ToString() + ", ";
                sqlz += " BILLADDRESS  = '" + OracleDB.QRText(_BILLADDRESS) + "', ";
                sqlz += " BILLROAD  = '" + OracleDB.QRText(_BILLROAD) + "', ";

                if (_BILLTAMBOL == 0)
                    sqlz += " BILLTAMBOL  = NULL, ";
                else
                    sqlz += " BILLTAMBOL  = " + _BILLTAMBOL.ToString() + ", ";

                if (_BILLAMPHUR == 0)
                    sqlz += " BILLAMPHUR  = NULL, ";
                else
                    sqlz += " BILLAMPHUR  = " + _BILLAMPHUR.ToString() + ", ";

                if (_BILLPROVINCE == 0)
                    sqlz += " BILLPROVINCE  = NULL, ";
                else
                    sqlz += " BILLPROVINCE  = " + _BILLPROVINCE.ToString() + ", ";
                
                
                sqlz += " BILLZIPCODE  = '" + OracleDB.QRText(_BILLZIPCODE) + "', ";
                sqlz += " BILLTEL  = '" + OracleDB.QRText(_BILLTEL) + "', ";
                sqlz += " BILLFAX  = '" + OracleDB.QRText(_BILLFAX) + "', ";
                sqlz += " BILLEMAIL  = '" + OracleDB.QRText(_BILLEMAIL) + "', ";

                if (_CTITLE == 0)
                    sqlz += " CTITLE  = NULL, ";
                else
                    sqlz += " CTITLE  = " + _CTITLE.ToString() + ", ";
                
                sqlz += " CNAME  = '" + OracleDB.QRText(_CNAME) + "', ";
                sqlz += " CLASTNAME  = '" + OracleDB.QRText(_CLASTNAME) + "', ";
                sqlz += " CADDRESS  = '" + OracleDB.QRText(_CADDRESS) + "', ";
                sqlz += " CROAD  = '" + OracleDB.QRText(_CROAD) + "', ";

                if (_CTAMBOL == 0)
                    sqlz += " CTAMBOL  = NULL, ";
                else
                    sqlz += " CTAMBOL  = " + _CTAMBOL.ToString() + ", ";

                if (_CAMPHUR == 0)
                    sqlz += " CAMPHUR  = NULL, ";
                else
                    sqlz += " CAMPHUR  = " + _CAMPHUR.ToString() + ", ";

                if (_CPROVINCE == 0)
                    sqlz += " CPROVINCE  = NULL, ";
                else
                    sqlz += " CPROVINCE  = " + _CPROVINCE.ToString() + ", ";
                                
                sqlz += " CZIPCODE  = '" + OracleDB.QRText(_CZIPCODE) + "', ";
                sqlz += " CTEL  = '" + OracleDB.QRText(_CTEL) + "', ";
                sqlz += " CFAX  = '" + OracleDB.QRText(_CFAX) + "', ";
                sqlz += " CEMAIL  = '" + OracleDB.QRText(_CEMAIL) + "', ";
                sqlz += " CMOBILE  = '" + OracleDB.QRText(_CMOBILE) + "', ";
                sqlz += " SENDPLACE  = '" + OracleDB.QRText(_SENDPLACE) + "', ";
                sqlz += " SENDADDRESS  = '" + OracleDB.QRText(_SENDADDRESS) + "', ";
                sqlz += " SENDROAD  = '" + OracleDB.QRText(_SENDROAD) + "', ";

                if (_SENDTAMBOL == 0)
                    sqlz += " SENDTAMBOL  = NULL, ";
                else
                    sqlz += " SENDTAMBOL  = " + _SENDTAMBOL.ToString() + ", ";

                if (_SENDAMPHUR == 0)
                    sqlz += " SENDAMPHUR  = NULL, ";
                else
                    sqlz += " SENDAMPHUR  = " + _SENDAMPHUR.ToString() + ", ";

                if (_SENDPROVINCE == 0)
                    sqlz += " SENDPROVINCE  = NULL, ";
                else
                    sqlz += " SENDPROVINCE  = " + _SENDPROVINCE.ToString() + ", ";
                              
                sqlz += " SENDZIPCODE  = '" + OracleDB.QRText(_SENDZIPCODE) + "', ";
                sqlz += " SENDFAX  = '" + OracleDB.QRText(_SENDFAX) + "', ";
                sqlz += " SENDEMAIL  = '" + OracleDB.QRText(_SENDEMAIL) + "', ";
                sqlz += " DELIVERTYPE  = '" + OracleDB.QRText(_DELIVERTYPE) + "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " EFDATE  = " + OracleDB.QRDateTime(_EFDATE) + ", ";
                sqlz += " EPDATE  = " + OracleDB.QRDateTime(_EPDATE) + ", ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "' ";
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
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["SENDTEL"])) _SENDTEL = zRdr["SENDTEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["TITLE"])) _TITLE = Convert.ToDouble(zRdr["TITLE"]);
                        if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["LASTNAME"])) _LASTNAME = zRdr["LASTNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["MEMBERTYPE"])) _MEMBERTYPE = Convert.ToDouble(zRdr["MEMBERTYPE"]);
                        if (!Convert.IsDBNull(zRdr["CUSTOMERTYPE"])) _CUSTOMERTYPE = zRdr["CUSTOMERTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["IDENTITY"])) _IDENTITY = zRdr["IDENTITY"].ToString();
                        if (!Convert.IsDBNull(zRdr["PAYMENT"])) _PAYMENT = zRdr["PAYMENT"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREDITDAY"])) _CREDITDAY = Convert.ToDouble(zRdr["CREDITDAY"]);
                        if (!Convert.IsDBNull(zRdr["CREDITAMOUNT"])) _CREDITAMOUNT = Convert.ToDouble(zRdr["CREDITAMOUNT"]);
                        if (!Convert.IsDBNull(zRdr["BILLADDRESS"])) _BILLADDRESS = zRdr["BILLADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["BILLROAD"])) _BILLROAD = zRdr["BILLROAD"].ToString();
                        if (!Convert.IsDBNull(zRdr["BILLTAMBOL"])) _BILLTAMBOL = Convert.ToDouble(zRdr["BILLTAMBOL"]);
                        if (!Convert.IsDBNull(zRdr["BILLAMPHUR"])) _BILLAMPHUR = Convert.ToDouble(zRdr["BILLAMPHUR"]);
                        if (!Convert.IsDBNull(zRdr["BILLPROVINCE"])) _BILLPROVINCE = Convert.ToDouble(zRdr["BILLPROVINCE"]);
                        if (!Convert.IsDBNull(zRdr["BILLZIPCODE"])) _BILLZIPCODE = zRdr["BILLZIPCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["BILLTEL"])) _BILLTEL = zRdr["BILLTEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["BILLFAX"])) _BILLFAX = zRdr["BILLFAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["BILLEMAIL"])) _BILLEMAIL = zRdr["BILLEMAIL"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTITLE"])) _CTITLE = Convert.ToDouble(zRdr["CTITLE"]);
                        if (!Convert.IsDBNull(zRdr["CNAME"])) _CNAME = zRdr["CNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CLASTNAME"])) _CLASTNAME = zRdr["CLASTNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["CADDRESS"])) _CADDRESS = zRdr["CADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["CROAD"])) _CROAD = zRdr["CROAD"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTAMBOL"])) _CTAMBOL = Convert.ToDouble(zRdr["CTAMBOL"]);
                        if (!Convert.IsDBNull(zRdr["CAMPHUR"])) _CAMPHUR = Convert.ToDouble(zRdr["CAMPHUR"]);
                        if (!Convert.IsDBNull(zRdr["CPROVINCE"])) _CPROVINCE = Convert.ToDouble(zRdr["CPROVINCE"]);
                        if (!Convert.IsDBNull(zRdr["CZIPCODE"])) _CZIPCODE = zRdr["CZIPCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CTEL"])) _CTEL = zRdr["CTEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["CFAX"])) _CFAX = zRdr["CFAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["CEMAIL"])) _CEMAIL = zRdr["CEMAIL"].ToString();
                        if (!Convert.IsDBNull(zRdr["CMOBILE"])) _CMOBILE = zRdr["CMOBILE"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDPLACE"])) _SENDPLACE = zRdr["SENDPLACE"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDADDRESS"])) _SENDADDRESS = zRdr["SENDADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDROAD"])) _SENDROAD = zRdr["SENDROAD"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDTAMBOL"])) _SENDTAMBOL = Convert.ToDouble(zRdr["SENDTAMBOL"]);
                        if (!Convert.IsDBNull(zRdr["SENDAMPHUR"])) _SENDAMPHUR = Convert.ToDouble(zRdr["SENDAMPHUR"]);
                        if (!Convert.IsDBNull(zRdr["SENDPROVINCE"])) _SENDPROVINCE = Convert.ToDouble(zRdr["SENDPROVINCE"]);
                        if (!Convert.IsDBNull(zRdr["SENDZIPCODE"])) _SENDZIPCODE = zRdr["SENDZIPCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDFAX"])) _SENDFAX = zRdr["SENDFAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDEMAIL"])) _SENDEMAIL = zRdr["SENDEMAIL"].ToString();
                        if (!Convert.IsDBNull(zRdr["DELIVERTYPE"])) _DELIVERTYPE = zRdr["DELIVERTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["EFDATE"])) _EFDATE = OracleDB.DBDate(zRdr["EFDATE"]);
                        if (!Convert.IsDBNull(zRdr["EPDATE"])) _EPDATE = OracleDB.DBDate(zRdr["EPDATE"]);
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
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