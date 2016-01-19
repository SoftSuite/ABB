using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.WH
{
    public class StockInProductReceiveData
    {
        private double _STOCKINITEM = 0;
        private string _PRODUCTNAME = "";
        private string _CODE = "";
        private string _SUPPLIERNAME = "";
        private string _INVCODE = "";
        private string _ORDERCODE = "";
        private double _QCQTY = 0;
        private string _UNITNAME = "";
        private DateTime _ORDERDATE = new DateTime(1, 1, 1);

        public double STOCKINITEM
        {
            get { return _STOCKINITEM; }
            set { _STOCKINITEM = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string SUPPLIERNAME
        {
            get { return _SUPPLIERNAME; }
            set { _SUPPLIERNAME = value; }
        }

        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }

        public string ORDERCODE
        {
            get { return _ORDERCODE; }
            set { _ORDERCODE = value; }
        }

        public double QCQTY
        {
            get { return _QCQTY; }
            set { _QCQTY = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }

        public DateTime ORDERDATE
        {
            get { return _ORDERDATE; }
            set { _ORDERDATE = value; }
        }

    }
}
