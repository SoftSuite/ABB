using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Sales;
using ABB.Data.Purchase;
using ABB.DAL;
using ABB.DAL.Sales;
using ABB.Flow.Admin;
using ABB.Flow.Sales;


namespace ABB.Flow.Purchase
{
    public class PurchaseRequestFlow
    {
        string _error = "";
        double _LOID = 0;
        PDRequestDAL _dal;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public PDRequestDAL DALObj
        {
            get { if (_dal == null) { _dal = new PDRequestDAL(); } return _dal; }
        }

        public DataTable GetPDRequestList(PurchaseRequestSearchData data)
        {
            string whereString = "";
            string wherePrStatusPc = "";
            string wherePrStatusOt = "";

            if (data.DIVISION != Constz.AdminDepartment.LOID && data.DIVISION != Constz.PurchaseDepartment.LOID)
                whereString += (whereString == "" ? "" : "AND ") + "DIVISION = " + data.DIVISION.ToString() + " ";
            if (data.CODEFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(PRCODE) >= '" + OracleDB.QRText(data.CODEFROM.Trim()).ToUpper() + "' ";
            if (data.CODETO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(PRCODE) <= '" + OracleDB.QRText(data.CODETO.Trim()).ToUpper() + "' ";
            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQUESTDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQUESTDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
            if (data.PURCHASETYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PURCHASETYPE = " + data.PURCHASETYPE.ToString() + " ";
            if (data.PRODUCTNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(PRODUCTNAME) LIKE '%" + data.PRODUCTNAME.Trim().ToUpper() + "%' ";
            if (data.STATUSFROM.Trim() != "0")
                whereString += (whereString == "" ? "" : "AND ") + "STSTATUSRANK >= " + OracleDB.QRText(data.STATUSFROM.Trim()) + " ";
            if (data.STATUSTO.Trim() != "0")
                whereString += (whereString == "" ? "" : "AND ") + "STSTATUSRANK <= " + OracleDB.QRText(data.STATUSTO.Trim()) + " ";
            if (data.STATUSPRFROM.Trim() != "0")
                wherePrStatusOt += (wherePrStatusOt == "" ? "" : "AND ") + "PRSTATUSRANK >= " + OracleDB.QRText(data.STATUSPRFROM.Trim()) + " ";
            if (data.STATUSPRTO.Trim() != "0")
                wherePrStatusOt += (wherePrStatusOt == "" ? "" : "AND ") + "PRSTATUSRANK <= " + OracleDB.QRText(data.STATUSPRTO.Trim()) + " ";

            //กรณีฝ่ายจัดซื้อ สามารถค้นหาใบ PR สถานะส่งให้จัดซื้อ, อนุมัติ, ยกเลิก ได้ทุกแผนก แต่สถานกำลังดำเนินการค้นหาได้เฉพาะของฝ่ายจัดซื้อเท่านั้น
            if (data.DIVISION == Constz.PurchaseDepartment.LOID)
            {
                if (data.STATUSPRFROM.Trim() != "0")
                    wherePrStatusPc += (wherePrStatusPc == "" ? "" : "AND ") + "PRSTATUSRANK >= " + OracleDB.QRText(data.STATUSPRFROM.Trim()) + " ";
                if (data.STATUSPRTO.Trim() != "0")
                    wherePrStatusPc += (wherePrStatusPc == "" ? "" : "AND ") + "PRSTATUSRANK <= " + OracleDB.QRText(data.STATUSPRTO.Trim()) + " ";

                wherePrStatusOt += (wherePrStatusOt == "" ? "" : "AND ") + "PRSTATUSRANK >= " + Constz.Requisition.Status.SP.Rank + " ";
                wherePrStatusOt += (wherePrStatusOt == "" ? "" : "AND ") + "PRSTATUSRANK <= " + Constz.Requisition.Status.Void.Rank + " ";
            }
            
            if (data.STATUSPOFROM.Trim() != "0")
                whereString += (whereString == "" ? "" : "AND ") + "POSTATUSRANK >= " + OracleDB.QRText(data.STATUSPOFROM.Trim()) + " ";
            if (data.STATUSPOTO.Trim() != "0")
                whereString += (whereString == "" ? "" : "AND ") + "POSTATUSRANK <= " + OracleDB.QRText(data.STATUSPOTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO,  PRLOID, PRCODE, REQUESTDATE, PRODUCTNAME, PRQTY, UNITNAME, REQUESTBYNAME, ";
            sql += "PURCHASETYPENAME, PRSTATUS, PRSTATUSNAME, POCODE, POQTY, NVL(POSTATUS,'WA') POSTATUS, POSTATUSNAME, STCODE, STQTY, LOTNO, STSTATUSNAME, PRILOID, SENDPODATE, ";
            sql += "case when sysdate-REQUESTDATE >= (select configvalue from sysconfig where loid = 6) then 'Y' else 'N' end as REDWA, ";
            sql += "case when sysdate-PRAPPROVEDATE >= (select configvalue from sysconfig where loid = 7) then 'Y' else 'N' end as REDAP ";
            sql += "FROM V_PRODUCT_PR_SEARCH ";
            string sqlPc = sql;
            sql += " WHERE DIVISION<>3 " + (whereString == "" ? "" : "AND " + whereString) + " " + (wherePrStatusOt == "" ? "" : "AND " + wherePrStatusOt);

            if (data.DIVISION == Constz.PurchaseDepartment.LOID)
            {
                sql += "UNION " + sqlPc + " WHERE DIVISION=3 " + (wherePrStatusPc == "" ? "" : "AND " + wherePrStatusPc) + " " + (whereString == "" ? "" : "AND " + whereString);
            }

            sql += "ORDER BY PRCODE DESC, PRODUCTNAME ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            //string sql = "SELECT ROWNUM NO, PRLOID, PRCODE, REQUESTDATE, PRODUCTNAME, PRQTY, UNITNAME, REQUESTBYNAME, ";
            //sql += "PURCHASETYPENAME, PRSTATUS, PRSTATUSNAME, POCODE, POQTY, NVL(POSTATUS,'WA') POSTATUS, POSTATUSNAME, STCODE, STQTY, LOTNO, STSTATUSNAME, PRILOID, SENDPODATE, ";
            //sql += "case when sysdate-REQUESTDATE >= (select configvalue from sysconfig where loid = 6) then 'Y' else 'N' end as REDWA, ";
            //sql += "case when sysdate-PRAPPROVEDATE >= (select configvalue from sysconfig where loid = 7) then 'Y' else 'N' end as REDAP ";
            //sql += "FROM V_PRODUCT_PR_SEARCH ";
            //sql +=  (whereString == "" ? "" : "WHERE " + whereString);

            //sql = "SELECT PR.RANK, B.* FROM (SELECT ROWNUM RANK, PRLOID FROM (SELECT DISTINCT A.PRLOID, A.PRCODE FROM (" + " SELECT PRLOID, PRCODE FROM V_PRODUCT_PR_SEARCH " + (whereString == "" ? "" : "WHERE " + whereString) + ") A ORDER BY A.PRCODE DESC) ) PR INNER JOIN (" + sql + ") B ON PR.PRLOID = B.PRLOID  ";
            //sql += "ORDER BY B.PRCODE DESC, B.PRODUCTNAME ";
            return dt;
        }

        public DataTable GetPRItem(double PDRequest)
        {
            string sql = "SELECT PRI.LOID, ROWNUM RANK, PRI.PRODUCT, PD.NAME PRODUCTNAME, PRI.QTY, PRI.UNIT, UNIT.NAME UNITNAME, PRI.MINSTOCK, PRI.MAXSTOCK, PRI.STOCK, PRI.OLDPRICE, PRI.CURPRICE, PRI.MINPRICE, PRI.LAST3MON, PRI.LASTYEAR, PRI.DUEDATE, PD.BARCODE, UNIT.NAME UNITNAME, PRI.URGENT, PRI.ISMATERIAL ";
            sql += "FROM PRITEM PRI INNER JOIN PRODUCT PD ON PD.LOID = PRI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = PRI.UNIT ";
            sql += "WHERE PRI.PDREQUEST = '" + PDRequest + "' ";
            sql += "ORDER BY PRI.DUEDATE ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["RANK"] = i + 1;
            }
            return dt;
        }

        public DataTable GetPRItemBlank()
        {
            string sql = "SELECT 0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 MINSTOCK, 0 MAXSTOCK, 0 STOCK, 0 OLDPRICE, 0 CURPRICE, 0 MINPRICE, 0 LAST3MON, 0 LASTYEAR, NULL DUEDATE, '' BARCODE, '' UNITNAME, '' URGENT , '' ISMATERIAL ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
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

        public PRItemData GetRecentPRItem(double loid)
        {
            string sql = "SELECT * FROM v_product_pr_list WHERE LOID = '" + loid.ToString() + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            return SetRecentPRItem(dt);
        }

        public PRItemData GetRecentPRItem(string barcode)
        {
            string sql = "SELECT * FROM v_product_pr_list WHERE CODE = '" + barcode + "'";
            return SetRecentPRItem(OracleDB.ExecListCmd(sql));
        }

        private PRItemData SetRecentPRItem(DataTable table)
        {
            PRItemData data = new PRItemData();
            if (table.Rows.Count != 0)
            {
                DataRow oneRow = table.Rows[0];
                data.PRODUCT = Convert.ToDouble(oneRow["LOID"]);
                data.BARCODE = oneRow["CODE"].ToString();
                data.PRODUCTNAME = oneRow["PDNAME"].ToString();
                data.UNIT = Convert.ToDouble(oneRow["UNIT"]);
                data.UNITNAME = oneRow["UNAME"].ToString();
                data.OLDPRICE = Convert.ToDouble(oneRow["LASTPRICE"]);
                data.MINPRICE = Convert.ToDouble(oneRow["MINPRICE"]);
                data.MINSTOCK = Convert.ToDouble(oneRow["MIN"]);
                data.MAXSTOCK = Convert.ToDouble(oneRow["MAX"]);
                data.STOCK = Convert.ToDouble(oneRow["STOCK"]);
                data.LAST3MON = Convert.ToDouble(oneRow["USED3MONTH"]);
                data.LASTYEAR = Convert.ToDouble(oneRow["USED12MONTH"]);
                data.DUEDATE = Convert.ToDateTime(oneRow["DUEDATE"]);
            }
            return data;
        }

        public UnitSearchData GetUnitData(double loid)
        {
            UnitFlow uFlow = new UnitFlow();
            return uFlow.GetData(loid);
        }

        public OfficerData GetOfficerData(double loid)
        {
            OfficerDAL dal = new OfficerDAL();
            OfficerData data = new OfficerData();
            dal.GetDataByLOID(loid, null);
            data.TNAME = dal.TNAME;
            data.LASTNAME = dal.LASTNAME;
            data.DIVISION = dal.DIVISION;
            data.USERID = dal.USERID;
            data.PASSWORD = dal.PASSWORD;
            data.EFDATE = dal.EFDATE;
            data.EPDATE = dal.EPDATE;
            return data;
        }

        public PurchaseRequestData GetData(double loid)
        {
            PurchaseRequestData data = new PurchaseRequestData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                data.REQUSETDATE = DALObj.REQUESTDATE;
                data.PURCHASETYPE = DALObj.PURCHASETYPE;
                data.REQUESTBY = DALObj.REQUESTBY;
                data.DIVISION = DALObj.DIVISION;
                data.STATUS = DALObj.STATUS;
                data.REQUIREMENT = DALObj.REQUIREMENT;
                data.REASON = DALObj.REASON;
                data.REMARK = DALObj.REMARK;
                data.FROMCOMPANY = DALObj.FROMCOMPANY;
            }
            return data;
        }

        public bool ValidateData(PurchaseRequestData data)
        {
            bool ret = true;
            if (data.PURCHASETYPE == 0)
            {
                ret = false;
                _error = "กรุณาระบุประเภทใบบันทึกรายการ";
            }
            else if (data.REQUSETDATE.Year == 1)
            {
                ret = false;
                _error = "กรุณาวันที่บันทึกรายการ";
            }
            else if (data.REASON == "")
            {
                ret = false;
                _error = "กรุณาระบุเหตุผลในการขอซื้อ";
            }
            else if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            return ret;
        }

        public bool VoidData(string userID, double PDRequest)
        {
            bool ret = true;
            
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.GetDataByLOID(PDRequest, obj.zTrans);
                if (DALObj.STATUS == Constz.Requisition.Status.SP.Code)
                {
                    DALObj.STATUS = Constz.Requisition.Status.Void.Code;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                }
                _LOID = DALObj.LOID;
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

        public bool UpdateData(string userID, PurchaseRequestData data)
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
                    DALObj.REQUESTDATE = data.REQUSETDATE;
                    DALObj.ORDERTYPE = data.ORDERTYPE;
                    DALObj.PURCHASETYPE = data.PURCHASETYPE;
                    DALObj.REQUESTBY = data.REQUESTBY;
                    DALObj.DIVISION = data.DIVISION;
                    DALObj.APPROVER = data.APPROVER;
                    if (data.APPROVEDATE.Year != 1)
                    {
                        DALObj.APPROVEDATE = data.APPROVEDATE;
                    }
                    DALObj.APPROVE = data.APPROVE;
                    DALObj.ACTIVE = data.ACTIVE;
                    DALObj.STATUS = data.STATUS;
                    DALObj.REQUIREMENT = data.REQUIREMENT;
                    DALObj.REASON = data.REASON;
                    DALObj.REMARK = data.REMARK;
                    DALObj.FROMCOMPANY = data.FROMCOMPANY;

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }

                    PRItemDAL itemDAL = new PRItemDAL();
                    itemDAL.DeleteDataByPDRequest(data.LOID, obj.zTrans);
                    for (Int16 i = 0; i < data.ITEM.Count; ++i)
                    {
                        PRItemData item = (PRItemData)data.ITEM[i];
                        itemDAL.PRODUCT = item.PRODUCT;
                        itemDAL.PDREQUEST = DALObj.LOID;
                        itemDAL.QTY = item.QTY;
                        itemDAL.UNIT = item.UNIT;
                        itemDAL.MINSTOCK = item.MINSTOCK;
                        itemDAL.MAXSTOCK = item.MAXSTOCK;
                        itemDAL.STOCK = item.STOCK;
                        itemDAL.OLDPRICE = item.OLDPRICE;
                        itemDAL.CURPRICE = item.CURPRICE;
                        itemDAL.MINPRICE = item.MINPRICE;
                        itemDAL.LAST3MON = item.LAST3MON;
                        itemDAL.LASTYEAR = item.LASTYEAR;
                        itemDAL.DUEDATE = item.DUEDATE;
                        itemDAL.ACTIVE = item.ACTIVE;
                        itemDAL.URGENT = item.URGENT;
                        itemDAL.ISMATERIAL = item.ISMATERIAL;

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

        public bool VoidData(string userID, ArrayList arrData)
        {
            bool ret = true;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PRItemDAL itemDAL = new PRItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.SP.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Void.Code;
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

        public bool DeleteData(ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PRItemDAL itemDAL = new PRItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByPDRequest(Convert.ToDouble(arrData[i]), obj.zTrans);
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

        public bool UpdatePDRequestStatus(ArrayList arrData, string status, string userID)
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
                        if (GetPRItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                        PurchaseRequestData data = GetData(Convert.ToDouble(arrData[i]));
                        if (data.REASON =="") throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุเหตุผลในการขอซื้อ");
                    }
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code || DALObj.STATUS == Constz.Requisition.Status.SP.Code)
                    {
                        DALObj.STATUS = status;
                        DALObj.APPROVER = userID;
                        DALObj.APPROVEDATE = DateTime.Now.Date;
                        DALObj.APPROVE = "Y";
                        //DALObj.STATUS = "AP";
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

        public bool UpdateRequestStatus(ArrayList arrData, string status, string userID)
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
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALObj.STATUS = status;
                        DALObj.APPROVER = userID;
                        DALObj.APPROVEDATE = DateTime.Now.Date;
                        DALObj.STATUS = Constz.Requisition.Status.Void.Code;
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

        public bool CopyPDRequest(string userID, double loidSource)
        {
            PurchaseRequestData data = GetData(loidSource);
            DataTable itemList = GetPRItem(data.LOID);
            ArrayList arr = new ArrayList();
            foreach (DataRow dRow in itemList.Rows)
            {
                PRItemData idata = new PRItemData();
                idata.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                idata.QTY = Convert.ToDouble(dRow["QTY"]);
                idata.UNIT = Convert.ToDouble(dRow["UNIT"]);
                idata.MINSTOCK = Convert.ToDouble(dRow["MINSTOCK"]);
                idata.MAXSTOCK = Convert.ToDouble(dRow["MAXSTOCK"]);
                idata.STOCK = Convert.ToDouble(dRow["STOCK"]);
                idata.OLDPRICE = Convert.ToDouble(dRow["OLDPRICE"]);
                idata.CURPRICE = Convert.ToDouble(dRow["CURPRICE"]);
                idata.MINPRICE = Convert.ToDouble(dRow["MINPRICE"]);
                idata.LAST3MON = Convert.ToDouble(dRow["LAST3MON"]);
                idata.LASTYEAR = Convert.ToDouble(dRow["LASTYEAR"]);
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
