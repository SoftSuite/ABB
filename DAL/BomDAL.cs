using System; 
using System.Collections.Generic; 
using System.Text; 
using System.Data; 
using System.Data.OracleClient; 
using ABB.Data; 

namespace ABB.DAL 
{ 
    public class BomDAL 
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

        private string tableName = "BOM";

        #endregion
 
        #region Private Variable
        string _error = "";
        bool _OnDB = false; 
        double _PROCESS = 0;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        double _MAINPRODUCT = 0;
        double _MATERIAL = 0;
        double _MASTER = 0;
        double _UNIT = 0;
        string _RADIATION = "";
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
        public double PROCESS 
        {
           get { return _PROCESS; } 
           set { _PROCESS= value; } 
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
           set { _CODE= value; } 
        }
        public double MAINPRODUCT 
        {
           get { return _MAINPRODUCT; } 
           set { _MAINPRODUCT= value; } 
        }
        public double MATERIAL 
        {
           get { return _MATERIAL; } 
           set { _MATERIAL= value; } 
        }
        public double MASTER 
        {
           get { return _MASTER; } 
           set { _MASTER= value; } 
        }
        public double UNIT 
        {
           get { return _UNIT; } 
           set { _UNIT= value; } 
        }
        public string RADIATION 
        {
           get { return _RADIATION; } 
           set { _RADIATION= value; } 
        }
        public string ACTIVE 
        {
           get { return _ACTIVE; } 
           set { _ACTIVE= value; } 
        }
        #endregion

        #region Query String 
        private string sql_insert 
        { 
           get
           {
               string sqlz =  "INSERT INTO " + tableName + " (PROCESS,LOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,CODE,MAINPRODUCT,MATERIAL,MASTER,UNIT,RADIATION,ACTIVE)VALUES(";
                sqlz += "  "+_PROCESS.ToString() + ",";// PROCESS";
                sqlz += "  "+_LOID.ToString() + ",";// LOID";
                sqlz += " '"+ OracleDB.QRText(_CREATEBY)+ "',";// CREATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_CREATEON)+ ",";// CREATEON";
                sqlz += " '"+ OracleDB.QRText(_UPDATEBY)+ "',";// UPDATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_UPDATEON)+ ",";// UPDATEON";
                sqlz += " '"+ OracleDB.QRText(_CODE)+ "',";// CODE";
                sqlz += "  "+_MAINPRODUCT.ToString() + ",";// MAINPRODUCT";
                sqlz += "  "+_MATERIAL.ToString() + ",";// MATERIAL";
                sqlz += "  "+_MASTER.ToString() + ",";// MASTER";
                sqlz += "  "+_UNIT.ToString() + ",";// UNIT";
                sqlz += " '"+ OracleDB.QRText(_RADIATION)+ "',";// RADIATION";
                sqlz += " '"+ OracleDB.QRText(_ACTIVE)+ "'";// ACTIVE";
                sqlz += " ) "; 
                return sqlz; 
           } 
        }
        private string sql_update 
        { 
           get
           {
               string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " PROCESS  = " + _PROCESS.ToString()+", ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY)+ "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON)+ ", ";
                sqlz += " CODE  = '" + OracleDB.QRText(_CODE)+ "', ";
                sqlz += " MAINPRODUCT  = " + _MAINPRODUCT.ToString()+", ";
                sqlz += " MATERIAL  = " + _MATERIAL.ToString()+", ";
                sqlz += " MASTER  = " + _MASTER.ToString()+", ";
                sqlz += " UNIT  = " + _UNIT.ToString()+", ";
                sqlz += " RADIATION  = '" + OracleDB.QRText(_RADIATION)+ "', ";
                sqlz += " ACTIVE  = '" + OracleDB.QRText(_ACTIVE)+ "' ";
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
                            if (!Convert.IsDBNull(zRdr["PROCESS"]))  _PROCESS = Convert.ToDouble(zRdr["PROCESS"]);
                            if (!Convert.IsDBNull(zRdr["LOID"]))  _LOID = Convert.ToDouble(zRdr["LOID"]);
                            if (!Convert.IsDBNull(zRdr["CREATEBY"]))  _CREATEBY = zRdr["CREATEBY"].ToString();
                            if (!Convert.IsDBNull(zRdr["CREATEON"]))  _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                            if (!Convert.IsDBNull(zRdr["UPDATEBY"]))  _UPDATEBY = zRdr["UPDATEBY"].ToString();
                            if (!Convert.IsDBNull(zRdr["UPDATEON"]))  _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                            if (!Convert.IsDBNull(zRdr["CODE"]))  _CODE = zRdr["CODE"].ToString();
                            if (!Convert.IsDBNull(zRdr["MAINPRODUCT"]))  _MAINPRODUCT = Convert.ToDouble(zRdr["MAINPRODUCT"]);
                            if (!Convert.IsDBNull(zRdr["MATERIAL"]))  _MATERIAL = Convert.ToDouble(zRdr["MATERIAL"]);
                            if (!Convert.IsDBNull(zRdr["MASTER"]))  _MASTER = Convert.ToDouble(zRdr["MASTER"]);
                            if (!Convert.IsDBNull(zRdr["UNIT"]))  _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                            if (!Convert.IsDBNull(zRdr["RADIATION"]))  _RADIATION = zRdr["RADIATION"].ToString();
                            if (!Convert.IsDBNull(zRdr["ACTIVE"]))  _ACTIVE = zRdr["ACTIVE"].ToString();
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
        public bool DeleteDataByMainProduct(double product, OracleTransaction zTrans)
        {
            return doDelete("MAINPRODUCT = " + product.ToString() + " ", zTrans);
        }

    }
}