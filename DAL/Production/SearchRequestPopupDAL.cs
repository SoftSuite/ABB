using System;
using System.Collections.Generic;
using System.Text;
using ABB.DAL;
using System.Collections;
using System.Data;

namespace ABB.DAL.Production
{
    public class SearchRequestPopupDAL
    {
        public static DataTable GetRequestPopup(string CFrom, string CTo, string Datefrom, string DateTo, string Pname)
        {   DataTable dt = new DataTable();
            string str = "";
            string whr = "";
            str = " SELECT RQILOID,CODE,TO_CHAR(REQDATE,'DD/MM/YYYY') REQDATE,NAME, SUM(QTY) QTY, UNAME,PDLOID  ";
            str += " FROM V_PRODUCT_REQUEST ";

            if (Datefrom.Trim() != "1/1/1" && DateTo.Trim() != "1/1/1")
                whr += (whr == "" ? "WHERE " : "AND ")+ " REQDATE BETWEEN TO_DATE('" + Datefrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY')";

            if (CFrom != "" && CTo != "")
                whr += (whr == "" ? "WHERE " : "AND ") + " UPPER(CODE) BETWEEN UPPER('" + CFrom + "') AND UPPER('" + CTo + "')";

            else if (CFrom == "" && CTo != "")
                whr += (whr == "" ? "WHERE " : "AND ") + " UPPER(CODE) <= UPPER('" + CTo + "')";

            else if (CFrom != "" && CTo == "")
                whr += (whr == "" ? "WHERE " : "AND ") + " UPPER(CODE) >= UPPER('" + CFrom + "')";


            if (Pname != "")
                whr += (whr == "" ? "WHERE " : "AND ") + " UPPER(NAME) LIKE UPPER('%" + Pname + "%')";

            str += whr;
            str += " GROUP BY RQILOID,CODE,TO_CHAR(REQDATE,'DD/MM/YYYY'),NAME, UNAME,PDLOID  ";
            str += " ORDER BY REQDATE ";
            dt = OracleDB.ExecListCmd(str);
            return dt;
        }
    }
}
