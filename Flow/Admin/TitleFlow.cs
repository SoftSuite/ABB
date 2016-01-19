using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Admin;
using ABB.DAL;

namespace ABB.Flow.Admin
{
    public class TitleFlow
    {
        string _error = "";
        TitleDAL _dal;

        public string ErrorMessage
        {
            get { return _error; }
        }
        public TitleDAL DALObj
        {
            get { if (_dal == null) { _dal = new TitleDAL(); } return _dal; }
        }

        public DataTable GetDataList()
        {
            return DALObj.GetDataList("", null);
        }

        private bool ValidateData(TitleData data)
        {
            bool ret = true;
            if (data.CODE.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุรหัสคำนำหน้าชื่อ";
            }
            else if (data.NAME.Trim() == "")
            {
                _error = "กรุณาระบุคำนำหน้าชื่อ";
            }
            return ret;
        }

        public TitleData GetData(double loid)
        {
            TitleData data = new TitleData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.ACTIVE = DALObj.ACTIVE;
                data.CODE = DALObj.CODE;
                data.LOID = DALObj.LOID;
                data.NAME = DALObj.NAME;
            }
            return data;
        }

        public bool UpdateData(string userID, TitleData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    DALObj.NAME = data.NAME.Trim();
                    DALObj.ACTIVE = data.ACTIVE.Trim();
                    DALObj.CODE = data.CODE.Trim();

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

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
