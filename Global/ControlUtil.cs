using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABB.Global
{
    public class ControlUtil
    {

        //public static void ClientAlert(Control ctl, string alertText)
        //{
        //    ScriptManager.RegisterStartupScript(ctl, typeof(UpdatePanel), "AlertMsg", "alert('" + alertText.Replace("'", "\"").Replace("\n", "") + "');", true);
        //}

        public static void SetDblTextBox(TextBox ctlz)
        {
            ctlz.Attributes.Add("OnKeyPress", "ChkDbl(this)");
            ctlz.Attributes.Add("OnBlur", "valDbl(this)");
            ctlz.Attributes.Add("OnFocus", "prepareNum(this)");
        }
        
        public static void SetDblTextBox3(TextBox ctlz)
        {
            ctlz.Attributes.Add("OnKeyPress", "ChkDbl(this)");
            ctlz.Attributes.Add("OnBlur", "valDbl3(this)");
            ctlz.Attributes.Add("OnFocus", "prepareNum(this)");
        }

        public static void SetDblTextBox4(TextBox ctlz)
        {
            ctlz.Attributes.Add("OnKeyPress", "ChkDbl(this)");
            ctlz.Attributes.Add("OnBlur", "valDbl4(this)");
            ctlz.Attributes.Add("OnFocus", "prepareNum(this)");
        }

        public static void SetDblTextBox5(TextBox ctlz)
        {
            ctlz.Attributes.Add("OnKeyPress", "ChkDbl(this)");
            ctlz.Attributes.Add("OnBlur", "valDbl5(this)");
            ctlz.Attributes.Add("OnFocus", "prepareNum(this)");
        }

        public static void SetDblTextBox6(TextBox ctlz)
        {
            ctlz.Attributes.Add("OnKeyPress", "ChkDbl(this)");
            ctlz.Attributes.Add("OnBlur", "valDbl6(this)");
            ctlz.Attributes.Add("OnFocus", "prepareNum(this)");
        }

        public static void SetMinusDblTextBox(TextBox ctlz)
        {
            ctlz.Attributes.Add("OnKeyPress", "ChkMinusDbl(this)");
            ctlz.Attributes.Add("OnBlur", "valMDbl(this)");
            ctlz.Attributes.Add("OnFocus", "prepareNum(this)");
        }

        public static void SetIntTextBox(TextBox ctlz)
        {
            ctlz.Attributes.Add("OnKeyPress", "ChkInt(this)");
            ctlz.Attributes.Add("OnBlur", "valInt(this)");
            ctlz.Attributes.Add("OnFocus", "prepareNum(this)");
        }

        public static void SetMinusIntTextBox(TextBox ctlz)
        {
            ctlz.Attributes.Add("OnKeyPress", "ChkMinusInt(this)");
            ctlz.Attributes.Add("OnBlur", "valMInt(this)");
            ctlz.Attributes.Add("OnFocus", "prepareNum(this)");
        }

        public static void SetYearTextbox(TextBox ctlz)
        {
            ctlz.Attributes.Add("OnKeyPress", "ChkInt(this)");
            ctlz.Attributes.Add("OnFocus", "prepareNum(this);");
            ctlz.Attributes.Add("OnBlur", "valYear(this); if (this.value.length <4 && this.value != '') { alert('กรุณาระบุเลขปีให้ถูกต้อง'); this.focus(); }");
        }

    }
}
