using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Purchase
{
    public class POEditData
    {
        double _LOID = 0;
        string _TYPE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _CODE = "";
        DateTime _POEDITDATE = new DateTime(1, 1, 1);
        string _APPROVER = "";
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _REMARK = "";
        string _REASON = "";
        string _STATUS = "";
        string _ACTIVE = "";
        double _POOLD = 0;
        double _PONEW = 0;

        string _PECODE = "";
        string _POCODE = "";
        DateTime _DATEFROM = new DateTime(1, 1, 1);
        DateTime _DATETO = new DateTime(1, 1, 1);
        DateTime _PODATEFROM = new DateTime(1, 1, 1);
        DateTime _PODATETO = new DateTime(1, 1, 1);
        string _SUPPLIER = "";
        string _STATUSFROM = "";
        string _STATUSTO = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
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
        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public DateTime POEDITDATE
        {
            get { return _POEDITDATE; }
            set { _POEDITDATE = value; }
        }
        public string APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }
        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
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
        public double POOLD
        {
            get { return _POOLD; }
            set { _POOLD = value; }
        }
        public double PONEW
        {
            get { return _PONEW; }
            set { _PONEW = value; }
        }

        public string PECODE
        {
            get { return _PECODE; }
            set { _PECODE = value; }
        }
        public string POCODE
        {
            get { return _POCODE; }
            set { _POCODE = value; }
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
        public DateTime PODATEFROM
        {
            get { return _PODATEFROM; }
            set { _PODATEFROM = value; }
        }
        public DateTime PODATETO
        {
            get { return _PODATETO; }
            set { _PODATETO = value; }
        }
        public string SUPPLIER
        {
            get { return _SUPPLIER; }
            set { _SUPPLIER = value; }
        }
        public string STATUSFROM
        {
            get { return _STATUSFROM; }
            set { _STATUSFROM = value; }
        }
        public string STATUSTO
        {
            get { return _STATUSTO; }
            set { _STATUSTO = value; }
        }
    }
}
