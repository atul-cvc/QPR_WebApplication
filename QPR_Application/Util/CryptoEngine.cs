﻿using QPR_Application.Models.DTO.Utility;
using System.Security.Cryptography;
using System.Text;

namespace QPR_Application.Util
{
    public class CryptoEngine
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(new Guid().ToString()); // 16 bytes for AES-128
        private static readonly byte[] IV = Encoding.UTF8.GetBytes(new Guid().ToString()); // 16 bytes for AES-128
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        //CryptoEngine.Decrypt(Ciphertext.Text, "sblw-3hn8-sqoy19");
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public string GenerateVAWSSOToken(TokenModel tokenModel, string VAW_URL = "", string _key = "")
        {
            //string token = "https://localhost:44381/Account/LoginByQPR?";
            //string _key = GenerateRandomKey();
            var modelStr = Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel);
            var encryptedString = CryptoEngine.Encrypt(modelStr, _key);
            var tokenString = System.Net.WebUtility.UrlEncode(encryptedString);
            _key = System.Net.WebUtility.UrlEncode(_key);
            VAW_URL += "?token=" + tokenString;
            return VAW_URL;
        }

        public static string GenerateRandomKey(int length = 16)
        {
            // Create a byte array to hold the key
            byte[] keyBytes = new byte[length];

            // Fill the array with cryptographically secure random bytes
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            // Convert the byte array to a Base64 string or hex string
            return Convert.ToBase64String(keyBytes);
            // Alternatively, for a hex string:
            // return BitConverter.ToString(keyBytes).Replace("-", "").ToLower();
        }
    }
}
