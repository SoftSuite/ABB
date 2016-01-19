using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class BillData : Common.CommonData
    {
        private double _REQUISITION = 0;
        private string _CODE = "";
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private string _REFCODE = "";
        private string _CUSTOMERNAME = "";
        private double _TOTAL = 0;
        private double _TOTDIS = 0;
        private double _VAT = 0;
        private double _TOTVAT = 0;
        private double _GRANDTOT = 0;
        string _CCODE = "";
        string _CNAME = "";
        string _CADDRESS = "";
        string _CTEL = "";
        string _CFAX = "";
        string _PAYMENT = "";
        string _CHEQUE = "";
        DateTime _CHEQUEDATE = new DateTime(1, 1, 1);
        string _BANKNAME = "";
        string _BANKBRANCH = "";
        string _RECEIVEBY = "";
        string _INVCODE = "";
        string _CREDITCARDID = "";
        DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        private ArrayList _ITEM = new ArrayList();

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }
        public string REFCODE
        {
            get { return _REFCODE; }
            set { _REFCODE = value; }
        }
        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }
        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }
        public double TOTDIS
        {
            get { return _TOTDIS; }
            set { _TOTDIS = value; }
        }
        public double VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }
        public double TOTVAT
        {
            get { return _TOTVAT; }
            set { _TOTVAT = value; }
        }
        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
        }
        public string CCODE
        {
            get { return _CCODE; }
            set { _CCODE = value; }
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
        public string PAYMENT
        {
            get { return _PAYMENT; }
            set { _PAYMENT = value; }
        }
        public string CHEQUE
        {
            get { return _CHEQUE; }
            set { _CHEQUE = value; }
        }
        public string CREDITCARDID
        {
            get { return _CREDITCARDID; }
            set { _CREDITCARDID = value; }
        }
        public DateTime CHEQUEDATE
        {
            get { return _CHEQUEDATE; }
            set { _CHEQUEDATE = value; }
        }
        public string BANKNAME
        {
            get { return _BANKNAME; }
            set { _BANKNAME = value; }
        }
        public string BANKBRANCH
        {
            get { return _BANKBRANCH; }
            set { _BANKBRANCH = value; }
        }
        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }
        public string RECEIVEBY
        {
            get { return _RECEIVEBY; }
            set { _RECEIVEBY = value; }
        }
        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }
        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }
    }
}
