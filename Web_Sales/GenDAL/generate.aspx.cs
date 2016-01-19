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
using ABB.Flow;
using ABB.Data;

public partial class GenDAL_generate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGen_Click(object sender, EventArgs e)
    {
        GenerateFlow genFlow = new GenerateFlow();
        genFlow.Data = Data;
        if (genFlow.CheckConnection)
        {
            txtShowDAL.Text = genFlow.CreateDAL();
        }
        else
        {
            txtShowDAL.Text = "ไม่สามารถติดต่อกับระบบได้";
        }
    }

    private GenerateData Data
    {
        get
        {
            GenerateData _data = new GenerateData();
            _data.Database = txtDataSource.Text.Trim();
            //_data.Server = txtServer.Text.Trim();
            _data.UserID = txtUserid.Text.Trim();
            _data.Password = txtPassword.Text.Trim();
            _data.Table = txtTable.Text.Trim();
            _data.Namespace = txtNameSpace.Text.Trim();
            _data.Class = txtClass.Text.Trim();
            return _data;
        }
    }
}
