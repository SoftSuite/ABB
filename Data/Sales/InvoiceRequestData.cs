using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ABB.Data.Sales
{
    public class InvoiceRequestData : Common.CommonData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private double _REFLOID = 0;
        private string _STATUS = "";
        private double _CUSTOMER = 0;
        private double _WAREHOUSE = 0;
        private double _CTITLE = 0;
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private string _CADDRESS = "";
        private string _CTEL = "";
        private string _CFAX = "";
        private string _REASON = "";
        private string _REMARK = "";
        private double _TOTAL = 0;
        private double _TOTDIS = 0;
        private double _VAT = 0;
        private double _TOTVAT = 0;
        private double _GRANDTOT = 0;
        private double _OLDTOTAL = 0;
        private string _INVCODE = "";
        private string _CUSTOMERCODE = "";
        private string _CUSTOMERNAME = "";
        private ArrayList _ITEMS = new ArrayList();
        private DataTable _OLDITEMS = new DataTable();

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

        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
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

        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
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

        public double OLDTOTAL
        {
            get { return _OLDTOTAL; }
            set { _OLDTOTAL = value; }
        }

        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }

        public string CUSTOMERCODE
        {
            get { return _CUSTOMERCODE; }
            set { _CUSTOMERCODE = value; }
        }

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }

        public ArrayList ITEMS
        {
            get { return _ITEMS; }
            set { _ITEMS = value; }
        }

        public DataTable OLDITEMS
        {
            get { return _OLDITEMS; }
            set { _OLDITEMS = value; }
        }

    }
}
