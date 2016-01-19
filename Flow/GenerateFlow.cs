using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;
using ABB.DAL;

namespace ABB.Flow
{
    public class GenerateFlow
    {
        #region     data
        private GenerateData _gendata;
        public GenerateData Data
        {
            set { _gendata = value; }
            get
            {
                if (_gendata == null)
                {
                    _gendata = new GenerateData();
                }
                return _gendata;
            }
        }
        private string _error = "";
        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }
        #endregion

        #region Connect
        private OracleConnection ConnectServer()
        {
            string conn = "Data Source=" + _gendata.Database + ";Persist Security Info=True;User ID=" + _gendata.UserID + ";Password=" + _gendata.Password + ";Unicode=True";
            OracleConnection zConn = new OracleConnection(conn);
            try
            {
                zConn.Open();
            }
            catch (Exception zz)
            {
                zConn = null;
                zz.ToString();
            }
            return zConn;

        }

        public bool CheckConnection
        {
            get
            {
                OracleConnection zConn = ConnectServer();
                if (zConn == null)
                {
                    return false;
                }
                else { zConn.Close(); return true; }
            }
        }

        #endregion

        #region GenDAL
        private string GenMethod
        {
            get
            {
                string _method = "";
                _method = "\nusing System; " +
                "\nusing System.Collections.Generic; " +
                "\nusing System.Text; " +
                "\nusing System.Data; " +
                "\nusing System.Data.OracleClient; " +
                "\nusing ABB.Data; " +
                "\n" +
                "\nnamespace " + _gendata.Namespace + " " +
                "\n{ " +
                "\n    public class " + _gendata.Class + " " +
                "\n    {" +
                "\n        #region Public Method" +
                "\n        " +
                "\n        /// <summary>" +
                "\n        /// Insert Data From Object to DB" +
                "\n        /// </summary>" +
                "\n        /// <param name=\"UserID\"></param>" +
                "\n        /// <param name=\"zTrans\">Transaction, set to null if no transaction provided</param>" +
                "\n        /// <returns></returns>" +
                "\n        public bool InsertCurrentData(string UserID, OracleTransaction zTrans)" +
                "\n        {" +
                "\n            _CREATEBY = UserID;" +
                "\n            _CREATEON = DateTime.Now;" +
                "\n            return doInsert(zTrans);" +
                "\n         }" +
                "\n " +
                "\n         /// <summary>" +
                "\n         /// Update Data From Object to DB" +
                "\n         /// </summary>" +
                "\n         /// <param name=\"UserID\"></param>" +
                "\n         /// <param name=\"zTrans\">Transaction, set to null if no transaction provided</param>" +
                "\n         /// <returns></returns>" +
                "\n        public bool UpdateCurrentData(string UserID, OracleTransaction zTrans)" +
                "\n        {" +
                "\n            _UPDATEBY = UserID;" +
                "\n            _UPDATEON = DateTime.Now;" +
                "\n            return doUpdate(\" LOID = \" + _LOID.ToString() + \" \", zTrans);" +
                "\n        }" +
                "\n " +
                "\n        /// <summary>" +
                "\n        /// Get Data From DB to Object by LOID" +
                "\n        /// </summary>" +
                "\n        /// <param name=\"zLOID\"></param>" +
                "\n        /// <param name=\"zTrans\">Transaction, set to null if no transaction provided</param>" +
                "\n        /// <returns></returns>" +
                "\n        public bool GetDataByLOID(double zLOID, OracleTransaction zTrans)" +
                "\n        {" +
                "\n            return doGetdata(\" LOID = \" + zLOID.ToString() + \" \", zTrans);" +
                "\n        }" +
                "\n  " +
                "\n        /// <summary>" +
                "\n        /// Delete Current Data From DB" +
                "\n        /// </summary>" +
                "\n        /// <param name=\"zTrans\">Transaction, set to null if no transaction provided</param>" +
                "\n        /// <returns></returns>" +
                "\n        public bool DeleteCurrentData(OracleTransaction zTrans)" +
                "\n        {" +
                "\n            return doDelete(\" LOID = \" + _LOID.ToString() + \" \", zTrans);" +
                "\n        }" +
                "\n          " +
                "\n        /// <summary>" +
                "\n        /// Get Data List of This Table" +
                "\n        /// </summary>" +
                "\n        /// <param name=\"whereCause\"></param>" +
                "\n        /// <param name=\"zTrans\">Transaction, set to null if no transaction provided</param>" +
                "\n        /// <returns></returns>" +
                "\n        public DataTable GetDataList(string whereCause, OracleTransaction zTrans)" +
                "\n        {" +
                "\n            return OracleDB.ExecListCmd(sql_select + whereCause);" +
                "\n        }" +
                "\n" +
                "\n        #endregion" +
                "\n ";

                return _method;
            }
        }
        private string GenConstant
        {
            get
            {
                string _ret = "\n        #region Constant" +
                    "\n" +
                    "\n        private string tableName = \"" + _gendata.Table + "\";" +
                    "\n" +
                    "\n        #endregion" +
                    "\n ";
                return _ret;
            }
        }
        #region body
        private string GetBody()
        {
            string ret = "";
            OracleConnection zConn = ConnectServer();
            if (zConn == null)
            {
                _error += "\nไม่สามารถเข้าสู่ระบบได้";
            }
            else
            {
                string zTable = "SELECT column_name AS \"COLUMN_NAME\", data_type AS \"TYPE_NAME\" FROM user_tab_columns WHERE table_name = '" + _gendata.Table + "'";
                OracleCommand zCmd = new OracleCommand(zTable, zConn);
                OracleDataAdapter zAdp = new OracleDataAdapter();
                zAdp.SelectCommand = zCmd;
                DataTable zDt = new DataTable();
                zAdp.Fill(zDt);
                zConn.Close();
                zAdp.Dispose();
                if (zDt.Rows.Count > 0)
                {
                    ret = GetVariable(zDt);
                    ret += GetQueryString(zDt);
                    ret += GetInternalMethod(zDt);
                }
            }
            return ret;
        }
        private string GetVariable(DataTable ddt)
        {
            string _private = "\n        #region Private Variable" +
                            "\n        string _error = \"\";" +
                            "\n        bool _OnDB = false; ";
            string _public = "\n        #region Public Property" +
                            "\n        public string TableName" +
                            "\n        {" +
                            "\n           get { return tableName; }" +
                            "\n        }" +
                            "\n        public string ErrorMessage" +
                            "\n        {" +
                            "\n           get { return _error; }" +
                            "\n           set { _error = value; }" +
                            "\n        }" +
                            "\n        public bool OnDB" +
                            "\n        {" +
                            "\n           get { return _OnDB; }" +
                            "\n           set { _OnDB = value; }" +
                            "\n        }";
            for (int i = 0; i < ddt.Rows.Count; i++)
            {

                switch (ddt.Rows[i]["TYPE_NAME"].ToString())
                {
                    //case "numeric":
                    //case "double":
                    //case "int":
                    case "NUMBER":
                    //case "bigint":
                        _private += "\n        double _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = 0;";
                        if (ddt.Rows[i]["COLUMN_NAME"].ToString() == "LOID" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "loid")
                        {
                            _public += "\n        public double " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                                         "\n        {" +
                                         "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                                         "\n        }";
                        }
                        else
                        {
                            _public += "\n        public double " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                                         "\n        {" +
                                         "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                                         "\n           set { _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "= value; } " +
                                         "\n        }";
                        }

                        break;
                    case "CHAR":
                    case "VARCHAR2":
                    case "varbinary":
                        _private += "\n        string _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = \"\";";
                        if (ddt.Rows[i]["COLUMN_NAME"].ToString() == "CREATEBY" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "UPDATEBY" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "createby" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "updateby")
                        {
                            _public += "\n        public string " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                            "\n        {" +
                            "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                            "\n        }";
                        }
                        else
                        {
                            _public += "\n        public string " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                             "\n        {" +
                             "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                             "\n           set { _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "= value; } " +
                             "\n        }";
                        }
                        break;
                    case "datetime":
                        _private += "\n        DateTime _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = new DateTime(1, 1, 1);";
                        if (ddt.Rows[i]["COLUMN_NAME"].ToString() == "CREATEON" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "UPDATEON" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "createon" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "updateon")
                        {
                            _public += "\n        public DateTime " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                            "\n        {" +
                            "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                            "\n        }";
                        }
                        else
                        {
                            _public += "\n        public DateTime " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                             "\n        {" +
                             "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                             "\n           set { _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "= value; } " +
                             "\n        }";
                        }
                        break;
                    case "smalldatetime":
                        _private += "\n        DateTime _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = new DateTime(1, 1, 1);";
                        if (ddt.Rows[i]["COLUMN_NAME"].ToString() == "CREATEON" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "UPDATEON" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "createon" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "updateon")
                        {
                            _public += "\n        public DateTime " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                            "\n        {" +
                            "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                            "\n        }";
                        }
                        else
                        {
                            _public += "\n        public DateTime " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                             "\n        {" +
                             "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                             "\n           set { _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "= value; } " +
                             "\n        }";
                        }
                        break;
                    case "DATE":
                        _private += "\n        DateTime _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = new DateTime(1, 1, 1);";
                        if (ddt.Rows[i]["COLUMN_NAME"].ToString() == "CREATEON" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "UPDATEON" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "createon" || ddt.Rows[i]["COLUMN_NAME"].ToString() == "updateon")
                        {
                            _public += "\n        public DateTime " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                            "\n        {" +
                            "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                            "\n        }";
                        }
                        else
                        {
                            _public += "\n        public DateTime " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                             "\n        {" +
                             "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                             "\n           set { _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "= value; } " +
                             "\n        }";
                        }
                        break;
                    case "decimal":
                        _private += "\n        decimal _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = 0;";
                        _public += "\n        public decimal " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                                     "\n        {" +
                                     "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                                     "\n           set { _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "= value; } " +
                                     "\n        }";


                        break;
                    case "bit":
                        _private += "\n        bool _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = false;";
                        _public += "\n        public bool " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                                     "\n        {" +
                                     "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                                     "\n           set { _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "= value; } " +
                                     "\n        }";


                        break;

                    default:
                        string aa = ddt.Rows[i]["TYPE_NAME"].ToString();
                        _private += "\n        string _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = \"\";";
                        _public += "\n        public string " + ddt.Rows[i]["COLUMN_NAME"].ToString() + " " +
                        "\n        {" +
                        "\n           get { return _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "; } " +
                        "\n           set { _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "= value; } " +
                        "\n        }";
                        break;

                }
            }//end for
            _private += "\n        #endregion";
            _public += "\n        #endregion";
            return _private + "\n" + _public;
        }
        private string GetQueryString(DataTable ddt)
        {
            string ret = GetQueryStringINS(ddt) + GetQueryStringUPDATE(ddt);
            ret += "\n        private string sql_delete " +
                    "\n        { " +
                    "\n             get " +
                    "\n             { " +
                    "\n                 string sqlz = \" DELETE FROM \" + tableName + \" \"; " +
                    "\n                 return sqlz; " +
                    "\n             } " +
                    "\n        } " +
                    "\n         " +
                    "\n        private string sql_select " +
                    "\n        { " +
                    "\n             get " +
                    "\n             { " +
                    "\n                 string sqlz = \" SELECT * FROM \" + tableName + \" \"; " +
                    "\n                 return sqlz; " +
                    "\n             } " +
                    "\n        } " +
                    "\n         #endregion ";
            return ret;
        }
        private string GetQueryStringINS(DataTable ddt)
        {
            string sqlins = "\n\n        #region Query String " +
                            "\n        private string sql_insert " +
                            "\n        { " +
                            "\n           get" +
                            "\n           {" +
                            "\n               string sqlz =  \"INSERT INTO \" + tableName + \" (";
            for (int i = 0; i < ddt.Rows.Count; i++)
            {
                sqlins += ddt.Rows[i]["COLUMN_NAME"].ToString() + ",";
            }
            sqlins = sqlins.Remove(sqlins.Length - 1, 1) + ")VALUES(\";";

            for (int i = 0; i < ddt.Rows.Count; i++)
            {
                //if (ddt.Rows[i]["COLUMN_NAME"].ToString() != "UPDATEON" && ddt.Rows[i]["COLUMN_NAME"].ToString() != "UPDATEBY")
                //{
                switch (ddt.Rows[i]["TYPE_NAME"].ToString())
                {
                    case "numeric":
                    case "double":
                    case "int":
                    case "bigint":
                    case "decimal":
                    case "NUMBER":
                        sqlins += "\n                sqlz += \"  \"+_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ".ToString() + \",\";";
                        break;
                    case "CHAR":
                    case "VARCHAR2":
                    case "varbinary":
                        sqlins += "\n                sqlz += \" '\"+ OracleDB.QRText(_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ")+ \"',\";";
                        break;
                    case "datetime":
                        sqlins += "\n                sqlz += \" \"+ OracleDB.QRDateTime(_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ")+ \",\";";
                        break;
                    case "DATE":
                        sqlins += "\n                sqlz += \" \"+ OracleDB.QRDateTime(_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ")+ \",\";";
                        break;
                    case "smalldatetime":
                        sqlins += "\n                sqlz += \" \"+ OracleDB.QRDateTime(_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ")+ \",\";";
                        break;
                    case "bit":
                        sqlins += "\n                sqlz += \"  \"+_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ".ToString() + \",\";";
                        break;
                    default:
                        sqlins += "\n                sqlz += \" '\"+ OracleDB.QRText(_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ")+ \"',\";";
                        break;

                }
                if (i == ddt.Rows.Count - 1)
                {
                    sqlins = sqlins.Remove(sqlins.Length - 3, 1);
                }
                sqlins += "// " + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\";";
                //}
            }//end for

            sqlins += "\n                sqlz += \" ) \"; " +
                       "\n                return sqlz; " +
                       "\n           } " +
                       "\n        }";

            return sqlins;
        }
        private string GetQueryStringUPDATE(DataTable ddt)
        {
            string sqlUP = "\n        private string sql_update " +
                            "\n        { " +
                            "\n           get" +
                            "\n           {" +
                            "\n               string sqlz = \" UPDATE \" + tableName + \" SET \";";

            for (int i = 0; i < ddt.Rows.Count; i++)
            {
                if (ddt.Rows[i]["COLUMN_NAME"].ToString() != "LOID" && ddt.Rows[i]["COLUMN_NAME"].ToString() != "CREATEON" && ddt.Rows[i]["COLUMN_NAME"].ToString() != "CREATEBY")
                    switch (ddt.Rows[i]["TYPE_NAME"].ToString())
                    {
                        case "numeric":
                        case "double":
                        case "int":
                        case "bigint":
                        case "decimal":
                        case "NUMBER":
                            sqlUP += "\n                sqlz += \" " + ddt.Rows[i]["COLUMN_NAME"].ToString() + "  = \" + _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ".ToString()+\", \";";
                            break;
                        case "CHAR":
                        case "VARCHAR2":
                        case "varbinary":
                            sqlUP += "\n                sqlz += \" " + ddt.Rows[i]["COLUMN_NAME"].ToString() + "  = '\" + OracleDB.QRText(_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ")+ \"', \";";
                            break;
                        case "datetime":
                            sqlUP += "\n                sqlz += \" " + ddt.Rows[i]["COLUMN_NAME"].ToString() + "  = \" + OracleDB.QRDateTime(_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ")+ \", \";";
                            break;
                        case "DATE":
                            sqlUP += "\n                sqlz += \" " + ddt.Rows[i]["COLUMN_NAME"].ToString() + "  = \" + OracleDB.QRDateTime(_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ")+ \", \";";
                            break;
                        case "smalldatetime":
                            sqlUP += "\n                sqlz += \" " + ddt.Rows[i]["COLUMN_NAME"].ToString() + "  = \" + OracleDB.QRDateTime(_" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ")+ \", \";";
                            break;
                        case "bit":
                            sqlUP += "\n                sqlz += \" " + ddt.Rows[i]["COLUMN_NAME"].ToString() + "  = \" + _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ".ToString()+\", \";";
                            break;
                        default:
                            sqlUP += "\n                sqlz += \" " + ddt.Rows[i]["COLUMN_NAME"].ToString() + "  = \" + _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + ".ToString()+\", \";";
                            break;

                    }
            }//end for
            sqlUP = sqlUP.Remove(sqlUP.Length - 4, 1);
            sqlUP += "\n                sqlz += \"  \"; " +
                        "\n                return sqlz; " +
                        "\n             } " +
                        "\n        }";

            return sqlUP;
        }
        private string GetInternalMethod(DataTable ddt)
        {
            string ret = "";
            ret = "\n\n        #region Internal Method " +
                "\n        private bool doInsert(OracleTransaction zTrans) " +
                "\n        { " +
                "\n             bool ret = true; " +
                "\n             if (!_OnDB) " +
                "\n             { " +
                "\n                 try " +
                "\n                 { " +
                "\n                     _LOID = OracleDB.GetLOID(tableName, zTrans); " +
                "\n                     ret = (OracleDB.ExecNonQueryCmd(sql_insert, zTrans) > 0); " +
                "\n                     if (!ret) _error = OracleDB.Err_NoInsert; " +
                "\n                     else _OnDB = true; " +
                "\n                 } " +
                "\n                 catch (OracleException ex) " +
                "\n                 { " +
                "\n                     ret = false; " +
                "\n                     _error = OracleDB.GetOracleExceptionText(ex); " +
                "\n                 } " +
                "\n                 catch (Exception ex) " +
                "\n                 { " +
                "\n                     ret = false; " +
                "\n                     _error = ex.Message; " +
                "\n                 } " +
                "\n             } " +
                "\n         " +
                "\n             return ret; " +
                "\n        } " +
                "\n         " +
                "\n        private bool doUpdate(string whText, OracleTransaction zTrans) " +
                "\n        { " +
                "\n             bool ret = true; " +
                "\n             if (_OnDB) " +
                "\n             { " +
                "\n                 if (whText.Trim() != \"\") " +
                "\n                 { " +
                "\n                     string tmpWhere = \" WHERE \" + whText; " +
                "\n                     try " +
                "\n                     { " +
                "\n                         ret = (OracleDB.ExecNonQueryCmd(sql_update + tmpWhere, zTrans) > 0); " +
                "\n                         if (!ret) _error = OracleDB.Err_NoUpdate; " +
                "\n                     } " +
                "\n                     catch (OracleException ex) " +
                "\n                     { " +
                "\n                         ret = false; " +
                "\n                         _error = OracleDB.GetOracleExceptionText(ex); " +
                "\n                     } " +
                "\n                     catch (Exception ex) " +
                "\n                     { " +
                "\n                         ret = false; " +
                "\n                         _error = ex.Message; " +
                "\n                     } " +
                "\n                 } " +
                "\n                 else " +
                "\n                 { " +
                "\n                     ret = false; " +
                "\n                     _error = OracleDB.Err_UpdateNoWhere; " +
                "\n                 } " +
                "\n             } " +
                "\n             else " +
                "\n             { " +
                "\n                 ret = false; " +
                "\n                 _error = OracleDB.Err_NoExistUpdate; " +
                "\n             } " +
                "\n             return ret; " +
                "\n         " +
                "\n        } " +
                "\n         " +
                "\n        private bool doDelete(string whText, OracleTransaction zTrans) " +
                "\n        { " +
                "\n             bool ret = true; " +
                "\n             if (whText.Trim() != \"\") " +
                "\n             { " +
                "\n                 string tmpWhere = \" WHERE \" + whText; " +
                "\n                 try " +
                "\n                 { " +
                "\n                     ret = (OracleDB.ExecNonQueryCmd(sql_delete + tmpWhere, zTrans) > 0); " +
                "\n                     if (!ret) _error = OracleDB.Err_NoDelete; " +
                "\n                     else _OnDB = false; " +
                "\n                 } " +
                "\n                 catch (OracleException ex) " +
                "\n                 { " +
                "\n                     ret = false; " +
                "\n                     _error = OracleDB.GetOracleExceptionText(ex); " +
                "\n                 } " +
                "\n                 catch (Exception ex) " +
                "\n                 { " +
                "\n                     ret = false; " +
                "\n                     _error = ex.Message; " +
                "\n                 } " +
                "\n             } " +
                "\n             else " +
                "\n             { " +
                "\n                 ret = false; " +
                "\n                 _error = OracleDB.Err_DeleteNoWhere; " +
                "\n             } " +
                "\n         " +
                "\n             return ret; " +
                "\n        } " +
                "\n         ";
            ret += GetdoGetdata(ddt);
            return ret;
        }
        private string GetdoGetdata(DataTable ddt)
        {
            string ret = "";
            ret = "\n     private bool doGetdata(string whText, OracleTransaction zTrans)" +
                "\n     {" +
                "\n         bool ret = true;" +
                "\n         if (whText.Trim() != \"\")" +
                "\n         {" +
                "\n             string tmpWhere = \" WHERE \" + whText;" +
                "\n             OracleDataReader zRdr = null;" +
                "\n             try" +
                "\n             {" +
                "\n                 zRdr = OracleDB.ExecQueryCmd(sql_select +tmpWhere , zTrans);" +
                "\n                 if (zRdr.Read())" +
                "\n                 {" +
                "\n                     _OnDB = true;";
            for (int i = 0; i < ddt.Rows.Count; i++)
            {
                switch (ddt.Rows[i]["TYPE_NAME"].ToString())
                {
                    case "numeric":
                    case "double":
                    case "int":
                    case "bigint":
                    case "NUMBER":
                        ret += "\n                        if (!Convert.IsDBNull(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]))  _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = Convert.ToDouble(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]);";
                        break;
                    case "CHAR":
                    case "VARCHAR2":
                    case "varbinary":
                        ret += "\n                        if (!Convert.IsDBNull(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]))  _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"].ToString();";
                        break;
                    case "datetime":
                        ret += "\n                        if (!Convert.IsDBNull(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]))  _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = OracleDB.DBDate(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]);";
                        break;
                    case "DATE":
                        ret += "\n                        if (!Convert.IsDBNull(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]))  _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = OracleDB.DBDate(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]);";
                        break;
                    case "smalldatetime":
                        ret += "\n                        if (!Convert.IsDBNull(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]))  _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = OracleDB.DBDate(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]);";
                        break;
                    case "decimal":
                        ret += "\n                        if (!Convert.IsDBNull(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]))  _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = Convert.ToDecimal(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]);";
                        break;
                    case "bit":
                        ret += "\n                        if (!Convert.IsDBNull(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]))  _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = Convert.ToBoolean(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"] + "\"]);";
                        break;
                    default:
                        ret += "\n                        if (!Convert.IsDBNull(zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"]))  _" + ddt.Rows[i]["COLUMN_NAME"].ToString() + " = zRdr[\"" + ddt.Rows[i]["COLUMN_NAME"].ToString() + "\"].ToString();";
                        break;

                }
            }//end for

            ret += "\n                }" +
                    "\n                else" +
                    "\n                {" +
                    "\n                     ret = false;" +
                    "\n                     _error = OracleDB.Err_NoSelect;" +
                    "\n                }" +
                    "\n                zRdr.Close();" +
                    "\n             }" +
                "\n                 catch (OracleException ex) " +
                "\n                 { " +
                "\n                     ret = false; " +
                "\n                     _error = OracleDB.GetOracleExceptionText(ex); " +
                    "\n                 if (zRdr != null && !zRdr.IsClosed)" +
                    "\n                 zRdr.Close();" +
                "\n                 } " +
                    "\n             catch (Exception ex)" +
                    "\n             {" +
                    "\n                 ret = false;" +
                    "\n                 _error = ex.Message;" +
                    "\n                 if (zRdr != null && !zRdr.IsClosed)" +
                    "\n                 zRdr.Close();" +
                    "\n             }" +
                    "\n         }" +
                    "\n         else" +
                    "\n         {" +
                    "\n             ret = false;" +
                    "\n             _error = \"No data found.\";" +
                    "\n         }" +
                    "\n         return ret;" +
                    "\n    }" +
                    "\n    #endregion" +
                    "\n" +
                    "\n    }" +
                    "\n}";
            return ret;
        }
        #endregion
        public string CreateDAL()
        {

            string ret = "";
            string zbody = "";
            ret = GenMethod;
            ret += GenConstant;
            zbody = GetBody();
            if (zbody == "")
            {
                ret = "";
            }
            else
            {
                ret += zbody;
            }
            return ret;

        }


        #endregion
    }
}
