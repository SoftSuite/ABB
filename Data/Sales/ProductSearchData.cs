using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Sales
{
    public class ProductSearchData
    {
        private double _LOID = 0;
        private double _PBLOID = 0;
        private string _CODE = "";
        private string _BARCODE = "";
        private string _NAME = "";
        private string _ENAME = "";
        private string _ABBNAME = "";
        private double _PRODUCTGROUP = 0;
        private double _PRODUCTTYPE = 0;
        private double _UNIT = 0;
        private string _UNITNAME = "";
        private double _PDQTY = 0;
        private double _COST = 0;
        private double _PRICE = 0;
        private double _STDPRICE = 0;
        private string _ISDISCOUNT = "";
        private string _ISVAT = "";
        private string _ORDERTYPE = "";
        private double _LOTSIZE = 0;
        private double _LEADTIME = 0;
        private double _LOTSIZEPD = 0;
        private double _LEADTIMEPD = 0;
        private string _ACTIVE = "";
        private string _REGISNO = "";
        private string _ISEDIT = "";
        private int _ORDERNO = 0;
        private string _REMARK = "";
        private double _AGE = 1;
        private string _ISREFUND = "";
        private double _PACKSIZE = 0;
        private double _PACKSIZEUNIT = 0;
        private ArrayList _ITEM = new ArrayList();
        private double _PRODUCEGROUP = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public double PBLOID
        {
            get { return _PBLOID; }
            set { _PBLOID = value; }
        }
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public string ENAME
        {
            get { return _ENAME; }
            set { _ENAME = value; }
        }
        public string ABBNAME
        {
            get { return _ABBNAME; }
            set { _ABBNAME = value; }
        }
        public double PRODUCTGROUP
        {
            get { return _PRODUCTGROUP; }
            set { _PRODUCTGROUP = value; }
        }
        public double PRODUCTTYPE
        {
            get { return _PRODUCTTYPE; }
            set { _PRODUCTTYPE = value; }
        }
        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }
        public double COST
        {
            get { return _COST; }
            set { _COST = value; }
        }
        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }
        public double STDPRICE
        {
            get { return _STDPRICE; }
            set { _STDPRICE = value; }
        }
        public string ISDISCOUNT
        {
            get { return _ISDISCOUNT; }
            set { _ISDISCOUNT = value; }
        }
        public string ISVAT
        {
            get { return _ISVAT; }
            set { _ISVAT = value; }
        }
        public string ORDERTYPE
        {
            get { return _ORDERTYPE; }
            set { _ORDERTYPE = value; }
        }
        public double LOTSIZE
        {
            get { return _LOTSIZE; }
            set { _LOTSIZE = value; }
        }
        public double LEADTIME
        {
            get { return _LEADTIME; }
            set { _LEADTIME = value; }
        }
        public double LOTSIZEPD
        {
            get { return _LOTSIZEPD; }
            set { _LOTSIZEPD = value; }
        }
        public double LEADTIMEPD
        {
            get { return _LEADTIMEPD; }
            set { _LEADTIMEPD = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public string REGISNO
        {
            get { return _REGISNO; }
            set { _REGISNO = value; }
        }
        public string ISEDIT
        {
            get { return _ISEDIT; }
            set { _ISEDIT = value; }
        }

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public double AGE
        {
            get { return _AGE; }
            set { _AGE = value; }
        }

        public string ISREFUND
        {
            get { return _ISREFUND; }
            set { _ISREFUND = value; }
        }
        public double PACKSIZE
        {
            get { return _PACKSIZE; }
            set { _PACKSIZE = value; }
        }
        public double PACKSIZEUNIT
        {
            get { return _PACKSIZEUNIT; }
            set { _PACKSIZEUNIT = value; }
        }
        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }
        public double PRODUCEGROUP
        {
            get { return _PRODUCEGROUP; }
            set { _PRODUCEGROUP = value; }
        }
    }
}
