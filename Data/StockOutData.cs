using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class StockOutData : Common.CommonData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private double _APPROVER = 0;
        private DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        private string _ACTIVE = "";
        private string _REFTABLE = "";
        private double _REFLOID = 0;
        private double _RECEIVER = 0;
        private double _DOCTYPE = 0;
        private string _STATUS = "";
        private string _INVCODE = "";
        private string _REASON = "";
        private string _REMARK = "";
        private double _SENDER = 0;
        private double _CTITLE = 0;
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private string _CADDRESS = "";
        private string _CTEL = "";
        private string _CFAX = "";
        private DateTime _DELIVERYDATE = new DateTime(1, 1, 1);
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private string _PRODUCTREF = "";
        private double _PRODUCTLOID = 0;

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

        public double APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }

        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }

        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
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

        public double CTITLE
        {
            get { return _CTITLE; }
            set { _CTITLE = value; }
        }

        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }

        public string CLASTNAME
        {
            get { return _CLASTNAME; }
            set { _CLASTNAME = value; }
        }

        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }

        public string CTEL
        {
            get { return _CTEL; }
            set { _CTEL = value; }
        }

        public string CFAX
        {
            get { return _CFAX; }
            set { _CFAX = value; }
        }

        public DateTime DELIVERYDATE
        {
            get { return _DELIVERYDATE; }
            set { _DELIVERYDATE = value; }
        }

        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }

        public string PRODUCTREF
        {
            get { return _PRODUCTREF; }
            set { _PRODUCTREF = value; }
        }

        public double PRODUCTLOID
        {
            get { return _PRODUCTLOID; }
            set { _PRODUCTLOID = value; }
        }

    }
}
