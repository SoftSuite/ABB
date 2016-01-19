using System;
using System.Collections;
using System.Text;

namespace ABB.Data.Production
{
    public class QCAnalysisData
    {
        private double _STLOID = 0;
        private string _QCCODE = "";
        private DateTime _QCDATE = new DateTime(1, 1, 1);
        private string _CODE = "";
        private string _PDNAME = "";
        private double _QTY = 0;
        private string _UNAME = "";
        private string _APPROVER = "";
        private string _DVNAME = "";
        private string _STATUSVAL = "";
        private ArrayList _ITEM = new ArrayList();

        public double STLOID
        {
            get { return _STLOID; }
            set { _STLOID = value; }
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

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string PDNAME
        {
            get { return _PDNAME; }
            set { _PDNAME = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public string UNAME
        {
            get { return _UNAME; }
            set { _UNAME = value; }
        }

        public string APPROVER
        {
            get { return _APPROVER; }
            set { _APPROVER = value; }
        }

        public string DVNAME
        {
            get { return _DVNAME; }
            set { _DVNAME = value; }
        }

        public string STATUSVAL
        {
            get { return _STATUSVAL; }
            set { _STATUSVAL = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }
    }
}


