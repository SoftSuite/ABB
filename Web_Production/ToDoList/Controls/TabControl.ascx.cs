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
    public event TabChangeEvent SelectedChange;

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

        this.btnProductionWaitList.Visible = false;
        this.lblProductionWaitList.Visible = true;

        this.btnProductionDuringList.Visible = true;
        this.lblProductionDuringList.Visible = false;

        switch (SelectedTab)
        {
            case Constz.ToDoListTab.Production.ProductionWaitList.Index:
                this.imgBD1L.ImageUrl = selectL;
                this.imgBD1R.ImageUrl = selectR;

                this.btnProductionWaitList.Visible = false;
                this.lblProductionWaitList.Visible = true;

                this.btnProductionDuringList.Visible = true;
                this.lblProductionDuringList.Visible = false;

                break;

            case Constz.ToDoListTab.Production.ProductionDuringList.Index:
                this.imgBD2L.ImageUrl = selectL;
                this.imgBD2R.ImageUrl = selectR;

                this.btnProductionWaitList.Visible = true;
                this.lblProductionWaitList.Visible = false;

                this.btnProductionDuringList.Visible = false;
                this.lblProductionDuringList.Visible = true;

                break;

            default:
                break;

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnProductionWaitList.Text = Constz.ToDoListTab.Production.ProductionWaitList.Name;
            this.lblProductionWaitList.Text = Constz.ToDoListTab.Production.ProductionWaitList.Name;

            this.btnProductionDuringList.Text = Constz.ToDoListTab.Production.ProductionDuringList.Name;
            this.lblProductionDuringList.Text = Constz.ToDoListTab.Production.ProductionDuringList.Name;

            this.lblTab.Text = Constz.ToDoListTab.Production.ProductionWaitList.Index;

            SetImage();
        }
    }

    protected void btnProductionWaitList_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ToDoListTab.Production.ProductionWaitList.Index;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }

    protected void btnProductionDuringList_Click(object sender, EventArgs e)
    {
        this.lblTab.Text = Constz.ToDoListTab.Production.ProductionDuringList.Index;
        SetImage();
        if (SelectedChange != null) SelectedChange(sender, e);
    }
}
