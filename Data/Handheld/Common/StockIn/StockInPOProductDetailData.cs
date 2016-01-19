using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.Common.StockIn
{
    public class StockInPOProductDetailData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _ORDERCODE = "";
        private DateTime _ORDERDATE = new DateTime(1, 1, 1);
        private string _NAME = "";
        private double _ORDERQTY = 0;
        private string _UNITNAME = "";
        private string _INVNO = "";
        private string _SUPPLIERNAME = "";
        private double _QCQTY = 0;
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

        public string ORDERCODE
        {
            get { return _ORDERCODE; }
            set { _ORDERCODE = value; }
        }

        public DateTime ORDERDATE
        {
            get { return _ORDERDATE; }
            set { _ORDERDATE = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public double ORDERQTY
        {
            get { return _ORDERQTY; }
            set { _ORDERQTY = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
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

        public double QCQTY
        {
            get { return _QCQTY; }
            set { _QCQTY = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }
    }
}
