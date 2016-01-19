using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Sales;
using ABB.Flow.Admin;
namespace ABB.Flow.Sales
{
    public class ProductRequestInFlow
    {
        string _error = "";
        double _LOID = 0;
        RequisitionDAL _dal;
        ProductRequestInDAL search;

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

        public ProductRequestInDAL SearchDAL
        {
            get { if (search == null) search = new ProductRequestInDAL(); return search; }
        }

        public DataTable GetRequisitionList(ProductOrderSearchData data)
        {
            DataTable dt= SearchDAL.GetOrderList(data);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = (i + 1);
            }
            return dt;
        }

        public ProductOrderData GetData(double loid)
        {
            ProductOrderData data = new ProductOrderData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.ACTIVE = DALObj.ACTIVE;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.CUSTOMER = DALObj.CUSTOMER;
                data.REMARK = DALObj.REMARK;
                data.RESERVEDATE = DALObj.RESERVEDATE;
                data.REQUISITIONTYPE = DALObj.REQUISITIONTYPE;
                data.STATUS = DALObj.STATUS;
                data.TOTAL = DALObj.TOTAL;
                data.WAREHOUSE = DALObj.WAREHOUSE;
                data.REFWAREHOUSE = DALObj.REFWAREHOUSE;
                data.TOTDIS = DALObj.TOTDIS;
                data.TOTVAT = DALObj.TOTVAT;
                data.GRANDTOT = DALObj.GRANDTOT;
                WarehouseDAL itemDAL = new WarehouseDAL();
                itemDAL.GetDataByLOID(DALObj.WAREHOUSE, null);
                data.WAREHOUSENAME = itemDAL.NAME;
            }
            return data;
        }

        //public DataTable GetRequisitionItem(double requisition)
        //{
        //    RequisitionItemDAL itemDAL = new RequisitionItemDAL();
        //    return SearchDAL.GetOrderItemList(requisition);
        //}

        public DataTable GetRequisitionItem(double requisition)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return SearchDAL.GetReserveItemList(requisition);
        }
        public DataTable GetRequisitionItemBlank()
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return SearchDAL.GetReserveItemListBlank();
        }
        //public DataTable GetRequisitionItemBlank()
        //{
        //    RequisitionItemDAL itemDAL = new RequisitionItemDAL();
        //    return SearchDAL.GetOrderItemListBlank();
        //}

        public bool ValidateData(ProductOrderData data)
        {
            bool ret = true;

            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            return ret;
        }

        private void UpdateData(string userID, ProductOrderData data, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;
            DALObj.GetDataByLOID(data.LOID, zTrans);

            DALObj.ACTIVE = data.ACTIVE;
            DALObj.CODE = data.CODE;
            DALObj.CUSTOMER = data.CUSTOMER;
            DALObj.REMARK = data.REMARK;
            DALObj.RESERVEDATE = data.RESERVEDATE;
            DALObj.REQDATE = data.RESERVEDATE;
            DALObj.REQUISITIONTYPE = data.REQUISITIONTYPE;
            DALObj.STATUS = data.STATUS;
            DALObj.TOTAL = data.TOTAL;
            DALObj.WAREHOUSE = data.WAREHOUSE;
            DALObj.REFWAREHOUSE = data.REFWAREHOUSE;

            if (DALObj.OnDB)
                ret = DALObj.UpdateCurrentData(userID, zTrans);
            else
                ret = DALObj.InsertCurrentData(userID, zTrans);

            _LOID = DALObj.LOID;
            if (!ret)
            {
                throw new ApplicationException(DALObj.ErrorMessage);
            }

            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            itemDAL.DeleteDataByRequisition(data.LOID, zTrans);
            for (Int16 i = 0; i < data.ITEM.Count; ++i)
            {
                RequisitionItemData item = (RequisitionItemData)data.ITEM[i];
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
                //itemDAL.DUEDATE = data.DUEDATE;

                itemDAL.OnDB = false;
                ret = itemDAL.InsertCurrentData(userID, zTrans);
                if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
            }
        }

        public bool CommitData(string userID, ProductOrderData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    UpdateData(userID, data, obj.zTrans);

                    ret = DALObj.CutStockRequisition(_LOID, userID, obj.zTrans);
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

        public bool UpdateData(string userID, ProductOrderData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
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
                RequisitionItemDAL itemDAL = new RequisitionItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByRequisition(Convert.ToDouble(arrData[i]), obj.zTrans);
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

        public bool CopyRequisition(string userID, double loidSource)
        {
            ProductOrderData data = GetData(loidSource);
            DataTable itemList = GetRequisitionItem(data.LOID);
            ArrayList arr = new ArrayList();
            foreach (DataRow dRow in itemList.Rows)
            {
                RequisitionItemData idata = new RequisitionItemData();
                idata.ACTIVE = dRow["ACTIVE"].ToString();
                //idata.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                //idata.DUEDATE = Convert.ToDateTime(dRow["DUEDATE"]);
                idata.NETPRICE = Convert.ToDouble(dRow["NETPRICE"]);
                idata.PRICE = Convert.ToDouble(dRow["PRICE"]);
                idata.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                idata.QTY = Convert.ToDouble(dRow["QTY"]);
                idata.UNIT = Convert.ToDouble(dRow["UNIT"]);
                arr.Add(idata);
            }
            data.ITEM = arr;
            DALObj.OnDB = false;
            data.LOID = 0;
            data.CODE = "";
            data.RESERVEDATE = DateTime.Today;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.ACTIVE = Constz.ActiveStatus.Active;
            return UpdateData(userID, data);
        }

        public bool UpdateRequisitionStatus(ArrayList arrData, string status,string userID)
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
                        if (GetRequisitionItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                    }
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    DALObj.STATUS = status;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    ret = DALObj.CutStockRequisition(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
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

        public ProductSearchData GetProductData(double loid)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(loid);
        }

        public ProductSearchData GetProductBarcode(double loid)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(loid);
        }

        public ProductSearchData GetProductData(string barcode)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(barcode);
        }

        public UnitSearchData GetUnitData(double loid)
        {
            UnitFlow uFlow = new UnitFlow();
            return uFlow.GetData(loid);
        }

        public MemberTypeData GetMemberTypeData(double loid)
        {
            MemberTypeFlow flow = new MemberTypeFlow();
            return flow.GetData(loid);
        }

        public TitleData GetTitleData(double loid)
        {
            TitleFlow flow = new TitleFlow();
            return flow.GetData(loid);
        }

        public CustomerData GetCustomerData(double loid)
        {
            CustomerDAL dal = new CustomerDAL();
            CustomerData data = new CustomerData();
            dal.GetDataByLOID(loid, null);
            data.ACTIVE = dal.ACTIVE;
            data.BILLADDRESS = dal.BILLADDRESS;
            data.BILLAMPHUR = dal.BILLAMPHUR;
            data.BILLEMAIL = dal.BILLEMAIL;
            data.BILLFAX = dal.BILLFAX;
            data.BILLPROVINCE = dal.BILLPROVINCE;
            data.BILLROAD = dal.BILLROAD;
            data.BILLTAMBOL = dal.BILLTAMBOL;
            data.BILLTEL = dal.BILLTEL;
            data.BILLZIPCODE = dal.BILLZIPCODE;
            data.CADDRESS = dal.CADDRESS;
            data.CAMPHUR = dal.CAMPHUR;
            data.CEMAIL = dal.CEMAIL;
            data.CFAX = dal.CFAX;
            data.CLASTNAME = dal.CLASTNAME;
            data.CMOBILE = dal.CMOBILE;
            data.CNAME = dal.CNAME;
            data.CODE = dal.CODE;
            data.CPROVINCE = dal.CPROVINCE;
            data.CREDITAMOUNT = dal.CREDITAMOUNT;
            data.CREDITDAY = dal.CREDITDAY;
            data.CROAD = dal.CROAD;
            data.CTAMBOL = dal.CTAMBOL;
            data.CTEL = dal.CTEL;
            data.CTITLE = dal.CTITLE;
            data.CUSTOMERTYPE = dal.CUSTOMERTYPE;
            data.CZIPCODE = dal.CZIPCODE;
            data.DELIVERTYPE = dal.DELIVERTYPE;
            data.EFDATE = dal.EFDATE;
            data.EPDATE = dal.EPDATE;
            data.IDENTITY = dal.IDENTITY;
            data.LASTNAME = dal.LASTNAME;
            data.MEMBERTYPE = dal.MEMBERTYPE;
            data.NAME = dal.NAME;
            data.PAYMENT = dal.PAYMENT;
            data.REMARK = dal.REMARK;
            data.SENDADDRESS = dal.SENDADDRESS;
            data.SENDAMPHUR = dal.SENDAMPHUR;
            data.SENDEMAIL = dal.SENDEMAIL;
            data.SENDFAX = dal.SENDFAX;
            data.SENDPLACE = dal.SENDPLACE;
            data.SENDPROVINCE = dal.SENDPROVINCE;
            data.SENDROAD = dal.SENDROAD;
            data.SENDTAMBOL = dal.SENDTAMBOL;
            data.SENDTEL = dal.SENDTEL;
            data.SENDZIPCODE = dal.SENDZIPCODE;
            data.TITLE = dal.TITLE;

            return data;
        }

    }
}
