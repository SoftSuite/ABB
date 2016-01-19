using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Inventory.FG
{
    public class StockoutBasketData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private DateTime _CHECKDATE = new DateTime(1, 1, 1);
        private double _PRODUCT = 0;
        private double _QTY = 0;
        private string _STATUS = "";
        private DateTime _STOCKINDATE = new DateTime(1, 1, 1);
        private string _REMARK = "";
        private string _TYPE = "";
        private double _UNIT = 0;
        private string _LOTNO = "";
        private double _WAREHOUSE = 0;
        private ArrayList _ITEM = new ArrayList();

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

        public DateTime CHECKDATE
        {
            get { return _CHECKDATE; }
            set { _CHECKDATE = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public DateTime STOCKINDATE
        {
            get { return _STOCKINDATE; }
            set { _STOCKINDATE = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }
    }
}
