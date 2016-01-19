using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Purchase;
using ABB.Data.Sales;
using ABB.DAL;

namespace ABB.Flow.Purchase
{
    public class PDReturnFlow
    {
        string _error = "";
        double _LOID = 0;
        PDReturnDAL _dal;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public PDReturnDAL DALObj
        {
            get { if (_dal == null) { _dal = new PDReturnDAL(); } return _dal; }
        }

        //public TransportDAL SearchDAL
        //{
        //    get { if (search == null) search = new TransportDAL(); return search; }
        //}

        public DataTable GetPDReturnList(ProductReturnData data)
        {
            return DALObj.GetPDReturnList(data);
        }

        public DataTable GetPDReturnItemList(double pdreturn)
        {
            return DALObj.GetPDReturnItemList(pdreturn);
        }

        public DataTable GetStockoutItemList(double pdreturn)
        {
            return DALObj.GetStockoutItemList(pdreturn);
        }

        public ProductReturnData GetData(double loid)
        {
            ProductReturnData data = new ProductReturnData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.CADDRESS = DALObj.CADDRESS;
                data.CFAX = DALObj.CFAX;
                data.CNAME = DALObj.CNAME;
                data.CTEL = DALObj.CTEL;
                data.PDRETURNDATE = DALObj.PDRETURNDATE;
                data.REASON = DALObj.REASON;
                data.REFLOID = DALObj.REFLOID;
                data.REMARK = DALObj.REMARK;
                data.STATUS = DALObj.STATUS;
                data.SUPPLIER = DALObj.SUPPLIER;

            }
            return data;
        }

        public DataTable GetPDReturnItem(double pdreturn)
        {
            PDReturnItemDAL itemDAL = new PDReturnItemDAL();
            return DALObj.GetPDReturnItemList(pdreturn);
        }

        public DataTable GetPDReturnItemBlank()
        {
            CtrlDeliveryItemDAL itemDAL = new CtrlDeliveryItemDAL();
            return DALObj.GetPDReturnItemListBlank();
        }

        public bool ValidateData(ProductReturnData data)
        {
            bool ret = true;

            if (data.REFLOID == 0)
            {
                ret = false;
                _error = "กรุณาเลือกใบแจ้งคืนสินค้า/วัตถุดิบ";
            }
            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการที่ต้องการส่งคืน";
            }
            return ret;
        }

        private void UpdateData(string userID, ProductReturnData data, System.Data.OracleClient.OracleTransaction trans)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                DALObj.ACTIVE = data.ACTIVE;
                DALObj.STATUS = data.STATUS;
                DALObj.SUPPLIER = data.SUPPLIER;
                DALObj.CADDRESS = data.CADDRESS;
                DALObj.CFAX = data.CFAX;
                DALObj.CNAME = data.CNAME;
                DALObj.CTEL = data.CTEL;
                DALObj.PDRETURNDATE = data.PDRETURNDATE;
                DALObj.REASON = data.REASON;
                DALObj.REFLOID = data.REFLOID;
                DALObj.REFTABLE = data.REFTABLE;
                DALObj.REMARK = data.REMARK;
                DALObj.TYPE = data.TYPE;

                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, trans);
                else
                    ret = DALObj.InsertCurrentData(userID, trans);

                _LOID = DALObj.LOID;
                if (!ret)
                {
                    throw new ApplicationException(DALObj.ErrorMessage);
                }

                PDReturnItemDAL itemDAL = new PDReturnItemDAL();
                itemDAL.DeleteDataByPDReturn(data.LOID, trans);
                for (Int16 i = 0; i < data.ITEM.Count; ++i)
                {
                    ProductReturnItemData item = (ProductReturnItemData)data.ITEM[i];
                    itemDAL.ACTIVE = Constz.ActiveStatus.Active;
                    itemDAL.PDRETURN = DALObj.LOID;
                    itemDAL.PRODUCT = item.PRODUCT;
                    itemDAL.PRICE = item.PRICE;
                    itemDAL.QTY = item.QTY;
                    itemDAL.REFLOID = item.LOID;
                    itemDAL.REFTABLE = "STOCKOUTITEM";
                    itemDAL.UNIT = item.UNIT;
                    itemDAL.STATUS = Constz.Requisition.Status.Waiting.Code;
                    itemDAL.LOTNO = item.LOTNO;

                    itemDAL.OnDB = false;
                    ret = itemDAL.InsertCurrentData(userID, trans);
                    if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
                }
            }
            else
                throw new ApplicationException(_error);
        }

        public bool UpdateData(string userID, ProductReturnData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                UpdateData(userID, data, obj.zTrans);
                
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

        public bool CommitData(string userID, ProductReturnData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                {
                    UpdateData(userID, data, obj.zTrans);
                    ret = DALObj.CutStockPDReturn(DALObj.LOID, userID, obj.zTrans);
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

        public bool DeleteData(ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PDReturnItemDAL itemDAL = new PDReturnItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByPDReturn(Convert.ToDouble(arrData[i]), obj.zTrans);
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

        public bool UpdatePDReturnStatus(ArrayList arrData, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    if (status == Constz.Requisition.Status.Approved.Code)
                    {
                        if (DALObj.GetPDReturnItemList(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                        ProductReturnData data = GetData(Convert.ToDouble(arrData[i]));
                        if (data.REASON == "") throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุเหตุผลในการขอซื้อ");
                    }
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALObj.STATUS = status;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = DALObj.CutStockPDReturn(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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

        public ArrayList GetItemList(double loid)
        {
            DataTable dt = GetStockoutItemList(loid);
            ArrayList arr = new ArrayList();
            if (dt != null)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    ProductReturnItemData data = new ProductReturnItemData();
                    data.ACTIVE = Constz.ActiveStatus.Active;
                    data.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                    data.LOID = Convert.ToDouble(dRow["LOID"]);
                    data.NETPRICE = Convert.ToDouble(dRow["NETPRICE"]);
                    data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                    data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                    data.QTY = Convert.ToDouble(dRow["QTY"]);
                    data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                    data.LOTNO = dRow["LOTNO"].ToString();
                    arr.Add(data);
                }
            }
            return arr;
        }

        //public bool NewRequisition(string userID, ProductReturnData data)
        //{
        //    bool ret = true;
        //    OracleDBObj obj = new OracleDBObj();
        //    obj.CreateConnection();
        //    obj.CreateTransaction();
        //    try
        //    {

        //        DALObj.CODE = data.CODE;
        //        DALObj.DELIVERYDATE = data.DELIVERYDATE;
        //        DALObj.TYPE = data.TYPE;
        //        DALObj.CARNO = data.CARNO;
        //        DALObj.DELIVERYNAME = data.DELIVERYNAME;


        //        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

        //        _LOID = DALObj.LOID;
        //        if (!ret)
        //        {
        //            throw new ApplicationException(DALObj.ErrorMessage);
        //        }

        //        obj.zTrans.Commit();
        //        obj.CloseConnection();
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.zTrans.Rollback();
        //        obj.CloseConnection();
        //        ret = false;
        //        _error = ex.Message;
        //    }
        //    return ret;
        //}

        public SupplierData GetSupplierData(double supplier)
        {
            SupplierDAL sDAL = new SupplierDAL();
            sDAL.GetDataByLOID(supplier, null);
            SupplierData data = new SupplierData();
            data.SUPPLIERNAME = sDAL.SUPPLIERNAME;
            data.CNAME = sDAL.CNAME;
            data.CLASTNAME = sDAL.CLASTNAME;
            data.CADDRESS = sDAL.CADDRESS;
            data.CTEL = sDAL.CTEL;
            data.CFAX = sDAL.CFAX;
            //return DALObj.GetSupplierData(supplier);
            return data;
        }
        public DateTime GetSTDate(double loid)
        {
            return DALObj.GetSTDate(loid);
        }
        public string GetSTCode(double loid)
        {
            return DALObj.GetSTCode(loid);
        }

    }
}


