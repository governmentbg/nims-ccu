using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes;
using Eumis.Public.Resources;
using System;
using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class IndicativeAnnualWorkingProgrammeTableVO
    {
        public int IndicativeAnnualWorkingProgrammeTableId { get; set; }

        public int ProcedureId { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProgrammePriorityNameAlt { get; set; }

        public int OrderNum { get; set; }

        public string ProcedureCode { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string ProcedureDescription { get; set; }

        public string ProcedureDescriptionAlt { get; set; }

        public IndicativeAnnualWorkingProgrammeTypeConducting IndicativeAnnualWorkingProgrammeTypeConducting { get; set; }

        public bool WithPreSelection { get; set; }

        public IList<string> IndicativeAnnualWorkingProgrammeTableProgrammes { get; set; }

        public IList<string> IndicativeAnnualWorkingProgrammeTableProgrammesAlt { get; set; }

        public string LeadingProgram { get; set; }

        public string LeadingProgramAlt { get; set; }

        public decimal ProcedureTotalAmount { get; set; }

        public IList<string> IndicativeAnnualWorkingProgrammeTableCandidates { get; set; }

        public IList<string> IndicativeAnnualWorkingProgrammeTableCandidatesAlt { get; set; }

        public string EligibleActivities { get; set; }

        public string EligibleActivitiesAlt { get; set; }

        public string EligibleCosts { get; set; }

        public string EligibleCostsAlt { get; set; }

        public decimal MaxPercentCoFinancing { get; set; }

        public string MaxPercentCoFinancingInfo { get; set; }

        public string MaxPercentCoFinancingInfoAlt { get; set; }

        public DateTime ListingDate { get; set; }

        public IList<DateTime> IndicativeAnnualWorkingProgrammeTableTimeLimits { get; set; }

        public IndicativeAnnualWorkingProgrammeAssistance IsStateAssistance { get; set; }

        public IndicativeAnnualWorkingProgrammeAssistance IsMinimalAssistance { get; set; }

        public decimal ProjectMinAmount { get; set; }

        public string ProjectMinAmountInfo { get; set; }

        public string ProjectMinAmountInfoAlt { get; set; }

        public decimal ProjectMaxAmount { get; set; }

        public string ProjectMaxAmountInfo { get; set; }

        public string ProjectMaxAmountInfoAlt { get; set; }

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

        public string TransProcedureDescription
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProcedureDescriptionAlt) ? this.ProcedureDescription : this.ProcedureDescriptionAlt;
                }
                else
                {
                    return this.ProcedureDescription;
                }
            }
        }

        public string TransProgrammePriorityName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProgrammePriorityNameAlt) ? this.ProgrammePriorityName : this.ProgrammePriorityNameAlt;
                }
                else
                {
                    return this.ProgrammePriorityName;
                }
            }
        }

        public string TransLeadingProgram
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.LeadingProgramAlt) ? this.LeadingProgram : this.LeadingProgramAlt;
                }
                else
                {
                    return this.LeadingProgram;
                }
            }
        }

        public string TransEligibleActivities
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.EligibleActivitiesAlt) ? this.EligibleActivities : this.EligibleActivitiesAlt;
                }
                else
                {
                    return this.EligibleActivities;
                }
            }
        }

        public string TransEligibleCosts
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.EligibleCostsAlt) ? this.EligibleCosts : this.EligibleCostsAlt;
                }
                else
                {
                    return this.EligibleCosts;
                }
            }
        }

        public string TransMaxPercentCoFinancingInfo
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.MaxPercentCoFinancingInfoAlt) ? this.MaxPercentCoFinancingInfo : this.MaxPercentCoFinancingInfoAlt;
                }
                else
                {
                    return this.MaxPercentCoFinancingInfo;
                }
            }
        }

        public string TransProjectMinAmountInfo
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProjectMinAmountInfoAlt) ? this.ProjectMinAmountInfo : this.ProjectMinAmountInfoAlt;
                }
                else
                {
                    return this.ProjectMinAmountInfo;
                }
            }
        }

        public string TransProjectMaxAmountInfo
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProjectMaxAmountInfoAlt) ? this.ProjectMaxAmountInfo : this.ProjectMaxAmountInfoAlt;
                }
                else
                {
                    return this.ProjectMaxAmountInfo;
                }
            }
        }

        public IList<string> TransIndicativeAnnualWorkingProgrammeTableProgrammes
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.IndicativeAnnualWorkingProgrammeTableProgrammesAlt == null ? this.IndicativeAnnualWorkingProgrammeTableProgrammes : this.IndicativeAnnualWorkingProgrammeTableProgrammesAlt;
                }
                else
                {
                    return this.IndicativeAnnualWorkingProgrammeTableProgrammes;
                }
            }
        }

        public IList<string> TransIndicativeAnnualWorkingProgrammeTableCandidates
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.IndicativeAnnualWorkingProgrammeTableCandidatesAlt == null ? this.IndicativeAnnualWorkingProgrammeTableCandidates : this.IndicativeAnnualWorkingProgrammeTableCandidatesAlt;
                }
                else
                {
                    return this.IndicativeAnnualWorkingProgrammeTableCandidates;
                }
            }
        }

        public string WithPreSelectionText
        {
            get
            {
                return this.WithPreSelection ? Texts.IndicativeAnnualWorkingProgrammes_Table_YesText : Texts.IndicativeAnnualWorkingProgrammes_Table_NoText;
            }
        }
    }
}
