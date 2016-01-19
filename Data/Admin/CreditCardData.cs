using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Admin
{
    public class CreditCardData
    {
        double _LOID = 0;
        string _CODE = "";
        string _NAME = "";
        double _CHARGE = 0;

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

        public double CHARGE
        {
            get { return _CHARGE; }
            set { _CHARGE = value; }
        }
    }
}
