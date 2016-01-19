using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class StockInItemData
    {
        double _LOID = 0;
        double _STOCKIN = 0;
        double _QTY = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _PRODUCT = 0;
        string _LOTNO = "";
        string _STATUS = "";
        double _REFLOID = 0;
        string _REFTABLE = "";
        string _ACTIVE = "";
        double _PRICE = 0;
        string _QCRESULT = "";
        string _QCREMARK= "";

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

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public DateTime CREATEON
        {
            get { return _CREATEON; }
            set { _CREATEON = value; }
        }

        public string UPDATEBY
        {
            get { return _UPDATEBY; }
            set { _UPDATEBY = value; }
        }

        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
            set { _UPDATEON = value; }
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

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        public string QCREMARK
        {
            get { return _QCREMARK; }
            set { _QCREMARK = value; }
        }

        public string QCRESULT
        {
            get { return _QCRESULT; }
            set { _QCRESULT = value; }
        }

    }
}

