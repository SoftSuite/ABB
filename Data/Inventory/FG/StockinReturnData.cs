using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Inventory.FG
{
    public class StockinReturnData
    {
        private double _LOID = 0;
        private string _CREATEBY = "";
        private double _APPROVER = 0;
        private string _CODE = "";
        private double _DOCTYPE = 0;
        private double _SENDER = 0;
        private DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        private DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        private string _STATUS = "";
        private string _REMARK = "";
        private double _GRANDTOT = 0;
        private string _CADDRESS = "";
        private double _REFLOID = 0;
        private string _REFTABLE = "";
        private double _CTITLE = 0;
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private string _CTEL = "";
        private string _CFAX = "";
        private double _RECEIVER = 0;
        private ArrayList _ITEM = new ArrayList();
        private double _WAREHOUSE = 0;
        private string _PDBARCODE = "";
        private string _PDNAME = "";
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private double _QTY = 0;
        private string _UNAME = "";
        private double _PPLOID = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double PPLOID
        {
            get { return _PPLOID; }
            set { _PPLOID = value; }
        }

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

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
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

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
        }

        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
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

        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }
        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
        public string PDBARCODE
        {
            get { return _PDBARCODE; }
            set { _PDBARCODE = value; }
        }
        public string PDNAME
        {
            get { return _PDNAME; }
            set { _PDNAME = value; }
        }
        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }
        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }
        public string UNAME
        {
            get { return _UNAME; }
            set { _UNAME = value; }
        }
    }
}
