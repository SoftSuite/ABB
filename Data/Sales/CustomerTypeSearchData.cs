using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class CustomerTypeSearchData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _NAME = "";
        private string _DESCRIPTION = "";
        private double _DISCOUNT = 0;
        private double _LOWERPRICE = 0;
        private string _ACTIVE = "";

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

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }

        public double DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }

        public double LOWERPRICE
        {
            get { return _LOWERPRICE; }
            set { _LOWERPRICE = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
    }
}
