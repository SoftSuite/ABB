using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Create by: Ta
/// Create Date: 9 Jan 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

namespace ABB.Data.Purchase
{
    public class SupplierResultData
    {
        private string _LOID = "";
        private int _ORDERNO = 0;
        private string _CODE = "";
        private string _SUPPLIERNAME = "";
        private string _TAXID = "";
        private string _CNAME = "";
        private string _TEL = "";

        public string LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string SUPPLIERNAME
        {
            get { return _SUPPLIERNAME; }
            set { _SUPPLIERNAME = value; }
        }

        public string TAXID
        {
            get { return _TAXID; }
            set { _TAXID = value; }
        }

        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }

        public string TEL
        {
            get { return _TEL; }
            set { _TEL = value; }
        }
    }
}
