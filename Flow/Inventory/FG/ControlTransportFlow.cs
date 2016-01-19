using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Inventory.FG;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Inventory.FG;


namespace ABB.Flow.Inventory.FG
{
    public class ControlTransportFlow
    {
        string _error = "";
        double _LOID = 0;
        CtrlDeliveryDAL _dal;
        TransportDAL search;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public CtrlDeliveryDAL DALObj
        {
            get { if (_dal == null) { _dal = new CtrlDeliveryDAL(); } return _dal; }
        }

        public TransportDAL SearchDAL
        {
            get { if (search == null) search = new TransportDAL(); return search; }
        }

        public DataTable GetDeliveryList(CtrlDeliveryData data)
        {
            return SearchDAL.GetDeliveryList(data);
        }

        public CtrlDeliveryData GetData(double loid)
        {
            CtrlDeliveryData data = new CtrlDeliveryData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.CARNO = DALObj.CARNO;
                data.DELIVERYDATE = DALObj.DELIVERYDATE;
                data.DELIVERYNAME = DALObj.DELIVERYNAME;

            }
            return data;
        }

        public DataTable GetDeliveryItem(double ctrldelivery)
        {
            CtrlDeliveryItemDAL itemDAL = new CtrlDeliveryItemDAL();
            return SearchDAL.GetDeliveryItemList(ctrldelivery);
        }

        public DataTable GetDeliveryItemBlank()
        {
            CtrlDeliveryItemDAL itemDAL = new CtrlDeliveryItemDAL();
            return SearchDAL.GetDeliveryItemListBlank();
        }

        public bool ValidateData(CtrlDeliveryData data)
        {
            bool ret = true;

            //if (data.DUEDATE.Year == 1)
            //{
            //    ret = false;
            //    _error = "กรุณากำหนดวันที่บันทึกสั่งผลิต";
            //}
            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการ Invoice";
            }
            return ret;
        }

        public bool UpdateData(string userID, CtrlDeliveryData data)
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

                    //DALObj.CODE = data.CODE;
                    DALObj.CARNO = data.CARNO;
                    DALObj.DELIVERYDATE = data.DELIVERYDATE;
                    DALObj.DELIVERYNAME = data.DELIVERYNAME;

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }

                    CtrlDeliveryItemDAL itemDAL = new CtrlDeliveryItemDAL();
                    itemDAL.DeleteDataByCtrlDelivery(data.LOID, obj.zTrans);
                    for (Int16 i = 0; i < data.ITEM.Count; ++i)
                    {
                        CtrlDeliveryItemData item = (CtrlDeliveryItemData)data.ITEM[i];
                        itemDAL.BOXQTY = item.BOXQTY;
                        itemDAL.CADDRESS = item.CADDRESS;
                        itemDAL.CNAME = item.CNAME;
                        itemDAL.CONTACTNAME = item.CONTACTNAME;
                        itemDAL.CTEL = item.CTEL;
                        itemDAL.CTRLDELIVERY = DALObj.LOID;
                        itemDAL.REQUISITION = item.REQUISITION;


                        itemDAL.OnDB = false;
                        ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
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
                CtrlDeliveryItemDAL itemDAL = new CtrlDeliveryItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByCtrlDelivery(Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = DALObj.DeleteCurrentData(obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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


        public bool NewRequisition(string userID, CtrlDeliveryData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {

                DALObj.CODE = data.CODE;
                DALObj.DELIVERYDATE = data.DELIVERYDATE;
                DALObj.TYPE = data.TYPE;
                DALObj.CARNO = data.CARNO;
                DALObj.DELIVERYNAME = data.DELIVERYNAME;


                ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                _LOID = DALObj.LOID;
                if (!ret)
                {
                    throw new ApplicationException(DALObj.ErrorMessage);
                }

                CtrlDeliveryItemDAL itemDAL = new CtrlDeliveryItemDAL();
                itemDAL.DeleteDataByCtrlDelivery(_LOID, obj.zTrans);
                for (int i = 0; i < data.ITEM.Count; ++i)
                {
                    CtrlDeliveryItemData item = (CtrlDeliveryItemData)data.ITEM[i];
                    itemDAL.BOXQTY = item.BOXQTY;
                    itemDAL.CADDRESS = item.CADDRESS;
                    itemDAL.CNAME = item.CNAME;
                    itemDAL.CONTACTNAME = item.CONTACTNAME;
                    itemDAL.CTEL = item.CTEL;
                    itemDAL.CTRLDELIVERY = DALObj.LOID;
                    itemDAL.REQUISITION = item.REQUISITION;


                    itemDAL.OnDB = false;
                    ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
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

        public string GetInvCode(double loid)
        {
            TransportDAL TDal = new TransportDAL();
            return TDal.GetInvCode(loid);
        }

        public CtrlDeliveryItemData GetRequisition(double requisition, string invcode)
        {
            TransportDAL TDal = new TransportDAL();
            return TDal.GetRequisition(requisition, invcode);
        }

        public CtrlDeliveryItemData GetRequisition(string invcode)
        {
            TransportDAL TDal = new TransportDAL();
            return TDal.GetRequisition(0, invcode);
        }

    }
}

