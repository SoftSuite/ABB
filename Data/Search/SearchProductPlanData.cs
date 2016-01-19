using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class SearchProductPlanData
    {
        private double _PLAN = 0;
        private double _PRODUCTTYPE = 0;
        private double _PRODUCTGROUP = 0;
        private string _PRODUCTNAME = "";

        public double PLAN
        {
            get { return _PLAN; }
            set { _PLAN = value; }
        }

        public double PRODUCTTYPE
        {
            get { return _PRODUCTTYPE; }
            set { _PRODUCTTYPE = value; }
        }

        public double PRODUCTGROUP
        {
            get { return _PRODUCTGROUP; }
            set { _PRODUCTGROUP = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

    }
}
