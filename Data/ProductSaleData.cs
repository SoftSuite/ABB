using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class ProductSaleData
    {
        private double _PRODUCT = 0;
        private string _PRODUCTNAME = "";
        private double _UNIT = 0;
        private string _UNITNAME = "";
        private double _UNITPRICE = 0;
        private double _DISCOUNT = 0;
        private string _ISVAT = "";
        private string _BARCODE = "";
        private double _STOCKQTY = 0;
        private string _ISDISCOUNT = "";
        private string _ISEDIT = "";

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
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

        public double UNITPRICE
        {
            get { return _UNITPRICE; }
            set { _UNITPRICE = value; }
        }

        public double DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }

        public string ISVAT
        {
            get { return _ISVAT; }
            set { _ISVAT = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public double STOCKQTY
        {
            get { return _STOCKQTY; }
            set { _STOCKQTY = value; }
        }

        public string ISDISCOUNT
        {
            get { return _ISDISCOUNT; }
            set { _ISDISCOUNT = value; }
        }

        public string ISEDIT
        {
            get { return _ISEDIT; }
            set { _ISEDIT = value; }
        }

    }
}
