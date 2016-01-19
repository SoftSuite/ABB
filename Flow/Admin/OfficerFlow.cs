using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.DAL.Admin;
using ABB.Data.Admin;
using ABB.Flow;

namespace ABB.Flow.Admin
{
    public class OfficerFlow
    {
        private string _error = "";
        double _LOID = 0;
        private OfficerDAL _searchDAL;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        private OfficerDAL SearchDAL
        {
            get { if (_searchDAL == null) { _searchDAL = new OfficerDAL(); } return _searchDAL; }
        }

        public DataTable GetDataList()
        {
            string sql = "SELECT O.LOID, O.USERID, O.TNAME || ' ' || O.LASTNAME AS USERNAME, O.NICKNAME, DV.TNAME DVNAME, O.TEL ";
            sql += "FROM OFFICER O INNER JOIN DIVISION DV ON O.DIVISION = DV.LOID ORDER BY O.TNAME,O.LASTNAME";
            return OracleDB.ExecListCmd(sql);
        }

        public OfficerData GetData(double loid)
        {
            OfficerData data = new OfficerData();
            if (SearchDAL.GetDataByLOID(loid, null))
            {
                data.LOID = SearchDAL.LOID;
                data.TNAME = SearchDAL.TNAME;
                data.LASTNAME = SearchDAL.LASTNAME;
                data.DIVISION = SearchDAL.DIVISION;
                data.USERID = SearchDAL.USERID;
                data.PASSWORD = AppFlow.Decrypt(SearchDAL.PASSWORD);
                data.EFDATE = SearchDAL.EFDATE;
                data.NICKNAME = SearchDAL.NICKNAME;
                data.BIRTHDATE = SearchDAL.BIRTHDATE;
                data.TEL = SearchDAL.TEL;
                data.EMAIL = SearchDAL.EMAIL;
                data.ADDRESS = SearchDAL.ADDRESS;
                data.ROAD = SearchDAL.ROAD;
                data.PROVINCE = SearchDAL.PROVINCE;
                data.AMPHUR = SearchDAL.AMPHUR;
                data.TAMBOL = SearchDAL.TAMBOL;
                data.ZIPCODE = SearchDAL.ZIPCODE;
                data.REMARK = SearchDAL.REMARK;
                data.TITLE = SearchDAL.TITLE;
            }
            return data;
        }

        public bool UpdateData(string userID, OfficerData data)
        {
            bool ret = true;
            if (VeridateData(data))
            {

                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    SearchDAL.OnDB = false;
                    SearchDAL.GetDataByLOID(data.LOID, obj.zTrans);
                    SearchDAL.TNAME = data.TNAME.Trim();
                    SearchDAL.LASTNAME = data.LASTNAME.Trim();
                    SearchDAL.DIVISION = data.DIVISION;
                    SearchDAL.USERID = data.USERID;
                    SearchDAL.PASSWORD = AppFlow.Encrypt(data.PASSWORD);
                    SearchDAL.EFDATE = data.EFDATE;
                    SearchDAL.NICKNAME = data.NICKNAME;
                    SearchDAL.BIRTHDATE = data.BIRTHDATE;
                    SearchDAL.TEL = data.TEL;
                    SearchDAL.EMAIL = data.EMAIL;
                    SearchDAL.ADDRESS = data.ADDRESS;
                    SearchDAL.ROAD = data.ROAD;
                    SearchDAL.PROVINCE = data.PROVINCE;
                    SearchDAL.AMPHUR = data.AMPHUR;
                    SearchDAL.TAMBOL = data.TAMBOL;
                    SearchDAL.ZIPCODE = data.ZIPCODE;
                    SearchDAL.REMARK = data.REMARK;
                    SearchDAL.TITLE = data.TITLE;
                    
                    if (SearchDAL.OnDB)
                        ret = SearchDAL.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = SearchDAL.InsertCurrentData(userID, obj.zTrans);

                    _LOID = SearchDAL.LOID;

                    if (ret)
                    {
                        obj.zTrans.Commit();
                        obj.CloseConnection();
                    }
                    else
                    {
                        _error = SearchDAL.ErrorMessage;
                    }
                }
                catch (Exception ex)
                {
                    obj.zTrans.Rollback();
                    obj.CloseConnection();
                    ret = false;
                    throw new ApplicationException(ex.Message);
                }
            }
            else
                ret = false;
            return ret;
        }

        private bool VeridateData(OfficerData data)
        {
            bool ret = true;
            if (data.LOID == 0)
            {
                if (data.PASSWORD.Trim() == "")
                {
                    _error = "กรุณาระบุรหัสผ่าน";
                    ret = false;
                }
            }
            if (data.PASSWORD.Trim() != data.PASSCONFIRM.Trim())
            {
                _error = "ยืนยันรหัสผ่านไม่ถูกต้อง";
                ret = false;
            }
            else if (data.USERID.Trim() == "")
            {
                _error = "กรุณาระบุชื่อเข้าระบบ";
                ret = false;
            }
            else if (data.TITLE == 0)
            {
                _error = "กรุณาระบุคำนำหน้าชื่อ";
                ret = false;
            }
            else if (data.TNAME.Trim() == "")
            {
                _error = "กรุณาระบุชื่อพนักงาน";
                ret = false;
            }
            else if (data.LASTNAME.Trim() == "")
            {
                _error = "กรุณาระบุนามสกุลพนักงาน";
                ret = false;
            }
            else if (data.NICKNAME.Trim() == "")
            {
                _error = "กรุณาระบุชื่อเล่นพนักงาน";
                ret = false;
            }
            else if (data.DIVISION == 0)
            {
                _error = "กรุณาระบุฝ่าย";
                ret = false;
            }
            else if (data.ZIPCODE.Trim() == "")
            {
                _error = "กรุณาระบุรหัสไปรษณีย์";
                ret = false;
            }
            else if (data.PROVINCE == 0)
            {
                _error = "กรุณาระบุจังหวัด";
                ret = false;
            }
            else if (data.AMPHUR == 0)
            {
                _error = "กรุณาระบุอำเภอ";
                ret = false;
            }
            else if (data.TAMBOL == 0)
            {
                _error = "กรุณาระบุตำบล";
                ret = false;
            }
            else if (SearchDAL.CheckUserID(data.LOID, data.USERID.Trim()) == false)
            {
                _error = "ชื่อเข้าระบบนี้มีอยู่แล้ว";
                ret = false;
            }
            else if (SearchDAL.CheckName(data.LOID, data.TNAME.Trim(), data.LASTNAME.Trim()) == false)
            {
                _error = "ชื่อและนามสกุลนี้มีอยู่แล้ว";
                ret = false;
            }
            return ret;
        }

        public bool DeleteData(ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                OfficerPositionDAL opDAL = new OfficerPositionDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    SearchDAL.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    opDAL.DeleteDataByOfficer(Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = SearchDAL.DeleteCurrentData(obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(SearchDAL.ErrorMessage);
                    }
                }
                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }
    }
}
