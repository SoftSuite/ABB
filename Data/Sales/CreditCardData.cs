using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class CreditCardData
    {
        private double _LOID = 0;
        private string _NAME = "";
        private double _CHARGE = 0;
        private string _ACTIVE = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
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

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
    }
}
