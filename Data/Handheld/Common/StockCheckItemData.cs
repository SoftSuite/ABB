using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.Common
{
    public class StockCheckItemData : ABB.Data.Common.CommonData
    {
        private double _LOID = 0;
        private double _STOCKCHECK = 0;
        private double _PRODUCT = 0;
        private string _LOTNO = "";
        private double _COUNTQTY = 0;
        private double _LOCATION = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double STOCKCHECK
        {
            get { return _STOCKCHECK; }
            set { _STOCKCHECK = value; }
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

        public double COUNTQTY
        {
            get { return _COUNTQTY; }
            set { _COUNTQTY = value; }
        }

        public double LOCATION
        {
            get { return _LOCATION; }
            set { _LOCATION = value; }
        }

    }
}
