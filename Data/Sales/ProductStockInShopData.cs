using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ProductStockInShopData
    {
        int _ORDERNO = 0;
        string _PDCODE = "";
        string _PDNAME = "";
        double _RQ_QTY = 0;
        double _RECEIVE_QTY = 0;
        string _UNITNAME = "";
        double _PRICE = 0;
        double _TOTAL = 0;
        double _SILOID = 0;

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public string PDCODE
        {
            get { return _PDCODE; }
            set { _PDCODE = value; }
        }

        public string PDNAME
        {
            get { return _PDNAME; }
            set { _PDNAME = value; }
        }

        public double RQ_QTY
        {
            get { return _RQ_QTY; }
            set { _RQ_QTY = value; }
        }

        public double RECEIVE_QTY
        {
            get { return _RECEIVE_QTY; }
            set { _RECEIVE_QTY = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }

        public double SILOID
        {
            get { return _SILOID; }
            set { _SILOID = value; }
        }
    }
}
