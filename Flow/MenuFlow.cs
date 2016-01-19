using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ABB.DAL;
using ABB.Data;

namespace ABB.Flow
{
    public class MenuFlow
    {
        private string _error = "";
        public string ErrorMessage { get { return _error; } }

        public DataTable GetMenuData(string UserID)
        {
            DataTable zDt;
            try
            {
                string sqlz = "";

                sqlz = " SELECT * FROM ZROLE WHERE OFFICER = ( SELECT LOID FROM OFFICER WHERE USERID = '" + OracleDB.QRText(UserID) + "') ";
                zDt = OracleDB.ExecListCmd(sqlz);

                string zRole = "";
                string zLevel = "";

                if (zDt.Rows.Count > 0)
                {
                    zRole = zDt.Rows[0]["LOID"].ToString();
                    zLevel = zDt.Rows[0]["ZLEVEL"].ToString();
                }


                sqlz = " SELECT ZM.*, ZS.NAME as SYSNAME, ZS.LOID as SYSLOID, ZS.LINK as SYSLINK, ZG.GNAME as GROUPNAME, ZG.IMAGE as GROUPIMAGE FROM ZMENU ZM INNER JOIN ZSYSTEM ZS ON ZM.ZSYSTEM = ZS.LOID LEFT JOIN ZMENUGROUP ZG ON ZM.MENUGROUP = ZG.GID WHERE ZM.ENABLED = 'Y' ";

                if (zLevel != "A")
                    sqlz += " AND ZM.LOID IN ( SELECT ZMENU FROM ZROLEASSIGN WHERE ZROLE = " + zRole + " OR ZROLE IN ( SELECT PARENT FROM ZROLEREF WHERE ZROLE = " + zRole + ") )";

                sqlz += " ORDER BY ZS.LOID, ZM.MENUGROUP, ZM.SEQUENCE  ";
                zDt = OracleDB.ExecListCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                zDt = new DataTable();
            }

            return zDt;
        }
    }
}
