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
    public class EditPOFlow
    {
        string _error = "";
        double _LOID = 0;
        double _LOIDEDIT = 0;
        PDOrderDAL _dal;
        POEditDAL _dal2;

        public double LOID
        {
            get { return _LOID; }
        }

        public double LOIDEDIT
        {
            get { return _LOIDEDIT; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public PDOrderDAL DALObj
        {
            get { if (_dal == null) { _dal = new PDOrderDAL(); } return _dal; }
        }

        public POEditDAL DALObj2
        {
            get { if (_dal2 == null) { _dal2 = new POEditDAL(); } return _dal2; }
        }

        //public DataTable GetPOEditList(POEditData data)
        //{
        //    string whereString = "";

        //    if (data.PECODE.Trim() != "")
        //        whereString += (whereString == "" ? "" : "AND ") + "UPPER(PECODE) = '" + OracleDB.QRText(data.PECODE.Trim()).ToUpper() + "' ";
        //    if (data.POCODE.Trim() != "")
        //        whereString += (whereString == "" ? "" : "AND ") + "UPPER(POCODE) = '" + OracleDB.QRText(data.POCODE.Trim()).ToUpper() + "' ";
        //    if (data.DATEFROM.Year != 1)
        //        whereString += (whereString == "" ? "" : "AND ") + "POEDITDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
        //    if (data.DATETO.Year != 1)
        //        whereString += (whereString == "" ? "" : "AND ") + "POEDITDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
        //    if (data.PODATEFROM.Year != 1)
        //        whereString += (whereString == "" ? "" : "AND ") + "ORDERDATE >= " + OracleDB.QRDate(data.PODATEFROM) + " ";
        //    if (data.PODATETO.Year != 1)
        //        whereString += (whereString == "" ? "" : "AND ") + "ORDERDATE <= " + OracleDB.QRDate(data.PODATETO) + " ";
        //    if (data.SUPPLIER != "0")
        //        whereString += (whereString == "" ? "" : "AND ") + "SUPPLIER = " + data.SUPPLIER.ToString() + " ";
        //    if (data.STATUSFROM.Trim() != "")
        //        whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(data.STATUSFROM.Trim()) + "' ";
        //    if (data.STATUSTO.Trim() != "")
        //        whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(data.STATUSTO.Trim()) + "' ";

        //    string sql = "SELECT * FROM (SELECT  ROWNUM NO, PE.LOID PELOID, PE.CODE PECODE, PE.POEDITDATE, PE.REASON, PO.LOID POLOID, PO.CODE POCODE, PO.ORDERDATE, ";
        //    sql += "CASE PE.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
        //    sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
        //    sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
        //    sql += "ELSE '' END AS STATUSNAME, ";
        //    sql += "CASE PE.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
        //    sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
        //    sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
        //    sql += "ELSE '' END AS RANK, PO.SUPPLIER,S.SUPPLIERNAME, PE.PONEW, PE.POOLD ";
        //    sql += "FROM POEDIT PE INNER JOIN PDORDER PO ON PE.POOLD = PO.LOID ";
        //    sql += "INNER JOIN SUPPLIER S ON PO.SUPPLIER = S.LOID) ";
        //    sql += (whereString == "" ? "" : "WHERE " + whereString);
        //    sql += "ORDER BY PECODE ";

        //    DataTable dt = OracleDB.ExecListCmd(sql);
        //    return dt;
        //}

        public DataTable GetPOEditList(POEditData data)
        {
            DataTable dt = DALObj2.GetPOEditList(data);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["NO"] = i;
                i += 1;
            }
            return dt;
        }

        public DataTable GetPOItem(double PDOrder)
        {
            string sql = "SELECT POI.LOID, ROWNUM RANK, POI.PRODUCT, POI.QTY, POI.UNIT, POI.PRICE, POI.DISCOUNT, POI.DUEDATE, POI.PRITEM, PD.NAME PRODUCTNAME, PD.BARCODE, UNIT.NAME UNITNAME, PR.CODE, ((NVL(POI.QTY,0)*NVL(POI.PRICE,0))-NVL(POI.DISCOUNT,0)) NETPRICE, PD.ISVAT ";
            sql += "FROM POITEM POI INNER JOIN PRODUCT PD ON PD.LOID = POI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = POI.UNIT ";
            sql += "INNER JOIN PRITEM PRI ON PRI.LOID = POI.PRITEM ";
            sql += "INNER JOIN PDREQUEST PR ON PRI.PDREQUEST = PR.LOID ";
            sql += "WHERE POI.PDORDER = " + PDOrder;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetViewProductPOPopupList(double loid)
        {
            string sql = "SELECT * FROM V_PRODUCT_PO_POPUP_LIST ";
            sql += "WHERE LOID = " + loid;
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
            //string sql = "SELECT TT.NAME TITLENAME, SP.CNAME, SP.CLASTNAME, SP.CADDRESS, SP.CROAD, TB.NAME TAMBOLNAME, AM.NAME AMPHURNAME, PV.NAME PROVINCENAME, SP.ZIPCODE, SP.CTEL, SP.FAX, SP.PAYMENTYPE ";
            //sql += "FROM SUPPLIER SP INNER JOIN TITLE TT ON TT.LOID = SP.CTITLE ";
            //sql += "INNER JOIN TAMBOL TB ON TB.LOID = SP.CTAMBOL ";
            //sql += "INNER JOIN AMPHUR AM ON AM.LOID = SP.CAMPHUR ";
            //sql += "INNER JOIN PROVINCE PV ON PV.LOID = SP.CPROVINCE ";
            //sql += "WHERE SP.LOID = " + loid;
            //return OracleDB.ExecListCmd(sql);
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

        public DataTable GetREFPOfromTable(string loid, string table)
        {
            string sql = "SELECT * FROM '" + table + "' WHERE LOID = '" + loid + "'";
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
               // data.ORDERDATE = DALObj.ORDERDATE;
                data.SUPPLIER = DALObj.SUPPLIER;
                data.CNAME = DALObj.CNAME;
                data.CADDRESS = DALObj.CADDRESS;
                data.CTEL = DALObj.CTEL;
                data.CFAX = DALObj.CFAX;
               // data.REMARK = DALObj.REMARK;
                data.PAYMENTTYPE = DALObj.PAYMENTTYPE;
                data.PAYMENTDESC = DALObj.PAYMENTDESC;
                data.TOTAL = DALObj.TOTAL;
                data.TOTVAT = DALObj.TOTVAT;
                data.TOTDIS = DALObj.TOTDIS;
                data.GRANDTOT = DALObj.GRANDTOT;
                data.REFLOID = DALObj.REFLOID;
                data.REFTABLE = DALObj.REFTABLE;
               // data.STATUS = DALObj.STATUS;
                data.DELIVERY = DALObj.DELIVERY;
                data.OTHER = DALObj.OTHER;
                data.VAT = DALObj.VAT;
                data.TYPE = DALObj.POTYPE;
            }
            return data;
        }

        public POEditData GetDataEdit(double loid)
        {
            POEditData data = new POEditData();
            if (DALObj2.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj2.LOID;
                data.CODE = DALObj2.CODE;
                data.ACTIVE = DALObj2.ACTIVE;
                data.APPROVEDATE = DALObj2.APPROVEDATE;
                data.APPROVER = DALObj2.APPROVER;
                data.POEDITDATE = DALObj2.POEDITDATE;
                data.PONEW = DALObj2.PONEW;
                data.POOLD = DALObj2.POOLD;
                data.REASON = DALObj2.REASON;
                data.REMARK = DALObj2.REMARK;
                data.STATUS = DALObj2.STATUS;
             
            }
            return data;
        }

        public bool ValidateData(PurchaseOrderData data)
        {
            bool ret = true;
            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
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

                    ret = DALObj2.UpdatePOOldActive(DALObj.REFLOID, Constz.ActiveStatus.InActive, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    POItemDAL itemDAL = new POItemDAL();
                    itemDAL.DeleteDataByPDOrder(data.LOID, obj.zTrans);
                    for (Int16 i = 0; i < data.ITEM.Count; ++i)
                    {
                        POItemData item = (POItemData)data.ITEM[i];
                        itemDAL.PRODUCT = item.PRODUCT;
                        itemDAL.PRITEM = item.PRITEM;
                        itemDAL.PDORDER = DALObj.LOID;
                        itemDAL.QTY = item.QTY;
                        itemDAL.RECEIVEQTY = item.RECEIVEQTY;
                        itemDAL.UNIT = item.UNIT;
                        itemDAL.PRICE = item.PRICE;
                        itemDAL.DISCOUNT = item.DISCOUNT;
                        itemDAL.DUEDATE = item.DUEDATE;
                        itemDAL.ACTIVE = item.ACTIVE;
                        itemDAL.REFPOITEM = item.LOID;
                        itemDAL.STATUS = DALObj.STATUS;
                        itemDAL.ISVAT = item.ISVAT;

                        itemDAL.OnDB = false;
                        ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
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

        public bool UpdateDataPOEdit(string userID, POEditData data)
        {
            bool ret = true;

                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj2.OnDB = false;
                    DALObj2.GetDataByLOID(data.LOID, obj.zTrans);

                    DALObj2.ACTIVE = data.ACTIVE;
                    DALObj2.APPROVEDATE = data.APPROVEDATE;
                    DALObj2.APPROVER = data.APPROVER;
                    DALObj2.POEDITDATE = data.POEDITDATE;
                    DALObj2.CODE = data.CODE;
                    DALObj2.PONEW = data.PONEW;
                    DALObj2.POOLD = data.POOLD;
                    DALObj2.REASON = data.REASON;
                    DALObj2.REMARK = data.REMARK;
                    DALObj2.STATUS = data.STATUS;
                    DALObj2.TYPE = data.TYPE;


                    if (DALObj2.OnDB)
                        ret = DALObj2.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj2.InsertCurrentData(userID, obj.zTrans);

                    _LOIDEDIT = DALObj2.LOID;
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

        public bool DeleteDataPOEdit(ArrayList arrData)
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
                    DALObj2.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    //itemDAL.DeleteDataByPDOrder(Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = DALObj2.DeleteCurrentData(obj.zTrans);
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
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
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

        public bool UpdatePOEditStatus(ArrayList arrData, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj2.OnDB = false;
                    DALObj2.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = DALObj2.UpdateCurrentData(userID, obj.zTrans);
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

        public bool UpdateStatusPOOld(double loid, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                ret = DALObj2.UpdateStatusPOOld(loid, status, userID, obj.zTrans);
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

        public bool UpdateStockIn(double oldloid, double newloid, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                //ret = DALObj2.UpdateStockIn(oldloid, newloid, userID, obj.zTrans);
                   
                //if (ret)
                //{
                    ret = DALObj2.UpdateStockInItem(newloid, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                //}
                //else
                //    throw new ApplicationException(DALObj.ErrorMessage);

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


        public bool UpdateActiveStatus(ArrayList arrData, string active, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    //DALObj.OnDB = false;
                    //DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    //ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    ret = DALObj2.UpdatePOOldActive(Convert.ToDouble(arrData[i]), active, userID, obj.zTrans);
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

        public string GetPOOldCode(double loid)
        {
            string sql = "SELECT CODE FROM PDORDER WHERE LOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            string CODE = "";
            if (dt.Rows.Count > 0)
            {
                CODE = dt.Rows[0]["CODE"].ToString();
            }

            return CODE;
        }
    }
}

