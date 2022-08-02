using System;
using System.Security.Cryptography;
using System.Text;

namespace Eumis.Common.Crypto
{
    public static class ConfigurationBasedStringEncrypter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA5350:DoNotUseWeakCryptographicAlgorithms", Justification = "Should be used same algorithm as eufunds")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA5351:DoNotUseInsecureCryptographicAlgorithmMD5", Justification = "Should be used same key as eufunds")]
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var key = "v2_inZR4ggVGrAEHnCvYG7auz1TWhVCVXWcvoaEZYjQILtl1YBXcDFJKTzOfRivsRogsLleNBCMPgTHmUIl7eMVqvXDL5haiZLmjZ8T";

            var hashmd5 = new MD5CryptoServiceProvider();

            byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7,
            };

            var bytes = UTF8Encoding.UTF8.GetBytes(value);
            var encrypter = tdes.CreateEncryptor();
            tdes.Clear();
            var encryptedBytes = encrypter.TransformFinalBlock(bytes, 0, bytes.Length);
            var encrypted = Convert.ToBase64String(encryptedBytes);

            return encrypted;
        }
    }
}
