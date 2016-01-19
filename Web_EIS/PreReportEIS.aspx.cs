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

public partial class PreReportEIS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Sale_Month_Yearly.aspx?type=sale");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Sale_Month_Product.aspx?type=sale");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Stockin_Month_Yearly.aspx?type=stockin");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Stockin_Month_Product.aspx?type=stockin");
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Stockout_Month_Yearly.aspx?type=stockout");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Stockout_Month_Product.aspx?type=stockout");
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Support_Month_Yearly.aspx?type=support");
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Support_Month_Product.aspx?type=support");
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Return_Month_Yearly.aspx?type=return");
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Return_Month_Product.aspx?type=return");
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Sendback_Month_Yearly.aspx?type=sendback");
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Sendback_Month_Product.aspx?type=sendback");
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/SalePrice_Day_Yearly.aspx");
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/SalePrice_Month_Product.aspx");
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Production_Month_Yearly.aspx");
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreReport/Production_Month_Product.aspx");
    }
}
