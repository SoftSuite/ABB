using System;
using System.Collections.Generic;
using System.Text;
using ABB.Data;
using System.Web.UI;


namespace ABB.Global
{
    public class Authz
    {
        public static UserData CurrentUserInfo
        {
            get
            {
                UserData data = new UserData();
                data.UserID = "test";
                data.DivisionID = 1;
                data.Name = "test";
                data.OfficerID = 1;
                data.Warehouse = 1;
                if (System.Web.HttpContext.Current.Request.Cookies["Name"] != null) data.Name = System.Web.HttpContext.Current.Request.Cookies["Name"].Value;
                if (System.Web.HttpContext.Current.Request.Cookies["DivisionID"] != null) data.DivisionID = Convert.ToDouble(System.Web.HttpContext.Current.Request.Cookies["DivisionID"].Value);
                if (System.Web.HttpContext.Current.Request.Cookies["OfficerID"] != null) data.OfficerID = Convert.ToDouble(System.Web.HttpContext.Current.Request.Cookies["OfficerID"].Value);
                if (System.Web.HttpContext.Current.Request.Cookies["UserID"] != null) data.UserID = System.Web.HttpContext.Current.Request.Cookies["UserID"].Value;
                if (System.Web.HttpContext.Current.Request.Cookies["Warehouse"] != null) data.Warehouse = Convert.ToDouble(System.Web.HttpContext.Current.Request.Cookies["Warehouse"].Value);
                return data;
            }
        }

        public static void SetUserLogIn(UserData data)
        {
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("DivisionID");
            cookie.Value = data.DivisionID.ToString();
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            cookie = new System.Web.HttpCookie("Name");
            cookie.Value = data.Name;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            cookie = new System.Web.HttpCookie("OfficerID");
            cookie.Value = data.OfficerID.ToString();
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            cookie = new System.Web.HttpCookie("Warehouse");
            cookie.Value = data.Warehouse.ToString();
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            cookie = new System.Web.HttpCookie("UserID");
            cookie.Value = data.UserID;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public Authz() { }
    }
}
