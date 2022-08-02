using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.Xml;
using System.Text;

namespace Eumis.Public.Common.Crypto
{
    public static class CryptoUtils
    {
        private static char[] alphanumerics = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GetSha256XMLHash(string xml)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                // canonicalize the xml so that changes that
                // do not modify the data representation will not change the hash
                XmlDsigC14NTransform t = new XmlDsigC14NTransform(true);
                t.LoadInput(ms);

                using (var hash = new SHA256Managed())
                {
                    byte[] digest = t.GetDigestedOutput(hash);
                    return GetHexString(digest);
                }
            }
        }

        public static bool IsDetachedSignatureValid(byte[] content, byte[] signature)
        {
            if (content == null || content.Length == 0)
            {
                throw new ArgumentException("content cannot be null or empty");
            }

            if (signature == null || signature.Length == 0)
            {
                throw new ArgumentException("signature cannot be null or empty");
            }

            try
            {
                var contentInfo = new ContentInfo(content);
                SignedCms signedCms = new SignedCms(contentInfo, true);

                signedCms.Decode(signature);
                signedCms.CheckSignature(true);

                return true;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        public static string GetHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        public static string GetRandomAlphanumericString(int length)
        {
            if (length < 1)
            {
                throw new ArgumentException("length should be greater than or equal to 1");
            }

            byte[] bytes = new byte[length];
            char[] result = new char[length];

            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(bytes);
            }

            for (int i = 0; i < length; i++)
            {
                result[i] = alphanumerics[bytes[i] % alphanumerics.Length];
            }

            return new string(result);
        }

        public static string GetSha1Hash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(inputBytes);
                return GetHexString(hash).ToLowerInvariant();
            }
        }
    }
}
