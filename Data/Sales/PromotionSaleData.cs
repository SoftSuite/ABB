using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Sales
{
    public class PromotionSaleData
    {
        private double _LOID = 0;
        private string _CODE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        private string _NAME = "";
        private DateTime _EFDATEFROM = new DateTime(1, 1, 1);
        private DateTime _EFDATETO = new DateTime(1, 1, 1);
        private DateTime _EPDATEFROM = new DateTime(1, 1, 1);
        private DateTime _EPDATETO = new DateTime(1, 1, 1);
        private double _DISCOUNT = 0;
        private double _WAREHOUSE = 0;
        private double _ZONE = 0;
        private double _LOWERPRICE = 0;
        private ArrayList _ITEM = new ArrayList();

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

        public DateTime EFDATEFROM
        {
            get { return _EFDATEFROM; }
            set { _EFDATEFROM = value; }
        }

        public DateTime EFDATETO
        {
            get { return _EFDATETO; }
            set { _EFDATETO = value; }
        }
        public DateTime EPDATEFROM
        {
            get { return _EPDATEFROM; }
            set { _EPDATEFROM = value; }
        }

        public DateTime EPDATETO
        {
            get { return _EPDATETO; }
            set { _EPDATETO = value; }
        }
        public double DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
        public double ZONE
        {
            get { return _ZONE; }
            set { _ZONE = value; }
        }
        public double LOWERPRICE
        {
            get { return _LOWERPRICE; }
            set { _LOWERPRICE = value; }
        }
        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }
    }
}
