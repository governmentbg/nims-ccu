using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Eumis.Components
{
    /// <summary>
    /// Представлява базов клас за универсален електронен подпис.
    /// </summary>
    public abstract class UesBase
    {
        #region Constructors

        public UesBase(X509Certificate2 certificate)
        {
            this.Certificate = certificate;
            this.IsPersonal = CheckIsPersonal();
            this.IsCompany = CheckIsCompany();
            this.PersonalIdentifier = GetPersonalIdentifier();
            this.Name = GetName();
        }

        protected UesBase(X509Certificate2 certificate, string personalIdentifier, string name)
        {
            this.Certificate = certificate;
            this.IsPersonal = CheckIsPersonal();
            this.IsCompany = CheckIsCompany();
            this.PersonalIdentifier = personalIdentifier;
            this.Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Връща сертификата асоцииран с този електронен подпис.
        /// </summary>
        public X509Certificate2 Certificate { get; private set; }

        public bool IsPersonal { get; private set; }

        public bool IsCompany { get; private set; }

        /// <summary>
        /// Връща ЕГН на лицето, което е собственик на подписа.
        /// </summary>
        public string PersonalIdentifier { get; private set; }

        /// <summary>
        /// Име на собственика на подписа.
        ///</summary>
        public string Name { get; private set; }

        /// <summary>
        /// Указва дали в подписа са намерени данни за фирма или физическо лице.
        /// </summary>
        public bool HasPersonalIdentifier
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.PersonalIdentifier);
            }
        }

        protected bool MatchRegexInCertificatePolicies(Regex regex)
        {
            bool returnValue = false;

            X509Extension extension = this.Certificate.Extensions["Certificate Policies"];
            if (extension != null)
            {
                if (regex.IsMatch(extension.Format(true)))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        #endregion

        #region Abstract Methods

        protected abstract bool CheckIsPersonal();

        protected abstract bool CheckIsCompany();

        protected abstract string GetPersonalIdentifier();

        protected abstract string GetName();

        #endregion
    }
}