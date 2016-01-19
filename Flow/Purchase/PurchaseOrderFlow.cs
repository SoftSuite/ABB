using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.DAL;
using ABB.Flow.Sales;
using ABB.Data.Sales;
using ABB.Data.Purchase;

namespace ABB.Flow.Purchase
{
    public class PurchaseOrderFlow
    {
        string _error = "";
        double _LOID = 0;
        PDOrderDAL _dal;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public PDOrderDAL DALObj
        {
            get { if (_dal == null) { _dal = new PDOrderDAL(); } return _dal; }
        }

        public DataTable GetPDOrderList(PurchaseOrderSearchData data)
        {
            string whereString = "";

            if (data.POCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(POCODE) = '" + OracleDB.QRText(data.POCODE.Trim()).ToUpper() + "' ";
            if (data.PRCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(PRCODE) = '" + OracleDB.QRText(data.PRCODE.Trim()).ToUpper() + "' ";
            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "DUEDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "DUEDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
            if (data.PURCHASETYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PURCHASETYPE = " + data.PURCHASETYPE.ToString() + " ";
            if (data.DIVISION != 0)
                whereString += (whereString == "" ? "" : "AND ") + "DIVISION = " + data.DIVISION.ToString() + " ";
            if (data.PRODUCT != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCT = " + data.PRODUCT.ToString() + " ";
            if (data.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(data.STATUSFROM.Trim()) + " ";
            if (data.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(data.STATUSTO.Trim()) + " ";

            string sql = "select * from ( SELECT ROWNUM NO, PO.LOID, PO.CODE AS POCODE, POI.DUEDATE, PO.ORDERDATE, PD.NAME AS PRODUCTNAME, POI.QTY AS POIQTY, UN.NAME AS UNITNAME, POI.PRICE, ((NVL(POI.QTY,0)*NVL(POI.PRICE,0))-NVL(POI.DISCOUNT,0)) AS NETPRICE, PR.CODE AS PRCODE, PRI.QTY AS PRIQTY, PR.PURCHASETYPE, PR.DIVISION, POI.PRODUCT, PR.LOID AS PRLOID, ";
            sql += "CASE PO.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE PO.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK ";
            sql += "FROM PDORDER PO INNER JOIN POITEM POI ON PO.LOID = POI.PDORDER ";
            sql += "and poi.loid = (select min(loid) from poitem where pdorder = po.loid) ";
            sql += "INNER JOIN PRITEM PRI ON PRI.LOID = POI.PRITEM ";
            sql += "INNER JOIN PDREQUEST PR ON PR.LOID = PRI.PDREQUEST ";
            sql += "LEFT JOIN PURCHASETYPE PT ON PT.LOID = PR.PURCHASETYPE ";
            sql += "LEFT JOIN PRODUCT PD ON PD.LOID = POI.PRODUCT ";
            sql += "LEFT JOIN UNIT UN ON UN.LOID = POI.UNIT ";
            sql += "LEFT JOIN DIVISION DV ON PR.DIVISION = DV.LOID) ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY POCODE ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            return dt;
        }

        public DataTable GetPRQTY(double PRITEM , double PDORDER)
        {
            if (PDORDER == 0)
            {
                string sql = "SELECT QTY FROM V_PRODUCT_PO_POPUP_LIST ";
                sql += "WHERE PRITEM = " + PRITEM.ToString() + " ";
                return OracleDB.ExecListCmd(sql);
            }
            else
            {
                string sql = "SELECT PRI.QTY- SUM(nvl(POI.QTY,0)) POQTY ";
                sql += "FROM PRITEM PRI ";
                sql += "INNER JOIN PDREQUEST PR ON PR.LOID=PRI.PDREQUEST ";
                sql += "INNER JOIN PRODUCT PD ON PD.LOID=PRI.PRODUCT ";
                sql += "INNER JOIN PURCHASETYPE PT ON PT.LOID=PR.PURCHASETYPE ";
                sql += "INNER JOIN UNIT U ON U.LOID=PD.UNIT ";
                sql += "INNER JOIN OFFICER OC ON OC.LOID=PR.REQUESTBY ";
                sql += "INNER JOIN DIVISION DV ON DV.LOID=PR.DIVISION ";
                sql += "LEFT JOIN POITEM POI ON PRI.LOID=POI.PRITEM ";
                sql += "LEFT JOIN PDORDER PO ON PO.LOID=POI.PDORDER ";
                sql += "WHERE PR.ORDERTYPE ='PO' AND PR.STATUS='AP' AND (PO.STATUS IN ('WA','AP') OR PO.STATUS IS NULL) ";
                sql += "and po.loid <> " + PDORDER.ToString() + " AND PRI.LOID = " + PRITEM.ToString() + " ";
                sql += "GROUP BY PRI.LOID, PRI.QTY ";
                return OracleDB.ExecListCmd(sql);
            }
        }

        public DataTable GetPOQty(double PRITEM)
        {
            string sql="SELECT SUM(POI.QTY) POQTY ";
            sql+=" FROM POITEM POI ";
            sql+=" INNER JOIN PDORDER PO ON PO.LOID=POI.PDORDER ";
            sql+=" INNER JOIN PRITEM PRI ON PRI.LOID=POI.PRITEM ";
            sql +=" INNER JOIN PDREQUEST PR ON PR.LOID=PRI.PDREQUEST ";
            sql +=" WHERE PR.ORDERTYPE ='PO' AND PR.STATUS='AP' AND (PO.STATUS IN ('WA','AP') OR PO.STATUS IS NULL) ";
            sql += " AND PRI.LOID =" + PRITEM.ToString();
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPOItem(double PDOrder)
        {
            string sql = "SELECT POI.LOID LOID, ROWNUM RANK, POI.PRODUCT, POI.QTY, POI.RECEIVEQTY, POI.UNIT, POI.PRICE, POI.DISCOUNT, POI.DUEDATE, POI.PRITEM, PD.NAME PRODUCTNAME, PD.BARCODE, UNIT.NAME UNITNAME, PR.CODE, ((NVL(POI.QTY,0)*NVL(POI.PRICE,0))-NVL(POI.DISCOUNT,0)) NETPRICE, POI.ISVAT ";
            sql += "FROM POITEM POI INNER JOIN PRODUCT PD ON PD.LOID = POI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = POI.UNIT ";
            sql += "INNER JOIN PRITEM PRI ON PRI.LOID = POI.PRITEM ";
            sql += "INNER JOIN PDREQUEST PR ON PRI.PDREQUEST = PR.LOID ";
            sql += "WHERE POI.PDORDER = " + PDOrder;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPOItemPopup(double PDOrder)
        {
            string sql = "SELECT POI.LOID, ROWNUM RANK, POI.PRODUCT, POI.QTY, POI.RECEIVEQTY, POI.UNIT, POI.PRICE, POI.DISCOUNT, POI.DUEDATE, POI.PRITEM, PD.NAME PRODUCTNAME, PD.BARCODE, UNIT.NAME UNITNAME, PR.CODE, ((NVL(POI.QTY,0)*NVL(POI.PRICE,0))-NVL(POI.DISCOUNT,0)) NETPRICE, POI.ISVAT ";
            sql += "FROM POITEM POI INNER JOIN PRODUCT PD ON PD.LOID = POI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = POI.UNIT ";
            sql += "INNER JOIN PRITEM PRI ON PRI.LOID = POI.PRITEM ";
            sql += "INNER JOIN PDREQUEST PR ON PRI.PDREQUEST = PR.LOID ";
            sql += "WHERE POI.PDORDER = " + PDOrder;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetViewProductPOPopupList(double pritem)
        {
            string sql = "SELECT * FROM V_PRODUCT_PO_POPUP_LIST ";
            sql += "WHERE PRITEM = " + pritem;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPOItemBlank()
        {
            string sql = "SELECT 0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, NULL DUEDATE, 0 PRITEM, '' PRODUCTNAME, '' BARCODE, '' UNITNAME , '' CODE, 0 NETPRICE, '' ISVAT ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public ProductSearchData GetProductData(double loid)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(loid);
        }

        public UnitSearchData GetUnitData(double loid)
        {
            UnitFlow uFlow = new UnitFlow();
            return uFlow.GetData(loid);
        }

        public PurchaseRequestData GetPRItemData(double loid)
        {
            PurchaseRequestFlow prFlow = new PurchaseRequestFlow();
            return prFlow.GetData(loid);
        }

        public DataTable GetSupplierData(string loid)
        {
            string sql = "SELECT TT.NAME TITLENAME, SP.CNAME, SP.CLASTNAME, SP.CADDRESS || ' ' || CASE WHEN  NVL(SP.CROAD,'X') = 'X' THEN '' ELSE  'ถ.' || SP.CROAD || ' ' END || ";
            sql += "CASE WHEN SP.CTAMBOL IS NULL THEN '' ELSE 'ต.' || TB.NAME || ' ' END || ";
            sql += "CASE WHEN SP.CAMPHUR IS NULL THEN '' ELSE 'อ.' || AM.NAME || ' ' END || ";
            sql += "CASE WHEN SP.CPROVINCE IS NULL THEN '' ELSE 'จ.' || PV.NAME || ' ' END || SP.CZIPCODE CADDRESS, SP.CROAD, TB.NAME TAMBOLNAME, AM.NAME AMPHURNAME, PV.NAME PROVINCENAME, SP.ZIPCODE, SP.CTEL, SP.FAX, SP.PAYMENTYPE ";
            sql += "FROM SUPPLIER SP LEFT JOIN TITLE TT ON TT.LOID = SP.CTITLE ";
            sql += "LEFT JOIN TAMBOL TB ON TB.LOID = SP.CTAMBOL ";
            sql += "LEFT JOIN AMPHUR AM ON AM.LOID = SP.CAMPHUR ";
            sql += "LEFT JOIN PROVINCE PV ON PV.LOID = SP.CPROVINCE ";
            sql += "WHERE SP.LOID = " + loid;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPOEditFromLOID(string loid)
        {
            string sql = "SELECT CODE, POOLD FROM POEDIT WHERE PONEW='" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }


        public DataTable GetCodeFromPOOLD(string poold)
        {
            string sql = "SELECT CODE FROM PDORDER WHERE LOID='" + poold + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public double GetLOIDFromTable(string table, string where)
        {
            string sql = "SELECT LOID FROM '" + table + "' WHERE '" + where + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            return Convert.ToDouble(dt.Rows[0]["LOID"]);
        }

        public PurchaseOrderData GetData(double loid)
        {
            PurchaseOrderData data = new PurchaseOrderData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                data.ORDERDATE = DALObj.ORDERDATE;
                data.SUPPLIER = DALObj.SUPPLIER;
                data.CNAME = DALObj.CNAME;
                data.CADDRESS = DALObj.CADDRESS;
                data.CTEL = DALObj.CTEL;
                data.CFAX = DALObj.CFAX;
                data.REMARK = DALObj.REMARK;
                data.PAYMENTTYPE = DALObj.PAYMENTTYPE;
                data.PAYMENTDESC = DALObj.PAYMENTDESC;
                data.TOTAL = DALObj.TOTAL;
                data.TOTVAT = DALObj.TOTVAT;
                data.TOTDIS = DALObj.TOTDIS;
                data.GRANDTOT = DALObj.GRANDTOT;
                data.REFLOID = DALObj.REFLOID;
                data.REFTABLE = DALObj.REFTABLE;
                data.STATUS = DALObj.STATUS;
                data.DELIVERY = DALObj.DELIVERY;
                data.OTHER = DALObj.OTHER;
                data.VAT = DALObj.VAT;
                data.TYPE = DALObj.POTYPE;
            }
            return data;
        }

        public bool ValidateData(PurchaseOrderData data)
        {
            bool ret = true;
            if (data.SUPPLIER == 0)
            {
                ret = false;
                _error = "กรุณาระบุผู้จำหน่าย";
            }

            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }

            if (data.PAYMENTTYPE == "CC" && data.PAYMENTDESC == "")
            {
                ret = false;
                _error = "กรุณาระบุหมายเลขบัตรเครดิต";
            }

            return ret;
        }

        public bool UpdateData(string userID, PurchaseOrderData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);

                    DALObj.CODE = data.CODE;
                    DALObj.ORDERDATE = data.ORDERDATE;
                    DALObj.ORDERTYPE = data.ORDERTYPE;
                    DALObj.SUPPLIER = data.SUPPLIER;
                    DALObj.CNAME = data.CNAME;
                    DALObj.CADDRESS = data.CADDRESS;
                    DALObj.CTEL = data.CTEL;
                    DALObj.CFAX = data.CFAX;
                    DALObj.APPROVER = data.APPROVER;
                    if (data.APPROVEDATE.Year != 1)
                    {
                        DALObj.APPROVEDATE = data.APPROVEDATE;
                    }
                    DALObj.REMARK = data.REMARK;
                    DALObj.PAYMENTTYPE = data.PAYMENTTYPE;
                    DALObj.PAYMENTDESC = data.PAYMENTDESC;
                    DALObj.TOTAL = data.TOTAL;
                    DALObj.TOTVAT = data.TOTVAT;
                    DALObj.TOTDIS = data.TOTDIS;
                    DALObj.GRANDTOT = data.GRANDTOT;
                    DALObj.REFLOID = data.REFLOID;
                    DALObj.REFTABLE = data.REFTABLE;
                    DALObj.STATUS = data.STATUS;
                    DALObj.ACTIVE = data.ACTIVE;
                    DALObj.DELIVERY = data.DELIVERY;
                    DALObj.OTHER = data.OTHER;
                    DALObj.VAT = data.VAT;
                    DALObj.POTYPE = data.TYPE;

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }

                    POItemDAL itemDAL = new POItemDAL();
                    itemDAL.DeleteDataByPDOrder(data.LOID, obj.zTrans);
                    for (Int16 i = 0; i < data.ITEM.Count; ++i)
                    {
                        POItemData item = (POItemData)data.ITEM[i];
                        itemDAL.PRODUCT = item.PRODUCT;
                        itemDAL.PRITEM = item.PRITEM;
                        itemDAL.PDORDER = DALObj.LOID;
                        itemDAL.QTY = item.QTY;
                        itemDAL.UNIT = item.UNIT;
                        itemDAL.PRICE = item.PRICE;
                        itemDAL.DISCOUNT = item.DISCOUNT;
                        itemDAL.DUEDATE = item.DUEDATE;
                        itemDAL.ACTIVE = item.ACTIVE;
                        itemDAL.ISVAT = item.ISVAT;

                        itemDAL.OnDB = false;
                        ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);

                        if (DALObj.STATUS == Constz.Requisition.Status.Approved.Code)
                        {
                            PRItemDAL pritemDAL = new PRItemDAL();
                            pritemDAL.GetDataByLOID(itemDAL.PRITEM, obj.zTrans);
                            pritemDAL.CURPRICE = itemDAL.PRICE;
                            pritemDAL.UpdateCurrentData(userID, obj.zTrans);
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
                POItemDAL itemDAL = new POItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByPDOrder(Convert.ToDouble(arrData[i]), obj.zTrans);
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

        public bool UpdatePDOrderStatus(ArrayList arrData, string status, string userID)
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
                        if (GetPOItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                        PurchaseOrderData data = GetData(Convert.ToDouble(arrData[i]));
                        //if (data.REASON == "") throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุเหตุผลในการขอซื้อ");
                    }
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                        DALObj.APPROVER = userID;
                        DALObj.APPROVEDATE = DateTime.Now.Date;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
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

        public bool CopyPDOrder(string userID, double loidSource)
        {
            PurchaseOrderData data = GetData(loidSource);
            DataTable itemList = GetPOItem(data.LOID);
            ArrayList arr = new ArrayList();
            foreach (DataRow dRow in itemList.Rows)
            {
                POItemData idata = new POItemData();
                idata.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                idata.QTY = Convert.ToDouble(dRow["QTY"]);
                idata.UNIT = Convert.ToDouble(dRow["UNIT"]);
                idata.PRICE = Convert.ToDouble(dRow["PRICE"]);
                idata.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                idata.PRITEM = Convert.ToDouble(dRow["PRITEM"]);
                idata.ACTIVE = Constz.ActiveStatus.Active;
                idata.DUEDATE = Convert.ToDateTime(dRow["DUEDATE"]);
                arr.Add(idata);
            }
            data.ITEM = arr;
            DALObj.OnDB = false;
            data.LOID = 0;
            data.CODE = "";
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.ORDERTYPE = Constz.OrderType.PO.Code;
            return UpdateData(userID, data);
        }
    }
}
