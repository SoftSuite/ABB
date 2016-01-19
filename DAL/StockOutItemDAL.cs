using System; 
using System.Collections.Generic; 
using System.Text; 
using System.Data; 
using System.Data.OracleClient; 
using ABB.Data; 

namespace ABB.DAL 
{ 
    public class StockOutItemDAL 
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

        private string tableName = "STOCKOUTITEM";

        #endregion
 
        #region Private Variable
        string _error = "";
        bool _OnDB = false; 
        string _STATUS = "";
        double _UNIT = 0;
        double _PRICE = 0;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _STOCKOUT = 0;
        double _PRODUCT = 0;
        string _LOTNO = "";
        double _QTY = 0;
        string _ACTIVE = "";
        string _INVNO = "";
        string _REFTABLE = "";
        double _REFLOID = 0;
        double _REMAIN = 0;
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
        public string STATUS 
        {
           get { return _STATUS; } 
           set { _STATUS= value; } 
        }
        public string INVNO
        {
            get { return _INVNO; }
            set { _INVNO = value; }
        }
        public double UNIT 
        {
           get { return _UNIT; } 
           set { _UNIT= value; } 
        }
        public double PRICE 
        {
           get { return _PRICE; } 
           set { _PRICE= value; } 
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
        public double STOCKOUT 
        {
           get { return _STOCKOUT; } 
           set { _STOCKOUT= value; } 
        }
        public double PRODUCT 
        {
           get { return _PRODUCT; } 
           set { _PRODUCT= value; } 
        }
        public string LOTNO 
        {
           get { return _LOTNO; } 
           set { _LOTNO= value; } 
        }
        public double QTY 
        {
           get { return _QTY; } 
           set { _QTY= value; } 
        }
        public double REMAIN
        {
            get { return _REMAIN; }
            set { _REMAIN = value; }
        }
        public string ACTIVE 
        {
           get { return _ACTIVE; } 
           set { _ACTIVE= value; } 
        }
        public string REFTABLE 
        {
           get { return _REFTABLE; } 
           set { _REFTABLE= value; } 
        }
        public double REFLOID 
        {
           get { return _REFLOID; } 
           set { _REFLOID= value; } 
        }
        #endregion

        #region Query String 
        private string sql_insert 
        { 
           get
           {
               string sqlz =  "INSERT INTO " + tableName + " (STATUS,UNIT,PRICE,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,STOCKOUT,PRODUCT,LOTNO,QTY,ACTIVE,INVNO,REFTABLE,REFLOID,REMAIN)VALUES(";
                sqlz += " '"+ OracleDB.QRText(_STATUS)+ "',";// STATUS";
                sqlz += "  "+_UNIT.ToString() + ",";// UNIT";
                sqlz += "  "+_PRICE.ToString() + ",";// PRICE";
                sqlz += "  "+_LOID.ToString() + ",";// LOID";
                sqlz += " '"+ OracleDB.QRText(_CREATEBY)+ "',";// CREATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_CREATEON)+ ",";// CREATEON";
                sqlz += " '"+ OracleDB.QRText(_UPDATEBY)+ "',";// UPDATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_UPDATEON)+ ",";// UPDATEON";
                sqlz += "  "+_STOCKOUT.ToString() + ",";// STOCKOUT";
                sqlz += "  "+_PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += " '"+ OracleDB.QRText(_LOTNO)+ "',";// LOTNO";
                sqlz += "  "+_QTY.ToString() + ",";// QTY";
                sqlz += " '"+ OracleDB.QRText(_ACTIVE)+ "',";// ACTIVE";
                sqlz += " '" + OracleDB.QRText(_INVNO) + "',";// INVNO";
                sqlz += " '"+ OracleDB.QRText(_REFTABLE)+ "',";// REFTABLE";
                sqlz += "  "+_REFLOID.ToString() + ",";// REFLOID";
                sqlz += "  " + _REMAIN.ToString() + "";// REMAIN";
                sqlz += " ) "; 
                return sqlz; 
           } 
        }
        private string sql_update 
        { 
           get
           {
               string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS)+ "', ";
                sqlz += " UNIT  = " + _UNIT.ToString()+", ";
                sqlz += " PRICE  = " + _PRICE.ToString()+", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY)+ "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON)+ ", ";
                sqlz += " STOCKOUT  = " + _STOCKOUT.ToString()+", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString()+", ";
                sqlz += " LOTNO  = '" + OracleDB.QRText(_LOTNO)+ "', ";
                sqlz += " INVNO  = '" + OracleDB.QRText(_INVNO) + "', ";
                sqlz += " QTY  = " + _QTY.ToString()+", ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE)+ "', ";
                sqlz += " REFTABLE  = '" + OracleDB.QRText(_REFTABLE)+ "', ";
                sqlz += " REFLOID  = " + _REFLOID.ToString()+", ";
                sqlz += " REMAIN = " + _REMAIN.ToString() + " ";
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
                        if (!Convert.IsDBNull(zRdr["STATUS"]))  _STATUS = zRdr["STATUS"].ToString();
                        if (!Convert.IsDBNull(zRdr["UNIT"]))  _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                        if (!Convert.IsDBNull(zRdr["PRICE"]))  _PRICE = Convert.ToDouble(zRdr["PRICE"]);
                        if (!Convert.IsDBNull(zRdr["LOID"]))  _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["CREATEBY"]))  _CREATEBY = zRdr["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["CREATEON"]))  _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                        if (!Convert.IsDBNull(zRdr["UPDATEBY"]))  _UPDATEBY = zRdr["UPDATEBY"].ToString();
                        if (!Convert.IsDBNull(zRdr["UPDATEON"]))  _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                        if (!Convert.IsDBNull(zRdr["STOCKOUT"]))  _STOCKOUT = Convert.ToDouble(zRdr["STOCKOUT"]);
                        if (!Convert.IsDBNull(zRdr["PRODUCT"]))  _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["LOTNO"]))  _LOTNO = zRdr["LOTNO"].ToString();
                        if (!Convert.IsDBNull(zRdr["QTY"]))  _QTY = Convert.ToDouble(zRdr["QTY"]);
                        if (!Convert.IsDBNull(zRdr["ACTIVE"]))  _ACTIVE = zRdr["ACTIVE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFTABLE"]))  _REFTABLE = zRdr["REFTABLE"].ToString();
                        if (!Convert.IsDBNull(zRdr["REFLOID"])) _REFLOID = Convert.ToDouble(zRdr["REFLOID"]);
                        if (!Convert.IsDBNull(zRdr["REMAIN"])) _REMAIN = Convert.ToDouble(zRdr["REMAIN"]);
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
        //public bool DeleteCurrentData(string where, OracleTransaction zTrans)
        //{
        //    return doDelete(where, zTrans);
        //}

        public bool DeleteDataByStockOut(double stockOut, OracleTransaction zTrans)
        {
            return doDelete("STOCKOUT = " + stockOut.ToString(), zTrans);
        } 

        public DataTable GetStockOutList(string stockout)
        {
            string sql = "SELECT ROWNUM NO,A.* FROM (SELECT ST.LOID, ST.LOTNO, ST.QTY, PD.BARCODE,PD.LOID PRODUCT, PD.NAME PRODUCTNAME, PD.PRICE, ST.QTY*PD.PRICE AS NETPRICE, U.LOID UNIT, U.NAME UNITNAME, 0 AS DISCOUNT,ST.ACTIVE,PD.ISVAT, ST.REFLOID, ST.REMAIN AS REMAINQTY FROM STOCKOUTITEM ST ";
                   sql += " INNER JOIN PRODUCT PD ON ST.PRODUCT = PD.LOID INNER JOIN UNIT U ON PD.UNIT = U.LOID ";
                   sql += " WHERE ST.STOCKOUT = " + stockout + " ORDER BY ST.LOID) A";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutItemListBlank()
        {
            string sql = "SELECT 0 NO, 0 RANK, 0 LOID, 0 PRODUCT, 0 QTY, 0 LOTNO, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutReturnList(string stockout)
        {
            string sql = "SELECT ROWNUM RANK,A.* FROM (SELECT ST.LOID, ST.LOTNO, ST.QTY, PD.BARCODE,PD.LOID PRODUCT, PD.NAME PRODUCTNAME, PD.PRICE, ST.QTY*PD.PRICE AS NETPRICE, U.LOID UNIT, U.NAME UNITNAME, 0 AS DISCOUNT,ST.ACTIVE,PD.ISVAT, ST.REFLOID, ST.INVNO FROM STOCKOUTITEM ST ";
            sql += " INNER JOIN PRODUCT PD ON ST.PRODUCT = PD.LOID INNER JOIN UNIT U ON PD.UNIT = U.LOID ";
            sql += " WHERE ST.STOCKOUT = " + stockout + " ORDER BY ST.LOID) A";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutReturnItemListBlank()
        {
            string sql = "SELECT 0 RANK, 0 RANK, 0 LOID, 0 PRODUCT, 0 QTY, 0 LOTNO, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductStock(double warehouse, double productBarcode)
        {
            double zone = Constz.Zone.Z04;
            string sql = "SELECT PS.* ";
            sql += "FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.WAREHOUSE = " + warehouse.ToString() + " AND P.LOID = " + productBarcode.ToString() + " AND ZONE = " + zone.ToString() + " ORDER BY PS.LOTNO ";
            return OracleDB.ExecListCmd(sql);
        }

        public double GetQTYStock(string lotno, double productBarcode)
        {
            //double zone = Constz.Zone.Z31;
            double zone = Constz.Zone.Z04;
            string sql = "SELECT SUM(PS.QTY/P.MULTIPLY) QTY FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.LOTNO = '" + lotno.ToString() + "' AND P.LOID = " + productBarcode.ToString() + " AND PS.ZONE = " + zone.ToString() + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double STOCK = 0;
            if (dt.Rows.Count > 0)
            {
                STOCK = Convert.ToDouble(dt.Rows[0]["QTY"]);

            }

            return STOCK;
        }

        public double GetRemainQTYStock(string lotno, double productBarcode)
        {
            return GetRemainQTYStock(lotno, productBarcode, null);
        }
        public double GetRemainQTYStock(string lotno, double productBarcode, OracleTransaction trans)
        {
            double zone1 = Constz.Zone.Z04;
            double zone2 = Constz.Zone.Z31;
            string sql = "SELECT ROUND(SUM(PS.QTY/P.MULTIPLY),2)  QTY FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.LOTNO = '" + lotno.ToString() + "' AND P.LOID = " + productBarcode.ToString() + " AND PS.ZONE IN (" + zone1.ToString() + "," + zone2.ToString() + ") ";
            DataTable dt = OracleDB.ExecListCmd(sql, trans);
            double STOCK = 0;
            if (dt.Rows.Count > 0)
            {
                STOCK = Convert.ToDouble(dt.Rows[0]["QTY"]);

            }

            return STOCK;
        }

        public double GetQTYStockFG(string lotno, double productBarcode)
        {
            double zone = Constz.Zone.Z30;
            string sql = "SELECT ROUND(SUM(PS.QTY/P.MULTIPLY),2)  QTY FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.LOTNO = '" + lotno.ToString() + "' AND P.LOID = " + productBarcode.ToString() + " AND PS.ZONE = " + zone.ToString() + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double STOCK = 0;
            if (dt.Rows.Count > 0)
            {
                STOCK = Convert.ToDouble(dt.Rows[0]["QTY"]);

            }

            return STOCK;
        }

        public double GetRemainQTYStockFG(string lotno, double productBarcode)
        {
            return GetRemainQTYStockFG(lotno, productBarcode, null);
        }
        public double GetRemainQTYStockFG(string lotno, double productBarcode, OracleTransaction trans)
        {
            double zone1 = Constz.Zone.Z01;
            double zone2 = Constz.Zone.Z30;
            string sql = "SELECT ROUND(SUM(PS.QTY/P.MULTIPLY),2)  QTY FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.LOTNO = '" + lotno.ToString() + "' AND P.LOID = " + productBarcode.ToString() + " AND PS.ZONE IN (" + zone1.ToString() + ","+ zone2.ToString()+ ") ";
            DataTable dt = OracleDB.ExecListCmd(sql, trans);
            double STOCK = 0;
            if (dt.Rows.Count > 0)
            {
                STOCK = Convert.ToDouble(dt.Rows[0]["QTY"]);

            }

            return STOCK;
        }

        public double GetQTYStockReturn(string lotno, double productBarcode ,double warehouse)
        {
            double zone = 0;

            if(warehouse == 1)
                zone = Constz.Zone.Z01;
            else if (warehouse == 2)
                zone = Constz.Zone.Z04;

            string sql = "SELECT ROUND(PS.QTY/P.MULTIPLY,2) QTY FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.LOTNO = '" + lotno.ToString() + "' AND P.LOID = " + productBarcode.ToString() + " AND PS.ZONE = " + zone.ToString() + " AND PS.WAREHOUSE = " + warehouse + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double STOCK = 0;
            if (dt.Rows.Count > 0)
            {
                STOCK = Convert.ToDouble(dt.Rows[0]["QTY"]);

            }

            return STOCK;
        }


        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zLOID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetData(double stockOut, double product, double refLOID, string refTable, string lotNo, OracleTransaction zTrans)
        {
            return doGetdata(" STOCKOUT = " + stockOut.ToString() + " AND PRODUCT = " + product + " AND REFLOID = " + refLOID + " AND REFTABLE = '" + refTable + "' AND LOTNO = '" + lotNo + "' ", zTrans);
        }

        public bool UpdateStatusByStockOut(double stockOut, string status, string userID, OracleTransaction zTrans)
        {
            bool ret = true;
            string sql = "UPDATE " + TableName + " SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
            sql += "WHERE STOCKOUT = " + stockOut.ToString() + " ";
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
