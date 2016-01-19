using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Create by: Pom
/// Create Date: 21 Dec 2007
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>
/// 

namespace ABB.Data.Inventory.FG.Master
{
    public class BasketSearchResultData
    {
        private int _ORDERNO = 0;
        private string _LOID = "";
        private string _BARCODE = "";
        private string _PRODUCTNAME = "";
        private string _COST = "";
        private string _PRICE = "";

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public string LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

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

        public string COST
        {
            get { return _COST; }
            set { _COST = value; }
        }

        public string PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }
    }
}
