using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using System.Configuration;
using ABB.Data;

namespace ABB.DAL
{
    public class OracleDB
    {
        private static string ConnectionString
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["ABBCONNECTION"].ConnectionString;
                }
                catch
                {
                    throw new Exception("Connection String not been config");
                }
            }
        }

        private static string ServerConnectionString
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["ABBSERVERCONNECTION"].ConnectionString;
                }
                catch
                {
                    throw new Exception("Connection String not been config");
                }
            }
        }

        public static string GetOracleExceptionText(OracleException ex)
        {
            string text = "ไม่สามารถทำรายการได้ ";
            switch (ex.Code.ToString())
            {
                case "2292" :
                    text = text + " เนื่องจากรายการที่แก้ไข มีผลกับรายการอื่น";
                    break;

                default :
                    text = ex.Message;
                    break;
            }
            return text;
        }

        public static OracleConnection GetConnection()
        {
            OracleConnection conn = new OracleConnection(ConnectionString);
            try
            {
                conn.Open();
                return conn;
            }
            catch { return null; }
        }

        public static OracleConnection GetServerConnection()
        {
            OracleConnection conn = new OracleConnection(ServerConnectionString);
            try
            {
                conn.Open();
                return conn;
            }
            catch { return null; }
        }

        #region Executing Query Tool [ExecQuery / ExecNonQuery, ExecList]

        public static int ExecNonQueryCmd(string sqlz) { return ExecNonQueryCmd(sqlz, null); }
        public static int ExecNonQueryCmd(string sqlz, OracleTransaction zTrans)
        {
            OracleCommand zCommand = new OracleCommand();

            int retval;

            if (zTrans != null)
            {
                BuildzCommand(zCommand, zTrans.Connection, zTrans, CommandType.Text, sqlz, null);
                retval = zCommand.ExecuteNonQuery();
            }
            else
            {
                using (OracleConnection zConn = new OracleConnection(ConnectionString))
                {
                    BuildzCommand(zCommand, zConn, zTrans, CommandType.Text, sqlz, null);
                    retval = zCommand.ExecuteNonQuery();
                }
            }
            return retval;

        }

        public static OracleDataReader ExecQueryCmd(string sqlz) { return ExecQueryCmd(sqlz, null, null); }
        public static OracleDataReader ExecQueryCmd(string sqlz, OracleTransaction zTrans) { return ExecQueryCmd(sqlz, null, zTrans); }
        public static OracleDataReader ExecQueryCmd(string sqlz, OracleConnection zConn) { return ExecQueryCmd(sqlz, zConn, null); }
        public static OracleDataReader ExecQueryCmd(string sqlz, OracleConnection zConn, OracleTransaction zTrans)
        {
            OracleCommand zCommand = new OracleCommand();
            OracleDataReader zReader;
            bool LetClose = false;

            if (zTrans != null && zConn == null)
                zConn = zTrans.Connection;
            else if (zConn == null)
            {
                zConn = GetConnection();
                LetClose = true;
            }

            BuildzCommand(zCommand, zConn, zTrans, CommandType.Text, sqlz, null);
            zReader = (LetClose ? zCommand.ExecuteReader(CommandBehavior.CloseConnection) : zCommand.ExecuteReader());

            return zReader;
        }

        public static DataTable ExecListCmd(string sqlz) { return ExecListCmd(sqlz, null, null); }
        public static DataTable ExecListCmd(string sqlz, OracleConnection zConn) { return ExecListCmd(sqlz, zConn, null); }
        public static DataTable ExecListCmd(string sqlz, OracleTransaction zTrans) { return ExecListCmd(sqlz, null, zTrans); }
        public static DataTable ExecListCmd(string sqlz, OracleConnection zConn, OracleTransaction zTrans)
        {
            OracleCommand zCommand = new OracleCommand();
            OracleDataAdapter zAdapt = new OracleDataAdapter();
            zAdapt.SelectCommand = zCommand;
            DataTable zDt = new DataTable();

            if (zTrans == null && zConn == null)
            {
                using (zConn = new OracleConnection(ConnectionString))
                {
                    BuildzCommand(zCommand, zConn, zTrans, CommandType.Text, sqlz, null);
                    zAdapt.Fill(zDt);
                }
            }
            else
            {
                if (zTrans != null && zConn == null)
                    zConn = zTrans.Connection;

                BuildzCommand(zCommand, zConn, zTrans, CommandType.Text, sqlz, null);
                zAdapt.Fill(zDt);
            }
            zAdapt.Dispose();

            return zDt;

        }

        public static DataTable ExecListCmd(string sqlz, string con)
        {
            DataTable zDt = new DataTable();

            using (OracleConnection zConn = new OracleConnection(con))
            {
                zDt = ExecListCmd(sqlz, zConn, null);
            }

            return zDt;
        }

        public static object ExecSingleCmd(string sqlz) { return ExecSingleCmd(sqlz, null, null); }
        public static object ExecSingleCmd(string sqlz, OracleConnection zConn) { return ExecSingleCmd(sqlz, zConn, null); }
        public static object ExecSingleCmd(string sqlz, OracleTransaction zTrans)  { return ExecSingleCmd(sqlz, null, zTrans); }
        public static object ExecSingleCmd(string sqlz, OracleConnection zConn, OracleTransaction zTrans)
        {
            OracleCommand zCommand = new OracleCommand();
            object retval;

            if (zTrans == null && zConn == null)
            {
                using (zConn = new OracleConnection(ConnectionString))
                {
                    BuildzCommand(zCommand, zConn, zTrans, CommandType.Text, sqlz, null);
                    retval = zCommand.ExecuteScalar();
                }
            }
            else
            {
                if (zTrans != null && zConn == null)
                    zConn = zTrans.Connection;

                BuildzCommand(zCommand, zTrans.Connection, zTrans, CommandType.Text, sqlz, null);
                retval = zCommand.ExecuteScalar();
            }
            return retval;
        }

        private static void BuildzCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }


        #endregion

        #region Running Work
        /// <summary>
        /// Get running code with transaction
        /// </summary>
        /// <param name="RunningName"></param>
        /// <param name="RunningItem"></param>
        /// <returns></returns>
        public static string GetRunningCode(string RunningName, string RunningItem) { return GetRunningCode(RunningName, RunningItem, null); }
        public static string GetRunningCode(string RunningName, string RunningItem, OracleTransaction zTrans)
        {
            string tablename = "RUNNING";
            bool LetClose = false;
            OracleConnection zConn = null;
            if (zTrans == null)
            {
                LetClose = true;
                zConn = GetConnection();
                zTrans = zConn.BeginTransaction(IsolationLevel.ReadCommitted);
            }

            string loid = "";
            string code = "";
            string lastValue = "";
            string year = "";
            string month = "";
            string sqlz = "SELECT LOID, CODE, YEAR, MONTH, VALUE FROM " + tablename + " WHERE RUNNING = '" + RunningName + "' AND ITEM = '" + RunningItem + "' ";
            OracleDataReader zRd = ExecQueryCmd(sqlz, zTrans);
            if (zRd.Read())
            {
                int length = 0;
                loid = zRd["LOID"].ToString();
                year = zRd["YEAR"].ToString();
                month = zRd["MONTH"].ToString();
                lastValue = zRd["VALUE"].ToString();
                length = lastValue.Length;

                if (month != "" && year != "")
                {
                    if ((DateTime.Now.Year +543).ToString().Substring(4 - year.Length) != year || DateTime.Now.Month.ToString("00") != month)
                    {
                        year = (DateTime.Now.Year + 543).ToString().Substring(4 - year.Length);
                        month = DateTime.Now.Month.ToString("00");
                        lastValue = "0";
                    }
                }
                else if (year != "")
                {
                    if (year != (DateTime.Now.Year + 543).ToString().Substring(4 - year.Length) && year != "")
                    {
                        year = (DateTime.Now.Year + 543).ToString().Substring(4 - year.Length);
                        lastValue = "0";
                    }
                }

                //if (year != DateTime.Now.Year.ToString().Substring(4 - year.Length) && year != "")
                //{
                //    year = DateTime.Now.Year.ToString().Substring(4 - year.Length);
                //    lastValue = "0";
                //}
                lastValue = "00000000000000000000" + (Convert.ToDouble(lastValue) + 1).ToString();
                lastValue = lastValue.Substring(lastValue.Length - length);
                code = zRd["CODE"].ToString() + year + month + lastValue;
                zRd.Close();

                sqlz = "UPDATE " + tablename + " SET MONTH = '" + month + "', YEAR = '" + year + "', VALUE = '" + lastValue + "' WHERE LOID = " + loid;
                ExecNonQueryCmd(sqlz, zTrans);
            }
            else
            {
                if (LetClose)
                {
                    zTrans.Commit();
                    zConn.Close();
                }
                throw new ApplicationException("ไม่สามารถอ่านค่า running ได้");
            }

            if (LetClose)
            {
                zTrans.Commit();
                zConn.Close();
            }
            return code;
        }

        #endregion

        #region Backward Running PDREQUEST/PDORDER

        public static string GetRunningCode(DateTime docDate, string RunningItem, OracleTransaction zTrans)
        {
            string tablename = "RUNNINGDAY";
            string code = "";
            string day = docDate.ToString("yyMMdd");
            bool LetClose = false;
            OracleConnection zConn = null;
            if (zTrans == null)
            {
                LetClose = true;
                zConn = GetConnection();
                zTrans = zConn.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            string loid = "";
            string lastValue = "";
            string sqlz = "SELECT LOID, RUNNING, DAY, VALUE FROM " + tablename + " WHERE DAY = '" + day + "' AND RUNNING = '" + RunningItem + "' ";
            OracleDataReader zRd = ExecQueryCmd(sqlz, zTrans);
            if (zRd.Read())
            {
                loid = zRd["LOID"].ToString();
                lastValue = zRd["VALUE"].ToString();
                lastValue = (Convert.ToDouble(lastValue) + 1).ToString("000");
                zRd.Close();

                sqlz = "UPDATE " + tablename + " SET VALUE = '" + lastValue + "' WHERE LOID = " + loid;
                ExecNonQueryCmd(sqlz, zTrans);
            }
            else
            {
                lastValue = "001";
                loid = GetLOID("RUNNINGDAY", zTrans).ToString();
                sqlz = "INSERT INTO " + tablename + " (LOID, DAY, VALUE, RUNNING) VALUES (" + loid + ", '" + day + "', '" + lastValue + "', '" + RunningItem + "') ";
                ExecNonQueryCmd(sqlz, zTrans);
            }
            code = day + lastValue;

            if (LetClose)
            {
                zTrans.Commit();
                zConn.Close();
            }
            return code;
        }

        #endregion

        #region LOID Work

        /// <summary>
        /// Get LOID of Table whith transaction use
        /// </summary>
        /// <param name="tablename">Table name's LOID</param>
        /// <param name="zTrans">Transaction</param>
        /// <returns></returns>
        public static double GetLOID(string tablename) { return GetLOID(tablename, null); }
        public static double GetLOID(string tablename, OracleTransaction zTrans)
        {
            bool LetClose = false;
            OracleConnection zConn = null;
            if (zTrans == null)
            {
                LetClose = true;
                zConn = GetConnection();
                zTrans = zConn.BeginTransaction(IsolationLevel.ReadCommitted);
            }

            string lastid;
            string sqlz = "SELECT SQ" + tablename.ToUpper() + ".NEXTVAL AS RUNNINGNUMBER FROM DUAL";

            try
            {
                lastid = ExecSingleCmd(sqlz, zTrans).ToString();
            }
            catch (Exception ex)
            {
                if (LetClose)
                {
                    zTrans.Commit();
                    zConn.Close();
                }
                throw new ApplicationException(ex.Message);
            }

            if (LetClose)
            {
                zTrans.Commit();
                zConn.Close();
            }
            return Convert.ToDouble(lastid);
        }

        public static string GetJDate() { return GetJDate(DateTime.Now); }
        public static string GetJDate(DateTime zdate)
        {
            return "1" + zdate.Year.ToString().Substring(2, 2) + zdate.DayOfYear.ToString("000");
        }

        #endregion

        #region Verify Data From Database
        public static DateTime DBDate(object zObj)
        {
            DateTime ret;
            try
            {
                ret = Convert.ToDateTime(zObj);
            }
            catch
            {
                ret = new DateTime();
            }
            return ret;
        }

        public static double DBDbl(object zObj)
        {
            double ret;
            try
            {
                ret = Convert.ToDouble(zObj);
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        #endregion

        #region Data to Insert to Database
        public static string QRText(string strinput) { return strinput.Trim().Replace("'", "''"); }

        public static string QRDate() { return QRDate(DateTime.Now); }
        public static string QRDate(DateTime DateIn)
        {
            return (DateIn.Year == 1 ? "null" : "TO_DATE('" + DateIn.Year.ToString("0000") + DateIn.ToString("-MM-dd") + "','YYYY-MM-DD')");
        }

        public static string QRDateTime() { return QRDateTime(DateTime.Now); }
        public static string QRDateTime(DateTime DateIn)
        {
            return (DateIn.Year == 1 ? "null" : "TO_DATE('" + DateIn.Year.ToString("0000") + DateIn.ToString("-MM-dd HH:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS')");
        }

        public static string SetDate(DateTime DateIn)
        {
            return SetDateTime(DateIn.Date);
        }
        public static string SetDateToStringField(string fieldName)
        {
            return "TO_CHAR(" + fieldName + ", 'YYYYMMDD')";
        }
        public static string SetDateToStringValue(DateTime DateIn)
        {
            return "'" + DateIn.Year.ToString("0000") + DateIn.ToString("MMdd") + "'";
        }
        #endregion
        /// <summary>
        /// Converts the System.DateTime instant to its equivalent string representation against using the database format.
        /// </summary>
        /// <param name="DateIn">The System.DateTime object.</param>
        /// <returns>A string representation of value of this System.DateTime instant as database format.</returns>
        public static string SetDateTime(DateTime DateIn)
        {
            return (DateIn.Year == 1 ? "null" : "TO_DATE('" + DateIn.Year.ToString("0000") + DateIn.ToString("-MM-dd HH:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS')");
        }
        #region Error Message
        public static string Err_UpdateNoWhere = "Update query without where cause.";
        public static string Err_DeleteNoWhere = "Delete query without where cause.";
        public static string Err_SelectNoWhere = "No where cause in select query may result too many rows of data.";
        public static string Err_NoExistUpdate = "Please getdata before update.";
        public static string Err_NoDelete = "No row was deleted.";
        public static string Err_NoUpdate = "No row was updated.";
        public static string Err_NoSelect = "No data found.";
        public static string Err_NoInsert = "Unable to insert data.";
        public static string Err_NoLOIDSeq = "Unable to get LOID sequence.";
        #endregion
    }
}
