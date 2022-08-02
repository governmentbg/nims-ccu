using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Eumis.Components
{
    public class UesStampIT : UesBase
    {
        #region Constructors

        public UesStampIT(X509Certificate2 certificate)
            : base(certificate)
        { }

        #endregion        

        #region Overrides

        protected override bool CheckIsPersonal()
        {
            return
                MatchRegexInCertificatePolicies(IsPersonalRegex1) ||
                MatchRegexInCertificatePolicies(IsPersonalRegex2);
        }

        protected override bool CheckIsCompany()
        {
            return MatchRegexInCertificatePolicies(IsCompanyRegex);
        }

        protected override string GetPersonalIdentifier()
        {
            if (this.EgnRegex1.IsMatch(this.Certificate.Subject))
            {
                Match match = this.EgnRegex1.Match(this.Certificate.Subject);
                return match.Groups["egn"].Value.Trim();
            }
            else if (this.EgnRegex2.IsMatch(this.Certificate.Subject))
            {
                Match match = this.EgnRegex2.Match(this.Certificate.Subject);
                return match.Groups["egn"].Value.Trim();
            }
            else
            {
                return String.Empty;
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

        private readonly Regex EgnRegex1 = new Regex(@"S=EGN:(?<egn>\d+)", RegexOptions.Compiled);
        private readonly Regex EgnRegex2 = new Regex(@"S="".*EGN:(?<egn>\d+).*""", RegexOptions.Compiled);

        private readonly Regex NameRegex = new Regex(@"CN=(?<name>.*?),", RegexOptions.Compiled);

        private readonly Regex IsPersonalRegex1 = new Regex(@"1.3.6.1.4.1.11290.1.1.1.1", RegexOptions.Compiled);
        private readonly Regex IsPersonalRegex2 = new Regex(@"1.3.6.1.4.1.11290.1.1.1.5", RegexOptions.Compiled);

        private readonly Regex IsCompanyRegex = new Regex(@"1.3.6.1.4.1.11290.1.1.1.4", RegexOptions.Compiled);
        
        #endregion                
    }
}