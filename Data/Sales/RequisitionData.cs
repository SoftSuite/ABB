using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class RequisitionData
    {
        private string _CODE = "";
        private string _ACTIVE = "";
        private string _STATUS = "";
        private double _TOTAL = 0;
        private double _REQUISITIONTYPE = 0;
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private double _WAREHOUSE = 0;
        private double _CUSTOMER = 0;
        private double _LOID = 0;
        private DateTime _RESERVEDATE = new DateTime(1, 1, 1);
        private string _REMARK = "";
        private string _REASON = "";

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }

        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
        }

        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public DateTime RESERVEDATE
        {
            get { return _RESERVEDATE; }
            set { _RESERVEDATE = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }
    }
}
