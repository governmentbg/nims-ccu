using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Eumis.Components
{
    public class UesSpektar : UesBase
    {
        #region Constructors

        public UesSpektar(X509Certificate2 certificate)
            : base(certificate)
        { }

        #endregion        

        #region Overrides

        protected override bool CheckIsPersonal()
        {
            return
                MatchRegexInCertificatePolicies(IsPersonalRegex1) ||
                MatchRegexInCertificatePolicies(IsPersonalRegex2) ||
                MatchRegexInCertificatePolicies(IsPersonalRegex3);
        }

        protected override bool CheckIsCompany()
        {
            return
                MatchRegexInCertificatePolicies(IsCompanyRegex1) ||
                MatchRegexInCertificatePolicies(IsCompanyRegex2) ||
                MatchRegexInCertificatePolicies(IsCompanyRegex3);
        }

        protected override string GetPersonalIdentifier()
        {
            if (IsPersonal)
            {
                if (this.EgnRegex_Personal.IsMatch(this.Certificate.Subject))
                {
                    Match match = this.EgnRegex_Personal.Match(this.Certificate.Subject);
                    return match.Groups["egn"].Value.Trim();
                }
                else
                {
                    return String.Empty;
                }
            }
            else
            {
                if (this.EgnRegex_Company.IsMatch(this.Certificate.Subject))
                {
                    Match match = this.EgnRegex_Company.Match(this.Certificate.Subject);
                    return match.Groups["egn"].Value.Trim();
                }
                else
                {
                    return String.Empty;
                }
            }
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

        #endregion

        #region Regex

        private readonly Regex EgnRegex_Personal = new Regex(@"OU=EGNT:(?<egn>\d+)", RegexOptions.Compiled);
        private readonly Regex EgnRegex_Company = new Regex(@"T="".*EGN:(?<egn>\d+).*""", RegexOptions.Compiled);


        private readonly Regex NameRegex = new Regex(@"CN=(?<name>.*?),", RegexOptions.Compiled);

        private readonly Regex IsPersonalRegex1 = new Regex(@"1.3.6.1.4.1.18463.1.1.1.1", RegexOptions.Compiled);
        private readonly Regex IsPersonalRegex2 = new Regex(@"1.3.6.1.4.1.18463.1.1.1.2", RegexOptions.Compiled);
        private readonly Regex IsPersonalRegex3 = new Regex(@"1.3.6.1.4.1.18463.1.1.1.5", RegexOptions.Compiled);

        private readonly Regex IsCompanyRegex1 = new Regex(@"1.3.6.1.4.1.18463.1.1.1.3", RegexOptions.Compiled);
        private readonly Regex IsCompanyRegex2 = new Regex(@"1.3.6.1.4.1.18463.1.1.1.4", RegexOptions.Compiled);
        private readonly Regex IsCompanyRegex3 = new Regex(@"1.3.6.1.4.1.18463.1.1.1.6", RegexOptions.Compiled);

        #endregion                
    }
}