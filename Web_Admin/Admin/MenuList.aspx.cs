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

public partial class Admin_MenuList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MenuDataSource md = new MenuDataSource();
    }
 
}

[System.ComponentModel.DataObject()]
public class MenuDataSource
{

    public MenuDataSource()
    {
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetMenuItem()
    {
        DataTable zDt = new DataTable();


        return zDt;
    }

}
