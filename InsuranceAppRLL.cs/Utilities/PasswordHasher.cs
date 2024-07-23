using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Utilities
{
    public static class PasswordHasher
    {
        // Method to hash a password
        public static string HashPassword(string password, byte[] key, byte[] iv)
        {
            byte[] cipheredtext;

            using(Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                using(MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(password);
                        }
                        cipheredtext = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(cipheredtext);    
        }

        // Method to verify a password against a hash
        public static string VerifyPassword(byte[] cipheredPassword, byte[] key, byte[] iv)
        {
            string plainText = string.Empty;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(cipheredPassword))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            plainText = sr.ReadToEnd();
                        }
                    }
                }
            }
            return plainText;
        }

    }
}
