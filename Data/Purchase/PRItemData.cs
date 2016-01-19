using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Purchase
{
    public class PRItemData
    {
        private double _LOID = 0;
        private double _PRODUCT = 0;
        private string _BARCODE = "";
        private string _PRODUCTNAME = "";
        private double _QTY = 0;
        private double _UNIT = 0;
        private string _UNITNAME = "";
        private double _MINSTOCK = 0;
        private double _MAXSTOCK = 0;
        private double _STOCK = 0;
        private double _OLDPRICE = 0;
        private double _CURPRICE = 0;
        private double _MINPRICE = 0;
        private double _LAST3MON = 0;
        private double _LASTYEAR = 0;
        private DateTime _DUEDATE = new DateTime(1, 1, 1);
        private string _STATUS = "";
        private string _ACTIVE = "";
        private string _URGENT = "";
        private string _ISMATERIAL = "";

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

        public double MINSTOCK
        {
            get { return _MINSTOCK; }
            set { _MINSTOCK = value; }
        }

        public double MAXSTOCK
        {
            get { return _MAXSTOCK; }
            set { _MAXSTOCK = value; }
        }

        public double STOCK
        {
            get { return _STOCK; }
            set { _STOCK = value; }
        }

        public double OLDPRICE
        {
            get { return _OLDPRICE; }
            set { _OLDPRICE = value; }
        }

        public double CURPRICE
        {
            get { return _CURPRICE; }
            set { _CURPRICE = value; }
        }

        public double MINPRICE
        {
            get { return _MINPRICE; }
            set { _MINPRICE = value; }
        }

        public double LAST3MON
        {
            get { return _LAST3MON; }
            set { _LAST3MON = value; }
        }

        public double LASTYEAR
        {
            get { return _LASTYEAR; }
            set { _LASTYEAR = value; }
        }

        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string URGENT
        {
            get { return _URGENT; }
            set { _URGENT = value; }
        }

        public string ISMATERIAL
        {
            get { return _ISMATERIAL; }
            set { _ISMATERIAL = value; }
        }
    }
}
