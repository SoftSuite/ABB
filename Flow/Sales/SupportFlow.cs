using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using ABB.DAL;
using ABB.Data;
using ABB.Data.Sales;

namespace ABB.Flow.Sales
{
    public class SupportFlow
    {
        string _error = "";
        double _LOID = 0;
        RequisitionDAL _dal;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public RequisitionDAL DALObj
        {
            get { if (_dal == null) { _dal = new RequisitionDAL(); } return _dal; }
        }

        public bool CheckProductStock(double warehouse, double product, double quantity)
        {
            bool ret = true;
            DAL.Sales.SaleDAL stockDAL = new DAL.Sales.SaleDAL();
            if (quantity > stockDAL.GetProductStockQty(product, warehouse, Constz.Zone.Z11))
                ret = false;
            return ret;
        }

        public DataTable GetRequisitionItem(double requisition)
        {
            return ABB.DAL.Sales.PointOfSaleDAL.GetItemList(requisition);
        }

        //public PointOfSaleRefData GetRefRequisitionItem(double refRequisition)
        //{
        //    PointOfSaleRefData data = new PointOfSaleRefData();
        //    DataTable dt = ABB.DAL.Sales.PointOfSaleDAL.GetRefRequisition(refRequisition);
        //    foreach (DataRow dRow in dt.Rows)
        //    {
        //        data.REQUISITION = Convert.ToDouble(dRow["LOID"]);
        //        data.REQUISITIONCODE = dRow["REQUISITIONCODE"].ToString();
        //        data.CUSTOMER = Convert.ToDouble(dRow["CUSTOMER"]);
        //        data.CUSTOMERCODE = dRow["CODE"].ToString();
        //        data.CUSTOMERDISCOUNT = Convert.ToDouble(dRow["CUSTOMERDISCOUNT"]);
        //    }
        //    DataTable dtItem = ABB.DAL.Sales.PointOfSaleDAL.GetRefRequisitionItem(refRequisition);
        //    foreach (DataRow dRow in dtItem.Rows)
        //    {
        //        PointOfSaleRefItemData itemData = new PointOfSaleRefItemData();
        //        itemData.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
        //        itemData.BARCODE = dRow["BARCODE"].ToString();
        //        itemData.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
        //        itemData.ISVAT = dRow["ISVAT"].ToString();
        //        itemData.NAME = dRow["NAME"].ToString();
        //        itemData.PRICE = Convert.ToDouble(dRow["PRICE"]);
        //        itemData.QTY = Convert.ToDouble(dRow["QTY"]);
        //        itemData.REFREQUISITIONITEM = Convert.ToDouble(dRow["LOID"]);
        //        itemData.UNIT = Convert.ToDouble(dRow["UNIT"]);
        //        itemData.UNITNAME = dRow["UNITNAME"].ToString();

        //        data.REFITEM.Add(itemData);
        //    }
        //    return data;
        //}

        public bool ValidateData(RequisitionData data)
        {
            bool ret = true;
            if (data.CUSTOMER == 0)
            {
                ret = false;
                _error = "กรุณาระบุลูกค้า";
            }
            else if (data.REQUISITIONITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            return ret;
        }

        public RequisitionData GetData(double loid)
        {
            RequisitionData data = new RequisitionData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.CODE = DALObj.CODE;
                data.ACTIVE = DALObj.ACTIVE;
                data.CUSTOMER = DALObj.CUSTOMER;
                data.GRANDTOT = DALObj.GRANDTOT;
                data.STATUS = DALObj.STATUS;
                data.TOTAL = DALObj.TOTAL;
                data.TOTDIS = DALObj.TOTDIS;
                data.TOTVAT = DALObj.TOTVAT;
                data.VAT = DALObj.VAT;
                data.WAREHOUSE = DALObj.WAREHOUSE;
                data.RESERVEDATE = DALObj.RESERVEDATE;
                data.CREATEBY = DALObj.CREATEBY;
                data.REASON = DALObj.REASON;
                data.REMARK = DALObj.REMARK;

                RequisitionItemDAL itemDAL = new RequisitionItemDAL();
                DataTable dt = GetRequisitionItem(loid);
                foreach (DataRow dRow in dt.Rows)
                {
                    RequisitionItemData itemData = new RequisitionItemData();
                    itemData.BarCode = dRow["BARCODE"].ToString();
                    itemData.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                    itemData.ISVAT = dRow["ISVAT"].ToString();
                    itemData.NETPRICE = Convert.ToDouble(dRow["NETPRICE"]);
                    itemData.PRICE = Convert.ToDouble(dRow["PRICE"]);
                    itemData.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                    itemData.QTY = Convert.ToDouble(dRow["QTY"]);
                    itemData.ProductName = dRow["NAME"].ToString();
                    itemData.UNIT = Convert.ToDouble(dRow["UNIT"]);
                    itemData.UnitName = dRow["UNITNAME"].ToString();
                    data.REQUISITIONITEM.Add(itemData);
                }
            }
            return data;
        }

        private void UpdateData(string userID, RequisitionData data, OracleTransaction trans)
        {
            bool ret = true;
            DALObj.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ04;
            DALObj.ACTIVE = data.ACTIVE;
            DALObj.CUSTOMER = data.CUSTOMER;
            DALObj.GRANDTOT = data.GRANDTOT;
            DALObj.STATUS = data.STATUS;
            DALObj.TOTAL = data.TOTAL;
            DALObj.TOTDIS = data.TOTDIS;
            DALObj.TOTVAT = data.TOTVAT;
            DALObj.VAT = data.VAT;
            DALObj.WAREHOUSE = data.WAREHOUSE;
            DALObj.RESERVEDATE = data.RESERVEDATE;
            DALObj.REMARK = data.REMARK;
            DALObj.REASON = data.REASON;

            if (DALObj.OnDB)
                ret = DALObj.UpdateCurrentData(userID, trans);
            else
            {
                DALObj.REQDATE = data.RESERVEDATE;
                ret = DALObj.InsertCurrentData(userID, trans);
            }

            if (!ret)
            {
                throw new ApplicationException(DALObj.ErrorMessage);
            }

            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            itemDAL.DeleteDataByRequisition(data.LOID, trans);
            for (Int16 i = 0; i < data.REQUISITIONITEM.Count; ++i)
            {
                RequisitionItemData item = (RequisitionItemData)data.REQUISITIONITEM[i];
                itemDAL.ACTIVE = item.ACTIVE;
                itemDAL.DISCOUNT = item.DISCOUNT;
                itemDAL.DUEDATE = item.DUEDATE;
                itemDAL.NETPRICE = item.NETPRICE;
                itemDAL.PRODUCT = item.PRODUCT;
                itemDAL.QTY = item.QTY;
                itemDAL.PRICE = item.PRICE;
                itemDAL.REQUISITION = DALObj.LOID;
                itemDAL.UNIT = item.UNIT;
                itemDAL.ISVAT = item.ISVAT;

                itemDAL.OnDB = false;
                ret = itemDAL.InsertCurrentData(userID, trans);
                if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
            }
        }

        public bool UpdateData(string userID, RequisitionData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    _dal = new RequisitionDAL();
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    UpdateData(userID, data, obj.zTrans);
                    _LOID = DALObj.LOID;

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

        public bool CommitData(string userID, RequisitionData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    _dal = new RequisitionDAL();
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        data.STATUS = Constz.Requisition.Status.Approved.Code;
                        UpdateData(userID, data, obj.zTrans);
                        _LOID = DALObj.LOID;

                        ret = DALObj.CutStockRequisition(DALObj.LOID, userID, obj.zTrans);
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
            }
            else
                ret = false;
            return ret;
        }

        public bool CommitData(string userID, ArrayList arrLOID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrLOID.Count; ++i)
                {
                    _dal = new RequisitionDAL();
                    double loid = Convert.ToDouble(arrLOID[i]);
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(loid, obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = DALObj.CutStockRequisition(DALObj.LOID, userID, obj.zTrans);
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

        public bool DeleteData(string userID, ArrayList arrLOID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                RequisitionItemDAL itemDAL;
                for (int i = 0; i < arrLOID.Count; ++i)
                {
                    _dal = new RequisitionDAL();
                    double loid = Convert.ToDouble(arrLOID[i]);
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(loid, obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        itemDAL = new RequisitionItemDAL();
                        ret = itemDAL.DeleteDataByRequisition(loid, obj.zTrans);
                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);

                        ret = DALObj.DeleteCurrentData(obj.zTrans);
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

    }
}
