using System;
using System.Collections;

namespace ABB.Data
{
    /// <summary>
    /// Represents a STOCKIN table data.
    /// </summary>
    public class StockInData
    {
        string _ACCCODE = "";
        string _ANACODE = "";
        DateTime _ANADATE = new DateTime(1, 1, 1);
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        double _APPROVER = 0;
        string _CADDRESS = "";
        string _CFAX = "";
        string _CLASTNAME = "";
        string _CNAME = "";
        string _CODE = "";
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _CTEL = "";
        double _CTITLE = 0;
        double _DOCTYPE = 0;
        double _GRANDTOT = 0;
        string _INVNO = "";
        double _LOID = 0;
        string _QCCODE = "";
        DateTime _QCDATE = new DateTime(1, 1, 1);
        string _QCRESULT = "";
        string _REASON = "";
        DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        double _RECEIVER = 0;
        double _REFLOID = 0;
        string _REFTABLE = "";
        string _REMARK = "";
        double _SENDER = 0;
        string _STATUS = "";
        double _TOTAL = 0;
        double _TOTDIS = 0;
        double _TOTVAT = 0;
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _REFCODE = "";
        ArrayList _STOCKINITEM = new ArrayList();

        public string ACCCODE
        {
            get { return _ACCCODE; }
            set { _ACCCODE = value; }
        }

        public string ANACODE
        {
            get { return _ANACODE; }
            set { _ANACODE = value; }
        }

        public DateTime ANADATE
        {
            get { return _ANADATE; }
            set { _ANADATE = value; }
        }

        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }

        public double APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }

        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }

        public string CFAX
        {
            get { return _CFAX; }
            set { _CFAX = value; }
        }

        public string CLASTNAME
        {
            get { return _CLASTNAME; }
            set { _CLASTNAME = value; }
        }

        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
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

        public string CTEL
        {
            get { return _CTEL; }
            set { _CTEL = value; }
        }

        public double CTITLE
        {
            get { return _CTITLE; }
            set { _CTITLE = value; }
        }

        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }

        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
        }

        public string INVNO
        {
            get { return _INVNO; }
            set { _INVNO = value; }
        }

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string QCCODE
        {
            get { return _QCCODE; }
            set { _QCCODE = value; }
        }

        public DateTime QCDATE
        {
            get { return _QCDATE; }
            set { _QCDATE = value; }
        }

        public string QCRESULT
        {
            get { return _QCRESULT; }
            set { _QCRESULT = value; }
        }

        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
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

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public double SENDER
        {
            get { return _SENDER; }
            set { _SENDER = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
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

        public double TOTVAT
        {
            get { return _TOTVAT; }
            set { _TOTVAT = value; }
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

        public ArrayList STOCKINITEM
        {
            get { return _STOCKINITEM; }
            set { _STOCKINITEM = value; }
        }

        public string REFCODE
        {
            get { return _REFCODE; }
            set { _REFCODE = value; }
        }

    }
}