using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class StockInItemData : ABB.Data.Common.CommonData
    {
        double _LOID = 0;
        double _STOCKIN = 0;
        double _PRODUCT = 0;
        string _LOTNO = "";
        double _QCQTY = 0;
        double _QTY = 0;
        double _PQTY = 0;
        string _STATUS = "";
        double _REFLOID = 0;
        string _REFTABLE = "";
        double _PRICE = 0;
        double _UNIT = 0;
        string _QCRESULT = "";
        string _QCREMARK = "";
        string _REMARK = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double STOCKIN
        {
            get { return _STOCKIN; }
            set { _STOCKIN = value; }
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

        public double PQTY
        {
            get { return _PQTY; }
            set { _PQTY = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }

        public string QCRESULT
        {
            get { return _QCRESULT; }
            set { _QCRESULT = value; }
        }

        public string QCREMARK
        {
            get { return _QCREMARK; }
            set { _QCREMARK = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

    }
}
