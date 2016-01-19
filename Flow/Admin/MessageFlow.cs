using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.DAL;
using ABB.Data.Admin;

namespace ABB.Flow.Admin
{
    public class MessageFlow
    {
        string _error = "";
        double _LOID = 0;
        MessageDAL _dal;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public MessageDAL DALObj
        {
            get { if (_dal == null) { _dal = new MessageDAL(); } return _dal; }
        }

        public DataTable GetMessageList()
        {
            string sql = "SELECT MS.LOID, MS.MESSAGE, MS.CREATEON, OC.TNAME||' '||OC.LASTNAME AS NAME, MS.CREATEBY, MS.EFDATE, MS.EPDATE , MS.UPDATEON ";
            sql += "FROM MESSAGE MS INNER JOIN OFFICER OC ON MS.CREATEBY = OC.USERID ";
            sql += "WHERE TO_CHAR(SYSDATE,'YYYY/MM/DD') BETWEEN TO_CHAR(MS.EFDATE,'YYYY/MM/DD') AND TO_CHAR(MS.EPDATE,'YYYY/MM/DD') ";
            sql += "ORDER BY NVL(MS.UPDATEON,MS.CREATEON)DESC";
            return OracleDB.ExecListCmd(sql);
        }

        public MessageData GetMessageData(double LOID)
        {
            MessageData data = new MessageData();
            DALObj.GetDataByLOID(LOID, null);
            data.EFDATE = DALObj.EFDATE;
            data.EPDATE = DALObj.EPDATE;
            data.MESSAGE = DALObj.MESSAGE;
            data.LOID = DALObj.LOID;
            return data;
        }

        public bool ValidateData(MessageData data)
        {
            bool ret = true;
            if (data.MESSAGE == "")
            {
                ret = false;
                _error = "กรุณาระบุข้อความ";
            }
            else if (data.MESSAGE.Length > 4000)
            {
                ret = false;
                _error = "กรอกข้อความได้ไม่เกิน 4,000 ตัวอักษร";
            }
            else if (data.EFDATE.Year == 1)
            {
                ret = false;
                _error = "กรุณาวันที่เริ่มแสดงข้อความ";
            }
            else if (data.EPDATE.Year == 1)
            {
                ret = false;
                _error = "กรุณาวันที่สิ้นสุดการแสดงข้อความ";
            }
            else if (data.EPDATE < data.EFDATE)
            {
                ret = false;
                _error = "วันที่เริ่มแสดงข้อความต้องน้อยกว่าวันที่สิ้นสุดการแสดง";
            }
            return ret;
        }

        public bool UpdateData(string userID, MessageData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);

                    DALObj.MESSAGE = data.MESSAGE;
                    DALObj.EFDATE = data.EFDATE;
                    DALObj.EPDATE = data.EPDATE;

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
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
            }
            else
                ret = false;
            return ret;
        }

        public bool DeleteData(Double loid)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.GetDataByLOID(loid, obj.zTrans);
                ret = DALObj.DeleteCurrentData(obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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
