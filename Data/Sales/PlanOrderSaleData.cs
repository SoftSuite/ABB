using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class PlanOrderSaleData
    {
        private double _LOID = 0;
        private double _SALEMAN = 0;
        private string _SALENAME = "";
        private double _QTY = 0;
        private int _MONTH = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double SALEMAN
        {
            get { return _SALEMAN; }
            set { _SALEMAN = value; }
        }

        public string SALENAME
        {
            get { return _SALENAME; }
            set { _SALENAME = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public int MONTH
        {
            get { return _MONTH; }
            set { _MONTH = value; }
        }

    }
}