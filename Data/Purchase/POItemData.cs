using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Purchase
{
    public class POItemData
    {
        private double _LOID = 0;
        private double _PRODUCT = 0;
        private string _BARCODE = "";
        private string _PRODUCTNAME = "";
        private double _QTY = 0;
        private double _RECEIVEQTY = 0;
        private double _UNIT = 0;
        private string _UNITNAME = "";
        private double _PRICE = 0;
        private double _DISCOUNT = 0;
        private double _NETPRICE = 0;
        private DateTime _DUEDATE = new DateTime(1, 1, 1);
        private double _PRITEM = 0;
        private string _PRITEMCODE = "";
        private string _ACTIVE = "";
        private string _ISVAT = "";
        private double _PDORDER = 0;

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

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public double RECEIVEQTY
        {
            get { return _RECEIVEQTY; }
            set { _RECEIVEQTY = value; }
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

        public double DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }

        public double NETPRICE
        {
            get { return _NETPRICE; }
            set { _NETPRICE = value; }
        }

        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }

        public double PRITEM
        {
            get { return _PRITEM; }
            set { _PRITEM = value; }
        }

        public string PRITEMCODE
        {
            get { return _PRITEMCODE; }
            set { _PRITEMCODE = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string ISVAT
        {
            get { return _ISVAT; }
            set { _ISVAT = value; }
        }

        public double PDORDER
        {
            get { return _PDORDER; }
            set { _PDORDER = value; }
        }
    }
}
