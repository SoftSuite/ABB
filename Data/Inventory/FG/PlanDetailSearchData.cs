using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class PlanDetailSearchData
    {
        private double _PLAN = 0;
        private double _PRODUCTTYPE = 0;
        private double _PRODUCTGROUP = 0;
        private string _PRODUCTNAME = "";
        private string _PRODUCTSTATUS = "";
        private double _MONTH = 0;

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

        public string PRODUCTSTATUS
        {
            get { return _PRODUCTSTATUS; }
            set { _PRODUCTSTATUS = value; }
        }

        public double MONTH
        {
            get { return _MONTH; }
            set { _MONTH = value; }
        }
    }
}
