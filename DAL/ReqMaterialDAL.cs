using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace ABB.DAL
{
    public class ReqMaterialDAL
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

        private string tableName = "REQMATERIAL";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _REQUISITION = 0;
        double _PRODUCT = 0;
        double _MASTER = 0;
        double _USEQTY = 0;
        double _WASTEQTYMAT = 0;
        string _WASTEQTYMAN = "";
        double _RETURNQTY = 0;
        double _CHANGEQTY = 0;
        double _UNIT = 0;
        string _ACTIVE = "";
        string _REMARK = "";
        double _YIELDMAT = 0;
        double _YIELDMAM = 0;
        double _BOM = 0;
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
        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public double MASTER
        {
            get { return _MASTER; }
            set { _MASTER = value; }
        }
        public double USEQTY
        {
            get { return _USEQTY; }
            set { _USEQTY = value; }
        }
        public double WASTEQTYMAT
        {
            get { return _WASTEQTYMAT; }
            set { _WASTEQTYMAT = value; }
        }
        public string WASTEQTYMAN
        {
            get { return _WASTEQTYMAN; }
            set { _WASTEQTYMAN = value; }
        }
        public double RETURNQTY
        {
            get { return _RETURNQTY; }
            set { _RETURNQTY = value; }
        }
        public double CHANGEQTY
        {
            get { return _CHANGEQTY; }
            set { _CHANGEQTY = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public double YIELDMAT
        {
            get { return _YIELDMAT; }
            set { _YIELDMAT = value; }
        }
        public double YIELDMAM
        {
            get { return _YIELDMAM; }
            set { _YIELDMAM = value; }
        }
        public double BOM
        {
            get { return _BOM; }
            set { _BOM = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,REQUISITION,PRODUCT,MASTER,USEQTY,WASTEQTYMAT,WASTEQTYMAN,RETURNQTY,CHANGEQTY,UNIT,ACTIVE,REMARK,YIELDMAT,YIELDMAM,BOM)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += "  " + _REQUISITION.ToString() + ",";// REQUISITION";
                sqlz += "  " + _PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += "  " + _MASTER.ToString() + ",";// MASTER";
                sqlz += "  " + _USEQTY.ToString() + ",";// USEQTY";
                sqlz += "  " + _WASTEQTYMAT.ToString() + ",";// WASTEQTYMAT";
                sqlz += " '" + OracleDB.QRText(_WASTEQTYMAN) + "',";// WASTEQTYMAN";
                sqlz += "  " + _RETURNQTY.ToString() + ",";// RETURNQTY";
                sqlz += "  " + _CHANGEQTY.ToString() + ",";// CHANGEQTY";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += "  " + _YIELDMAT.ToString() + ",";// YIELDMAT";
                sqlz += "  " + _YIELDMAM.ToString() + ",";// YIELDMAM";
                sqlz += "  " + _BOM.ToString() + "";// BOM";
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
                sqlz += " REQUISITION  = " + _REQUISITION.ToString() + ", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString() + ", ";
                sqlz += " MASTER  = " + _MASTER.ToString() + ", ";
                sqlz += " USEQTY  = " + _USEQTY.ToString() + ", ";
                sqlz += " WASTEQTYMAT  = " + _WASTEQTYMAT.ToString() + ", ";
                sqlz += " WASTEQTYMAN  = '" + OracleDB.QRText(_WASTEQTYMAN) + "', ";
                sqlz += " RETURNQTY  = " + _RETURNQTY.ToString() + ", ";
                sqlz += " CHANGEQTY  = " + _CHANGEQTY.ToString() + ", ";
                sqlz += " UNIT  = " + _UNIT.ToString() + ", ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " YIELDMAT  = " + _YIELDMAT.ToString() + ", ";
                sqlz += " YIELDMAM  = " + _YIELDMAM.ToString() + ", ";
                sqlz += " BOM  = " + _BOM.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["REQUISITION"])) _REQUISITION = Convert.ToDouble(zRdr["REQUISITION"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["MASTER"])) _MASTER = Convert.ToDouble(zRdr["MASTER"]);
                        if (!Convert.IsDBNull(zRdr["USEQTY"])) _USEQTY = Convert.ToDouble(zRdr["USEQTY"]);
                        if (!Convert.IsDBNull(zRdr["WASTEQTYMAT"])) _WASTEQTYMAT = Convert.ToDouble(zRdr["WASTEQTYMAT"]);
                        if (!Convert.IsDBNull(zRdr["WASTEQTYMAN"])) _WASTEQTYMAN = zRdr["WASTEQTYMAN"].ToString();
                        if (!Convert.IsDBNull(zRdr["RETURNQTY"])) _RETURNQTY = Convert.ToDouble(zRdr["RETURNQTY"]);
                        if (!Convert.IsDBNull(zRdr["CHANGEQTY"])) _CHANGEQTY = Convert.ToDouble(zRdr["CHANGEQTY"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["YIELDMAT"])) _YIELDMAT = Convert.ToDouble(zRdr["YIELDMAT"]);
                        if (!Convert.IsDBNull(zRdr["YIELDMAM"])) _YIELDMAM = Convert.ToDouble(zRdr["YIELDMAM"]);
                        if (!Convert.IsDBNull(zRdr["BOM"])) _BOM = Convert.ToDouble(zRdr["BOM"]);
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

        public bool DeleteDataByRequisition(double requisition, OracleTransaction trans)
        {
            return doDelete("REQUISITION = " + requisition.ToString(), trans);
        }

        public DataTable GetDataByRequisition(double requisition, OracleTransaction zTrans)
        {
            return GetDataList("WHERE REQUISITION = " + requisition, zTrans);
        }

        public bool UpdateStatusByRequisition(double requisition, string status, string userID, OracleTransaction zTrans)
        {
            bool ret = true;
            string sql = "UPDATE " + TableName + " SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
            sql += "WHERE REQUISITION = " + requisition.ToString() + " ";
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
    }
}
