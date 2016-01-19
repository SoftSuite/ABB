using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.DAL.Admin;
using ABB.Data.Admin;

namespace ABB.Flow.Admin
{
    public class PositionFlow
    {
        private string _error = "";
        double _LOID = 0;
        private PositionDAL _searchDAL;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        private PositionDAL SearchDAL
        {
            get { if (_searchDAL == null) { _searchDAL = new PositionDAL(); } return _searchDAL; }
        }

        public DataTable GetDataList()
        {
            return SearchDAL.GetDataList("", "CODE", null);
        }

        public PositionData GetData(double loid)
        {
            PositionData data = new PositionData();
            if (SearchDAL.GetDataByLOID(loid, null))
            {
                data.LOID = SearchDAL.LOID;
                data.CODE = SearchDAL.CODE;
                data.NAME = SearchDAL.NAME;
            }
            return data;
        }

        public bool UpdateData(string userID, PositionData data)
        {
            bool ret = true;
            if (VeridateData(data))
            {

                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    SearchDAL.GetDataByLOID(data.LOID, obj.zTrans);
                    SearchDAL.LOID = data.LOID;
                    SearchDAL.NAME = data.NAME.Trim();
                    SearchDAL.CODE = data.CODE.Trim();

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

        private bool VeridateData(PositionData data)
        {
            bool ret = true;
            if (data.CODE.Trim() == "")
            {
                _error = "กรุณาระบุรหัส";
                ret = false;
            }
            else if (data.NAME.Trim() == "")
            {
                _error = "กรุณาระบุชื่อหน่วยงาน";
                ret = false;
            }
            else if (SearchDAL.CheckCode(data.LOID, data.CODE.Trim()) == false)
            {
                _error = "รหัสหน่วยงานนี้มีอยู่แล้ว";
                ret = false;
            }
            else if (SearchDAL.CheckName(data.LOID, data.NAME.Trim()) == false)
            {
                _error = "ชื่อหน่วยงานนี้มีอยู่แล้ว";
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
                    SearchDAL.LOID = Convert.ToDouble(arrData[i]);
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
