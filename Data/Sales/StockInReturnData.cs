using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ABB.Data.Sales
{
    public class StockInReturnData
    {
        private double _LOID = 0;
        private string _CREATEBY = "";
        private string _CODE = "";
        private double _TOTAL = 0;
        private DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        private DateTime _INVOICEDATE = new DateTime(1, 1, 1);
        private double _RECEIVER = 0;
        private string _REASON = "";
        private string _REMARK = "";
        private double _SENDER = 0;
        private string _STATUS = "";
        private double _REFLOID = 0;
        private string _INVOICECODE = "";
        private string _CUSTOMERCODE = "";
        private string _CUSTOMERNAME = "";
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

        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }

        public DateTime INVOICEDATE
        {
            get { return _INVOICEDATE; }
            set { _INVOICEDATE = value; }
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

        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
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

        public string INVOICECODE
        {
            get { return _INVOICECODE; }
            set { _INVOICECODE = value; }
        }

        public string CUSTOMERCODE
        {
            get { return _CUSTOMERCODE; }
            set { _CUSTOMERCODE = value; }
        }

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }
    }
}
