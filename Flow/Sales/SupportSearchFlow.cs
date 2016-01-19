using System; 
using System.Collections.Generic; 
using System.Text; 
using System.Data; 
using System.Data.OracleClient; 
using ABB.Data;
using System.Collections;
using ABB.DAL.Sales;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Search;

namespace ABB.Flow.Sales
{
    public class SupportSearchFlow  
    {
        private string _error = "";

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
        public static DataTable  GetSupportSearch(string WHLoid, string RqCode, string DateFrom, string DateTo, string Customer, string StatusFrom)
        {

            DataTable dt = new DataTable();
            V_SUPPORT_LIST_DAL oDAL = new V_SUPPORT_LIST_DAL();
            dt = oDAL.GetSupportSearch(WHLoid.ToString(), RqCode.ToString(), DateFrom.ToString(), DateTo.ToString(), Customer.ToString(), StatusFrom.ToString());
            return dt;
        }

        public static DataTable GetSupportEditData(string RQLoid,string WHLoid)
        {

            DataTable dt = new DataTable();
            V_SUPPORT_LIST_DAL oDAL = new V_SUPPORT_LIST_DAL();
            dt = oDAL.GetRequisitionData(RQLoid, WHLoid);
            return dt;
        }


        public static DataTable GetSupportEditItemData(string RQLoid, string WHLoid)
        {
            DataTable dt = new DataTable();
            V_SUPPORT_LIST_DAL oDAL = new V_SUPPORT_LIST_DAL();
            dt = oDAL.GetRequisitionItemData(RQLoid, WHLoid);
            return dt;
        }

        public ProductSaleData GetProductSale(double product, double warehouse)
        {
            DataTable dt = ABB.DAL.Sales.V_SUPPORT_LIST_DAL.GetProductWithPromotion(warehouse, DateTime.Now.Date, product, "");
            ProductSaleData data = new ProductSaleData();
            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["BARCODE"])) data.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["DISCOUNT"])) data.DISCOUNT = Convert.ToDouble(dt.Rows[0]["DISCOUNT"]);
                if (!Convert.IsDBNull(dt.Rows[0]["ISVAT"])) data.ISVAT = dt.Rows[0]["ISVAT"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["NAME"])) data.PRODUCTNAME = dt.Rows[0]["NAME"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["PRICE"])) data.UNITPRICE = Convert.ToDouble(dt.Rows[0]["PRICE"]);
                if (!Convert.IsDBNull(dt.Rows[0]["LOID"])) data.PRODUCT = Convert.ToDouble(dt.Rows[0]["LOID"]);
                if (!Convert.IsDBNull(dt.Rows[0]["UNIT"])) data.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                if (!Convert.IsDBNull(dt.Rows[0]["UNITNAME"])) data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
            }
            return data;
        }

        public string GetConfigValue(string strWh)
        {
            return SysConfigFlow.GetValue(Constz.ConfigName.SUPDISCOUNT);
        }

        //public PointOfSaleCustomerData GetCustomerSale(double customer)
        //{
        //    DataTable dt = ABB.DAL.Sales.V_SUPPORT_LIST_DAL.GetCustomerDetail(customer);
        //    PointOfSaleCustomerData data = new PointOfSaleCustomerData();
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (!Convert.IsDBNull(dt.Rows[0]["CODE"])) data.CODE = dt.Rows[0]["CODE"].ToString();
        //        if (!Convert.IsDBNull(dt.Rows[0]["DISCOUNT"])) data.DISCOUNT = Convert.ToDouble(dt.Rows[0]["DISCOUNT"]);
        //        if (!Convert.IsDBNull(dt.Rows[0]["CUSTOMERNAME"])) data.CUSTOMERNAME = dt.Rows[0]["CUSTOMERNAME"].ToString();
        //        if (!Convert.IsDBNull(dt.Rows[0]["LOID"])) data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
        //    }
        //    return data;
        //}

        public DataTable GetRequisitionItem(double requisition)
        {
            return ABB.DAL.Sales.V_SUPPORT_LIST_DAL.GetItemList(requisition);
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

        public  double UpdateRequisitionItem(string userID, RequisitionItemData data)
        {
            double ret = 0;
            V_SUPPORT_LIST_DAL v_spport_listt_dal = new V_SUPPORT_LIST_DAL();
            ret = v_spport_listt_dal.UpdateRequsitionItemData(userID.ToString(), data);
            if (ret == 0)
            {
                _error = v_spport_listt_dal.ErrorMessage;
                return 0;
            }
            else
                return ret;
        }

        public bool InsertRequisitionItem(string userID, RequisitionItemData data)
        {
            bool ret = true;
            V_SUPPORT_LIST_DAL v_spport_listt_dal = new V_SUPPORT_LIST_DAL();
            ret = v_spport_listt_dal.InsertRequsitionItemData(userID.ToString(), data);
            if (ret != true)
            {
                _error = v_spport_listt_dal.ErrorMessage;
                return ret;
            }
            else
                return ret;
        }

        public double InsertRequisition(string UserId, RequisitionData data)
        {
            Boolean ret = true;
            RequisitionDAL oDAL = new RequisitionDAL();
            oDAL.REQDATE = Convert.ToDateTime(data.REQDATE);
            oDAL.REQUISITIONTYPE = Convert.ToDouble(data.REQUISITIONTYPE);
            oDAL.STATUS = data.STATUS.ToString();
            oDAL.WAREHOUSE = Convert.ToDouble(data.WAREHOUSE);
            oDAL.RESERVEDATE = Convert.ToDateTime(data.RESERVEDATE);
            oDAL.ACTIVE = data.ACTIVE.ToString();

            ret = oDAL.InsertCurrentData(UserId, null);

            if (ret == false)
            {
                _error = oDAL.ErrorMessage;
                return 0;
            }
            else
                return oDAL.LOID;
        }

        public static DataTable GetRequisitionNewData(string RQLoid,string WHLoid)
        {

            DataTable dt = new DataTable();
            V_SUPPORT_LIST_DAL oDAL = new V_SUPPORT_LIST_DAL();
            dt = oDAL.GetRequisitionNewData(RQLoid.ToString(),WHLoid.ToString());
            return dt;
        }

        public bool UpdateRequisition(string userID, RequisitionData data)
        {
            bool ret = true;
            V_SUPPORT_LIST_DAL v_spport_listt_dal = new V_SUPPORT_LIST_DAL();
            ret = v_spport_listt_dal.UpdateRequsitionData(userID.ToString(), data);
            if (ret != true)
            {
                _error = v_spport_listt_dal.ErrorMessage;
                return ret;
            }
            else
            {
                RequisitionDAL rDAL = new RequisitionDAL();
                bool rr = rDAL.CutStockRequisition(data.LOID, userID, null);
                return ret;
            }
        }

        public bool DeleteReq_ReqItemData(ArrayList arrLOID)
        {
            bool ret = true;
            V_SUPPORT_LIST_DAL oDAL = new V_SUPPORT_LIST_DAL();
            ret = oDAL.DeleteReq_ReqItemData(arrLOID);
            return ret;
        }

        public bool RequisitionApprove(string UserID, ArrayList arrLOID)
        {
            bool ret = true;
            V_SUPPORT_LIST_DAL oDAL = new V_SUPPORT_LIST_DAL();
            ret = oDAL.RequisitionApprove(UserID.ToString(), arrLOID);
            return ret;
            
        }

        public static int CheckRequisitionItemData(string RQLoid)
        {
            int ret = -1;
            ret = V_SUPPORT_LIST_DAL.CheckRequisitionItemData(RQLoid.ToString());
            return ret;
        }

        public bool DeleteRequisition(RequisitionData data)
        {
            bool ret = true;
            ret = V_SUPPORT_LIST_DAL.DeleteRequisition(data);
            return ret;
        }

        public ProductSaleData GetProductSaleByBarcode(string  Barcode, double warehouse)
        {
            DataTable dt = ABB.DAL.Sales.V_SUPPORT_LIST_DAL.GetProductByBarcode(warehouse, DateTime.Now.Date, Barcode);
            ProductSaleData data = new ProductSaleData();
            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["BARCODE"])) data.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["DISCOUNT"])) data.DISCOUNT = Convert.ToDouble(dt.Rows[0]["DISCOUNT"]);
                if (!Convert.IsDBNull(dt.Rows[0]["ISVAT"])) data.ISVAT = dt.Rows[0]["ISVAT"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["NAME"])) data.PRODUCTNAME = dt.Rows[0]["NAME"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["PRICE"])) data.UNITPRICE = Convert.ToDouble(dt.Rows[0]["PRICE"]);
                if (!Convert.IsDBNull(dt.Rows[0]["LOID"])) data.PRODUCT = Convert.ToDouble(dt.Rows[0]["LOID"]);
                if (!Convert.IsDBNull(dt.Rows[0]["UNIT"])) data.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                if (!Convert.IsDBNull(dt.Rows[0]["UNITNAME"])) data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
            }
            return data;
        }

    }       
}
