using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class SalePayData
    {
        private double _CASH = 0;
        private double _CREDITCARDPAY = 0;
        private double _COUPON = 0;
        private string _CREDITCARDID = "";
        private double _CREDITTYPE = 0;
        private double _GRANDTOTAL = 0;

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

        public double GRANDTOTAL
        {
            get { return _GRANDTOTAL; }
            set { _GRANDTOTAL = value; }
        }

    }
}
