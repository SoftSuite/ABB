using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Create by: Pom
/// Create Date: 2 JAN 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

namespace ABB.Data.Inventory.FG.Master
{
    public class BasketData
    {
        //------------- กระเช้า ----------------------------
        private double _LOID = 0;
        private string _CODE = "";
        private string _NAME = "";
        private string _ABBNAME = "";
        private string _ENAME = "";
        private string _BARCODE = "";
        private double _UNITBASKET = 0;
        private double _PRODUCTTYPE = 0;
        private double _PRODUCTGROUP = 0;
        private double _COST = 0;
        private double _PRICESUM = 0;
        private double _STDPRICE = 0;
        private string _ISVAT = "";
        private string _ISDISCOUNT = "";
        private string _ISEDITPRICE = "";
        private string _ISREFUND = "";
        private string _REMARK = "";
        private string _ACTIVE = "";
        private double _PRODUCTMASTER = 0;

        //------------- สินค้าในกระเช้า ----------------------------
        private double _MAINPRODUCT = 0;
        private double _SUBPRODUCT = 0;
        private double _QUANTITY = 0;
        private double _PRICE = 0;
        private double _UNIT = 0;


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

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public string ABBNAME
        {
            get { return _ABBNAME; }
            set { _ABBNAME = value; }
        }

        public string ENAME
        {
            get { return _ENAME; }
            set { _ENAME = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public double UNITBASKET
        {
            get { return _UNITBASKET; }
            set { _UNITBASKET = value; }
        }

        public double PRODUCTTYPE
        {
            get { return _PRODUCTTYPE; }
            set { _PRODUCTTYPE = value; }
        }

        public double PRODUCTGROUP
        {
            get { return _PRODUCTGROUP; }
            set { _PRODUCTGROUP = value; }
        }

        public double COST
        {
            get { return _COST; }
            set { _COST = value; }
        }

        public double PRICESUM
        {
            get { return _PRICESUM; }
            set { _PRICESUM = value; }
        }

        public double STDPRICE
        {
            get { return _STDPRICE; }
            set { _STDPRICE = value; }
        }

        public string ISVAT
        {
            get { return _ISVAT; }
            set { _ISVAT = value; }
        }

        public string ISDISCOUNT
        {
            get { return _ISDISCOUNT; }
            set { _ISDISCOUNT = value; }
        }

        public string ISEDITPRICE
        {
            get { return _ISEDITPRICE; }
            set { _ISEDITPRICE = value; }
        }

        public string ISREFUND
        {
            get { return _ISREFUND; }
            set { _ISREFUND = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }


        public double MAINPRODUCT
        {
            get { return _MAINPRODUCT; }
            set { _MAINPRODUCT = value; }
        }

        public double SUBPRODUCT
        {
            get { return _SUBPRODUCT; }
            set { _SUBPRODUCT = value; }
        }

        public double QUANTITY
        {
            get { return _QUANTITY; }
            set { _QUANTITY = value; }
        }

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public Double PRODUCTMASTER
        {
            get { return _PRODUCTMASTER; }
            set { _PRODUCTMASTER = value; }
        }

    }
}
