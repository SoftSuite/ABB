using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Inventory.FG
{
    public class CtrlDeliveryData
    {
        double _LOID = 0;
        double _TYPE= 0;
        string _CODE = "";
        DateTime _DATEFROM = new DateTime(1, 1, 1);
        DateTime _DATETO = new DateTime(1, 1, 1);
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        DateTime _DELIVERYDATE = new DateTime(1, 1, 1);
        string _CARNO = "";
        string _DELIVERYNAME = "";
        private ArrayList _ITEM = new ArrayList();

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public DateTime DATEFROM
        {
            get { return _DATEFROM; }
            set { _DATEFROM = value; }
        }

        public DateTime DATETO
        {
            get { return _DATETO; }
            set { _DATETO = value; }
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

        public DateTime DELIVERYDATE
        {
            get { return _DELIVERYDATE; }
            set { _DELIVERYDATE = value; }
        }

        public string CARNO
        {
            get { return _CARNO; }
            set { _CARNO = value; }
        }

        public string DELIVERYNAME
        {
            get { return _DELIVERYNAME; }
            set { _DELIVERYNAME = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }

    }
}
