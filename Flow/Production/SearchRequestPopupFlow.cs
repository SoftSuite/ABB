using System;
using System.Collections.Generic;
using System.Text;
using ABB.DAL.Production;
using System.Collections;
using System.Data;

namespace ABB.Flow.Production
{
    public  class SearchRequestPopupFlow
    {
        public static DataTable GetRequestPopup(string CFrom, string CTo, string Datefrom, string DateTo, string Pname)
        {
            DataTable dt = new DataTable();
            dt = SearchRequestPopupDAL.GetRequestPopup(CFrom, CTo, Datefrom, DateTo, Pname);
            return dt;
        }
    }
}
