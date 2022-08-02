using System.Security.Cryptography.X509Certificates;

namespace Eumis.Components
{
    /// <summary>
    /// Factory клас за Ues обекти.
    /// </summary>
    public class UesFactoryImpl : IUesFactory
    {
        #region Public Methods

        /// <summary>
        /// Връща конкретна имплементация на универсален електронен подпис в зависимост от издателя.
        /// </summary>
        public UesBase GetUes(X509Certificate2 certificate)
        {
            if (certificate.Issuer.Contains(BTrust))
            {
                return new UesBTrust(certificate);
            }
            else if (certificate.Issuer.Contains(StampIT))
            {
                return new UesStampIT(certificate);
            }
            else if (certificate.Issuer.Contains(Spektar))
            {
                return new UesSpektar(certificate);
            }
            if (certificate.Issuer.Contains(InfoNotary))
            {
                return new UesInfoNotary(certificate);
            }
            else if (certificate.Issuer.Contains(Sep))
            {
                return new UesSep(certificate);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Consts

        
        private const string BTrust = "B-Trust";
        private const string StampIT = "StampIT";
        private const string Spektar = "Spektar";
        private const string InfoNotary = "InfoNotary";
        private const string Sep = "SEP Bulgaria";

        #endregion
    }
}