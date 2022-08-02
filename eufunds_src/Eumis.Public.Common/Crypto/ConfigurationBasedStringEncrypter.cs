using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Eumis.Public.Common.Crypto
{
    public class ConfigurationBasedStringEncrypter
    {
        private static readonly ICryptoTransform Encrypter;
        private static readonly ICryptoTransform Decrypter;

        static ConfigurationBasedStringEncrypter()
        {
            var key = "v2_inZR4ggVGrAEHnCvYG7auz1TWhVCVXWcvoaEZYjQILtl1YBXcDFJKTzOfRivsRogsLleNBCMPgTHmUIl7eMVqvXDL5haiZLmjZ8T";
            var useHashingString = "no need";

            bool useHashing = true;

            if (string.Compare(useHashingString, "false", true) == 0)
            {
                useHashing = false;
            }

            byte[] keyArray = null;

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            Encrypter = tdes.CreateEncryptor();
            Decrypter = tdes.CreateDecryptor();
            tdes.Clear();
        }

        #region IEncryptionSettingsProvider Members

        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var bytes = UTF8Encoding.UTF8.GetBytes(value);

            var encryptedBytes = Encrypter.TransformFinalBlock(bytes, 0, bytes.Length);
            var encrypted = Convert.ToBase64String(encryptedBytes);

            return encrypted;
        }

        public static string Encrypt(int value)
        {
            return Encrypt(value.ToString(CultureInfo.InvariantCulture));
        }

        public static string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var bytes = Convert.FromBase64String(value);

            var decryptedBytes = Decrypter.TransformFinalBlock(bytes, 0, bytes.Length);
            var decrypted = UTF8Encoding.UTF8.GetString(decryptedBytes);

            return decrypted;
        }

        #endregion
    }
}
