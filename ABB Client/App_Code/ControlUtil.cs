using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ABBClient
{
    public class ControlUtil
    {
        public static void SetDBlTextBox(object sender, KeyPressEventArgs e)
        {
            int position = -1;
            string word = "";
            word = ((TextBox)sender).Text.Trim();
            position = word.IndexOf(".");

            switch (position)
            {
                case -1:
                    if (!char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(".") && e.KeyChar != Convert.ToChar("\b"))
                    {
                        e.Handled = true;
                    }
                    break;
                default:
                    if (!char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar("\b"))
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        public static void SetIntTextBox(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
    }
}
