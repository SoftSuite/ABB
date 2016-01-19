using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ReturnTesterData : Data.Common.CommonData
    {
        private double _LOID = 0;
        private string _REASON = "";
        private string _CODE = "";
        private string _REMARK = "";
        private double _RECEIVER = 0;
        private double _SENDER = 0;
        private ArrayList _STOCKITEM = new ArrayList();
        private string _STATUS = "";
        private DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        private double _APPROVER = 0;
        private double _DOCTYPE = 0;
        private string _ACTIVE = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public ArrayList STOCKITEM
        {
            get { return _STOCKITEM; }
            set { _STOCKITEM = value; }
        }

        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
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

        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }

        public double APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }

        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

    }
}
