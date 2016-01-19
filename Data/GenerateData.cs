using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class GenerateData
    {
        private string _server = "";
        private string _password = "";
        private string _userid = "";
        private string _database = "";
        private string _table = "";
        private string _namespace = "";
        private string _class = "";
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }
        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }
        public string Table
        {
            get { return _table; }
            set { _table = value; }
        }
        public string Namespace
        {
            get { return _namespace; }
            set { _namespace = value; }
        }
        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }
    }
}
