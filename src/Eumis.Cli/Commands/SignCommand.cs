using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Eumis.Cli
{
    public class SignCommand : ICommand
    {
        public string Name { get; } = "sign";

        public void Configure(CommandLineApplication app, CancellationToken stopped)
        {
            var fileArg = app.Argument("file", "Path to the isun file to sign");

            app.OnExecute(() =>
            {
                string file = fileArg.Value;

                if (string.IsNullOrEmpty(file) || !File.Exists(file))
                {
                    Console.WriteLine("Invalid file!");
                    app.ShowHelp();

                    return 1;
                }

                File.WriteAllBytes($"{file}.p7s", this.Sign(File.ReadAllBytes(file)));

                return 0;
            });
        }

        private byte[] Sign(byte[] data)
        {
            var certificate = this.GenerateCertificate();

            CmsSigner signer = new CmsSigner(certificate);
            signer.DigestAlgorithm = new Oid("SHA1");

            SignedCms signedCms = new SignedCms(new System.Security.Cryptography.Pkcs.ContentInfo(data), true);
            signedCms.ComputeSignature(signer, true);

            return signedCms.Encode();
        }

        private X509Certificate2 GenerateCertificate()
        {
            var random = new SecureRandom(new CryptoApiRandomGenerator());
            var keyStrength = 2048;
            var subject = "CN=Isun Test";

            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(new KeyGenerationParameters(random, keyStrength));

            var subjectKeyPair = keyPairGenerator.GenerateKeyPair();

            var certificateGenerator = new X509V3CertificateGenerator();
            certificateGenerator.SetSerialNumber(BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random));
            certificateGenerator.SetIssuerDN(new X509Name(subject)); // self signed
            certificateGenerator.SetSubjectDN(new X509Name(subject));
            certificateGenerator.SetNotBefore(DateTime.UtcNow.Date);
            certificateGenerator.SetNotAfter(DateTime.UtcNow.Date.AddYears(2));
            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA256WithRSA", subjectKeyPair.Private /*self signed*/, random);
            var certificate = new X509Certificate2(certificateGenerator.Generate(signatureFactory).GetEncoded());

            // destructure private key
            var rsa = RsaPrivateKeyStructure.GetInstance(PrivateKeyInfoFactory.CreatePrivateKeyInfo(subjectKeyPair.Private).ParsePrivateKey());
            certificate.PrivateKey = DotNetUtilities.ToRSA(new RsaPrivateCrtKeyParameters(
                rsa.Modulus, rsa.PublicExponent, rsa.PrivateExponent, rsa.Prime1, rsa.Prime2, rsa.Exponent1, rsa.Exponent2, rsa.Coefficient));

            return certificate;
        }
    }
}
