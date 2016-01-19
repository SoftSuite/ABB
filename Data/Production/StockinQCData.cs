using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Production
{
    public class StockinQCData
    {
        double _LOID = 0;
        double _PDORDERLOID = 0;
        double _TYPE = 0;
        double _PRODUCT = 0;
        string _CODE = "";
        string _POCODE = "";
        string _CODEFROM = "";
        string _CODETO = "";
        DateTime _RECEIVEFROM = new DateTime(1, 1, 1);
        DateTime _RECEIVETO = new DateTime(1, 1, 1);
        DateTime _ORDERFROM = new DateTime(1, 1, 1);
        DateTime _ORDERTO = new DateTime(1, 1, 1);
        DateTime _DATEFROM = new DateTime(1, 1, 1);
        DateTime _DATETO = new DateTime(1, 1, 1);
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        string _QCCODE = "";
        string _ACCCODE = "";
        double _DOCTYPE = 0;
        double _SENDER = 0;
        string _SENDERCODE = "";
        string _SENDERNAME = "";
        double _RECEIVER = 0;
        DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        double _APPROVER = 0;
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _INVNO = "";
        string _STATUS = "";
        DateTime _QCDATE = new DateTime(1, 1, 1);
        double _GRANDTOT = 0;
        string _REMARK = "";
        string _REASON = "";
        string _ACTIVE = "";
        string _ANACODE = "";
        DateTime _ANADATE = new DateTime(1, 1, 1);
        private string _STATUSFROM = "";
        private string _STATUSTO = "";
        private ArrayList _ITEM = new ArrayList();
        double _WAREHOUSE = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double PDORDERLOID
        {
            get { return _PDORDERLOID; }
            set { _PDORDERLOID = value; }
        }

        public double TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string POCODE
        {
            get { return _POCODE; }
            set { _POCODE = value; }
        }

        public string CODEFROM
        {
            get { return _CODEFROM; }
            set { _CODEFROM = value; }
        }

        public string CODETO
        {
            get { return _CODETO; }
            set { _CODETO = value; }
        }

        public DateTime RECEIVEFROM
        {
            get { return _RECEIVEFROM; }
            set { _RECEIVEFROM = value; }
        }

        public DateTime ORDERTO
        {
            get { return _ORDERTO; }
            set { _ORDERTO = value; }
        }

        public DateTime ORDERFROM
        {
            get { return _ORDERFROM; }
            set { _ORDERFROM = value; }
        }

        public DateTime RECEIVETO
        {
            get { return _RECEIVETO; }
            set { _RECEIVETO = value; }
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

        public string QCCODE
        {
            get { return _QCCODE; }
            set { _QCCODE = value; }
        }

        public string ACCCODE
        {
            get { return _ACCCODE; }
            set { _ACCCODE = value; }
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

        public string SENDERCODE
        {
            get { return _SENDERCODE; }
            set { _SENDERCODE = value; }
        }

        public string SENDERNAME
        {
            get { return _SENDERNAME; }
            set { _SENDERNAME = value; }
        }

        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

        public double APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }

        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }

        public string INVNO
        {
            get { return _INVNO; }
            set { _INVNO = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public DateTime QCDATE
        {
            get { return _QCDATE; }
            set { _QCDATE = value; }
        }

        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
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
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
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
    }
}
