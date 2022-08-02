using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.EvalSessions;
using System;

namespace Eumis.Public.Data.UmisVOs
{
    public class EvalSessionResultVO
    {
        public int EvalSessionResultId { get; set; }

        public string EvalSessionNum { get; set; }

        public EvalSessionResultType Type { get; set; }

        public EvalSessionResultStatus Status { get; set; }

        public DateTime PublicationDate { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string TransProcedureName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProcedureNameAlt) ? this.ProcedureName : this.ProcedureNameAlt;
                }
                else
                {
                    return this.ProcedureName;
                }
            }
        }

        public string EncryptedResultId
        {
            get
            {
                return ConfigurationBasedStringEncrypter.Encrypt(this.EvalSessionResultId.ToString());
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
