using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.Common.StockIn
{
    public class StockInQCData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _QCCODE = "";
        private DateTime _RECEIVEDATE = new DateTime(1,1,1);
        private string _STATUS = "";

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

        public string QCCODE
        {
            get { return _QCCODE; }
            set { _QCCODE = value; }
        }

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

    }
}
