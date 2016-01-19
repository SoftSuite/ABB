using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class PointOfSaleRefItemData
    {
        private double _REFREQUISITIONITEM = 0;
        private double _PRODUCT = 0;
        private string _BARCODE = "";
        private double _UNIT = 0;
        private string _NAME = "";
        private double _QTY = 0;
        private string _UNITNAME = "";
        private double _PRICE = 0;
        private double _DISCOUNT = 0;
        private string _ISVAT = "";
        private string _ISDISCOUNT = "";
        private string _ISEDIT = "";

        public double REFREQUISITIONITEM
        {
            get { return _REFREQUISITIONITEM; }
            set { _REFREQUISITIONITEM = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
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
