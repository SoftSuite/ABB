using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.Data;
using ABB.Data.Sales;
using ABB.Data.Inventory.WH;

namespace ABB.Flow.Inventory.WH
{
    public class ProductFlow
    {
        private string _error = "";
        double _LOID = 0;
        public string ErrorMessage
        {
            get { return _error; }
        }
        public double LOID
        {
            get { return _LOID; }
        }
        ProductDAL _dal;
        public ProductDAL DALObj
        {
            get { if (_dal == null) { _dal = new ProductDAL(); } return _dal; }
        }

        ProductMonthDAL _dal2;
        public ProductMonthDAL DALObj2
        {
            get { if (_dal2 == null) { _dal2 = new ProductMonthDAL(); } return _dal2; }
        }

        private ProductMasterDAL _masterdal;
        private ProductBarcodeDAL _barcodedal;

        public ProductMasterDAL MasterDALObj
        {
            get { if (_masterdal == null) { _masterdal = new ProductMasterDAL(); } return _masterdal; }
        }
        public ProductBarcodeDAL BarcodeDALObj
        {
            get { if (_barcodedal == null) { _barcodedal = new ProductBarcodeDAL(); } return _barcodedal; }
        }

        public DataTable GetDataList()
        {
            return DALObj.GetDataList("", "LOID", null);
        }

        public DataTable GetDataList(string sWhere)
        {
            return DALObj.GetDataList(sWhere, "LOID", null);
        }

        public DataTable GetProductList(string sWhere)
        {
            string str = "SELECT PD.LOID, PD.PRODUCTMASTER, PD.BARCODE, PD.NAME, U.NAME UNIT, PG.NAME PRODUCTGROUPNAME, ";
            str += "PT.NAME PRODUCTTYPENAME, PD.COST, PT.TYPE, PG.PRODUCTTYPE, PD.PRODUCTGROUP, PD.ISDEFAULT ";
            str += "FROM PRODUCT PD ";
            str += "INNER JOIN PRODUCTGROUP PG ON PD.PRODUCTGROUP = PG.LOID ";
            str += "INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID ";
            str += "INNER JOIN UNIT U ON PD.UNIT = U.LOID ";
            str += sWhere + " ";
            str += "ORDER BY PD.NAME ";

            return OracleDB.ExecListCmd(str);
        }



        private void ValidateData(ProductSearchData data)
        {
            if (data.LOID == 0)
            {
                if (data.CODE.Trim() == "")
                    throw new ApplicationException("กรุณาระบุรหัสวัตถุดิบ");
                else if (data.PRODUCTTYPE == 0)
                    throw new ApplicationException("กรุณาเลือกประเภทวัตถุดิบ");
                else if (data.PRODUCTGROUP == 0)
                    throw new ApplicationException("กรุณาเลือกกลุ่มวัตถุดิบ");
                else if (data.NAME.Trim() == "")
                    throw new ApplicationException("กรุณาระบุชื่อวัตถุดิบ");
                else if (data.ABBNAME.Trim() == "")
                    throw new ApplicationException("กรุณาระบุชื่อย่อวัตถุดิบ");
                else if (data.BARCODE.Trim() == "")
                    throw new ApplicationException("กรุณาระบุบาร์โค้ด");
                else if (data.UNIT == 0)
                    throw new ApplicationException("กรุณาเลือกหน่วยวัตถุดิบ");
                else if (data.COST == 0)
                    throw new ApplicationException("กรุณาระบุราคาทุน");
                else if (data.ORDERTYPE.Trim() == "")
                    throw new ApplicationException("กรุณาเลือกวิธีการนำเข้า");
                else if ((data.ORDERTYPE.Trim() == "PO" || data.ORDERTYPE.Trim() == "AR") && data.LEADTIME == 0)
                    throw new ApplicationException("กรุณาระบุระยะเวลาการสั่งซื้อ");
                else if ((data.ORDERTYPE.Trim() == "PD" || data.ORDERTYPE.Trim() == "AR") && data.LEADTIMEPD == 0)
                    throw new ApplicationException("กรุณาระบุระยะเวลาการผลิต");
                else if ((data.ORDERTYPE.Trim() == "PO" || data.ORDERTYPE.Trim() == "AR") && data.LOTSIZE == 0)
                    throw new ApplicationException("กรุณาระบุจำนวนการสั่งซื้อ");
                else if ((data.ORDERTYPE.Trim() == "PD" || data.ORDERTYPE.Trim() == "AR") && data.LOTSIZEPD == 0)
                    throw new ApplicationException("กรุณาระบุจำนวนการผลิต");
                else if (data.PACKSIZE == 0)
                    throw new ApplicationException("กรุณาระบุขนาดบรรจุ");
                else if (data.PACKSIZEUNIT == 0)
                    throw new ApplicationException("กรุณาเลือกหน่วยขนาดบรรจุ");
                else if (MasterDALObj.CheckCode(data.LOID, data.CODE.Trim()) == false)
                    throw new ApplicationException("รหัสวัตถุดิบนี้มีแล้ว");
                else if (MasterDALObj.CheckName(data.LOID, data.NAME.Trim()) == false)
                    throw new ApplicationException("ชื่อวัตถุดิบนี้มีแล้ว");
                if (data.ENAME != "")
                {
                    if (MasterDALObj.CheckEName(data.LOID, data.ENAME.Trim()) == false)
                        throw new ApplicationException("ชื่อวัตถุดิบภาษาอังกฤษนี้มีแล้ว");
                }
                if (BarcodeDALObj.CheckBarcode(data.LOID, data.BARCODE.Trim(), data.PBLOID) == false)
                    throw new ApplicationException("บาร์โค้ด " + data.BARCODE + " นี้มีแล้ว");
                else if (BarcodeDALObj.CheckAbbname(data.LOID, data.ABBNAME.Trim(), data.PBLOID) == false)
                    throw new ApplicationException("ชื่อย่อ " + data.ABBNAME + " นี้มีแล้ว");
                else if (BarcodeDALObj.CheckUnit(data.LOID, data.UNIT.ToString(), data.PBLOID) == false)
                    throw new ApplicationException("หน่วยนับนี้มีแล้ว");
            }
        }


        public ProductSearchData GetData(double loid)
        {
            ProductSearchData data = new ProductSearchData();
            if (MasterDALObj.GetDataByLOID(loid, null))
            {
                string sqlstr = MasterDALObj.LOID.ToString() + " AND ISDEFAULT = 'Y'";
                BarcodeDALObj.GetDataByProductMaster(sqlstr, null);
                data.LOID = MasterDALObj.LOID;
                data.PBLOID = BarcodeDALObj.LOID;
                data.CODE = MasterDALObj.CODE;
                data.ACTIVE = MasterDALObj.ACTIVE;
                data.BARCODE = BarcodeDALObj.BARCODE;
                data.NAME = MasterDALObj.NAME;
                data.ENAME = MasterDALObj.ENAME;
                data.ABBNAME = BarcodeDALObj.ABBNAME;
                data.PRODUCTGROUP = MasterDALObj.PRODUCTGROUP;
                ABB.DAL.ProductGroupDAL groupDAL = new ABB.DAL.ProductGroupDAL();
                groupDAL.GetDataByLOID(MasterDALObj.PRODUCTGROUP, null);
                data.PRODUCTTYPE = groupDAL.PRODUCTTYPE;
                data.COST = BarcodeDALObj.COST;
                data.LEADTIME = MasterDALObj.LEADTIME;
                data.LOTSIZE = MasterDALObj.LOTSIZE;
                data.LEADTIMEPD = MasterDALObj.LEADTIMEPD;
                data.LOTSIZEPD = MasterDALObj.LOTSIZEPD;
                data.ORDERTYPE = MasterDALObj.ORDERTYPE;
                data.PRICE = BarcodeDALObj.PRICE;
                data.REGISNO = MasterDALObj.REGISNO;
                data.STDPRICE = BarcodeDALObj.STDPRICE;
                data.UNIT = MasterDALObj.UNIT;
                data.PACKSIZE = BarcodeDALObj.PACKSIZE;
                data.PACKSIZEUNIT = BarcodeDALObj.PACKSIZEUNIT;
                //data.AGE = MasterDALObj.AGE;
            }
            return data;
        }
        public String GetUnitName(string loid)
        {
            string str = "SELECT NAME ";
            str += "FROM UNIT ";
            str += "WHERE LOID = '" + loid + "' ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt.Rows[0][0].ToString();
        }
        public bool InsertData(string userID, ProductSearchData data)
        {
            bool ret = true;
            ValidateData(data);
            DALObj.NAME = data.NAME.Trim();
         //   DALObj.ABBNAME = data.ABBNAME.Trim();
            DALObj.PRODUCTGROUP = data.PRODUCTGROUP;
            DALObj.ACTIVE = data.ACTIVE.Trim();
            DALObj.BARCODE = data.BARCODE.Trim();
            DALObj.CODE = data.CODE.Trim();
            DALObj.COST = data.COST;
         //   DALObj.ISDISCOUNT = data.ISDISCOUNT.Trim();
         //   DALObj.ISEDIT = data.ISEDIT.Trim();
         //   DALObj.ISVAT = data.ISVAT.Trim();
            DALObj.LEADTIME = data.LEADTIME;
            DALObj.LOTSIZE = data.LOTSIZE;
            DALObj.LEADTIMEPD = data.LEADTIMEPD;
            DALObj.LOTSIZEPD = data.LOTSIZEPD;
            DALObj.ORDERTYPE = data.ORDERTYPE.Trim();
            DALObj.PRICE = data.PRICE;
            DALObj.REGISNO = data.REGISNO.Trim();
            DALObj.STDPRICE = data.STDPRICE;
            DALObj.UNIT = data.UNIT;
            DALObj.ABBNAME = data.ABBNAME;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
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
                throw new ApplicationException(ex.Message);
            }
            return ret;
        }

        public bool UpdateData(string userID, ProductSearchData data)
        {
            bool ret = true;
            ValidateData(data);

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                MasterDALObj.OnDB = false;
                MasterDALObj.GetDataByLOID(data.LOID, obj.zTrans);
                BarcodeDALObj.GetDataByProductMaster(data.LOID.ToString(), obj.zTrans);
                MasterDALObj.NAME = data.NAME.Trim();
                MasterDALObj.ENAME = data.ENAME.Trim();
                MasterDALObj.CODE = data.CODE.Trim();
                BarcodeDALObj.ABBNAME = data.ABBNAME.Trim();
                MasterDALObj.PRODUCTGROUP = data.PRODUCTGROUP;
                MasterDALObj.ACTIVE = data.ACTIVE.Trim();
                BarcodeDALObj.ACTIVE = data.ACTIVE.Trim();
                BarcodeDALObj.BARCODE = data.BARCODE.Trim();
                BarcodeDALObj.COST = data.COST;
                BarcodeDALObj.ISDISCOUNT = "0";
                BarcodeDALObj.ISEDIT = "Y";
                BarcodeDALObj.ISVAT = "0";
                MasterDALObj.LEADTIME = data.LEADTIME;
                MasterDALObj.LOTSIZE = data.LOTSIZE;
                MasterDALObj.LEADTIMEPD = data.LEADTIMEPD;
                MasterDALObj.LOTSIZEPD = data.LOTSIZEPD;
                MasterDALObj.ORDERTYPE = data.ORDERTYPE.Trim();
                BarcodeDALObj.PRICE = data.PRICE;
                MasterDALObj.REGISNO = "";
                BarcodeDALObj.STDPRICE = data.STDPRICE;
                MasterDALObj.UNIT = data.UNIT;
                BarcodeDALObj.UNIT = data.UNIT;
                BarcodeDALObj.PACKSIZE = data.PACKSIZE;
                BarcodeDALObj.PACKSIZEUNIT = data.PACKSIZEUNIT;
                BarcodeDALObj.ISREFUND = "N";
                MasterDALObj.AGE = 0;
                BarcodeDALObj.ISDEFAULT = "Y";
                BarcodeDALObj.MULTIPLY = 1;
                BarcodeDALObj.ACTIVE = "1";

                if (MasterDALObj.OnDB)
                {
                    ret = MasterDALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(MasterDALObj.ErrorMessage);
                    }
                    BarcodeDALObj.PRODUCTMASTER = MasterDALObj.LOID;
                    ret = BarcodeDALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(BarcodeDALObj.ErrorMessage);
                    }
                }
                else
                {
                    ret = MasterDALObj.InsertCurrentData(userID, obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(MasterDALObj.ErrorMessage);
                    }
                    BarcodeDALObj.PRODUCTMASTER = MasterDALObj.LOID;
                    ret = BarcodeDALObj.InsertCurrentData(userID, obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(BarcodeDALObj.ErrorMessage);
                    }
                }

                _LOID = MasterDALObj.LOID;

                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                ret = false;
                throw new ApplicationException(ex.Message);
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
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj2.PRODUCT = Convert.ToDouble(arrData[i]);
                    ret = DALObj2.DeleteCurrentData(obj.zTrans);

                    MasterDALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    BarcodeDALObj.DeleteDataByProductMaster(Convert.ToDouble(arrData[i]), obj.zTrans);

                    ret = MasterDALObj.DeleteCurrentData(obj.zTrans);
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
                throw new ApplicationException(ex.Message);
            }
            return ret;
        }

        public DataTable GetItem(double ProductMaster)
        {
            string str = "SELECT PD.LOID, PD.BARCODE, PD.ABBNAME, PD.MULTIPLY, U.NAME UNITMASTER, PD.UNIT, ";
            str += "PD.COST, PD.PRICE, PD.STDPRICE, PD.PACKSIZE , PD.PACKSIZEUNIT UNITPACK, PD.ACTIVE ";
            str += "FROM PRODUCT PD ";
            str += "INNER JOIN UNIT U ON PD.UNITMASTER = U.LOID ";
            str += "LEFT JOIN UNIT U3 ON PD.PACKSIZEUNIT = U3.LOID ";
            str += "WHERE PD.PRODUCTMASTER = '" + ProductMaster + "'";
            str += "ORDER BY PD.MULTIPLY ";

            return OracleDB.ExecListCmd(str);
        }

        public bool UpdateItemData(string userID, ProductSearchData data)
        {
            bool ret = true;
            
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ProductBarcodeDAL itemDAL = new ProductBarcodeDAL();
                ProductBarcodeDAL itemDAL2 = new ProductBarcodeDAL();
                ProductBarcodeDAL itemDAL3 = new ProductBarcodeDAL();
                //itemDAL.DeleteDataByProductMasterExceptDefault(data.LOID, obj.zTrans);
                itemDAL.UpdateActiveByProductMasterExceptDefault(data.LOID, "0", userID, obj.zTrans);
                for (Int16 i = 1; i < data.ITEM.Count; ++i)
                {
                    ProductBarcodeData item = (ProductBarcodeData)data.ITEM[i];
                    itemDAL.OnDB = false;
                    itemDAL.ACTIVE = "";
                    itemDAL.GetDataByBARCODE(item.BARCODE, obj.zTrans);
                    if (itemDAL.ACTIVE != Constz.ActiveStatus.Active)
                    {
                        itemDAL2.GetDataByABBNAME(item.ABBNAME.Trim(), obj.zTrans);
                        if (itemDAL2.ACTIVE == Constz.ActiveStatus.Active)
                        {
                            throw new ApplicationException("ชื่อย่อ " + item.ABBNAME + " นี้มีแล้ว");
                        }
                        itemDAL3.GetDataByABBUNIT(item.LOID,item.UNIT, obj.zTrans);
                        if (itemDAL3.ACTIVE == Constz.ActiveStatus.Active)
                        {
                            throw new ApplicationException("ชื่อหน่วยซ้ำ");
                        }

                        itemDAL.BARCODE = item.BARCODE;
                        itemDAL.PRODUCTMASTER = data.LOID;
                        itemDAL.ABBNAME = item.ABBNAME;
                        itemDAL.UNIT = item.UNIT;
                        itemDAL.COST = item.COST;
                        itemDAL.PRICE = item.PRICE;
                        itemDAL.STDPRICE = item.STDPRICE;
                        itemDAL.ISDISCOUNT = "0";
                        itemDAL.ISVAT = "1";
                        itemDAL.PACKSIZE = item.PACKSIZE;
                        itemDAL.PACKSIZEUNIT = item.UNITPACK;
                        itemDAL.ISEDIT = "Y";
                        itemDAL.ISREFUND = "N";
                        itemDAL.ACTIVE = Convert.ToString(item.ACTIVE);
                        itemDAL.MULTIPLY = item.MULTIPLY;
                        itemDAL.ISDEFAULT = "N";

                        if (itemDAL.OnDB)
                            ret = itemDAL.UpdateCurrentData(userID, obj.zTrans);
                        else
                            ret = itemDAL.InsertCurrentData(userID, obj.zTrans);

                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
                    }
                    else
                    {
                        throw new ApplicationException("บาร์โค้ด " + item.BARCODE + " นี้มีแล้ว");
                    }
                }
                _LOID = data.LOID;
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
