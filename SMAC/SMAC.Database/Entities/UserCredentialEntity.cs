using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SMAC.Database
{
    public class UserCredentialEntity
    {
        public static void CreateUserCred(string id, string username, string password)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                { 
                    if (DoesUsernameExist(username, id))
                        throw new Exception("Username already exists.  Please select another.");

                    UserCredential cred = new UserCredential()
                    {
                        Password = password,
                        UserName = GetSHA256Hash(password),
                        User = UserEntity.GetUser(id)
                    };

                    context.UserCredentials.Add(cred);
                    context.SaveChanges();
                }
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
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.UserCredentials where a.UserId == id select a).FirstOrDefault();
                }
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
                using (SmacEntities context = new SmacEntities())
                {
                    var cred = GetUserCred(id);

                    if (cred == null)
                        throw new Exception("User Credentials could not be found.");

                    if (DoesUsernameExist(username, id))
                        throw new Exception("Username already exists.  Please select another.");

                    cred.UserName = username;

                    if (password != null)
                        cred.Password = GetSHA256Hash(password);

                    context.Entry(cred).State = System.Data.Entity.EntityState.Modified;

                    context.SaveChanges();
                }
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
                using (SmacEntities context = new SmacEntities())
                {
                    var cred = GetUserCred(id);

                    if (cred == null)
                        throw new Exception("User Credentials could not be found.");

                    context.UserCredentials.Remove(cred);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DoesUsernameExist(string username, string userId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.UserCredentials where a.UserName == username && a.UserId != userId select a).Count() > 0;
                }
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
                using (SmacEntities context = new SmacEntities())
                {
                    User user = null;

                    username = username.ToLower();

                    // Match username
                    var cred = (from a in context.UserCredentials where a.UserName == username select a).FirstOrDefault();

                    if (cred == null)
                        throw new Exception("Username or password could not be found.");

                    // Match password
                    string pass = GetSHA256Hash(password);
                    if (cred.Password == pass)
                        user = cred.User;
                    else
                        throw new Exception("Username or password could not be found.");

                    if (cred.User.Student == null && cred.User.Teacher == null && cred.User.Admin == null && cred.User.Staff == null)
                    {
                        throw new Exception("User has not been placed into a role.  Please contact your lazy administrator.");
                    }
                    else
                        return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
