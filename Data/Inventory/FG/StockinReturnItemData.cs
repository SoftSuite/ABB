using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class StockinReturnItemData
    {
        private double _LOID = 0;
        private double _PRODUCT = 0;
        private string _BARCODE = "";
        private string _PDNAME = "";
        private string _LOTNO = "";
        private double _QTY = 0;
        private double _OLDQTY = 0;
        private double _QTYLOST = 0;
        private double _UNIT = 0;
        private string _UNITNAME = "";
        private double _PRICE = 0;
        private double _NETPRICE = 0;
        private double _REFLOID = 0;
        private string _REFTABLE = "";
        private string _STATUS = "";

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

        public double OLDQTY
        {
            get { return _OLDQTY; }
            set { _OLDQTY = value; }
        }

        public double QTYLOST
        {
            get { return _QTYLOST; }
            set { _QTYLOST = value; }
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

        public double NETPRICE
        {
            get { return _NETPRICE; }
            set { _NETPRICE = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
    }
}
