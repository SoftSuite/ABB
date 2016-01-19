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
    public class SupplierSearchData
    {
        private string _CODE = "";
        private string _SUPPLIERNAME = "";

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
    }
}
