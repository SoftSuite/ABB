using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Purchase
{
    public class PurchaseRequestData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private DateTime _REQUSETDATE = new DateTime(1, 1, 1);
        private string _ORDERTYPE = "";
        private double _PURCHASETYPE = 0;
        private double _REQUESTBY = 0;
        private double _DIVISION = 0;
        private string _APPROVER = "";
        private DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        private string _APPROVE = "";
        private string _ACTIVE = "";
        private string _STATUS = "";
        private string _REQUIREMENT = "";
        private string _REASON = "";
        private string _REMARK = "";
        private string _FROMCOMPANY = "";
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

        public DateTime REQUSETDATE
        {
            get { return _REQUSETDATE; }
            set { _REQUSETDATE = value; }
        }

        public string ORDERTYPE
        {
            get { return _ORDERTYPE; }
            set { _ORDERTYPE = value; }
        }

        public double PURCHASETYPE
        {
            get { return _PURCHASETYPE; }
            set { _PURCHASETYPE = value; }
        }

        public double REQUESTBY
        {
            get { return _REQUESTBY; }
            set { _REQUESTBY = value; }
        }

        public double DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }

        public string APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }

        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }

        public string APPROVE
        {
            get { return _APPROVE; }
            set { _APPROVE = value; }
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

        public string REQUIREMENT
        {
            get { return _REQUIREMENT; }
            set { _REQUIREMENT = value; }
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

        public string FROMCOMPANY
        {
            get { return _FROMCOMPANY; }
            set { _FROMCOMPANY = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }
    }
}
