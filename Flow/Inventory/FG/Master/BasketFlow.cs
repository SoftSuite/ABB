using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.DAL;
using ABB.Data.Inventory.FG.Master;

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

namespace ABB.Flow.Inventory.FG.Master
{
    public class BasketFlow
    {
        string _error = "";
        double _PBLOID = 0;
        


        public string ErrorMessage
        {
            get { return _error; }
        }

        public double PBLOID
        {
            get { return _PBLOID; }
        }

        public static DataTable GetProduct(string LOID)
        {
            string sql = "SELECT * FROM V_PRODUCT_LIST WHERE LOID = " + LOID;
            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public static DataTable GetProductByLoid(double LOID)
        {
            string sql = "SELECT * FROM PRODUCT WHERE LOID = " + LOID;
            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public static DataTable GetProductByBarcode(string Barcode)
        {
            string sql = "SELECT * FROM V_PRODUCT_LIST WHERE BARCODE = '" + Barcode + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public static double GetProductType(string product_group_loid)
        {
            string sql = "SELECT PRODUCTTYPE FROM PRODUCTGROUP WHERE LOID = " + product_group_loid;
            object producttype = OracleDB.ExecSingleCmd(sql);
            return Convert.ToDouble(producttype);
        }

        public static DataTable GetProductInPackage(string LOID)
        {
            string sql = "SELECT PD.LOID, PD.BARCODE, PD.NAME AS PRODUCTNAME, PK.QTY AS QUANTITY, PK.UNIT AS PUNIT, U.NAME AS UNIT,";
            sql += "PD.COST, PD.PRICE, PD.STDPRICE";
            sql += " FROM PRODUCT PD LEFT JOIN PACKAGE PK ON PD.LOID = PK.SUBPRODUCT";
            sql += " INNER JOIN UNIT U ON U.LOID = PK.UNIT";
            sql += " WHERE PK.MAINPRODUCT = " + LOID;
            sql += " ORDER BY PD.BARCODE";
            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }


        public bool InsertData(string UserID, BasketData bkData, DataTable tempTable)
        {
            double _PMLOID = 0;
            
            _PMLOID = InsertProductMaster(UserID, bkData);
            if (_PMLOID == 0)
                return false;
            
            _PBLOID = InsertProductBarcode(UserID, bkData, _PMLOID);
            if (_PBLOID == 0)
                return false;

            if (InsertPackage(UserID, tempTable, _PBLOID, _PMLOID) == false)
                return false;
            else
            {
                return true;
            }
        }

        private double InsertProductMaster(string UserID, BasketData bkData)
        {
            bool ret = true;
            ProductMasterDAL oDAL = new ProductMasterDAL();

            oDAL.CODE = bkData.CODE;
            oDAL.NAME = bkData.NAME;
            oDAL.ENAME = bkData.ENAME;
            oDAL.UNIT = bkData.UNITBASKET;
            oDAL.PRODUCTGROUP = bkData.PRODUCTGROUP;
            oDAL.ACTIVE = bkData.ACTIVE;

            ret = oDAL.InsertCurrentData(UserID, null);
            if (ret == false)
            {
                _error = oDAL.ErrorMessage;
                return 0;
            }
            else
                return oDAL.LOID;
        }

        private double InsertProductBarcode(string UserID, BasketData bkData, double ProductMaster)
        {
             bool ret = true;
             ProductBarcodeDAL bDAL = new ProductBarcodeDAL();
             bDAL.PRODUCTMASTER = ProductMaster;
             bDAL.ABBNAME = bkData.ABBNAME;
             bDAL.BARCODE = bkData.BARCODE;
             bDAL.UNIT = bkData.UNITBASKET;
             bDAL.COST = bkData.COST;
             bDAL.PRICE = bkData.PRICE;
             bDAL.ISVAT = bkData.ISVAT;
             bDAL.ISDISCOUNT = bkData.ISDISCOUNT;
             bDAL.ISREFUND = bkData.ISREFUND;
             bDAL.ACTIVE = bkData.ACTIVE;
             bDAL.STDPRICE = bkData.PRICE;
             bDAL.MULTIPLY = 1;
             bDAL.ISEDIT = "Y";
             bDAL.ISDEFAULT = "Y";
             ret = bDAL.InsertCurrentData(UserID, null);
             if (ret == false)
             {                   
                 string sqlDelete = "DELETE FROM PRODUCTMASTER WHERE LOID = " + ProductMaster;
                 OracleDB.ExecNonQueryCmd(sqlDelete);

                 _error = bDAL.ErrorMessage;
                 return 0;
             }
             else
             {
               return bDAL.LOID;
             } 
        }

        private bool InsertPackage(string UserID, DataTable tempTable, double BarcodeLOID, double PMLOID)
        {
            bool ret = true;
            OracleDBObj objDB = new OracleDBObj();

            objDB.CreateConnection();
            objDB.CreateTransaction();

            if (tempTable.Rows.Count > 0)
            {
                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    PackageDAL pkDAL = new PackageDAL();
                    pkDAL.MAINPRODUCT = BarcodeLOID;
                    pkDAL.SUBPRODUCT = Convert.ToDouble(tempTable.Rows[i]["LOID"]);
                    pkDAL.QTY = Convert.ToDouble(tempTable.Rows[i]["QUANTITY"]);
                    pkDAL.PRICE = Convert.ToDouble(tempTable.Rows[i]["PRICE"]);
                    pkDAL.UNIT = Convert.ToDouble(tempTable.Rows[i]["PUNIT"]);
                    ret = pkDAL.InsertCurrentData(UserID, objDB.zTrans);
                    if (ret == false)
                    {
                        _error = pkDAL.ErrorMessage;
                        break;
                    }
                }
                if (ret == false)
                {
                    objDB.zTrans.Rollback();
                    objDB.CloseConnection();
                    string sqlDelete = "DELETE FROM PRODUCTBARCODE WHERE LOID = " + BarcodeLOID;
                    OracleDB.ExecNonQueryCmd(sqlDelete);
                    string sqlDelete1 = "DELETE FROM PRODUCTMASTER WHERE LOID = " + PMLOID;
                    OracleDB.ExecNonQueryCmd(sqlDelete1);
                    return ret;
                }
                else
                {
                    objDB.zTrans.Commit();
                    objDB.CloseConnection();
                    return ret;
                }
            }
            else
            {
                objDB.CloseConnection();
                return ret;
            }
        }

        private double GetLOID(string Barcode)
        {
            string sql = "SELECT LOID FROM PRODUCT WHERE BARCODE = '" + Barcode + "'";
            object LOID = OracleDB.ExecSingleCmd(sql);
            return Convert.ToDouble(LOID);
        }

        public bool UpdateData(string UserID, BasketData bkData, DataTable tempTable)
        {
            bool ret = true;

            ret = UpdateProductMaster(UserID, bkData);
            if (ret == false )
                return false;

            _PBLOID = UpdateProductBarcode(UserID, bkData);
            if (_PBLOID == 0)
                return false;

            if (UpdatePackage(UserID, tempTable, _PBLOID) == false)
                return false;
            else
            {
                return true;
            }
            //bool ret = true;
            //ProductDAL oDAL = new ProductDAL();
            //OracleDBObj objDB = new OracleDBObj();

            //objDB.CreateConnection();
            //objDB.CreateTransaction();

            //try
            //{
            //    oDAL.GetDataByLOID(bkData.LOID, null);
            //    oDAL.CODE = bkData.CODE;
            //    oDAL.NAME = bkData.NAME;
            //    oDAL.ABBNAME = bkData.ABBNAME;
            //    oDAL.BARCODE = bkData.BARCODE;
            //    oDAL.UNIT = bkData.UNITBASKET;
            //    oDAL.PRODUCTGROUP = bkData.PRODUCTGROUP;
            //    oDAL.COST = bkData.COST;
            //    oDAL.PRICE = bkData.PRICE;
            //    oDAL.STDPRICE = bkData.STDPRICE;
            //    oDAL.ISVAT = bkData.ISVAT;
            //    oDAL.ISDISCOUNT = bkData.ISDISCOUNT;
            //    oDAL.ISEDIT = bkData.ISEDITPRICE;
            //    oDAL.ISREFUND = bkData.ISREFUND;
            //    oDAL.REMARK = bkData.REMARK;
            //    oDAL.ACTIVE = "1";

            //    ret = oDAL.UpdateCurrentData(UserID, objDB.zTrans);

            //    if (ret == true)
            //    {
            //        if (tempTable.Rows.Count > 0)
            //        {
            //            if (UpdatePackage(UserID, tempTable, objDB, oDAL.LOID) == true)
            //            {
            //                objDB.zTrans.Commit();
            //                ret = true;
            //            }
            //            else
            //            {
            //                objDB.zTrans.Rollback();
            //                ret = false;
            //            }
            //        }
            //        else
            //        {
            //            objDB.zTrans.Commit();
            //            ret = true;
            //        }
            //    }
            //    else
            //    {
            //        _error = oDAL.ErrorMessage;
            //        objDB.zTrans.Rollback();
            //        ret = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _error = ex.Message; ;
            //    objDB.zTrans.Rollback();
            //    ret = false;
            //}

            //objDB.CloseConnection();
            //return ret;
        }


        private bool UpdateProductMaster(string UserID, BasketData bkData)
        {
            bool ret = true;
            ProductMasterDAL oDAL = new ProductMasterDAL();

            oDAL.GetDataByLOID(bkData.PRODUCTMASTER, null);
            oDAL.CODE = bkData.CODE;
            oDAL.NAME = bkData.NAME;
            oDAL.ENAME = bkData.ENAME;
            oDAL.UNIT = bkData.UNITBASKET;
            oDAL.PRODUCTGROUP = bkData.PRODUCTGROUP;
            oDAL.ACTIVE = bkData.ACTIVE;

            ret = oDAL.UpdateCurrentData(UserID, null);
            if (ret == false)
            {
                _error = oDAL.ErrorMessage;
                return ret;
            }
            else
                return ret;
        }

        private double UpdateProductBarcode(string UserID, BasketData bkData )
        {
            bool ret = true;
            ProductBarcodeDAL bDAL = new ProductBarcodeDAL();
            bDAL.GetDataByLOID(bkData.LOID, null);
            bDAL.ABBNAME = bkData.ABBNAME;
            bDAL.BARCODE = bkData.BARCODE;
            bDAL.UNIT = bkData.UNITBASKET;
            bDAL.COST = bkData.COST;
            bDAL.PRICE = bkData.PRICE;
            bDAL.ISVAT = bkData.ISVAT;
            bDAL.ISDISCOUNT = bkData.ISDISCOUNT;
            bDAL.ISREFUND = bkData.ISREFUND;
            bDAL.ACTIVE = bkData.ACTIVE;
            bDAL.STDPRICE = bkData.PRICE;
            bDAL.MULTIPLY = 1;

            ret = bDAL.UpdateCurrentData(UserID, null);
            if (ret == false)
            {
                _error = bDAL.ErrorMessage;
                return 0;
            }
            else
            {
                return bDAL.LOID;
            }
        }

        private bool UpdatePackage(string UserID, DataTable tempTable, double ProductBarcodeLOID)
        {
            bool ret = true;
            OracleDBObj objDB = new OracleDBObj();

            objDB.CreateConnection();
            objDB.CreateTransaction();
            
            string sqlDelete = "DELETE FROM PACKAGE WHERE MAINPRODUCT = " + ProductBarcodeLOID;
            OracleDB.ExecNonQueryCmd(sqlDelete, objDB.zTrans);

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                PackageDAL pkDAL = new PackageDAL();
                pkDAL.MAINPRODUCT = ProductBarcodeLOID;
                pkDAL.SUBPRODUCT = Convert.ToDouble(tempTable.Rows[i]["LOID"]);
                pkDAL.QTY = Convert.ToDouble(tempTable.Rows[i]["QUANTITY"]);
                pkDAL.PRICE = Convert.ToDouble(tempTable.Rows[i]["PRICE"]);
                pkDAL.UNIT = Convert.ToDouble(tempTable.Rows[i]["PUNIT"]);

                ret = pkDAL.InsertCurrentData(UserID, objDB.zTrans);
                if (ret == false)
                {
                    _error = pkDAL.ErrorMessage;
                    break;
                }
            }

            if (ret == false)
            {
                objDB.zTrans.Rollback();
                objDB.CloseConnection();
                return ret;
            }
            else
            {
                objDB.zTrans.Commit();
                objDB.CloseConnection();
                return ret;
            }
        }

        public bool CheckCode(double loid, string code)
        {
            string sql = "SELECT * FROM PRODUCT WHERE CODE = '" + code + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckName(double loid, string name)
        {
            string sql = "SELECT * FROM PRODUCT WHERE NAME = '" + name + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool CheckBarcode(double loid, string barcode)
        {
            string sql = "SELECT * FROM PRODUCT WHERE BARCODE = '" + barcode + "' AND LOID <> " + loid + " ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }
    }
}
