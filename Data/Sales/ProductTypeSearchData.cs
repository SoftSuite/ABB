// <summary>
// ProductTypeSearchData Class
// Version 1.0
// =======================================
// Create by: Pro
// Create Date: 11 Dec 2007
//---------------------------------------
//Modify By: -
// Modify From: -
// Modify Date: -
// ---------------------------------------
// Remark: -
// Description: Data สำหรับ SearchBox 
// =======================================
// </summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ProductTypeSearchData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _NAME = "";
        private string _TYPE = "";
        private string _ACTIVE = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

    }
}
