using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data;
using ABB.Data.Purchase;
using ABB.DAL;

/// <summary>
/// Create by: Ta
/// Create Date: 8 Jan 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

namespace ABB.Flow.Purchase
{
    public class SupplierFlow
    {
        string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public static DataTable GetSupplier(string SupLOID)
        {
            string sql = "SELECT * FROM SUPPLIER WHERE LOID = '" + SupLOID + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public bool UpdateData(string UserID, SupplierData supData)
        {
            Boolean ret = true;
            SupplierDAL oDAL = new SupplierDAL();

            oDAL.OnDB = false;
            oDAL.GetDataByLOID(Convert.ToDouble(supData.LOID == "" ? "0" : supData.LOID), null);
            SetDataToDAL(oDAL, supData);

            if (oDAL.OnDB)
            {
                ret = oDAL.UpdateCurrentData(UserID, null);
            }
            else
            {
                ret = oDAL.InsertCurrentData(UserID, null);
            }

            if (ret == false)
                _error = oDAL.ErrorMessage;

            return ret;
        }

        public double GetLOIDbyCODE(string SupCode)
        {
            string sql = "SELECT LOID FROM SUPPLIER WHERE CODE = '" + SupCode + "'";
            object LOID = OracleDB.ExecSingleCmd(sql);
            return Convert.ToDouble(LOID);
        }

        public double GetLOIDbyTAXID(string TaxID)
        {
            string sql = "SELECT LOID FROM SUPPLIER WHERE TAXID = '" + TaxID + "'";
            object LOID = OracleDB.ExecSingleCmd(sql);
            return Convert.ToDouble(LOID);
        }

        public double GetLOIDbySUPPLIERNAME(string Name)
        {
            string sql = "SELECT LOID FROM SUPPLIER WHERE SUPPLIERNAME = '" + Name + "'";
            object LOID = OracleDB.ExecSingleCmd(sql);
            return Convert.ToDouble(LOID);
        }

        private void SetDataToDAL(SupplierDAL oDAL, SupplierData supData)
        {
            oDAL.ACTIVE = supData.ACTIVE;

            //-------------------- ชื่อบริษัท/ผู้จำหน่าย ---------------------------------------
            oDAL.CODE = supData.CODE;
            oDAL.SUPPLIERNAME = supData.SUPPLIERNAME;
            oDAL.TAXID = supData.TAXID;

            //-------------------- ที่อยู่บริษัท/ผู้จำหน่าย ---------------------------------------
            oDAL.ADDRESS = supData.ADDRESS;
            oDAL.ROAD = supData.ROAD;
            oDAL.PROVINCE = supData.PROVINCE;
            oDAL.AMPHUR = supData.AMPHUR;
            oDAL.TAMBOL = supData.TAMBOL;
            oDAL.ZIPCODE = supData.ZIPCODE;
            oDAL.TEL = supData.TEL;
            oDAL.FAX = supData.FAX;
            oDAL.EMAIL = supData.EMAIL;

            //-------------------- ชื่อผู้ติดต่อ ---------------------------------------
            oDAL.CTITLE = supData.CTITLE;
            oDAL.CNAME = supData.CNAME;
            oDAL.CLASTNAME = supData.CLASTNAME;
            oDAL.CTEL = supData.CTEL;
            oDAL.CMOBILE = supData.CMOBILE;
            oDAL.CEMAIL = supData.CEMAIL;
            oDAL.CADDRESS = supData.CADDRESS;
            oDAL.CROAD = supData.CROAD;
            oDAL.CPROVINCE = supData.CPROVINCE;
            oDAL.CAMPHUR = supData.CAMPHUR;
            oDAL.CTAMBOL = supData.CTAMBOL;
            oDAL.CZIPCODE = supData.CZIPCODE;

            //-------------------- หมายเหตุ ---------------------------------------
            oDAL.REMARK = supData.REMARK;
        }
    }
}
