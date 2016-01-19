using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class RequisitionItemData : ABB.Data.Common.CommonData
    {
        private double _LOID = 0;
        private double _REQUISITION = 0;
        private double _PRODUCT = 0;
        private double _QTY = 0;
        private double _UNIT = 0;
        private double _DISCOUNT = 0;
        private double _NETPRICE = 0;
        private string _ACTIVE = "";
        private DateTime _DUEDATE = new DateTime(1, 1, 1);
        private double _PRICE = 0;
        private string _ISVAT = "";
        private double _REFLOID = 0;
        private string _REFTABLE = "";

        private string _BarCode = "";
        private string _UnitName = "";
        private string _PRODUCTNAME = "";
        private double _PDQTY = 0;
        private string _LOTNO = "";
        private string _INVNO = "";
        private double _STOCKQTY = 0;

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

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
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

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        public string ISVAT
        {
            get { return _ISVAT; }
            set { _ISVAT = value; }
        }

        public string BarCode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }

        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }

        public string ProductName
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
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

        public string INVNO
        {
            get { return _INVNO; }
            set { _INVNO = value; }
        }

        public double STOCKQTY
        {
            get { return _STOCKQTY; }
            set { _STOCKQTY = value; }
        }
    }
}
