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
    public class BillFlow
    {
        private BillDAL _search;
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public BillDAL SearchObj
        {
            get { if (_search == null) { _search = new BillDAL(); } return _search; }
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
                if (!Convert.IsDBNull(dRow["CUSTOMERNAME"])) data.CUSTOMERNAME = dRow["CUSTOMERNAME"].ToString();
                data.TOTAL = Convert.ToDouble(dRow["TOTAL"]);
                data.TOTDIS = Convert.ToDouble(dRow["TOTDIS"]);
                data.VAT = Convert.ToDouble(dRow["VAT"]);
                data.TOTVAT = Convert.ToDouble(dRow["TOTVAT"]);
                data.GRANDTOT = Convert.ToDouble(dRow["GRANDTOT"]);
                data.CREATEBY = dRow["CREATEBY"].ToString();
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

        public bool SetInvoiceCode(string userID, double requisition)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                RequisitionDAL reqDAL = new RequisitionDAL();
                reqDAL.GetDataByLOID(requisition, obj.zTrans);
                if (reqDAL.INVCODE == "")
                {
                    reqDAL.INVCODE = OracleDB.GetRunningCode("REQUISITION_INVCODE", reqDAL.REQUISITIONTYPE.ToString(), obj.zTrans);
                    ret = reqDAL.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(reqDAL.ErrorMessage);
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
