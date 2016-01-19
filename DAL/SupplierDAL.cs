using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class SupplierDAL
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

        private string tableName = "SUPPLIER";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CODE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _SUPPLIERNAME = "";
        string _TAXID = "";
        string _ADDRESS = "";
        string _ROAD = "";
        double _TAMBOL = 0;
        double _AMPHUR = 0;
        double _PROVINCE = 0;
        string _ZIPCODE = "";
        string _TEL = "";
        string _FAX = "";
        string _EMAIL = "";
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
        string _CMOBILE = "";
        string _CFAX = "";
        string _CEMAIL = "";
        string _REMARK = "";
        string _ACTIVE = "";
        string _PAYMENTYPE = "";
        double _CREDITDAY = 0;
        double _CREDITAMOUNT = 0;
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
        public string UPDATEBY
        {
            get { return _UPDATEBY; }
        }
        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
        }
        public string SUPPLIERNAME
        {
            get { return _SUPPLIERNAME; }
            set { _SUPPLIERNAME = value; }
        }
        public string TAXID
        {
            get { return _TAXID; }
            set { _TAXID = value; }
        }
        public string ADDRESS
        {
            get { return _ADDRESS; }
            set { _ADDRESS = value; }
        }
        public string ROAD
        {
            get { return _ROAD; }
            set { _ROAD = value; }
        }
        public double TAMBOL
        {
            get { return _TAMBOL; }
            set { _TAMBOL = value; }
        }
        public double AMPHUR
        {
            get { return _AMPHUR; }
            set { _AMPHUR = value; }
        }
        public double PROVINCE
        {
            get { return _PROVINCE; }
            set { _PROVINCE = value; }
        }
        public string ZIPCODE
        {
            get { return _ZIPCODE; }
            set { _ZIPCODE = value; }
        }
        public string TEL
        {
            get { return _TEL; }
            set { _TEL = value; }
        }
        public string FAX
        {
            get { return _FAX; }
            set { _FAX = value; }
        }
        public string EMAIL
        {
            get { return _EMAIL; }
            set { _EMAIL = value; }
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
        public string CMOBILE
        {
            get { return _CMOBILE; }
            set { _CMOBILE = value; }
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
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public string PAYMENTYPE
        {
            get { return _PAYMENTYPE; }
            set { _PAYMENTYPE = value; }
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
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CODE,CREATEBY,CREATEON,UPDATEBY,UPDATEON,SUPPLIERNAME,TAXID,ADDRESS,ROAD,TAMBOL,AMPHUR,PROVINCE,ZIPCODE,TEL,FAX,EMAIL,CTITLE,CNAME,CLASTNAME,CADDRESS,CROAD,CTAMBOL,CAMPHUR,CPROVINCE,CZIPCODE,CTEL,CMOBILE,CFAX,CEMAIL,REMARK,ACTIVE,PAYMENTYPE,CREDITDAY,CREDITAMOUNT)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_SUPPLIERNAME) + "',";// SUPPLIERNAME";
                sqlz += " '" + OracleDB.QRText(_TAXID) + "',";// TAXID";
                sqlz += " '" + OracleDB.QRText(_ADDRESS) + "',";// ADDRESS";
                sqlz += " '" + OracleDB.QRText(_ROAD) + "',";// ROAD";
                sqlz += "  " + _TAMBOL.ToString() + ",";// TAMBOL";
                sqlz += "  " + _AMPHUR.ToString() + ",";// AMPHUR";
                sqlz += "  " + _PROVINCE.ToString() + ",";// PROVINCE";
                sqlz += " '" + OracleDB.QRText(_ZIPCODE) + "',";// ZIPCODE";
                sqlz += " '" + OracleDB.QRText(_TEL) + "',";// TEL";
                sqlz += " '" + OracleDB.QRText(_FAX) + "',";// FAX";
                sqlz += " '" + OracleDB.QRText(_EMAIL) + "',";// EMAIL";
                sqlz += "  " + _CTITLE.ToString() + ",";// CTITLE";
                sqlz += " '" + OracleDB.QRText(_CNAME) + "',";// CNAME";
                sqlz += " '" + OracleDB.QRText(_CLASTNAME) + "',";// CLASTNAME";
                sqlz += " '" + OracleDB.QRText(_CADDRESS) + "',";// CADDRESS";
                sqlz += " '" + OracleDB.QRText(_CROAD) + "',";// CROAD";
                sqlz += "  " + _CTAMBOL.ToString() + ",";// CTAMBOL";
                sqlz += "  " + _CAMPHUR.ToString() + ",";// CAMPHUR";
                sqlz += "  " + _CPROVINCE.ToString() + ",";// CPROVINCE";
                sqlz += " '" + OracleDB.QRText(_CZIPCODE) + "',";// CZIPCODE";
                sqlz += " '" + OracleDB.QRText(_CTEL) + "',";// CTEL";
                sqlz += " '" + OracleDB.QRText(_CMOBILE) + "',";// CMOBILE";
                sqlz += " '" + OracleDB.QRText(_CFAX) + "',";// CFAX";
                sqlz += " '" + OracleDB.QRText(_CEMAIL) + "',";// CEMAIL";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_PAYMENTYPE) + "',";// PAYMENTYPE";
                sqlz += "  " + _CREDITDAY.ToString() + ",";// CREDITDAY";
                sqlz += "  " + _CREDITAMOUNT.ToString() + "";// CREDITAMOUNT";
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
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " SUPPLIERNAME  = '" + OracleDB.QRText(_SUPPLIERNAME) + "', ";
                sqlz += " TAXID  = '" + OracleDB.QRText(_TAXID) + "', ";
                sqlz += " ADDRESS  = '" + OracleDB.QRText(_ADDRESS) + "', ";
                sqlz += " ROAD  = '" + OracleDB.QRText(_ROAD) + "', ";
                sqlz += " TAMBOL  = " + _TAMBOL.ToString() + ", ";
                sqlz += " AMPHUR  = " + _AMPHUR.ToString() + ", ";
                sqlz += " PROVINCE  = " + _PROVINCE.ToString() + ", ";
                sqlz += " ZIPCODE  = '" + OracleDB.QRText(_ZIPCODE) + "', ";
                sqlz += " TEL  = '" + OracleDB.QRText(_TEL) + "', ";
                sqlz += " FAX  = '" + OracleDB.QRText(_FAX) + "', ";
                sqlz += " EMAIL  = '" + OracleDB.QRText(_EMAIL) + "', ";
                sqlz += " CTITLE  = " + _CTITLE.ToString() + ", ";
                sqlz += " CNAME  = '" + OracleDB.QRText(_CNAME) + "', ";
                sqlz += " CLASTNAME  = '" + OracleDB.QRText(_CLASTNAME) + "', ";
                sqlz += " CADDRESS  = '" + OracleDB.QRText(_CADDRESS) + "', ";
                sqlz += " CROAD  = '" + OracleDB.QRText(_CROAD) + "', ";
                sqlz += " CTAMBOL  = " + _CTAMBOL.ToString() + ", ";
                sqlz += " CAMPHUR  = " + _CAMPHUR.ToString() + ", ";
                sqlz += " CPROVINCE  = " + _CPROVINCE.ToString() + ", ";
                sqlz += " CZIPCODE  = '" + OracleDB.QRText(_CZIPCODE) + "', ";
                sqlz += " CTEL  = '" + OracleDB.QRText(_CTEL) + "', ";
                sqlz += " CMOBILE  = '" + OracleDB.QRText(_CMOBILE) + "', ";
                sqlz += " CFAX  = '" + OracleDB.QRText(_CFAX) + "', ";
                sqlz += " CEMAIL  = '" + OracleDB.QRText(_CEMAIL) + "', ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " PAYMENTYPE  = '" + OracleDB.QRText(_PAYMENTYPE) + "', ";
                sqlz += " CREDITDAY  = " + _CREDITDAY.ToString() + ", ";
                sqlz += " CREDITAMOUNT  = " + _CREDITAMOUNT.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["SUPPLIERNAME"])) _SUPPLIERNAME = zRdr["SUPPLIERNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["TAXID"])) _TAXID = zRdr["TAXID"].ToString();
                        if (!Convert.IsDBNull(zRdr["ADDRESS"])) _ADDRESS = zRdr["ADDRESS"].ToString();
                        if (!Convert.IsDBNull(zRdr["ROAD"])) _ROAD = zRdr["ROAD"].ToString();
                        if (!Convert.IsDBNull(zRdr["TAMBOL"])) _TAMBOL = Convert.ToDouble(zRdr["TAMBOL"]);
                        if (!Convert.IsDBNull(zRdr["AMPHUR"])) _AMPHUR = Convert.ToDouble(zRdr["AMPHUR"]);
                        if (!Convert.IsDBNull(zRdr["PROVINCE"])) _PROVINCE = Convert.ToDouble(zRdr["PROVINCE"]);
                        if (!Convert.IsDBNull(zRdr["ZIPCODE"])) _ZIPCODE = zRdr["ZIPCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["TEL"])) _TEL = zRdr["TEL"].ToString();
                        if (!Convert.IsDBNull(zRdr["FAX"])) _FAX = zRdr["FAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["EMAIL"])) _EMAIL = zRdr["EMAIL"].ToString();
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
                        if (!Convert.IsDBNull(zRdr["CMOBILE"])) _CMOBILE = zRdr["CMOBILE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CFAX"])) _CFAX = zRdr["CFAX"].ToString();
                        if (!Convert.IsDBNull(zRdr["CEMAIL"])) _CEMAIL = zRdr["CEMAIL"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["PAYMENTYPE"])) _PAYMENTYPE = zRdr["PAYMENTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREDITDAY"])) _CREDITDAY = Convert.ToDouble(zRdr["CREDITDAY"]);
                        if (!Convert.IsDBNull(zRdr["CREDITAMOUNT"])) _CREDITAMOUNT = Convert.ToDouble(zRdr["CREDITAMOUNT"]);
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