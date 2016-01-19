using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class PointOfSaleCustomerData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _CUSTOMERNAME = "";
        private double _DISCOUNT = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }

        public double DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }
    }
}
