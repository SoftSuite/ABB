using System;
using System.Text;
using System.Security.Cryptography;

namespace ABB.Global
{
    public class zText
    {
        public static string MD5Encode(string text)
        {
            MD5CryptoServiceProvider enc= new MD5CryptoServiceProvider();
            byte[] res = enc.ComputeHash(Encoding.ASCII.GetBytes(text));
            return Convert.ToBase64String(res);
        }

    }
}
