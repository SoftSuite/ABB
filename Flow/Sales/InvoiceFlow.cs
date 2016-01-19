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
    public class InvoiceFlow
    {
        string _error = "";
        double _LOID = 0;
        RequisitionDAL _dal;
        InvoiceDAL search;

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

        public InvoiceDAL SearchDAL
        {
            get { if (search == null) search = new InvoiceDAL(); return search; }
        }

        public double RequisitionLOID(string code)
        {
            return SearchDAL.GetRequisitionLOID(code);
        }

        public double RequisitionLOID2(string invcode)
        {
            return SearchDAL.GetRequisitionLOID2(invcode);
        }

        public DataTable GetRequisitionList(ProductReserveSearchData data)
        {
            return SearchDAL.GetInvoiceList(data);
        }

        public string GetRefTypeTable(double reftype)
        {
            return SearchDAL.GetRefTypeTable(reftype);
        }

        public ProductReserveData GetData(double loid)
        {
            ProductReserveData data = new ProductReserveData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                data.ACTIVE = DALObj.ACTIVE;
                data.CADDRESS = DALObj.CADDRESS;
                data.CEMAIL = DALObj.CEMAIL;
                data.CFAX = DALObj.CFAX;
                data.CLASTNAME = DALObj.CLASTNAME;
                data.CNAME = DALObj.CNAME;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.CTEL = DALObj.CTEL;
                data.CTITLE = DALObj.CTITLE;
                data.CUSTOMER = DALObj.CUSTOMER;
                data.DUEDATE = DALObj.DUEDATE;
                data.GRANDTOT = DALObj.GRANDTOT;
                data.REMARK = DALObj.REMARK;
                data.REQDATE = DALObj.REQDATE;
                data.REQUISITIONTYPE = DALObj.REQUISITIONTYPE;
                data.STATUS = DALObj.STATUS;
                data.TOTAL = DALObj.TOTAL;
                data.TOTDIS = DALObj.TOTDIS;
                data.TOTVAT = DALObj.TOTVAT;
                data.VAT = DALObj.VAT;
                data.WAREHOUSE = DALObj.WAREHOUSE;
                data.INVCODE = DALObj.INVCODE;
                data.PAYMENTCONDITION = DALObj.PAYMENTCONDITION;
                data.CREDITDATE = DALObj.CREDITDATE;
                data.RESERVEDATE = DALObj.RESERVEDATE;
                data.CDELIVERY = DALObj.DELIVERYTYPE;
                data.OTHER = DALObj.OTHER;
                data.PAYMENT = DALObj.PAYMENT;
                data.CREDITCARDID = DALObj.CREDITCARDID;
                data.CREDITTYPE = DALObj.CREDITTYPE;
                data.BANK = DALObj.BANK;
                data.BANKBRANCH = DALObj.BANKBRANCH;
                data.CHEQUE = DALObj.CHEQUE;
                data.CHEQUEDATE = DALObj.CHEQUEDATE;
                data.REASON = DALObj.REASON;
                data.REFTYPELOID = DALObj.REFTYPELOID;
                data.REFNO = DALObj.REFNO;
            }
            return data;
        }

        public DataTable GetProductItemList(string popup, string warehouse, string customer, string requisitiontype)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            DataTable dt = SearchDAL.GetProductItemList(popup,warehouse,customer, requisitiontype);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["RANK"] = i + 1;
            }
            return dt;
        }

        public DataTable GetRequisitionItem(double requisition, string warehouse)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            DataTable dt = SearchDAL.GetPurchaseItemList(requisition, warehouse);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["RANK"] = i + 1;
            }
            return dt;
        }

        public DataTable GetRequisitionItemBlank()
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return SearchDAL.GetPurchaseItemListBlank();
        }

        public string GetUsedProduct(double loid)
        {
            return SearchDAL.GetUsedProduct(loid);
        }

        public bool ValidateData(ProductReserveData data)
        {
            bool ret = true;
            if (data.CUSTOMER == 0)
            {
                ret = false;
                _error = "กรุณาระบุลูกค้า";
            }
            else if (data.DUEDATE.Year == 1)
            {
                ret = false;
                _error = "กรุณากำหนดวันที่ส่งสินค้า";
            }
            //else if (data.CREDITDATE.Year == 1)
            //{
            //    ret = false;
            //    _error = "กรุณากำหนดวันที่ครบกำหนดชำระ";
            //}
            else if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            return ret;
        }

        private void UpdateData(string userID, ProductReserveData data, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;
            DALObj.GetDataByLOID(data.LOID, zTrans);

            DALObj.ACTIVE = data.ACTIVE;
            DALObj.CADDRESS = data.CADDRESS;
            DALObj.CEMAIL = data.CEMAIL;
            DALObj.CFAX = data.CFAX;
            DALObj.CLASTNAME = data.CLASTNAME;
            DALObj.CNAME = data.CNAME;
           // DALObj.CODE = data.CODE;
            DALObj.CTEL = data.CTEL;
            DALObj.CTITLE = data.CTITLE;
            DALObj.CUSTOMER = data.CUSTOMER;
            DALObj.DUEDATE = data.DUEDATE;
            DALObj.GRANDTOT = data.GRANDTOT;
            DALObj.REMARK = data.REMARK;
            DALObj.REQDATE = data.REQDATE;
            //DALObj.REQUISITIONTYPE = data.REQUISITIONTYPE;
            DALObj.REFTYPELOID = data.REFTYPELOID;
            DALObj.REFTYPETABLE = data.REFTYPETABLE;
            DALObj.STATUS = data.STATUS;
            DALObj.TOTAL = data.TOTAL;
            DALObj.TOTDIS = data.TOTDIS;
            DALObj.TOTVAT = data.TOTVAT;
            DALObj.VAT = data.VAT;
            DALObj.WAREHOUSE = data.WAREHOUSE;
            DALObj.DELIVERYTYPE = data.CDELIVERY;
            DALObj.OTHER = data.OTHER;
            if (DALObj.INVCODE == "")
                DALObj.INVCODE = DALObj.GetInvCode(1, zTrans);
            DALObj.PAYMENTCONDITION = data.PAYMENTCONDITION;
            DALObj.CREDITDATE = data.CREDITDATE;
            DALObj.RESERVEDATE = data.RESERVEDATE;
            DALObj.OTHER = data.OTHER;
            DALObj.PAYMENT = data.PAYMENT;
            DALObj.CREDITCARDID = data.CREDITCARDID;
            DALObj.CREDITTYPE = data.CREDITTYPE;
            DALObj.BANK = data.BANK;
            DALObj.BANKBRANCH = data.BANKBRANCH;
            DALObj.CHEQUE = data.CHEQUE;
            DALObj.CHEQUEDATE = data.CHEQUEDATE;
            DALObj.REASON = data.REASON;
            DALObj.REFNO = data.REFNO;

            if (DALObj.OnDB)
                ret = DALObj.UpdateCurrentData(userID, zTrans);
            else
                DALObj.REQUISITIONTYPE = 11;
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
                itemDAL.DUEDATE = data.DUEDATE;

                itemDAL.OnDB = false;
                ret = itemDAL.InsertCurrentData(userID, zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
            }
        }

        public bool CommitData(string userID, ProductReserveData data)
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

        public bool UpdateData(string userID, ProductReserveData data)
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

        public bool NewRequisition(string userID, ProductReserveData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.ACTIVE = data.ACTIVE;
                DALObj.REQUISITIONTYPE = data.REQUISITIONTYPE;
                DALObj.STATUS = data.STATUS;
                DALObj.WAREHOUSE = data.WAREHOUSE;

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
            return ret;
        }

        public bool UpdateRequisitionStatus(ArrayList arrData, string status, string warehouse, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    if (status == Constz.Requisition.Status.Finish.Code)
                    {
                        if (GetRequisitionItem(Convert.ToDouble(arrData[i]),warehouse).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                        ProductReserveData data = GetData(Convert.ToDouble(arrData[i]));
                        if (data.CUSTOMER == 0) throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุลูกค้า");
                        if (data.DUEDATE.Year == 1) throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้กำหนดวันที่ส่งสินค้า");
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

