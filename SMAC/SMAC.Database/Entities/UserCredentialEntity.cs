using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SMAC.Database
{
    public class UserCredentialEntity
    {
        private static SmacEntities context;

        public static void CreateUserCred(string id, string username, string password)
        {
            try
            {
                context = new SmacEntities();

                UserCredential cred = new UserCredential()
                {
                    Password = password,
                    UserName = GetSHA256Hash(password),
                    User = UserEntity.GetUser(id)
                };

                context.UserCredentials.Add(cred);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UserCredential GetUserCred(string id)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.UserCredentials where a.UserId == id select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetSHA256Hash(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentException("An empty string value cannot be hashed.");
            }

            Byte[] data = System.Text.Encoding.UTF8.GetBytes(s);
            Byte[] hash = new SHA256CryptoServiceProvider().ComputeHash(data);
            return Convert.ToBase64String(hash);
        }

        public static void UpdateUserCred(string id, string username, string password)
        {
            try
            {
                context = new SmacEntities();

                var cred = GetUserCred(id);

                if (cred == null)
                    throw new Exception("User Credentials could not be found.");

                cred.UserName = username;

                if (password != null)
                    cred.Password = GetSHA256Hash(password);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteUserCred(string id)
        {
            try
            {
                context = new SmacEntities();

                var cred = GetUserCred(id);

                if (cred == null)
                    throw new Exception("User Credentials could not be found.");

                context.UserCredentials.Remove(cred);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DoesUsernameExist(string username)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.UserCredentials where a.UserName == username select a).Count() > 0;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User Authenticate(string username, string password)
        {
            try
            {
                context = new SmacEntities();

                username = username.ToLower();

                // Match username
                var cred = (from a in context.UserCredentials where a.UserName == username select a).FirstOrDefault();

                if (cred == null)
                    throw new Exception("Username or password could not be found.");

                // Match password
                string pass = GetSHA256Hash(password);
                if (cred.Password == pass)
                    return cred.User;
                else
                    throw new Exception("Username or password could not be found.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
