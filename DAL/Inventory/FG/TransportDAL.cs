using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Inventory.FG;
using ABB.Data;
namespace ABB.DAL.Inventory.FG
{
    public class TransportDAL
    {
        public DataTable GetDeliveryList(CtrlDeliveryData whereData)
        {
            string whereString = "";
            if (whereData.CODE.Trim() != "")
                whereString += "AND CODE = '" + OracleDB.QRText(whereData.CODE.Trim()) + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += "AND RESERVEDATE >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += "AND RESERVEDATE <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            if (whereData.CARNO.Trim() != "")
                whereString += "AND CARNO = '" + OracleDB.QRText(whereData.CARNO.Trim()) + "' ";
            if (whereData.DELIVERYNAME.Trim() != "")
                whereString += "AND DRIVERNAME = '" + OracleDB.QRText(whereData.DELIVERYNAME.Trim()) + "' ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT * FROM CTRLDELIVERY ORDER BY CODE) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetDeliveryItemList(double ctrldelivery)
        {
            string sql = "SELECT CDI.LOID, ROWNUM RANK, RQ.LOID REQUISITION, CDI.CONTACTNAME, CDI.CNAME, CDI.CADDRESS, CDI.CTEL, CDI.BOXQTY, RQ.INVCODE ";
            sql += "FROM CTRLDELIVERYITEM CDI ";
            sql += "INNER JOIN REQUISITION RQ ON CDI.REQUISITION = RQ.LOID ";
            sql += "WHERE CDI.CTRLDELIVERY = " + ctrldelivery;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetDeliveryItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 RQLOID, 0 BOXQTY, '' CONTACTNAME, '' CNAME, '' CADDRESS, '' CTEL ,'' INVCODE ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public string GetInvCode(double loid)
        {
            string sql = "SELECT * FROM REQUISTION WHERE LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            string INVCODE = "";
            if (dt.Rows.Count > 0)
            {
                INVCODE = dt.Rows[0]["INVCODE"].ToString();

            }

            return INVCODE;
        }

        public CtrlDeliveryItemData GetRequisition(double requisition, string invcode)
        {
            string where = "";
            if (requisition != 0)
                where += (where == "" ? "" : "AND ") + "LOID = " + requisition.ToString() + " ";

            if (invcode.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(INVCODE) = '" + invcode.Trim().ToUpper() + "' ";

            string sql = "SELECT LOID, INVCODE, REFTYPELOID, TYPENAME, CUSTOMERNAME, CADDRESS, CTEL, CONTACTNAME ";
            sql += "FROM V_INVOICE_FOR_DELIVERLY ";
            sql += (where == "" ? "" : "WHERE ") + where + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            CtrlDeliveryItemData data = new CtrlDeliveryItemData();
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                if (!Convert.IsDBNull(dRow["CADDRESS"])) data.CADDRESS = dRow["CADDRESS"].ToString();
                if (!Convert.IsDBNull(dRow["CUSTOMERNAME"])) data.CNAME = dRow["CUSTOMERNAME"].ToString();
                if (!Convert.IsDBNull(dRow["CONTACTNAME"])) data.CONTACTNAME = dRow["CONTACTNAME"].ToString();
                if (!Convert.IsDBNull(dRow["CTEL"])) data.CTEL = dRow["CTEL"].ToString();
                if (!Convert.IsDBNull(dRow["INVCODE"])) data.INVCODE = dRow["INVCODE"].ToString();
                if (!Convert.IsDBNull(dRow["LOID"])) data.REQUISITION = Convert.ToDouble(dRow["LOID"]);
            }

            return data;
        }

        public CtrlDeliveryItemData GetRequisition(string invcode)
        {
            string where = "";

            if (invcode.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(INVCODE) = '" + invcode.Trim().ToUpper() + "' ";

            string sql = "SELECT LOID, INVCODE, REFTYPELOID, TYPENAME, CUSTOMERNAME, CADDRESS, CTEL, CONTACTNAME ";
            sql += "FROM V_INVOICE_FOR_DELIVERLY ";
            sql += (where == "" ? "" : "WHERE ") + where + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            CtrlDeliveryItemData data = new CtrlDeliveryItemData();
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                if (!Convert.IsDBNull(dRow["CADDRESS"])) data.CADDRESS = dRow["CADDRESS"].ToString();
                if (!Convert.IsDBNull(dRow["CUSTOMERNAME"])) data.CNAME = dRow["CUSTOMERNAME"].ToString();
                if (!Convert.IsDBNull(dRow["CONTACTNAME"])) data.CONTACTNAME = dRow["CONTACTNAME"].ToString();
                if (!Convert.IsDBNull(dRow["CTEL"])) data.CTEL = dRow["CTEL"].ToString();
                if (!Convert.IsDBNull(dRow["INVCODE"])) data.INVCODE = dRow["INVCODE"].ToString();
                if (!Convert.IsDBNull(dRow["LOID"])) data.REQUISITION = Convert.ToDouble(dRow["LOID"]);
            }

            return data;
        }

    }
}

