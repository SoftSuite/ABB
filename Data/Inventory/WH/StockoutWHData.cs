using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Inventory.WH
{
    public class StockoutWHData
    {
        private string _TYPE = "";
        private string _CREATEBY = "";
        private double _APPROVER = 0;
        private string _REMARK = "";
        private string _PRODUCTCODE = "";
        private double _PRODUCT = 0;
        private string _PRODUCTNAME = "";
        private double _LOID = 0;
        private string _REQCODE = "";
        private string _REFTABLE = "";
        private string _CODE = "";
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private DateTime _DUEDATE = new DateTime(1, 1, 1);
        private double _TOTAL = 0;
        private double _REQUISITIONTYPE = 0;
        private string _STATUS = "";
        private string _ACTIVE = "";
        private string _LOTNO = "";
        private double _QTY = 0;
        private string _UNIT = "";
        private double _REFLOID = 0;
        private DataTable _STOCKOUTITEM = null;
        private DateTime _CREATEON = new DateTime(1, 1, 1);
        private DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        private double _CUSTOMER = 0;
        private double _CTITLE = 0;
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private string _CADDRESS = "";
        private string _CTEL = "";
        private string _CFAX = "";
        //private string _CEMAIL = "";
        //private string _CDELIVERY = "";
        //private double _VAT = 0;
        private double _WAREHOUSE = 0;
        //private string _REFNO = "";
        private string _INVCODE = "";
        private string _PRODUCTREF = "";
        private double _REFPROD = 0;
        private double _SENDER = 0;
        private string _SUPPORTREFCODE = "";
        private string _SUPPORTCAUSE = "";
        private double _DIVISION = 0;
        private double _DOCTYPE = 0;

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public double APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }

        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public string PRODUCTCODE
        {
            get { return _PRODUCTCODE; }
            set { _PRODUCTCODE = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }


        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string REQCODE
        {
            get { return _REQCODE; }
            set { _REQCODE = value; }
        }

        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }

        public string UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
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

        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }

        public DateTime CREATEON
        {
            get { return _CREATEON; }
            set { _CREATEON = value; }
        }

        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
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

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public DataTable STOCKOUTITEM
        {
            get { return _STOCKOUTITEM; }
            set { _STOCKOUTITEM = value; }
        }
        //public string OTHER
        //{
        //    get { return _OTHER; }
        //    set { _OTHER = value; }
        //}

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



        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
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

        public double SENDER
        {
            get { return _SENDER; }
            set { _SENDER = value; }
        }

        public double REFPROD
        {
            get { return _REFPROD; }
            set { _REFPROD = value; }
        }

        public string PRODUCTREF
        {
            get { return _PRODUCTREF; }
            set { _PRODUCTREF = value; }
        }

        public string SUPPORTREFCODE
        {
            get { return _SUPPORTREFCODE; }
            set { _SUPPORTREFCODE = value; }
        }

        public string SUPPORTCAUSE
        {
            get { return _SUPPORTCAUSE; }
            set { _SUPPORTCAUSE = value; }
        }

        public double DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }

        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }
    }
}

