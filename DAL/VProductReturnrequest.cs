using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data; 

namespace ABB.DAL
{
    public class VProductReturnrequest
    {
        #region Public Method

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

        private string tableName = "V_PRODUCT_RETURNREQUEST";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        string _REMARK = "";
        string _ISREFUND = "";
        string _RUNNING = "";
        string _YEAR = "";
        string _LOTNO = "";
        double _LOID = 0;
        double _CULOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _BARCODE = "";
        string _ABBNAME = "";
        string _NAME = "";
        double _PRODUCTGROUP = 0;
        double _UNIT = 0;
        double _COST = 0;
        double _PRICE = 0;
        double _STDPRICE = 0;
        string _ISDISCOUNT = "";
        string _ISVAT = "";
        string _ORDERTYPE = "";
        string _UNAME = "";
        double _LOTSIZE = 0;
        double _PACKSIZE = 0;
        double _PACKSIZEUNIT = 0;
        string _PRODUCTLINE = "";
        double _LEADTIME = 0;
        double _QTY = 0;
        string _ACTIVE = "";
        string _REGISNO = "";
        string _ISEDIT = "";
        string _CODE = "";
        string _CUCODE = "";
        string _RTNAME = "";
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
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public string ISREFUND
        {
            get { return _ISREFUND; }
            set { _ISREFUND = value; }
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
        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }
        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public double CULOID
        {
            get { return _CULOID; }
            set { _CULOID = value; }
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
        public string UNAME
        {
            get { return _UNAME; }
            set { _UNAME = value; }
        }
        public double LOTSIZE
        {
            get { return _LOTSIZE; }
            set { _LOTSIZE = value; }
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
        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
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
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public string CUCODE
        {
            get { return _CUCODE; }
            set { _CUCODE = value; }
        }
        public string RTNAME
        {
            get { return _RTNAME; }
            set { _RTNAME = value; }
        }
        #endregion

        #region Query String

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
                        //if (!Convert.IsDBNull(zRdr["RUNNING"])) _RUNNING = zRdr["RUNNING"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        //if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
                        //if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        //if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        //if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["BARCODE"])) _BARCODE = zRdr["BARCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["COST"])) _COST = Convert.ToDouble(zRdr["COST"]);
                        if (!Convert.IsDBNull(zRdr["QTY"])) _QTY = Convert.ToDouble(zRdr["QTY"]);
                        if (!Convert.IsDBNull(zRdr["PRICE"])) _PRICE = Convert.ToDouble(zRdr["PRICE"]);
                        if (!Convert.IsDBNull(zRdr["ACTIVE"])) _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["UNAME"])) _UNAME = zRdr["UNAME"].ToString();
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
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByBarCode(string barCode, OracleTransaction zTrans)
        {
            return doGetdata(" BARCODE = '" + barCode + "' ", zTrans);
        }

        /// <summary>
        /// Get Data List of This Table
        /// </summary>
        /// <param name="whereCause"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public DataTable GetDataList(string whereCause, string orderByField, OracleTransaction zTrans)
        {
            return OracleDB.ExecListCmd(sql_select + whereCause + (orderByField == "" ? "" : " ORDER BY " + orderByField + " "));
        }

        public DataTable GetProductList(string whereCause)
        {
            string sqlz = "SELECT * FROM (SELECT P.LOID,P.CODE,P.BARCODE,P.NAME NAME,PG.PRODUCTTYPE,P.PRODUCTGROUP,PT.NAME PRODUCTTYPENAME,PT.TYPE, ";
            sqlz += "PG.NAME PRODUCTGROUPNAME,P.LOTSIZE,P.COST,P.PRICE,U.NAME UNIT FROM PRODUCT P INNER JOIN PRODUCTGROUP PG ON P.PRODUCTGROUP = PG.LOID ";
            sqlz += "INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID INNER JOIN UNIT U ON P.UNIT = U.LOID)A ";

            return OracleDB.ExecListCmd(sqlz + whereCause);
        }

        public bool CheckCode(double loid, string code)
        {
            string sql = "SELECT * FROM V_PRODUCT_RETURNREQUEST WHERE CODE = '" + code + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckName(double loid, string name)
        {
            string sql = "SELECT * FROM V_PRODUCT_RETURNREQUEST WHERE NAME = '" + name + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckBarcode(double loid, string barcode)
        {
            string sql = "SELECT * FROM V_PRODUCT_RETURNREQUEST WHERE BARCODE = '" + barcode + "' AND LOID <> " + loid + " ";
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

            ProductDAL _dal = new ProductDAL();
            if (_dal.GetDataByLOID(productLOID, zTrans))
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
            else
            {
                if (LetClose)
                {
                    zTrans.Commit();
                    zConn.Close();
                }
                throw new ApplicationException("ไม่สามารถอ่านค่า running สำหรับ lotno ได้");
            }

            if (LetClose)
            {
                zTrans.Commit();
                zConn.Close();
            }

            return code;
        }

        #endregion


    }
}

