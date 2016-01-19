using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL
{
    public class ProductMasterDAL
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

        private string tableName = "PRODUCTMASTER";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        string _NAME = "";
        double _PRODUCTGROUP = 0;
        double _UNIT = 0;
        string _ORDERTYPE = "";
        double _LOTSIZE = 0;
        string _PRODUCTLINE = "";
        double _LEADTIME = 0;
        string _ACTIVE = "";
        string _REGISNO = "";
        string _REMARK = "";
        string _LOTNO = "";
        string _RUNNING = "";
        string _YEAR = "";
        double _LOTSIZEPD = 0;
        double _LEADTIMEPD = 0;
        double _AGE = 0;
        string _ENAME = "";
        double _PRODUCEGROUP = 0;
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
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public double PRODUCTGROUP
        {
            get { return _PRODUCTGROUP; }
            set { _PRODUCTGROUP = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
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
        public string PRODUCTLINE
        {
            get { return _PRODUCTLINE; }
            set { _PRODUCTLINE = value; }
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
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }
        public string RUNNING
        {
            get { return _RUNNING; }
            set { _RUNNING = value; }
        }
        public string YEAR
        {
            get { return _YEAR; }
            set { _YEAR = value; }
        }
        public double LOTSIZEPD
        {
            get { return _LOTSIZEPD; }
            set { _LOTSIZEPD = value; }
        }
        public double LEADTIMEPD
        {
            get { return _LEADTIMEPD; }
            set { _LEADTIMEPD = value; }
        }
        public double AGE
        {
            get { return _AGE; }
            set { _AGE = value; }
        }
        public string ENAME
        {
            get { return _ENAME; }
            set { _ENAME = value; }
        }
        public double PRODUCEGROUP
        {
            get { return _PRODUCEGROUP; }
            set { _PRODUCEGROUP = value; }
        }
        #endregion

        #region Query String
        private string sql_insert
        {
            get
            {
                string sqlz = "INSERT INTO " + tableName + " (LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,CODE,NAME,PRODUCTGROUP,UNIT,ORDERTYPE,LOTSIZE,PRODUCTLINE,LEADTIME,ACTIVE,REGISNO,REMARK,LOTNO,RUNNING,YEAR,LOTSIZEPD,LEADTIMEPD,AGE,ENAME,PRODUCEGROUP)VALUES(";
                sqlz += "  " + _LOID.ToString() + ",";// LOID";
                sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";// CREATEBY";
                sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";// CREATEON";
                sqlz += " '" + OracleDB.QRText(_UPDATEBY) + "',";// UPDATEBY";
                sqlz += " " + OracleDB.QRDateTime(_UPDATEON) + ",";// UPDATEON";
                sqlz += " '" + OracleDB.QRText(_CODE) + "',";// CODE";
                sqlz += " '" + OracleDB.QRText(_NAME) + "',";// NAME";
                sqlz += "  " + _PRODUCTGROUP.ToString() + ",";// PRODUCTGROUP";
                sqlz += "  " + _UNIT.ToString() + ",";// UNIT";
                sqlz += " '" + OracleDB.QRText(_ORDERTYPE) + "',";// ORDERTYPE";
                sqlz += "  " + _LOTSIZE.ToString() + ",";// LOTSIZE";
                sqlz += " '" + OracleDB.QRText(_PRODUCTLINE) + "',";// PRODUCTLINE";
                sqlz += "  " + _LEADTIME.ToString() + ",";// LEADTIME";
                sqlz += " '" + OracleDB.QRText(_ACTIVE) + "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_REGISNO) + "',";// REGISNO";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "',";// REMARK";
                sqlz += " '" + OracleDB.QRText(_LOTNO) + "',";// LOTNO";
                sqlz += " '" + OracleDB.QRText(_RUNNING) + "',";// RUNNING";
                sqlz += " '" + OracleDB.QRText(_YEAR) + "',";// YEAR";
                sqlz += "  " + _LOTSIZEPD.ToString() + ",";// LOTSIZEPD";
                sqlz += "  " + _LEADTIMEPD.ToString() + ",";// LEADTIMEPD";
                sqlz += "  " + _AGE.ToString() + ",";// AGE";
                sqlz += " '" + OracleDB.QRText(_ENAME) + "',";// ENAME";
                sqlz += "  " + _PRODUCEGROUP.ToString() + "";// PRODUCEGROUP";
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
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                sqlz += " NAME  = '" + OracleDB.QRText(_NAME) + "', ";
                sqlz += " PRODUCTGROUP  = " + _PRODUCTGROUP.ToString() + ", ";
                sqlz += " UNIT  = " + _UNIT.ToString() + ", ";
                sqlz += " ORDERTYPE  = '" + OracleDB.QRText(_ORDERTYPE) + "', ";
                sqlz += " LOTSIZE  = " + _LOTSIZE.ToString() + ", ";
                sqlz += " PRODUCTLINE  = '" + OracleDB.QRText(_PRODUCTLINE) + "', ";
                sqlz += " LEADTIME  = " + _LEADTIME.ToString() + ", ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE) + "', ";
                sqlz += " REGISNO  = '" + OracleDB.QRText(_REGISNO) + "', ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "', ";
                sqlz += " LOTNO  = '" + OracleDB.QRText(_LOTNO) + "', ";
                sqlz += " RUNNING  = '" + OracleDB.QRText(_RUNNING) + "', ";
                sqlz += " YEAR  = '" + OracleDB.QRText(_YEAR) + "', ";
                sqlz += " LOTSIZEPD  = " + _LOTSIZEPD.ToString() + ", ";
                sqlz += " LEADTIMEPD  = " + _LEADTIMEPD.ToString() + ", ";
                sqlz += " AGE  = " + _AGE.ToString() + ", ";
                sqlz += " ENAME  = '" + OracleDB.QRText(_ENAME) + "', ";
                sqlz += " PRODUCEGROUP  = " + _PRODUCEGROUP.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCTGROUP"])) _PRODUCTGROUP = Convert.ToDouble(zRdr["PRODUCTGROUP"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["ORDERTYPE"])) _ORDERTYPE = zRdr["ORDERTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOTSIZE"])) _LOTSIZE = Convert.ToDouble(zRdr["LOTSIZE"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCTLINE"])) _PRODUCTLINE = zRdr["PRODUCTLINE"].ToString();
                        if (!Convert.IsDBNull(zRdr["LEADTIME"])) _LEADTIME = Convert.ToDouble(zRdr["LEADTIME"]);
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REGISNO"])) _REGISNO = zRdr["REGISNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["REMARK"])) _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOTNO"])) _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["RUNNING"])) _RUNNING = zRdr["RUNNING"].ToString();
                        if (!Convert.IsDBNull(zRdr["YEAR"])) _YEAR = zRdr["YEAR"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOTSIZEPD"])) _LOTSIZEPD = Convert.ToDouble(zRdr["LOTSIZEPD"]);
                        if (!Convert.IsDBNull(zRdr["LEADTIMEPD"])) _LEADTIMEPD = Convert.ToDouble(zRdr["LEADTIMEPD"]);
                        if (!Convert.IsDBNull(zRdr["AGE"])) _AGE = Convert.ToDouble(zRdr["AGE"]);
                        if (!Convert.IsDBNull(zRdr["ENAME"])) _ENAME = zRdr["ENAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCEGROUP"])) _PRODUCEGROUP = Convert.ToDouble(zRdr["PRODUCEGROUP"]);
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

        public bool CheckName(double loid, string name)
        {
            string sql = "SELECT * FROM PRODUCTMASTER WHERE UPPER(NAME) = '" + name.ToUpper() + "' ";

            if (loid != 0)
                sql += " AND LOID <> " + loid + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckEName(double loid, string ename)
        {
            string sql = "SELECT * FROM PRODUCTMASTER WHERE UPPER(ENAME) = '" + ename.ToUpper() + "' ";

            if (loid != 0)
                sql += " AND LOID <> " + loid + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckCode(double loid, string code)
        {
            string sql = "SELECT * FROM PRODUCTMASTER WHERE UPPER(CODE) = '" + code.ToUpper() + "' ";

            if (loid != 0)
                sql += " AND LOID <> " + loid + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        //#region Lotno Work
        ///// <summary>
        ///// Get running code with transaction
        ///// </summary>
        ///// <param name="RunningName"></param>
        ///// <param name="RunningItem"></param>
        ///// <returns></returns>
        //public static string GetLotNo(string userID, double productLOID) { return GetLotNo(userID, productLOID, null); }
        //public static string GetLotNo(string userID, double productLOID, OracleTransaction zTrans)
        //{
        //    bool LetClose = false;
        //    string code = "";
        //    OracleConnection zConn = null;
        //    if (zTrans == null)
        //    {
        //        LetClose = true;
        //        zConn = OracleDB.GetConnection();
        //        zTrans = zConn.BeginTransaction(IsolationLevel.ReadCommitted);
        //    }

        //    ProductMasterDAL _dal = new ProductMasterDAL();
        //    if (_dal.GetDataByLOID(productLOID, null))
        //    {
        //        if (_dal.YEAR == (DateTime.Now.Year + 543).ToString().Substring(2))
        //        {
        //            if (_dal.RUNNING == "9")
        //            {
        //                _dal.RUNNING = "1";
        //                switch (_dal.LOTNO)
        //                {
        //                    case "A": _dal.LOTNO = "B"; break;
        //                    case "B": _dal.LOTNO = "C"; break;
        //                    case "C": _dal.LOTNO = "D"; break;
        //                    case "D": _dal.LOTNO = "E"; break;
        //                    case "E": _dal.LOTNO = "F"; break;
        //                    case "F": _dal.LOTNO = "G"; break;
        //                    case "G": _dal.LOTNO = "H"; break;
        //                    case "H": _dal.LOTNO = "I"; break;
        //                    case "I": _dal.LOTNO = "J"; break;
        //                    case "J": _dal.LOTNO = "K"; break;
        //                    case "K": _dal.LOTNO = "L"; break;
        //                    case "L": _dal.LOTNO = "M"; break;
        //                    case "M": _dal.LOTNO = "N"; break;
        //                    case "N": _dal.LOTNO = "O"; break;
        //                    case "O": _dal.LOTNO = "P"; break;
        //                    case "P": _dal.LOTNO = "Q"; break;
        //                    case "Q": _dal.LOTNO = "R"; break;
        //                    case "R": _dal.LOTNO = "S"; break;
        //                    case "S": _dal.LOTNO = "T"; break;
        //                    case "T": _dal.LOTNO = "U"; break;
        //                    case "U": _dal.LOTNO = "V"; break;
        //                    case "V": _dal.LOTNO = "W"; break;
        //                    case "W": _dal.LOTNO = "X"; break;
        //                    case "X": _dal.LOTNO = "Y"; break;
        //                    case "Y": _dal.LOTNO = "Z"; break;
        //                    case "Z": _dal.LOTNO = "A"; break;
        //                    default: _dal.LOTNO = "A"; break;
        //                }
        //            }
        //            else
        //            {
        //                _dal.RUNNING = (Convert.ToInt32(_dal.RUNNING == "" ? "0" : _dal.RUNNING) + 1).ToString().Trim();
        //            }
        //        }
        //        else
        //        {
        //            _dal.LOTNO = "A";
        //            _dal.RUNNING = "1";
        //            _dal.YEAR = (DateTime.Now.Year + 543).ToString().Substring(2);
        //        }

        //        ProductMasterDAL odal = new ProductMasterDAL();
        //        odal.GetDataByLOID(_dal.PRODUCTMASTER, null);
        //        odal.LOTNO = _dal.LOTNO;
        //        odal.RUNNING = _dal.RUNNING;
        //        odal.YEAR = _dal.YEAR;

        //        if (!odal.UpdateCurrentData(userID, zTrans))
        //        {
        //            if (LetClose)
        //            {
        //                zTrans.Commit();
        //                zConn.Close();
        //            }
        //            throw new ApplicationException(odal.ErrorMessage);
        //        }
        //        else
        //        {
        //            code = (_dal.CODE.Length > 3 ? _dal.CODE.Substring(0, 3) : _dal.CODE) + " " + _dal.LOTNO + _dal.RUNNING + _dal.YEAR;
        //        }
        //    }
        //    else
        //    {
        //        if (LetClose)
        //        {
        //            zTrans.Commit();
        //            zConn.Close();
        //        }
        //        throw new ApplicationException("ไม่สามารถอ่านค่า running สำหรับ lotno ได้");
        //    }

        //    if (LetClose)
        //    {
        //        zTrans.Commit();
        //        zConn.Close();
        //    }

        //    return code;
        //}

        //#endregion
    }
}