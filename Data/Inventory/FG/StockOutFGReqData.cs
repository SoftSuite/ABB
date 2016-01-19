using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class StockOutFGReqData
    {
        private double _REQUISITION = 0;
        private string _REQUISITIONCODE = "";
        private double _CUSTOMER = 0;
        private string _CUSTOMERCODE = "";
        private string _CUSTOMERNAME = "";
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private DateTime _RESERVEDATE = new DateTime(1, 1, 1);
        private double _REQUISITIONTYPE = 0;
        private double _DOCTYPE = 0;
        private double _CTITLE = 0;
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private string _CADDRESS = "";
        private string _CTEL = "";
        private string _CFAX = "";
        private DataTable _REQUISITIONITEM = new DataTable();
        private double _WAREHOUSE = 0;
        private double _REFWAREHOUSE = 0;
        private string _WAREHOUSECODE = "";
        private string _WAREHOUSENAME = "";

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }

        public string REQUISITIONCODE
        {
            get { return _REQUISITIONCODE; }
            set { _REQUISITIONCODE = value; }
        }

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
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

        public DateTime RESERVEDATE
        {
            get { return _RESERVEDATE; }
            set { _RESERVEDATE = value; }
        }

        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }

        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
        }

        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
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

        public DataTable REQUISITIONITEM
        {
            get { return _REQUISITIONITEM; }
            set { _REQUISITIONITEM = value; }
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

        public string WAREHOUSECODE
        {
            get { return _WAREHOUSECODE; }
            set { _WAREHOUSECODE = value; }
        }

        public string WAREHOUSENAME
        {
            get { return _WAREHOUSENAME; }
            set { _WAREHOUSENAME = value; }
        }

    }
}
