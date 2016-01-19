using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.Common
{
    public class StockCheckBatchData
    {
        private double _STOCKCHECK = 0;
        private DateTime _CHECKDATE = new DateTime(1, 1, 1);
        private string _BATCHNO = "";
        private string _WAREHOUSENAME = "";

        public double STOCKCHECK
        {
            get { return _STOCKCHECK; }
            set { _STOCKCHECK = value; }
        }

        public DateTime CHECKDATE
        {
            get { return _CHECKDATE; }
            set { _CHECKDATE = value; }
        }

        public string BATCHNO
        {
            get { return _BATCHNO; }
            set { _BATCHNO = value; }
        }

        public string WAREHOUSENAME
        {
            get { return _WAREHOUSENAME; }
            set { _WAREHOUSENAME = value; }
        }

    }
}
