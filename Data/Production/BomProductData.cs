using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class BomProductData
    {
        private string _BARCODE = "";
        private double _LOID = 0;
        private double _PRODUCTGROUP = 0;
        private double _PRODUCTTYPE = 0;
        private double _LOTSIZE = 0;
        private string _PRODCUTTYPENAME = "";
        private double _UNIT = 0;
        private string _UNITNAME = "";

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
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

        public double LOTSIZE
        {
            get { return _LOTSIZE; }
            set { _LOTSIZE = value; }
        }

        public string PRODUCTTYPENAME
        {
            get { return _PRODCUTTYPENAME; }
            set { _PRODCUTTYPENAME = value; }
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
    }
}
