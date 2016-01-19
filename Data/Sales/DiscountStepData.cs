using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class DiscountStepData
    {
        private double _LOID = 0;
        private double _LOWERPRICE = 0;
        private double _DISCOUNT = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double LOWERPRICE
        {
            get { return _LOWERPRICE; }
            set { _LOWERPRICE = value; }
        }

        public double DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }
    }
}
