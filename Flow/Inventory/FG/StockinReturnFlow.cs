using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data.Inventory.FG;
using ABB.Data;
using ABB.DAL;
using ABB.DAL.Inventory;
using ABB.DAL.Inventory.FG;

namespace ABB.Flow.Inventory.FG
{
    public class StockinReturnFlow
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

        StockinReturnDAL _dal;
        public StockinReturnDAL DALObj
        {
            get { if (_dal == null) { _dal = new StockinReturnDAL(); } return _dal; }
        }

        public DataTable GetItemList(StockinReturnSearchData data)
        {
            string whereString = "";

            if (data.DOCTYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "DOCTYPE = " + data.DOCTYPE.ToString() + " ";
            if (data.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";
            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
            //if (data.CUSTOMER != 0)
            //    whereString += (whereString == "" ? "" : "AND ") + "SENDER = " + data.CUSTOMER.ToString() + " ";
            if (data.NAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CNAME) LIKE '%" + data.NAME.Trim().ToUpper() + "%' ";
            if (data.LNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CLASTNAME) LIKE '%" + data.LNAME.Trim().ToUpper() + "%' ";
            if (data.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(data.STATUSFROM.Trim()) + "' ";
            if (data.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(data.STATUSTO.Trim()) + "' ";

            string sql = "select * from ( SELECT ROWNUM NO, ST.LOID, ST.DOCTYPE, VF.DOCNAME, ST.CODE, ST.RECEIVEDATE, ST.CREATEBY, ST.SENDER, VC.CUSTOMERNAME, ST.GRANDTOT,VC.CNAME,VC.CLASTNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.StockinReturn.Status.Waiting.Code + "' THEN '" + Constz.StockinReturn.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.StockinReturn.Status.Approved.Code + "' THEN '" + Constz.StockinReturn.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.StockinReturn.Status.Void.Code + "' THEN '" + Constz.StockinReturn.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.StockinReturn.Status.Waiting.Code + "' THEN '" + Constz.StockinReturn.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.StockinReturn.Status.Approved.Code + "' THEN '" + Constz.StockinReturn.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.StockinReturn.Status.Void.Code + "' THEN '" + Constz.StockinReturn.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK ";
            sql += "FROM STOCKIN ST INNER JOIN V_RETURNTYPE_FG VF ON ST.DOCTYPE = VF.DOCTYPE ";
            sql += "INNER JOIN V_CUSTOMER VC ON ST.SENDER = VC.LOID) ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY CODE ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            return dt;
        }

        public DataTable GetReturnWHList(StockinReturnSearchData data)
        {
            string whereString = "";

            if (data.PRODUCTNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(PRODUCTNAME) LIKE '%" + data.PRODUCTNAME.Trim().ToUpper() + "%' ";
            if (data.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) =" + data.CODE.Trim().ToUpper() + " ";
            if (data.RQCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(REQCODE) =" + data.RQCODE.Trim().ToUpper() + " ";
            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
            if (data.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(data.STATUSFROM.Trim()) + "' ";
            if (data.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(data.STATUSTO.Trim()) + "' ";

            string sql = "select * from ( SELECT ROWNUM NO,ST.LOID, ST.CODE, ST.RECEIVEDATE, VM.PDLOID, VM.PRODUCTNAME, VM.PDQTY, VM.UNITNAME,VM.LOTNO, VM.REQCODE, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Rank + "' ";
            sql += "ELSE '' END AS RANK ";
            sql += "FROM STOCKIN ST INNER JOIN V_MATERIAL_RETURN_POPUP_LIST VM ON ST.REFTABLE = 'REQUISITION' AND ST.REFLOID = VM.LOID) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY RECEIVEDATE ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            return dt;
        }

        public DataTable GetRefData(string refloid)
        {
            string sql = "SELECT DOCCODE, CUSTOMER, REFTABLE ";
            sql += "FROM V_RETURN_WAIT_DOC ";
            sql += "WHERE REFLOID = " + refloid;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetCustomerData(string loid)
        {
            string sql = "SELECT CODE, CUSTOMERNAME, CTITLE, CNAME, CLASTNAME, BILLADDRESSFULL, BILLTEL, BILLFAX ";
            sql += "FROM V_CUSTOMER_ADDRESS ";
            sql += "WHERE CULOID = " + loid;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetWareHouseData(string loid)
        {
            string sql = "SELECT CODE, NAME ";
            sql += "FROM WAREHOUSE ";
            sql += "WHERE LOID = " + loid;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetDocType(string type)
        {
            string sql = "SELECT REFLOID, REFTABLE, REFTYPE FROM V_RETURNTYPE_FG ";
            sql += "WHERE DOCTYPE = " + type;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetViewReturnWaitList(string pdloid, string lotno)
        {
            string sql = "SELECT PDNAME, LOTNO, QTY,0 QTYLOST, PRICE, UNAME, ULOID, REFTABLE, REFLOID, BARCODE, (PRICE*QTY) AS NETPRICE,QTY AS OLDQTY FROM V_RETURN_WAIT_LIST ";
            sql += "WHERE PDLOID = '" + pdloid + "' AND LOTNO = '" + lotno + "'";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReturnWaitListRefLoid(string pdloid, string refloid)
        {
            string sql = "SELECT PDNAME, LOTNO, QTY,0 QTYLOST, PRICE, UNAME, ULOID, REFTABLE, REFLOID, BARCODE, (PRICE*QTY) AS NETPRICE,QTY AS OLDQTY FROM V_RETURN_WAIT_LIST ";
            sql += "WHERE PDLOID = '" + pdloid + "' AND REFLOID = '" + refloid + "'";
            return OracleDB.ExecListCmd(sql);
        }


        public StockinReturnData GetData(double loid)
        {
            StockinReturnData data = new StockinReturnData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CREATEBY = DALObj.CREATEBY;
                data.CODE = DALObj.CODE;
                data.DOCTYPE = DALObj.DOCTYPE;
                data.SENDER = DALObj.SENDER;
                data.RECEIVEDATE = DALObj.RECEIVEDATE;
                data.STATUS = DALObj.STATUS;
                data.REMARK = DALObj.REMARK;
                data.GRANDTOT = DALObj.GRANDTOT;
                data.CADDRESS = DALObj.CADDRESS;
                data.REFLOID = DALObj.REFLOID;
                data.REFTABLE = DALObj.REFTABLE;
                data.CTITLE = DALObj.CTITLE;
                data.CNAME = DALObj.CNAME;
                data.CLASTNAME = DALObj.CLASTNAME;
                data.CTEL = DALObj.CTEL;
                data.CFAX = DALObj.CFAX;
            }
            return data;
        }

        public DataTable GetItem(double LOID)
        {
            string sql = "SELECT STI.LOID, ROWNUM RANK , STI.PRODUCT, PD.BARCODE, PD.NAME PDNAME, STI.LOTNO, STI.QTY QTY, STI.QTY OLDQTY, STI.QTYLOST, STI.UNIT, UN.NAME UNITNAME, STI.REFLOID, STI.REFTABLE, STI.STATUS, STI.PRICE, STI.PRICE*(STI.QTY + STI.QTYLOST) AS NETPRICE ";
            sql += "FROM STOCKINITEM STI ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID=STI.PRODUCT ";
            sql += "INNER JOIN UNIT UN ON UN.LOID=STI.UNIT ";
            sql += "WHERE STI.STOCKIN = " + LOID;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReqmaterial(double refloid)
        {
            string sql = "SELECT RM.LOID, ROWNUM RANK , RM.PRODUCT, PD.BARCODE, PD.NAME PDNAME, '' LOTNO, RM.RETURNQTY QTY, RM.UNIT, UN.NAME UNITNAME, RM.LOID REFLOID, 'REQMATERIAL' REFTABLE, 'WA' STATUS, PD.PRICE, PD.PRICE*RM.RETURNQTY AS NETPRICE ";
            sql += "FROM REQMATERIAL RM ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID=RM.PRODUCT ";
            sql += "INNER JOIN UNIT UN ON UN.LOID=RM.UNIT ";
            sql += "WHERE RM.REQUISITION = " + refloid;
            return OracleDB.ExecListCmd(sql);
        }


        public StockinReturnItemData GetReqmaterialProduct(double refloid, double product)
        {
            string sql = "SELECT RM.LOID, ROWNUM RANK , RM.PRODUCT, PD.BARCODE, PD.NAME PDNAME, '' LOTNO, RM.RETURNQTY QTY, RM.UNIT, UN.NAME UNITNAME, RM.LOID REFLOID, 'REQMATERIAL' REFTABLE, 'WA' STATUS, PD.PRICE, PD.PRICE*RM.RETURNQTY AS NETPRICE ";
            sql += "FROM REQMATERIAL RM ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID=RM.PRODUCT ";
            sql += "INNER JOIN UNIT UN ON UN.LOID=RM.UNIT ";
            sql += "WHERE RM.REQUISITION = " + refloid + " AND RM.PRODUCT = " + product;

            DataTable dt = OracleDB.ExecListCmd(sql);
            StockinReturnItemData data = new StockinReturnItemData();
            if (dt.Rows.Count > 0)
            {
                data.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                data.QTY = Convert.ToDouble(dt.Rows[0]["QTY"]);
                data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
                data.PRICE = Convert.ToDouble(dt.Rows[0]["PRICE"]);
                data.PRODUCT = Convert.ToDouble(dt.Rows[0]["PRODUCT"]);
                data.REFLOID = Convert.ToDouble(dt.Rows[0]["REFLOID"]);
                data.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
            }

            return data;
        }
        public double GetPrice(string LotNo, double product)
        {
            string sql = "SELECT PRICE ";
            sql += "FROM V_MATERIAL_RETURN_WAIT_LIST  RM";
            sql += "WHERE RM.PRODUCT = " + product + " AND RM.LOTNO = " + LotNo;

            DataTable dt = OracleDB.ExecListCmd(sql);
            double price = 0;
            if (dt.Rows.Count > 0)
            {
                price = Convert.ToDouble(dt.Rows[0]["PRICE"]);
            }

            return price;
        }

        public StockinReturnItemData GetReqmaterialBarcode(double refloid, string barcode)
        {
            string sql = "SELECT RM.LOID, ROWNUM RANK , RM.PRODUCT, PD.BARCODE, PD.NAME PDNAME, '' LOTNO, RM.RETURNQTY QTY, RM.UNIT, UN.NAME UNITNAME, RM.LOID REFLOID, 'REQMATERIAL' REFTABLE, 'WA' STATUS, PD.PRICE, PD.PRICE*RM.RETURNQTY AS NETPRICE ";
            sql += "FROM REQMATERIAL RM ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID=RM.PRODUCT ";
            sql += "INNER JOIN UNIT UN ON UN.LOID=RM.UNIT ";
            sql += "WHERE RM.REQUISITION = " + refloid + " AND PD.BARCODE = " + barcode;

            DataTable dt = OracleDB.ExecListCmd(sql);
            StockinReturnItemData data = new StockinReturnItemData();
            if (dt.Rows.Count > 0)
            {
                data.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                data.QTY = Convert.ToDouble(dt.Rows[0]["QTY"]);
                data.UNITNAME = dt.Rows[0]["UNITNAME"].ToString();
                data.PRICE = Convert.ToDouble(dt.Rows[0]["PRICE"]);
                data.PRODUCT = Convert.ToDouble(dt.Rows[0]["PRODUCT"]);
                data.REFLOID = Convert.ToDouble(dt.Rows[0]["REFLOID"]);
                data.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
            }

            return data;
        }

        public DataTable GetProductLot(double refloid, double pploid)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return CompareLot(GetReqmaterial(refloid), pploid);
        }

        private DataTable CompareLot(DataTable dtReqItem, double pploid)
        {
            DataTable dtReqWithLot = new DataTable();
            dtReqWithLot.Columns.Add("LOID", typeof(double));
            dtReqWithLot.Columns.Add("RANK", typeof(double));
            dtReqWithLot.Columns.Add("NO", typeof(double));
            dtReqWithLot.Columns.Add("PRODUCT", typeof(double));
            dtReqWithLot.Columns.Add("BARCODE", typeof(string));
            dtReqWithLot.Columns.Add("LOTNO", typeof(string));
            dtReqWithLot.Columns.Add("REMAINQTY", typeof(double));
            dtReqWithLot.Columns.Add("QTY", typeof(double));
            dtReqWithLot.Columns.Add("UNIT", typeof(string));
            dtReqWithLot.Columns.Add("UNITNAME", typeof(string));
            dtReqWithLot.Columns.Add("PRICE", typeof(string));
            dtReqWithLot.Columns.Add("NETPRICE", typeof(string));
            dtReqWithLot.Columns.Add("REFLOID", typeof(double));

            int Rank = 0;
            for (int iReq = 0; iReq < dtReqItem.Rows.Count; iReq++)
            {
                double Product = Convert.ToDouble(dtReqItem.Rows[iReq]["PRODUCT"]);
                DataTable dtLot = GetProductStock(pploid, Product);
                double allQTY = Convert.ToDouble(dtReqItem.Rows[iReq]["QTY"]);
                for (int iLot = 0; iLot < dtLot.Rows.Count; iLot++)
                {
                    double QTY = Convert.ToDouble(dtLot.Rows[iLot]["QTY"]);
                    if (QTY <= allQTY)
                        allQTY = allQTY - QTY;
                    else
                    {
                        QTY = allQTY;
                        allQTY = 0;
                    }

                    DataRow newRow = dtReqWithLot.NewRow();
                    Rank = Rank + 1;
                    newRow["RANK"] = Rank;
                    newRow["LOID"] = dtReqItem.Rows[iReq]["LOID"].ToString();
                    newRow["PRODUCT"] = Product;
                    newRow["BARCODE"] = dtReqItem.Rows[iReq]["BARCODE"].ToString();
                    newRow["LOTNO"] = dtLot.Rows[iLot]["LOTNO"].ToString();
                    newRow["QTY"] = QTY;
                    newRow["UNIT"] = dtReqItem.Rows[iReq]["UNIT"].ToString();
                    newRow["UNITNAME"] = dtReqItem.Rows[iReq]["UNITNAME"].ToString();
                    newRow["PRICE"] = dtLot.Rows[iLot]["PRICE"].ToString();
                    newRow["NETPRICE"] = QTY * Convert.ToDouble(dtReqItem.Rows[iReq]["PRICE"]);
                    newRow["REFLOID"] = Convert.ToDouble(dtReqItem.Rows[iReq]["REFLOID"]);
                    dtReqWithLot.Rows.Add(newRow);

                    if (allQTY <= 0)
                        break;
                }//for lot
            }//for item

            return dtReqWithLot;
        }
        public DataTable GetProductStock(double pploid, double product)
        {
            string sql = "SELECT * ";
            sql += "FROM V_MATERIAL_RETURN_WAIT_LIST ";
            sql += "WHERE PPLOID = " + pploid.ToString() + " AND PRODUCT = " + product.ToString() +" ORDER BY LOTNO ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProduct(double refloid)
        {
            string sql = "SELECT RM.PRODUCT PRODUCT, PD.NAME PRODUCTNAME ";
            sql += "FROM REQMATERIAL RM INNER JOIN PRODUCT PD ON RM.PRODUCT = PD.LOID ";
            sql += "WHERE RM.REQUISITION = " + refloid.ToString() + " ORDER BY PD.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetItemBlank()
        {
            string sql = "SELECT 0 LOID, 0 PRODUCT, '' BARCODE, '' PDNAME, '' LOTNO, 0 QTY, 0 UNIT, '' UNITNAME, 0 REFLOID, '' REFTABLE, '' STATUS, 0 PRICE, 0 NETPRICE ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public double GetApprover(string userid)
        {
            string sql = "SELECT LOID FROM OFFICER WHERE USERID = '" + userid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double approver = 0;
            if (dt.Rows.Count > 0)
            {
                approver = Convert.ToDouble(dt.Rows[0]["LOID"]);
            }

            return approver;
        }

        public bool ValidateData(StockinReturnData data)
        {
            bool ret = true;
            if (data.SENDER == 0)
            {
                ret = false;
                _error = "กรุณาเลือกลูกค้า";
            }

            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }

            return ret;
        }

        private void UpdateData(string userID, StockinReturnData data, System.Data.OracleClient.OracleTransaction trans)
        {
            bool ret = true;

            DALObj.CODE = data.CODE;
            DALObj.DOCTYPE = data.DOCTYPE;
            DALObj.SENDER = data.SENDER;
            DALObj.RECEIVEDATE = data.RECEIVEDATE;
            DALObj.STATUS = data.STATUS;
            DALObj.REMARK = data.REMARK;
            DALObj.GRANDTOT = data.GRANDTOT;
            DALObj.CADDRESS = data.CADDRESS;
            DALObj.REFLOID = data.REFLOID;
            DALObj.REFTABLE = data.REFTABLE;
            DALObj.CTITLE = data.CTITLE;
            DALObj.CNAME = data.CNAME;
            DALObj.CLASTNAME = data.CLASTNAME;
            DALObj.CTEL = data.CTEL;
            DALObj.CFAX = data.CFAX;
            DALObj.RECEIVER = data.RECEIVER;
            DALObj.APPROVEDATE = data.APPROVEDATE;
            DALObj.APPROVER = data.APPROVER;

            if (DALObj.OnDB)
            {
                ret = DALObj.UpdateCurrentData(userID, trans);
            }
            else
            {
                ret = DALObj.InsertCurrentData(userID, trans);
            }

            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
            _LOID = DALObj.LOID;


            StockInItemDAL itemDAL = new StockInItemDAL();
            itemDAL.DeleteDataByStockIn(data.LOID, trans);
            for (Int16 i = 0; i < data.ITEM.Count; ++i)
            {
                StockinReturnItemData item = (StockinReturnItemData)data.ITEM[i];
                itemDAL.STOCKIN = DALObj.LOID;
                itemDAL.PRODUCT = item.PRODUCT;
                itemDAL.UNIT = item.UNIT;
                itemDAL.LOTNO = item.LOTNO;
                itemDAL.PRICE = item.PRICE;
                itemDAL.QTY = item.QTY;
                itemDAL.QTYLOST = item.QTYLOST;
                itemDAL.REFLOID = item.REFLOID;
                itemDAL.REFTABLE = item.REFTABLE;
                itemDAL.STATUS = DALObj.STATUS;

                itemDAL.OnDB = false;
                ret = itemDAL.InsertCurrentData(userID, trans);
                if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
            }
        }

        public bool UpdateData(string userID, StockinReturnData data)
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

        public bool CommitData(string userID, StockinReturnData data)
        {
            bool ret = true;
            bool cutstock = true;
            bool custockinreturnlost = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    cutstock = (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code);
                    custockinreturnlost = cutstock;
                    UpdateData(userID, data, obj.zTrans);
                    if (cutstock)
                    {
                        StockInDAL sDAL = new StockInDAL();
                        ret = sDAL.CutStock(DALObj.LOID, userID, obj.zTrans);
                        if (!ret) _error = sDAL.ErrorMessage;
                    }

                    if (custockinreturnlost)
                    {
                        StockInDAL sDAL = new StockInDAL();
                        ret = sDAL.CutStockInReturnLost(DALObj.LOID, userID, obj.zTrans);
                        if (!ret) _error = sDAL.ErrorMessage;
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
                StockInItemDAL itemDAL = new StockInItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByStockIn(Convert.ToDouble(arrData[i]), obj.zTrans);
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

        public bool UpdateStockinReturnStatus(ArrayList arrData, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    if (GetItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                    {
                        throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                    }
                    StockinReturnData data = GetData(Convert.ToDouble(arrData[i]));

                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.StockinReturn.Status.Waiting.Code)
                    {
                        DALObj.STATUS = Constz.StockinReturn.Status.Approved.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        StockInDAL sDAL = new StockInDAL();
                        ret = sDAL.CutStock(DALObj.LOID, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(sDAL.ErrorMessage);
                    }
                    StockInItemDAL itemDAL = new StockInItemDAL();
                    ret = itemDAL.UpdateStatusByStockIn(Convert.ToDouble(arrData[i]), Constz.StockinReturn.Status.Approved.Code, userID, obj.zTrans);
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
            return ret;
        }
        public bool UpdateStockinReturnWHStatus(ArrayList arrData, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    if (GetItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                    {
                        throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                    }
                    StockinReturnData data = GetData(Convert.ToDouble(arrData[i]));

                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Finish.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    }
                    StockInItemDAL itemDAL = new StockInItemDAL();
                    ret = itemDAL.UpdateStatusByStockIn(Convert.ToDouble(arrData[i]), Constz.StockinReturn.Status.Approved.Code, userID, obj.zTrans);
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
            return ret;
        }
        public StockinReturnData GetPDData(double loid)
        {
            StockFGDAL pFlow = new StockFGDAL();
            return pFlow.DoGetPDData(loid);
        }
    }
}
