using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Ionic.Zip;
using Eumis.Common.Config;

namespace Eumis.Portal.Web.Helpers
{
    public static class ZipManager
    {
        public static byte[] ZipProject(string xml, string entryName)
        {
            byte[] content = Eumis.Components.DocumentSerializerSettings.DefaultEncoding.GetBytes(xml);

            byte[] zipContent = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipFile zf = new ZipFile())
                {
                    zf.Password = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:ZipPassword");
                    zf.Encryption = EncryptionAlgorithm.WinZipAes256;
                    zf.AddEntry(entryName, content);
                    zf.Save(ms);
                }

                zipContent = ms.ToArray();
            }

            return zipContent;
        }

        public static string UnzipProject(Stream zip)
        {
            string xml;

            if (ZipFile.IsZipFile(zip, false))
            {
                zip.Position = 0;
                using (ZipFile zf = ZipFile.Read(zip))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        if (zf.Count() == 1)
                        {
                            zf[0].Password = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:ZipPassword");
                            zf[0].Encryption = EncryptionAlgorithm.WinZipAes256;

                            try
                            {
                                zf[0].Extract(ms);
                            }
                            catch
                            {
                                if (Constants.UseDeprecatedZipPassword)
                                {
                                    zf[0].Password = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:DeprecatedZipPassword");
                                }

                                zf[0].Extract(ms);
                            }
                        }

                        ms.Position = 0;
                        using (StreamReader sr = new StreamReader(ms, true))
                        {
                            xml = sr.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(zip, true))
                {
                    xml = sr.ReadToEnd();
                }
            }

            return xml;
        }
    }
}