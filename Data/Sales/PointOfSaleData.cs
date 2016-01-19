using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class PointOfSaleData : Data.Common.CommonData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _REFNO = "";
        private double _REFLOID = 0;
        private string _INVCODE = "";
        private double _CUSTOMER = 0;
        private double _WAREHOUSE = 0;
        private double _TOTAL = 0;
        private double _TOTDIS = 0;
        private double _GRANDTOT = 0;
        private string _REFTABLE = "";
        private string _STATUS = "";
        private string _ACTIVE = "";
        private double _CASH = 0;
        private double _CREDITCARDPAY = 0;
        private double _COUPON = 0;
        private string _CREDITCARDID = "";
        private double _CREDITTYPE = 0;
        private ArrayList _REQUISITIONITEM = new ArrayList();
        private double _VAT = 0;
        private double _TOTALVAT = 0;
        private string _USEMEMBERDISCOUNT = "";
        private DateTime _REQDATE = new DateTime(1, 1, 1);

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

        public string REFNO
        {
            get { return _REFNO; }
            set { _REFNO = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
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

        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
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

        public double CASH
        {
            get { return _CASH; }
            set { _CASH = value; }
        }

        public double CREDITCARDPAY
        {
            get { return _CREDITCARDPAY; }
            set { _CREDITCARDPAY = value; }
        }

        public double COUPON
        {
            get { return _COUPON; }
            set { _COUPON = value; }
        }

        public string CREDITCARDID
        {
            get { return _CREDITCARDID; }
            set { _CREDITCARDID = value; }
        }

        public double CREDITTYPE
        {
            get { return _CREDITTYPE; }
            set { _CREDITTYPE = value; }
        }

        public double VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }

        public double TOTVAT
        {
            get { return _TOTALVAT; }
            set { _TOTALVAT = value; }
        }

        public ArrayList REQUISITIONITEM
        {
            get { return _REQUISITIONITEM; }
            set { _REQUISITIONITEM = value; }
        }

        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }

        public string USEMEMBERDISCOUNT
        {
            get { return _USEMEMBERDISCOUNT; }
            set { _USEMEMBERDISCOUNT = value; }
        }
    }
}