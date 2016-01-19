using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class BasketItemData
    {
        private double _LOID = 0;
        private double _PRODUCT = 0;
        private string _BARCODE = "";
        private string _PDNAME = "";
        private string _LOTNO = "";
        private double _QTY = 0;
        private double _SYSQTY = 0;
        private double _UNIT = 0;
        private string _UNITNAME = "";
        private double _PACKAGE = 0;
        private double _PRODUCTSTOCK = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public string PDNAME
        {
            get { return _PDNAME; }
            set { _PDNAME = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public double SYSQTY
        {
            get { return _SYSQTY; }
            set { _SYSQTY = value; }
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

        public double PACKAGE
        {
            get { return _PACKAGE; }
            set { _PACKAGE = value; }
        }

        public double PRODUCTSTOCK
        {
            get { return _PRODUCTSTOCK; }
            set { _PRODUCTSTOCK = value; }
        }
    }
}
