using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class UserData
    {
        private string _UserID = "";
        private double _Warehouse = 0;
        private string _Name = "";
        private double _OfficerLOID = 0;
        private double _DivisionID = 0;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public double Warehouse
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        }

        public string Name
        {
            get { return (_Name == "" ? " ไม่ระบุ " : _Name); }
            set { _Name = value; }
        }

        public double OfficerID
        {
            get { return _OfficerLOID; }
            set { _OfficerLOID = value; }
        }

        public double DivisionID
        {
            get { return _DivisionID; }
            set { _DivisionID = value; }
        }

    }
}
