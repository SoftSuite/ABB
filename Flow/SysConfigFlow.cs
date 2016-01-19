using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data.Admin;
using ABB.DAL;

namespace ABB.Flow
{
    public class SysConfigFlow
    {
        string _error = "";
        SysConfigDAL _dal;

        public string ErrorMessage
        {
            get { return _error; }
        }
        public SysConfigDAL DALObj
        {
            get { if (_dal == null) { _dal = new SysConfigDAL(); } return _dal; }
        }

        public DataTable GetDataList()
        {
            return DALObj.GetDataList("", null);
        }

        private bool ValidateData(SysConfigData data)
        {
            bool ret = true;
            return ret;
        }

        public SysConfigData GetData(double loid)
        {
            SysConfigData data = new SysConfigData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CONFIGNAME = DALObj.CONFIGNAME;
                data.CONFIGVALUE = DALObj.CONFIGVALUE;
                data.DESCRIPTION = DALObj.DESCRIPTION;
            }
            return data;
        }

        public bool UpdateData(string userID, SysConfigData data)
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
                    DALObj.CONFIGNAME = data.CONFIGNAME.Trim();
                    DALObj.CONFIGVALUE = data.CONFIGVALUE;
                    DALObj.DESCRIPTION = data.DESCRIPTION.Trim();

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

        public static string GetValue(string ConfigName)
        {
            SysConfigDAL dal = new SysConfigDAL();
            SysConfigData data = new SysConfigData();
            if (dal.GetDataByName(ConfigName, null))
            {
                data.LOID = dal.LOID;
                data.CONFIGNAME = dal.CONFIGNAME;
                data.CONFIGVALUE = dal.CONFIGVALUE;
                data.DESCRIPTION = dal.DESCRIPTION;
            }
            return data.CONFIGVALUE;
        }
    }
}
