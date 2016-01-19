using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.DAL;

namespace ABB.Flow.Sales
{
    public class CustomerTypeFlow
    {
        string _error = "";
        MemberTypeDAL _dal;

        public string ErrorMessage
        {
            get { return _error; }
        }
        public MemberTypeDAL DALObj
        {
            get { if (_dal == null) { _dal = new MemberTypeDAL(); } return _dal; }
        }

        public DataTable GetDataList()
        {
            return DALObj.GetDataList("", null);
        }

        private bool VeridateData(CustomerTypeSearchData data)
        {
            bool ret = true;
            if (data.CODE.Trim() == "")
            {
                _error = "กรุณาระบุรหัสประเภทลูกค้า";
                ret = false;
            }
            else if (data.NAME.Trim() == "")
            {
                _error = "กรุณาระบุชื่อประเภทลูกค้า";
                ret = false;
            }
            return ret;
        }

        public CustomerTypeSearchData GetData(double loid)
        {
            CustomerTypeSearchData data = new CustomerTypeSearchData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.ACTIVE = DALObj.ACTIVE;
                data.CODE = DALObj.CODE;
                data.LOID = DALObj.LOID;
                data.NAME = DALObj.NAME;
                data.DISCOUNT = DALObj.DISCOUNT;
                data.LOWERPRICE = DALObj.LOWERPRICE;
                data.DESCRIPTION = DALObj.DESCRIPTION;
            }
            return data;
        }

        public bool UpdateData(string userID, CustomerTypeSearchData data)
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
                    DALObj.LOID = data.LOID;
                    DALObj.NAME = data.NAME.Trim();
                    DALObj.DESCRIPTION = data.DESCRIPTION.Trim();
                    DALObj.ACTIVE = data.ACTIVE.Trim();
                    DALObj.CODE = data.CODE.Trim();
                    DALObj.DISCOUNT = data.DISCOUNT;
                    DALObj.LOWERPRICE = data.LOWERPRICE;

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    if (ret)
                    {
                        obj.zTrans.Commit();
                        obj.CloseConnection();
                    }
                    else
                    {
                        _error= DALObj.ErrorMessage;
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
                    DALObj.LOID = Convert.ToDouble(arrData[i]);
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
