using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class PopupStockinReturnSearchData
    {
        private double _DOCTYPE = 0;
        private double _CUSTOMER = 0;
        private double _REFLOID = 0;

        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }
    }
}
