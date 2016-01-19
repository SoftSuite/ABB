using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.DAL;
using ABB.Data;
using ABB.Data.Sales;

namespace ABB.Flow.Sales
{
    public class PointOfSaleFlow
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

        public PointOfSaleRefData GetRefRequisitionItem(double refRequisition)
        {
            PointOfSaleRefData data = new PointOfSaleRefData();
            DataTable dt = ABB.DAL.Sales.PointOfSaleDAL.GetRefRequisition(refRequisition);
            foreach (DataRow dRow in dt.Rows)
            {
                data.REQUISITION = Convert.ToDouble(dRow["LOID"]);
                data.REQUISITIONCODE = dRow["REQUISITIONCODE"].ToString();
                data.CUSTOMER = Convert.ToDouble(dRow["CUSTOMER"]);
                data.CUSTOMERCODE = dRow["CODE"].ToString();
                data.CUSTOMERDISCOUNT = Convert.ToDouble(dRow["CUSTOMERDISCOUNT"]);
            }
            DataTable dtItem = ABB.DAL.Sales.PointOfSaleDAL.GetRefRequisitionItem(refRequisition);
            foreach (DataRow dRow in dtItem.Rows)
            {
                PointOfSaleRefItemData itemData = new PointOfSaleRefItemData();
                itemData.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                itemData.BARCODE = dRow["BARCODE"].ToString();
                itemData.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                itemData.ISVAT = dRow["ISVAT"].ToString();
                itemData.NAME = dRow["NAME"].ToString();
                itemData.PRICE = Convert.ToDouble(dRow["PRICE"]);
                itemData.QTY = Convert.ToDouble(dRow["QTY"]);
                itemData.REFREQUISITIONITEM = Convert.ToDouble(dRow["LOID"]);
                itemData.UNIT = Convert.ToDouble(dRow["UNIT"]);
                itemData.UNITNAME = dRow["UNITNAME"].ToString();
                itemData.ISVAT = dRow["ISVAT"].ToString();
                itemData.ISDISCOUNT = dRow["ISDISCOUNT"].ToString();
                itemData.ISEDIT = dRow["ISEDIT"].ToString();

                data.REFITEM.Add(itemData);
            }
            return data;
        }

        public CreditCardData GetCreditCardData(double creditCardID)
        {
            CreditCardFlow flow = new CreditCardFlow();
            return flow.GetData(creditCardID);
        }

        public bool ValidateData(PointOfSaleData data)
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

        public PointOfSaleData GetData(double loid)
        {
            PointOfSaleData data = new PointOfSaleData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.INVCODE = DALObj.INVCODE;
                data.CODE = DALObj.CODE;
                data.ACTIVE = DALObj.ACTIVE;
                data.CASH = DALObj.CASH;
                data.COUPON = DALObj.COUPON;
                data.CREDITCARDID = DALObj.CREDITCARDID;
                data.CREDITCARDPAY = DALObj.CREDITCARDPAY;
                data.CREDITTYPE = DALObj.CREDITTYPE;
                data.CUSTOMER = DALObj.CUSTOMER;
                data.GRANDTOT = DALObj.GRANDTOT;
                data.REFLOID = DALObj.REFLOID;
                data.REFNO = DALObj.REFNO;
                data.REFTABLE = DALObj.REFTABLE;
                data.STATUS = DALObj.STATUS;
                data.TOTAL = DALObj.TOTAL;
                data.TOTDIS = DALObj.TOTDIS;
                data.TOTVAT = DALObj.TOTVAT;
                data.VAT = DALObj.VAT;
                data.WAREHOUSE = DALObj.WAREHOUSE;
                data.REQDATE = DALObj.REQDATE;
                data.CREATEBY = DALObj.CREATEBY;

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
                    data.REQUISITIONITEM.Add(itemData);
                }
            }
            return data;
        }

        public bool UpdateData(string userID, PointOfSaleData data, bool setInvoice)
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

                    DALObj.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ13;
                    DALObj.ACTIVE = data.ACTIVE;
                    DALObj.CASH = data.CASH;
                    DALObj.COUPON = data.COUPON;
                    DALObj.CREDITCARDID = data.CREDITCARDID;
                    DALObj.CREDITCARDPAY = data.CREDITCARDPAY;
                    DALObj.CREDITTYPE = data.CREDITTYPE;
                    DALObj.CUSTOMER = data.CUSTOMER;
                    DALObj.GRANDTOT = data.GRANDTOT;
                    DALObj.REFLOID = data.REFLOID;
                    DALObj.REFNO = data.REFNO;
                    DALObj.REFTABLE = data.REFTABLE;
                    DALObj.STATUS = data.STATUS;
                    DALObj.TOTAL = data.TOTAL;
                    DALObj.TOTDIS = data.TOTDIS;
                    DALObj.TOTVAT = data.TOTVAT;
                    DALObj.VAT = data.VAT;
                    DALObj.WAREHOUSE = data.WAREHOUSE;
                    DALObj.REQDATE = data.REQDATE;
                    if (setInvoice && DALObj.INVCODE != "") DALObj.INVCODE = OracleDB.GetRunningCode("REQUISITION_INVCODE", DALObj.REQUISITIONTYPE.ToString(), obj.zTrans);

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }

                    RequisitionItemDAL itemDAL = new RequisitionItemDAL();
                    itemDAL.DeleteDataByRequisition(data.LOID, obj.zTrans);
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
                        ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
                    }

                    ret = DALObj.CutStockRequisition(DALObj.LOID, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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
