using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ProductTesterData
    {
        private double _PRODUCT = 0;
        private string _NAME = "";
        private double _UNIT = 0;
        private string _UNITNAME = "";
        private double _PRICE = 0;
        private string _BARCODE = "";

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
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

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }
    }
}
