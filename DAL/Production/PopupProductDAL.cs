using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace ABB.DAL.Production
{
    public class PopupProductDAL
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
            return doUpdate(" PDPLOID = " + _PDPLOID.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByLOID(double zLOID, OracleTransaction zTrans)
        {
            return doGetdata(" PDPLOID = " + zLOID.ToString() + " ", zTrans);
        }
        public bool GetDataByCODE(string zcode, OracleTransaction zTrans)
        {
            return doGetdataInv("LOTNO = '" + zcode.ToString() + "' ", zTrans);
        }
        public bool GetDataByLOTNO(string code, OracleTransaction zTrans)
        {
            return doGetdata(" LOTNO = '" + code.ToString() + "'", zTrans);
        }
        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteCurrentData(OracleTransaction zTrans)
        {
            return doDelete(" PDPLOID = " + _PDPLOID.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Get Data List of This Table
        /// </summary>
        /// <param name="whereCause"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public DataTable GetDataList(string whereCause, OracleTransaction zTrans)
        {
            whereCause += (whereCause == "" ? "" : " AND ") + "LOTNO IS NOT NULL AND PRODSTATUS <> 'AP' ";
            return OracleDB.ExecListCmd(sql_select + (whereCause == "" ? "" : " WHERE ") + whereCause);
        }

        #endregion

        #region Constant

        private string tableName = "V_PRODUCT_PDPRODUCT";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _PDLOID = 0;
        string _PDCODE = "";
        string _PDBARCODE = "";
        string _PDABBNAME = "";
        string _PDNAME = "";
        double _PDPLOID = 0;
        string _LOTNO = "";
        double _PDORDER = 0;
        double _BATCHSIZE = 0;
        double _BATCHSIZEUNIT = 0;
        string _BATCHSIZEUNITNAME = "";
        double _PACKSIZE = 0;
        double _PACKSIZEUNIT = 0;
        double _PDQTY = 0;
        double _STDQTY = 0;
        double _PDUNIT = 0;
        string _PDUNITNAME = "";
        DateTime _MFGDATE = new DateTime(1, 1, 1);
        string _PRODSTATUS = "";
        string _POSTATUS = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
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
        public double PDLOID
        {
            get { return _PDLOID; }
            set { _PDLOID = value; }
        }
        public string PDCODE
        {
            get { return _PDCODE; }
            set { _PDCODE = value; }
        }
        public string PDBARCODE
        {
            get { return _PDBARCODE; }
            set { _PDBARCODE = value; }
        }
        public string PDABBNAME
        {
            get { return _PDABBNAME; }
            set { _PDABBNAME = value; }
        }
        public string PDNAME
        {
            get { return _PDNAME; }
            set { _PDNAME = value; }
        }
        public double PDPLOID
        {
            get { return _PDPLOID; }
            set { _PDPLOID = value; }
        }
        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }
        public double PDORDER
        {
            get { return _PDORDER; }
            set { _PDORDER = value; }
        }
        public double BATCHSIZE
        {
            get { return _BATCHSIZE; }
            set { _BATCHSIZE = value; }
        }
        public double BATCHSIZEUNIT
        {
            get { return _BATCHSIZEUNIT; }
            set { _BATCHSIZEUNIT = value; }
        }
        public string BATCHSIZEUNITNAME
        {
            get { return _BATCHSIZEUNITNAME; }
            set { _BATCHSIZEUNITNAME = value; }
        }
        public double PACKSIZE
        {
            get { return _PACKSIZE; }
            set { _PACKSIZE = value; }
        }
        public double PACKSIZEUNIT
        {
            get { return _PACKSIZEUNIT; }
            set { _PACKSIZEUNIT = value; }
        }
        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }
        public double STDQTY
        {
            get { return _STDQTY; }
            set { _STDQTY = value; }
        }
        public double PDUNIT
        {
            get { return _PDUNIT; }
            set { _PDUNIT = value; }
        }
        public string PDUNITNAME
        {
            get { return _PDUNITNAME; }
            set { _PDUNITNAME = value; }
        }
        public DateTime MFGDATE
        {
            get { return _MFGDATE; }
            set { _MFGDATE = value; }
        }
        public string PRODSTATUS
        {
            get { return _PRODSTATUS; }
            set { _PRODSTATUS = value; }
        }
        public string POSTATUS
        {
            get { return _POSTATUS; }
            set { _POSTATUS = value; }
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
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (PDPLOID,PDBARCODE,PDNAME, LOTNO, PDUNITNAME, MFGDATE) VALUES (";
                sqlz += "  " + PDPLOID.ToString() + ",";
                sqlz += " '" + OracleDB.QRText(PDNAME) + "',";
                sqlz += " '" + OracleDB.QRText(PDBARCODE) + "',";
                sqlz += " '" + OracleDB.QRText(LOTNO) + "',";
                //sqlz += " " + OracleDB.QRText(BATCHSIZE).ToString() + ",";
                //sqlz += " " + OracleDB.QRText(STDQTY).ToString() + ",";
                sqlz += " '" + OracleDB.QRText(PDUNITNAME) + "',";
                sqlz += " '" + OracleDB.QRDateTime(MFGDATE) + "' ";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " PDPLOID  = " + _PDPLOID.ToString() + ", ";
                sqlz += " PDBARCODE  = '" + OracleDB.QRText(_PDBARCODE) + "', ";
                sqlz += " PDNAME  = '" + OracleDB.QRText(_PDNAME) + "', ";
                sqlz += " LOTNO  = '" + OracleDB.QRText(_LOTNO) + "', ";
                //sqlz += " BATCHSIZE  = " + OracleDB.QRText(_BATCHSIZE) + ", ";
                //sqlz += " STDQTY  = " + OracleDB.QRText(_STDQTY) + ", ";
                sqlz += " PDUNITNAME  = '" + OracleDB.QRText(_PDUNITNAME) + "', ";
                sqlz += " MFGDATE  = '" + OracleDB.QRDateTime(_MFGDATE) + "' ";
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
                string sqlz = "SELECT * FROM v_product_pdproduct ";
                //string sqlz = "SELECT RQ.LOID,RQ.CODE,RQ.REQDATE,RQ.INVCODE,RQ.CNAME || ' ' || RQ.CLASTNAME RQCUSNAME,PD.NAME PRODUCTNAME, ";
                //sqlz += "CU.NAME || ' ' || CU.LASTNAME CUNAME, CU.CODE CUSTOMERCODE, ";
                //sqlz += "CASE WHEN RQ.CNAME IS NULL THEN CU.NAME || ' ' || CU.LASTNAME ";
                //sqlz += "WHEN RQ.CNAME IS NOT NULL THEN RQ.CNAME || ' ' || RQ.CLASTNAME END AS CUSTOMERNAME ";
                //sqlz += "FROM REQUISITION RQ INNER JOIN CUSTOMER CU ON RQ.CUSTOMER=CU.LOID ";
                //sqlz += "INNER JOIN REQUISITIONITEM RQI ON  RQ.LOID=RQI.REQUISITION ";
                //sqlz += "INNER JOIN PRODUCT PD ON RQI.PRODUCT= PD.LOID ";
                //sqlz += "WHERE RQ.STATUS='AP' ";
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
                    _PDPLOID = OracleDB.GetLOID(tableName, zTrans);
                    ret = (OracleDB.ExecNonQueryCmd(sql_insert, zTrans) > 0);
                    if (!ret) _error = OracleDB.Err_NoInsert;
                    else _OnDB = true;
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
                        if (!Convert.IsDBNull(zRdr["PDPLOID"])) _PDPLOID = Convert.ToDouble(zRdr["PDPLOID"]);
                        if (!Convert.IsDBNull(zRdr["PDBARCODE"])) _PDBARCODE = zRdr["PDBARCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["PDNAME"])) _PDNAME = zRdr["PDNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOTNO"])) _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["BATCHSIZE"])) _BATCHSIZE = Convert.ToDouble(zRdr["BATCHSIZE"]);
                        if (!Convert.IsDBNull(zRdr["BATCHSIZEUNIT"])) _BATCHSIZEUNIT = Convert.ToDouble(zRdr["BATCHSIZEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["BATCHSIZEUNITNAME"])) _BATCHSIZEUNITNAME = zRdr["BATCHSIZEUNITNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["PACKSIZE"])) _PACKSIZE = Convert.ToDouble(zRdr["PACKSIZE"]);
                        if (!Convert.IsDBNull(zRdr["PACKSIZEUNIT"])) _PACKSIZEUNIT = Convert.ToDouble(zRdr["PACKSIZEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["STDQTY"])) _STDQTY = Convert.ToDouble(zRdr["STDQTY"]);
                        if (!Convert.IsDBNull(zRdr["PDUNITNAME"])) _PDUNITNAME = zRdr["PDUNITNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["MFGDATE"])) _MFGDATE = OracleDB.DBDate(zRdr["MFGDATE"]);
                    }
                    else
                    {
                        ret = false;
                        _error = OracleDB.Err_NoSelect;
                    }
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
        private bool doGetdataInv(string whText, OracleTransaction zTrans)
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
                        if (!Convert.IsDBNull(zRdr["PDPLOID"])) _PDPLOID = Convert.ToDouble(zRdr["PDPLOID"]);
                        if (!Convert.IsDBNull(zRdr["PDBARCODE"])) _PDBARCODE = zRdr["PDBARCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["PDNAME"])) _PDNAME = zRdr["PDNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOTNO"])) _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["BATCHSIZE"])) _BATCHSIZE = Convert.ToDouble(zRdr["BATCHSIZE"]);
                        if (!Convert.IsDBNull(zRdr["BATCHSIZEUNIT"])) _BATCHSIZEUNIT = Convert.ToDouble(zRdr["BATCHSIZEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["BATCHSIZEUNITNAME"])) _BATCHSIZEUNITNAME = zRdr["BATCHSIZEUNITNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["PACKSIZE"])) _PACKSIZE = Convert.ToDouble(zRdr["PACKSIZE"]);
                        if (!Convert.IsDBNull(zRdr["PACKSIZEUNIT"])) _PACKSIZEUNIT = Convert.ToDouble(zRdr["PACKSIZEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["STDQTY"])) _STDQTY = Convert.ToDouble(zRdr["STDQTY"]);
                        if (!Convert.IsDBNull(zRdr["PDUNITNAME"])) _PDUNITNAME = zRdr["PDUNITNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["MFGDATE"])) _MFGDATE = OracleDB.DBDate(zRdr["MFGDATE"]);
                    }
                    else
                    {
                        ret = false;
                        _error = OracleDB.Err_NoSelect;
                    }
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

