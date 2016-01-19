using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
namespace ABB.Data.Inventory.FG
{
    public class StockOutItemFGData : ABB.Data.StockOutItemData
    {
        private double _STOCKOUT = 0;
        private double _DISCOUNT = 0;
        private double _NETPRICE = 0;

        public double STOCKOUT
        {
            get { return _STOCKOUT; }
            set { _STOCKOUT = value; }
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

    }
}
