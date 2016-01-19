using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class ProductStockAdjustData
    {
        private double _LOID = 0;
        private double _QTY = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }
    }
}
