using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.WH
{
    public class StockOutWHItemData
    {
        private double _NO = 0;
        private double _REFLOID = 0;
        private string _BARCODE = "";
        private double _PRODUCT = 0;
        private string _LOTNO = "";
        private double _QTY = 0;
        private double _UNIT = 0;
        private double _PRICE = 0;
        private string _UNITNAME = "";
        private double _REQUISITION = 0;
        private double _REMAIN = 0;
        private string _PRODUCTNAME = "";

        public double NO
        {
            get { return _NO; }
            set { _NO = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
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

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
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

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }

        public double REMAIN
        {
            get { return _REMAIN; }
            set { _REMAIN = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

    }
}
