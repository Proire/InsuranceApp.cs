using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace BookStoreRL.Utilities
{
    public static class KeyIvManager
    {
        private static readonly string FilePath = "email_keys_and_iv.json";

        // Class to represent the key and IV
        private class EmailKeyIv
        {
            public string Email { get; set; }
            public string Key { get; set; }
            public string Iv { get; set; }
        }

        // Save the key and IV to a JSON file for email
        public static void SaveKeyAndIv(string email, byte[] key, byte[] iv)
        {
            var emailKeyIv = new EmailKeyIv
            {
                Email = email,
                Key = Convert.ToBase64String(key),
                Iv = Convert.ToBase64String(iv)
            };

            var existingData = ReadAllKeysAndIvs();
            existingData[email] = emailKeyIv;

            string jsonString = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, jsonString);
        }

        // Retrieve the key and IV from the JSON file for email
        public static (byte[] key, byte[] iv) GetKeyAndIv(string email)
        {
            var existingData = ReadAllKeysAndIvs();

            if (existingData.TryGetValue(email, out var emailKeyIv))
            {
                byte[] key = Convert.FromBase64String(emailKeyIv.Key);
                byte[] iv = Convert.FromBase64String(emailKeyIv.Iv);
                return (key, iv);
            }

            throw new ArgumentException("Key and IV not found for the given email address.");
        }

        // Update the key and IV for a specific email address
        public static void UpdateKeyAndIv(string email, byte[] newKey, byte[] newIv)
        {
            var existingData = ReadAllKeysAndIvs();

            if (existingData.TryGetValue(email, out var emailKeyIv))
            {
                emailKeyIv.Key = Convert.ToBase64String(newKey);
                emailKeyIv.Iv = Convert.ToBase64String(newIv);

                existingData[email] = emailKeyIv;

                string jsonString = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, jsonString);
            }
            else
            {
                throw new ArgumentException("Email address not found. Cannot update key and IV.");
            }
        }

        // Read all keys and IVs from the JSON file
        private static Dictionary<string, EmailKeyIv> ReadAllKeysAndIvs()
        {
            if (!File.Exists(FilePath))
            {
                return new Dictionary<string, EmailKeyIv>();
            }

            string jsonString = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<Dictionary<string, EmailKeyIv>>(jsonString) ?? new Dictionary<string, EmailKeyIv>();
        }
    }
}
