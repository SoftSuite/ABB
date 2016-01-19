using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ABB.Data.Sales
{
    public class StockInShopData
    {
        private double _LOID = 0;
        private string _CREATEBY = "";
        private string _CODE = "";
        private double _GRANDTOT = 0;
        private DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        private double _RECEIVER = 0;
        private string _REMARK = "";
        private double _SENDER = 0;
        private string _STATUS = "";
        private double _REFLOID = 0;
        private string _REQUISITIONCODE = "";
        private DataTable _ITEM = new DataTable();

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
        }

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
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

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public DataTable ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }

        public string REQUISITIONCODE
        {
            get { return _REQUISITIONCODE; }
            set { _REQUISITIONCODE = value; }
        }
    }
}
