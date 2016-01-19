using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.Common.StockIn
{
    public class StockInPOData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _INVNO = "";
        private string _SUPPLIERNAME = "";
        private double _SENDER = 0;
        private string _STATUS = "";
        private double _PDORDER = 0;
        private string _ORDERCODE = "";

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

        public string INVNO
        {
            get { return _INVNO; }
            set { _INVNO = value; }
        }

        public string SUPPLIERNAME
        {
            get { return _SUPPLIERNAME; }
            set { _SUPPLIERNAME = value; }
        }

        public double SENDER
        {
            get { return _SENDER; }
            set { _SENDER = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public double PDORDER
        {
            get { return _PDORDER; }
            set { _PDORDER = value; }
        }

        public string ORDERCODE
        {
            get { return _ORDERCODE; }
            set { _ORDERCODE = value; }
        }

    }
}
