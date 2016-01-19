using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class PDOrderData
    {
        DateTime _DUEDATE = new DateTime(1, 1, 1);
        double _VAT = 0;
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        DateTime _ORDERDATE = new DateTime(1, 1, 1);
        string _ORDERTYPE = "";
        double _SUPPLIER = 0;
        string _CNAME = "";
        string _CADDRESS = "";
        string _CTEL = "";
        string _CFAX = "";
        string _APPROVER = "";
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _REMARK = "";
        string _PAYMENTTYPE = "";
        string _PAYMENTDESC = "";
        double _TOTAL = 0;
        double _TOTVAT = 0;
        double _TOTDIS = 0;
        double _GRANDTOT = 0;
        double _REFLOID = 0;
        string _REFTABLE = "";
        string _STATUS = "";
        string _ACTIVE = "";
        string _DELIVERY = "";
        string _OTHER = "";

        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }
        public double VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }
        public double LOID
        {
            get { return _LOID; }
        }
        public string CREATEBY
        {
            get { return _CREATEBY; }
        }
        public DateTime CREATEON
        {
            get { return _CREATEON; }
        }
        public string UPDATEBY
        {
            get { return _UPDATEBY; }
        }
        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
        }
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
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
    }
}
