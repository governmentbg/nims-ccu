using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Auth
{
    public class AesDataProtector : Microsoft.Owin.Security.DataProtection.IDataProtector
    {
        #region Static

        private byte[] key;
        private byte[] preamble;

        #endregion Static

        #region Constructors

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "May be used in the future.")]
        public AesDataProtector(string appname, string key, string preamble)
        {
            using (var sha1 = new System.Security.Cryptography.SHA256Managed())
            {
                this.key = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
                this.preamble = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(preamble));
            }
        }

        #endregion Constructors

        #region IDataProtector Methods

        public byte[] Protect(byte[] data)
        {
            byte[] dataHash;
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                dataHash = sha.ComputeHash(data);
            }

            using (System.Security.Cryptography.AesManaged aesAlg = new System.Security.Cryptography.AesManaged())
            {
                aesAlg.Key = this.key;
                aesAlg.GenerateIV();

                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    msEncrypt.Write(this.preamble, 0, 32);
                    msEncrypt.Write(aesAlg.IV, 0, 16);

                    using (var csEncrypt = new System.Security.Cryptography.CryptoStream(msEncrypt, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                    using (var bwEncrypt = new System.IO.BinaryWriter(csEncrypt))
                    {
                        bwEncrypt.Write(dataHash);
                        bwEncrypt.Write(data.Length);
                        bwEncrypt.Write(data);
                    }

                    var protectedData = msEncrypt.ToArray();
                    return protectedData;
                }
            }
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            using (System.Security.Cryptography.AesManaged aesAlg = new System.Security.Cryptography.AesManaged())
            {
                aesAlg.Key = this.key;

                using (var msDecrypt = new System.IO.MemoryStream(protectedData))
                {
                    byte[] p = new byte[32];
                    msDecrypt.Read(p, 0, 32);

                    if (!Enumerable.SequenceEqual(p, this.preamble))
                    {
                        throw new System.Security.SecurityException("Incorrect preamble!");
                    }

                    byte[] iv = new byte[16];
                    msDecrypt.Read(iv, 0, 16);

                    aesAlg.IV = iv;

                    using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    using (var csDecrypt = new System.Security.Cryptography.CryptoStream(msDecrypt, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                    using (var brDecrypt = new System.IO.BinaryReader(csDecrypt))
                    {
                        var signature = brDecrypt.ReadBytes(32);
                        var len = brDecrypt.ReadInt32();
                        var data = brDecrypt.ReadBytes(len);

                        byte[] dataHash;
                        using (var sha = new System.Security.Cryptography.SHA256Managed())
                        {
                            dataHash = sha.ComputeHash(data);
                        }

                        if (!dataHash.SequenceEqual(signature))
                        {
                            throw new System.Security.SecurityException("Signature does not match the computed hash");
                        }

                        return data;
                    }
                }
            }
        }

        #endregion IDataProtector Methods
    }
}
