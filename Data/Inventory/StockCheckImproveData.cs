using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory
{
    public class StockCheckImproveData
    {
        private double _STOCKCHECK = 0;
        private double _PRODUCTSTOCK = 0;
        private double _SYSQTY = 0;
        private double _IMPROVEQTY = 0;
        private string _REASON = "";

        public double STOCKCHECK
        {
            get { return _STOCKCHECK; }
            set { _STOCKCHECK = value; }
        }

        public double PRODUCTSTOCK
        {
            get { return _PRODUCTSTOCK; }
            set { _PRODUCTSTOCK = value; }
        }

        public double SYSQTY
        {
            get { return _SYSQTY; }
            set { _SYSQTY = value; }
        }

        public double IMPROVEQTY
        {
            get { return _IMPROVEQTY; }
            set { _IMPROVEQTY = value; }
        }

        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }
    }
}
