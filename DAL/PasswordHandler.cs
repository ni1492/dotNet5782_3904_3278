using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DO
{
    public class PasswordHandler
    {
        private static Random saltGenerator = new Random();
        private static string hash(string passwordWithSalt)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt)));
        }
        public static int generateSalt()
        {
            return saltGenerator.Next();
        }
        public static string generateNewPassword(string password, int salt)
        {
            return hash(password + salt);
        }
        public static bool checkPassword(string password, string hashed, int salt)
        {
            return hashed == hash(password + salt);
        }
    }
}
