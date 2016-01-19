using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Create by: Pom
/// Create Date: 18 Dec 2007
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

namespace ABB.Data.Inventory.FG.Master
{
    public class BasketSearchData
    {
        private string _BARCODE = "";
        private string _PRODUCTNAME = "";

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }
    }
}
