using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Eumis.Database.Configurator.Helpers
{
    public class DeterministicGuidGenerator
    {
        private int nextGuid = 1;

        public void ResetDeterministicGuidCounter()
        {
            this.nextGuid = 1;
        }

        [SuppressMessage("", "CA5350:DoNotUseWeakCryptographicAlgorithms", Justification = "Not used for security")]
        public Guid GetNextDeterministicGuid()
        {
            byte[] hashedBytes = new SHA1CryptoServiceProvider().ComputeHash(BitConverter.GetBytes(this.nextGuid++));
            Array.Resize(ref hashedBytes, 16);
            return new Guid(hashedBytes);
        }
    }
}
