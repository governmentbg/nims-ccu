using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Eumis.Components
{
    /// <summary>
    /// Представлява електронен подпис издаден от B-Trust
    /// 
    /// Професионален УЕП на българска фирма с автор български  гражданин: 
    /// OU=BULSTAT:xxxxxxxxxxxxx 
    /// S=…………..,EGN:xxxxxxxxxx 
    /// 
    /// Професионален УЕП на българска фирма с автор чужд  гражданин: 
    /// OU=BULSTAT:xxxxxxxxxxxxx 
    /// S=…………..,PID:ЛНЧ/номер на документ за самоличност
    /// 
    /// Професионален УЕП на чужда фирма с автор български  гражданин: 
    /// OU=BULSTAT:остава празно 
    /// OU=SR:еквивалентен на ЕИК номер
    /// S=…………..,EGN:xxxxxxxxxx 
    /// 
    /// Професионален УЕП на чужда фирма с автор чужд  гражданин: 
    /// OU=BULSTAT:остава празно 
    /// OU=SR:еквивалентен на ЕИК номер
    /// S=…………..,PID:ЛНЧ/номер на документ за самоличност
    /// 
    /// Персонален УЕП на български  гражданин: 
    /// OU=EGN:xxxxxxxxxx 
    /// S=…………..,EGN:xxxxxxxxxx 
    /// 
    /// Персонален УЕП на чужд гражданин: 
    /// OU=PID:ЛНЧ/номер на документ за самоличност 
    /// S=…………..,PID:ЛНЧ/номер на документ за самоличност
    ///     
    /// УЕП за свободна професия на български гражданин
    /// OU=EGN:xxxxxxxxxx 
    /// OU=BULSTAT: xxxxxxxxxxxxx 
    /// S=…………..,EGN:xxxxxxxxxx 
    /// </summary>
    public class UesBTrust : UesBase
    {
        #region Constructors

        public UesBTrust(X509Certificate2 certificate)
            : base(certificate)
        { }

        #endregion        

        #region Overrides

        protected override bool CheckIsPersonal()
        {
            return MatchRegexInCertificatePolicies(IsPersonalRegex_Policies) && IsPersonalRegex_Subject.IsMatch(this.Certificate.Subject);
        }

        protected override bool CheckIsCompany()
        {
            return MatchRegexInCertificatePolicies(IsCompanyRegex_Policies) && IsCompanyRegex_Subject.IsMatch(this.Certificate.Subject);
        }

        protected override string GetPersonalIdentifier()
        {
            if (IsPersonal)
            {
                if (this.EgnRegex_Personal1.IsMatch(this.Certificate.Subject))
                {
                    Match match = this.EgnRegex_Personal1.Match(this.Certificate.Subject);
                    return match.Groups["egn"].Value.Trim();
                }
                else if (this.EgnRegex_Personal2.IsMatch(this.Certificate.Subject))
                {
                    Match match = this.EgnRegex_Personal2.Match(this.Certificate.Subject);
                    return match.Groups["egn"].Value.Trim();
                }
                else
                {
                    return String.Empty;
                }
            }
            else
            {
                if (this.EgnRegex_Company1.IsMatch(this.Certificate.Subject))
                {
                    Match match = this.EgnRegex_Company1.Match(this.Certificate.Subject);
                    return match.Groups["egn"].Value.Trim();
                }
                else if (this.EgnRegex_Company2.IsMatch(this.Certificate.Subject))
                {
                    Match match = this.EgnRegex_Company2.Match(this.Certificate.Subject);
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

        private readonly Regex EgnRegex_Personal1 = new Regex(@"OU=EGN:(?<egn>\d+)", RegexOptions.Compiled);
        private readonly Regex EgnRegex_Personal2 = new Regex(@"S=EGN:(?<egn>\d+)", RegexOptions.Compiled);
        private readonly Regex EgnRegex_Company1 = new Regex(@"S="".*EGN:(?<egn>\d+).*""", RegexOptions.Compiled);
        private readonly Regex EgnRegex_Company2 = new Regex(@"S=EGN:(?<egn>\d+)", RegexOptions.Compiled);

        private readonly Regex NameRegex = new Regex(@"CN=(?<name>.*?),", RegexOptions.Compiled);

        private readonly Regex IsPersonalRegex_Policies = new Regex(@"1.3.6.1.4.1.15862.1.5.1.1", RegexOptions.Compiled);
        private readonly Regex IsPersonalRegex_Subject = new Regex(@"OU=EGN:", RegexOptions.Compiled);

        private readonly Regex IsCompanyRegex_Policies = new Regex(@"1.3.6.1.4.1.15862.1.5.1.1", RegexOptions.Compiled);
        private readonly Regex IsCompanyRegex_Subject = new Regex(@"OU=BULSTAT:", RegexOptions.Compiled);

        #endregion                
    }
}