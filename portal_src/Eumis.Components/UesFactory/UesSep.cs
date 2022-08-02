using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Eumis.Components
{
    /// <summary>
    /// Представлява електронен подпис издаден от SEP Bulgaria (MobiSafe)
    /// 
    ///  Всички интересуващи Ви данни може да получите от “Subject DN” на УЕП.
    ///  1.	Поле UID = EGNxxxxxxxxxx – това поле съдържа информация за ЕГН, форматирана по указания начин
    ///  2.	Поле OU = EIKxxxxxxxx - това поле съдържа информация за ЕИК, форматирана по указания начин
    ///  3.	Допълнително поле OU = MobiSafe Private(Organization, Profession) - това поле съдържа информация за типа на сертификата, форматирана по указания начин съответно Физическо лице, Фирма, Свободна професия
    ///  Не съществува възможност да бъдат скрити полетата за ЕГН и ЕИК. Те се използват от всички държавни институции за идентифициране.
    /// </summary>
    public class UesSep : UesBase
    {
        #region Constructors

        public UesSep(X509Certificate2 certificate)
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

        private readonly Regex NameRegex = new Regex(@"CN=(?<name>.*?),", RegexOptions.Compiled);

        private readonly Regex EgnRegex1 = new Regex(@"UID=EGN(?<egn>\d+)", RegexOptions.Compiled);
        private readonly Regex EgnRegex2 = new Regex(@"OID.0.9.2342.19200300.100.1.1=EGN(?<egn>\d+)", RegexOptions.Compiled);

        private readonly Regex IsPersonalRegex1 = new Regex(@"1.3.6.1.4.1.30299.2.1.1", RegexOptions.Compiled);
        private readonly Regex IsPersonalRegex2 = new Regex(@"1.3.6.1.4.1.30299.2.1.3", RegexOptions.Compiled);
        private readonly Regex IsPersonalRegex3 = new Regex(@"1.3.6.1.4.1.30299.2.5.1", RegexOptions.Compiled);

        private readonly Regex IsCompanyRegex = new Regex(@"1.3.6.1.4.1.30299.2.1.2", RegexOptions.Compiled);
        

        #endregion        
    }
}