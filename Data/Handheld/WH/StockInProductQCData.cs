using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.WH
{
    public class StockInProductQCData
    {
        private double _STOCKINITEM = 0;
        private string _PRODUCTNAME = "";
        private string _CODE = "";
        private string _QCCODE = "";
        private double _QTY = 0;
        private double _QCQTY = 0;
        private string _UNITNAME = "";
        private DateTime _RECEIVEDATE = new DateTime(1, 1, 1);

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

        public string QCCODE
        {
            get { return _QCCODE; }
            set { _QCCODE = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
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

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

    }
}
