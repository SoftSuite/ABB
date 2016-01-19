using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace ABB.DAL
{
    public class ProductSearchDAL
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
        /// <param name="zID"></param>
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

        /// <summary>
        /// Get Data List of This Table
        /// </summary>
        /// <param name="whereCause"></param>
        /// <param name="sortField"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public DataTable GetDataList(string whereCause, string sortField, OracleTransaction zTrans)
        {
            return OracleDB.ExecListCmd(sql_select + whereCause + (sortField == "" ? "" : "ORDER BY " + sortField));
        }

        #endregion

        #region Constant

        private string tableName = "PRODUCT";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CODE = "";
        string _BARCODE = "";
        string _NAME = "";
        string _ABBNAME = "";
        double _PRODUCTGROUP = 0;
        double _PRODUCTTYPE = 0;
        double _UNIT = 0;
        double _COST = 0;
        double _PRICE = 0;
        double _STDPRICE = 0;
        string _ISDISCOUNT = "";
        string _ISVAT = "";
        string _ORDERTYPE = "";
        double _LOTSIZE = 0;
        double _LEADTIME = 0;
        string _ACTIVE = "";
        string _REGISNO = "";
        string _ISEDIT = "";
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
        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public string ABBNAME
        {
            get { return _ABBNAME; }
            set { _ABBNAME = value; }
        }
        public double PRODUCTGROUP
        {
            get { return _PRODUCTGROUP; }
            set { _PRODUCTGROUP = value; }
        }
        public double PRODUCTTYPE
        {
            get { return _PRODUCTTYPE; }
            set { _PRODUCTTYPE = value; }
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
        public string ORDERTYPE
        {
            get { return _ORDERTYPE; }
            set { _ORDERTYPE = value; }
        }
        public double LOTSIZE
        {
            get { return _LOTSIZE; }
            set { _LOTSIZE = value; }
        }
        public double LEADTIME
        {
            get { return _LEADTIME; }
            set { _LEADTIME = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public string REGISNO
        {
            get { return _REGISNO; }
            set { _REGISNO = value; }
        }
        public string ISEDIT
        {
            get { return _ISEDIT; }
            set { _ISEDIT = value; }
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
                string sqlz = "INSERT INTO " + tableName + " (LOID,CODE, BARCODE, NAME, ABBNAME, PRODUCTGROUP, UNIT, COST, PRICE, STDPRICE, ISDISCOUNT, ISVAT ";
                sqlz += ", ORDERTYPE, LOTSIZE, LEADTIME, ACTIVE, REGISNO, ISEDIT, CREATEBY, CREATEON) VALUES (";
                sqlz += "  " + _LOID.ToString() + ",";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";
                sqlz += " '" + OracleDB.QRText(_BARCODE) + "',";
                sqlz += " '" + OracleDB.QRText(_NAME) + "',";
                sqlz += " '" + OracleDB.QRText(_ABBNAME) + "',";
                sqlz += " " + OracleDB.QRText(_PRODUCTGROUP.ToString()) + ",";
                sqlz += " " + OracleDB.QRText(_UNIT.ToString()) + ",";
                sqlz += " " + OracleDB.QRText(_COST.ToString()) + ",";
                sqlz += " " + OracleDB.QRText(_PRICE.ToString()) + ",";
                sqlz += " " + OracleDB.QRText(_STDPRICE.ToString()) + ",";
                sqlz += " '" + OracleDB.QRText(_ISDISCOUNT) + "',";
                sqlz += " '" + OracleDB.QRText(_ISVAT) + "',";
                sqlz += " '" + OracleDB.QRText(_ORDERTYPE) + "',";
                sqlz += " " + OracleDB.QRText(_LOTSIZE.ToString()) + ",";
                sqlz += " " + OracleDB.QRText(_LEADTIME.ToString()) + ",";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";
                sqlz += " '" + OracleDB.QRText(_REGISNO) + "',";
                sqlz += " '" + OracleDB.QRText(_ISEDIT) + "',";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + " ";
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
                sqlz += " BARCODE  = '" + OracleDB.QRText(_BARCODE) + "', ";
                sqlz += " NAME  = '" + OracleDB.QRText(_NAME) + "', ";
                sqlz += " ABBNAME  = '" + OracleDB.QRText(_ABBNAME) + "', ";
                sqlz += " PRODUCTGROUP  = " + PRODUCTGROUP.ToString() + ", ";
                sqlz += " UNIT  = " + OracleDB.QRText(_UNIT.ToString()) + ", ";
                sqlz += " COST  = " + OracleDB.QRText(_COST.ToString()) + ", ";
                sqlz += " PRICE  = " + OracleDB.QRText(_PRICE.ToString()) + ", ";
                sqlz += " STDPRICE  = " + OracleDB.QRText(_STDPRICE.ToString()) + ", ";
                sqlz += " ISDISCOUNT  = '" + OracleDB.QRText(ISDISCOUNT) + "', ";
                sqlz += " ISVAT  = '" + OracleDB.QRText(ISVAT) + "', ";
                sqlz += " ORDERTYPE  = '" + OracleDB.QRText(ORDERTYPE) + "', ";
                sqlz += " LOTSIZE  = " + OracleDB.QRText(_LOTSIZE.ToString()) + ", ";
                sqlz += " LEADTIME  = " + OracleDB.QRText(_LEADTIME.ToString()) + ", ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(ACTIVE) + "', ";
                sqlz += " REGISNO  = '" + OracleDB.QRText(REGISNO) + "', ";
                sqlz += " ISEDIT  = '" + OracleDB.QRText(ISEDIT) + "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDate(_UPDATEON) + " ";
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
                string sqlz = "SELECT PD.LOID,PD.CODE,PD.NAME,PD.ABBNAME,PD.BARCODE,PD.LOTSIZE,PD.COST,PD.PRICE,UN.NAME AS UNIT,PG.NAME AS PRODUCTGROUP,PT.NAME AS PRODUCTTYPE ";
                sqlz += "FROM PRODUCT PD INNER JOIN UNIT UN ON PD.UNIT = UN.LOID ";
                sqlz += "INNER JOIN PRODUCTGROUP PG ON PD.PRODUCTGROUP = PG.LOID INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID ";
                return sqlz;
            }
        }

        private string sql_select2
        {
            get
            {
                string sqlz = " SELECT PD.*,PG.PRODUCTTYPE AS PRODUCTTYPE FROM PRODUCT PD INNER JOIN PRODUCTGROUP PG ON PD.PRODUCTGROUP = PG.LOID";
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
                string tmpWhere = " WHERE PD." + whText;
                OracleDataReader zRdr = null;
                try
                {
                    zRdr = OracleDB.ExecQueryCmd(sql_select2 + tmpWhere, zTrans);
                    if (zRdr.Read())
                    {
                        _OnDB = true;
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["BARCODE"])) _BARCODE = zRdr["BARCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["ABBNAME"])) _ABBNAME = zRdr["ABBNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCTGROUP"])) _PRODUCTGROUP = Convert.ToDouble(zRdr["PRODUCTGROUP"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCTTYPE"])) _PRODUCTTYPE = Convert.ToDouble(zRdr["PRODUCTTYPE"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["COST"])) _COST = Convert.ToDouble(zRdr["COST"]);
                        if (!Convert.IsDBNull(zRdr["PRICE"])) _PRICE = Convert.ToDouble(zRdr["PRICE"]);
                        if (!Convert.IsDBNull(zRdr["STDPRICE"])) _STDPRICE = Convert.ToDouble(zRdr["STDPRICE"]);
                        if (!Convert.IsDBNull(zRdr["ISDISCOUNT"])) _ISDISCOUNT = zRdr["ISDISCOUNT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISVAT"])) _ISVAT = zRdr["ISVAT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ORDERTYPE"])) _ORDERTYPE = zRdr["ORDERTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOTSIZE"])) _LOTSIZE = Convert.ToDouble(zRdr["LOTSIZE"]);
                        if (!Convert.IsDBNull(zRdr["LEADTIME"])) _LEADTIME = Convert.ToDouble(zRdr["LEADTIME"]);
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REGISNO"])) _REGISNO = zRdr["REGISNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISEDIT"])) _ISEDIT = zRdr["ISEDIT"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
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
