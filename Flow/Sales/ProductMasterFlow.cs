using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.DAL.Sales;
using ABB.Data.Sales;

namespace ABB.Flow.Sales
{
    public class ProductMasterFlow
    {
        private string _error = "";
        double _LOID = 0;
        private VProductReturnrequest _dAL;
        private ProductMasterDAL _masterdal;
        private ProductBarcodeDAL _barcodedal;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public ProductMasterDAL MasterDALObj
        {
            get { if (_masterdal == null) { _masterdal = new ProductMasterDAL(); } return _masterdal; }
        }
        public ProductBarcodeDAL BarcodeDALObj
        {
            get { if (_barcodedal == null) { _barcodedal = new ProductBarcodeDAL(); } return _barcodedal; }
        }
        private VProductReturnrequest DAL
        {
            get { if (_dAL == null) { _dAL = new VProductReturnrequest(); } return _dAL; }
        }

        ProductDAL _dal;
        VProductReturnrequest _VPDdal;
        public ProductDAL DALObj
        {
            get { if (_dal == null) { _dal = new ProductDAL(); } return _dal; }
        }
        public VProductReturnrequest VPDDALObj
        {
            get { if (_VPDdal == null) { _VPDdal = new VProductReturnrequest(); } return _VPDdal; }
        }

        public DataTable GetDataList()
        {
            return GetDataList("");
        }

        public DataTable GetDataList(string sWhere)
        {
            string str = "SELECT PD.LOID, PD.PRODUCTMASTER, PD.BARCODE, PD.NAME, U.NAME UNIT, PG.NAME PRODUCTGROUPNAME, ";
            str += "PT.NAME PRODUCTTYPENAME, PD.COST, PD.PRICE, PT.TYPE, PG.PRODUCTTYPE, PD.PRODUCTGROUP, PD.ISDEFAULT ";
            str += "FROM PRODUCT PD ";
            str += "INNER JOIN PRODUCTGROUP PG ON PD.PRODUCTGROUP = PG.LOID ";
            str += "INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID ";
            str += "INNER JOIN UNIT U ON PD.UNIT = U.LOID ";
            str += sWhere + " ";
            str += "ORDER BY PD.NAME";

            return OracleDB.ExecListCmd(str);
        }


        private void ValidateData(ProductSearchData data)
        {
            if (data.LOID == 0)
            {
                if (data.PRODUCTTYPE == 0)
                    throw new ApplicationException("กรุณาเลือกประเภทสินค้า");
                else if (data.PRODUCTGROUP == 0)
                    throw new ApplicationException("กรุณาเลือกกลุ่มสินค้า");
                else if (data.CODE.Trim() == "")
                    throw new ApplicationException("กรุณาระบุรหัสสินค้า");
                else if (data.NAME.Trim() == "")
                    throw new ApplicationException("กรุณาระบุชื่อสินค้า");
                else if (data.ABBNAME.Trim() == "")
                    throw new ApplicationException("กรุณาระบุชื่อย่อสินค้า");
                else if (data.BARCODE.Trim() == "")
                    throw new ApplicationException("กรุณาระบุบาร์โค้ด");
                else if (data.UNIT == 0)
                    throw new ApplicationException("กรุณาเลือกหน่วยสินค้า");
                else if (data.COST == 0)
                    throw new ApplicationException("กรุณาระบุราคาทุน");
                else if (data.PRICE == 0)
                    throw new ApplicationException("กรุณาระบุราคาขาย");
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
            }

        }

        private void ValidateData2(ProductSearchData data)
        {
            if (MasterDALObj.CheckName(data.LOID, data.NAME.Trim()) == false)
                throw new ApplicationException("ชื่อสินค้าภาษาไทยนี้มีแล้ว");
            if (MasterDALObj.CheckCode(data.LOID, data.CODE.Trim()) == false)
                throw new ApplicationException("รหัสสินค้านี้มีแล้ว");
            if (data.ENAME != "")
            {
                if (MasterDALObj.CheckEName(data.LOID, data.ENAME.Trim()) == false)
                    throw new ApplicationException("ชื่อสินค้าภาษาอังกฤษนี้มีแล้ว");
            }
            if (BarcodeDALObj.CheckBarcode(data.LOID, data.BARCODE.Trim(), data.PBLOID) == false)
                throw new ApplicationException("บาร์โค้ดนี้มีแล้ว");
            if (BarcodeDALObj.CheckAbbname(data.LOID, data.ABBNAME.Trim(), data.PBLOID) == false)
                throw new ApplicationException("ชื่อย่อนี้มีแล้ว");
            if (BarcodeDALObj.CheckUnit(data.LOID, data.UNIT.ToString(), data.PBLOID) == false)
                throw new ApplicationException("หน่วยนับนี้มีแล้ว");
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
                data.ISDISCOUNT = BarcodeDALObj.ISDISCOUNT;
                data.ISEDIT = BarcodeDALObj.ISEDIT;
                data.ISVAT = BarcodeDALObj.ISVAT;
                data.LEADTIME = MasterDALObj.LEADTIME;
                data.LOTSIZE = MasterDALObj.LOTSIZE;
                data.LEADTIMEPD = MasterDALObj.LEADTIMEPD;
                data.LOTSIZEPD = MasterDALObj.LOTSIZEPD;
                data.ORDERTYPE = MasterDALObj.ORDERTYPE;
                data.PRICE = BarcodeDALObj.PRICE;
                data.REGISNO = MasterDALObj.REGISNO;
                data.STDPRICE = BarcodeDALObj.STDPRICE;
                data.UNIT = MasterDALObj.UNIT;
                data.ISREFUND = BarcodeDALObj.ISREFUND;
                data.PACKSIZE = BarcodeDALObj.PACKSIZE;
                data.PACKSIZEUNIT = BarcodeDALObj.PACKSIZEUNIT;
                data.REMARK = MasterDALObj.REMARK;
                data.AGE = MasterDALObj.AGE;
                data.PRODUCEGROUP = MasterDALObj.PRODUCEGROUP;
            }
            return data;
        }
        public ProductSearchData GetProductBarcode(double loid)
        {
            ProductSearchData data = new ProductSearchData();
            if (BarcodeDALObj.GetDataByLOID(loid, null))
            {
                data.LOID = BarcodeDALObj.LOID;
                //data.CODE = BarcodeDALObj.CODE;
                //data.ACTIVE = BarcodeDALObj.ACTIVE;
                data.BARCODE = BarcodeDALObj.BARCODE;
                //data.NAME = BarcodeDALObj.NAME;
                //data.ENAME = BarcodeDALObj.ENAME;
                //data.ABBNAME = BarcodeDALObj.ABBNAME;
                //data.PRODUCTGROUP = BarcodeDALObj.PRODUCTGROUP;
                //ABB.DAL.ProductGroupDAL groupDAL = new ABB.DAL.ProductGroupDAL();
                //groupDAL.GetDataByLOID(MasterDALObj.PRODUCTGROUP, null);
                //data.PRODUCTTYPE = groupDAL.PRODUCTTYPE;
                data.COST = BarcodeDALObj.COST;
                //data.ISDISCOUNT = BarcodeDALObj.ISDISCOUNT;
                //data.ISEDIT = BarcodeDALObj.ISEDIT;
                //data.ISVAT = BarcodeDALObj.ISVAT;
                //data.LEADTIME = BarcodeDALObj.LEADTIME;
                //data.LOTSIZE = BarcodeDALObj.LOTSIZE;
                //data.LEADTIMEPD = BarcodeDALObj.LEADTIMEPD;
                //data.LOTSIZEPD = BarcodeDALObj.LOTSIZEPD;
                //data.ORDERTYPE = BarcodeDALObj.ORDERTYPE;
                data.PRICE = BarcodeDALObj.PRICE;
                //data.REGISNO = BarcodeDALObj.REGISNO;
                //data.STDPRICE = BarcodeDALObj.STDPRICE;
                data.UNIT = BarcodeDALObj.UNIT;
                //data.ISREFUND = BarcodeDALObj.ISREFUND;
                //data.PACKSIZE = BarcodeDALObj.PACKSIZE;
                //data.PACKSIZEUNIT = BarcodeDALObj.PACKSIZEUNIT;
                //data.REMARK = BarcodeDALObj.REMARK;
                //data.AGE = BarcodeDALObj.AGE;
            }
            return data;
        }
        public ProductSearchData GetDataVProduct(double loid)
        {
            ProductSearchData data = new ProductSearchData();
            if (VPDDALObj.GetDataByLOID(loid, null))
            {
                data.LOID = VPDDALObj.LOID;
                data.ACTIVE = VPDDALObj.ACTIVE;
                data.BARCODE = VPDDALObj.BARCODE;
                data.CODE = VPDDALObj.CODE;
                data.NAME = VPDDALObj.NAME;
                data.COST = VPDDALObj.COST;
                data.PDQTY = VPDDALObj.QTY;
                data.PRICE = VPDDALObj.PRICE;
                data.UNIT = VPDDALObj.UNIT;
                data.UNITNAME = VPDDALObj.UNAME;
            }
            return data;
        }
        public ProductSearchData GetData(string barCode)
        {
            ProductSearchData data = new ProductSearchData();
            if (DALObj.GetDataByBarCode(barCode, null))
            {
                data.LOID = DALObj.LOID;
                data.ACTIVE = DALObj.ACTIVE;
                data.BARCODE = DALObj.BARCODE;
                data.CODE = DALObj.CODE;
                data.NAME = DALObj.NAME;
                data.ABBNAME = DALObj.ABBNAME;
                data.PRODUCTGROUP = DALObj.PRODUCTGROUP;
                ABB.DAL.ProductGroupDAL groupDAL = new ABB.DAL.ProductGroupDAL();
                groupDAL.GetDataByLOID(DALObj.PRODUCTGROUP, null);
                data.PRODUCTTYPE = groupDAL.PRODUCTTYPE;
                data.COST = DALObj.COST;
                data.ISDISCOUNT = DALObj.ISDISCOUNT;
                data.ISEDIT = DALObj.ISEDIT;
                data.ISVAT = DALObj.ISVAT;
                data.LEADTIME = DALObj.LEADTIME;
                data.LOTSIZE = DALObj.LOTSIZE;
                data.ORDERTYPE = DALObj.ORDERTYPE;
                data.PRICE = DALObj.PRICE;
                data.REGISNO = DALObj.REGISNO;
                data.STDPRICE = DALObj.STDPRICE;
                data.UNIT = DALObj.UNIT;
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
       public ProductSearchData GetDataVProduct(string barCode)
        {
            ProductSearchData data = new ProductSearchData();
            if (VPDDALObj.GetDataByBarCode(barCode, null))
            {
                data.LOID = VPDDALObj.LOID;
                data.ACTIVE = VPDDALObj.ACTIVE;
                data.BARCODE = VPDDALObj.BARCODE;
                data.CODE = VPDDALObj.CODE;
                data.NAME = VPDDALObj.NAME;
                data.COST = VPDDALObj.COST;
                data.PDQTY = VPDDALObj.QTY;
                data.PRICE = VPDDALObj.PRICE;
                data.UNIT = VPDDALObj.UNIT;
                data.UNITNAME = VPDDALObj.UNAME;
            }
            return data;
        }
        public bool InsertData(string userID, ProductSearchData data)
        {
            bool ret = true;
            ValidateData(data);
            ValidateData2(data);
            MasterDALObj.NAME = data.NAME.Trim();
            MasterDALObj.ENAME = data.ENAME.Trim();
            MasterDALObj.CODE = data.CODE.Trim();
            BarcodeDALObj.ABBNAME = data.ABBNAME.Trim();
            MasterDALObj.PRODUCTGROUP = data.PRODUCTGROUP;
            MasterDALObj.ACTIVE = data.ACTIVE.Trim();
            BarcodeDALObj.ACTIVE = data.ACTIVE.Trim();
            BarcodeDALObj.BARCODE = data.BARCODE.Trim();
            BarcodeDALObj.COST = data.COST;
            BarcodeDALObj.ISDISCOUNT = data.ISDISCOUNT.Trim();
            BarcodeDALObj.ISEDIT = data.ISEDIT.Trim();
            BarcodeDALObj.ISVAT = data.ISVAT.Trim();
            MasterDALObj.LEADTIME = data.LEADTIME;
            MasterDALObj.LOTSIZE = data.LOTSIZE;
            MasterDALObj.LEADTIMEPD = data.LEADTIMEPD;
            MasterDALObj.LOTSIZEPD = data.LOTSIZEPD;
            MasterDALObj.ORDERTYPE = data.ORDERTYPE.Trim();
            BarcodeDALObj.PRICE = data.PRICE;
            MasterDALObj.REGISNO = data.REGISNO.Trim();
            BarcodeDALObj.STDPRICE = data.PRICE;
            MasterDALObj.UNIT = data.UNIT;
            BarcodeDALObj.UNIT = data.UNIT;
            MasterDALObj.REMARK = data.REMARK;
            BarcodeDALObj.PACKSIZE = data.PACKSIZE;
            BarcodeDALObj.PACKSIZEUNIT = data.PACKSIZEUNIT;
            BarcodeDALObj.ISREFUND = data.ISREFUND;
            MasterDALObj.AGE = data.AGE;
            BarcodeDALObj.ISDEFAULT = "Y";
            BarcodeDALObj.MULTIPLY = 1;
            MasterDALObj.PRODUCEGROUP = data.PRODUCEGROUP;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ret = MasterDALObj.InsertCurrentData(userID, obj.zTrans);
                _LOID = MasterDALObj.LOID;
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
            ValidateData2(data);
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
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
                BarcodeDALObj.ISDISCOUNT = data.ISDISCOUNT.Trim();
                BarcodeDALObj.ISEDIT = data.ISEDIT.Trim();
                BarcodeDALObj.ISVAT = data.ISVAT.Trim();
                MasterDALObj.LEADTIME = data.LEADTIME;
                MasterDALObj.LOTSIZE = data.LOTSIZE;
                MasterDALObj.LEADTIMEPD = data.LEADTIMEPD;
                MasterDALObj.LOTSIZEPD = data.LOTSIZEPD;
                MasterDALObj.ORDERTYPE = data.ORDERTYPE.Trim();
                BarcodeDALObj.PRICE = data.PRICE;
                MasterDALObj.REGISNO = data.REGISNO.Trim();
                BarcodeDALObj.STDPRICE = data.PRICE;
                MasterDALObj.UNIT = data.UNIT;
                BarcodeDALObj.UNIT = data.UNIT;
                MasterDALObj.REMARK = data.REMARK;
                BarcodeDALObj.PACKSIZE = data.PACKSIZE;
                BarcodeDALObj.PACKSIZEUNIT = data.PACKSIZEUNIT;
                BarcodeDALObj.ISREFUND = data.ISREFUND;
                MasterDALObj.AGE = data.AGE;
                BarcodeDALObj.ISDEFAULT = "Y";
                BarcodeDALObj.MULTIPLY = 1;
                BarcodeDALObj.ACTIVE = "1";
                MasterDALObj.PRODUCEGROUP = data.PRODUCEGROUP;

                ret = MasterDALObj.UpdateCurrentData(userID, obj.zTrans);
                if (!ret)
                {
                    throw new ApplicationException(MasterDALObj.ErrorMessage);
                }

                ret = BarcodeDALObj.UpdateCurrentData(userID, obj.zTrans);
                if (!ret)
                {
                    throw new ApplicationException(BarcodeDALObj.ErrorMessage);
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
                    MasterDALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    BarcodeDALObj.DeleteDataByProductMaster(Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = MasterDALObj.DeleteCurrentData(obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(MasterDALObj.ErrorMessage);
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
                throw new ApplicationException(ex.Message);
            }
            return ret;
        }

        public DataTable GetItem(double ProductMaster)
        {
            string str = "SELECT PD.LOID, PD.BARCODE, PD.ABBNAME, PD.MULTIPLY, U.NAME UNITMASTER, PD.UNIT, ";
            str += "PD.COST, PD.PRICE, PD.ISVAT, PD.ISDISCOUNT, PD.PACKSIZE , PD.PACKSIZEUNIT UNITPACK,PD.ACTIVE ";
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
                //itemDAL.DeleteDataByProductMasterExceptDefault(data.LOID, obj.zTrans);
                itemDAL.UpdateActiveByProductMasterExceptDefault(data.LOID, "0", userID, obj.zTrans);
                for (Int16 i = 1; i < data.ITEM.Count; ++i)
                {
                    ProductBarcodeData item = (ProductBarcodeData)data.ITEM[i];
                    itemDAL.OnDB = false;
                    itemDAL.GetDataByBARCODE(item.BARCODE, obj.zTrans);

                    itemDAL.BARCODE = item.BARCODE;
                    itemDAL.PRODUCTMASTER = data.LOID;
                    itemDAL.ABBNAME = item.ABBNAME;
                    itemDAL.UNIT = item.UNIT;
                    itemDAL.COST = item.COST;
                    itemDAL.PRICE = item.PRICE;
                    itemDAL.STDPRICE = item.PRICE;
                    itemDAL.ISDISCOUNT = item.ISDISCOUNT;
                    itemDAL.ISVAT = item.ISVAT;
                    itemDAL.PACKSIZE = item.PACKSIZE;
                    itemDAL.PACKSIZEUNIT = data.PACKSIZEUNIT;
                    itemDAL.ISEDIT = "N";
                    itemDAL.ISREFUND = "Y";
                    itemDAL.ACTIVE = Convert.ToString(item.ACTIVE);
                    itemDAL.MULTIPLY = item.MULTIPLY;
                    itemDAL.ISDEFAULT = "N";

                    if (itemDAL.OnDB)
                        ret = itemDAL.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = itemDAL.InsertCurrentData(userID, obj.zTrans);

                    

                    if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
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
