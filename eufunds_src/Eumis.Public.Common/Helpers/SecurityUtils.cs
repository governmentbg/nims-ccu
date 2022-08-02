using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Eumis.Public.Common.Helpers
{
    public static class SecurityUtils
    {
        private static bool? encryptRedirectData = null;
        private static Random random = new Random();

        private static bool EncryptRedirectDataConfigValue
        {
            get
            {
                if (!encryptRedirectData.HasValue)
                {
                    encryptRedirectData = true;
                    string rdConfig = @"XJQDd6wUPZ56swmilPxR607TdF9vD8viuUGCm3NTzQsnJaqrziTvHdhyfKc9HEywBr0FTwKT8RVqKyalPSBcUYkG1FoAJVOkeDP/Tjr5ECYwM2K6ZeciuCIsmS55KY0+U/Wnr2laNlq1/iGDNFcvC0DSuXLmj0Rr+9HWqa83MNy1yVcFEVrwAs+U90vD2Y6xXQx6/8wkY19S5a6NKr69gH8OmiOkRBTIeSqvTmrXW0BrmPLVSK2BkAGuv87l/k4kKbOkyCelloVNpf4R6jhWU854AICQH++5IjA=";

                    // ConfigurationManager.AppSettings["EncryptRedirectData"];
                    if (!string.IsNullOrEmpty(rdConfig))
                    {
                        encryptRedirectData = bool.Parse(rdConfig);
                    }
                }

                return encryptRedirectData.Value;
            }
        }

        #region Crypto Mix

        private static string EncryptToBase64String(string data, byte[] iV, byte[] key)
        {
            Rijndael r = Rijndael.Create();
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, r.CreateEncryptor(key, iV), CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(data);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private static string DecryptFromBase64String(string encryptedData, byte[] iV, byte[] key)
        {
            byte[] encryptedByteContens = Convert.FromBase64String(encryptedData);
            Rijndael r = Rijndael.Create();
            using (MemoryStream ms = new MemoryStream(encryptedByteContens))
            {
                using (CryptoStream cs = new CryptoStream(ms, r.CreateDecryptor(key, iV), CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        #endregion

        #region Redirect Data

        private static string GetRandomData()
        {
            return random.Next(1, 1).ToString();
        }

        public static string EncryptValue(int redirectData)
        {
            string contents = redirectData.ToString();

            if (!EncryptRedirectDataConfigValue)
            {
                return contents;
            }

            contents = GetRandomData() + '?' + contents;

            return SecurityUtils.EncryptToBase64String(
                contents,
                SecurityUtils.RandomCryptoKeys.RijndaelIV,
                SecurityUtils.RandomCryptoKeys.RijndaelKey);
        }

        public static Dictionary<string, string> DecryptValue(string encryptedStringContents)
        {
            string content;

            if (EncryptRedirectDataConfigValue)
            {
                string randomDataConcatenatedContents =
                    SecurityUtils.DecryptFromBase64String(
                        encryptedStringContents,
                        SecurityUtils.RandomCryptoKeys.RijndaelIV,
                        SecurityUtils.RandomCryptoKeys.RijndaelKey);

                string[] contentsPair = randomDataConcatenatedContents.Split('?');

                content = contentsPair[1];
            }
            else
            {
                content = encryptedStringContents;
            }

            return new Dictionary<string, string>() { { "id", content } };
        }

        #endregion

        private static class RandomCryptoKeys
        {
            private static byte[] rijndaelKey;
            private static byte[] rijndaelIV;

            static RandomCryptoKeys()
            {
                Rijndael r = Rijndael.Create();
                r.GenerateKey();
                r.GenerateIV();

                RijndaelKey = r.Key;
                RijndaelIV = r.IV;
            }

            public static byte[] RijndaelKey { get => rijndaelKey; set => rijndaelKey = value; }

            public static byte[] RijndaelIV { get => rijndaelIV; set => rijndaelIV = value; }
        }
    }
}