using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ProductStockInShopPopupData
    {
        int _ORDERNO = 0;
        DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        string _RQCODE = "";
        String _CREATEBY = "";

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

        public string RQCODE
        {
            get { return _RQCODE; }
            set { _RQCODE = value; }
        }

        public String CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }
    }
}
