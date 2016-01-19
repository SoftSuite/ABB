using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Admin
{
    public class DepartmentData
    {
        double _LOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _TNAME = "";
        DateTime _EFDATE = new DateTime(1, 1, 1);
        DateTime _EPDATE = new DateTime(1, 1, 1);
        string _CODE = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public string CREATEBY
        {
            get { return _CREATEBY; }
        }
        public DateTime CREATEON
        {
            get { return _CREATEON; }
        }
        public string UPDATEBY
        {
            get { return _UPDATEBY; }
        }
        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
        }

        public string TNAME
        {
            get { return _TNAME; }
            set { _TNAME = value; }
        }

        public DateTime EFDATE
        {
            get { return _EFDATE; }
            set { _EFDATE = value; }
        }

        public DateTime EPDATE
        {
            get { return _EPDATE; }
            set { _EPDATE = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
    }
}
