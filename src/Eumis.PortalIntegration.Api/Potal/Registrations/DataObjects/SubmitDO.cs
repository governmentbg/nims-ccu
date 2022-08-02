using Eumis.Common.Config;
using Eumis.Common.Crypto;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Eumis.PortalIntegration.Api.Portal.Registrations.DataObjects
{
    public class SubmitDO
    {
        public byte[] Isun { get; set; }

        public IList<byte[]> Signatures { get; set; }

        public byte[] Version { get; set; }

        public string UnzipData()
        {
            using (MemoryStream zipStream = new MemoryStream(this.Isun))
            {
                if (!ZipFile.IsZipFile(zipStream, false))
                {
                    throw new Exception("Cannot read isun file.");
                }

                zipStream.Position = 0;
                using (ZipFile zf = ZipFile.Read(zipStream))
                {
                    if (zf.Count != 1)
                    {
                        throw new Exception("Incorrect isun file format.");
                    }

                    using (MemoryStream ms = new MemoryStream())
                    {
                        zf[0].Password = ConfigurationManager.AppSettings.GetWithEnv("Eumis.PortalIntegration.Host:ZipPassword");
                        zf[0].Encryption = EncryptionAlgorithm.WinZipAes256;

                        try
                        {
                            zf[0].Extract(ms);
                        }
                        catch
                        {
                            bool useDeprecatedZipPassword = bool.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.PortalIntegration.Host:UseDeprecatedZipPassword"));
                            if (useDeprecatedZipPassword)
                            {
                                zf[0].Password = ConfigurationManager.AppSettings.GetWithEnv("Eumis.PortalIntegration.Host:DeprecatedZipPassword");
                            }

                            zf[0].Extract(ms);
                        }

                        ms.Position = 0;
                        using (StreamReader sr = new StreamReader(ms, true))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        public bool IsValid()
        {
            if (this.Isun == null ||
                this.Isun.Length == 0)
            {
                return false;
            }

            if (this.Signatures == null ||
                this.Signatures.Count == 0 ||
                this.Signatures.Any(sig => sig == null || sig.Length == 0))
            {
                return false;
            }

            foreach (var signature in this.Signatures)
            {
                if (!CryptoUtils.IsDetachedSignatureValid(this.Isun, signature))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
