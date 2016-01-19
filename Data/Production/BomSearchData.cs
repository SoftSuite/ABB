using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class BomSearchData
    {
        private string _BARCODE = "";
        private string _PRODUCTNAME = "";
        private string _RADIATION = "";
        private double _PRODUCTGROUP = 0;
        private double _PRODUCTTYPE = 0;
        private string _PROCESS = "";
        private string _ACTIVE = "";
        private double _MAINPRODUCT = 0;
        private double _OLDMAINPRODUCT = 0;
        private ArrayList _ITEM = new ArrayList();

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string RADIATION
        {
            get { return _RADIATION; }
            set { _RADIATION = value; }
        }

        public double PRODUCTGROUP
        {
            get { return _PRODUCTGROUP; }
            set { _PRODUCTGROUP = value; }
        }

        public double PRODUCTTYPE
        {
            get { return _PRODUCTTYPE; }
            set { _PRODUCTTYPE = value; }
        }

        public string PROCESS
        {
            get { return _PROCESS; }
            set { _PROCESS = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public double MAINPRODUCT
        {
            get { return _MAINPRODUCT; }
            set { _MAINPRODUCT = value; }
        }

        public double OLDMAINPRODUCT
        {
            get { return _OLDMAINPRODUCT; }
            set { _OLDMAINPRODUCT = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }

    }
}
