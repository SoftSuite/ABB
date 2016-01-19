using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Sales;

namespace ABB.Flow.Sales
{
    public class SaleInvoiceFlow
    {
        private SaleInvoiceDAL _search;
        RequisitionDAL _dal;
        double _LOID = 0;
        private string _error = "";

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public SaleInvoiceDAL SearchObj
        {
            get { if (_search == null) { _search = new SaleInvoiceDAL(); } return _search; }
        }

        public RequisitionDAL DALObj
        {
            get { if (_dal == null) { _dal = new RequisitionDAL(); } return _dal; }
        }

        public BillData GetData(double requisition)
        {
            BillData data = new BillData();
            DataTable dt = SearchObj.GetRequisitionData(requisition);
            foreach (DataRow dRow in dt.Rows)
            {
                data.REQUISITION = Convert.ToDouble(dRow["LOID"]);
                data.CODE = dRow["CODE"].ToString();
                data.REQDATE = Convert.ToDateTime(dRow["REQDATE"]);
                if (!Convert.IsDBNull(dRow["REFCODE"])) data.REFCODE = dRow["REFCODE"].ToString();
                if (!Convert.IsDBNull(dRow["CNAME"])) data.CUSTOMERNAME = dRow["CNAME"].ToString();
                data.TOTAL = Convert.ToDouble(dRow["TOTAL"]);
                data.TOTDIS = Convert.ToDouble(dRow["TOTDIS"]);
                data.VAT = Convert.ToDouble(dRow["VAT"]);
                data.TOTVAT = Convert.ToDouble(dRow["TOTVAT"]);
                data.GRANDTOT = Convert.ToDouble(dRow["GRANDTOT"]);
                data.CREATEBY = dRow["CREATEBY"].ToString();
                if (!Convert.IsDBNull(dRow["CCODE"])) data.CCODE = dRow["CCODE"].ToString();
                if (!Convert.IsDBNull(dRow["CNAME"])) data.CNAME = dRow["CNAME"].ToString();
                if (!Convert.IsDBNull(dRow["CADDRESS"])) data.CADDRESS = dRow["CADDRESS"].ToString();
                if (!Convert.IsDBNull(dRow["CTEL"])) data.CTEL = dRow["CTEL"].ToString();
                if (!Convert.IsDBNull(dRow["CFAX"])) data.CFAX = dRow["CFAX"].ToString();
                if (!Convert.IsDBNull(dRow["PAYMENT"])) data.PAYMENT = dRow["PAYMENT"].ToString();
                if (!Convert.IsDBNull(dRow["CHEQUE"])) data.CHEQUE = dRow["CHEQUE"].ToString();
                if (!Convert.IsDBNull(dRow["CREDITCARDID"])) data.CREDITCARDID= dRow["CREDITCARDID"].ToString();
                if (!Convert.IsDBNull(dRow["CHEQUEDATE"])) data.CHEQUEDATE = Convert.ToDateTime(dRow["CHEQUEDATE"]);
                if (!Convert.IsDBNull(dRow["BANKNAME"])) data.BANKNAME = dRow["BANKNAME"].ToString();
                if (!Convert.IsDBNull(dRow["BANKBRANCH"])) data.BANKBRANCH = dRow["BANKBRANCH"].ToString();
                if (!Convert.IsDBNull(dRow["RECEIVEBY"])) data.RECEIVEBY = dRow["RECEIVEBY"].ToString();
                if (!Convert.IsDBNull(dRow["RECEIVEDATE"])) data.RECEIVEDATE = Convert.ToDateTime(dRow["RECEIVEDATE"]);
                if (!Convert.IsDBNull(dRow["INVCODE"])) data.INVCODE = dRow["INVCODE"].ToString();
            }
            DataTable dtItem = SearchObj.GetRequisitionItem(requisition);
            foreach (DataRow dRow in dtItem.Rows)
            {
                RequisitionItemData itemData = new RequisitionItemData();
                itemData.LOID = Convert.ToDouble(dRow["RANK"]);
                itemData.BarCode = dRow["BARCODE"].ToString();
                itemData.ProductName = dRow["PRODUCTNAME"].ToString();
                itemData.QTY = Convert.ToDouble(dRow["QTY"]);
                itemData.UnitName = dRow["UNITNAME"].ToString();
                itemData.PRICE = Convert.ToDouble(dRow["PRICE"]);
                itemData.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                itemData.NETPRICE = Convert.ToDouble(dRow["NETPRICE"]);
                itemData.ISVAT = dRow["ISVAT"].ToString();

                data.ITEM.Add(itemData);
            }
            return data;
        }

        public bool ValidateData(BillData data)
        {
            bool ret = true;
            return ret;
        }

        public bool UpdateData(string userID, BillData data)
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
                    DALObj.GetDataByLOID(data.REQUISITION, obj.zTrans);

                    DALObj.CCODE = data.CCODE;
                    DALObj.CNAME = data.CNAME;
                    DALObj.CADDRESS = data.CADDRESS;
                    DALObj.CTEL = data.CTEL;
                    DALObj.CFAX = data.CFAX;
                    DALObj.PAYMENT = data.PAYMENT;
                    DALObj.CHEQUE = data.CHEQUE;
                    DALObj.CREDITCARDID = data.CREDITCARDID;
                    DALObj.CHEQUEDATE = data.CHEQUEDATE;
                    DALObj.BANKNAME = data.BANKNAME;
                    DALObj.BANKBRANCH = data.BANKBRANCH;
                    DALObj.RECEIVEBY = data.RECEIVEBY;
                    DALObj.RECEIVEDATE = data.RECEIVEDATE;
                    DALObj.REFNO = data.REFCODE;
                    if (DALObj.INVCODE == "") DALObj.INVCODE = OracleDB.GetRunningCode("REQUISITION_INVCODE", DALObj.REQUISITIONTYPE.ToString(), obj.zTrans);

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }

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
