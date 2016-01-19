using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace ABB.DAL.Inventory
{
    public class PopupProductSearchDAL
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
            return doGetdata("PPD.LOID = " + zLOID.ToString() + " ", zTrans);
        }
        public bool GetDataByCODE(string zcode, OracleTransaction zTrans)
        {
            return doGetdataInv("LOTNO = '" + zcode.ToString() + "' ", zTrans);
        }
        public bool GetDataByLOTNO(string code, OracleTransaction zTrans)
        {
            return doGetdata("PPD.LOTNO = '" + code.ToString() + "'", zTrans);
        }
        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteCurrentData(OracleTransaction zTrans)
        {
            return doDelete("PPD.LOTNO = " + _LOID.ToString() + " ", zTrans);
        }

        /// <summary>
        /// Get Data List of This Table
        /// </summary>
        /// <param name="whereCause"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public DataTable GetDataList(string whereCause, OracleTransaction zTrans)
        {
            whereCause += (whereCause == "" ? "" : " AND ") + " PPD.LOTNO IS NOT NULL AND PPD.PRODSTATUS = 'AP'  AND SII.LOTNO IS NULL";
            return OracleDB.ExecListCmd(sql_select + (whereCause == "" ? "" : " WHERE ") + whereCause + " ORDER BY PD.NAME, PPD.LOTNO, PPD.MFGDATE ");
        }

        #endregion

        #region Constant

        private string tableName = "PDPRODUCT";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        double _PRODUCT = 0;
        double _UNIT = 0;
        string _PRODUCTNAME = "";
        string _LOTNO = "";
        DateTime _MFGDATE = new DateTime(1, 1, 1);
        string _UNITNAME = "";
        double _PDQTY = 0;
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
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }
        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }
        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }
        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }
        public DateTime MFGDATE
        {
            get { return _MFGDATE; }
            set { _MFGDATE = value; }
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
                string sqlz = "INSERT INTO " + tableName + " (LOID, LOTNO, MFGDATE) VALUES (";
                sqlz += "  " + LOID.ToString() + ",";
                sqlz += " '" + OracleDB.QRText(LOTNO) + "',";
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
                sqlz += " LOID  = " + _LOID.ToString() + ", ";
                sqlz += " LOTNO  = '" + OracleDB.QRText(_LOTNO) + "', ";
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
                //string sqlz = "SELECT * FROM v_product_pdproduct ";
                string sqlz = "SELECT PPD.LOID LOID,PD.LOID PRODUCTLOID, PD.NAME PRODUCTNAME,PPD.LOTNO,PPD.MFGDATE,PPD.SENDFGQTY PDQTY,U.LOID UNITLOID,PPD.LOID PDPLOID,U.NAME UNITNAME,PO.SUPPLIER,PPD.TOWAREHOUSE ";
                sqlz += "FROM PDPRODUCT PPD INNER JOIN PRODUCT PD ON PPD.PRODUCT=PD.LOID ";
                sqlz += "INNER JOIN UNIT U ON PD.UNIT=U.LOID ";
                sqlz += "INNER JOIN PDORDER PO ON PPD.PDORDER=PO.LOID ";
                sqlz += "LEFT JOIN (STOCKINITEM SII INNER JOIN STOCKIN SI ON SII.STOCKIN = SI.LOID ) ON SII.LOTNO = PPD.LOTNO ";
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
                string tmpWhere = " WHERE " + whText;
                OracleDataReader zRdr = null;
                try
                {
                    zRdr = OracleDB.ExecQueryCmd(sql_select + tmpWhere, zTrans);
                    if (zRdr.Read())
                    {
                        _OnDB = true;
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["LOTNO"])) _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["PDQTY"])) _PDQTY = Convert.ToDouble(zRdr["PDQTY"]);
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
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["LOTNO"])) _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["PDQTY"])) _PDQTY = Convert.ToDouble(zRdr["PDQTY"]);
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


