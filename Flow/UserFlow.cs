using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using ABB.DAL;
using ABB.Data;

namespace ABB.Flow
{
    public class UserFlow
    {

        private string _error = "";
        public string ErrorMessage { get { return _error; } }

        public DataTable GetUserList(string userID, string userName, string userROLE)
        {
            string sqlz = " SELECT O.LOID, O.USERID, O.LOID as OFFICER, nvl(O.TNAME, '') || ' ' || nvl(O.LASTNAME, '') as OFFNAME, R.ZLEVEL, ";
            sqlz += " CASE R.ZLEVEL WHEN 'A' THEN 'ผู้ดูแลระบบ' WHEN 'U' THEN 'ผู้ใช้งานระบบ' WHEN 'M' THEN 'หัวหน้างาน' ELSE 'ยังไม่กำหนด' END as ZLEVELNAME, ";
            sqlz += " O.LASTLOGIN, NVL(R.LOID,0) ROLE ";
            sqlz += " FROM OFFICER O LEFT JOIN ZROLE R ON O.LOID = R.OFFICER ";

            string whStr = "";
            whStr += (userID.Trim() == "" ? "" : " O.USERID LIKE '%" + userID + "%' ");
            whStr += (userName.Trim() == "" ? "" : (whStr.Trim() == "" ? "" : " AND ") + " ( O.TNAME LIKE '%" + userName + "%' OR O.LASTNAME LIKE '%" + userName + "%'");
            whStr += (userROLE.Trim() == "" ? "" : (whStr.Trim() == "" ? "" : " AND ") + " R.ZLEVEL = '" + userROLE + "' ");

            sqlz = sqlz + (whStr.Trim() == "" ? "" : " WHERE " + whStr) + " ORDER BY O.TNAME,O.LASTNAME ";

            DataTable zDt;
            try
            {
                zDt = OracleDB.ExecListCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                zDt = new DataTable();
            }

            return zDt;
        }

        public DataTable GetGroupList(string groupName)
        {
            string sqlz = " SELECT R.LOID, R.DESCRIPTION, ( SELECT COUNT(ZMENU) FROM ZROLEASSIGN WHERE ZROLE = R.LOID ) as MENUCOUNT ";
            sqlz += " FROM ZROLE R ";
            sqlz += " WHERE ZLEVEL = 'G' ";

            string whStr = "";
            whStr += (groupName.Trim() == "" ? "" : " R.DESCRIPTION LIKE '%" + groupName + "%' ");

            sqlz = sqlz + (whStr.Trim() == "" ? "" : " AND " + whStr);

            DataTable zDt;
            try
            {
                zDt = OracleDB.ExecListCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                zDt = new DataTable();
            }


            return zDt;
        }

        public bool InsertGroup(string UserID, string groupName)
        {
            bool ret = true;
            string sqlz = " INSERT INTO ZROLE ( LOID, OFFICER, DESCRIPTION, ZLEVEL, CREATEBY ) VALUES ( nvl( (SELECT MAX(LOID) FROM ZROLE) ,0) + 1, 0, '" + OracleDB.QRText(groupName) + "', 'G', '" + OracleDB.QRText(UserID) + "') ";
            try
            {
                OracleDB.ExecNonQueryCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = false;
            }

            return ret;
        }

        public bool UpdateGroup(string UserID, string RoleID, string groupName)
        {
            bool ret = true;
            string sqlz = " UPDATE ZROLE SET DESCRIPTION = '" + OracleDB.QRText(groupName) + "', UPDATEBY = '" + OracleDB.QRText(UserID) + "', UPDATEON = sysdate WHERE LOID = " + RoleID + " ";
            try
            {
                OracleDB.ExecNonQueryCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = false;
            }

            return ret;
        }

        public string GetLastRoleID()
        {
            string ret = "";
            string sqlz = " SELECT MAX(LOID) FROM ZROLE ";
            try
            {
                ret = OracleDB.ExecSingleCmd(sqlz).ToString();
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = "";
            }

            return ret;
        }

        public DataTable  GetRoleData(string RoleID)
        {
            string sqlz = " SELECT * FROM ZROLE WHERE LOID = " + RoleID + " ";
            DataTable zDt;
            try
            {
                zDt = OracleDB.ExecListCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                zDt = new DataTable();
            }


            return zDt;
        }

        public DataTable GetUserData(string UserLOID)
        {
            string sqlz = " SELECT * FROM OFFICER WHERE LOID = " + UserLOID + " ";
            DataTable zDt;
            try
            {
                zDt = OracleDB.ExecListCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                zDt = new DataTable();
            }


            return zDt;
        }

        public string GetRoleID(string UserLOID)
        {
            string ret = "";
            string sqlz = " SELECT LOID FROM ZROLE WHERE OFFICER = '" + UserLOID + "' ";
            try
            {
                ret = OracleDB.ExecSingleCmd(sqlz).ToString();
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = "";
            }

            return ret;
        }

        public string GetRoleLevel(string RoleID)
        {
            string ret = "";
            string sqlz = " SELECT ZLEVEL FROM ZROLE WHERE LOID = '" + RoleID + "' ";
            try
            {
                ret = OracleDB.ExecSingleCmd(sqlz).ToString();
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = "";
            }

            return ret;
        }


        public bool CreateRoleForUser(string UserID, string UserLOID, string zLEVEL)
        {
            bool ret = true;
            string sqlz = " INSERT INTO ZROLE ( LOID, OFFICER, DESCRIPTION, ZLEVEL, CREATEBY ) VALUES ( nvl( (SELECT MAX(LOID) FROM ZROLE) ,0) + 1, " + UserLOID + ", 'User Role', '" + zLEVEL + "', '" + OracleDB.QRText(UserID) + "') ";
            try
            {
                OracleDB.ExecNonQueryCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = false;
            }

            return ret;
        }

        public bool UpdateRoleLevel(string UserID, string RoleID, string zLEVEL, bool roleHHT, bool rolePOS)
        {
            bool ret = true;
            string sqlz = " UPDATE ZROLE SET ZLEVEL = '" + OracleDB.QRText(zLEVEL) + "', UPDATEBY = '" + OracleDB.QRText(UserID) + "', HHT = '" + (roleHHT ? "Y" : "N") + "', POS = '" + (rolePOS ? "Y" : "N") + "', UPDATEON = sysdate WHERE LOID = " + RoleID + " ";
            try
            {
                OracleDB.ExecNonQueryCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = false;
            }

            return ret;
        }

        public bool InvokeRole(ArrayList arrLOID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateTransaction();
            string sql = "";
            try
            {
                for (int i = 0; i < arrLOID.Count; ++i)
                {
                    sql = "DELETE FROM ZROLEASSIGN WHERE ZROLE = " + arrLOID[i].ToString();
                    OracleDB.ExecNonQueryCmd(sql, obj.zTrans);
                    sql = "DELETE FROM ZROLEREF WHERE ZROLE = " + arrLOID[i].ToString();
                    OracleDB.ExecNonQueryCmd(sql, obj.zTrans);
                    sql = "DELETE FROM ZROLE WHERE LOID = " + arrLOID[i].ToString();
                    OracleDB.ExecNonQueryCmd(sql, obj.zTrans);
                }
                obj.zTrans.Commit();
                obj.zConn.Close();
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = false;
                obj.zTrans.Rollback();
                obj.zConn.Close();
            }

            return ret;
        }

        public bool DeleteGroup(ArrayList arrLOID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateTransaction();
            string sql = "";
            try
            {
                for (int i = 0; i < arrLOID.Count; ++i)
                {
                    string[] arr = arrLOID[i].ToString().Split('#');
                    sql = "SELECT COUNT(ZROLE) FROM ZROLEREF WHERE PARENT = " + arr[0];
                    if (Convert.ToDouble(OracleDB.ExecSingleCmd(sql, obj.zTrans)) > 0)
                    {
                        throw new ApplicationException("ไม่สามารถลบกลุ่มผู้ใช้งาน '" + arr[1] + "' ได้ เนื่องจากมีการกำหนดสิทธิ์ผู้ใช้งานกลุ่มนี้");
                    }
                    else
                    {
                        sql = "DELETE FROM ZROLEASSIGN WHERE ZROLE = " + arr[0];
                        OracleDB.ExecNonQueryCmd(sql, obj.zTrans);
                        sql = "DELETE FROM ZROLE WHERE LOID = " + arr[0];
                        OracleDB.ExecNonQueryCmd(sql, obj.zTrans);
                    }
                }
                obj.zTrans.Commit();
                obj.zConn.Close();
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = false;
                obj.zTrans.Rollback();
                obj.zConn.Close();
            }

            return ret;
        }

        #region Group Management

        public DataTable GetGroupRoleNotIn(string RoleID)
        {
            string sqlz = " SELECT DESCRIPTION as NAME, LOID FROM ZROLE WHERE ZLEVEL = 'G' AND LOID NOT IN ( SELECT PARENT FROM ZROLEREF WHERE ZROLE = " + RoleID + ") ";
            DataTable zDt;
            try
            {
                zDt = OracleDB.ExecListCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                zDt = new DataTable();
            }


            return zDt;
        }
        public DataTable GetGroupRoleIn(string RoleID)
        {
            string sqlz = " SELECT DESCRIPTION as NAME, LOID FROM ZROLE WHERE ZLEVEL = 'G' AND LOID IN ( SELECT PARENT FROM ZROLEREF WHERE ZROLE = " + RoleID + " ) ";
            DataTable zDt;
            try
            {
                zDt = OracleDB.ExecListCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                zDt = new DataTable();
            }


            return zDt;
        }


        public bool SaveUserGroup(string UserID, string RoleID, ArrayList arrGroup)
        {
            bool ret = true;
            OracleDBObj zObj = new OracleDBObj();
            zObj.CreateTransaction();
            
            try
            {
                string sqlz = "";
                sqlz = " DELETE FROM ZROLEREF WHERE ZROLE = " + RoleID + " ";
                OracleDB.ExecNonQueryCmd(sqlz, zObj.zTrans);
                for (int i = 0; i < arrGroup.Count; i++)
                {
                    sqlz = " INSERT INTO ZROLEREF (ZROLE, PARENT, CREATEBY) VALUES (" + RoleID + ", " + arrGroup[i].ToString() + ", '" + OracleDB.QRText(UserID) + "' ) ";
                    OracleDB.ExecNonQueryCmd(sqlz, zObj.zTrans);
                }
                zObj.zTrans.Commit();
            }
            catch (Exception ex)
            {
                zObj.zTrans.Rollback();
                _error = "Error while save user group data. " + ex.Message;
                ret = false;
            }

            zObj.CloseConnection();
            return ret;
        }
        #endregion

        #region Menu Management

        public DataTable GetMenuRoleNotAssign(string RoleID)
        {
            string sqlz = " SELECT S.NAME || ' >> ' || T.MENUNAME AS NAME, T.LOID FROM ZMENU T ";
            sqlz += " INNER JOIN ZSYSTEM S ON T.ZSYSTEM = S.LOID ";
            sqlz += " WHERE T.LOID NOT IN ( SELECT ZMENU FROM ZROLEASSIGN WHERE ZROLE = " + RoleID + ") ";
            sqlz += " ORDER BY S.LOID, T.SEQUENCE ";
            DataTable zDt;
            try
            {
                zDt = OracleDB.ExecListCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                zDt = new DataTable();
            }


            return zDt;
        }
        public DataTable GetMenuRoleAssign(string RoleID)
        {
            string sqlz = " SELECT S.NAME || ' >> ' || T.MENUNAME AS NAME, T.LOID FROM ZMENU T ";
            sqlz += " INNER JOIN ZSYSTEM S ON T.ZSYSTEM = S.LOID ";
            sqlz += " WHERE T.LOID IN ( SELECT ZMENU FROM ZROLEASSIGN WHERE ZROLE = " + RoleID + ") ";
            sqlz += " ORDER BY S.LOID, T.SEQUENCE ";
            DataTable zDt;
            try
            {
                zDt = OracleDB.ExecListCmd(sqlz);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                zDt = new DataTable();
            }

            return zDt;
        }


        public bool SaveRoleMenu(string UserID, string RoleID, ArrayList arrMenu)
        {
            bool ret = true;
            OracleDBObj zObj = new OracleDBObj();
            zObj.CreateTransaction();

            try
            {
                string sqlz = "";
                sqlz = " DELETE FROM ZROLEASSIGN WHERE ZROLE = " + RoleID + " ";
                OracleDB.ExecNonQueryCmd(sqlz, zObj.zTrans);
                for (int i = 0; i < arrMenu.Count; i++)
                {
                    sqlz = " INSERT INTO ZROLEASSIGN (ZROLE, ZMENU, CREATEBY) VALUES (" + RoleID + ", " + arrMenu[i].ToString() + ", '" + OracleDB.QRText(UserID) + "' ) ";
                    OracleDB.ExecNonQueryCmd(sqlz, zObj.zTrans);
                }
                zObj.zTrans.Commit();
            }
            catch (Exception ex)
            {
                zObj.zTrans.Rollback();
                _error = "Error while save user menu data. " + ex.Message;
                ret = false;
            }

            zObj.CloseConnection();
            return ret;
        }
        #endregion



    }

}
