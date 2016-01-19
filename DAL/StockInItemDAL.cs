using System; 
using System.Collections.Generic; 
using System.Text; 
using System.Data; 
using System.Data.OracleClient; 
using ABB.Data; 

namespace ABB.DAL 
{ 
    public class StockInItemDAL 
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

        private string tableName = "STOCKINITEM";

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _PRICE = 0;
        string _QCRESULT = "";
        string _QCREMARK = "";
        double _QCQTY = 0;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _STOCKIN = 0;
        double _PRODUCT = 0;
        string _LOTNO = "";
        double _QTY = 0;
        double _QTYLOST = 0;
        string _STATUS = "";
        double _REFLOID = 0;
        string _REFTABLE = "";
        string _REMARK = "";
        double _UNIT = 0;
        string _ACTIVE = "";
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
        public double QCQTY
        {
            get { return _QCQTY; }
            set { _QCQTY = value; }
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
        public double STOCKIN
        {
            get { return _STOCKIN; }
            set { _STOCKIN = value; }
        }
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }
        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }
        public double QTYLOST
        {
            get { return _QTYLOST; }
            set { _QTYLOST = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }
        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
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
        #endregion

        #region Query String 
        private string sql_insert 
        { 
           get
           {
               string sqlz = "INSERT INTO " + tableName + " (UNIT,PRICE,QCRESULT,QCREMARK,QCQTY,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,STOCKIN,PRODUCT,LOTNO,QTY,QTYLOST,STATUS,REFLOID,REFTABLE,REMARK)VALUES(";
                sqlz += "  "+_UNIT.ToString() + ",";// UNIT";
                sqlz += "  "+_PRICE.ToString() + ",";// PRICE";
                sqlz += " '"+ OracleDB.QRText(_QCRESULT)+ "',";// QCRESULT";
                sqlz += " '"+ OracleDB.QRText(_QCREMARK)+ "',";// QCREMARK";
                sqlz += "  "+_QCQTY.ToString() + ",";// QCQTY";
                sqlz += "  "+_LOID.ToString() + ",";// LOID";
                sqlz += " '"+ OracleDB.QRText(_CREATEBY)+ "',";// CREATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_CREATEON)+ ",";// CREATEON";
                sqlz += " '"+ OracleDB.QRText(_UPDATEBY)+ "',";// UPDATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_UPDATEON)+ ",";// UPDATEON";
                sqlz += "  "+_STOCKIN.ToString() + ",";// STOCKIN";
                sqlz += "  "+_PRODUCT.ToString() + ",";// PRODUCT";
                sqlz += " '"+ OracleDB.QRText(_LOTNO)+ "',";// LOTNO";
                sqlz += "  "+_QTY.ToString() + ",";// QTY";
                sqlz += "  " + _QTYLOST.ToString() + ",";// QTYLOST";
                sqlz += " '"+ OracleDB.QRText(_STATUS)+ "',";// STATUS";
                sqlz += "  "+_REFLOID.ToString() + ",";// REFLOID";
                sqlz += " '"+ OracleDB.QRText(_REFTABLE)+ "',";// REFTABLE";
                sqlz += " '" + OracleDB.QRText(_REMARK) + "'";// REMARK";
                sqlz += " ) "; 
                return sqlz; 
           } 
        }
        private string sql_update 
        { 
           get
           {
               string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " UNIT  = " + _UNIT.ToString()+", ";
                sqlz += " PRICE  = " + _PRICE.ToString()+", ";
                sqlz += " QCRESULT  = '" + OracleDB.QRText(_QCRESULT)+ "', ";
                sqlz += " QCREMARK  = '" + OracleDB.QRText(_QCREMARK)+ "', ";
                sqlz += " QCQTY  = " + _QCQTY.ToString()+", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY)+ "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON)+ ", ";
                sqlz += " STOCKIN  = " + _STOCKIN.ToString()+", ";
                sqlz += " PRODUCT  = " + _PRODUCT.ToString()+", ";
                sqlz += " LOTNO  = '" + OracleDB.QRText(_LOTNO)+ "', ";
                sqlz += " QTY  = " + _QTY.ToString()+", ";
                sqlz += " QTYLOST  = " + _QTYLOST.ToString() + ", ";
                sqlz += " STATUS  = '" + OracleDB.QRText(_STATUS)+ "', ";
                sqlz += " REFLOID  = " + _REFLOID.ToString()+", ";
                sqlz += " REFTABLE  = '" + OracleDB.QRText(_REFTABLE)+ "', ";
                sqlz += " REMARK  = '" + OracleDB.QRText(_REMARK) + "' ";
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
                            if (!Convert.IsDBNull(zRdr["UNIT"]))  _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                            if (!Convert.IsDBNull(zRdr["PRICE"]))  _PRICE = Convert.ToDouble(zRdr["PRICE"]);
                            if (!Convert.IsDBNull(zRdr["QCRESULT"]))  _QCRESULT = zRdr["QCRESULT"].ToString();
                            if (!Convert.IsDBNull(zRdr["QCREMARK"]))  _QCREMARK = zRdr["QCREMARK"].ToString();
                            if (!Convert.IsDBNull(zRdr["QCQTY"]))  _QCQTY = Convert.ToDouble(zRdr["QCQTY"]);
                            if (!Convert.IsDBNull(zRdr["LOID"]))  _LOID = Convert.ToDouble(zRdr["LOID"]);
                            if (!Convert.IsDBNull(zRdr["CREATEBY"]))  _CREATEBY = zRdr["CREATEBY"].ToString();
                            if (!Convert.IsDBNull(zRdr["CREATEON"]))  _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                            if (!Convert.IsDBNull(zRdr["UPDATEBY"]))  _UPDATEBY = zRdr["UPDATEBY"].ToString();
                            if (!Convert.IsDBNull(zRdr["UPDATEON"]))  _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                            if (!Convert.IsDBNull(zRdr["STOCKIN"]))  _STOCKIN = Convert.ToDouble(zRdr["STOCKIN"]);
                            if (!Convert.IsDBNull(zRdr["PRODUCT"]))  _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                            if (!Convert.IsDBNull(zRdr["LOTNO"]))  _LOTNO = zRdr["LOTNO"].ToString();
                            if (!Convert.IsDBNull(zRdr["QTY"]))  _QTY = Convert.ToDouble(zRdr["QTY"]);
                            if (!Convert.IsDBNull(zRdr["QTYLOST"])) _QTYLOST = Convert.ToDouble(zRdr["QTYLOST"]);
                            if (!Convert.IsDBNull(zRdr["STATUS"]))  _STATUS = zRdr["STATUS"].ToString();
                            if (!Convert.IsDBNull(zRdr["REFLOID"]))  _REFLOID = Convert.ToDouble(zRdr["REFLOID"]);
                            if (!Convert.IsDBNull(zRdr["REFTABLE"]))  _REFTABLE = zRdr["REFTABLE"].ToString();
                            if (!Convert.IsDBNull(zRdr["REMARK"])) _REFTABLE = zRdr["REMARK"].ToString();

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

        public bool DeleteDataByStockIn(double stockIn, OracleTransaction zTrans)
        {
            return doDelete("STOCKIN = " + stockIn.ToString(), zTrans);
        } 

        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zLOID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetData(double stockIn, double product, double refLOID, string refTable, OracleTransaction zTrans)
        {
            return doGetdata(" STOCKIN = " + stockIn.ToString() + " AND PRODUCT = " + product + " AND REFLOID = " + refLOID + " AND REFTABLE = '" + refTable + "' " , zTrans);
        }

        public bool UpdateStatusByStockIn(double stockIn, string status, string userID, OracleTransaction zTrans)
        {
            bool ret = true;
            string sql = "UPDATE " + TableName + " SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', UPDATEON = SYSDATE ";
            sql += "WHERE STOCKIN = " + stockIn.ToString() + " ";
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