using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace SeatingPlanCreator
{
    public class User
    {
        public string Username { get; set; }

        private byte[] salt;
        private string hash;

        public User()
        {
            Username = Environment.UserName;
            CalculateSalt();
        }

        public User(string uString)
        {
            string[] details = uString.Split("~".ToCharArray());
            Username = details[1];
            CalculateSalt();
            hash = details[2];
        }

        public User(string username, string password)
        {
            Username = username;
            CalculateSalt();
            hash = ComputeHash(password);
        }

        private void CalculateSalt()
        {
            salt = new byte[Username.Length];

            for (int i = 0; i < Username.Length; i++)
            {
                salt[i] = (byte)Username[i];
            }
        }

        public string ComputeHash(string password)
        {
            byte[] p = Encoding.UTF8.GetBytes(password);
            byte[] c = new byte[p.Length + salt.Length];

            int i = 0;
            for (; i < p.Length; i++)
            {
                c[i] = p[i];
            }
            for (; i < c.Length; i++)
            {
                c[i] = salt[i-p.Length];
            }

            byte[] bHash = new SHA256Managed().ComputeHash(c);

            byte[] h = new byte[bHash.Length + salt.Length];

            for (i = 0; i < bHash.Length; i++)
            {
                h[i] = bHash[i];
            }
            for (; i < h.Length; i++)
            {
                h[i] = salt[i-bHash.Length];
            }

            return Convert.ToBase64String(h);
        }

        public string GetHash()
        {
            return hash;
        }

        public bool PasswordIs(string givenPassword)
        {
            return hash == ComputeHash(givenPassword);
        }

        public void SetPassword(string newPassword)
        {
            hash = ComputeHash(newPassword);
        }

        public override string ToString()
        {
            return string.Format("U~{0}~{1}", Username, hash);
        }
    }
}
