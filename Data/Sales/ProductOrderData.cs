using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Sales
{
    public class ProductOrderData
    {
        private string _REMARK = "";
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private double _LOID = 0;
        private double _VAT = 0;
        private string _CODE = "";
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private DateTime _RESERVEDATE = new DateTime(1, 1, 1);
        private string _STATUS = "";
        private string _CREATEBY = "";
        private string _ACTIVE = "";
        private double _CUSTOMER = 0;
        private double _REQUISITIONTYPE = 0;
        private DateTime _DUEDATE = new DateTime(1, 1, 1);
        private ArrayList _ITEM = new ArrayList();
        private double _TOTAL = 0;
        private double _WAREHOUSE = 0;
        private double _REFWAREHOUSE = 0;
        private string _WAREHOUSENAME = "";
        private double _TOTDIS = 0;
        private double _GRANDTOT = 0;
        private double _TOTVAT = 0;

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
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

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
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

        public DateTime RESERVEDATE
        {
            get { return _RESERVEDATE; }
            set { _RESERVEDATE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
        }

        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }            
        
        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public double REFWAREHOUSE
        {
            get { return _REFWAREHOUSE; }
            set { _REFWAREHOUSE = value; }
        }

        public string WAREHOUSENAME
        {
            get { return _WAREHOUSENAME; }
            set { _WAREHOUSENAME = value; }
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
        public double TOTVAT
        {
            get { return _TOTVAT; }
            set { _TOTVAT = value; }
        }
    }


}
