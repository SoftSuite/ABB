using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ProductBarcodeData
    {
        private double _LOID = 0;
        private string _BARCODE = "";
        private string _ABBNAME = "";
        private double _MULTIPLY = 0;
        private double _ACTIVE = 0;
        private string _UNITMASTER = "";
        private double _UNIT = 0;
        private double _COST = 0;
        private double _PRICE = 0;
        private double _STDPRICE = 0;
        private string _ISVAT = "";
        private string _ISDISCOUNT = "";
        private double _PACKSIZE = 0;
        private double _UNITPACK = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }
        public string ABBNAME
        {
            get { return _ABBNAME; }
            set { _ABBNAME = value; }
        }
        public double MULTIPLY
        {
            get { return _MULTIPLY; }
            set { _MULTIPLY = value; }
        }
        public double ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public string UNITMASTER
        {
            get { return _UNITMASTER; }
            set { _UNITMASTER = value; }
        }
        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
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
        public string ISVAT
        {
            get { return _ISVAT; }
            set { _ISVAT = value; }
        }
        public string ISDISCOUNT
        {
            get { return _ISDISCOUNT; }
            set { _ISDISCOUNT = value; }
        }
        public double PACKSIZE
        {
            get { return _PACKSIZE; }
            set { _PACKSIZE = value; }
        }
        public double UNITPACK
        {
            get { return _UNITPACK; }
            set { _UNITPACK = value; }
        }
    }
}
