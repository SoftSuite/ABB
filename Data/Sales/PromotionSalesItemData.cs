using System;
using System.Collections.Generic;
using System.Text;


namespace ABB.Data.Sales
{
    public class PromotionSalesItemData
    {
        private double _LOID = 0;
        private string _BARCODE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        private string _NAME = "";
        private string _UNAME = "";
        private double _PRICEOLD = 0;
        private double _PRICENEW = 0;
        private double _PRODUCT = 0;
        private double _PROMOTION = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public DateTime CREATEON
        {
            get { return _CREATEON; }
            set { _CREATEON = value; }
        }

        public string UPDATEBY
        {
            get { return _UPDATEBY; }
            set { _UPDATEBY = value; }
        }

        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
            set { _UPDATEON = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public string UNAME
        {
            get { return _UNAME; }
            set { _UNAME = value; }
        }

        public double PRICEOLD
        {
            get { return _PRICEOLD; }
            set { _PRICEOLD = value; }
        }
        public double PRICENEW
        {
            get { return _PRICENEW; }
            set { _PRICENEW = value; }
        }
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public double PROMOTION
        {
            get { return _PROMOTION; }
            set { _PROMOTION = value; }
        }
    }
}
