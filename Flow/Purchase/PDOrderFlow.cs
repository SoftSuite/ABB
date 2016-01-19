using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.DAL;
using ABB.Flow.Sales;
using ABB.Data.Sales;
using ABB.Data.Purchase;


namespace ABB.Flow.Purchase
{
    public class PDOrderFlow
    {
        double _LOID = 0;
        string _error = "";
        PDOrderDAL _dal;
        
        public PDOrderDAL DALObj
        {
            get { if (_dal == null) { _dal = new PDOrderDAL(); } return _dal; }
        }
        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public PurchaseOrderData GetData(double loid)
        {
            PurchaseOrderData data = new PurchaseOrderData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                data.SENDPODATE = DALObj.SENDPODATE;
                data.SENDPO = DALObj.SENDPO;
                data.SENDOTHER = DALObj.SENDOTHER;
                data.REFSUPPCODE = DALObj.REFSUPPCODE;
            }
            return data;
        }
        public bool ValidateData(PurchaseOrderData data)
        {
            bool ret = true;
            if (data.SENDPO == "0")
            {
                ret = false;
                _error = "กรุณาระบุวิธีการส่งใบสั่งซื้อ";
            }
            if (data.SENDPO == "OT" && data.SENDOTHER == "")
            {
                ret = false;
                _error = "กรุณาระบุวิธีการส่งใบสั่งซื้อ";
            }
            return ret;
        }

        public bool UpdateData(string userID, PurchaseOrderData data)
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

                    DALObj.CODE = data.CODE;
                    DALObj.SENDPODATE = data.SENDPODATE;
                    DALObj.SENDPO = data.SENDPO;
                    DALObj.SENDOTHER = data.SENDOTHER;
                    DALObj.REFSUPPCODE = data.REFSUPPCODE;

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

    }
     
}
