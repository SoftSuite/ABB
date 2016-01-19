using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.Common
{
    public class ProductSearchData
    {
        private double _REFLOID = 0;
        private double _LOID = 0;
        private string _BARCODE = "";
        private string _NAME = "";
        private double _UNIT = 0;
        private string _UNITNAME = "";
        private double _QTY = 0;
        private double _PRICE = 0;

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

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
    }
}
