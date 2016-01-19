using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data.Inventory.FG;
using ABB.Data;
using ABB.DAL;

namespace ABB.Flow.Inventory.FG
{
    public class StockoutBasketFlow
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

        BasketDAL _dal;
        public BasketDAL DALObj
        {
            get { if (_dal == null) { _dal = new BasketDAL(); } return _dal; }
        }

        public DataTable GetBasketList(StockoutBasketSearchData data)
        {
            string whereString = "";

            if (data.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";
            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CHECKDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CHECKDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
            if (data.PDNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(PDNAME) LIKE '%" + data.PDNAME.Trim().ToUpper() + "%' ";
            if (data.CREATEBY.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CREATEBY) LIKE '%" + data.CREATEBY.Trim().ToUpper() + "%' ";
            if (data.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(data.STATUSFROM.Trim()) + " ";
            if (data.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(data.STATUSTO.Trim()) + " ";

            string sql = "select * from ( SELECT ROWNUM NO, BS.LOID, BS.CODE, BS.CHECKDATE, BS.CREATEBY, PD.NAME PDNAME, BS.QTY, UN.NAME UNITNAME, BS.STOCKINDATE, ";
            sql += "CASE BS.STATUS WHEN '" + Constz.Basket.Status.Waiting.Code + "' THEN '" + Constz.Basket.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Basket.Status.Approved.Code + "' THEN '" + Constz.Basket.Status.Approved.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE BS.TYPE WHEN '" + Constz.Basket.Type.New.Code + "' THEN '" + Constz.Basket.Type.New.Name + "' ";
            sql += "WHEN '" + Constz.Basket.Type.Return.Code + "' THEN '" + Constz.Basket.Type.Return.Name + "' ";
            sql += "ELSE '' END AS TYPENAME, ";
            sql += "CASE BS.STATUS WHEN '" + Constz.Basket.Status.Waiting.Code + "' THEN '" + Constz.Basket.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Basket.Status.Approved.Code + "' THEN '" + Constz.Basket.Status.Approved.Rank + "' ";
            sql += "ELSE '' END AS RANK ";
            sql += "FROM BASKET BS INNER JOIN PRODUCT PD ON BS.PRODUCT = PD.LOID ";
            sql += "INNER JOIN UNIT UN ON BS.UNIT = UN.LOID ";
            sql += "LEFT JOIN WAREHOUSE WA ON BS.WAREHOUSE = WA.LOID) ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY CODE ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            return dt;
        }

        public StockoutBasketData GetData(double loid)
        {
            StockoutBasketData data = new StockoutBasketData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                data.CHECKDATE = DALObj.CHECKDATE;
                data.PRODUCT = DALObj.PRODUCT;
                data.QTY = DALObj.QTY;
                data.STATUS = DALObj.STATUS;
                data.STOCKINDATE = DALObj.STOCKINDATE;
                data.REMARK = DALObj.REMARK;
                data.TYPE = DALObj.TYPE;
                data.UNIT = DALObj.UNIT;
                data.LOTNO = DALObj.LOTNO;
                data.WAREHOUSE = DALObj.WAREHOUSE;
            }
            return data;
        }

        public bool ValidateData(StockoutBasketData data)
        {
            bool ret = true;
            if (data.PRODUCT == 0)
            {
                ret = false;
                _error = "กรุณาระบุกระเช้า";
            }

            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }

            if (data.QTY == 0)
            {
                ret = false;
                _error = "กรุณาระบุจำนวนกระเช้า";
            }

            string sql = "SELECT PK.SUBPRODUCT, PK.QTY, PD.NAME PDNAME FROM PACKAGE PK INNER JOIN PRODUCT PD ON PD.LOID = PK.SUBPRODUCT WHERE PK.MAINPRODUCT = '" + data.PRODUCT.ToString() + "'";
            DataTable tb = OracleDB.ExecListCmd(sql);
            for (Int16 i = 0; i < tb.Rows.Count; ++i)
            {
                double Product = Convert.ToDouble(tb.Rows[i]["SUBPRODUCT"]);
                double Qty = Convert.ToDouble(tb.Rows[i]["QTY"]) * data.QTY;
                double AllQty = 0;
                for (Int16 j = 0; j < data.ITEM.Count; ++j)
                {
                    BasketItemData itemCompare = (BasketItemData)data.ITEM[j];
                    if (Product == itemCompare.PRODUCT)
                    {
                        AllQty += itemCompare.QTY;
                    }
                }
                if (AllQty != Qty)
                {
                    _error = "จำนวนของ " + tb.Rows[i]["PDNAME"].ToString() + " ไม่ถูกต้อง";
                    return false;
                }
            }

            return ret;
        }

        public bool UpdateData(string userID, StockoutBasketData data)
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
                    DALObj.CHECKDATE = data.CHECKDATE;
                    DALObj.PRODUCT = data.PRODUCT;
                    DALObj.QTY = data.QTY;
                    if (data.STOCKINDATE.Year != 1)
                    {
                        DALObj.STOCKINDATE = data.STOCKINDATE;
                    }
                    DALObj.REMARK = data.REMARK;
                    DALObj.TYPE = data.TYPE;
                    DALObj.UNIT = data.UNIT;
                    DALObj.STATUS = data.STATUS;
                    DALObj.LOTNO = data.LOTNO;
                    DALObj.WAREHOUSE = data.WAREHOUSE;

                    if (DALObj.OnDB)
                    {
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    }
                    else
                    {
                        DALObj.STATUS = data.STATUS;
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    }

                    _LOID = DALObj.LOID;
                    

                    BasketItemDAL itemDAL = new BasketItemDAL();
                    itemDAL.DeleteDataByBasket(data.LOID, obj.zTrans);
                    for (Int16 i = 0; i < data.ITEM.Count; ++i)
                    {
                        BasketItemData item = (BasketItemData)data.ITEM[i];
                        itemDAL.BASKET = DALObj.LOID;
                        itemDAL.PRODUCT = item.PRODUCT;
                        itemDAL.UNIT = item.UNIT;
                        itemDAL.LOTNO = item.LOTNO;
                        itemDAL.PACKAGE = item.PACKAGE;
                        itemDAL.QTY = item.QTY;
                        itemDAL.PRODUCTSTOCK = item.PRODUCTSTOCK;

                        itemDAL.OnDB = false;
                        ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
                    }

                    if (data.STATUS == Constz.Requisition.Status.Approved.Code)
                    {
                        ret = DALObj.CutStockBasket(DALObj.LOID, userID, obj.zTrans);
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
                BasketItemDAL itemDAL = new BasketItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByBasket(Convert.ToDouble(arrData[i]), obj.zTrans);
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

        public bool UpdateBasketStatus(ArrayList arrData, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    if (GetBasketItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                    {
                        throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                    }
                    StockoutBasketData data = GetData(Convert.ToDouble(arrData[i]));

                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = DALObj.CutStockBasket(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
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

        public DataTable GetProductPackage(string PDLOID)
        {
            string sql = "SELECT BARCODE, NAME, UNIT, UNITNAME, PRICE ";
            sql += "FROM V_PRODUCT_PACKAGE ";
            sql += "WHERE LOID = " + PDLOID;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetSubProductData(string MainPDLOID, string SubPDLOID)
        {
            string sql = "SELECT PD.BARCODE, PK.QTY, PK.UNIT, UN.NAME UNITNAME, PK.LOID PACKAGE, PST.LOID PRODUCTSTOCK ";
            sql += "FROM PACKAGE PK  ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID = PK.SUBPRODUCT ";
            sql += "INNER JOIN UNIT UN ON UN.LOID = PK.UNIT ";
            sql += "INNER JOIN PRODUCTSTOCK PST ON PST.PRODUCT = PK.SUBPRODUCT ";
            sql += "WHERE PK.MAINPRODUCT = '" + MainPDLOID + "' AND PK.SUBPRODUCT = '" + SubPDLOID + "' ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetSYSQTY(string SubPDLOID, string LotNo)
        {
            string sql = "SELECT SYSQTY FROM V_STOCKOUT_BASKET_PRODUCT ";
            sql += "WHERE SUBPRODUCT = '" + SubPDLOID + "' AND LOTNO = '" + LotNo + "' ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetBasketItem(double BasketLOID)
        {
            string sql = "SELECT BSI.LOID, ROWNUM RANK , BSI.PRODUCT, PD.BARCODE, PD.NAME PDNAME, BSI.LOTNO, BSI.QTY, BSI.UNIT, UN.NAME UNITNAME, BSI.PACKAGE, BSI.PRODUCTSTOCK ";
            sql += "FROM BASKETITEM BSI ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID=BSI.PRODUCT ";
            sql += "INNER JOIN UNIT UN ON UN.LOID=BSI.UNIT ";
            sql += "WHERE BSI.BASKET = " + BasketLOID;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductBasket(double pdloid)
        {
            string sql = "SELECT MAINPRODUCT LOID, ROWNUM RANK , SUBPRODUCT PRODUCT, SUBBARCODE BARCODE, SUBNAME PDNAME, LOTNO, QTY, UNIT, UNITNAME, PACKAGE, PRODUCTSTOCK ";
            sql += "FROM V_STOCKOUT_BASKET_PRODUCT ";
            sql += "WHERE WAREHOUSE = '1' AND MAINPRODUCT = " + pdloid;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetBasketItemBlank()
        {
            string sql = "SELECT 0 LOID, 0 PRODUCT, '' BARCODE, '' PDNAME, '' LOTNO, 0 QTY, 0 UNIT, '' UNITNAME, 0 PACKAGE, 0 PRODUCTSTOCK ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }
    }
}
