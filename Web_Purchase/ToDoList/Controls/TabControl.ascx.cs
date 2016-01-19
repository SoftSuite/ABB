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

public partial class ToDoList_Controls_TabControl : System.Web.UI.UserControl
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

        this.btnProductPurchaseList.Visible = false;
        this.lblProductPurchaseList.Visible = true;

        this.btnProductReceiveList.Visible = true;
        this.lblProductReceiveList.Visible = false;

        switch (SelectedTab)
        {
            case Constz.ToDoListTab.Purchase.ProductPurchaseList.Index :
                this.imgBD1L.ImageUrl = selectL;
                this.imgBD1R.ImageUrl = selectR;
             
                this.btnProductPurchaseList.Visible = false;
                this.lblProductPurchaseList.Visible = true;

                this.btnProductReceiveList.Visible = true;
                this.lblProductReceiveList.Visible = false;

                break;

            case Constz.ToDoListTab.Purchase.ProductReceiveList.Index:
                this.imgBD2L.ImageUrl = selectL;
                this.imgBD2R.ImageUrl = selectR;

                this.btnProductPurchaseList.Visible = true;
                this.lblProductPurchaseList.Visible = false;

                this.btnProductReceiveList.Visible = false;
                this.lblProductReceiveList.Visible = true;

                break;

            default :
                break;

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnProductPurchaseList.Text = Constz.ToDoListTab.Purchase.ProductReceiveList.Name;
            this.lblProductPurchaseList.Text = Constz.ToDoListTab.Purchase.ProductReceiveList.Name;

            this.btnProductReceiveList.Text = Constz.ToDoListTab.Purchase.ProductPurchaseList.Name;
            this.lblProductReceiveList.Text = Constz.ToDoListTab.Purchase.ProductPurchaseList.Name;

            this.lblTab.Text = Constz.ToDoListTab.Purchase.ProductPurchaseList.Index;

            SetImage();
        }
    }

    protected void btnProductPurchaseList_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ToDoListTab.Purchase.ProductPurchaseList.Index;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnProductReceiveList_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ToDoListTab.Purchase.ProductReceiveList.Index;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
}
