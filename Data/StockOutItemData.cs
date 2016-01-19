using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class StockOutItemData
    {
        private double _LOID = 0;
        private double _STOCKOUT = 0;
        private double _PRODUCT = 0;
        private string _LOTNO = "";
        private double _QTY = 0;
        private string _ACTIVE = "";
        private string _REFTABLE = "";
        private double _REFLOID = 0;
        private string _STATUS = "";
        private double _PRICE = 0;
        private double _UNIT = 0;
        private string _INVNO = "";
        private double _REMAIN = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double STOCKOUT
        {
            get { return _STOCKOUT; }
            set { _STOCKOUT = value; }
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
        public double REMAIN
        {
            get { return _REMAIN; }
            set { _REMAIN = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        public string INVNO
        {
            get { return _INVNO; }
            set { _INVNO = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
    }
}
