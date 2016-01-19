using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using ABB.DAL;
using ABB.Data;

namespace ABB.Flow
{
    public class AppFlow
    {
        private OfficerDAL _dal;
        private UserData _data;
        private string _error = "";
        private const string encryptKey = "@Bb$y$teM";
        private static byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
        private const string UserNotFound = "�������ö�������к��� ���ͧ�ҡ��������к����١��ͧ ��س��ͧ�����ա����";
        private const string InvalidPassword = "�������ö�������к��� ���ͧ�ҡ���ʼ�ҹ���١��ͧ ��س��ͧ�����ա����";
        private const string InvalidCurrentPassword = "�������ö����¹���ʼ�ҹ�� ���ͧ�ҡ���ʼ�ҹ���١��ͧ ��س��ͧ�����ա����";
        private const string NotActivateUser = "�������ö�������к��� ���ͧ�ҡ��ҹ������Ѻ͹حҵ���������к���㹢�й�� ��سҵԴ������˹�ҷ��������к�";
        private const string NotSuthorizedForSystem = "�������ö�������к��� ���ͧ�ҡ��ҹ������Ѻ͹حҵ������к���� ��سҵԴ������˹�ҷ��������к�";

        private OfficerDAL DALObj
        {
            get { if (_dal == null) { _dal = new OfficerDAL(); } return _dal; }
        }

        public UserData Data
        {
            get { if (_data == null) { _data = new UserData(); } return _data; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        #region POS

        public bool IsPOSAuthenticated(string userID, string password, double warehouse)
        {
            bool ret = true;
            _data = new UserData();
            if (!DALObj.GetDataByPOSUserID(userID))
            {
                ret = false;
                _error = UserNotFound;
            }
            else
            {
                if (Decrypt(DALObj.PASSWORD) == password)
                {
                    if (DALObj.POS == "Y")
                    {
                        if (DALObj.EFDATE <= DateTime.Today && (DateTime.Today <= DALObj.EPDATE || DALObj.EPDATE.Year == 1))
                        {
                            _data.DivisionID = DALObj.DIVISION;
                            _data.Name = DALObj.TNAME + " " + DALObj.LASTNAME;
                            _data.OfficerID = DALObj.LOID;
                            _data.UserID = DALObj.USERID;
                            _data.Warehouse = warehouse;
                        }
                        else
                        {
                            ret = false;
                            _error = NotActivateUser;
                        }
                    }
                    else
                    {
                        ret = false;
                        _error = NotSuthorizedForSystem;
                    }
                }
                else
                {
                    ret = false;
                    _error = InvalidPassword;
                }
            }
            return ret;
        }

        #endregion

        #region Handheld

        public bool IsHHTAuthenticated(string userID, string password, double warehouse)
        {
            bool ret = true;
            _data = new UserData();
            if (!DALObj.GetDataByHHTUserID(userID))
            {
                ret = false;
                _error = UserNotFound;
            }
            else
            {
                if (Decrypt(DALObj.PASSWORD) == password)
                {
                    if (DALObj.HHT == "Y")
                    {
                        if (DALObj.EFDATE <= DateTime.Today && (DateTime.Today <= DALObj.EPDATE || DALObj.EPDATE.Year == 1))
                        {
                            _data.DivisionID = DALObj.DIVISION;
                            _data.Name = DALObj.TNAME + " " + DALObj.LASTNAME;
                            _data.OfficerID = DALObj.LOID;
                            _data.UserID = DALObj.USERID;
                            _data.Warehouse = warehouse;
                        }
                        else
                        {
                            ret = false;
                            _error = NotActivateUser;
                        }
                    }
                    else
                    {
                        ret = false;
                        _error = NotSuthorizedForSystem;
                    }
                }
                else
                {
                    ret = false;
                    _error = InvalidPassword;
                }
            }
            return ret;
        }

        #endregion

        public bool IsAuthenticated(string userID, string password, double warehouse)
        {
            bool ret = true;
            _data = new UserData();
            if (!DALObj.GetDataByUserID(userID, null))
            {
                ret = false;
                _error = UserNotFound;
            }
            else
            {
                if (Decrypt(DALObj.PASSWORD) == password)
                {
                    if (DALObj.EFDATE <= DateTime.Today && (DateTime.Today <= DALObj.EPDATE || DALObj.EPDATE.Year == 1))
                    {
                        _data.DivisionID = DALObj.DIVISION;
                        _data.Name = DALObj.TNAME + " " + DALObj.LASTNAME;
                        _data.OfficerID = DALObj.LOID;
                        _data.UserID = DALObj.USERID;
                        _data.Warehouse = warehouse;
                    }
                    else
                    {
                        ret = false;
                        _error = NotActivateUser;
                    }
                }
                else
                {
                    ret = false;
                    _error = InvalidPassword;
                }
            }
            return ret;
        }

        public bool IsValidPassword(string userID, string password)
        {
            bool ret = true;
            _data = new UserData();
            if (!DALObj.GetDataByUserID(userID, null))
            {
                ret = false;
                _error = UserNotFound;
            }
            else
            {
                if (Decrypt(DALObj.PASSWORD) != password)
                {
                    ret = false;
                    _error = InvalidCurrentPassword;
                }
            }
            return ret;
        }

        public bool ChangePassword(double officerID, string newPassword)
        {
            bool ret = true;
            OfficerDAL offDAL = new OfficerDAL();
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                offDAL.GetDataByLOID(officerID, obj.zTrans);
                offDAL.PASSWORD = Encrypt(newPassword);

                if (offDAL.OnDB)
                    offDAL.UpdateCurrentData(offDAL.USERID, obj.zTrans);
                else
                    throw new ApplicationException("��辺�����Ţͧ��ҹ��к�");

                obj.zTrans.Commit();
                obj.zConn.Close();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                obj.zTrans.Rollback();
                obj.zConn.Close();
            }
            return ret;
        }

        public static string Encrypt(string text)
        {
            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5csp = new MD5CryptoServiceProvider();
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            tdsp.Key = md5csp.ComputeHash(ASCIIEncoding.ASCII.GetBytes(encryptKey));
            tdsp.IV = IV;
            return Convert.ToBase64String(tdsp.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

        public static string Decrypt(string text)
        {
            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5csp = new MD5CryptoServiceProvider();
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            buffer = Convert.FromBase64String(text);
            tdsp.Key = md5csp.ComputeHash(ASCIIEncoding.ASCII.GetBytes(encryptKey));
            tdsp.IV = IV;
            return Encoding.ASCII.GetString(tdsp.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

    }
}
