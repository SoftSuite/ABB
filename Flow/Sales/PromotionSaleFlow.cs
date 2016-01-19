using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ABB.Data.Sales;
using System.Data.OracleClient;
using System.Data;
using ABB.DAL;
using ABB.DAL.Sales;

namespace ABB.Flow.Sales
{
    public class PromotionSaleFlow
    {
        private string _error = "";
        double _LOID = 0;
        PromotionDAL _dal;
        PromotionItemDAL _dal1;
        PromotionSaleDAL search;

        public double LOID
        {
            get { return _LOID; }
        }
        public string ErrorMessage
        {
            get { return _error; }
        }
        public PromotionDAL DALObj
        {
            get { if (_dal == null) _dal = new PromotionDAL(); return _dal; }
        }
        public PromotionItemDAL ItemDALObj
        {
            get { if (_dal1 == null) _dal1 = new PromotionItemDAL(); return _dal1; }
        }
        public PromotionSaleDAL SearchDAL
        {
            get { if (search == null) { search = new PromotionSaleDAL(); } return search; }
        }
        public DataTable GetPDRequestList(PromotionSaleData data)
        {
            string whereString = "";

            if (data.WAREHOUSE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "WAREHOUSE = '" + OracleDB.QRText(Convert.ToString(data.WAREHOUSE)) + "' ";
            if (data.ZONE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "ZONE = '" + OracleDB.QRText(Convert.ToString(data.ZONE)) + "' ";
            if (data.EFDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "EFDATE >= " + OracleDB.QRDate(data.EFDATEFROM) + " ";
            if (data.EFDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "EFDATE <= " + OracleDB.QRDate(data.EFDATETO) + " ";
            if (data.EPDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "EPDATE >= " + OracleDB.QRDate(data.EPDATEFROM) + " ";
            if (data.EPDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "EPDATE <= " + OracleDB.QRDate(data.EPDATETO) + " ";

            string sql = "SELECT * ";
            sql += "FROM PROMOTION ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);

            DataTable dt = OracleDB.ExecListCmd(sql);
            //for (int i = 0; i < dt.Rows.Count; ++i)
            //{
            //    dt.Rows[i]["NO"] = i + 1;
            //}
            return dt;
        }
        public DataTable GetPromotionSalesList(PromotionDAL data)
        {
            return SearchDAL.GetPromotionList(data);
        }

        public DataTable GetDataList(string sWhere)
        {
            return DALObj.GetDataList(sWhere, null);
        }

        public PromotionSaleData GetData(double loid)
        {
            PromotionSaleData data = new PromotionSaleData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.EFDATEFROM = DALObj.EFDATE;
                data.EPDATEFROM = DALObj.EPDATE;
                data.CODE = DALObj.CODE;
                data.LOID = DALObj.LOID;
                data.NAME = DALObj.NAME;
                data.DISCOUNT = DALObj.DISCOUNT;
                data.WAREHOUSE = DALObj.WAREHOUSE;
                data.ZONE = DALObj.ZONE;
                data.CREATEON = DALObj.CREATEON;
                data.LOWERPRICE = DALObj.LOWERPRICE;
            }
            return data;
        }
        public PromotionSalesItemData GetDataPromotion(double loid)
        {
            PromotionSalesItemData data = new PromotionSalesItemData();
            if (SearchDAL.GetDataByLOID(loid, null))
            {
                data.PRICENEW = Convert.ToDouble(SearchDAL.PRICENEW);
                data.PRICEOLD = Convert.ToDouble(SearchDAL.PRICEOLD);
                data.PROMOTION = Convert.ToDouble(SearchDAL.PROMOTION);
                data.PRODUCT = Convert.ToDouble(SearchDAL.PRODUCT);
                //data.CODE = DALObj1.CODE;
                data.LOID = SearchDAL.LOID;
                data.NAME = SearchDAL.NAME;
                data.UNAME = SearchDAL.UNAME;
                data.BARCODE = SearchDAL.BARCODE;
                data.CREATEON = SearchDAL.CREATEON;
            }
            return data;
        }

        public DataTable GetPromotionItem(double loid)
        {
            DataTable dt = SearchDAL.GetPromotionItemList(loid);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["RANK"] = i + 1;
            }
            return dt;
        }
        public DataTable GetPromotionItemBlank()
        {
            return SearchDAL.GetPromotionItemListBlank();
        }

        public DataTable GetAllProductList(double discountPercent)
        {
            DataTable dt = SearchDAL.GetAllProductList(discountPercent);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["RANK"] = i + 1;
            }
            return dt;
        }

        private bool VeridateData(PromotionSaleData dal)
        {
            bool ret = true;
            if (dal.CODE.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุรหัสส่งเสริมการขาย";
            }
            else if (dal.NAME.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุชื่อการส่งเสริมการขาย";
            }
            else if (dal.WAREHOUSE == 0)
            {
                ret = false;
                _error = "กรุณาเลือกคลังสินค้า";
            }
            else if (dal.EFDATEFROM.Year == 1)
            {
                ret = false;
                _error = "กรุณาระบุวันที่เริ่มใช้";
            }
            else if (dal.EPDATEFROM.Year == 1)
            {
                ret = false;
                _error = "กรุณาระบุวันที่สิ้นสุด";
            }
            else if (dal.EFDATEFROM > dal.EPDATEFROM)
            {
                ret = false;
                _error = "กรุณาระบุวันที่เริ่มใช้ และวันที่สิ้นสุดให้ถูกต้อง";
            }
            else if (dal.DISCOUNT <=0)
            {
                ret = false;
                _error = "กรุณาระบุส่วนลด";
            }
            else if (dal.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้าที่ต้องการลดราคา";
            }
            return ret;
        }

       

        public bool UpdateData(string userID, PromotionSaleData data)
        {
            bool ret = true;
            if (VeridateData(data))
            {

                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    DALObj.NAME = data.NAME.Trim();
                    DALObj.EFDATE = data.EFDATEFROM;
                    DALObj.EPDATE = data.EPDATEFROM;
                    DALObj.CODE = data.CODE.Trim();
                    DALObj.DISCOUNT = Convert.ToDouble(data.DISCOUNT);
                    DALObj.WAREHOUSE = Convert.ToDouble(data.WAREHOUSE);
                    DALObj.ZONE = Convert.ToDouble(data.ZONE);
                    DALObj.LOWERPRICE = data.LOWERPRICE;

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }

                    ItemDALObj.DeleteDataByPromotion(data.LOID , obj.zTrans);
                    for (Int16 i = 0; i < data.ITEM.Count; ++i)
                    {
                        PromotionSalesItemData item = (PromotionSalesItemData)data.ITEM[i];
                        ItemDALObj.PRODUCT = Convert.ToDouble(item.PRODUCT);
                        ItemDALObj.PROMOTION = DALObj.LOID;
                        ItemDALObj.PRICENEW = Convert.ToDouble(item.PRICENEW);
                        ItemDALObj.PRICEOLD = Convert.ToDouble(item.PRICEOLD);
                        ItemDALObj.OnDB = false;
                        ret = ItemDALObj.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(ItemDALObj.ErrorMessage);
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
            else ret = false;
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
                for (int i = 0; i < arrData.Count; i++)
                {
                    ret = ItemDALObj.DeleteDataByPromotion(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    DALObj.GetDataByLOID( Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = DALObj.DeleteCurrentData(obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
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

        public bool CopyPromotion(string userID, double loidSource)
        {
            PromotionSaleData data = GetData(loidSource);
            DataTable itemList = GetPromotionItem(data.LOID);
            ArrayList arr = new ArrayList();
            foreach (DataRow dRow in itemList.Rows)
            {
                PromotionSalesItemData idata = new PromotionSalesItemData();
                idata.LOID = data.LOID;
                idata.BARCODE = Convert.ToString(dRow["BARCODE"]);
                idata.NAME = Convert.ToString(dRow["NAME"]);
                idata.PRICENEW = Convert.ToDouble(dRow["PRICENEW"]);
                idata.PRICEOLD = Convert.ToDouble(dRow["PRICEOLD"]);
                idata.UNAME = Convert.ToString(dRow["UNAME"]);
                arr.Add(idata);
            }
            //data.ITEM = arr;
            DALObj.OnDB = false;
            data.LOID = 0;
            return UpdateData(userID, data);
        }

        public bool NewPromotion(string userID, PromotionSaleData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.NAME = data.NAME.Trim();
                DALObj.EFDATE = data.EFDATEFROM;
                DALObj.EFDATE = data.EFDATETO;
                DALObj.EPDATE = data.EPDATEFROM;
                DALObj.EPDATE = data.EPDATETO;
                DALObj.CODE = data.CODE.Trim();
                DALObj.DISCOUNT = Convert.ToDouble(data.DISCOUNT);
                DALObj.WAREHOUSE = Convert.ToDouble(data.WAREHOUSE);
                DALObj.ZONE = Convert.ToDouble(data.ZONE);
                DALObj.LOWERPRICE = data.LOWERPRICE;

                ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                _LOID = DALObj.LOID;
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

        public PromotionSaleData GetPromotionData(double loid)
        {
            return GetData(loid);
        }

        public PromotionSalesItemData GetPromotionData1(double loid)
        {
            return GetDataPromotion(loid);
        }

        public ProductSearchData GetProductData(string barcode)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(barcode);
        }

        public ProductSearchData GetProductData(double product)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(product);
        }

        public string GetUnitName(double unit)
        {
            UnitFlow uFlow = new UnitFlow();
            UnitSearchData data = uFlow.GetData(unit);
            return data.NAME;
        }

    }
}
