using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class BomData
    {
        private double  _LOID = 0;
        private string _CODE = "";
        private double _MAINPRODUCT = 0;
        private double _MATERIAL = 0;
        private double _MASTER = 0;
        private double _UNIT = 0;
        private string  _RADIATION = "";
        private string _ACTIVE = "";
        private double _PROCESS = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string  CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public double MAINPRODUCT
        {
            get { return _MAINPRODUCT; }
            set { _MAINPRODUCT = value; }
        }

        public double MATERIAL
        {
            get { return _MATERIAL; }
            set { _MATERIAL = value; }
        }

        public double MASTER
        {
            get { return _MASTER; }
            set { _MASTER = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }

        public string RADIATION
        {
            get { return _RADIATION; }
            set { _RADIATION = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public double PROCESS
        {
            get { return _PROCESS; }
            set { _PROCESS = value; }
        }
    }
}
