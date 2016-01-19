using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class PDReserveItemData
    {
        private double _LOID = 0;
        private string _RWBARCODE = "";
        private string _RWNAME = "";
        private string _RWGROUPNAME = "";
        private double _MASTER = 0;
        private string _UNAME = "";
        private string _STATUS = "";
        private string _ACTIVE = "";
        private int _RANK = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string RWBARCODE
        {
            get { return _RWBARCODE; }
            set { _RWBARCODE = value; }
        }

        public string RWNAME
        {
            get { return _RWNAME; }
            set { _RWNAME = value; }
        }

        public string RWGROUPNAME
        {
            get { return _RWGROUPNAME; }
            set { _RWGROUPNAME = value; }
        }

        public double MASTER
        {
            get { return _MASTER; }
            set { _MASTER = value; }
        }

        public string UNAME
        {
            get { return _UNAME; }
            set { _UNAME = value; }
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

        public int RANK
        {
            get { return _RANK; }
            set { _RANK = value; }
        }
    }
}

