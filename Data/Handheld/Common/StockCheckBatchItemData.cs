using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.Common
{
    public class StockCheckBatchItemData : ABB.Data.Handheld.Common.StockCheckBatchData
    {
        private double _LOCATION = 0;
        private string _LOCATIONNAME = "";
        private string _PRODUCTNAME = "";
        private string _LOTNO = "";
        private double _COUNTQTY = 0;
        private string _UNITNAME = "";

        public double LOCATION
        {
            get { return _LOCATION; }
            set { _LOCATION = value; }
        }

        public string LOCATIONNAME
        {
            get { return _LOCATIONNAME; }
            set { _LOCATIONNAME = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public double COUNTQTY
        {
            get { return _COUNTQTY; }
            set { _COUNTQTY = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }
    }
}
