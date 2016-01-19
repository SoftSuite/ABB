using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Inventory
{
    public class StockinProductData : Common.CommonData
    {
        private string _REMARK = "";
        private double _LOID = 0;
        private string _CODE = "";
        private string _SPCODE = "";
        private DateTime _STCREATEON = new DateTime(1, 1, 1);
        private DateTime _MFGDATE = new DateTime(1, 1, 1);
        private string _SPNAME = "";
        private double _QTY = 0;
        private double _PDQTY = 0;
        private double _WAREHOUSE = 0;
        private double _REQUISITIONTYPE = 0;
        private string _STATUS = "";
        private string _ACTIVE = "";
        private ArrayList _ITEM = new ArrayList();
        double _DOCTYPE = 0;
        double _SENDER = 0;
        private double _PRODUCT = 0;
        private double _UNIT = 0;
        private double _PDPRODUCT = 0;
        private string _USERID = "";
        private string _LOTNO = "";
        private string _UNITNAME = "";
        private string _PRODUCTNAME = "";
        double _REFLOID = 0;
        string _REFTABLE = "";
        double _RECEIVER = 0;

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

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

        public string SPCODE
        {
            get { return _SPCODE; }
            set { _SPCODE = value; }
        }

        public DateTime STCREATEON
        {
            get { return _STCREATEON; }
            set { _STCREATEON = value; }
        }

        public DateTime MFGDATE
        {
            get { return _MFGDATE; }
            set { _MFGDATE = value; }
        }

        public string SPNAME
        {
            get { return _SPNAME; }
            set { _SPNAME = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
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
        
        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }
        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }

        public double SENDER
        {
            get { return _SENDER; }
            set { _SENDER = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public double PDPRODUCT
        {
            get { return _PDPRODUCT; }
            set { _PDPRODUCT = value; }
        }
        public string USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
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

        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }
    }
}


