using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class PlanDetailData : PlanPopupData
    {
        private int _MONTH = 0;
        private int _YEAR = 0;
        private double _PRODUCT = 0;
        private string _PRODUCTNAME = "";
        private string _UNITNAME = "";
        private string _STATUS = "";
        private double _MINIMUM = 0;
        private double _MAXIMUM = 0;
        private double _PDLOTSIZE = 0;
        private double _PDLEADTIME = 0;
        private double _POLOTSIZE = 0;
        private double _POLEADTIME = 0;

        public int YEAR
        {
            get { return _YEAR; }
            set { _YEAR = value; }
        }

        public int MONTH
        {
            get { return _MONTH; }
            set { _MONTH = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
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

        public double MINIMUM
        {
            get { return _MINIMUM; }
            set { _MINIMUM = value; }
        }

        public double MAXIMUM
        {
            get { return _MAXIMUM; }
            set { _MAXIMUM = value; }
        }

        public double POLOTSIZE
        {
            get { return _POLOTSIZE; }
            set { _POLOTSIZE = value; }
        }

        public double PDLOTSIZE
        {
            get { return _PDLOTSIZE; }
            set { _PDLOTSIZE = value; }
        }

        public double POLEADTIME
        {
            get { return _POLEADTIME; }
            set { _POLEADTIME = value; }
        }

        public double PDLEADTIME
        {
            get { return _PDLEADTIME; }
            set { _PDLEADTIME = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

    }
}
