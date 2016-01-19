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
    public class ReturnRequestFlow
    {
        string _error = "";
        double _LOID = 0;
        string _code = "";
        RequisitionDAL _dal;
        ReturnRequestDAL search;

        public double LOID
        {
            get { return _LOID; }
        }
        public string code
        {
            get { return _code; }
        }
        public string ErrorMessage
        {
            get { return _error; }
        }

        public RequisitionDAL DALObj
        {
            get { if (_dal == null) { _dal = new RequisitionDAL(); } return _dal; }
        }

        public ReturnRequestDAL SearchObj
        {
            get { if (search == null) search = new ReturnRequestDAL(); return search; }
        }

        public DataTable GetRequisitionList(ProductReserveSearchData data)
        {
            return SearchObj.GetRequestList(data);
        }

        public DataTable GetRequestItemList(double requisition)
        {
            DataTable dt = SearchObj.GetRequestItemList(requisition);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["RANK"] = i;
                ++i;
            }
            return dt;
        }

        public bool SubmitReturnRequisition(ArrayList arrData, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; ++i)
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = DALObj.CutStockRequisition(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
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

        private DataTable GetRequisitionItem(double requisition)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return itemDAL.GetDataByRequisition(requisition, null);
        }

        public bool CopyRequisition(string userID, double loidSource)
        {
            ProductReserveData data = GetOldData(loidSource);
            DataTable itemList = GetRequisitionItem(data.LOID);
            ArrayList arr = new ArrayList();
            foreach (DataRow dRow in itemList.Rows)
            {
                RequisitionItemData idata = new RequisitionItemData();
                idata.ACTIVE = dRow["ACTIVE"].ToString();
                idata.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                idata.DUEDATE = data.DUEDATE;
                idata.NETPRICE = Convert.ToDouble(dRow["NETPRICE"]);
                idata.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                idata.QTY = Convert.ToDouble(dRow["QTY"]);
                idata.PRICE = Convert.ToDouble(dRow["PRICE"]);
                idata.REQUISITION = Convert.ToDouble(dRow["REQUISITION"]);
                idata.UNIT = Convert.ToDouble(dRow["UNIT"]);
                idata.ISVAT = dRow["ISVAT"].ToString();
                idata.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                idata.REFTABLE = dRow["REFTABLE"].ToString();
                arr.Add(idata);
            }
            data.ITEM = arr;
            DALObj.OnDB = false;
            data.LOID = 0;
            data.CODE = "";
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.ACTIVE = Constz.ActiveStatus.Active;
            return UpdateData(userID, data);
        }

        private ProductReserveData GetOldData(double loid)
        {
            ProductReserveData data = new ProductReserveData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.ACTIVE = DALObj.ACTIVE;
                data.CADDRESS = DALObj.CADDRESS;
                data.CEMAIL = DALObj.CEMAIL;
                data.CFAX = DALObj.CFAX;
                data.CLASTNAME = DALObj.CLASTNAME;
                data.CNAME = DALObj.CNAME;
                data.CODE = "";
                data.CREATEBY = DALObj.CREATEBY;
                data.CTEL = DALObj.CTEL;
                data.CTITLE = DALObj.CTITLE;
                data.CUSTOMER = DALObj.CUSTOMER;
                data.DUEDATE = DALObj.DUEDATE;
                data.GRANDTOT = DALObj.GRANDTOT;
                data.REASON = DALObj.REASON;
                data.REFLOID = DALObj.REFLOID;
                data.REFTABLE = DALObj.REFTABLE;
                data.REMARK = DALObj.REMARK;
                data.REQDATE = DALObj.REQDATE;
                data.RESERVEDATE = DALObj.RESERVEDATE;
                data.REQUISITIONTYPE = DALObj.REQUISITIONTYPE;
                data.STATUS = Constz.Requisition.Status.Waiting.Code;
                data.TOTAL = DALObj.TOTAL;
                data.TOTDIS = DALObj.TOTDIS;
                data.TOTVAT = DALObj.TOTVAT;
                data.VAT = DALObj.VAT;
                data.WAREHOUSE = DALObj.WAREHOUSE;
            }
            return data;
        }

        private bool ValidateData(ProductReserveData data)
        {
            bool ret = true;
            if (data.REFLOID == 0)
            {
                ret = false;
                _error = "กรุณาเลือกใบเสร็จรับเงิน";
            }
            else if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            return ret;
        }

        private void UpdateData(string userID, ProductReserveData data, System.Data.OracleClient.OracleTransaction trans)
        {
            bool ret = true;

            if (ValidateData(data))
            {
                if (SearchObj.GetDataByRefLOID(data.LOID, data.REFLOID, trans).Rows.Count > 0)
                    throw new ApplicationException("ไม่สามารถทำรายการได้ เนื่องจากใบเสร็จที่เลือก อยู่ระหว่างดำเนินการลดหนี้หรือลดหนี้เสร็จสิ้นแล้ว");
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(data.LOID, trans);

                DALObj.CODE = data.CODE;
                DALObj.ACTIVE = data.ACTIVE;
                DALObj.CADDRESS = data.CADDRESS;
                DALObj.CEMAIL = data.CEMAIL;
                DALObj.CFAX = data.CFAX;
                DALObj.CLASTNAME = data.CLASTNAME;
                DALObj.CNAME = data.CNAME;
                DALObj.CTEL = data.CTEL;
                DALObj.CTITLE = data.CTITLE;
                DALObj.CUSTOMER = data.CUSTOMER;
                DALObj.DUEDATE = data.DUEDATE;
                DALObj.GRANDTOT = data.GRANDTOT;
                DALObj.REASON = data.REASON;
                DALObj.REFLOID = data.REFLOID;
                DALObj.REFTABLE = data.REFTABLE;
                DALObj.REMARK = data.REMARK;
                DALObj.RESERVEDATE = data.RESERVEDATE;
                DALObj.REQUISITIONTYPE = data.REQUISITIONTYPE;
                DALObj.STATUS = data.STATUS;
                DALObj.TOTAL = data.TOTAL;
                DALObj.TOTDIS = data.TOTDIS;
                DALObj.TOTVAT = data.TOTVAT;
                DALObj.VAT = data.VAT;
                DALObj.WAREHOUSE = data.WAREHOUSE;
                DALObj.REQDATE = data.REQDATE;
                DALObj.INVCODE = data.INVCODE;

                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, trans);
                else
                    ret = DALObj.InsertCurrentData(userID, trans);

                _LOID = DALObj.LOID;
                if (!ret)
                {
                    throw new ApplicationException(DALObj.ErrorMessage);
                }

                RequisitionItemDAL itemDAL = new RequisitionItemDAL();
                itemDAL.DeleteDataByRequisition(data.LOID, trans);
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
                    itemDAL.REFTABLE = item.REFTABLE;
                    itemDAL.REFLOID = item.REFLOID;

                    itemDAL.OnDB = false;
                    ret = itemDAL.InsertCurrentData(userID, trans);
                    if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
                }
            }
            else
                throw new ApplicationException(_error);
        }

        public bool UpdateData(string userID, ProductReserveData data)
        {
            bool ret = true;
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
            return ret;
        }

        public bool SubmitReturnRequisition(string userID, ProductReserveData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();

            try
            {
                data.STATUS = Constz.Requisition.Status.Approved.Code;
                UpdateData(userID, data, obj.zTrans);
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
            return ret;
        }

        public bool CancelReturnRequisition(string userID, double requisition)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();

            try
            {
                DALObj.GetDataByLOID(requisition, obj.zTrans);
                if (DALObj.STATUS == Constz.Requisition.Status.Approved.Code)
                {
                    DALObj.STATUS = Constz.Requisition.Status.Void.Code;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    ret = DALObj.CutStockRequisition(DALObj.LOID, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                }
                else if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                {
                    DALObj.STATUS = Constz.Requisition.Status.Void.Code;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
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

        public InvoiceRequestData GetData(double requisition)
        {
            InvoiceRequestData data = new InvoiceRequestData();
            DataTable dt = SearchObj.GetInvoiceRequest(requisition);
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                if (!Convert.IsDBNull(dRow["CADDRESS"])) data.CADDRESS = dRow["CADDRESS"].ToString();
                if (!Convert.IsDBNull(dRow["CFAX"])) data.CFAX = dRow["CFAX"].ToString();
                if (!Convert.IsDBNull(dRow["CLASTNAME"])) data.CLASTNAME = dRow["CLASTNAME"].ToString();
                if (!Convert.IsDBNull(dRow["CNAME"])) data.CNAME = dRow["CNAME"].ToString();
                if (!Convert.IsDBNull(dRow["CODE"])) data.CODE = dRow["CODE"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CREATEBY"])) data.CREATEBY = dRow["CREATEBY"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CTEL"])) data.CTEL = dRow["CTEL"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CTITLE"])) data.CTITLE = Convert.ToDouble(dRow["CTITLE"]);
                if (!Convert.IsDBNull(dRow["CUSTOMER"])) data.CUSTOMER = Convert.ToDouble(dRow["CUSTOMER"]);
                if (!Convert.IsDBNull(dRow["GRANDTOT"])) data.GRANDTOT = Convert.ToDouble(dRow["GRANDTOT"]);
                if (!Convert.IsDBNull(dRow["LOID"])) data.LOID = Convert.ToDouble(dRow["LOID"]);
                if (!Convert.IsDBNull(dRow["OLDTOTAL"])) data.OLDTOTAL = Convert.ToDouble(dRow["OLDTOTAL"]);
                if (!Convert.IsDBNull(dRow["REASON"])) data.REASON = dRow["REASON"].ToString();
                if (!Convert.IsDBNull(dRow["REFLOID"])) data.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                if (!Convert.IsDBNull(dRow["REMARK"])) data.REMARK = dRow["REMARK"].ToString();;
                if (!Convert.IsDBNull(dRow["REQDATE"])) data.REQDATE = Convert.ToDateTime(dRow["REQDATE"]);
                if (!Convert.IsDBNull(dRow["STATUS"])) data.STATUS = dRow["STATUS"].ToString();;
                if (!Convert.IsDBNull(dRow["TOTAL"])) data.TOTAL = Convert.ToDouble(dRow["TOTAL"]);
                if (!Convert.IsDBNull(dRow["TOTDIS"])) data.TOTDIS = Convert.ToDouble(dRow["TOTDIS"]);
                if (!Convert.IsDBNull(dRow["TOTVAT"])) data.TOTVAT = Convert.ToDouble(dRow["TOTVAT"]);
                if (!Convert.IsDBNull(dRow["VAT"])) data.VAT = Convert.ToDouble(dRow["VAT"]);
                if (!Convert.IsDBNull(dRow["WAREHOUSE"])) data.WAREHOUSE = Convert.ToDouble(dRow["WAREHOUSE"]);
                if (!Convert.IsDBNull(dRow["INVCODE"])) data.INVCODE = dRow["INVCODE"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CUSTOMERCODE"])) data.CUSTOMERCODE = dRow["CUSTOMERCODE"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CUSTOMERNAME"])) data.CUSTOMERNAME = dRow["CUSTOMERNAME"].ToString(); ;
            }
            return data;
        }

        public InvoiceRequestData GetInvoiceData(double requisition)
        {
            InvoiceRequestData data = new InvoiceRequestData();
            DataTable dt = SearchObj.GetOldInvoiceRequest(requisition);
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                if (!Convert.IsDBNull(dRow["CADDRESS"])) data.CADDRESS = dRow["CADDRESS"].ToString();
                if (!Convert.IsDBNull(dRow["CFAX"])) data.CFAX = dRow["CFAX"].ToString();
                if (!Convert.IsDBNull(dRow["CLASTNAME"])) data.CLASTNAME = dRow["CLASTNAME"].ToString();
                if (!Convert.IsDBNull(dRow["CNAME"])) data.CNAME = dRow["CNAME"].ToString();
                if (!Convert.IsDBNull(dRow["CODE"])) data.CODE = dRow["CODE"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CREATEBY"])) data.CREATEBY = dRow["CREATEBY"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CTEL"])) data.CTEL = dRow["CTEL"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CTITLE"])) data.CTITLE = Convert.ToDouble(dRow["CTITLE"]);
                if (!Convert.IsDBNull(dRow["CUSTOMER"])) data.CUSTOMER = Convert.ToDouble(dRow["CUSTOMER"]);
                if (!Convert.IsDBNull(dRow["GRANDTOT"])) data.GRANDTOT = Convert.ToDouble(dRow["GRANDTOT"]);
                if (!Convert.IsDBNull(dRow["LOID"])) data.LOID = Convert.ToDouble(dRow["LOID"]);
                if (!Convert.IsDBNull(dRow["OLDTOTAL"])) data.OLDTOTAL = Convert.ToDouble(dRow["OLDTOTAL"]);
                if (!Convert.IsDBNull(dRow["REASON"])) data.REASON = dRow["REASON"].ToString();
                if (!Convert.IsDBNull(dRow["REFLOID"])) data.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                if (!Convert.IsDBNull(dRow["REMARK"])) data.REMARK = dRow["REMARK"].ToString(); ;
                if (!Convert.IsDBNull(dRow["REQDATE"])) data.REQDATE = Convert.ToDateTime(dRow["REQDATE"]);
                if (!Convert.IsDBNull(dRow["STATUS"])) data.STATUS = dRow["STATUS"].ToString(); ;
                if (!Convert.IsDBNull(dRow["TOTAL"])) data.TOTAL = Convert.ToDouble(dRow["TOTAL"]);
                if (!Convert.IsDBNull(dRow["TOTDIS"])) data.TOTDIS = Convert.ToDouble(dRow["TOTDIS"]);
                if (!Convert.IsDBNull(dRow["TOTVAT"])) data.TOTVAT = Convert.ToDouble(dRow["TOTVAT"]);
                if (!Convert.IsDBNull(dRow["VAT"])) data.VAT = Convert.ToDouble(dRow["VAT"]);
                if (!Convert.IsDBNull(dRow["WAREHOUSE"])) data.WAREHOUSE = Convert.ToDouble(dRow["WAREHOUSE"]);
                if (!Convert.IsDBNull(dRow["INVCODE"])) data.INVCODE = dRow["INVCODE"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CUSTOMERCODE"])) data.CUSTOMERCODE = dRow["CUSTOMERCODE"].ToString(); ;
                if (!Convert.IsDBNull(dRow["CUSTOMERNAME"])) data.CUSTOMERNAME = dRow["CUSTOMERNAME"].ToString(); ;
            }
            data.OLDITEMS = SearchObj.GetInvoiceItemList(requisition);
            int i = 1;
            foreach (DataRow dRow in data.OLDITEMS.Rows)
            {
                dRow["RANK"] = i;
                ++i;
            }
            return data;
        }

    }
}
