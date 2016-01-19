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

public partial class Controls_Z2BoxControl : System.Web.UI.UserControl
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Z2SCRIPT", @"
<script language='JavaScript'>
// ย้ายข้อมูลที่เลือกจาก list box หนึ่งไปยัง listbox หนึ่ง
function lstmove(lst1, lst2)
{
	lsSrc = document.getElementById(lst1);
	lsDes = document.getElementById(lst2);

	for ( var i = 0 ; i < lsSrc.length ; i++ ){
		opt = lsSrc.options[i];
		if (opt.selected)
			lsDes.add( new Option(opt.text, opt.value) );
	}

	for ( var i = lsSrc.length - 1; i >= 0 ; i-- ) {
		opt = lsSrc.options[i];
		if (opt.selected)
			lsSrc.remove(i);
	}
}

// ย้ายข้อมูลทั้งหมดจาก list box หนึ่งไปยัง listbox หนึ่ง
function lstmoveall(lst1, lst2)
{
	lsSrc = document.getElementById(lst1);
	lsDes = document.getElementById(lst2);

	for ( var i = 0 ; i < lsSrc.length ; i++ ){
		opt = lsSrc.options[i];
		lsDes.add( new Option(opt.text, opt.value) );
	}

	for ( var i = lsSrc.length - 1; i >= 0 ; i-- ) {
		opt = lsSrc.options[i];
		lsSrc.remove(i);
	}
}

function getlstvalue(lst, tmp)
{
	zlst = document.getElementById(lst);
	ztmp = document.getElementById(tmp);
	ret = '';
	for ( var i = 0 ; i < zlst.length ; i++ ){
		ret = ret + zlst.options[i].value;
		if (i < zlst.length - 1)
			ret = ret + ',';
	}
	ztmp.value = ret;
}
</script>
");

        // Put user code to initialize the page here
        if (!IsPostBack)
        {
            string UpdateVal = "getlstvalue('" + lstAuth.ClientID + "', '" + lstAuth.ClientID + "_zLstSelect');getlstvalue('" + lstNoAuth.ClientID + "', '" + lstNoAuth.ClientID + "_zLstNoSelect');";
            btnAddSel.Attributes.Add("OnClick", "lstmove('" + lstNoAuth.ClientID + "','" + lstAuth.ClientID + "');" + UpdateVal + "return false;");
            btnAddAll.Attributes.Add("OnClick", "lstmoveall('" + lstNoAuth.ClientID + "','" + lstAuth.ClientID + "');" + UpdateVal + "return false;");
            btnRemSel.Attributes.Add("OnClick", "lstmove('" + lstAuth.ClientID + "','" + lstNoAuth.ClientID + "');" + UpdateVal + "return false;");
            btnRemAll.Attributes.Add("OnClick", "lstmoveall('" + lstAuth.ClientID + "','" + lstNoAuth.ClientID + "');" + UpdateVal + "return false;");
        }
        else
        {
            KeepState();
        }
    }

    public void SetSource(DataTable zDt)
    {
        lstNoAuth.DataSource = zDt;
        lstNoAuth.DataTextField = "NAME";
        lstNoAuth.DataValueField = "LOID";
        lstNoAuth.DataBind();
    }

    public void SetDest(DataTable zDt)
    {
        lstAuth.DataSource = zDt;
        lstAuth.DataTextField = "NAME";
        lstAuth.DataValueField = "LOID";
        lstAuth.DataBind();
    }

    public ArrayList SelectedData
    {
        get
        {
            ArrayList zLst = new ArrayList();
            for (int i = 0; i < lstAuth.Items.Count; i++)
            {
                zLst.Add(lstAuth.Items[i].Value);
            }
            return zLst;
        }
    }

    public string txtSourceHead
    {
        set { lblSource.Text = value; }
        get { return lblSource.Text; }
    }

    public string txtDestHead
    {
        set { lblDestination.Text = value; }
        get { return lblDestination.Text; }
    }

    private void KeepState()
    {
        // keep state for noauth
        string zSel = Request[lstNoAuth.ClientID + "_zLstNoSelect"];
        if (zSel != null && zSel.Trim().Length > 0)
        {
            string[] AllSel = zSel.Split(',');
            for (int i = 0; i < AllSel.Length; i++)
            {
                ListItem zItm = lstAuth.Items.FindByValue(AllSel[i].Trim());

                if (zItm != null)
                {
                    lstNoAuth.Items.Add(zItm);
                    lstAuth.Items.Remove(zItm);
                }
            }
        }
        //keep state for auth
        zSel = Request[lstAuth.ClientID + "_zLstSelect"];
        if (zSel != null && zSel.Trim().Length > 0)
        {
            string[] AllSel = zSel.Split(',');
            for (int i = 0; i < AllSel.Length; i++)
            {
                ListItem zItm = lstNoAuth.Items.FindByValue(AllSel[i].Trim());

                if (zItm != null)
                {
                    lstAuth.Items.Add(zItm);
                    lstNoAuth.Items.Remove(zItm);
                }
            }
        }
    }
}
