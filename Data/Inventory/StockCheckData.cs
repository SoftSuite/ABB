using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory
{
    public class StockCheckData
    {
        private DateTime _CHECKDATE = new DateTime(1, 1, 1);
        private string _BATCHNO = "";
        private double _WAREHOUSE = 0;

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

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
    }
}
