using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Purchase
{
    public class PurchaseOrderData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _OLDCODE = "";
        private DateTime _ORDERDATE = new DateTime(1, 1, 1);
        private string _ORDERTYPE = "";
        private double _SUPPLIER = 0;
        private string _CNAME = "";
        private string _CADDRESS = "";
        private string _CTEL = "";
        private string _CFAX = "";
        private string _APPROVER = "";
        private DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        private string _REMARK = "";
        private string _PAYMENTTYPE = "";
        private string _PAYMENTDESC = "";
        private double _TOTAL = 0;
        private double _TOTVAT = 0;
        private double _TOTDIS = 0;
        private double _GRANDTOT = 0;
        private double _REFLOID = 0;
        private string _REFTABLE = "";
        private string _STATUS = "";
        private string _ACTIVE = "";
        private string _DELIVERY = "";
        private string _OTHER = "";
        private double _VAT = 0;
        private string _TYPE = "";
        private string _SENDPO = "";
        private string _SENDOTHER = "";
        private DateTime _SENDPODATE = new DateTime(1, 1, 1);
        private ArrayList _ITEM = new ArrayList();
        private string _REFSUPPCODE = "";

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

        public string OLDCODE
        {
            get { return _OLDCODE; }
            set { _OLDCODE = value; }
        }

        public DateTime ORDERDATE
        {
            get { return _ORDERDATE; }
            set { _ORDERDATE = value; }
        }

        public string ORDERTYPE
        {
            get { return _ORDERTYPE; }
            set { _ORDERTYPE = value; }
        }

        public double SUPPLIER
        {
            get { return _SUPPLIER; }
            set { _SUPPLIER = value; }
        }

        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
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

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public string PAYMENTTYPE
        {
            get { return _PAYMENTTYPE; }
            set { _PAYMENTTYPE = value; }
        }

        public string PAYMENTDESC
        {
            get { return _PAYMENTDESC; }
            set { _PAYMENTDESC = value; }
        }

        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }

        public double TOTVAT
        {
            get { return _TOTVAT; }
            set { _TOTVAT = value; }
        }

        public double TOTDIS
        {
            get { return _TOTDIS; }
            set { _TOTDIS = value; }
        }

        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
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

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string DELIVERY
        {
            get { return _DELIVERY; }
            set { _DELIVERY = value; }
        }

        public string OTHER
        {
            get { return _OTHER; }
            set { _OTHER = value; }
        } 

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }

        public double VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }

        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        public string SENDPO
        {
            get { return _SENDPO; }
            set { _SENDPO = value; }
        } 

        public string SENDOTHER
        {
            get { return _SENDOTHER; }
            set { _SENDOTHER = value; }
        }

        public DateTime SENDPODATE
        {
            get { return _SENDPODATE; }
            set { _SENDPODATE = value; }
        }

        public string REFSUPPCODE
        {
            get { return _REFSUPPCODE; }
            set { _REFSUPPCODE = value; }
        }
    }
}
