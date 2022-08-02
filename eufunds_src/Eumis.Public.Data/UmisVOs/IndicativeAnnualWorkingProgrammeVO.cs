using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes;
using System;

namespace Eumis.Public.Data.UmisVOs
{
    public class IndicativeAnnualWorkingProgrammeVO
    {
        public int IndicativeAnnualWorkingProgrammeId { get; set; }

        public int ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameAlt { get; set; }

        public DateTime PublicationDate { get; set; }

        public int OrderVersionNum { get; set; }

        public IndicativeAnnualWorkingProgrammeYear Year { get; set; }

        public IndicativeAnnualWorkingProgrammeType Type { get; set; }

        public IndicativeAnnualWorkingProgrammeStatus Status { get; set; }

        public string TransProgrammeName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProgrammeNameAlt) ? this.ProgrammeName : this.ProgrammeNameAlt;
                }
                else
                {
                    return this.ProgrammeName;
                }
            }
        }

        public string EncryptedIawpId
        {
            get
            {
                return ConfigurationBasedStringEncrypter.Encrypt(this.IndicativeAnnualWorkingProgrammeId.ToString());
            }
        }

        public string EncryptedType
        {
            get
            {
                return ConfigurationBasedStringEncrypter.Encrypt(((int)this.Type).ToString());
            }
        }
    }
}
