using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.Common.StockIn
{
    public class StockInProductDetailData
    {
        private double _STOCKIN = 0;
        private string _CODE = "";
        private string _LOTNO = "";
        private DateTime _MFGDATE = new DateTime(1, 1, 1);
        private DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        private string _PRODUCTNAME = "";
        private double _QTY = 0;
        private string _UNITNAME = "";

        public double STOCKIN
        {
            get { return _STOCKIN; }
            set { _STOCKIN = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public DateTime MFGDATE
        {
            get { return _MFGDATE; }
            set { _MFGDATE = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }

        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }

    }
}
