using System;
using System.Collections.Generic;
using System.Text;
using System.Collections ;
using ABB.DAL.Production;
using ABB.Data.Production;
using System.Data;
using System.Data.OracleClient;
using ABB.DAL;

namespace ABB.Flow.Production
{
    public class BomFlow
    {
        string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        private BomSearchDAL _Search;
        private BomDAL _bom;
        private ProcessDAL _process;
        private ProductBarcodeDAL _barcode;

        public BomSearchDAL BomSearchItem
        {
            get { if (_Search == null) { _Search = new BomSearchDAL(); } return _Search; }
        }

        public BomDAL BomItem
        {
            get { if (_bom == null) { _bom = new BomDAL(); } return _bom; }
        }

        public ProcessDAL ProcessItem
        {
            get { if (_process == null) { _process = new ProcessDAL(); } return _process; }
        }

        private ProductBarcodeDAL BarcodeDAL
        {
            get { if (_barcode == null ) { _barcode = new ProductBarcodeDAL(); } return _barcode; }
        }

        public DataTable GetBomProductList(BomSearchData data)
        {
            DataTable dt = BomSearchItem.GetBomProductList(data);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["RANK"] = i;
                i += 1;
            }
            return dt;
        }

        public bool DeleteBomData(ArrayList arr)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arr.Count; ++i)
                {
                    double product = Convert.ToDouble(arr[i]);
                    if (BarcodeDAL.GetDataByLOID(product, obj.zTrans))
                    {
                        ret = BomItem.DeleteDataByMainProduct(product, obj.zTrans);
                        if (!ret) throw new ApplicationException(BomItem.ErrorMessage);

                        if (ProcessItem.GetDataList("WHERE PRODUCT = " + BarcodeDAL.PRODUCTMASTER.ToString(), obj.zTrans).Rows.Count == 1)
                        {
                            ret = ProcessItem.DeleteDataByProduct(BarcodeDAL.PRODUCTMASTER, obj.zTrans);
                            if (!ret) throw new ApplicationException(ProcessItem.ErrorMessage);
                        }
                    }
                }
                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                obj.zTrans.Rollback();
                obj.CloseConnection();
            }
            return ret;
        }

        public BomSearchData GetBomData(double productBarcode)
        {
            BomSearchData data = new BomSearchData();
            DataTable dt = BomSearchItem.GetBomProductData(productBarcode);
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                data.ACTIVE = dRow["ACTIVE"].ToString();
                data.BARCODE = dRow["BARCODE"].ToString();
                data.MAINPRODUCT = Convert.ToDouble(dRow["MAINPRODUCT"]);
                data.PROCESS = dRow["PROCESS"].ToString();
                data.PRODUCTGROUP = Convert.ToDouble(dRow["PRODUCTGROUP"]);
                data.PRODUCTTYPE = Convert.ToDouble(dRow["PRODUCTTYPE"]);
                data.RADIATION = dRow["RADIATION"].ToString();
            }
            return data;
        }

        public BomProductData GetProductData(double productBarcode, string barcode)
        {
            BomProductData data = new BomProductData();
            DataTable dt = BomSearchItem.GetProductData(productBarcode, barcode);
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                if (!Convert.IsDBNull(dRow["BARCODE"])) data.BARCODE = dRow["BARCODE"].ToString();
                if (!Convert.IsDBNull(dRow["LOID"])) data.LOID = Convert.ToDouble(dRow["LOID"]);
                if (!Convert.IsDBNull(dRow["LOTSIZE"])) data.LOTSIZE = Convert.ToDouble(dRow["LOTSIZE"]);
                if (!Convert.IsDBNull(dRow["PRODUCTTYPENAME"])) data.PRODUCTTYPENAME = dRow["PRODUCTTYPENAME"].ToString();
                if (!Convert.IsDBNull(dRow["PRODUCTGROUP"])) data.PRODUCTGROUP = Convert.ToDouble(dRow["PRODUCTGROUP"]);
                if (!Convert.IsDBNull(dRow["PRODUCTTYPE"])) data.PRODUCTTYPE = Convert.ToDouble(dRow["PRODUCTTYPE"]);
                if (!Convert.IsDBNull(dRow["UNIT"])) data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                if (!Convert.IsDBNull(dRow["UNITNAME"])) data.UNITNAME = dRow["UNITNAME"].ToString();
            }
            return data;
        }

        public DataTable GetBomList(double productBarcode)
        {
            DataTable dt = BomSearchItem.GetBomList(productBarcode);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["RANK"] = i;
                i += 1;
            }
            return dt;
        }

        public DataTable GetBomListBlank()
        {
            DataTable dt = BomSearchItem.GetBomList(0);
            DataRow dRow = dt.NewRow();
            dt.Rows.Add(dRow);
            return dt;
        }

        private bool VerifyData(BomSearchData data)
        {
            bool ret = true;
            if (data.MAINPRODUCT == 0)
            {
                ret = false;
                _error = "กรุณาเลือกสินค้า";
            }
            else if (data.PROCESS.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุวิธีเตรียม";
            }
            else if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุวัตุดิบที่ต้องการใช้";
            }
            else if (data.OLDMAINPRODUCT != data.MAINPRODUCT)
            {
                if (BomItem.GetDataList("WHERE MAINPRODUCT = " + data.MAINPRODUCT.ToString(), null).Rows.Count > 0)
                {
                    ret = false;
                    _error = "ชื่อสินค้าที่เลือกซ้ำ";
                }
            }
            return ret;
        }

        public bool UpdateData(string userID, BomSearchData data)
        {
            bool ret = true;
            if (VerifyData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateTransaction();
                try
                {
                    BomItem.DeleteDataByMainProduct(data.OLDMAINPRODUCT, obj.zTrans);
                    BomItem.DeleteDataByMainProduct(data.MAINPRODUCT, obj.zTrans);
                    BarcodeDAL.GetDataByLOID(data.MAINPRODUCT, obj.zTrans);
                    ProcessItem.GetDataByProduct(BarcodeDAL.PRODUCTMASTER, obj.zTrans);
                    ProcessItem.ACTIVE = Data.Constz.ActiveStatus.Active;
                    ProcessItem.PROCESS = data.PROCESS;
                    ProcessItem.RADIATION = data.RADIATION;
                    ProcessItem.PRODUCT = BarcodeDAL.PRODUCTMASTER;
                    if (ProcessItem.OnDB)
                        ret = ProcessItem.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = ProcessItem.InsertCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(ProcessItem.ErrorMessage);

                    for (int i = 0; i < data.ITEM.Count; ++i)
                    {
                        BomData dataItem = (BomData)data.ITEM[i];
                        BomItem.OnDB = false;
                        BomItem.ACTIVE = data.ACTIVE;
                        BomItem.MAINPRODUCT = data.MAINPRODUCT;
                        BomItem.MASTER = dataItem.MASTER;
                        BomItem.MATERIAL = dataItem.MATERIAL;
                        BomItem.PROCESS = ProcessItem.LOID;
                        BomItem.RADIATION = data.RADIATION;
                        BomItem.UNIT = dataItem.UNIT;
                        ret = BomItem.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(BomItem.ErrorMessage);
                    }

                    obj.zTrans.Commit();
                    obj.CloseConnection();
                }
                catch (Exception ex)
                {
                    ret = false;
                    _error = ex.Message;
                    obj.zTrans.Rollback();
                    obj.CloseConnection();
                }
            }
            else
            {
                ret = false;
            }
            return ret;
        }

    }
}
