using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ABB.Data.Production;
using System.Data.OracleClient;
using System.Data;
using ABB.DAL.Inventory;
using ABB.Data.Search;
using ABB.Data.Inventory;

namespace ABB.Flow.Inventory
{
    public class PopupProductSearchFlow
    {
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        PopupProductSearchDAL _dal;
        public PopupProductSearchDAL DALObj
        {
            get { if (_dal == null) { _dal = new PopupProductSearchDAL(); } return _dal; }
        }

        public DataTable GetDataList(string sWhere)
        {
            return DALObj.GetDataList(sWhere, null);
        }

        private bool VeridateData(StockinProductData data)
        {
            bool ret = true;
            if (data.LOTNO.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุเลขที่การผลิต";
            }
            else if (data.PRODUCTNAME.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุชื่อผลิตภัณฑ์";
            }
            return ret;
        }
        public StockinProductData GetData(double loid)
        {
            StockinProductData data = new StockinProductData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.PRODUCTNAME = DALObj.PRODUCTNAME;
                data.LOTNO = DALObj.LOTNO;
                data.LOID = DALObj.LOID;
                data.MFGDATE = DALObj.MFGDATE;
                data.PDQTY = DALObj.PDQTY;
                data.PRODUCT = DALObj.PRODUCT;
                data.UNIT = DALObj.UNIT;
                data.PDPRODUCT = DALObj.LOID;
            }
            return data;
        }

        public bool UpdateData(string userID,StockinProductData data)
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
                    DALObj.PRODUCT = data.PRODUCT;
                    DALObj.UNIT = data.UNIT;
                    DALObj.LOID = data.PDPRODUCT;
                    DALObj.LOTNO = data.LOTNO.Trim();
                    DALObj.PRODUCTNAME = data.PRODUCTNAME.Trim();

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
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    obj.zTrans.Rollback();
                    obj.CloseConnection();
                    ret = false;
                    _error = ex.Message;
                }
            }
            else ret = false;
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


