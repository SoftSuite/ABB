using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.DAL;

namespace ABB.Flow.Sales
{
    public class CreditCardFlow
    {
        string _error = "";
        CreditCardDAL _dal;

        public string ErrorMessage
        {
            get { return _error; }
        }
        public CreditCardDAL DALObj
        {
            get { if (_dal == null) { _dal = new CreditCardDAL(); } return _dal; }
        }

        public DataTable GetDataList()
        {
            return DALObj.GetDataList("", null);
        }

        private bool ValidateData(CreditCardData data)
        {
            bool ret = true;
            if (data.NAME.Trim() == "")
            {
                _error = "กรุณาระบุชื่อประเภทบัตรเครดิต";
            }
            return ret;
        }

        public CreditCardData GetData(double loid)
        {
            CreditCardData data = new CreditCardData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.ACTIVE = DALObj.ACTIVE;
                data.LOID = DALObj.LOID;
                data.NAME = DALObj.NAME;
                data.CHARGE = DALObj.CHARGE;
            }
            return data;
        }

        public bool UpdateData(string userID, CreditCardData data)
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
                    DALObj.CHARGE = data.CHARGE;
                    DALObj.ACTIVE = data.ACTIVE.Trim();

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
