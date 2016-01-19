using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.Common.StockIn
{
    public class StockInQCProductDetailData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        private string _NAME = "";
        private double _QCQTY= 0;
        private string _UNITNAME = "";
        private string _QCCODE = "";
        private double _QTY = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public double QCQTY
        {
            get { return _QCQTY; }
            set { _QCQTY= value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
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
    }
}
