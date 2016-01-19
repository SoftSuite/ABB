using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL.Production
{
    public class PDProductDAL
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

        private string tableName = "PDPRODUCT";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _PRICE = 0;
        double _PROCESS = 0;
        double _QUARANTINEUNIT = 0;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _LOTNO = "";
        double _PDORDER = 0;
        double _PRODUCT = 0;
        double _BOM = 0;
        double _BATCHSIZE = 0;
        double _BATCHSIZEUNIT = 0;
        string _PRODSTATUS = "";
        DateTime _DUEDATE = new DateTime(1, 1, 1);
        DateTime _MFGDATE = new DateTime(1, 1, 1);
        DateTime _EXPDATE = new DateTime(1, 1, 1);
        double _STDQTY = 0;
        double _PDQTY = 0;
        double _YIELD = 0;
        string _PACKING = "";
        string _PACKAGE = "";
        DateTime _RADIATEDATE = new DateTime(1, 1, 1);
        double _RADIATEQTY = 0;
        double _RADIATEUNIT = 0;
        string _RADIATEREMARK = "";
        DateTime _RADIATERETDATE = new DateTime(1, 1, 1);
        double _RADIATERETQTY = 0;
        double _RADIATERETUNIT = 0;
        string _RADIATERETREMARK = "";
        DateTime _QUARANTINEDATE = new DateTime(1, 1, 1);
        double _QUARANTINEQTY = 0;
        string _QUARANTINEREMARK = "";
        DateTime _SENDQCDATE = new DateTime(1, 1, 1);
        double _QCQTY1 = 0;
        double _QCQTY2 = 0;
        double _QCQTY3 = 0;
        string _QCRESULT = "";
        string _QCREMARK = "";
        DateTime _SENDFGDATE = new DateTime(1, 1, 1);
        double _SENDFGQTY = 0;
        string _SENDFGREMARK = "";
        string _REFTABLE = "";
        double _REFLOID = 0;
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
        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }
        public double PROCESS
        {
            get { return _PROCESS; }
            set { _PROCESS = value; }
        }
        public double QUARANTINEUNIT
        {
            get { return _QUARANTINEUNIT; }
            set { _QUARANTINEUNIT = value; }
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
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public double BOM
        {
            get { return _BOM; }
            set { _BOM = value; }
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
        public string PRODSTATUS
        {
            get { return _PRODSTATUS; }
            set { _PRODSTATUS = value; }
        }
        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }
        public DateTime MFGDATE
        {
            get { return _MFGDATE; }
            set { _MFGDATE = value; }
        }
        public DateTime EXPDATE
        {
            get { return _EXPDATE; }
            set { _EXPDATE = value; }
        }
        public double STDQTY
        {
            get { return _STDQTY; }
            set { _STDQTY = value; }
        }
        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }
        public double YIELD
        {
            get { return _YIELD; }
            set { _YIELD = value; }
        }
        public string PACKING
        {
            get { return _PACKING; }
            set { _PACKING = value; }
        }
        public string PACKAGE
        {
            get { return _PACKAGE; }
            set { _PACKAGE = value; }
        }
        public DateTime RADIATEDATE
        {
            get { return _RADIATEDATE; }
            set { _RADIATEDATE = value; }
        }
        public double RADIATEQTY
        {
            get { return _RADIATEQTY; }
            set { _RADIATEQTY = value; }
        }
        public double RADIATEUNIT
        {
            get { return _RADIATEUNIT; }
            set { _RADIATEUNIT = value; }
        }
        public string RADIATEREMARK
        {
            get { return _RADIATEREMARK; }
            set { _RADIATEREMARK = value; }
        }
        public DateTime RADIATERETDATE
        {
            get { return _RADIATERETDATE; }
            set { _RADIATERETDATE = value; }
        }
        public double RADIATERETQTY
        {
            get { return _RADIATERETQTY; }
            set { _RADIATERETQTY = value; }
        }
        public double RADIATERETUNIT
        {
            get { return _RADIATERETUNIT; }
            set { _RADIATERETUNIT = value; }
        }
        public string RADIATERETREMARK
        {
            get { return _RADIATERETREMARK; }
            set { _RADIATERETREMARK = value; }
        }
        public DateTime QUARANTINEDATE
        {
            get { return _QUARANTINEDATE; }
            set { _QUARANTINEDATE = value; }
        }
        public double QUARANTINEQTY
        {
            get { return _QUARANTINEQTY; }
            set { _QUARANTINEQTY = value; }
        }
        public string QUARANTINEREMARK
        {
            get { return _QUARANTINEREMARK; }
            set { _QUARANTINEREMARK = value; }
        }
        public DateTime SENDQCDATE
        {
            get { return _SENDQCDATE; }
            set { _SENDQCDATE = value; }
        }
        public double QCQTY1
        {
            get { return _QCQTY1; }
            set { _QCQTY1 = value; }
        }
        public double QCQTY2
        {
            get { return _QCQTY2; }
            set { _QCQTY2 = value; }
        }
        public double QCQTY3
        {
            get { return _QCQTY3; }
            set { _QCQTY3 = value; }
        }
        public string QCRESULT
        {
            get { return _QCRESULT; }
            set { _QCRESULT = value; }
        }
        public string QCREMARK
        {
            get { return _QCREMARK; }
            set { _QCREMARK = value; }
        }
        public DateTime SENDFGDATE
        {
            get { return _SENDFGDATE; }
            set { _SENDFGDATE = value; }
        }
        public double SENDFGQTY
        {
            get { return _SENDFGQTY; }
            set { _SENDFGQTY = value; }
        }
        public string SENDFGREMARK
        {
            get { return _SENDFGREMARK; }
            set { _SENDFGREMARK = value; }
        }
        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }
        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (PRICE,PROCESS,QUARANTINEUNIT,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,LOTNO,PDORDER,PRODUCT,BOM,BATCHSIZE,BATCHSIZEUNIT,PRODSTATUS,DUEDATE,MFGDATE,EXPDATE,STDQTY,PDQTY,YIELD,PACKING,PACKAGE,RADIATEDATE,RADIATEQTY,RADIATEUNIT,RADIATEREMARK,RADIATERETDATE,RADIATERETQTY,RADIATERETUNIT,RADIATERETREMARK,QUARANTINEDATE,QUARANTINEQTY,QUARANTINEREMARK,SENDQCDATE,QCQTY1,QCQTY2,QCQTY3,QCRESULT,QCREMARK,SENDFGDATE,SENDFGQTY,SENDFGREMARK,REFTABLE,REFLOID)VALUES(";
                sqlz += "  " + _PRICE.ToString() + ",";// PRICE";
                sqlz += "  " + _PROCESS.ToString() + ",";// PROCESS";
                sqlz += "  " + _QUARANTINEUNIT.ToString() + ",";// QUARANTINEUNIT";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_LOTNO) + "',";// LOTNO";
                sqlz += "  " + _PDORDER.ToString() + ",";// PDORDER";
                sqlz += "  " + _PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += "  " + _BOM.ToString() + ",";// BOM";
                sqlz += "  " + _BATCHSIZE.ToString() + ",";// BATCHSIZE";
                sqlz += "  " + _BATCHSIZEUNIT.ToString() + ",";// BATCHSIZEUNIT";
                sqlz += " '" + OracleDB.QRText(_PRODSTATUS) + "',";// PRODSTATUS";
                sqlz += " " + OracleDB.QRDateTime(_DUEDATE) + ",";// DUEDATE";
                sqlz += " " + OracleDB.QRDateTime(_MFGDATE) + ",";// MFGDATE";
                sqlz += " " + OracleDB.QRDateTime(_EXPDATE) + ",";// EXPDATE";
                sqlz += "  " + _STDQTY.ToString() + ",";// STDQTY";
                sqlz += "  " + _PDQTY.ToString() + ",";// PDQTY";
                sqlz += "  " + _YIELD.ToString() + ",";// YIELD";
                sqlz += " '" + OracleDB.QRText(_PACKING) + "',";// PACKING";
                sqlz += " '" + OracleDB.QRText(_PACKAGE) + "',";// PACKAGE";
                sqlz += " " + OracleDB.QRDateTime(_RADIATEDATE) + ",";// RADIATEDATE";
                sqlz += "  " + _RADIATEQTY.ToString() + ",";// RADIATEQTY";
                sqlz += "  " + _RADIATEUNIT.ToString() + ",";// RADIATEUNIT";
                sqlz += " '" + OracleDB.QRText(_RADIATEREMARK) + "',";// RADIATEREMARK";
                sqlz += " " + OracleDB.QRDateTime(_RADIATERETDATE) + ",";// RADIATERETDATE";
                sqlz += "  " + _RADIATERETQTY.ToString() + ",";// RADIATERETQTY";
                sqlz += "  " + _RADIATERETUNIT.ToString() + ",";// RADIATERETUNIT";
                sqlz += " '" + OracleDB.QRText(_RADIATERETREMARK) + "',";// RADIATERETREMARK";
                sqlz += " " + OracleDB.QRDateTime(_QUARANTINEDATE) + ",";// QUARANTINEDATE";
                sqlz += "  " + _QUARANTINEQTY.ToString() + ",";// QUARANTINEQTY";
                sqlz += " '" + OracleDB.QRText(_QUARANTINEREMARK) + "',";// QUARANTINEREMARK";
                sqlz += " " + OracleDB.QRDateTime(_SENDQCDATE) + ",";// SENDQCDATE";
                sqlz += "  " + _QCQTY1.ToString() + ",";// QCQTY1";
                sqlz += "  " + _QCQTY2.ToString() + ",";// QCQTY2";
                sqlz += "  " + _QCQTY3.ToString() + ",";// QCQTY3";
                sqlz += " '" + OracleDB.QRText(_QCRESULT) + "',";// QCRESULT";
                sqlz += " '" + OracleDB.QRText(_QCREMARK) + "',";// QCREMARK";
                sqlz += " " + OracleDB.QRDateTime(_SENDFGDATE) + ",";// SENDFGDATE";
                sqlz += "  " + _SENDFGQTY.ToString() + ",";// SENDFGQTY";
                sqlz += " '" + OracleDB.QRText(_SENDFGREMARK) + "',";// SENDFGREMARK";
                sqlz += " '" + OracleDB.QRText(_REFTABLE) + "',";// REFTABLE";
                sqlz += "  " + _REFLOID.ToString() + "";// REFLOID";
                sqlz += " ) ";
                return sqlz;
            }
        }
        private string sql_update
        {
            get
            {
                string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " PRICE  = " + _PRICE.ToString() + ", ";
                sqlz += " PROCESS  = " + _PROCESS.ToString() + ", ";
                sqlz += " QUARANTINEUNIT  = " + _QUARANTINEUNIT.ToString() + ", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON) + ", ";
                sqlz += " LOTNO  = '" + OracleDB.QRText(_LOTNO) + "', ";
                sqlz += " PDORDER  = " + _PDORDER.ToString() + ", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString() + ", ";
                sqlz += " BOM  = " + _BOM.ToString() + ", ";
                sqlz += " BATCHSIZE  = " + _BATCHSIZE.ToString() + ", ";
                sqlz += " BATCHSIZEUNIT  = " + _BATCHSIZEUNIT.ToString() + ", ";
                sqlz += " PRODSTATUS  = '" + OracleDB.QRText(_PRODSTATUS) + "', ";
                sqlz += " DUEDATE  = " + OracleDB.QRDateTime(_DUEDATE) + ", ";
                sqlz += " MFGDATE  = " + OracleDB.QRDateTime(_MFGDATE) + ", ";
                sqlz += " EXPDATE  = " + OracleDB.QRDateTime(_EXPDATE) + ", ";
                sqlz += " STDQTY  = " + _STDQTY.ToString() + ", ";
                sqlz += " PDQTY  = " + _PDQTY.ToString() + ", ";
                sqlz += " YIELD  = " + _YIELD.ToString() + ", ";
                sqlz += " PACKING  = '" + OracleDB.QRText(_PACKING) + "', ";
                sqlz += " PACKAGE  = '" + OracleDB.QRText(_PACKAGE) + "', ";
                sqlz += " RADIATEDATE  = " + OracleDB.QRDateTime(_RADIATEDATE) + ", ";
                sqlz += " RADIATEQTY  = " + _RADIATEQTY.ToString() + ", ";
                sqlz += " RADIATEUNIT  = " + _RADIATEUNIT.ToString() + ", ";
                sqlz += " RADIATEREMARK  = '" + OracleDB.QRText(_RADIATEREMARK) + "', ";
                sqlz += " RADIATERETDATE  = " + OracleDB.QRDateTime(_RADIATERETDATE) + ", ";
                sqlz += " RADIATERETQTY  = " + _RADIATERETQTY.ToString() + ", ";
                sqlz += " RADIATERETUNIT  = " + _RADIATERETUNIT.ToString() + ", ";
                sqlz += " RADIATERETREMARK  = '" + OracleDB.QRText(_RADIATERETREMARK) + "', ";
                sqlz += " QUARANTINEDATE  = " + OracleDB.QRDateTime(_QUARANTINEDATE) + ", ";
                sqlz += " QUARANTINEQTY  = " + _QUARANTINEQTY.ToString() + ", ";
                sqlz += " QUARANTINEREMARK  = '" + OracleDB.QRText(_QUARANTINEREMARK) + "', ";
                sqlz += " SENDQCDATE  = " + OracleDB.QRDateTime(_SENDQCDATE) + ", ";
                sqlz += " QCQTY1  = " + _QCQTY1.ToString() + ", ";
                sqlz += " QCQTY2  = " + _QCQTY2.ToString() + ", ";
                sqlz += " QCQTY3  = " + _QCQTY3.ToString() + ", ";
                sqlz += " QCRESULT  = '" + OracleDB.QRText(_QCRESULT) + "', ";
                sqlz += " QCREMARK  = '" + OracleDB.QRText(_QCREMARK) + "', ";
                sqlz += " SENDFGDATE  = " + OracleDB.QRDateTime(_SENDFGDATE) + ", ";
                sqlz += " SENDFGQTY  = " + _SENDFGQTY.ToString() + ", ";
                sqlz += " SENDFGREMARK  = '" + OracleDB.QRText(_SENDFGREMARK) + "', ";
                sqlz += " REFTABLE  = '" + OracleDB.QRText(_REFTABLE) + "', ";
                sqlz += " REFLOID  = " + _REFLOID.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["PRICE"])) _PRICE = Convert.ToDouble(zRdr["PRICE"]);
                        if (!Convert.IsDBNull(zRdr["PROCESS"])) _PROCESS = Convert.ToDouble(zRdr["PROCESS"]);
                        if (!Convert.IsDBNull(zRdr["QUARANTINEUNIT"])) _QUARANTINEUNIT = Convert.ToDouble(zRdr["QUARANTINEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["LOTNO"])) _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["PDORDER"])) _PDORDER = Convert.ToDouble(zRdr["PDORDER"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["BOM"])) _BOM = Convert.ToDouble(zRdr["BOM"]);
                        if (!Convert.IsDBNull(zRdr["BATCHSIZE"])) _BATCHSIZE = Convert.ToDouble(zRdr["BATCHSIZE"]);
                        if (!Convert.IsDBNull(zRdr["BATCHSIZEUNIT"])) _BATCHSIZEUNIT = Convert.ToDouble(zRdr["BATCHSIZEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["PRODSTATUS"])) _PRODSTATUS = zRdr["PRODSTATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["DUEDATE"])) _DUEDATE = OracleDB.DBDate(zRdr["DUEDATE"]);
                        if (!Convert.IsDBNull(zRdr["MFGDATE"])) _MFGDATE = OracleDB.DBDate(zRdr["MFGDATE"]);
                        if (!Convert.IsDBNull(zRdr["EXPDATE"])) _EXPDATE = OracleDB.DBDate(zRdr["EXPDATE"]);
                        if (!Convert.IsDBNull(zRdr["STDQTY"])) _STDQTY = Convert.ToDouble(zRdr["STDQTY"]);
                        if (!Convert.IsDBNull(zRdr["PDQTY"])) _PDQTY = Convert.ToDouble(zRdr["PDQTY"]);
                        if (!Convert.IsDBNull(zRdr["YIELD"])) _YIELD = Convert.ToDouble(zRdr["YIELD"]);
                        if (!Convert.IsDBNull(zRdr["PACKING"])) _PACKING = zRdr["PACKING"].ToString();
                        if (!Convert.IsDBNull(zRdr["PACKAGE"])) _PACKAGE = zRdr["PACKAGE"].ToString();
                        if (!Convert.IsDBNull(zRdr["RADIATEDATE"])) _RADIATEDATE = OracleDB.DBDate(zRdr["RADIATEDATE"]);
                        if (!Convert.IsDBNull(zRdr["RADIATEQTY"])) _RADIATEQTY = Convert.ToDouble(zRdr["RADIATEQTY"]);
                        if (!Convert.IsDBNull(zRdr["RADIATEUNIT"])) _RADIATEUNIT = Convert.ToDouble(zRdr["RADIATEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["RADIATEREMARK"])) _RADIATEREMARK = zRdr["RADIATEREMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["RADIATERETDATE"])) _RADIATERETDATE = OracleDB.DBDate(zRdr["RADIATERETDATE"]);
                        if (!Convert.IsDBNull(zRdr["RADIATERETQTY"])) _RADIATERETQTY = Convert.ToDouble(zRdr["RADIATERETQTY"]);
                        if (!Convert.IsDBNull(zRdr["RADIATERETUNIT"])) _RADIATERETUNIT = Convert.ToDouble(zRdr["RADIATERETUNIT"]);
                        if (!Convert.IsDBNull(zRdr["RADIATERETREMARK"])) _RADIATERETREMARK = zRdr["RADIATERETREMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["QUARANTINEDATE"])) _QUARANTINEDATE = OracleDB.DBDate(zRdr["QUARANTINEDATE"]);
                        if (!Convert.IsDBNull(zRdr["QUARANTINEQTY"])) _QUARANTINEQTY = Convert.ToDouble(zRdr["QUARANTINEQTY"]);
                        if (!Convert.IsDBNull(zRdr["QUARANTINEREMARK"])) _QUARANTINEREMARK = zRdr["QUARANTINEREMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDQCDATE"])) _SENDQCDATE = OracleDB.DBDate(zRdr["SENDQCDATE"]);
                        if (!Convert.IsDBNull(zRdr["QCQTY1"])) _QCQTY1 = Convert.ToDouble(zRdr["QCQTY1"]);
                        if (!Convert.IsDBNull(zRdr["QCQTY2"])) _QCQTY2 = Convert.ToDouble(zRdr["QCQTY2"]);
                        if (!Convert.IsDBNull(zRdr["QCQTY3"])) _QCQTY3 = Convert.ToDouble(zRdr["QCQTY3"]);
                        if (!Convert.IsDBNull(zRdr["QCRESULT"])) _QCRESULT = zRdr["QCRESULT"].ToString();
                        if (!Convert.IsDBNull(zRdr["QCREMARK"])) _QCREMARK = zRdr["QCREMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["SENDFGDATE"])) _SENDFGDATE = OracleDB.DBDate(zRdr["SENDFGDATE"]);
                        if (!Convert.IsDBNull(zRdr["SENDFGQTY"])) _SENDFGQTY = Convert.ToDouble(zRdr["SENDFGQTY"]);
                        if (!Convert.IsDBNull(zRdr["SENDFGREMARK"])) _SENDFGREMARK = zRdr["SENDFGREMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFTABLE"])) _REFTABLE = zRdr["REFTABLE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFLOID"])) _REFLOID = Convert.ToDouble(zRdr["REFLOID"]);
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