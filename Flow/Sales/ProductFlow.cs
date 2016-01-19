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
    public class ProductFlow
    {
        private string _error = "";
        double _LOID = 0;
        private ProductDAL _searchDAL;
        private VProductReturnrequest _dAL;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        private ProductDAL SearchDAL
        {
            get { if (_searchDAL == null) { _searchDAL = new ProductDAL(); } return _searchDAL; }
        }
        private VProductReturnrequest DAL
        {
            get { if (_dAL == null) { _dAL = new VProductReturnrequest(); } return _dAL; }
        }

        public static ArrayList GetSearchProduct(string PType, string PGroup, string Barcode, string PName, string WHLoid)
        {
            string str = "";
            ArrayList arrResult = new ArrayList();

            str = " SELECT PL.BARCODE,PL.PNAME ";
            str += " FROM  V_PRODUCT_LIST_REQUISITION PL";
            str += " WHERE PTLOID = '" + PType + "'";
            str += " AND WAREHOUSE = " + WHLoid + "";
            str += " AND PGLOID = " + PGroup;

            if (Barcode != "")
            {
                str += " AND BARCODE  = '" + Barcode + "'";
            }

            if (PName != "")
            {
                str += " AND UPPER(PNAME) LIKE UPPER('%" + PName + "%')";
            }

            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(str);
                arrResult.Clear();
                int i = 1;
                while (zRd.Read())
                {
                    V_Product_List_RequisitionData irData = new V_Product_List_RequisitionData();
                    irData.ORDERNO = i;
                    irData.BARCODE = zRd["BARCODE"].ToString();
                    irData.PNAME = zRd["PNAME"].ToString();
                    arrResult.Add(irData);
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return arrResult;
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
            return SearchDAL.GetProductList(sWhere);
        }

        public ProductSearchData GetData(double loid)
        {
            ProductSearchData data = new ProductSearchData();
            if (DALObj.GetDataByLOID(loid, null))
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
                data.LEADTIMEPD = DALObj.LEADTIMEPD;
                data.LOTSIZEPD = DALObj.LOTSIZEPD;
                data.ORDERTYPE = DALObj.ORDERTYPE;
                data.PRICE = DALObj.PRICE;
                data.REGISNO = DALObj.REGISNO;
                data.STDPRICE = DALObj.STDPRICE;
                data.UNIT = DALObj.UNIT;
                data.ISREFUND = DALObj.ISREFUND;
                data.PACKSIZE = DALObj.PACKSIZE;
                data.PACKSIZEUNIT = DALObj.PACKSIZEUNIT;
                data.REMARK = DALObj.REMARK;
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
                data.LEADTIMEPD = DALObj.LEADTIMEPD;
                data.LOTSIZE = DALObj.LOTSIZE;
                data.ORDERTYPE = DALObj.ORDERTYPE;
                data.PRICE = DALObj.PRICE;
                data.REGISNO = DALObj.REGISNO;
                data.STDPRICE = DALObj.STDPRICE;
                data.UNIT = DALObj.UNIT;
            }
            return data;
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
    }
}
