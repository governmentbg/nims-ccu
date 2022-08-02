using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Eumis.Components
{
    /// <summary>
    /// Представлява електронен подпис издаден от InfoNotary.
    /// </summary>
    public class UesInfoNotary : UesBase
    {
        #region Constructors

        public UesInfoNotary(X509Certificate2 certificate)
            : base(certificate)
        { }

        #endregion

        #region Overrides

        protected override bool CheckIsPersonal()
        {
            return MatchRegexInCertificatePolicies(IsPersonalRegex);
        }

        protected override bool CheckIsCompany()
        {
            return MatchRegexInCertificatePolicies(IsCompanyRegex);
        }

        protected override string GetName()
        {
            if (this.NameRegex.IsMatch(this.Certificate.Subject))
            {
                Match match = this.NameRegex.Match(this.Certificate.Subject);
                return match.Groups["name"].Value.Trim();
            }
            else
            {
                return String.Empty;
            }
        }

        protected override string GetPersonalIdentifier()
        {
            string returnValue = String.Empty;

            X509Extension extension = this.Certificate.Extensions["Subject Alternative Name"];
            if (extension != null)
            {
                if (this.EgnRegex.IsMatch(extension.Format(true)))
                {
                    Match match = this.EgnRegex.Match(extension.Format(true));
                    return match.Groups["egn"].Value.Trim();
                }
            }

            return returnValue;
        }

        #endregion

        #region Regex

        private readonly Regex EgnRegex = new Regex(@"OID.2.5.4.3.100.1.1=(?<egn>\d+)", RegexOptions.Compiled);

        private readonly Regex NameRegex = new Regex(@"CN=(?<name>.*?)\+", RegexOptions.Compiled);

        private readonly Regex IsPersonalRegex = new Regex(@"1.3.6.1.4.1.22144.1.1.1.1", RegexOptions.Compiled);

        private readonly Regex IsCompanyRegex = new Regex(@"1.3.6.1.4.1.22144.1.1.2.1", RegexOptions.Compiled);
        

        #endregion
    }
}

