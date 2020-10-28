using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Task_List
{
    /// <summary>
    /// Class will manage salt generation and hashing for user login
    /// </summary>
    public class clsPasswordManager
    {
        private static byte[] salt = new byte[128 / 8];

        public byte[] GenerateSalt()
        {
            if (salt == null)
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }
            return salt;
        }

        public string ProtectPass(string password)
        {
            GenerateSalt();

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
