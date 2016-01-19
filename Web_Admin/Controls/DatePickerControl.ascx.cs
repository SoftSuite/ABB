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

public partial class Controls_DatePickerControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.DatePicker1.ImageButtonUrl = Constz.ImageFolder + "calendar.gif";
        this.DatePicker1.ImageDropUrl = Constz.ImageFolder + "drop.gif";
    }

    public string CssClass
    {
        get { return this.DatePicker1.CssClass; }
        set { this.DatePicker1.CssClass = value; }
    }

    public DateTime DateValue
    {
        get
        {
            if (this.DatePicker1.DateValue == "")
            {
                return new DateTime(1, 1, 1);
            }
            else
            {
                string tmp = this.DatePicker1.DateValue;
                DateTime dt = new DateTime(Convert.ToInt16(tmp.Substring(6, 4)) - 543, Convert.ToInt16(tmp.Substring(3, 2)), Convert.ToInt16(tmp.Substring(0, 2)));
                return dt;
            }
        }
        set
        {
            this.DatePicker1.DateValue = (value.Year == 1 ? "" : value.ToString("dd/MM/") + (value.Year + 543).ToString());
        }
    }

    public bool Enabled
    {
        get { return this.DatePicker1.Enabled; }
        set { this.DatePicker1.Enabled = value; }
    }
}
