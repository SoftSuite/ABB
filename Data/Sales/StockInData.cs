using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class StockInData : Common.CommonData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _QCCODE = "";
        private string _ACCCODE = "";
        private double _DOCTYPE = 0;
        private double _SENDER = 0;
        private double _RECEIVER = 0;
        private DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        private double _APPROVER = 0;
        private DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        private string _INVNO = "";
        private string _STATUS = "";
        private DateTime _QCDATE = new DateTime(1, 1, 1);
        private string _REASON = "";
        private string _REMARK = "";
        private double _GRANDTOT = 0;
        private string _QCRESULT = "";
        private string _CADDRESS = "";

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

        public string QCCODE
        {
            get { return _QCCODE; }
            set { _QCCODE = value; }
        }

        public string ACCCODE
        {
            get { return _ACCCODE; }
            set { _ACCCODE = value; }
        }

        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }

        public double SENDER
        {
            get { return _SENDER; }
            set { _SENDER = value; }
        }

        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
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

        public string INVNO
        {
            get { return _INVNO; }
            set { _INVNO = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public DateTime QCDATE
        {
            get { return _QCDATE; }
            set { _QCDATE = value; }
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

        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
        }

        public string QCRESULT
        {
            get { return _QCRESULT; }
            set { _QCRESULT = value; }
        }

        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }

    }
}
