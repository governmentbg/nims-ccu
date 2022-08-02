using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class NewProcedureDO
    {
        public NewProcedureDO()
        {
            this.Procedure = new ProcedureDO();
            this.ProcedureShare = new ProcedureShareDO();
            this.ProcedureTimeLimit = new ProcedureTimeLimitDO();
        }

        public NewProcedureDO(int programmeId, int programmePriorityId, bool isPrimary, string code)
        {
            this.Procedure = new ProcedureDO()
            {
                Code = code,
                ProcedureStatus = ProcedureStatus.Draft,
                ApplicationFormType = ApplicationFormType.Standard,
                Description = string.Empty,
                DescriptionAlt = string.Empty,
                ProcedureKind = ProcedureKind.Budget,
                AllowedRegistrationType = AllowedRegistrationType.Digital,
            };
            this.ProcedureShare = new ProcedureShareDO()
            {
                ProgrammeId = programmeId,
                ProgrammePriorityId = programmePriorityId,
                IsPrimary = isPrimary,
            };
            this.ProcedureTimeLimit = new ProcedureTimeLimitDO();
        }

        public ProcedureDO Procedure { get; set; }

        public ProcedureShareDO ProcedureShare { get; set; }

        public ProcedureTimeLimitDO ProcedureTimeLimit { get; set; }
    }
}
