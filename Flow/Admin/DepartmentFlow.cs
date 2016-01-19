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
    public class DepartmentFlow
    {
        private string _error = "";
        double _LOID = 0;
        private DepartmentDAL _searchDAL;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        private DepartmentDAL SearchDAL
        {
            get { if (_searchDAL == null) { _searchDAL = new DepartmentDAL(); } return _searchDAL; }
        }

        public DataTable GetDataList()
        {
            return SearchDAL.GetDataList("","CODE",null);
        }

        public DepartmentData GetData(double loid)
        {
            DepartmentData data = new DepartmentData();
            if (SearchDAL.GetDataByLOID(loid, null))
            {
                data.LOID = SearchDAL.LOID;
                data.CODE = SearchDAL.CODE;
                data.TNAME = SearchDAL.TNAME;
            }
            return data;
        }

        public bool UpdateData(string userID, DepartmentData data)
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
                    SearchDAL.TNAME = data.TNAME.Trim();
                    SearchDAL.CODE = data.CODE.Trim();
                    SearchDAL.EFDATE = data.EFDATE;
                    SearchDAL.EPDATE = data.EPDATE;

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

        private bool VeridateData(DepartmentData data)
        {
            bool ret = true;
            if (data.CODE.Trim() == "")
            {
                _error = "กรุณาระบุรหัส";
                ret = false;
            }
            else if (data.TNAME.Trim() == "")
            {
                _error = "กรุณาระบุชื่อหน่วยงาน";
                ret = false;
            }
            else if (SearchDAL.CheckCode(data.LOID, data.CODE.Trim()) == false)
            {
                _error = "รหัสหน่วยงานนี้มีอยู่แล้ว";
                ret = false;
            }
            else if (SearchDAL.CheckName(data.LOID, data.TNAME.Trim()) == false)
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
