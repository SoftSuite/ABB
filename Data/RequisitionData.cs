using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class RequisitionData : ABB.Data.Common.CommonData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _REFNO = "";
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private string _INVCODE = "";
        private DateTime _RESERVEDATE = new DateTime(1, 1, 1);
        private double _CUSTOMER = 0;
        private double _OFFICER = 0;
        private double _WAREHOUSE = 0;
        private double _TOTAL = 0;
        private double _TOTDIS = 0;
        private double _GRANDTOT = 0;
        private double _REQUISITIONTYPE = 0;
        private double _REFLOID = 0;
        private string _REFTABLE = "";
        private string _STATUS = "";
        private string _ACTIVE = "";
        private string _LOTNO = "";
        private string _PAYMENT = "";
        private double _CASH = 0;
        private double _CREDITPAY = 0;
        private double _CREDITCARDPAY = 0;
        private string _CREDITCARDID = "";
        private double _COUPON = 0;
        private double _BANK = 0;
        private double _CREDITTYPE = 0;
        private string _CHEQUE = "";
        private DateTime _CHEQUEDATE = new DateTime(1, 1, 1);
        private string _REASON = "";
        private DateTime _DUEDATE = new DateTime(1, 1, 1);
        private string _REMARK = "";
        private double _CTITLE = 0;
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private string _CADDRESS = "";
        private string _CTEL = "";
        private string _CFAX = "";
        private string _CEMAIL = "";
        private double _VAT = 0;
        private double _TOTVAT = 0;
        private string _DELIVERYTYPE = "";
        private string _OTHER = "";
        private DateTime _CREDITDATE = new DateTime(1, 1, 1);
        private string _BANKBRANCH = "";
        private string _PAYMENTCONDITION = "";
        private double _REFTYPELOID = 0;
        private string _REFTYPETABLE = "";
        private double _REFWAREHOUSE = 0;
        private ArrayList _REQUISITIONITEM = new ArrayList();
        
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

        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }

        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }

        public DateTime RESERVEDATE
        {
            get { return _RESERVEDATE; }
            set { _RESERVEDATE = value; }
        }

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public double OFFICER
        {
            get { return _OFFICER; }
            set { _OFFICER = value; }
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

        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
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

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public string PAYMENT
        {
            get { return _PAYMENT; }
            set { _PAYMENT = value; }
        }

        public double CASH
        {
            get { return _CASH; }
            set { _CASH = value; }
        }

        public double CREDITPAY
        {
            get { return _CREDITPAY; }
            set { _CREDITPAY = value; }
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

        public double BANK
        {
            get { return _BANK; }
            set { _BANK = value; }
        }

        public double CREDITTYPE
        {
            get { return _CREDITTYPE; }
            set { _CREDITTYPE = value; }
        }

        public string CHEQUE
        {
            get { return _CHEQUE; }
            set { _CHEQUE = value; }
        }

        public DateTime CHEQUEDATE
        {
            get { return _CHEQUEDATE; }
            set { _CHEQUEDATE = value; }
        }

        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }

        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
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

        public string CEMAIL
        {
            get { return _CEMAIL; }
            set { _CEMAIL = value; }
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

        public string DELIVERYTYPE
        {
            get { return _DELIVERYTYPE; }
            set { _DELIVERYTYPE = value; }
        }

        public string OTHER
        {
            get { return _OTHER; }
            set { _OTHER = value; }
        }

        public DateTime CREDITDATE
        {
            get { return _CREDITDATE; }
            set { _CREDITDATE = value; }
        }

        public string BANKBRANCH
        {
            get { return _BANKBRANCH; }
            set { _BANKBRANCH = value; }
        }

        public string PAYMENTCONDITION
        {
            get { return _PAYMENTCONDITION; }
            set { _PAYMENTCONDITION = value; }
        }

        public double REFTYPELOID
        {
            get { return _REFTYPELOID; }
            set { _REFTYPELOID = value; }
        }

        public string REFTYPETABLE
        {
            get { return _REFTYPETABLE; }
            set { _REFTYPETABLE = value; }
        }

        public double REFWAREHOUSE
        {
            get { return _REFWAREHOUSE; }
            set { _REFWAREHOUSE = value; }
        }

        public ArrayList REQUISITIONITEM
        {
            get { return _REQUISITIONITEM; }
            set { _REQUISITIONITEM = value; }
        }

    }
}
