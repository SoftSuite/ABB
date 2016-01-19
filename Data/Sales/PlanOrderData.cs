using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class PlanOrderData
    {
        private double _PLAN = 0;
        private string _STATUS = "";
        private string _PRODUCTNAME = "";
        private string _UNITNAME = "";
        private string _MONTHNAME = "";

        public double PLAN
        {
            get { return _PLAN; }
            set { _PLAN = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }

        public string MONTHNAME
        {
            get { return _MONTHNAME; }
            set { _MONTHNAME = value; }
        }
    }
}
