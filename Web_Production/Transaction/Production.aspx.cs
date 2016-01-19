using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ABB.Flow.Production;
using ABB.Global;
using ABB.Data;
using ABB.Data.Production;
using ABB.DAL.Production;
using ABB.DAL;

public partial class Transaction_Production : System.Web.UI.Page
{
    private PDProductFlow _flow;
    public PDProductFlow FlowObj
    {
        get { if (_flow == null) _flow = new PDProductFlow(); return _flow; }
    }


    private DataTable TempGetMaterial = null;
    private DataTable TempGetMaterialLost = null;
    private DataTable TempGetPackageLost = null;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ControlUtil.SetDblTextBox(txtBatchSize);
    }

    public string GetPdLoid
    {
        get { return (this.txtPdLoid.Text == "" ? "0" : this.txtPdLoid.Text); }
    }

    public string GetPdpLoid
    {
        get { return (this.txtPdpLoid.Text == "" ? "0" : this.txtPdpLoid.Text); }
    }

    private void SetProduct(DropDownList cmbProduct)
    {
        DataTable dt = FlowObj.GetProduct(this.cmbType.SelectedValue);
        cmbProduct.Items.Clear();
        cmbProduct.DataSource = dt;
        cmbProduct.DataTextField = "NAME";
        cmbProduct.DataValueField = "LOID";
        cmbProduct.DataBind();
        cmbProduct.Items.Insert(0, new ListItem("เลือก", ""));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            SetProduct(this.cmbProduct);
            //ComboSource.BuildCombo(this.cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");
            ComboSource.BuildCombo(cmbUnitBZ, "UNIT", "NAME", "LOID", "NAME", "ACTIVE =  '" + Constz.ActiveStatus.Active + "'");
            ComboSource.BuildCombo(cmbUnitPZ, "UNIT", "NAME", "LOID", "NAME", "ACTIVE =  '" + Constz.ActiveStatus.Active + "'");

            if (Request.QueryString["PDLOID"] != null && Request.QueryString["PDPLOID"] != null)
            {
                txtPdLoid.Text = Request.QueryString["PDLOID"].ToString();
                txtPdpLoid.Text = Request.QueryString["PDPLOID"].ToString();
                LoadData(Request.QueryString["PDLOID"].ToString(), Request.QueryString["PDPLOID"].ToString());
                SetControl();
            }
            else if (Request.QueryString["RQLOID"] != null && Request.QueryString["PDLOID"] != null)
            {
                txtPdLoid.Text = Request.QueryString["PDLOID"].ToString();
                LoadDataByToDoList(Request.QueryString["RQLOID"].ToString(), Request.QueryString["PDLOID"].ToString());
                //txtRqCode.Enabled = false;
                btnSelect.Enabled = false;
                ctlTab.Visible = false;
                dpMfgDate.DateValue = DateTime.Today;
            }
            else
            {
                txtRqCode.Enabled = true;
                btnSelect.Enabled = true;
                ctlTab.Visible = false;
                dpMfgDate.DateValue = DateTime.Today;
            }
        }
        string reqcode = txtRqCode.ClientID;
        string ReqDate = txtReqDate.ClientID;
        string ReqiLoid = txtRqiLoid.ClientID;
        string Qty = txtQty.ClientID;
        string Pdloid = cmbProduct.ClientID;
        //string script = "";
        //script += "var a; a = OpenNewModalDialog('" + Constz.HomeFolder + "Search/Request.aspx?reqcode=" + reqcode + "&reqdate=" + ReqDate + "&reqiloid=" + ReqiLoid + "&qty=" + Qty + "&pdloid=" + Pdloid + "', '650', '550' ,'yes'); ";
        string script = "";
        script += "var a; a = OpenNewModalDialog('" + Constz.HomeFolder + "Search/Request.aspx?reqiloid=" + ReqiLoid + "', '650', '550' ,'yes'); ";
        script += "if (a == 'undefined') { return false; }";// document.getElementById('" + this.txtLotNo.ClientID + "').value || '' == document.getElementById('" + this.txtInvoicecode.ClientID + "').value) ";
        script += " else { document.getElementById('" + this.txtRqiLoid.ClientID + "').value = a;  }";
        this.btnSelect.OnClientClick = script;
        //string report = FlowObj.GetReport(Convert.ToDouble(cmbProduct.SelectedValue == "" ? "0" : cmbProduct.SelectedValue));
        ToolbarControl1.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.Productionherb01, Convert.ToDouble(this.txtPdpLoid.Text)) + " return false;";

    }

    private void LoadDataByToDoList(string rqloid, string pdloid)
    {
        
        DataTable dt = new DataTable();
        dt = PDProductFlow.GetRequisitionItemDetailByToDoList(rqloid, pdloid);
        if (dt.Rows.Count > 0)
        {
            txtRqiLoid.Text = dt.Rows[0]["RQILOID"].ToString();
            txtRqCode.Text = dt.Rows[0]["RQCODE"].ToString();
            txtReqDate.Text = dt.Rows[0]["REQDATE"].ToString();


            txtBatchSize.Text = dt.Rows[0]["QTY"].ToString();
            cmbUnitBZ.SelectedIndex = this.cmbUnitPZ.Items.IndexOf(this.cmbUnitPZ.Items.FindByValue(dt.Rows[0]["RQIUNIT"].ToString()));
            if (dt.Rows[0]["PACKSIZEUNIT"] != null)
            {
                cmbUnitPZ.SelectedIndex = this.cmbUnitPZ.Items.IndexOf(this.cmbUnitPZ.Items.FindByValue(dt.Rows[0]["PACKSIZEUNIT"].ToString()));
            }

            if (dt.Rows[0]["PRODUCT"].ToString() != "")
            {
                cmbProduct.SelectedValue = dt.Rows[0]["PRODUCT"].ToString();
                GetProductDetail(); 
            }
            else
                Appz.ClientAlert(Page, "ไม่พบ BOM ของ " + dt.Rows[0]["NAME"].ToString());

         }

    }
    private void SetControl()
    {
        if (txtPRODSTATUS.Text.Trim() == "AP" || txtPOSTATUS.Text.Trim() == "AP")
        {
            txtLotNo.CssClass = "zTextboxR-View";
            txtLotNo.ReadOnly = true;
            dpMfgDate.Enabled = false;
            cmbProduct.Enabled = false;
            cmbType.Enabled = false;
            txtBatchSize.CssClass = "zTextboxR-View";
            txtBatchSize.ReadOnly = true;
            cmbUnitBZ.Enabled = false;
            btnSelect.Enabled = false;
            txtRemark.CssClass = "zTextBox-View";
            txtRemark.ReadOnly = true;
        }
        else if (txtPRODSTATUS.Text.Trim() == "WA" || txtPOSTATUS.Text.Trim() == "WA")
        {
            txtLotNo.CssClass = "zTextboxR-View";
            txtLotNo.ReadOnly = true;
            dpMfgDate.Enabled = false;
            cmbProduct.Enabled = false;
            cmbProduct.Enabled = true;
            txtBatchSize.CssClass = "zTextbox";
            //txtBatchSize.ReadOnly = true;
            cmbUnitBZ.Enabled = false;
            btnSelect.Enabled = false;
        }
        else
        {
            txtLotNo.CssClass = "zTextbox-View";
            txtLotNo.ReadOnly = true;
            dpMfgDate.Enabled = false;
            cmbProduct.Enabled = false;
            txtBatchSize.CssClass = "zTextboxR-View";
            txtBatchSize.ReadOnly = true;
            cmbUnitBZ.Enabled = false;
            btnSelect.Enabled = false;
            txtRemark.CssClass = "zTextBox-View";
            txtRemark.ReadOnly = true;
        }
    }
    private void LoadData(string PdLoid, string pdpLoid)
    {
        DataTable dt = new DataTable();
        dt = PDProductFlow.GetPdProductData(PdLoid, pdpLoid);
        if (dt.Rows.Count > 0)
        {
            txtLotNo.Text = dt.Rows[0]["LOTNO"].ToString();
            ComboSource.BuildCombo(this.cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND LOID = " + dt.Rows[0]["PRODUCT"].ToString() + "");
            txtPdUnit.Text = dt.Rows[0]["PDUNIT"].ToString();
            txtPackSize.Text = dt.Rows[0]["PACKSIZE"].ToString();
            cmbUnitPZ.SelectedIndex = this.cmbUnitPZ.Items.IndexOf(this.cmbUnitPZ.Items.FindByValue(dt.Rows[0]["UNITPZ"].ToString()));
            cmbUnitBZ.SelectedIndex = this.cmbUnitBZ.Items.IndexOf(this.cmbUnitBZ.Items.FindByValue(dt.Rows[0]["UNITBZ"].ToString()));
            txtBatchSize.Text = Convert.ToDouble(dt.Rows[0]["BATCHSIZE"]).ToString(Constz.DblFormat);
            txtRemark.Text = dt.Rows[0]["REMARK"].ToString();
            dpMfgDate.DateValue = Convert.ToDateTime(dt.Rows[0]["MFGDATE"]);
            txtPoLoid.Text = dt.Rows[0]["POLOID"].ToString();
            txtRqCode.Text = dt.Rows[0]["RQCODE"].ToString();
            txtPRODSTATUS.Text = dt.Rows[0]["PRODSTATUS"].ToString();
            txtPOSTATUS.Text = dt.Rows[0]["POSTATUS"].ToString();
            if (dt.Rows[0]["PRODUCETYPE"].ToString() != "" || Convert.IsDBNull(dt.Rows[0]["PRODUCETYPE"]) == false) 
                cmbType.SelectedValue = dt.Rows[0]["PRODUCETYPE"].ToString();
            cmbWarehouse.SelectedValue = dt.Rows[0]["TOWAREHOUSE"].ToString();
            if (!Convert.IsDBNull(dt.Rows[0]["REQDATE"]))
                txtReqDate.Text = Convert.ToDateTime(dt.Rows[0]["REQDATE"]).ToString(Constz.DateFormat);
            else
                txtReqDate.Text = "";
        }
        this.ctlTab.SetPackageLotPrintScript(Convert.ToDouble(pdpLoid=="" ? "0" : pdpLoid));
        this.ctlTab.SetMaterialLotPrintScript(Convert.ToDouble(pdpLoid == "" ? "0" : pdpLoid));
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Session["tempMaterial"] = null;
        Session["tempMaterialLost"] = null;
        Session["tempPgLost"] = null;
        Response.Redirect("ProductionSearch.aspx");
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (cmbProduct.SelectedValue == "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุสินค้า");
            return;
        }

        else if (cmbUnitBZ.SelectedValue == "")
        {
            Appz.ClientAlert(Page, "กรุณาระบุหน่วยปริมาณการผลิต");
            return;
        }

        else if (txtPackSize.Text == "" || Convert.ToDouble(txtPackSize.Text.Trim()) == 0)
        {
            Appz.ClientAlert(Page, "ไม่พบข้อมูลขนาดบรรจุ");
            return;
        }

        else if (txtBatchSize.Text == "" || Convert.ToDouble(txtBatchSize.Text.Trim()) == 0)
        {
            Appz.ClientAlert(Page, "กรุณาระบุปริมาณการผลิต");
            return;
        }

        else if (this.txtPdpLoid.Text.Trim() == "0")
        {

            
            SetStdQty(); //คำนวณผลผลิตตามทฤษฎี

            InsertPdProduct(); //Insert PdProduct,PdOrder
            InsertMaterialItem(); //Insert MaterialItem
            SetRequest();  //อัพเดท Requisition

            //set control
            ctlTab.Visible = true;
            cmbUnitBZ.Enabled = false;
            //txtBatchSize.Enabled = false;
            cmbProduct.Enabled = false;
           // txtRqCode.Enabled = false;
            btnSelect.Enabled = false ;
            dpMfgDate.Enabled = false;
            cmbType.Enabled = false;
            //คิวรี่ข้อมูลในแต่ละ tab ใหม่
            SetReload(Convert.ToDouble(cmbProduct.SelectedValue), Convert.ToDouble(txtPdpLoid.Text.Trim()));
            LoadData(cmbProduct.SelectedValue, txtPdpLoid.Text.Trim());
        }
        else
        {
            SetStdQty(); //คำนวณผลผลิตตามทฤษฎี
            UpdateData();
            SetRequest();
            SetReload(Convert.ToDouble(cmbProduct.SelectedValue), Convert.ToDouble(txtPdpLoid.Text.Trim()));
        }
    }

    private void UpdateData()
    {
        bool retMaterial = true;
        bool retMaterialLost = true;
        bool retPackageLost = true;
        bool retPDProduct = true;
        bool retPo = true;
        ReqMaterialData RmData = new ReqMaterialData();
        PDProductFlow PpFlow = new PDProductFlow();

        //### update Material
        if (Session["tempMaterial"] != null)
            TempGetMaterial = (DataTable)Session["tempMaterial"];

        retMaterial = PpFlow.UpdateMaterial(Authz.CurrentUserInfo.UserID, txtPdpLoid.Text.Trim(), TempGetMaterial);

        //### update MaterialLost
        if (Session["tempMaterialLost"] != null)
            TempGetMaterialLost = (DataTable)Session["tempMaterialLost"];
        retMaterialLost = PpFlow.UpdateMaterialLost(Authz.CurrentUserInfo.UserID, txtPdpLoid.Text.Trim(), TempGetMaterialLost);

        //### update PackageLost
        if (Session["tempPgLost"] != null)
            TempGetPackageLost = (DataTable)Session["tempPgLost"];
        retPackageLost = PpFlow.UpdatePackageLost(Authz.CurrentUserInfo.UserID, txtPdpLoid.Text.Trim(), TempGetPackageLost);


        //### update ProductFill
        PDProductData PpData = new PDProductData();
        PpData.LOID = Convert.ToDouble(txtPdpLoid.Text.Trim());
        PpData.PACKING = ctlTab.GetPacking;
        PpData.PACKAGE = ctlTab.GetPackage;
        PpData.PDQTY = ctlTab.GetPdQty;
        PpData.EXPDATE = Convert.ToDateTime(ctlTab.GetExpDate);
        PpData.YIELD = ctlTab.GetYield;
        PpData.LOSTQTY = ctlTab.GetLost;
        PpData.STDQTY = Convert.ToDouble(txtPdpStdqty.Text.Trim());

        //### update Radiate
        PpData.RADIATEDATE = Convert.ToDateTime(ctlTab.GetRadiateDate);
        PpData.RADIATEQTY = ctlTab.GetRadiateQty;
        PpData.RADIATEUNIT = ctlTab.GetRadiateUnit;
        PpData.RADIATEREMARK = ctlTab.GetRadiateRemark;

        //### update RadiateReturn
        PpData.RADIATERETDATE = Convert.ToDateTime(ctlTab.GetRadiateRetDate);
        PpData.RADIATERETQTY = Convert.ToDouble(ctlTab.GetRadiateRetQty);
        PpData.RADIATERETUNIT = Convert.ToDouble(ctlTab.GetRadiateRetUnit);
        PpData.RADIATERETREMARK = ctlTab.GetRadiateRetRemark;
        
        //### update StockInDetail
        PpData.QUARANTINEDATE= Convert.ToDateTime(ctlTab.GetQuarantineDate);
        PpData.QUARANTINEQTY = Convert.ToDouble(ctlTab.GetQuarantineQty);
        PpData.QUARANTINEUNIT = Convert.ToDouble(ctlTab.GetQuarantineUnit);
        PpData.QUARANTINEREMARK = ctlTab.GetQuarantineRemark;

        //### update SendQC
        //if (ctlTab.GetSendQCDate == "1/1/0544 0:00:00")
        //    Appz.ClientAlert(Page, "กรุณาระบุวันที่ส่งวิเคราะห์");
        //else
            PpData.SENDQCDATE = Convert.ToDateTime(ctlTab.GetSendQCDate);

        //### update StockOutDetail
        PpData.SENDFGDATE = Convert.ToDateTime(ctlTab.GetSendFGDate);
        PpData.SENDFGQTY = Convert.ToDouble(ctlTab.GetSendFGQty);
        PpData.SENDFGREMARK = ctlTab.GetSendFGRemark.ToString();

        //## Production
        PpData.LOTNO = txtLotNo.Text.Trim();
        PpData.MFGDATE = dpMfgDate.DateValue;
        PpData.BATCHSIZE = Convert.ToDouble(txtBatchSize.Text.Trim());
        PpData.BATCHSIZEUNIT = Convert.ToDouble(cmbUnitBZ.SelectedValue);
        PpData.PRODUCTTYPE = cmbType.SelectedValue;
        PpData.TOWAREHOUSE = Convert.ToDouble(cmbWarehouse.SelectedValue);

        retPDProduct = PDProductFlow.UpdatePDPrdouct(Authz.CurrentUserInfo.UserID.ToString(), PpData);

        retPo = UpdatePdOrder();

        if (retMaterial == false || retPDProduct == false || retPackageLost == false || retMaterialLost ==false || retPo == false  )
            Appz.ClientAlert(Page, PpFlow.ErrorMessage);
        else
        {
            LoadData(cmbProduct.SelectedValue.ToString(), txtPdpLoid.Text.Trim());
            Appz.ClientAlert(Page, "ทำการแก้ไขข้อมูลบันทึกการผลิตเรียบร้อย");
        }
    }

    private void InsertPdProduct()
    {
        PDProductData ppData = new PDProductData();
        DataTable dt = new DataTable();
        double POLoid = 0;
        string reftable = (txtRqiLoid.Text.Trim() == "" ? "" : "REQUISITIONITEM");
        double  refloid = (txtRqiLoid.Text.Trim() == "" ? 0 : Convert.ToDouble(txtRqiLoid.Text.Trim()));
        POLoid = PDProductFlow.InsertPdOrder(Authz.CurrentUserInfo.UserID.ToString(),txtRemark.Text.Trim(),reftable,refloid );
        if (POLoid != 0)
        {
            String expdate = SetExpDate();
            ppData.MFGDATE = dpMfgDate.DateValue;
            ppData.PRODUCT = Convert.ToDouble(cmbProduct.SelectedValue);
            ppData.BATCHSIZE =Convert.ToDouble(txtBatchSize.Text.Trim());
            ppData.BATCHSIZEUNIT = Convert.ToDouble(cmbUnitBZ.SelectedValue);
            ppData.PDORDER = POLoid;
            ppData.RADIATEUNIT = Convert.ToDouble(txtPdUnit.Text.Trim()) ;
            ppData.STDQTY = Convert.ToDouble(txtPdpStdqty.Text.Trim());
            ppData.EXPDATE = Convert.ToDateTime(expdate);
            ppData.PRODUCTTYPE = cmbType.SelectedValue;
            ppData.TOWAREHOUSE = Convert.ToDouble(cmbWarehouse.SelectedValue);
            ppData.LOTNO = txtLotNo.Text;
            if (txtRqiLoid.Text.Trim() != "")
            {
                ppData.REFLOID = Convert.ToDouble(txtRqiLoid.Text.Trim());
                ppData.REFTABLE = "REQUISITIONITEM";
            }
            
            //set control ด้วยค่าที่ insert ใหม่
            dt = PDProductFlow.InsertPdProduct(Authz.CurrentUserInfo.UserID.ToString(), ppData);
            if (dt.Rows.Count > 0)
            {
                txtLotNo.Text = dt.Rows[0]["LOTNO"].ToString();
                dpMfgDate.DateValue = Convert.ToDateTime(dt.Rows[0]["MFGDATE"]);
                cmbProduct.SelectedValue = dt.Rows[0]["PRODUCT"].ToString();
                txtPackSize.Text = dt.Rows[0]["PACKSIZE"].ToString();
                cmbUnitPZ.SelectedIndex = this.cmbUnitPZ.Items.IndexOf(this.cmbUnitPZ.Items.FindByValue(dt.Rows[0]["UNITPZ"].ToString()));
                txtBatchSize.Text = dt.Rows[0]["BATCHSIZE"].ToString();
                cmbUnitBZ.SelectedIndex = this.cmbUnitBZ.Items.IndexOf(this.cmbUnitBZ.Items.FindByValue(dt.Rows[0]["BATCHSIZEUNIT"].ToString()));
                txtPdpLoid.Text = dt.Rows[0]["PDPLOID"].ToString();
                txtPoLoid.Text = dt.Rows[0]["POLOID"].ToString();
                txtRqCode.Text = dt.Rows[0]["RQCODE"].ToString();
                txtPdUnit.Text = dt.Rows[0]["PDUNIT"].ToString();
                cmbType.SelectedValue = dt.Rows[0]["PRODUCETYPE"].ToString();
                cmbWarehouse.SelectedValue = dt.Rows[0]["TOWAREHOUSE"].ToString();
                if (!Convert.IsDBNull(dt.Rows[0]["REQDATE"]))
                    txtReqDate.Text = Convert.ToDateTime(dt.Rows[0]["REQDATE"]).ToString(Constz.DateFormat);
                else
                    txtReqDate.Text = "";
            }
        }
        else
        {
            Appz.ClientAlert(Page, POLoid.ToString());
        }
    }

    private string  SetExpDate()
    {
        string str = "";
        str = dpMfgDate.DateValue.Day.ToString() + '/';
        str += dpMfgDate.DateValue.Month.ToString() + '/';
        str += Convert.ToString(dpMfgDate.DateValue.Year+543 + Convert.ToDouble(txtAge.Text.Trim()));
        return str;
    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbProduct.SelectedValue != null)
            GetProductDetail();
    }

    private void GetProductDetail()
    {
        DataTable dt = PDProductFlow.GetProductDetail(cmbProduct.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["PACKSIZE"] != null )
                txtPackSize.Text = dt.Rows[0]["PACKSIZE"].ToString();

            if (dt.Rows[0]["ULOID"].ToString() != "0")
                cmbUnitPZ.SelectedIndex = this.cmbUnitPZ.Items.IndexOf(this.cmbUnitPZ.Items.FindByValue(dt.Rows[0]["ULOID"].ToString()));

            txtPdUnit.Text = dt.Rows[0]["PDUNIT"].ToString();
            txtAge.Text = dt.Rows[0]["AGE"].ToString();
        }
    }

    private void SetReload(double PDLOID, double PDPLOID)
    {
        ctlTab.TabReLoad(PDLOID, PDPLOID);
        this.ctlTab.SetPackageLotPrintScript(PDPLOID);
        this.ctlTab.SetMaterialLotPrintScript(PDPLOID);
    }

    private void InsertMaterialItem()
    {
        bool ret = true;
        PDProductFlow PpFlow = new PDProductFlow();
        if (txtRqCode.Text.Trim() == "")
        {
            DataTable dt = PDProductFlow.CheckMaterialItem(Convert.ToDouble(cmbProduct.SelectedValue));
            string pp = txtPdpLoid.Text.Trim();
            ret = PpFlow.InsertMaterialItem(Authz.CurrentUserInfo.UserID.ToString(), dt, Convert.ToDouble(txtPdpLoid.Text.Trim()));
        }
        else
        {
            DataTable dt = PDProductFlow.CheckMaterialItemWithReq(Convert.ToDouble(cmbProduct.SelectedValue));
            string pp = txtPdpLoid.Text.Trim();
            ret = PpFlow.InsertMaterialItem(Authz.CurrentUserInfo.UserID.ToString(), dt, Convert.ToDouble(txtPdpLoid.Text.Trim()));
        }
    }

    private void SetStdQty()
    {
        double stdqty = 0;
        stdqty = PDProductFlow.ConvertUnit(cmbUnitBZ.SelectedValue.ToString(), cmbUnitPZ.SelectedValue.ToString(), txtBatchSize.Text.Trim(), txtPackSize.Text.Trim());
        txtPdpStdqty.Text = stdqty.ToString();
    }

    private bool UpdatePdOrder()
    {
        bool ret = true;
        PDOrderData poData = new PDOrderData();
        poData.REMARK = txtRemark.Text.Trim();
        ret = PDProductFlow.UpdatePdOrder(Authz.CurrentUserInfo.UserID.ToString(), poData, Convert.ToDouble(txtPoLoid.Text.Trim()));
        return ret;
    }

    private void  SetRequest()
    {
        if (txtRqiLoid.Text.Trim() != "" && txtQty.Text.Trim() != "")
        {
            bool ret = PDProductFlow.UpdateRequest(Authz.CurrentUserInfo.UserID.ToString(), Convert.ToDouble(txtPdpLoid.Text.Trim()), Convert.ToDouble(txtRqiLoid.Text.Trim()), Convert.ToDouble(txtQty.Text.Trim()));
            if (ret == true)
            {
                DataTable  dt = PDProductFlow.GetPdProduct(Convert.ToDouble(txtPdpLoid.Text.Trim()));
                if (dt.Rows.Count > 0)
                {
                    txtLotNo.Text = dt.Rows[0]["LOTNO"].ToString();
                    dpMfgDate.DateValue = Convert.ToDateTime(dt.Rows[0]["MFGDATE"]);
                    cmbProduct.SelectedValue = dt.Rows[0]["PRODUCT"].ToString();
                    txtPackSize.Text = dt.Rows[0]["PACKSIZE"].ToString();
                    cmbUnitPZ.SelectedIndex = this.cmbUnitPZ.Items.IndexOf(this.cmbUnitPZ.Items.FindByValue(dt.Rows[0]["UNITPZ"].ToString()));
                    txtBatchSize.Text = dt.Rows[0]["BATCHSIZE"].ToString();
                    cmbUnitBZ.SelectedIndex = this.cmbUnitBZ.Items.IndexOf(this.cmbUnitBZ.Items.FindByValue(dt.Rows[0]["BATCHSIZEUNIT"].ToString()));
                    txtPdpLoid.Text = dt.Rows[0]["PDPLOID"].ToString();
                    txtPoLoid.Text = dt.Rows[0]["POLOID"].ToString();
                    txtRqCode.Text = dt.Rows[0]["RQCODE"].ToString();
                    if (!Convert.IsDBNull(dt.Rows[0]["REQDATE"]))
                        txtReqDate.Text = Convert.ToDateTime(dt.Rows[0]["REQDATE"]).ToString(Constz.DateFormat);
                    else
                        txtReqDate.Text = "";
                }
                Appz.ClientAlert(Page, "บันทึกข้อมูลการผลิตเรียบร้อย");
            }
        }
    }

    //protected void btnSelect_Click(object sender, ImageClickEventArgs e)
    //{
    //    //string reqcode = txtRqCode.ClientID;
    //    //string ReqDate = txtReqDate.ClientID;
    //    //string ReqiLoid = txtRqiLoid.ClientID;
    //    //string Qty = txtQty.ClientID;
    //    //string Pdloid = cmbProduct.ClientID;
    //    //string script = "window.open('../Search/Request.aspx?reqcode=" + reqcode + "&reqdate=" + ReqDate + "&reqiloid=" + ReqiLoid + "&qty=" + Qty + "&pdloid=" + Pdloid + "', 'zReport', 'status=yes, toolbar=no, scrollbars=yes, menubar=no, width=700, height=550, resizable=yes'); return false;";
        
    //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Search", script, true);
            
    //        GetProductDetail(); 
    //    //}
    //    //else
    //    //{
    //        //DataTable Rqdt = PDProductFlow.GetRequistionData(txtRqCode.Text.Trim());
    //        //if (Rqdt.Rows.Count > 0)
    //        //{
    //        //    txtRqLoid.Text  = Rqdt.Rows[0]["LOID"].ToString();
    //        //    txtQty.Text  = Rqdt.Rows[0]["QTY"].ToString();
    //        //    txtRqCode.Text = Rqdt.Rows[0]["CODE"].ToString();
    //        //    if (!Convert.IsDBNull(Rqdt.Rows[0]["REQDATE"]))
    //        //        txtReqDate.Text = Convert.ToDateTime(Rqdt.Rows[0]["REQDATE"]).ToString(Constz.DateFormat);
    //        //    else
    //        //        txtReqDate.Text = "";
    //        //    cmbProduct.SelectedValue = Rqdt.Rows[0]["PDLOID"].ToString();
    //        //}
    //        //else
    //        //{
    //        //    Appz.ClientAlert(Page, "ไม่พบข้อมูล");
    //        //}
    //    //}
         
    //}

    protected void btnSelect_Click(object sender, ImageClickEventArgs e)
    {
        if (txtRqiLoid.Text.Trim() != "undefined")
        {
            DataTable dt = PDProductFlow.GetRequisitionItemDetail(txtRqiLoid.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtRqCode.Text = dt.Rows[0]["RQCODE"].ToString();
                txtReqDate.Text = dt.Rows[0]["REQDATE"].ToString();

                    if (dt.Rows[0]["PACKSIZEUNIT"] != null)
                    {
                        cmbUnitPZ.SelectedIndex = this.cmbUnitPZ.Items.IndexOf(this.cmbUnitPZ.Items.FindByValue(dt.Rows[0]["PACKSIZEUNIT"].ToString()));
                        cmbUnitBZ.SelectedIndex = this.cmbUnitPZ.Items.IndexOf(this.cmbUnitPZ.Items.FindByValue(dt.Rows[0]["PACKSIZEUNIT"].ToString()));
                    }
                txtBatchSize.Text = dt.Rows[0]["BATCHQTY"].ToString();
                    if (dt.Rows[0]["PRODUCT"].ToString() != "")
                    {
                        cmbProduct.SelectedValue = dt.Rows[0]["PRODUCT"].ToString();
                        cmbProduct.Enabled = false;
                        GetProductDetail();
                    }
                    else
                        Appz.ClientAlert(Page, "ไม่พบ BOM ของ " + dt.Rows[0]["NAME"].ToString());
                    
                cmbProduct.Enabled = false;
            }
            
        }   
    }

    protected void PrintClick(object sender, EventArgs e)
    {

    }

    protected void cmbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbType.SelectedValue == "FG")
            this.cmbWarehouse.SelectedValue = "1";
        else
            this.cmbWarehouse.SelectedValue = "2";

        SetProduct(this.cmbProduct);
    }
}
