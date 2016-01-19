using System; 
using System.Collections.Generic; 
using System.Text; 
using System.Data; 
using System.Data.OracleClient; 
using ABB.Data; 

namespace ABB.DAL 
{ 
    public class ProductDAL 
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

        private string tableName = "PRODUCT";

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
        double _LOTSIZE = 0;
        double _LOTSIZEPD = 0;
        double _PACKSIZE = 0;
        double _PACKSIZEUNIT = 0;
        string _PRODUCTLINE = "";
        double _LEADTIME = 0;
        double _LEADTIMEPD = 0;
        string _ACTIVE = "";
        string _REGISNO = "";
        string _ISEDIT = "";
        string _CODE = "";
        double _PRODUCTMASTER = 0;
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
           set { _REMARK= value; } 
        }
        public string ISREFUND 
        {
           get { return _ISREFUND; } 
           set { _ISREFUND= value; } 
        }
        public string RUNNING 
        {
           get { return _RUNNING; } 
           set { _RUNNING= value; } 
        }
        public string YEAR 
        {
           get { return _YEAR; } 
           set { _YEAR= value; } 
        }
        public string LOTNO 
        {
           get { return _LOTNO; } 
           set { _LOTNO= value; } 
        }
        public double LOID 
        {
           get { return _LOID; }
            set { _LOID = value; }
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
           set { _BARCODE= value; } 
        }
        public string ABBNAME 
        {
           get { return _ABBNAME; } 
           set { _ABBNAME= value; } 
        }
        public string NAME 
        {
           get { return _NAME; } 
           set { _NAME= value; } 
        }
        public double PRODUCTGROUP 
        {
           get { return _PRODUCTGROUP; } 
           set { _PRODUCTGROUP= value; } 
        }
        public double UNIT 
        {
           get { return _UNIT; } 
           set { _UNIT= value; } 
        }
        public double COST 
        {
           get { return _COST; } 
           set { _COST= value; } 
        }
        public double PRICE 
        {
           get { return _PRICE; } 
           set { _PRICE= value; } 
        }
        public double STDPRICE 
        {
           get { return _STDPRICE; } 
           set { _STDPRICE= value; } 
        }
        public string ISDISCOUNT 
        {
           get { return _ISDISCOUNT; } 
           set { _ISDISCOUNT= value; } 
        }
        public string ISVAT 
        {
           get { return _ISVAT; } 
           set { _ISVAT= value; } 
        }
        public string ORDERTYPE 
        {
           get { return _ORDERTYPE; } 
           set { _ORDERTYPE= value; } 
        }
        public double LOTSIZE 
        {
           get { return _LOTSIZE; } 
           set { _LOTSIZE= value; } 
        }
        public double LOTSIZEPD
        {
            get { return _LOTSIZEPD; }
            set { _LOTSIZEPD = value; }
        }
        public double PACKSIZE 
        {
           get { return _PACKSIZE; } 
           set { _PACKSIZE= value; } 
        }
        public double PACKSIZEUNIT 
        {
           get { return _PACKSIZEUNIT; } 
           set { _PACKSIZEUNIT= value; } 
        }
        public string PRODUCTLINE 
        {
           get { return _PRODUCTLINE; } 
           set { _PRODUCTLINE= value; } 
        }
        public double LEADTIME 
        {
           get { return _LEADTIME; } 
           set { _LEADTIME= value; } 
        }
        public double LEADTIMEPD
        {
            get { return _LEADTIMEPD; }
            set { _LEADTIMEPD = value; }
        }
        public string ACTIVE 
        {
           get { return _ACTIVE; } 
           set { _ACTIVE= value; } 
        }
        public string REGISNO 
        {
           get { return _REGISNO; } 
           set { _REGISNO= value; } 
        }
        public string ISEDIT 
        {
           get { return _ISEDIT; } 
           set { _ISEDIT= value; } 
        }
        public string CODE 
        {
           get { return _CODE; } 
           set { _CODE= value; } 
        }

        public double PRODUCTMASTER
        {
            get { return _PRODUCTMASTER; }
            set { _PRODUCTMASTER = value; }
        }
        #endregion

        #region Query String 
        private string sql_insert 
        { 
           get
           {
               string sqlz = "INSERT INTO " + tableName + " (REMARK,ISREFUND,RUNNING,YEAR,LOTNO,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,BARCODE,ABBNAME,NAME,PRODUCTGROUP,UNIT,COST,PRICE,STDPRICE,ISDISCOUNT,ISVAT,ORDERTYPE,LOTSIZE,LOTSIZEPD,PACKSIZE,PACKSIZEUNIT,PRODUCTLINE,LEADTIME,LEADTIMEPD,ACTIVE,REGISNO,ISEDIT,CODE)VALUES(";
                sqlz += " '"+ OracleDB.QRText(_REMARK)+ "',";// REMARK";
                sqlz += " '"+ OracleDB.QRText(_ISREFUND)+ "',";// ISREFUND";
                sqlz += " '"+ OracleDB.QRText(_RUNNING)+ "',";// RUNNING";
                sqlz += " '"+ OracleDB.QRText(_YEAR)+ "',";// YEAR";
                sqlz += " '"+ OracleDB.QRText(_LOTNO)+ "',";// LOTNO";
                sqlz += "  "+_LOID.ToString() + ",";// LOID";
                sqlz += " '"+ OracleDB.QRText(_CREATEBY)+ "',";// CREATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_CREATEON)+ ",";// CREATEON";
                sqlz += " '"+ OracleDB.QRText(_UPDATEBY)+ "',";// UPDATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_UPDATEON)+ ",";// UPDATEON";
                sqlz += " '"+ OracleDB.QRText(_BARCODE)+ "',";// BARCODE";
                sqlz += " '"+ OracleDB.QRText(_ABBNAME)+ "',";// ABBNAME";
                sqlz += " '"+ OracleDB.QRText(_NAME)+ "',";// NAME";
                sqlz += "  "+_PRODUCTGROUP.ToString() + ",";// PRODUCTGROUP";
                sqlz += "  "+_UNIT.ToString() + ",";// UNIT";
                sqlz += "  "+_COST.ToString() + ",";// COST";
                sqlz += "  "+_PRICE.ToString() + ",";// PRICE";
                sqlz += "  "+_STDPRICE.ToString() + ",";// STDPRICE";
                sqlz += " '"+ OracleDB.QRText(_ISDISCOUNT)+ "',";// ISDISCOUNT";
                sqlz += " '"+ OracleDB.QRText(_ISVAT)+ "',";// ISVAT";
                sqlz += " '"+ OracleDB.QRText(_ORDERTYPE)+ "',";// ORDERTYPE";
                sqlz += "  "+_LOTSIZE.ToString() + ",";// LOTSIZE";
                sqlz += "  "+ _LOTSIZEPD.ToString() + ",";// LOTSIZEPD";
                sqlz += "  "+_PACKSIZE.ToString() + ",";// PACKSIZE";
                sqlz += "  "+_PACKSIZEUNIT.ToString() + ",";// PACKSIZEUNIT";
                sqlz += " '"+ OracleDB.QRText(_PRODUCTLINE)+ "',";// PRODUCTLINE";
                sqlz += "  "+_LEADTIME.ToString() + ",";// LEADTIME";
                sqlz += "  "+ _LEADTIMEPD.ToString() + ",";// LEADTIMEPD";
                sqlz += " '"+ OracleDB.QRText(_ACTIVE)+ "',";// ACTIVE";
                sqlz += " '"+ OracleDB.QRText(_REGISNO)+ "',";// REGISNO";
                sqlz += " '"+ OracleDB.QRText(_ISEDIT)+ "',";// ISEDIT";
                sqlz += " '"+ OracleDB.QRText(_CODE)+ "'";// CODE";
                sqlz += " ) "; 
                return sqlz; 
           } 
        }
        private string sql_update 
        { 
           get
           {
               string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK)+ "', ";
                sqlz += " ISREFUND  = '" + OracleDB.QRText(_ISREFUND)+ "', ";
                sqlz += " RUNNING  = '" + OracleDB.QRText(_RUNNING)+ "', ";
                sqlz += " YEAR  = '" + OracleDB.QRText(_YEAR)+ "', ";
                sqlz += " LOTNO  = '" + OracleDB.QRText(_LOTNO)+ "', ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY)+ "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON)+ ", ";
                sqlz += " BARCODE  = '" + OracleDB.QRText(_BARCODE)+ "', ";
                sqlz += " ABBNAME  = '" + OracleDB.QRText(_ABBNAME)+ "', ";
                sqlz += " NAME  = '" + OracleDB.QRText(_NAME)+ "', ";
                sqlz += " PRODUCTGROUP  = " + _PRODUCTGROUP.ToString()+", ";
                sqlz += " UNIT  = " + _UNIT.ToString()+", ";
                sqlz += " COST  = " + _COST.ToString()+", ";
                sqlz += " PRICE  = " + _PRICE.ToString()+", ";
                sqlz += " STDPRICE  = " + _STDPRICE.ToString()+", ";
                sqlz += " ISDISCOUNT  = '" + OracleDB.QRText(_ISDISCOUNT)+ "', ";
                sqlz += " ISVAT  = '" + OracleDB.QRText(_ISVAT)+ "', ";
                sqlz += " ORDERTYPE  = '" + OracleDB.QRText(_ORDERTYPE)+ "', ";
                sqlz += " LOTSIZE  = " + _LOTSIZE.ToString()+", ";
                sqlz += " LOTSIZEPD  = " + _LOTSIZEPD.ToString() + ", ";
                sqlz += " PACKSIZE  = " + _PACKSIZE.ToString()+", ";
                sqlz += " PACKSIZEUNIT  = " + _PACKSIZEUNIT.ToString()+", ";
                sqlz += " PRODUCTLINE  = '" + OracleDB.QRText(_PRODUCTLINE)+ "', ";
                sqlz += " LEADTIME  = " + _LEADTIME.ToString()+", ";
                sqlz += " LEADTIMEPD  = " + _LEADTIMEPD.ToString() + ", ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE)+ "', ";
                sqlz += " REGISNO  = '" + OracleDB.QRText(_REGISNO)+ "', ";
                sqlz += " ISEDIT  = '" + OracleDB.QRText(_ISEDIT)+ "', ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE)+ "' ";
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
                 zRdr = OracleDB.ExecQueryCmd(sql_select +tmpWhere , zTrans);
                 if (zRdr.Read())
                 {
                     _OnDB = true;
                        if (!Convert.IsDBNull(zRdr["REMARK"]))  _REMARK = zRdr["REMARK"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISREFUND"]))  _ISREFUND = zRdr["ISREFUND"].ToString();
                        if (!Convert.IsDBNull(zRdr["RUNNING"]))  _RUNNING = zRdr["RUNNING"].ToString();
                        if (!Convert.IsDBNull(zRdr["YEAR"]))  _YEAR = zRdr["YEAR"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOTNO"]))  _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOID"]))  _LOID = Convert.ToDouble(zRdr["LOID"]);
                        //if (!Convert.IsDBNull(zRdr["CREATEBY"]))  _CREATEBY = zRdr["CREATEBY"].ToString();
                        //if (!Convert.IsDBNull(zRdr["CREATEON"]))  _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        //if (!Convert.IsDBNull(zRdr["UPDATEBY"]))  _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        //if (!Convert.IsDBNull(zRdr["UPDATEON"]))  _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["BARCODE"]))  _BARCODE = zRdr["BARCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["ABBNAME"]))  _ABBNAME = zRdr["ABBNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["NAME"]))  _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCTGROUP"]))  _PRODUCTGROUP = Convert.ToDouble(zRdr["PRODUCTGROUP"]);
                        if (!Convert.IsDBNull(zRdr["UNIT"]))  _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["COST"]))  _COST = Convert.ToDouble(zRdr["COST"]);
                        if (!Convert.IsDBNull(zRdr["PRICE"]))  _PRICE = Convert.ToDouble(zRdr["PRICE"]);
                        if (!Convert.IsDBNull(zRdr["STDPRICE"]))  _STDPRICE = Convert.ToDouble(zRdr["STDPRICE"]);
                        if (!Convert.IsDBNull(zRdr["ISDISCOUNT"]))  _ISDISCOUNT = zRdr["ISDISCOUNT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISVAT"]))  _ISVAT = zRdr["ISVAT"].ToString();
                        if (!Convert.IsDBNull(zRdr["ORDERTYPE"]))  _ORDERTYPE = zRdr["ORDERTYPE"].ToString();
                        if (!Convert.IsDBNull(zRdr["LOTSIZE"]))  _LOTSIZE = Convert.ToDouble(zRdr["LOTSIZE"]);
                        if (!Convert.IsDBNull(zRdr["LOTSIZEPD"])) _LOTSIZEPD = Convert.ToDouble(zRdr["LOTSIZEPD"]);
                        if (!Convert.IsDBNull(zRdr["PACKSIZE"]))  _PACKSIZE = Convert.ToDouble(zRdr["PACKSIZE"]);
                        if (!Convert.IsDBNull(zRdr["PACKSIZEUNIT"]))  _PACKSIZEUNIT = Convert.ToDouble(zRdr["PACKSIZEUNIT"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCTLINE"]))  _PRODUCTLINE = zRdr["PRODUCTLINE"].ToString();
                        if (!Convert.IsDBNull(zRdr["LEADTIME"]))  _LEADTIME = Convert.ToDouble(zRdr["LEADTIME"]);
                        if (!Convert.IsDBNull(zRdr["LEADTIMEPD"])) _LEADTIMEPD = Convert.ToDouble(zRdr["LEADTIMEPD"]);
                        if (!Convert.IsDBNull(zRdr["ACTIVE"]))  _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REGISNO"]))  _REGISNO = zRdr["REGISNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["ISEDIT"]))  _ISEDIT = zRdr["ISEDIT"].ToString();
                        if (!Convert.IsDBNull(zRdr["CODE"]))  _CODE = zRdr["CODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCTMASTER"])) _PRODUCTMASTER = Convert.ToDouble(zRdr["PRODUCTMASTER"]);
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
            sqlz += "INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID INNER JOIN UNIT U ON P.UNIT = U.LOID ORDER BY UPPER(NAME))A ";

            return OracleDB.ExecListCmd(sqlz + whereCause);
        }

        public bool CheckCode(double loid, string code)
        {
            string sql = "SELECT * FROM PRODUCT WHERE UPPER(CODE) = '" + code.ToUpper() + "' AND LOID <> " + loid + " ";
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
            string sql = "SELECT * FROM PRODUCT WHERE UPPER(NAME) = '" + name.ToUpper() + "' AND LOID <> " + loid + " ";
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
            string sql = "SELECT * FROM PRODUCT WHERE UPPER(BARCODE) = '" + barcode.ToUpper() + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

    }
}
