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
    public class DivisionFlow
    {
        private string _error = "";
        double _LOID = 0;
        private DivisionDAL _searchDAL;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        private DivisionDAL SearchDAL
        {
            get { if (_searchDAL == null) { _searchDAL = new DivisionDAL(); } return _searchDAL; }
        }

        public DataTable GetDataList()
        {
            return SearchDAL.GetDataList("", "CODE", null);
        }

        public DataTable GetDivisionList()
        {
            return SearchDAL.GetDivisionList();
        }

        public DivisionData GetData(double loid)
        {
            DivisionData data = new DivisionData();
            if (SearchDAL.GetDataByLOID(loid, null))
            {
                data.LOID = SearchDAL.LOID;
                data.CODE = SearchDAL.CODE;
                data.TNAME = SearchDAL.TNAME;
                data.EFDATE = SearchDAL.EFDATE;
                data.EPDATE = SearchDAL.EPDATE;
                data.ABBNAME = SearchDAL.ABBNAME;
                data.DEPARTMENT = SearchDAL.DEPARTMENT;
            }
            return data;
        }

        public bool UpdateData(string userID, DivisionData data)
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
                    SearchDAL.ABBNAME = data.ABBNAME;
                    SearchDAL.DEPARTMENT = data.DEPARTMENT;

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

        private bool VeridateData(DivisionData data)
        {
            bool ret = true;
            if (data.CODE.Trim() == "")
            {
                _error = "กรุณาระบุรหัสฝ่าย";
                ret = false;
            }
            else if (data.TNAME.Trim() == "")
            {
                _error = "กรุณาระบุชื่อภาษาไทย";
                ret = false;
            }
            else if (SearchDAL.CheckCode(data.LOID, data.CODE.Trim()) == false)
            {
                _error = "รหัสฝ่ายนี้มีอยู่แล้ว";
                ret = false;
            }
            else if (SearchDAL.CheckName(data.LOID, data.TNAME.Trim()) == false)
            {
                _error = "ชื่อภาษาไทยฝ่ายนี้มีอยู่แล้ว";
                ret = false;
            }
            else if (SearchDAL.CheckAbbName(data.LOID, data.ABBNAME.Trim()) == false)
            {
                _error = "ชื่อย่อฝ่ายนี้มีอยู่แล้ว";
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

