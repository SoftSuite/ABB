using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class CtrlDeliveryItemData
    {
        double _LOID = 0;
        double _CTRLDELIVERY= 0;
        double _REQUISITION = 0;
        string _CODE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CONTACTNAME = "";
        string _CNAME = "";
        string _CADDRESS = "";
        string _CTEL = "";
        double _BOXQTY = 0;
        string _INVCODE = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double CTRLDELIVERY
        {
            get { return _CTRLDELIVERY; }
            set { _CTRLDELIVERY = value; }
        }

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
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


        public string CONTACTNAME
        {
            get { return _CONTACTNAME; }
            set { _CONTACTNAME = value; }
        }

        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }

        public string CTEL
        {
            get { return _CTEL; }
            set { _CTEL = value; }
        }

        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }

        public double BOXQTY
        {
            get { return _BOXQTY; }
            set { _BOXQTY = value; }
        }

        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }
    }
}
