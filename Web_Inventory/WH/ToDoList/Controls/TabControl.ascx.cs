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
using ABB.Data;

public partial class WH_ToDoList_Controls_TabControl : System.Web.UI.UserControl
{

    private string normalL = Constz.ImageFolder + "pMenuNL.PNG";
    private string normalR = Constz.ImageFolder + "pMenuNR.PNG";
    private string selectL = Constz.ImageFolder + "pMenuSL.PNG";
    private string selectR = Constz.ImageFolder + "pMenuSR.PNG";

    public delegate void TabChangeEvent(object sender, EventArgs e);
    public event TabChangeEvent SelectedChange ;

    public string SelectedTab
    {
        get { return (this.lblTab.Text == "" ? "0" : this.lblTab.Text); }
        set { this.lblTab.Text = value; SetImage(); }
    }

    private void SetImage()
    {
        this.imgBD1L.ImageUrl = normalL;
        this.imgBD1R.ImageUrl = normalR;
        this.imgBD2L.ImageUrl = normalL;
        this.imgBD2R.ImageUrl = normalR;
        this.imgBD3L.ImageUrl = normalL;
        this.imgBD3R.ImageUrl = normalR;
        this.imgBD4L.ImageUrl = normalL;
        this.imgBD4R.ImageUrl = normalR;

        this.btnMinimumStock.Visible = false;
        this.lblMinimumStock.Visible = true;

        this.btnStockIn.Visible = true;
        this.lblStockIn.Visible = false;

        this.btnStockOut.Visible = true;
        this.lblStockOut.Visible = false;

        this.btnExpire.Visible = true;
        this.lblExpire.Visible = false;

        switch (SelectedTab)
        {
            case Constz.ToDoListTab.Inventory.WH.StockIn.Index :
                this.imgBD2L.ImageUrl = selectL;
                this.imgBD2R.ImageUrl = selectR;

                this.btnMinimumStock.Visible = true;
                this.lblMinimumStock.Visible = false;

                this.btnStockIn.Visible = false;
                this.lblStockIn.Visible = true;

                this.btnStockOut.Visible = true;
                this.lblStockOut.Visible = false;

                this.btnExpire.Visible = true;
                this.lblExpire.Visible = false;

                break;

            case Constz.ToDoListTab.Inventory.WH.StockOut.Index:
                this.imgBD3L.ImageUrl = selectL;
                this.imgBD3R.ImageUrl = selectR;

                this.btnMinimumStock.Visible = true;
                this.lblMinimumStock.Visible = false;

                this.btnStockIn.Visible = true;
                this.lblStockIn.Visible = false;

                this.btnStockOut.Visible = false;
                this.lblStockOut.Visible = true;

                this.btnExpire.Visible = true;
                this.lblExpire.Visible = false;

                break;

            case Constz.ToDoListTab.Inventory.WH.Expire.Index:
                this.imgBD4L.ImageUrl = selectL;
                this.imgBD4R.ImageUrl = selectR;

                this.btnMinimumStock.Visible = true;
                this.lblMinimumStock.Visible = false;

                this.btnStockIn.Visible = true;
                this.lblStockIn.Visible = false;

                this.btnStockOut.Visible = true;
                this.lblStockOut.Visible = false;

                this.btnExpire.Visible = false;
                this.lblExpire.Visible = true;

                break;

            default :
                this.imgBD1L.ImageUrl = selectL;
                this.imgBD1R.ImageUrl = selectR;
                break;

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnMinimumStock.Text = Constz.ToDoListTab.Inventory.WH.MinimumStock.Name;
            this.lblMinimumStock.Text = Constz.ToDoListTab.Inventory.WH.MinimumStock.Name;

            this.btnStockIn.Text = Constz.ToDoListTab.Inventory.WH.StockIn.Name;
            this.lblStockIn.Text = Constz.ToDoListTab.Inventory.WH.StockIn.Name;

            this.btnStockOut.Text = Constz.ToDoListTab.Inventory.WH.StockOut.Name;
            this.lblStockOut.Text = Constz.ToDoListTab.Inventory.WH.StockOut.Name;

            this.btnExpire.Text = Constz.ToDoListTab.Inventory.WH.Expire.Name;
            this.lblExpire.Text = Constz.ToDoListTab.Inventory.WH.Expire.Name;

            SetImage();
        }
    }

    protected void btnMinimumStock_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ToDoListTab.Inventory.WH.MinimumStock.Index;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnStockIn_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ToDoListTab.Inventory.WH.StockIn.Index;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnStockOut_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ToDoListTab.Inventory.WH.StockOut.Index;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnExpire_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ToDoListTab.Inventory.WH.Expire.Index;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

}
