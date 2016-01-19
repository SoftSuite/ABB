using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory
{
    public class StockCheckSearchData
    {
        private string _BATCHNO = "";
        private double _WAREHOUSE = 0;
        private double _LOCATION = 0;
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private string _BARCODE = "";
        private string _PRODUCTNAME = "";
        private string _LOTNO = "";
        private bool _DIFFCHECK = true;

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

        public double LOCATION
        {
            get { return _LOCATION; }
            set { _LOCATION = value; }
        }

        public DateTime DATEFROM
        {
            get { return _DATEFROM; }
            set { _DATEFROM = value; }
        }

        public DateTime DATETO
        {
            get { return _DATETO; }
            set { _DATETO = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
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

        public bool DIFFCHECK
        {
            get { return _DIFFCHECK; }
            set { _DIFFCHECK = value; }
        }
    }
}
