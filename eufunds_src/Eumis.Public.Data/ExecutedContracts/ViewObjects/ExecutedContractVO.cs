using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;

namespace Eumis.Public.Data.ExecutedContracts.ViewObjects
{
    public class ExecutedContractVO
    {
        private string companyUin;
        private string companyName;
        private string companyNameAlt;

        public int ContractId { get; set; }

        public string ContractName { get; set; }

        public string ContractNameAlt { get; set; }

        public virtual string ContractTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.ContractNameAlt;
                }
                else
                {
                    return this.ContractName;
                }
            }
        }

        public string ContractRegNumber { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameAlt { get; set; }

        public virtual string ProgrammeTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.ProgrammeNameAlt;
                }
                else
                {
                    return this.ProgrammeName;
                }
            }
        }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public virtual string ProcedureTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.ProcedureNameAlt;
                }
                else
                {
                    return this.ProcedureName;
                }
            }
        }

        public string CompanyUin
        {
            get => this.CompanyUinType == UinType.PersonalBulstat ? string.Empty : this.companyUin;
            set => this.companyUin = value;
        }

        public UinType? CompanyUinType { get; set; }

        public string CompanyName
        {
            get => this.CompanyUinType == UinType.PersonalBulstat ? Helper.AnonymizeName(this.companyName) : this.companyName;
            set => this.companyName = value;
        }

        public string CompanyNameAlt
        {
            get => this.CompanyUinType == UinType.PersonalBulstat ? Helper.AnonymizeName(this.companyNameAlt) : this.companyNameAlt;
            set => this.companyNameAlt = value;
        }

        public virtual string CompanyTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.CompanyNameAlt;
                }
                else
                {
                    return this.CompanyName;
                }
            }
        }

        public string CompanyTypeName { get; set; }

        public string CompanyTypeNameAlt { get; set; }

        public virtual string CompanyTypeTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.CompanyTypeNameAlt;
                }
                else
                {
                    return this.CompanyTypeName;
                }
            }
        }

        public string CompanyLegalTypeName { get; set; }

        public string CompanyLegalTypeNameAlt { get; set; }

        public virtual string CompanyLegalTypeTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.CompanyLegalTypeNameAlt;
                }
                else
                {
                    return this.CompanyLegalTypeName;
                }
            }
        }

        public string CompanySizeTypeName { get; set; }

        public string CompanySizeTypeNameAlt { get; set; }

        public virtual string CompanySizeTypeTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.CompanySizeTypeNameAlt;
                }
                else
                {
                    return this.CompanySizeTypeName;
                }
            }
        }

        public int? ContractDuration { get; set; }

        public DateTime? InitialContractDate { get; set; }

        public DateTime? InitialCompletionDate { get; set; }

        public DateTime? ActualCompletionDate { get; set; }

        public ContractExecutionStatus? ContractExecutionStatus { get; set; }
    }
}
