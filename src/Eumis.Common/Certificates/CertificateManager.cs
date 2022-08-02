using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Certificates
{
    public static class CertificateManager
    {
        public static List<X509Certificate2> GetCertificatesFromSignature(byte[] signature)
        {
            ContentInfo contentInfo = new ContentInfo(Array.Empty<byte>());

            SignedCms signedCms = new SignedCms(contentInfo, true);
            signedCms.Decode(signature);
            List<X509Certificate2> certs = new List<X509Certificate2>();

            for (int k = 0; k < signedCms.Certificates.Count; k++)
            {
                var certificate = signedCms.Certificates[k];
                certs.Add(certificate);
            }

            return certs;
        }

        public static IList<X509Certificate2> GetSignerCertificates(byte[] encodedMessage)
        {
            var signedCms = new SignedCms();
            signedCms.Decode(encodedMessage);

            var result = new List<X509Certificate2>();

            foreach (var signerInfo in signedCms.SignerInfos)
            {
                result.Add(signerInfo.Certificate);
            }

            return result;
        }
    }
}
