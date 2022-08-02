using Eumis.Public.Common;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;

namespace Eumis.Public.Data.Procedures.ViewObjects
{
    public class ProcedureVO
    {
        public int ProcedureId { get; set; }

        public Guid ProcedureGid { get; set; }

        public ProcedureStatus Status { get; set; }

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

        public DateTime EndingDate { get; set; }

        public decimal BudgetTotal { get; set; }

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

        public string Candidates { get; set; }

        public string EligibleActivities { get; set; }

        public string EligibleCosts { get; set; }

        public decimal MaxPercentCoFinancing { get; set; }

        public string GetUrl()
        {
            return $"{Configuration.PortalLocation}/{SystemLocalization.GetCurrentCulture()}/s/Procedure/Info/{this.ProcedureGid}";
        }
    }
}
