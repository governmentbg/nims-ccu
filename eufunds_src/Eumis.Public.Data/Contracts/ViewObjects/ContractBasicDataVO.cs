using Eumis.Public.Common;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;
using System.Collections.Generic;
using UinTypeEnum = Eumis.Public.Domain.Entities.Umis.NonAggregates.UinType;

namespace Eumis.Public.Data.Contracts.ViewObjects
{
    public class ContractBasicDataVO : ContractCommonVO
    {
        private string companyName;
        private string companyNameAlt;

        public string RegNumber { get; set; }

        public string Name { get; set; }

        public string NameEN { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.NameEN) ? this.Name : this.NameEN;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public string CompanyName
        {
            get => this.CompanyUinType == UinTypeEnum.PersonalBulstat ? Helper.AnonymizeName(this.companyName) : this.companyName;
            set => this.companyName = value;
        }

        public string CompanyNameAlt
        {
            get => this.CompanyUinType == UinTypeEnum.PersonalBulstat ? Helper.AnonymizeName(this.companyNameAlt) : this.companyNameAlt;
            set => this.companyNameAlt = value;
        }

        public string TransCompanyName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.CompanyNameAlt) ? this.CompanyName : this.CompanyNameAlt;
                }
                else
                {
                    return this.CompanyName;
                }
            }
        }

        public string TransCompanyFullName
        {
            get
            {
                if (this.CompanyUinType == UinTypeEnum.PersonalBulstat)
                {
                    return this.TransCompanyName;
                }
                else
                {
                    return this.CompanyUin + " " + this.TransCompanyName;
                }
            }
        }

        public string CompanyUin { get; set; }

        public UinTypeEnum CompanyUinType { get; set; }

        public IEnumerable<string> Funds { get; set; }

        public int ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameEN { get; set; }

        public string TransProgrammeName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProgrammeNameEN) ? this.ProgrammeName : this.ProgrammeNameEN;
                }
                else
                {
                    return this.ProgrammeName;
                }
            }
        }

        public DateTime? ContractDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public ContractExecutionStatus? ExecutionStatus { get; set; }

        public string StatusDescription
        {
            get
            {
                return this.ExecutionStatus.HasValue ? this.ExecutionStatus.GetEnumDescription() : string.Empty;
            }
        }

        public IEnumerable<string> NutsFullPathNames { get; set; }

        public IEnumerable<string> NutsFullPathNamesEN { get; set; }

        public IEnumerable<string> TransNutsFullPathNames
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.NutsFullPathNamesEN;
                }

                return this.NutsFullPathNames;
            }
        }

        public string ProcedureCode { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameEn { get; set; }

        public string ProcedureTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English && !string.IsNullOrEmpty(this.ProcedureNameEn))
                {
                    return this.ProcedureNameEn;
                }

                return this.ProcedureName;
            }
        }

        public Guid ProcedureGid { get; set; }

        public ProcedureStatus ProcedureStatus { get; set; }

        public string ProcedureInfoUrl
        {
            get
            {
                if (this.ProcedureStatus == ProcedureStatus.Active)
                {
                    return $"{Configuration.PortalLocation}/{SystemLocalization.GetCurrentCulture()}/s/Procedure/Info/{this.ProcedureGid}";
                }

                return $"{Configuration.PortalLocation}/{SystemLocalization.GetCurrentCulture()}/s/Procedure/InfoEnded/{this.ProcedureGid}";
            }
        }
    }
}
