using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class ProductBarcodeDAL
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

        public bool GetDataByBARCODE(string zBARCODE, OracleTransaction zTrans)
        {
            return doGetdata(" BARCODE = '" + zBARCODE + "' ", zTrans);
        }

        public bool GetDataByABBNAME(string zABBNAME, OracleTransaction zTrans)
        {
            return doGetdata(" ABBNAME = '" + zABBNAME + "' ", zTrans);
        }
        public bool GetDataByABBUNIT(double zLOID,double zUNIT, OracleTransaction zTrans)
        {
            return doGetdataUnit(" LOID = " + zLOID + " ", " UNIT = " + zUNIT + " ", zTrans);
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

        private string tableName = "PRODUCTBARCODE";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _BARCODE = "";
        string _ABBNAME = "";
        string _NAME = "";
        double _PRODUCTMASTER = 0;
        double _UNIT = 0;
        double _COST = 0;
        double _PRICE = 0;
        double _STDPRICE = 0;
        string _ISDISCOUNT = "";
        string _ISVAT = "";
        double _PACKSIZE = 0;
        double _PACKSIZEUNIT = 0;
        string _ISEDIT = "";
        string _ISREFUND = "";
        string _REMARK = "";
        string _ACTIVE = "";
        double _MULTIPLY = 0;
        string _ISDEFAULT = "";
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
        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }
        public string ABBNAME
        {
            get { return _ABBNAME; }
            set { _ABBNAME = value; }
        }
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public double PRODUCTMASTER
        {
            get { return _PRODUCTMASTER; }
            set { _PRODUCTMASTER = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public double COST
        {
            get { return _COST; }
            set { _COST = value; }
        }
        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }
        public double STDPRICE
        {
            get { return _STDPRICE; }
            set { _STDPRICE = value; }
        }
        public string ISDISCOUNT
        {
            get { return _ISDISCOUNT; }
            set { _ISDISCOUNT = value; }
        }
        public string ISVAT
        {
            get { return _ISVAT; }
            set { _ISVAT = value; }
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
        public string ISEDIT
        {
            get { return _ISEDIT; }
            set { _ISEDIT = value; }
        }
        public string ISREFUND
        {
            get { return _ISREFUND; }
            set { _ISREFUND = value; }
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
        public double MULTIPLY
        {
            get { return _MULTIPLY; }
            set { _MULTIPLY = value; }
        }
        public string ISDEFAULT
        {
            get { return _ISDEFAULT; }
            set { _ISDEFAULT = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,BARCODE,ABBNAME,NAME,PRODUCTMASTER,UNIT,COST,PRICE,STDPRICE,ISDISCOUNT,ISVAT,PACKSIZE,PACKSIZEUNIT,ISEDIT,ISREFUND,REMARK,ACTIVE,MULTIPLY,ISDEFAULT)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_BARCODE) + "',";// BARCODE";
                sqlz += " '" + OracleDB.QRText(_ABBNAME) + "',";// ABBNAME";
                sqlz += " '" + OracleDB.QRText(_NAME) + "',";// NAME";
                sqlz += "  " + _PRODUCTMASTER.ToString() + ",";// PRODUCTMASTER";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += "  " + _COST.ToString() + ",";// COST";
                sqlz += "  " + _PRICE.ToString() + ",";// PRICE";
                sqlz += "  " + _STDPRICE.ToString() + ",";// STDPRICE";
                sqlz += " '" + OracleDB.QRText(_ISDISCOUNT) + "',";// ISDISCOUNT";
                sqlz += " '" + OracleDB.QRText(_ISVAT) + "',";// ISVAT";
                sqlz += "  " + (_PACKSIZE ==0 ? "NULL" :_PACKSIZE.ToString()) + ",";// PACKSIZE";
                sqlz += "  " + (_PACKSIZEUNIT == 0 ? "NULL" : _PACKSIZEUNIT.ToString()) + ",";// PACKSIZEUNIT";
                sqlz += " '" + OracleDB.QRText(_ISEDIT) + "',";// ISEDIT";
                sqlz += " '" + OracleDB.QRText(_ISREFUND) + "',";// ISREFUND";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += "  " + _MULTIPLY.ToString() + ",";// MULTIPLY";
                sqlz += " '" + OracleDB.QRText(_ISDEFAULT) + "'";// ISDEFAULT";
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
                sqlz += " BARCODE  = '" + OracleDB.QRText(_BARCODE) + "', ";
                sqlz += " ABBNAME  = '" + OracleDB.QRText(_ABBNAME) + "', ";
                sqlz += " NAME  = '" + OracleDB.QRText(_NAME) + "', ";
                sqlz += " PRODUCTMASTER  = " + _PRODUCTMASTER.ToString() + ", ";
                sqlz += " UNIT  = " + _UNIT.ToString() + ", ";
                sqlz += " COST  = " + _COST.ToString() + ", ";
                sqlz += " PRICE  = " + _PRICE.ToString() + ", ";
                sqlz += " STDPRICE  = " + _STDPRICE.ToString() + ", ";
                sqlz += " ISDISCOUNT  = '" + OracleDB.QRText(_ISDISCOUNT) + "', ";
                sqlz += " ISVAT  = '" + OracleDB.QRText(_ISVAT) + "', ";
                sqlz += " PACKSIZE  = " + (_PACKSIZE == 0 ? "NULL" : _PACKSIZE.ToString()) + ", ";
                sqlz += " PACKSIZEUNIT  = " + (_PACKSIZEUNIT == 0 ? "NULL" : _PACKSIZEUNIT.ToString()) + ", ";
                sqlz += " ISEDIT  = '" + OracleDB.QRText(_ISEDIT) + "', ";
                sqlz += " ISREFUND  = '" + OracleDB.QRText(_ISREFUND) + "', ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " MULTIPLY  = " + _MULTIPLY.ToString() + ", ";
                sqlz += " ISDEFAULT  = '" + OracleDB.QRText(_ISDEFAULT) + "' ";
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
        private bool doGetdataUnit(string whText, string whText1, OracleTransaction zTrans)
        {
            bool ret = true;
            if (whText.Trim() != "")
            {
                string tmpWhere = " WHERE " + whText + " AND " + whText1;
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
                        if (!Convert.IsDBNull(zRdr["BARCODE"])) _BARCODE = zRdr["BARCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ABBNAME"])) _ABBNAME = zRdr["ABBNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCTMASTER"])) _PRODUCTMASTER = Convert.ToDouble(zRdr["PRODUCTMASTER"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["COST"])) _COST = Convert.ToDouble(zRdr["COST"]);
                        if (!Convert.IsDBNull(zRdr["PRICE"])) _PRICE = Convert.ToDouble(zRdr["PRICE"]);
                        if (!Convert.IsDBNull(zRdr["STDPRICE"])) _STDPRICE = Convert.ToDouble(zRdr["STDPRICE"]);
                        if (!Convert.IsDBNull(zRdr["ISDISCOUNT"])) _ISDISCOUNT = zRdr["ISDISCOUNT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISVAT"])) _ISVAT = zRdr["ISVAT"].ToString();
                        if (!Convert.IsDBNull(zRdr["PACKSIZE"])) _PACKSIZE = Convert.ToDouble(zRdr["PACKSIZE"]);
                        if (!Convert.IsDBNull(zRdr["PACKSIZEUNIT"])) _PACKSIZEUNIT = Convert.ToDouble(zRdr["PACKSIZEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["ISEDIT"])) _ISEDIT = zRdr["ISEDIT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISREFUND"])) _ISREFUND = zRdr["ISREFUND"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["MULTIPLY"])) _MULTIPLY = Convert.ToDouble(zRdr["MULTIPLY"]);
                        if (!Convert.IsDBNull(zRdr["ISDEFAULT"])) _ISDEFAULT = zRdr["ISDEFAULT"].ToString();
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
                        if (!Convert.IsDBNull(zRdr["BARCODE"])) _BARCODE = zRdr["BARCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ABBNAME"])) _ABBNAME = zRdr["ABBNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCTMASTER"])) _PRODUCTMASTER = Convert.ToDouble(zRdr["PRODUCTMASTER"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["COST"])) _COST = Convert.ToDouble(zRdr["COST"]);
                        if (!Convert.IsDBNull(zRdr["PRICE"])) _PRICE = Convert.ToDouble(zRdr["PRICE"]);
                        if (!Convert.IsDBNull(zRdr["STDPRICE"])) _STDPRICE = Convert.ToDouble(zRdr["STDPRICE"]);
                        if (!Convert.IsDBNull(zRdr["ISDISCOUNT"])) _ISDISCOUNT = zRdr["ISDISCOUNT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISVAT"])) _ISVAT = zRdr["ISVAT"].ToString();
                        if (!Convert.IsDBNull(zRdr["PACKSIZE"])) _PACKSIZE = Convert.ToDouble(zRdr["PACKSIZE"]);
                        if (!Convert.IsDBNull(zRdr["PACKSIZEUNIT"])) _PACKSIZEUNIT = Convert.ToDouble(zRdr["PACKSIZEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["ISEDIT"])) _ISEDIT = zRdr["ISEDIT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISREFUND"])) _ISREFUND = zRdr["ISREFUND"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["MULTIPLY"])) _MULTIPLY = Convert.ToDouble(zRdr["MULTIPLY"]);
                        if (!Convert.IsDBNull(zRdr["ISDEFAULT"])) _ISDEFAULT = zRdr["ISDEFAULT"].ToString();
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
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteDataByProductMaster(double productMaster, OracleTransaction zTrans)
        {
            return doDelete("PRODUCTMASTER = " + productMaster.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Delete Current Data From DB except Default data
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteDataByProductMasterExceptDefault(double productMaster, OracleTransaction zTrans)
        {
            return doDelete("PRODUCTMASTER = " + productMaster.ToString() + " AND ISDEFAULT <> 'Y' ", zTrans);
        }

        public bool UpdateActiveByProductMasterExceptDefault(double productMaster, string active, string userID, OracleTransaction zTrans)
        {
            bool ret = true;
            string sql = "UPDATE " + TableName + " SET ACTIVE = '" + active + "', ABBNAME = BARCODE,";
            sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
            sql += "WHERE PRODUCTMASTER = '" + productMaster.ToString() + "' AND ISDEFAULT <> 'Y' ";
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
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
            return ret;
        }

        /// <summary>
        /// Get Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByProductMaster(string productMaster, OracleTransaction zTrans)
        {
            return doGetdata("PRODUCTMASTER = " + productMaster + " AND ISDEFAULT= 'Y' ", zTrans);
        }

        public bool CheckBarcode(double productMaster, string barcode, double pdloid)
        {
            string sql = "SELECT * FROM PRODUCTBARCODE WHERE UPPER(BARCODE) = '" + barcode.ToUpper() + "' ";

            //if (productMaster != 0)
            //    sql += " AND PRODUCTMASTER = " + productMaster + " ";

            if (pdloid != 0)
                sql += " AND LOID <> " + pdloid + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckAbbname(double productMaster, string abbname, double pdloid)
        {
            string sql = "SELECT * FROM PRODUCTBARCODE WHERE UPPER(ABBNAME) = '" + abbname.ToUpper() + "' ";

            //if (productMaster != 0)
            //    sql += " AND PRODUCTMASTER = " + productMaster + " ";

            if (pdloid != 0)
                sql += " AND LOID <> " + pdloid + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckUnit(double productMaster, string unit, double pdloid)
        {
            string sql = "SELECT * FROM PRODUCTBARCODE WHERE UNIT = " + unit + " ";

            //if (productMaster != 0)
                sql += " AND PRODUCTMASTER = " + productMaster + " ";

            //if (pdloid != 0)
                sql += " AND LOID <> " + pdloid + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        #region Lotno Work
        /// <summary>
        /// Get running code with transaction
        /// </summary>
        /// <param name="RunningName"></param>
        /// <param name="RunningItem"></param>
        /// <returns></returns>
        public static string GetLotNo(string userID, double productLOID) { return GetLotNo(userID, productLOID, null); }
        public static string GetLotNo(string userID, double productLOID, OracleTransaction zTrans)
        {
            bool LetClose = false;
            string code = "";
            OracleConnection zConn = null;
            if (zTrans == null)
            {
                LetClose = true;
                zConn = OracleDB.GetConnection();
                zTrans = zConn.BeginTransaction(IsolationLevel.ReadCommitted);
            }

            ProductBarcodeDAL _bDAL = new ProductBarcodeDAL();
            if (_bDAL.GetDataByLOID(productLOID, zTrans))
            {
                ProductMasterDAL _dal = new ProductMasterDAL();
                if (_dal.GetDataByLOID(_bDAL.PRODUCTMASTER, zTrans))
                {
                    if (_dal.YEAR == (DateTime.Now.Year + 543).ToString().Substring(2))
                    {
                        if (_dal.RUNNING == "9")
                        {
                            _dal.RUNNING = "1";
                            switch (_dal.LOTNO)
                            {
                                case "A": _dal.LOTNO = "B"; break;
                                case "B": _dal.LOTNO = "C"; break;
                                case "C": _dal.LOTNO = "D"; break;
                                case "D": _dal.LOTNO = "E"; break;
                                case "E": _dal.LOTNO = "F"; break;
                                case "F": _dal.LOTNO = "G"; break;
                                case "G": _dal.LOTNO = "H"; break;
                                case "H": _dal.LOTNO = "I"; break;
                                case "I": _dal.LOTNO = "J"; break;
                                case "J": _dal.LOTNO = "K"; break;
                                case "K": _dal.LOTNO = "L"; break;
                                case "L": _dal.LOTNO = "M"; break;
                                case "M": _dal.LOTNO = "N"; break;
                                case "N": _dal.LOTNO = "O"; break;
                                case "O": _dal.LOTNO = "P"; break;
                                case "P": _dal.LOTNO = "Q"; break;
                                case "Q": _dal.LOTNO = "R"; break;
                                case "R": _dal.LOTNO = "S"; break;
                                case "S": _dal.LOTNO = "T"; break;
                                case "T": _dal.LOTNO = "U"; break;
                                case "U": _dal.LOTNO = "V"; break;
                                case "V": _dal.LOTNO = "W"; break;
                                case "W": _dal.LOTNO = "X"; break;
                                case "X": _dal.LOTNO = "Y"; break;
                                case "Y": _dal.LOTNO = "Z"; break;
                                case "Z": _dal.LOTNO = "A"; break;
                                default: _dal.LOTNO = "A"; break;
                            }
                        }
                        else
                        {
                            _dal.RUNNING = (Convert.ToInt32(_dal.RUNNING == "" ? "0" : _dal.RUNNING) + 1).ToString().Trim();
                        }
                    }
                    else
                    {
                        _dal.LOTNO = "A";
                        _dal.RUNNING = "1";
                        _dal.YEAR = (DateTime.Now.Year + 543).ToString().Substring(2);
                    }

                    if (!_dal.UpdateCurrentData(userID, zTrans))
                    {
                        if (LetClose)
                        {
                            zTrans.Commit();
                            zConn.Close();
                        }
                        throw new ApplicationException(_dal.ErrorMessage);
                    }
                    else
                    {
                        code = (_dal.CODE.Length > 3 ? _dal.CODE.Substring(0, 3) : _dal.CODE) + " " + _dal.LOTNO + _dal.RUNNING + _dal.YEAR;
                    }
                }
            }


            if (LetClose)
            {
                zTrans.Commit();
                zConn.Close();
            }

            return code.ToUpper();
        }

        #endregion

    }
}