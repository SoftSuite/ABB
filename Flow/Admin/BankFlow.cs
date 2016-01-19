using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data.Admin;
using ABB.Data;
using ABB.DAL;

namespace ABB.Flow.Admin
{
    public class BankFlow
    {
        string _error = "";
        double _LOID = 0;
        BankDAL _dal;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public BankDAL DALObj
        {
            get { if (_dal == null) { _dal = new BankDAL(); } return _dal; }
        }

        public DataTable GetDataList()
        {
            return DALObj.GetDataList("", "CODE", null);
        }

        public BankData GetData(double loid)
        {
            BankData data = new BankData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                data.NAME = DALObj.NAME;
            }
            return data;
        }

        public string GenNewCode()
        {
            string code = "001";
            string sql = "SELECT MAX(LOID) FROM BANK ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count != 0 && dt.Rows[0][0].ToString() != "")
            {
                code = String.Format("{0:000}", Convert.ToInt32(dt.Rows[0][0]) + 1);
                if (code.Length > 3)
                {
                    code = code.Substring(0, code.Length - 3);
                }
            }
            return "B" + code;
        }


        public bool UpdateData(string userID, BankData data)
        {
            bool ret = true;
            if (VeridateData(data))
            {

                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    DALObj.CODE = data.CODE.Trim();
                    DALObj.NAME = data.NAME.Trim();
                    DALObj.ACTIVE = Constz.ActiveStatus.Active;

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;

                    if (ret)
                    {
                        obj.zTrans.Commit();
                        obj.CloseConnection();
                    }
                    else
                    {
                        _error = DALObj.ErrorMessage;
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

        private bool VeridateData(BankData data)
        {
            bool ret = true;
            if (data.NAME.Trim() == "")
            {
                _error = "กรุณาระบุชื่อธนาคาร";
                ret = false;
            }

            else if (DALObj.CheckName(data.LOID, data.NAME.Trim()) == false)
            {
                _error = "ชื่อธนาคารนี้มีอยู่แล้ว";
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
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = DALObj.DeleteCurrentData(obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
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
