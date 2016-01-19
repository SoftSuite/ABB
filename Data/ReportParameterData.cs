using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class ReportParameterData
    {
        private string _PARAMETERNAME = "";
        private string _PARAMETERVALUE = "";

        public string PARAMETERNAME
        {
            get { return _PARAMETERNAME; }
            set { _PARAMETERNAME = value; }
        }

        public string PARAMETERVALUE
        {
            get { return _PARAMETERVALUE; }
            set { _PARAMETERVALUE = value; }
        }
    }
}
