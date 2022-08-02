using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Eumis.Common.Crypto
{
    public static class CryptoUtils
    {
        private static char[] alphanumerics = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GetSha256XMLHash(string xml)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return GetSha256XMLHashInternal(ms);
            }
        }

        public static string GetSha256XMLHash(XmlDocument xmlDoc)
        {
            return GetSha256XMLHashInternal(xmlDoc);
        }

        private static string GetSha256XMLHashInternal(object input)
        {
            //canonicalize the xml so that changes that
            //do not modify the data representation will not change the hash
            XmlDsigC14NTransform t = new XmlDsigC14NTransform(true);
            t.LoadInput(input);

            using (var hash = new SHA256Managed())
            {
                byte[] digest = t.GetDigestedOutput(hash);
                return GetHexString(digest);
            }
        }

        public static string GetHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", String.Empty);
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

            return new String(result);
        }
    }
}
