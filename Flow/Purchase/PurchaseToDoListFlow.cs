using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.DAL;
using ABB.Data.Purchase;

namespace ABB.Flow.Purchase
{
    public class PurchaseToDoListFlow
    {
        double _LOID = 0;
        public double LOID
        {
            get { return _LOID; }
        }
        string _error = "";
        public string ErrorMessage
        {
            get { return _error; }
        }

        public DataTable GetProductPurchaseList(ProductPurchaseListSearchData data)
        {
            //string where = "WHERE PR.STATUS = 'AP' AND PRI.ACTIVE = '1' AND PR.ORDERTYPE = 'PO' ";
            //if (data.CODE.Trim() != "")
            //    where += (where == "" ? "" : "AND ") + "UPPER(PR.CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            //if (data.DATEFROM.Year != 1)
            //    where += (where == "" ? "" : "AND ") + "TO_DATE(REQUESTDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            //if (data.DATETO.Year != 1)
            //    where += (where == "" ? "" : "AND ") + "TO_DATE(REQUESTDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            //if (data.PURCHASETYPE != 0)
            //    where += (where == "" ? "" : "AND ") + "PURCHASETYPE = " + data.PURCHASETYPE.ToString() + " ";

            //if (data.PRODUCT != 0)
            //    where += (where == "" ? "" : "AND ") + "PRODUCT = " + data.PRODUCT.ToString() + " ";

            //string sql = "SELECT PRI.LOID, PR.LOID PRLOID, PR.CODE PRCODE, PR.REQUESTDATE, PD.NAME PRODUCTNAME, PRI.PRODUCT, PRI.QTY, UN.NAME UNITNAME, PRI.UNIT, PRI.OLDPRICE, PRI.URGENT, PRI.CURPRICE, PRI.MINPRICE, PT.NAME PURCHASETYPENAME, PRI.DUEDATE, PR.STATUS, ";
            //sql += "CASE PR.STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            //sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            //sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            //sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            //sql += "ELSE '' END STATUSNAME ";
            //sql += "FROM PRITEM PRI INNER JOIN PDREQUEST PR ON PRI.PDREQUEST = PR.LOID ";
            //sql += "INNER JOIN PRODUCT PD ON PRI.PRODUCT = PD.LOID ";
            //sql += "INNER JOIN UNIT UN ON PRI.UNIT = UN.LOID ";
            //sql += "INNER JOIN PURCHASETYPE PT ON PR.PURCHASETYPE = PT.LOID ";
            //sql += where;
            //sql += "ORDER BY PR.REQUESTDATE, PR.CODE ";
            string where = "WHERE VP.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' AND VP.ACTIVE = '" + Constz.ActiveStatus.Active + "' AND VP.ORDERTYPE = '" + Constz.OrderType.PO.Code + "' AND VP.QTY>0 ";
            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(VP.PRCODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQUESTDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQUESTDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.PURCHASETYPE != 0)
                where += (where == "" ? "" : "AND ") + "VP.PTLOID = '" + data.PURCHASETYPE.ToString() + "' ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCT = " + data.PRODUCT.ToString() + " ";

            string sql = "SELECT VP.PRITEM LOID,VP.PDREQUEST PRLOID,VP.PRCODE PRCODE,VP.REQUEStDate,VP.PDNAME PRODUCTNAME,VP.PRODUCT,VP.PRQTY QTY,VP.UNAME UNITNAME,VP.UNIT,VP.OLDPRICE,VP.URGENT, ";
            sql += "VP.CURPRICE,VP.MINPRICE,VP.PURCHASETYPE PURCHASETYPENAME,VP.DUEDATE,VP.STATUS, ";
            sql += "CASE VP.STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "ELSE '' END STATUSNAME ";
            sql += "FROM V_PRODUCT_PO_POPUP_LIST VP ";
            sql += where;
            sql += "ORDER BY VP.REQUESTDATE, VP.PRCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductReceiveList(ProductReceiveListSearchData data)
        {
            string where = "WHERE PO.STATUS = 'AP' AND PO.ACTIVE = '1' AND PO.ORDERTYPE = 'PO' ";
            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PO.CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(ORDERDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(ORDERDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCT = " + data.PRODUCT.ToString() + " ";

            if (data.SUPPLIER != 0)
                where += (where == "" ? "" : "AND ") + "SUPPLIER = " + data.SUPPLIER.ToString() + " ";


            string sql = "SELECT PO.LOID POLOID, POI.DUEDATE, PO.CODE POCODE, PO.ORDERDATE, ";
            sql += "SP.SUPPLIERNAME, PD.NAME PRODUCTNAME, POI.QTY, POI.RECEIVEQTY, UN.NAME UNITNAME, ";
            sql += "CASE WHEN POI.LOID IS NULL AND SII.LOID IS NULL THEN 'รอเปิด PO' ";
            sql += "WHEN POI.LOID IS NOT NULL AND SII.LOID IS NULL THEN 'รอรับสินค้า' ";
            sql += "WHEN POI.LOID IS NOT NULL AND SII.STATUS = 'QC' THEN 'รอตรวจQC' ";
            sql += "WHEN POI.LOID IS NOT NULL AND SII.STATUS = 'AP' THEN 'รอรับเข้าคลัง' ";
            sql += "WHEN POI.LOID IS NOT NULL AND SII.STATUS = 'FN' THEN 'รับเข้าคลังแล้ว' END STATUSPRODUCT ";
            sql += "FROM POITEM POI INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID ";
            sql += "INNER JOIN PRODUCT PD ON POI.PRODUCT = PD.LOID ";
            sql += "INNER JOIN UNIT UN ON POI.UNIT = UN.LOID ";
            sql += "INNER JOIN SUPPLIER SP ON PO.SUPPLIER = SP.LOID ";
            sql += "LEFT JOIN STOCKINITEM SII ON SII.REFTABLE = 'POITEM' AND SII.REFLOID = POI.LOID ";
            sql += where;
            sql += "ORDER BY POI.DUEDATE, PO.CODE, PD.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public bool NewPDOrder(string UserID, PurchaseOrderData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PDOrderDAL _DAL = new PDOrderDAL();
                _DAL.OnDB = false;
                _DAL.ACTIVE = data.ACTIVE;
                _DAL.CODE = data.CODE;
                _DAL.ORDERDATE = data.ORDERDATE;
                _DAL.STATUS = data.STATUS;
                _DAL.VAT = data.VAT;
                _DAL.ORDERTYPE = Constz.OrderType.PO.Code;
                _DAL.POTYPE = "N";

                ret = _DAL.InsertCurrentData(UserID, obj.zTrans);
                _LOID = _DAL.LOID;
                if (!ret)
                {
                    throw new ApplicationException(_DAL.ErrorMessage);
                }
                
                POItemDAL _DALItem = new POItemDAL();
                for (int i = 0; i < data.ITEM.Count; ++i)
                {
                    _DALItem.OnDB = false;
                    POItemData itemData = (POItemData)data.ITEM[i];
                    
                    _DALItem.PRODUCT = itemData.PRODUCT;
                    _DALItem.PRITEM = itemData.PRITEM;
                    _DALItem.PDORDER = _DAL.LOID;
                    _DALItem.QTY = itemData.QTY;
                    _DALItem.UNIT = itemData.UNIT;
                    _DALItem.PRICE = itemData.PRICE;
                    _DALItem.DUEDATE = DateTime.Now.Date;
                    _DALItem.ACTIVE = itemData.ACTIVE;

                    ret = _DALItem.InsertCurrentData(UserID, obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(_DALItem.ErrorMessage);
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
