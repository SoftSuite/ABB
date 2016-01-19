using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ReturnTesterItemData
    {
        private int _ORDERNO = 0;
        private string _BARCODE = "";
        private string _NAME = "";
        private double _QTY = 0;
        private double _PRICE = 0;
        private string _UNITNAME = "";
        private double _PRODUCT = 0;
        private double _UNIT = 0;

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
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

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
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

    }
}
